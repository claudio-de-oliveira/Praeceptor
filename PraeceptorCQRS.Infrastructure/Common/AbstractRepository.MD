# Classe AbstractRepository

Classe genérica para a manipulação de operações comuns relacionadas à manutenção 
de um repositório. A tabela manipulada em cada operação é dada por `_context.Set<T>()`

***

## Construtor:

```csharp
    protected AbstractRepository(
        DbContext context, 
        AbstractValidator<T> validator = null!
        )
    {
        _context = context;
        _validator = validator;
    }
```

- Modo de usar:

```csharp
    public class DataTypeRepository : AbstractRepository<DataType>, IDataTypeRepository
    {
        public DataTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            ...;
        }
        ...
    }
```

Na classe derivada `DataTypeRepository` você poderá implementar a interface 
`IDataTypeRepository` com o auxílio das operações *default* declaradas na classe base. 
Em seguida são apresentadas essas operações, bem como exemplos de utilização baseadas na classe `DataTypeRepository`.

***

### Operações

#### Count

- Retorna o número de entidades do banco de dados satisfazendo o predicado `predicate`

```csharp
    public async Task<int> Count(Func<T, bool> predicate)
    {
        var result = await Task.Run(
            () => _context.Set<T>().Count(predicate));
        return result;
    }
```

- **Modo de usar**: supondo que o tipo `DataType` tem uma propriedade `public Guid OnwerId`

```csharp
    class DataTypeRepository : AbstractRepository<DataType>
    {
        ...
        public async Task<int> GetDataTypeCountByOnwerId(Guid ownerId)
            => await Count(o => o.OnwerId == ownerId);
        public async Task<int> GetDataTypeCount()
            => await Count(o => true);
        ...
    }
```

---

#### QueryDefault

- Consulta genérica produzindo uma lista de entidades resultante do query `sql`

```csharp
    public async Task<List<T>> QueryDefault(string sql)
    {
        var list = await Task.Run(
            () => _context.Set<T>().FromSqlRaw(sql));
        return list.ToList();
    }
```

---

#### ListDefault

- Retorna **todas** as entidades da tabela de objetos `T`

```csharp
    protected async Task<List<T>> ListDefault()
    {
        // O ToList() é necessário aqui.
        var result = await Task.Run(
            () => _context.Set<T>().ToList()); 
        Guard.Against.Null(result);
        return result;
    }
```

- **Modo de usar**

```csharp
    class DataTypeRepository : AbstractRepository<DataType>
    {
        ...
        public async Task<List<DataType>> GetAllDataTypes()
            => await ListDefault();
        ...
    }
```

- Retorna as entidades da tabela de objetos `T` satisfazendo o predicado `predicate`

```csharp
    protected async Task<List<T>> ListDefault(Func<T, bool> predicate)
    {
        // O ToList() é necessário aqui.
        var result = await Task.Run(
            () => _context.Set<T>().Where(predicate).ToList());
        Guard.Against.Null(result);
        return result;
    }
```

- **Modo de usar**

```csharp
    class DataTypeRepository : AbstractRepository<DataType>
    {
        ...
        public async Task<List<DataType>> GetDataTypesByOnwerId(Guid ownerId)
            => await ListDefault(o => o.OnwerId == ownerId);
        ...
    }
```

---

#### PageDefault

- Retorna uma lista de `count` entidades satisfazendo o predicado `predicate` a partir da posição `start` da tabela

```csharp
    protected async Task<List<T>> PageDefault(Func<T, bool> predicate, int start, int count)
    {
        // O ToList() é necessário aqui.
        var list = await Task.Run(
            () => _context.Set<T>().Where(predicate).ToList());

        List<T> result;

        if (list.Count > start + count)
            result = list.GetRange(start, count);
        else if (list.Count <= count || count == -1)
            result = list;
        else
            result = list.GetRange(start, list.Count - start);
        Guard.Against.Null(result);

        return result;
    }
```

- Modo de usar:

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    public async Task<List<DataType>> GetDataTypeByOnwerPage(Guid onwer, int start, int count)
        => await PageDefault(o => o.Onwer == onwer, start, count);
    public async Task<List<DataType>> GetDataTypePage(int start, int count)
        => await PageDefault(o => true, start, count);
    ...
}
```


---

#### SelectDefault

- Retorna a lista de entidades `TTarget`, obtidas a partir das entidades `TSource` 
que satisfazem o predicado `predicate` - `fransform` é a função de conversão de `TSource` para `TTarget`

```csharp
    protected async Task<List<TTarget>> SelectDefault<TTarget>(Func<TSourse, bool> predicate, Func<TSourse, TTarget> fransform)
    {
        // O ToList() é necessário aqui.
        var result = await Task.Run(
            () => _context.Set<TSourse>().Where(predicate).Select(fransform).ToList());
        Guard.Against.Null(result);
        return result;
    }
```

- Retorna a lista de entidades `TTarget`, obtidas a partir das entidades `TSource` - `fransform` é a função de conversão de `TSource` para `TTarget`

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    protected async Task<List<TTarget>> SelectDefault<TTarget>(Func<TSource, TTarget> transform)
    {
        // O ToList() é necessário aqui.
        var result = await Task.Run(
            () => _context.Set<T>().Select(transform).ToList());
        Guard.Against.Null(result);
        return result;
    }
    ...
}
```

---

#### ReadDefault

- Retorna a entidade satisfazendo o predicado `predicate`

```csharp
    protected async Task<T?> ReadDefault(Func<T, bool> predicate)
    {
        var result = await Task.Run(
            () => _context.Set<T>().FirstOrDefault(predicate));
        return result;
    }
```

- Modo de usar:

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    public async Task<DataType?> GetDataTypeById(Guid id)
        => await ReadDefault(o => o.Id == id);
    public async Task<DataType?> GetDataTypeByCode(string code)
        => await ReadDefault(o => string.Compare(o.Code, code, true) == 0);
    ...
}
```

---

#### CreateDefault

- Insere `entity` na tabela correspondente

```csharp
    protected async Task<T?> CreateDefault(T entity)
    {
        if (!ValidateModel(entity))
            return null;

        try
        {
            var result = _context.Set<T>().Add(entity);
            if (result is null)
                return null;

            await SaveChangesAsync();

            return result.Entity;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{nameof(CreateDefault)}({nameof(T)})");
        }

        return null;
    }
```

- Modo de usar:

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    public async Task<DataType?> CreateDataType(DataType entityToCreate)
        => await CreateDefault(entityToCreate);
    ...
}
```

---

### UpdateDefault

- Atualiza `entity` na tabela correspondente

```csharp
    protected async Task<T?> UpdateDefault(T entity)
    {
        if (!ValidateModel(entity))
            return null;

        bool saveFailed;

        do
        {
            saveFailed = false;

            try
            {
                EntityEntry<T> result = _context.Set<T>().Update(entity);

                if (result is null)
                    return null;

                await SaveChangesAsync();

                return result.Entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                saveFailed = true;

                // Get the current entity values and the values in the database
                // as instances of the entity type
                var entry = ex.Entries.Single();
                var databaseValues = entry.GetDatabaseValues();
                Guard.Against.Null(databaseValues);
                var databaseValuesAsT = (T)databaseValues.ToObject();

                // Choose an initial set of resolved values. In this case we
                // make the default be the values currently in the database.
                var resolvedValuesAsT = (T)databaseValues.ToObject();

                // Have the user choose what the resolved values should be
                HaveUserResolveConcurrency((T)entry.Entity, databaseValuesAsT, resolvedValuesAsT);

                // Update the original values with the database values and
                // the current values with whatever the user choose.
                entry.OriginalValues.SetValues(databaseValues);
                entry.CurrentValues.SetValues(resolvedValuesAsT);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{nameof(UpdateDefault)}({nameof(T)})");
            }
        }
        while (saveFailed);

        return null;
    }
```

> OBS: Para mais informações sobre o tratamento da excessão DbUpdateConcurrencyException, ver <https://learn.microsoft.com/pt-br/ef/ef6/saving/concurrency>

- Modo de usar:

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    public async Task UpdateDataType(DataType entityToUpdate)
    {
        DetachLocal(o => o.Id == entityToUpdate.Id);
        await UpdateDefault(entityToUpdate);
    }
    ...
}
```

---

#### DeleteDefault

- Exclui `entity` da tabela correspondente

```csharp
    protected async Task<T?> DeleteDefault(T entity)
    {
        if (!ValidateModel(obj))
            return null;

        try
        {
            var deleted = _context.Set<T>().Remove(obj);
            if (deleted is null)
                return null;

            await SaveChangesAsync();

            return deleted.Entity;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{nameof(DeleteDefault)}({nameof(T)})");
        }

        return null;
    }
```

- Modo de usar:

```csharp
class DataTypeRepository : AbstractRepository<DataType>
{
    ...
    public async Task DeleteDataType(Guid id)
    {
        var entityToDelete = await ReadDefault(o => o.Id == id);
        if (entityToDelete is not null)
            await DeleteDefault(entityToDelete);
    }
    ...
}
```

---

