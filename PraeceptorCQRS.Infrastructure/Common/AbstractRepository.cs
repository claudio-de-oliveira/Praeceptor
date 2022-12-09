using Ardalis.GuardClauses;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

using Serilog;

using System.Reflection;

namespace PraeceptorCQRS.Infrastructure.Common
{
    public class AbstractRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly AbstractValidator<TEntity> _validator;

        protected AbstractRepository(DbContext context, AbstractValidator<TEntity> validator = null!)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<int> Count(Func<TEntity, bool> predicate)
        {
            var result = await Task.Run(
                () => _context.Set<TEntity>().Count(predicate));
            return result;
        }

        public async Task<List<TEntity>> QueryDefault(string sqlQuery)
        {
            var list = await Task.Run(
                () => _context.Set<TEntity>().FromSqlRaw(sqlQuery));

            return list.ToList();
        }

        protected async Task<List<TEntity>> ListDefault()
        {
            // ToList() is needed here
            var result = await Task.Run(
                () => _context.Set<TEntity>().ToList());
            Guard.Against.Null(result);
            return result;
        }

        protected async Task<List<TEntity>> ListDefault(Func<TEntity, bool> predicate)
        {
            // ToList() is needed here
            var result = await Task.Run(
                () => _context.Set<TEntity>().Where(predicate).ToList());
            Guard.Against.Null(result);
            return result;
        }

        protected async Task<List<TEntity>> PageDefault(Func<TEntity, bool> predicate, int pageStart, int pageSize)
        {
            // ToList() is needed here
            var list = await Task.Run(
                () => _context.Set<TEntity>().Where(predicate).ToList());

            // First test: The page is inside the list
            if (list.Count > pageStart + pageSize)
                return list.GetRange(pageStart, pageSize);
            // Second test: The list is smaller than page
            if (list.Count <= pageSize || pageSize == -1)
                return list;
            // Third test: The page is the end of the list
            return list.GetRange(pageStart, list.Count - pageStart);
        }

        protected async Task<List<T>> SelectDefault<T>(Func<TEntity, T> converter)
        {
            // ToList() is needed here
            var result = await Task.Run(
                () => _context.Set<TEntity>().Select(converter).ToList());
            Guard.Against.Null(result);
            return result;
        }

        protected async Task<List<T>> SelectDefault<T>(Func<TEntity, bool> predicate1, Func<TEntity, T> converter)
        {
            // ToList() is needed here
            var result = await Task.Run(
                () => _context.Set<TEntity>().Where(predicate1).Select(converter).ToList());
            Guard.Against.Null(result);
            return result;
        }

        protected async Task<TEntity?> ReadDefault(Func<TEntity, bool> predicate)
        {
            var result = await Task.Run(
                () => _context.Set<TEntity>().FirstOrDefault(predicate));
            return result;
        }

        protected async Task<TEntity?> CreateDefault(TEntity entity)
        {
            if (!ValidateModel(entity))
                return null;

            try
            {
                var result = _context.Set<TEntity>().Add(entity);
                if (result is null)
                    return null;

                await SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{nameof(CreateDefault)}({nameof(TEntity)})");
            }

            return null;
        }

        protected async Task<TEntity?> UpdateDefault(TEntity entity)
        {
            if (!ValidateModel(entity))
                return null;

            bool saveFailed;

            do
            {
                saveFailed = false;

                try
                {
                    EntityEntry<TEntity> result = _context.Set<TEntity>().Update(entity);
                    if (result is null)
                        return null;

                    await SaveChangesAsync();

                    return result.Entity;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    /*************************************************************************/
                    /* Code from https://learn.microsoft.com/pt-br/ef/ef6/saving/concurrency */
                    /*************************************************************************/

                    saveFailed = true;

                    // Get the current entity values and the values in the database
                    // as instances of the entity type
                    var entry = ex.Entries.Single();
                    var databaseValues = entry.GetDatabaseValues();
                    Guard.Against.Null(databaseValues);
                    var databaseValuesAsT = (TEntity)databaseValues.ToObject();

                    // Choose an initial set of resolved values. In this case we
                    // make the default be the values currently in the database.
                    var resolvedValuesAsT = (TEntity)databaseValues.ToObject();

                    // Have the user choose what the resolved values should be
                    HaveUserResolveConcurrency((TEntity)entry.Entity, databaseValuesAsT, resolvedValuesAsT);

                    // Update the original values with the database values and
                    // the current values with whatever the user choose.
                    entry.OriginalValues.SetValues(databaseValues);
                    entry.CurrentValues.SetValues(resolvedValuesAsT);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, $"{nameof(UpdateDefault)}({nameof(TEntity)})");
                }
            }
            while (saveFailed);

            return null;
        }

        protected async Task<TEntity?> DeleteDefault(TEntity entity)
        {
            if (!ValidateModel(entity))
                return null;

            try
            {
                var deleted = _context.Set<TEntity>().Remove(entity);
                if (deleted is null)
                    return null;

                await SaveChangesAsync();

                return deleted.Entity;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{nameof(DeleteDefault)}({nameof(TEntity)})");
            }

            return null;
        }

        protected void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = _context.Set<TEntity>().Local.Where(predicate).FirstOrDefault();
            if (local is not null)
                _context.Entry(local).State = EntityState.Detached;
        }

        private Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);

        private bool ValidateModel(TEntity entity)
        {
            try
            {
                if (entity is null)
                    throw new ArgumentNullException(nameof(entity));

                if (_validator is not null)
                {
                    var validationResult = _validator.Validate(entity);
                    if (!validationResult.IsValid)
                        return false;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{nameof(ValidateModel)}({nameof(TEntity)})");
            }

            return true;
        }

        protected virtual void HaveUserResolveConcurrency(TEntity currentValues, TEntity databaseValues, TEntity resolvedValues)
        {
            // Show the current, database, and resolved values to the user and have
            // them edit the resolved values to get the correct resolution.

            PropertyInfo[] props = typeof(TEntity).GetProperties();

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    var currentValue = prop.GetValue(currentValues);
                    var databaseValue = prop.GetValue(databaseValues);

                    prop.SetValue(resolvedValues, currentValue);

                    var resolvedValue = prop.GetValue(resolvedValues);

                    if (currentValue is not null && !currentValue.Equals(databaseValue))
                    {
                        // resolvedValue = currentValue;
                        Log.Error(
                            $"Conflito de simultaneidade (EF6) com {prop.Name}\n" +
                            $"\t- Banco de dados contém {databaseValue}\n" +
                            $"\t- Novo valor desejável: {currentValue}\n" +
                            $"\t=======> Valor gravado: {resolvedValue}");
                    }

                    prop.SetValue(resolvedValues, currentValue);
                }
            }
        }
    }
}