using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;

using Newtonsoft.Json;

namespace Document.App.SeedData.SubSubSections
{
    public static class InitializeSubSubSectionTable
    {
        private static readonly List<BookEntity> entities = new()
        {
            new BookEntity
            {
                Title = "Matemática, física e química", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Ciências sociais e humanas", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de sistemas elétricos", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de sistemas mecânicos", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de sistemas computacionais", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de sistemas de controle, instrumentação e automação", 
                Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de práticas integradoras", 
                Text = ""
            }
        };

        public static async Task<Dictionary<int, Guid>> Initialize(Guid instituteId, ISubSubSectionListService service)
        {
            Dictionary<int, Guid> subSubSections = new();

            var count = await service.GetEntitiesCount(instituteId);

            if (count == 0)
            {
                int id = 1;

                foreach (var entity in entities)
                {
                    var response = await service.CreateEntity(new CreateEntityRequest(
                        entity.Title,
                        entity.Text,
                        instituteId,
                        "Testador"
                    ));

                    if (response.IsSuccessStatusCode)
                    {
                        var section = JsonConvert.DeserializeObject<BookEntity>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(section);
                        subSubSections.Add(id++, section.Id);
                    }
                }
            }

            return subSubSections;
        }
    }
}
