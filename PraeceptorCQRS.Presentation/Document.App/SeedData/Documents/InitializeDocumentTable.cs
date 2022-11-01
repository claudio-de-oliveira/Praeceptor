using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;
using Document.App.SeedData.Chapters;
using Document.App.SeedData.Sections;
using Document.App.SeedData.SubSections;
using Document.App.SeedData.SubSubSections;

using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Node;

namespace Document.App.SeedData.Documents
{
    public static class InitializeDocumentTable
    {
        static readonly string[] parts = {
                "1", // Apresentação    2
                "2", // Dados Gerais Sobre a Universidade   4
                "2.1", // Histórico institucional 4
                "2.1.1", // Campi, infraestrutura e cursos  5
                "2.2", // Missão, valores e objetivos da UNIT 6
                "2.3", // Organograma da instituição  7
                "2.4", // Estrutura acadêmica e administrativa    8
                "3", // Dados de Identificação  10
                "3.5", // Identificação   10
                "3.6", // Legislação e normas que regem o curso   11
                "3.7", // Formas de acesso ao curso   11
                "4", // Dados Conceituais do Curso  13
                "4.8", // Histórico do curso: sua criação e trajetória    13
                "4.9", // Objetivos do curso  15
                "4.9.2", // Geral   15
                "4.9.3", // Específicos 15
                "4.10", // Perfil profissiográfico 16
                "4.11", // Campo de atuação    17
                "5", // Contextualização e Justificativa do Curso   18
                "5.12", // Aspectos físicos e demográficos 18
                "5.13", // Aspectos econômicos 19
                "5.14", // Aspectos educacionais   21
                "5.15", // A UNIT frente ao desenvolvimento do estado de Sergipe e da região   21
                "5.16", // Dados sobre a saúde 23
                "5.17", // Políticas institucionais no âmbito do curso 26
                "5.18", // Políticas de ensino 27
                "5.19", // Políticas de pesquisa   29
                "5.20", // Políticas de extensão   30
                "6", // Organização Curricular e Metodológica do Curso  31
                "6.21", // Outras características da estrutura curricular  31
                "6.21.4", // Acessibilidade Metodológica 31
                "6.21.5", // Flexibilização na Estrutura Curricular  31
                "6.21.6", // Interdisciplinaridade na Estrutura Curricular   32
                "6.21.7", // Educação das Relações Étnico-Raciais e o Ensino Da História e Cultura Afro-Brasileira, Africana e Indígena  33
                "6.21.8", // Educação Ambiental  33
                "6.21.9", // Educação em Direitos Humanos    34
                "6.22", // Estrutura curricular    34
                "6.23", // Eixos Interligados de Formação  39
                "6.23.10", // O eixo de fenômenos e processos básicos 39
                "6.23.11", // Eixos de formação profissional e específico 39
                "6.24", // Eixos estruturantes 39
                "6.24.12", // Eixo de formação específica(PPI)   39
                "6.24.13", // Eixo de práticas de pesquisa    39
                "6.24.14", // Eixo de práticas profissionais(PPI)    39
                "6.24.15", // Eixo de formação complementar   40
                "6.24.16", // O eixo de fenômenos e processos básicos 40
                "6.25", // Temas transversais  40
                "6.26", // Atividades complementares   40
                "6.27", // Atividades Práticas Supervisionadas Extraclasse – APS   41
                "6.28", // Integração Ensino/Pesquisa/Extensão/Núcleo de Pesquisa e Geradores de Extensão  43
                "6.29", // Programas/Projetos/Atividades de Iniciação Científica   43
                "6.30", // Interação teoria e prática – Princípios e orientações quanto às práticas pedagógicas    43
                "6.31", // Práticas profissionais e estágio    43
                "6.31.17", // Estágio curricular supervisionado obrigatório   43
                "6.31.18", // Estágio não obrigatório 45
                "6.32", // Trabalho de conclusão de curso  45
                "6.33", // Sistemas de avaliação   46
                "6.33.19", // Procedimentos e acompanhamento dos processos de avaliação de ensino e aprendizagem  46
                "6.33.20", // Avaliação do processo ensino/ aprendizagem   48
                "6.33.21", // Articulação da Autoavaliação do curso com a Autoavaliação Institucional 48
                "6.33.22", // Enade   51
                "7", // Participação dos Corpos Docente e Discente no Processo  53
                "7.34", // Núcleo docente estruturante(NDE)   55
                "7.35", // Colegiado de curso  56
                "8", // Corpo Social(Corpo Docente e Técnico – Administrativo) 58
                "8.36", // Corpo docente   58
                "8.37", // Administração acadêmica do curso    59
                "9", // Formas de Atualização e Reflexão    60
                "9.38", // Modos de integração entre a graduação e a pós - graduação 61
                "10", // Apoio ao Discente   62
                "10.39", // Núcleo de atendimento pedagógico e psicossocial 62
                "10.40", // Formação complementar e nivelamento discente    63
                "10.41", // Programa de integração de calouros  65
                "10.42", // Monitoria   65
                "10.43", // Internacionalização 66
                "10.44", // Unit Carreiras  66
                "10.45", // Programa de Bolsas  67
                "10.46", // Ouvidoria   67
                "10.47", // Acompanhamento dos egressos 68
                "11", // Ferramentas de Tecnologias Previstas e Implementadas    69
                "11.48", // As Tecnologias de Informação e Comunicação – TICs no processo ensino aprendizagem   69
                "11.49", // Ambiente Virtual de Aprendizagem(AVA)  69
                "12", // Conteúdos Curriculares  70
                "12.50", // Adequação e atualização 70
                "12.51", // Dimensionamento da carga horária das disciplinas    70
                "12.52", // Adequação e atualização das ementas e Planos de Ensino  70
                "12.53", // Adequação, atualização e relevância da bibliografia 70
                "12.54", // Bibliografia básica 70
                "12.55", // Bibliografia complementar   70
                "12.56", // Periódicos especializados   70
                "12.57", // Planos de Ensino e Aprendizagem 70
                "12.58", // Programas de disciplinas e seus componentes pedagógicos 70
                "12.58.23", // Primeiro período    70
                "12.58.24", // Segundo período 79
                "12.58.25", // Terceiro período    86
                "12.58.26", // Quarto período  94
                "12.58.27", // Quinto período  101
                "12.58.28", // Sexto período   111
                "12.58.29", // Sétimo período  117
                "12.58.30", // Oitavo período  123
                "12.58.31", // Nono período    128
                "12.58.32", // Décimo período  137
                "13", // Instalações do Curso    146
                "13.59", // Salas de aula   146
                "13.60", // Instalações administrativas 146
                "13.61", // Instalações para docentes – Salas de Professores, Salas de Reuniões e Gabinetes de Trabalho 146
                "13.62", // Espaço de trabalho para docentes em Tempo Integral – TI 147
                "13.63", // Espaço de trabalho para o coordenador   147
                "13.64", // Sala coletiva de professores    148
                "13.65", // Auditório / Sala de conferência   148
                "13.66", // Instalações sanitárias – adequação e limpeza    148
                "13.67", // Condições de acesso para portadores de necessidades especiais   149
                "13.68", // Infraestrutura de segurança 150
                "14", // Biblioteca  153
                "14.69", // Estrutura física    153
                "14.70", // Informatização da Biblioteca    153
                "14.71", // Acessibilidade informacional – biblioteca inclusiva 153
                "14.72", // Acervo Total da Biblioteca  153
                "14.73", // Base de Dados por Assinatura    153
                "14.74", // Política de aquisição, expansão e atualização do acervo 153
                "14.75", // Serviços    153
                "14.76", // Pessoal técnico e administrativo    153
                "14.77", // Serviço de acesso ao acervo 153
                "14.78", // Indexação   153
                "14.79", // Apoio na elaboração de trabalhos acadêmicos 153
                "15", // Laboratórios Específicos    154
                "15.80", // Laboratórios de Informática 154
                "15.81", // Laboratórios de Física  154
                "15.82", // Laboratórios de Química 154
                "15.83.33", // Laboratório G52 154
                "15.83.34", // Laboratório G54 154
                "15.83.35", // Laboratório G55 154
                "15.83.36", // Laboratório G56 154
                "15.83", // Laboratórios de Desenho Técnico – pranchetas    154
                "15.84", // Laboratório de Eletrônica – G03 154
                "15.85", // Laboratório de Automação – G04  154
                "15.86", // Laboratório de Acionamentos Elétricos e Instrumentação – G05    154
                "15.87", // Laboratório de Microcontroladores – G06 154
                "15.88", // Condições de conservação das instalações    155
                "15.89", // Manutenção e conservação dos equipamentos   155
                "16", // Referências 156
            };

        public static async Task Initialize(
            Guid instituteId,
            IDocumentListService documentService,
            IChapterListService chapterService,
            ISectionListService sectionService,
            ISubSectionListService subSectionService,
            ISubSubSectionListService subSubSectionService
            )
        {
            var response = await documentService.CreateEntity(new CreateEntityRequest(
                "Plano Pedagógico do Curso de Engenharia Elétrica",
                "",
                instituteId,
                "Testador"
                ));

            if (response.IsSuccessStatusCode)
            {
                var doc = JsonConvert.DeserializeObject<BookEntity>(await response.Content.ReadAsStringAsync());
                Guard.Against.Null(doc);

                var chaptersId = await InitializeChapterTable.Initialize(instituteId, chapterService);

                if (chaptersId.Count == 0)
                    return;

                var sectionsId = await InitializeSectionTable.Initialize(instituteId, sectionService);
                var subSectionsId = await InitializeSubSectionTable.Initialize(instituteId, subSectionService);
                var subSubSectionsId = await InitializeSubSubSectionTable.Initialize(instituteId, subSubSectionService);

                Guid lastChapterId = Guid.Empty;
                Guid lastSectionId = Guid.Empty;
                Guid lastSubSectionId = Guid.Empty;
                Guid lastSubSubSectionId = Guid.Empty;

                for (int i = 0; i < parts.Length; i++)
                {
                    string[] ids = parts[i].Split('.');

                    if (ids.Length == 4)
                    {
                        var id2 = subSectionsId[int.Parse(ids[2])];
                        var id3 = subSubSectionsId[int.Parse(ids[3])];

                        if (lastSubSubSectionId == Guid.Empty)
                        {
                            response = await subSubSectionService.CreateFirstEntity(
                                new CreateFirstNodeRequest(
                                    id2,
                                    doc.Id,
                                    id3
                                    )
                                );
                        }
                        else
                        {
                            response = await subSubSectionService.InsertEntityAfterPosition(
                                new InsertNodeRequest(
                                    lastSubSubSectionId,
                                    id2,
                                    doc.Id,
                                    id3
                                    )
                                );
                        }
                        // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                        response.EnsureSuccessStatusCode();
                        var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(node);
                        lastSubSubSectionId = node.Id;
                    }
                    else if (ids.Length == 3)
                    {
                        var id1 = sectionsId[int.Parse(ids[1])];
                        var id2 = subSectionsId[int.Parse(ids[2])];

                        if (lastSubSectionId == Guid.Empty)
                        {
                            response = await subSectionService.CreateFirstEntity(
                                new CreateFirstNodeRequest(
                                    id1,
                                    doc.Id,
                                    id2
                                    )
                                );
                        }
                        else
                        {
                            response = await subSectionService.InsertEntityAfterPosition(
                                new InsertNodeRequest(
                                    lastSubSectionId,
                                    id1,
                                    doc.Id,
                                    id2
                                    )
                                );
                        }
                        // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                        response.EnsureSuccessStatusCode();
                        var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(node);
                        lastSubSectionId = node.Id;
                        lastSubSubSectionId = Guid.Empty;
                    }
                    else if (ids.Length == 2)
                    {
                        var id0 = chaptersId[int.Parse(ids[0])];
                        var id1 = sectionsId[int.Parse(ids[1])];

                        if (lastSectionId == Guid.Empty)
                        {
                            response = await sectionService.CreateFirstEntity(
                                new CreateFirstNodeRequest(
                                    id0,
                                    doc.Id,
                                    id1
                                    )
                                );
                        }
                        else
                        {
                            response = await sectionService.InsertEntityAfterPosition(
                                new InsertNodeRequest(
                                    lastSectionId,
                                    id0,
                                    doc.Id,
                                    id1
                                    )
                                );
                        }
                        // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                        response.EnsureSuccessStatusCode();
                        var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(node);
                        lastSectionId = node.Id;
                        lastSubSectionId = Guid.Empty;
                        lastSubSubSectionId = Guid.Empty;
                    }
                    else if (ids.Length == 1)
                    {
                        var id0 = chaptersId[int.Parse(ids[0])];

                        if (lastChapterId == Guid.Empty)
                        {
                            response = await sectionService.CreateFirstEntity(
                                new CreateFirstNodeRequest(
                                    doc.Id,
                                    doc.Id,
                                    id0
                                    )
                                );
                        }
                        else
                        {
                            response = await sectionService.InsertEntityAfterPosition(
                                new InsertNodeRequest(
                                    lastChapterId,
                                    doc.Id,
                                    doc.Id,
                                    id0
                                    )
                                );
                        }
                        // Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
                        response.EnsureSuccessStatusCode();
                        var node = JsonConvert.DeserializeObject<NodeResponse>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(node);
                        lastChapterId = node.Id;
                        lastSectionId = Guid.Empty;
                        lastSubSectionId = Guid.Empty;
                        lastSubSubSectionId = Guid.Empty;
                    }
                }
            }
        }
    }
}
