using Ardalis.GuardClauses;

using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;
using PraeceptorCQRS.Application.Entities.Component.Queries;
using PraeceptorCQRS.Application.Entities.Course.Common;
using PraeceptorCQRS.Application.Entities.Course.Queries;
using PraeceptorCQRS.Application.Entities.FileStream.Common;
using PraeceptorCQRS.Application.Entities.FileStream.Queries;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Entities.Holding.Queries;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Entities.Institute.Queries;
using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Entities.SocialBody.Queries;
using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Entities.ToWord.Models;
using PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC;
using PraeceptorCQRS.Application.Entities.ToWord.Queries;
using PraeceptorCQRS.Application.Persistence;

using Serilog;

namespace PraeceptorCQRS.Application.Entities.ToWord.Commands
{
    public class CreateDocxCommandHandler
        : IRequestHandler<CreateDocxCommand, ErrorOr<SqlDocxInfoResult>>
    {
        private readonly ISender _mediator;
        private readonly IFileStreamRepository _fileRepository;
        private readonly IDocxStreamRepository _docxRepository;
        private readonly ISimpleTableRepository _tableRepository;

        public CreateDocxCommandHandler(ISender mediator, IFileStreamRepository fileRepository, IDocxStreamRepository docxRepository, ISimpleTableRepository tableRepository)
        {
            _mediator = mediator;
            _fileRepository = fileRepository;
            _docxRepository = docxRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SqlDocxInfoResult>> Handle(CreateDocxCommand request, CancellationToken cancellationToken)
        {
            // Console.WriteLine("Lendo informações sobre o curso...");

            var courseResult =
                await ReadCourse(_mediator, request.CourseId, cancellationToken);
            if (courseResult.IsError)
                return courseResult.Errors;
            var course = courseResult.Value.Course;

            var instituteResult =
                await ReadInstitute(_mediator, course.InstituteId, cancellationToken);
            if (instituteResult.IsError)
                return instituteResult.Errors;
            var institute = instituteResult.Value.Institute;

            var holdingResult =
                await ReadHolding(_mediator, institute.HoldingId, cancellationToken);
            if (holdingResult.IsError)
                return holdingResult.Errors;
            var holding = holdingResult.Value.Holding;

            var templateResult =
                await ReadTemplate(_mediator, request.TemplateId, cancellationToken);
            if (templateResult.IsError)
                return templateResult.Errors;
            var template = templateResult.Value.FileStream;

            // Console.WriteLine("Lendo informações sobre o documento...");
            PPCModel ppc = new(holding, institute, course, template, _fileRepository, _tableRepository);

            foreach (var v in request.GroupValues)
                ppc.variables.Add(v.Key, v.Value);

            var documentTextResult =
                await ConvertDocumentToText(_mediator, request.DocumentId, cancellationToken);
            if (documentTextResult.IsError)
                return documentTextResult.Errors;
            ppc.MainText = documentTextResult.Value.Text;

            if (!string.IsNullOrWhiteSpace(ppc.MainText))
            {
                ppc.Title = documentTextResult.Value.Title;

                var syllabusResult =
                    await ReadSyllabus(_mediator, request.CourseId, request.Curriculum, cancellationToken);
                if (syllabusResult.IsError)
                    return syllabusResult.Errors;
                ppc.Syllabus = syllabusResult.Value.Components;

                ppc.PlannerText = await ReadPeas(_mediator, ppc.Syllabus, cancellationToken);

                var socialBodyResult =
                    await ReadSocialBody(_mediator, request.CourseId, cancellationToken);
                if (socialBodyResult.IsError)
                    return socialBodyResult.Errors;
                ppc.SocialBody = socialBodyResult.Value.List;

                // var variablesQuery = new GetVariablesByCourseQuery(
                //     request.CourseId
                //     );
                // ErrorOr<VariablesResult> variablesResult = await _mediator.Send(variablesQuery);
                // if (variablesResult.IsError)
                //     return variablesResult.Errors;
                // ppc.variables = variablesResult.Value.Variables;

                ppc.InitializeTables();
            }
            else
            {
                Log.Error("Não há nada neste documento!");
                ppc.MainText = "\\paragraph[bold]{Não há nada neste documento!}";
            }

            return await TryConvertToDocxAndSave(
                ppc,
                request.FileId,
                request.Description,
                request.CreatedBy,
                _docxRepository,
                cancellationToken);
        }

        private static async Task<ErrorOr<CourseResult>> ReadCourse(
            ISender mediator,
            Guid courseId,
            CancellationToken cancellationToken)
        {
            var query = new GetCourseByIdQuery(courseId);
            ErrorOr<CourseResult> courseResult =
                await mediator.Send(query, cancellationToken);
            if (courseResult.IsError)
            {
                foreach (var error in courseResult.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return courseResult.Errors;
            }
            return courseResult.Value;
        }

        private static async Task<ErrorOr<InstituteResult>> ReadInstitute(
            ISender mediator,
            Guid instituteId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Lendo informações sobre o instituto...");
            var query = new GetInstituteByIdQuery(instituteId);
            ErrorOr<InstituteResult> result =
                await mediator.Send(query, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<ErrorOr<HoldingResult>> ReadHolding(
            ISender mediator,
            Guid holdingId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Lendo informações sobre a holding...");
            var query = new GetHoldingByIdQuery(holdingId);
            ErrorOr<HoldingResult> result =
                await mediator.Send(query, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<ErrorOr<FileResult>> ReadTemplate(
            ISender mediator,
            Guid templateId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Lendo informações sobre o template...");
            var query = new GetSqlFileStreamByIdQuery(templateId);
            ErrorOr<FileResult> result =
                await mediator.Send(query, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<ErrorOr<ComponentListResult>> ReadSyllabus(
            ISender mediator,
            Guid courseId,
            int curriculum,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Lendo informações sobre a estrutura curricular...");
            var syllabusQuery = new GetComponentByCourseAndCurriculumQuery(
                courseId,
                curriculum
                );
            ErrorOr<ComponentListResult> result =
                await mediator.Send(syllabusQuery, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<ErrorOr<SocialBodyEntryListResult>> ReadSocialBody(
            ISender mediator,
            Guid courseId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("Lendo informações sobre o corpo social...");
            var query = new GetSocialBodyEntriesByCourseQuery(courseId);
            ErrorOr<SocialBodyEntryListResult> result =
                await mediator.Send(query, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<Dictionary<Guid, string>> ReadPeas(
            ISender mediator,
            List<Domain.Entities.Component> components,
            CancellationToken cancellationToken)
        {
            Dictionary<Guid, string> peas = new();

            foreach (var component in components)
            {
                Log.Warning($"Obtendo PEA de {component.Class.Name}");

                var peaTextQuery = new GetPeaTextByClassIdQuery(component.ClassId, component.Season);

                ErrorOr<PlannerTextResult> plannerTextResult =
                    await mediator.Send(peaTextQuery, cancellationToken);
                if (!peas.ContainsKey(component.ClassId))
                {
                    if (plannerTextResult.IsError)
                        peas.Add(component.ClassId, $"\\paragraph[JUSTIFICATION=both]{{\\run[BOLD;color=8388608]{{Pea indefinido de {component.Class.Name}}}}}\n");
                    else
                        peas.Add(component.ClassId, plannerTextResult.Value.Text);
                }
                else
                {
                    Log.Error($"A disciplina {component.Class.Name} é atribuída mais de uma vez ao currículo.");
                }
            }

            return peas;
        }

        private static async Task<ErrorOr<DocumentTextResult>> ConvertDocumentToText(
            ISender mediator,
            Guid documentId,
            CancellationToken cancellationToken
            )
        {
            var query = new GetDocumentTextByIdQuery(documentId);
            ErrorOr<DocumentTextResult> result =
                await mediator.Send(query, cancellationToken);
            if (result.IsError)
            {
                foreach (var error in result.Errors)
                    Log.Error($"{error.Code}: {error.Description}");
                return result.Errors;
            }
            return result.Value;
        }

        private static async Task<ErrorOr<SqlDocxInfoResult>> TryConvertToDocxAndSave(
            PPCModel ppc,
            Guid fileId,
            string description,
            string createdBy,
            IDocxStreamRepository docxRepository,
            CancellationToken cancellationToken)
        {
            WordDocEnvironment docEnvironment = new(ppc);

            WordDocParser parser = new(docEnvironment);
            var result = parser.Parse(ppc.MainText, null);

            if (result is not null)
            {
                ppc.SaveToFile();

                long count = ppc._stream.Length;
                byte[] data = new byte[count];
                var buffer = ppc.GetData();
                for (int i = 0; i < count; i++)
                    data[i] = buffer[i];

                // Buffer.BlockCopy(ppc.GetData(), 0, data, 0, (int)count);

                var entity = new Domain.Entities.SqlDocxStream(fileId)
                {
                    Title = ppc.Title,
                    Description = description,
                    Data = data,
                    InstituteId = ppc.Institute.Id,
                    ContentType = "application/vnd.ms-word",
                    DateCreated = DateTime.UtcNow,
                    CreatedBy = createdBy
                };

                if (cancellationToken.IsCancellationRequested)
                    return Domain.Errors.Error.Docx.Canceled;

                Guard.Against.Null(entity);
                var created = await docxRepository.StoreDocx(entity);
                if (created is null)
                {
                    Log.Error("Erro ao tentar salvar o documento");
                    return Domain.Errors.Error.Docx.DataBaseError;
                }

                return new SqlDocxInfoResult(fileId);
            }
            else
            {
                Log.Error("Erro sintático");
                // Log.Error(ppc.MainText);
                return Domain.Errors.Error.Parser.Syntax;
            }
        }

        // private byte[] ConvertToStream(byte[] bytes)
        // {
        //     var memory = new MemoryStream();
        //
        //     using (var stream = new System.IO.FileStream("tmp", FileMode.Open))
        //     {
        //         await stream.CopyToAsync(memory, cancellationToken);
        //     }
        //     memory.Position = 0;
        // }
    }
}