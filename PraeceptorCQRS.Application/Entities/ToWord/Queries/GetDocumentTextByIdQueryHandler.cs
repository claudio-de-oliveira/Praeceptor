using Ardalis.GuardClauses;

using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetDocumentTextByIdQueryHandler
        : IRequestHandler<GetDocumentTextByIdQuery, ErrorOr<DocumentTextResult>>
    {
        private readonly IListRepository _listRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;
        private readonly ISubSubSectionRepository _subSubSectionRepository;

        public GetDocumentTextByIdQueryHandler(
            IListRepository listRepository,
            IDocumentRepository documentRepository,
            IChapterRepository chapterRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            ISubSubSectionRepository subSubSectionRepository
            )
        {
            _listRepository = listRepository;
            _documentRepository = documentRepository;
            _chapterRepository = chapterRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _subSubSectionRepository = subSubSectionRepository;
        }

        public async Task<ErrorOr<DocumentTextResult>> Handle(GetDocumentTextByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _documentRepository.GetDocumentById(request.DocumentId);
            Guard.Against.Null(entity);
            string text = entity.Text ?? "";

            var position = await _listRepository.GetFirstPosition(request.DocumentId);

            while (position is not null)
            {
                text += await ReadChapterText(position.SecondEntityId);
                position = await _listRepository.GetAt(position.NextNodeId);
            }

            text = RemoveEspacosDesnecessarios(text);

            return new DocumentTextResult(request.DocumentId, entity.Title, text);
        }

        public async Task<string> ReadChapterText(Guid entityId)
        {
            var entity = await _chapterRepository.GetChapterById(entityId);
            Guard.Against.Null(entity);

            string text = $"\\chapter{{{entity.Title}}}\n\n{entity.Text}\n\n";

            var position = await _listRepository.GetFirstPosition(entityId);

            while (position is not null)
            {
                text += await ReadSectionText(position.SecondEntityId);
                position = await _listRepository.GetAt(position.NextNodeId);
            }

            return text;
        }

        public async Task<string> ReadSectionText(Guid entityId)
        {
            var entity = await _sectionRepository.GetSectionById(entityId);
            Guard.Against.Null(entity);

            string text = $"\\section{{{entity.Title}}}\n\n{entity.Text}\n\n";

            var position = await _listRepository.GetFirstPosition(entity.Id);

            while (position is not null)
            {
                text += await ReadSubSectionText(position.SecondEntityId);
                position = await _listRepository.GetAt(position.NextNodeId);
            }

            return text;
        }

        public async Task<string> ReadSubSectionText(Guid entityId)
        {
            var entity = await _subSectionRepository.GetSubSectionById(entityId);
            Guard.Against.Null(entity);

            string text = $"\\subsection{{{entity.Title}}}\n\n{entity.Text}\n\n";

            var position = await _listRepository.GetFirstPosition(entityId);

            while (position is not null)
            {
                text += await ReadSubSubSectionText(position.SecondEntityId);
                position = await _listRepository.GetAt(position.NextNodeId);
            }

            return text;
        }

        public async Task<string> ReadSubSubSectionText(Guid entityId)
        {
            var entity = await _subSubSectionRepository.GetSubSubSectionById(entityId);
            Guard.Against.Null(entity);

            string text = $"\\subsubsection{{{entity.Title}}}\n\n{entity.Text}\n\n";

            return text;
        }

        private static string RemoveEspacosDesnecessarios(string str)
        {
            int state = 0;
            int pos = 0;

            string text = "";

            while (true)
            {
                if (pos >= str.Length)
                    break;

                switch (state)
                {
                    case 0:
                        if (char.IsWhiteSpace(str[pos]))
                        {
                            state = 3;
                            pos++;
                            break;
                        }
                        if (str[pos] == ';')
                        {
                            state = 1;
                            pos++;
                            break;
                        }
                        text += str[pos];
                        state = 0;
                        pos++;
                        break;

                    case 1:
                        if (str[pos] == ';')
                        {
                            state = 2;
                            pos++;
                            break;
                        }
                        pos--;
                        text += str[pos];
                        state = 0;
                        pos++;
                        break;

                    case 2:
                        if (str[pos] == '\n')
                        {
                            state = 0;
                            pos++;
                            break;
                        }
                        state = 2;
                        pos++;
                        break;

                    case 3:
                        if (!char.IsWhiteSpace(str[pos]))
                        {
                            text += ' ';
                            state = 0;
                            break;
                        }
                        // Console.WriteLine("Eliminando espaços...");
                        state = 3;
                        pos++;
                        break;
                }
            }

            return text;
        }
    }
}