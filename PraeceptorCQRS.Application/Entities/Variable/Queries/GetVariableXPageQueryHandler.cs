using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariableXPageQueryHandler
        : IRequestHandler<GetVariableXPageQuery, ErrorOr<VariableXPageResult>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVariableXRepository _variableRepository;

        public GetVariableXPageQueryHandler(IHoldingRepository holdingRepository, IInstituteRepository instituteRepository, ICourseRepository courseRepository, IVariableXRepository variableRepository)
        {
            _holdingRepository = holdingRepository;
            _instituteRepository = instituteRepository;
            _courseRepository = courseRepository;
            _variableRepository = variableRepository;
        }

        public async Task<ErrorOr<VariableXPageResult>> Handle(GetVariableXPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var holding = await _holdingRepository.GetHoldingById(request.HoldingId);
            if (holding is null)
                return Domain.Errors.Error.Holding.NotFound;
            var list1 = await _variableRepository.GetVariablesByGroupId(request.HoldingId, null);
            list1.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@ACRONIMO",
                    Value = holding.Acronym,
                    IsDeletable = false
                }
            );
            list1.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@NOME",
                    Value = holding.Name,
                    IsDeletable = false
                }
            );
            list1.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@ENDERECO",
                    Value = holding.Address,
                    IsDeletable = false
                }
            );

            var institute = await _instituteRepository.GetInstituteById(request.InstituteId);
            if (institute is null)
                return Domain.Errors.Error.Institute.NotFound;
            var list2 = await _variableRepository.GetVariablesByGroupId(request.InstituteId, null);
            list2.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@ACRONIMO",
                    Value = institute.Acronym,
                    IsDeletable = false
                }
            );
            list2.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@NOME",
                    Value = institute.Name,
                    IsDeletable = false
                }
            );
            list2.Add(
                new VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@ENDERECO",
                    Value = institute.Address,
                    IsDeletable = false
                }
            );

            var course = await _courseRepository.GetCourseById(request.CourseId);
            if (course is null)
                return Domain.Errors.Error.Course.NotFound;
            var list3 = await _variableRepository.GetVariablesByGroupId(request.CourseId, request.Curriculum);
            list3.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@CODIGO",
                    Value = course.Code,
                    IsDeletable = false
                }
            );
            list3.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@NOME",
                    Value = course.Name,
                    IsDeletable = false
                }
            );
            list3.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@AC",
                    Value = course.AC.ToString(),
                    IsDeletable = false
                }
            );
            list3.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@CURSO",
                    GroupId = course.Id,
                    Curriculum = request.Curriculum,
                    VariableName = "@PERIODOS",
                    Value = course.NumberOfSeasons.ToString(),
                    IsDeletable = false
                }
            );
            if (!string.IsNullOrWhiteSpace(course.Telephone))
            {
                list3.Add(
                    new Domain.Entities.VariableX()
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "@CURSO",
                        GroupId = course.Id,
                        Curriculum = request.Curriculum,
                        VariableName = "@TELEFONE",
                        Value = course.Telephone,
                        IsDeletable = false
                    }
                );
            }
            if (!string.IsNullOrWhiteSpace(course.Email))
            {
                list3.Add(
                    new Domain.Entities.VariableX()
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "@CURSO",
                        GroupId = course.Id,
                        Curriculum = request.Curriculum,
                        VariableName = "@EMAIL",
                        Value = course.Email,
                        IsDeletable = false
                    }
                );
            }

            var list4 = list3;
            list4.AddRange(list2);
            list4.AddRange(list1);

            Console.WriteLine(request.NameFilter ?? "Não tem");
            Console.WriteLine(request.ValueFilter ?? "Não tem");

            var filteredList = new List<VariableX>();
            bool isFiltered = false;

            string? nameFilter = request.NameFilter;
            string? valueFilter = request.ValueFilter;

            foreach (var entity in list4)
            {
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(nameFilter, $"{entity.GroupName}.{entity.VariableName}"))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(valueFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(valueFilter, entity.Value))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
            }

            var variables = isFiltered ? filteredList : list4;

            // if (!string.IsNullOrWhiteSpace(request.NameFilter))
            //     list4 = list4.Where(o => 
            //         o.GroupName.ToUpper().Contains(request.NameFilter.ToUpper()) 
            //         || o.VariableName.ToUpper().Contains(request.NameFilter.ToUpper())
            //         || (o.Value is not null && request.ValueFilter is not null 
            //             && o.Value.ToUpper().Contains(request.ValueFilter.ToUpper())
            //             )
            //         ).ToList();
            // if (!string.IsNullOrWhiteSpace(request.ValueFilter))
            //     list4 = list4.Where(o =>
            //         o.GroupName.ToUpper().Contains(request.NameFilter.ToUpper())
            //         || o.VariableName.ToUpper().Contains(request.NameFilter.ToUpper())
            //         || (o.Value is not null && request.ValueFilter is not null
            //             && o.Value.ToUpper().Contains(request.ValueFilter.ToUpper())
            //             )
            //         ).ToList();
            variables = SortBy(variables, request.Sort, request.Ascending);

            int page = request.Start;
            int pageSize = request.Count;

            int numberOfPages = (variables.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<VariableX> entities = variables.Count > (page + 1) * pageSize
                ? variables.GetRange(page * pageSize, pageSize)
                : variables.GetRange(page * pageSize, variables.Count - page * pageSize);

            return new VariableXPageResult(new PageOf<VariableX>(
                // CurrentPage
                page,
                // Size
                pageSize,
                // PreviousPage
                page - 1,
                // NextPage
                nextPage,
                // NumberOfPages
                numberOfPages,
                // List
                entities
                ));
        }

        private static List<VariableX> SortBy(List<VariableX> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Name" => Global.SortList(list, x => $"{x.GroupName}.{x.VariableName}", ascending),
                "Value" => Global.SortList(list, x => x.Value, ascending),
                _ => list
            };
        }
    }
}
