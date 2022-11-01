using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Chapter;
using PraeceptorCQRS.Contracts.Entities.Document;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Section;
using PraeceptorCQRS.Contracts.Entities.SubSection;
using PraeceptorCQRS.Contracts.Entities.SubSubSection;
using PraeceptorCQRS.Presentation.Document.Tester.Services;
using PraeceptorCQRS.Utilities;

using Xunit;

namespace PraeceptorCQRS.Presentation.Document.Tester
{
    public class UnitTest1
    {
        private static readonly DocumentHttpService httpDocumentService
            = new("https://localhost:7001/",
                  "",
                  new HttpClient(new HttpClientHandler
                  {
                      // Bypass the SSH certificate
                      ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                  })
                );
        private static readonly ChapterHttpService httpChapterService
            = new("https://localhost:7001/",
                  "",
                  new HttpClient(new HttpClientHandler
                  {
                      // Bypass the SSH certificate
                      ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                  })
                );
        private static readonly SectionHttpService httpSectionService
            = new("https://localhost:7001/",
                  "",
                  new HttpClient(new HttpClientHandler
                  {
                      // Bypass the SSH certificate
                      ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                  })
                );
        private static readonly SubSectionHttpService httpSubSectionService
            = new("https://localhost:7001/",
                  "",
                  new HttpClient(new HttpClientHandler
                  {
                      // Bypass the SSH certificate
                      ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                  })
                );
        private static readonly SubSubSectionHttpService httpSubSubSectionService
            = new("https://localhost:7001/",
                  "",
                  new HttpClient(new HttpClientHandler
                  {
                      // Bypass the SSH certificate
                      ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                  })
                );
        private readonly Guid instituteId;
        private Guid documentId;

        public UnitTest1()
        {
            instituteId = Guid.Parse("909589e0-9c35-4758-1492-08da86d4fb9e");
        }

#pragma warning disable CS8602
        [Fact]
        public async Task TestDocument()
        {
            var response = await httpDocumentService.CreateDocument(
                new CreateDocumentRequest(
                    "Plano Pedagógico do Curso de Engenharia Elétrica",
                    "Claudio de Oliveira",
                    instituteId
                    )
                );
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            var document = JsonConvert.DeserializeObject<DocumentResponse>(await response.Content.ReadAsStringAsync());

            documentId = document.Id;

            ChapterResponse?[] chapters = new ChapterResponse[5];
            Guid[] nodes = new Guid[5];

            for (int i = 0; i < chapters.Length; i++)
            {
                response = await httpChapterService.CreateChapter(
                    new CreateChapterRequest(
                        LoremIpsum.Generate(3, 6, 1, 1, 1),
                        LoremIpsum.Generate(5, 50, 1, 7, 8),
                        instituteId
                        )
                    ); ;
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                var message = JsonConvert.DeserializeObject<ChapterResponse>(await response.Content.ReadAsStringAsync());
                Assert.True(message is ChapterResponse);

                await TestChapter(message.Id);

                chapters[i] = message;
            }

            // Insere o primeiro capítulo no documento
            response = await httpDocumentService.CreateFirstChapter(
                new CreateFirstNodeRequest(
                    document.Id,
                    document.Id,
                    chapters[0].Id
                    )
                );
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.True(response.IsSuccessStatusCode);
            var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
            nodes[0] = node.Id;

            // Insere os demais capítulos no documento
            for (int i = 1; i < chapters.Length; i++)
            {
                response = await httpDocumentService.InsertChapterAfterPosition(
                    new InsertNodeRequest(
                        node.Id,
                        document.Id,
                        document.Id,
                        chapters[i].Id
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                nodes[i] = node.Id;
            }

            // Caminhamento para frente
            node = await httpDocumentService.GetFirstChapterPosition(document.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == document.Id && node.SecondEntityId == chapters[0].Id);

            for (int i = 1; i < chapters.Length; i++)
            {
                node = await httpDocumentService.GetNextChapterPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == document.Id && node.SecondEntityId == chapters[i].Id);
            }

            // Caminhamento para tráz
            node = await httpDocumentService.GetLastChapterPosition(document.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == document.Id && node.SecondEntityId == chapters[^1].Id);

            for (int i = chapters.Length - 2; i >= 0; i--)
            {
                node = await httpDocumentService.GetPreviousChapterPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == document.Id && node.SecondEntityId == chapters[i].Id);
            }

            // // Remoção dos capítulos
            // // Meio
            // response = await httpDocumentService.DeleteChapterAt(nodes[2]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[2] = Guid.Empty;
            // // Primeiro
            // response = await httpDocumentService.DeleteChapterAt(nodes[0]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[0] = Guid.Empty;
            // // Último
            // response = await httpDocumentService.DeleteChapterAt(nodes[^1]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[^1] = Guid.Empty;
            // // Demais
            // for (int i = 0; i < chapters.Length; i++)
            // {
            //     if (nodes[i] != Guid.Empty)
            //     {
            //         response = await httpDocumentService.DeleteChapterAt(nodes[i]);
            //         Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            //     }
            // }
            // 
            // // Exclusão dos capítulos de teste
            // for (int i = 0; i < chapters.Length; i++)
            // {
            //     response = await httpChapterService.DeleteChapter(chapters[i].Id);
            //     Assert.True(response.IsSuccessStatusCode);
            // }
            // 
            // // Exclusão dos documentos de teste
            // response = await httpDocumentService.DeleteDocument(document.Id);
            // Assert.True(response.IsSuccessStatusCode);
        }

        public async Task TestChapter(Guid chapterId)
        {
            var chapter = await httpChapterService.GetChapter(
                chapterId
                );
            Assert.NotNull(chapter);

            SectionResponse?[] sections = new SectionResponse[5];
            Guid[] nodes = new Guid[5];

            HttpResponseMessage response;

            for (int i = 0; i < sections.Length; i++)
            {
                response = await httpSectionService.CreateSection(
                    new CreateSectionRequest(
                        LoremIpsum.Generate(3, 6, 1, 1, 1),
                        LoremIpsum.Generate(5, 50, 1, 7, 8),
                        instituteId
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                var message = JsonConvert.DeserializeObject<SectionResponse>(await response.Content.ReadAsStringAsync());
                Assert.True(message is SectionResponse);

                await TestSection(message.Id);

                sections[i] = message;
            }

            // Insere o primeiro capítulo no documento
            response = await httpChapterService.CreateFirstSection(
                new CreateFirstNodeRequest(
                    chapter.Id,
                    documentId,
                    sections[0].Id
                    )
                );
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.True(response.IsSuccessStatusCode);
            var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
            nodes[0] = node.Id;

            // Insere os demais capítulos no documento
            for (int i = 1; i < sections.Length; i++)
            {
                response = await httpDocumentService.InsertChapterAfterPosition(
                    new InsertNodeRequest(
                        node.Id,
                        chapter.Id,
                        documentId,
                        sections[i].Id
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                nodes[i] = node.Id;
            }

            // Caminhamento para frente
            node = await httpChapterService.GetFirstSectionPosition(chapter.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == chapter.Id && node.SecondEntityId == sections[0].Id);

            for (int i = 1; i < sections.Length; i++)
            {
                node = await httpChapterService.GetNextSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == chapter.Id && node.SecondEntityId == sections[i].Id);
            }

            // Caminhamento para tráz
            node = await httpChapterService.GetLastSectionPosition(chapter.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == chapter.Id && node.SecondEntityId == sections[^1].Id);

            for (int i = sections.Length - 2; i >= 0; i--)
            {
                node = await httpChapterService.GetPreviousSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == chapter.Id && node.SecondEntityId == sections[i].Id);
            }

            // // Remoção dos capítulos
            // // Meio
            // response = await httpChapterService.DeleteSectionAt(nodes[2]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[2] = Guid.Empty;
            // // Primeiro
            // response = await httpChapterService.DeleteSectionAt(nodes[0]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[0] = Guid.Empty;
            // // Último
            // response = await httpChapterService.DeleteSectionAt(nodes[^1]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[^1] = Guid.Empty;
            // // Demais
            // for (int i = 0; i < sections.Length; i++)
            // {
            //     if (nodes[i] != Guid.Empty)
            //     {
            //         response = await httpChapterService.DeleteSectionAt(nodes[i]);
            //         Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            //     }
            // }
            // 
            // // Exclusão dos capítulos de teste
            // for (int i = 0; i < sections.Length; i++)
            // {
            //     response = await httpSectionService.DeleteSection(sections[i].Id);
            //     Assert.True(response.IsSuccessStatusCode);
            // }
        }

        public async Task TestSection(Guid sectionId)
        {
            var section = await httpSectionService.GetSection(
                sectionId
                );
            Assert.NotNull(section);

            SubSectionResponse?[] subsections = new SubSectionResponse[5];
            Guid[] nodes = new Guid[5];

            HttpResponseMessage response;

            for (int i = 0; i < subsections.Length; i++)
            {
                response = await httpSubSectionService.CreateSubSection(
                    new CreateSubSectionRequest(
                        LoremIpsum.Generate(3, 6, 1, 1, 1),
                        LoremIpsum.Generate(5, 50, 1, 7, 8),
                        instituteId
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                var message = JsonConvert.DeserializeObject<SubSectionResponse>(await response.Content.ReadAsStringAsync());
                Assert.True(message is SubSectionResponse);

                await TestSubSection(message.Id);

                subsections[i] = message;
            }

            // Insere o primeiro capítulo no documento
            response = await httpSectionService.CreateFirstSubSection(
                new CreateFirstNodeRequest(
                    section.Id,
                    documentId,
                    subsections[0].Id
                    )
                );
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.True(response.IsSuccessStatusCode);
            var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
            nodes[0] = node.Id;

            // Insere os demais capítulos no documento
            for (int i = 1; i < subsections.Length; i++)
            {
                response = await httpSectionService.InsertSubSectionAfterPosition(
                    new InsertNodeRequest(
                        node.Id,
                        section.Id,
                        documentId,
                        subsections[i].Id
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                nodes[i] = node.Id;
            }

            // Caminhamento para frente
            node = await httpSectionService.GetFirstSubSectionPosition(section.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == section.Id && node.SecondEntityId == subsections[0].Id);

            for (int i = 1; i < subsections.Length; i++)
            {
                node = await httpSectionService.GetNextSubSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == section.Id && node.SecondEntityId == subsections[i].Id);
            }

            // Caminhamento para tráz
            node = await httpSectionService.GetLastSubSectionPosition(section.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == section.Id && node.SecondEntityId == subsections[^1].Id);

            for (int i = subsections.Length - 2; i >= 0; i--)
            {
                node = await httpSectionService.GetPreviousSubSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == section.Id && node.SecondEntityId == subsections[i].Id);
            }

            // // Remoção dos capítulos
            // // Meio
            // response = await httpSectionService.DeleteSubSectionAt(nodes[2]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[2] = Guid.Empty;
            // // Primeiro
            // response = await httpSectionService.DeleteSubSectionAt(nodes[0]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[0] = Guid.Empty;
            // // Último
            // response = await httpSectionService.DeleteSubSectionAt(nodes[^1]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[^1] = Guid.Empty;
            // // Demais
            // for (int i = 0; i < subsections.Length; i++)
            // {
            //     if (nodes[i] != Guid.Empty)
            //     {
            //         response = await httpSectionService.DeleteSubSectionAt(nodes[i]);
            //         Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            //     }
            // }
            // 
            // // Exclusão dos capítulos de teste
            // for (int i = 0; i < subsections.Length; i++)
            // {
            //     response = await httpSubSectionService.DeleteSubSection(subsections[i].Id);
            //     Assert.True(response.IsSuccessStatusCode);
            // }
        }

        public async Task TestSubSection(Guid subSectionId)
        {
            var subsection = await httpSubSectionService.GetSubSection(
                subSectionId
                );
            Assert.NotNull(subsection);

            SubSubSectionResponse?[] subsubsections = new SubSubSectionResponse[5];
            Guid[] nodes = new Guid[5];

            HttpResponseMessage response;

            for (int i = 0; i < subsubsections.Length; i++)
            {
                response = await httpSubSubSectionService.CreateSubSubSection(
                    new CreateSubSubSectionRequest(
                        LoremIpsum.Generate(3, 6, 1, 1, 1),
                        LoremIpsum.Generate(5, 50, 1, 7, 8),
                        instituteId
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                var message = JsonConvert.DeserializeObject<SubSubSectionResponse>(await response.Content.ReadAsStringAsync());
                Assert.True(message is SubSubSectionResponse);

                subsubsections[i] = message;
            }

            // Insere o primeiro capítulo no documento
            response = await httpSubSectionService.CreateFirstSubSubSection(
                new CreateFirstNodeRequest(
                    subsection.Id,
                    documentId,
                    subsubsections[0].Id
                    )
                );
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.True(response.IsSuccessStatusCode);
            var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
            nodes[0] = node.Id;

            // Insere os demais capítulos no documento
            for (int i = 1; i < subsubsections.Length; i++)
            {
                response = await httpSubSectionService.InsertSubSubSectionAfterPosition(
                    new InsertNodeRequest(
                        node.Id,
                        subsection.Id,
                        documentId,
                        subsubsections[i].Id
                        )
                    );
                // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                Assert.True(response.IsSuccessStatusCode);
                node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                nodes[i] = node.Id;
            }

            // Caminhamento para frente
            node = await httpSubSectionService.GetFirstSubSubSectionPosition(subsection.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == subsection.Id && node.SecondEntityId == subsubsections[0].Id);

            for (int i = 1; i < subsubsections.Length; i++)
            {
                node = await httpSubSectionService.GetNextSubSubSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == subsection.Id && node.SecondEntityId == subsubsections[i].Id);
            }

            // Caminhamento para tráz
            node = await httpSubSectionService.GetLastSubSubSectionPosition(subsection.Id);
            Assert.NotNull(node);
            Assert.True(node.FirstEntityId == subsection.Id && node.SecondEntityId == subsubsections[^1].Id);

            for (int i = subsubsections.Length - 2; i >= 0; i--)
            {
                node = await httpSubSectionService.GetPreviousSubSubSectionPosition(node.Id);
                Assert.NotNull(node);
                Assert.True(node.FirstEntityId == subsection.Id && node.SecondEntityId == subsubsections[i].Id);
            }

            // // Remoção dos capítulos
            // // Meio
            // response = await httpSubSectionService.DeleteSubSubSectionAt(nodes[2]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[2] = Guid.Empty;
            // // Primeiro
            // response = await httpSubSectionService.DeleteSubSubSectionAt(nodes[0]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[0] = Guid.Empty;
            // // Último
            // response = await httpSubSectionService.DeleteSubSubSectionAt(nodes[^1]);
            // Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            // nodes[^1] = Guid.Empty;
            // // Demais
            // for (int i = 0; i < subsubsections.Length; i++)
            // {
            //     if (nodes[i] != Guid.Empty)
            //     {
            //         response = await httpSubSectionService.DeleteSubSubSectionAt(nodes[i]);
            //         Assert.True(response.StatusCode == System.Net.HttpStatusCode.NoContent);
            //     }
            // }
            // 
            // // Exclusão dos capítulos de teste
            // for (int i = 0; i < subsubsections.Length; i++)
            // {
            //     response = await httpSubSubSectionService.DeleteSubSubSection(subsubsections[i].Id);
            //     Assert.True(response.IsSuccessStatusCode);
            // }
        }
#pragma warning restore CS8602

    }
}

