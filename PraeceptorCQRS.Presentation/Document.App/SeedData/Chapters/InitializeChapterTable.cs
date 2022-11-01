using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;

using Newtonsoft.Json;

namespace Document.App.SeedData.Chapters
{
    public static class InitializeChapterTable
    {
        private static readonly List<BookEntity> entities = new()
        {
            new BookEntity
            {
                Title = "Apresentação",
                Text =
@"\default{paragraph}

\paragraph{
    O Projeto Pedagógico do Curso (PPC) de \variable{@CURSO.@NOME} da \variable{@IES.@NOME} – \variable{@IES.@ACRONIMO}
    é resultado da construção das diretrizes organizacionais, estruturais e pedagógicas, com a participação do corpo
    docente do curso por meio de seus representantes no Núcleo Docente Estruturante (NDE) e Colegiado. Encontra-se
    articulado com as bases legais e a concepção de formação profissional que favoreça o desenvolvimento de competências
    e habilidades necessárias ao exercício profissional do \variable{@Profissao.@Singular}, como a capacidade de observação,
    criticidade e questionamento, sintonizada com a dinâmica da sociedade nas suas demandas locais, regionais e nacionais,
    assim como com os avanços científicos e tecnológicos. O referido documento surge a partir da criação do curso,
    autorizado pela \variable{@CURSO.@PORTARIA} tendo como objetivo principal o atendimento aos princípios e
    diretrizes do Projeto Pedagógico Institucional, Diretrizes Curriculares Nacionais, Pareceres do CNE e
    indicadores de qualidade do Inep/MEC.
}

\paragraph{
    O Projeto Pedagógico do Curso de Graduação em \variable{@CURSO.@NOME} da \variable{@IES.@ACRONIMO} está em
    conformidade com as Diretrizes Curriculares Nacionais para os cursos de Graduação em Engenharia, Projeto
    Pedagógico Institucional da \variable{@IES.@ACRONIMO} – PPI e seu Plano de Desenvolvimento Institucional – PDI,
    fundamentado nas necessidades socioeconômicas, políticas, educacionais, demanda do mercado de trabalho no Estado
    de Sergipe e as condições institucionais da IES para expansão da oferta de cursos na área das exatas.
}

\paragraph{
    Cônscia de sua responsabilidade com a sociedade e com o desenvolvimento de Sergipe e do Nordeste, a
    \variable{@IES.@ACRONIMO} mantém o Curso de \variable{@CURSO.@NOME} tendo por base os princípios preconizados
    na Lei nº 9.394, de 20 de dezembro de 1996, que enfatiza a importância da construção dos conhecimentos mediante
    políticas e planejamentos educacionais, capazes de garantir o padrão de qualidade no ensino, flexibilizando a
    ação educativa, valorizando a experiência do aluno, respeitando o pluralismo de ideias e princípios básicos da
    democracia.
}

\paragraph{
    O PPC está organizado de modo a contemplar os critérios indispensáveis à formação de um
    \variable{@PROFISSAO.@SINGULAR} dotado das competências essenciais para o exercício profissional
    frente ao contexto socioeconômico-cultural e político da região e do país.
}

\paragraph{
    A proposta conceitual e metodológica é entendida como um conjunto de cenários em que há a construção
    do perfil do estudante a partir da aprendizagem significativa, que promove e produz sentidos. Esta proposta está
    em conformidade com os princípios da UNESCO, isto é, educar para fazer, para aprender, para sentir e para ser;
    busca-se a construção de uma visão da realidade e de situações excepcionais e singulares na qual atuará o futuro
    profissional com o compromisso de transformar a realidade em que vive.
}

\paragraph{
    Nesse contexto, a \variable{@IES.@ACRONIMO} se compromete com a oferta de um curso de relevância social
    que assegura a qualidade na formação acadêmica, vistas a atender as necessidades dos avanços científicos e
    tecnológicos da população de Aracaju e região circunvizinha considerando o binômio educação-tecnologia e como
    pilares essenciais para a construção da cidadania.
}"
            },
            new BookEntity
            {
                Title = "Dados Gerais Sobre a Universidade",
                Text = ""
            },
            new BookEntity
            {
                Title = "Dados de Identificação",
                Text = ""
            },
            new BookEntity
            {
                Title = "Dados Conceituais do Curso",
                Text = ""
            },
            new BookEntity
            {
                Title = "Contextualização e Justificativa do Curso",
                Text = ""
            },
            new BookEntity
            {
                Title = "Organização Curricular e Metodológica do Curso",
                Text = ""
            },
            new BookEntity
            {
                Title = "Participação dos Corpos Docente e Discente no Processo",
                Text =
@"\paragraph{A participação do corpo docente e discente no projeto do curso é obtida pela reflexão das ações com vistas a uma conduta pedagógica e acadêmica que possibilite a consecução dos objetivos nele contidos, bem como da divulgação do PPI, ressaltando a importância dos documentos como agentes norteadores das ações da instituição, dos cursos e das atividades acadêmicas.}
\paragraph{A participação de todos (docentes e discentes) no processo de construção, execução e aprimoramento do PPC vem imbuída da concepção de que a conhecimento possibilita aperfeiçoamento, divulgação, socialização e transparência, de modo a contribuir para criação de consciência e ética profissional, com vistas à compreensão e desenvolvimento de ações coadunadas ao que preconiza o referido documento.}
\paragraph{Nessa direção, as instâncias consultivas e deliberativas como o Conselho Superior de Ensino Pesquisa e Extensão – CONSEPE e o Conselho Superior de Administração – CONSAD, possuem representantes dos diversos segmentos da instituição e a alternância deles anualmente, vislumbra a participação representativa dos diversos atores. Nessas instâncias, participam o Diretor de Graduação, Coordenação de Extensão, Pós-Graduação e Pesquisa, além da Superintendência Acadêmica, Diretoria Administrativa, e demais representantes de órgãos que se relacionam direta e indiretamente com as atividades acadêmicas, com o objetivo de desenvolver integralmente as funções universitárias de ensino/pesquisa/extensão.}
\paragraph{No âmbito do curso, o Núcleo Docente Estruturante e o Colegiado por meio de seus representantes do Corpo Docente e discente são constantemente envolvidos nas decisões acadêmicas, onde são discutidas e deliberadas questões peculiares à vida universitária, objetivando o aprimoramento das atividades.}
\paragraph{A interação entre ensino e pesquisa é de suma importância para o desenvolvimento do futuro profissional, sendo a iniciação científica o primeiro passo para a concretização deste ideal. Com esse intuito, foi implantado o Programa de Bolsas de Iniciação Científica da \variable{@IES.@ACRONIMO} (PROBIC – UNIT) do qual participam professores e alunos da \variable{@IES.@ACRONIMO}.}
\paragraph{As bolsas de iniciação científica foram implantadas na instituição, inicialmente através de um programa mantido com recursos próprios e organizado por meio de critérios e normas que se pautaram pela transparência e acuidade através de Editais amplamente divulgados na Instituição.}
\paragraph{Desta forma, a \variable{@IES.@ACRONIMO} incentiva a participação dos discentes em projetos de pesquisa, visando o desenvolvimento e a transformação regional. Além disso a IES está investindo na formação de Grupos de Pesquisa, baseados na interdisciplinaridade de suas áreas de atuação.}
\paragraph{Ressalta-se que diversos alunos participam voluntariamente das pesquisas desenvolvidas na Instituição, principalmente no Instituto de Tecnologia e Pesquisa (ITP) e outros setores da IES, bem como de monitoria remunerada ou voluntária, projetos de pesquisa, projetos de extensão, estágios extracurriculares e eventos acadêmicos.}
\paragraph{A articulação do ensino, pesquisa e extensão é determinante para a formação do profissional reflexivo, comprometido com a transformação social e o desenvolvimento regional. Nessa direção, o corpo docente do Curso de \variable{@CURSO.@NOME}, liderado pelo seu Coordenador procura estimular a participação dos discentes nas diferentes atividades da vida acadêmica, como Iniciação Científica, participação em projetos de pesquisa institucionalizados ou não, monitorias remuneradas ou voluntárias, projetos de extensão, eventos e estágios extracurriculares.}
\paragraph{A participação dos professores e alunos no Colegiado do Curso se dá a partir das representantes titulares e suplentes, os quais possuem mandatos e atribuições regulamentados pelo Regimento Interno da Universidade.}
\paragraph{Os professores do curso participam sistematicamente de reuniões acadêmicas e administrativas, nas quais são discutidas e deliberadas questões peculiares à vida universitária, objetivando o aprimoramento das atividades. Desses fóruns participam também os Diretores de Graduação, Assuntos Comunitários e Extensão, Pós-Graduação e Pesquisa, além da Superintendência Acadêmica, Diretoria Administrativa e demais representantes de órgãos que se relacionam direta e indiretamente com as atividades acadêmicas, com o objetivo de desenvolver integradamente as funções universitárias de ensino/pesquisa/extensão, como dito anteriormente.}
\paragraph{Os professores e os alunos são ainda representados, mediante processo eleitoral, no Conselho Superior de Ensino Pesquisa e Extensão – CONSEPE e no Conselho Superior de Administração – CONSAD, com a alternância de representantes anualmente.}
\paragraph{No processo de construção do Projeto Pedagógico do curso de \variable{@CURSO.@NOME} valorizou-se a participação dos corpos docentes e discentes, seja através de reuniões periódicas através do Colegiado e dos representantes de sala, seja ainda através de cursos de capacitação promovidos pela universidade através das pró-reitorias, na perspectiva de envolvimento e comprometimento dos que fazem o Curso.}
\paragraph{A participação e o acompanhamento na execução do Projeto Pedagógico do Curso têm se efetivado, por meio de palestras, seminários, reuniões entre outros, com o corpo docente e discente para que a prática de ensino em cada disciplina atenda e esteja articulada com a concepção, os objetivos e o perfil profissiográfico do Projeto Pedagógico. O comprometimento do corpo docente e discente com o Projeto Pedagógico tem sido obtido através de divulgação do seu conteúdo no Curso, buscando a participação dos professores e estudantes no que se refere principalmente à determinação da conduta pedagógica e acadêmica mais adequada para alcançar os objetivos nele contidos.}
\paragraph{A \variable{@IES.@ACRONIMO} oferta regularmente bolsas de Monitoria e de Iniciação Científica, como parte do processo participativo do aluno nas atividades regulares de ensino e pesquisa, cabendo aos Cursos a divulgação semestral dos editais para seleção de alunos e preenchimento de vagas de monitoria, de acordo com as necessidades das disciplinas, exercendo atividade remunerada ou voluntária.}
"
            },
            new BookEntity
            {
                Title = "Corpo Social (Corpo Docente e Técnico – Administrativo)",
                Text = ""
            },
            new BookEntity
            {
                Title = "Formas de Atualização e Reflexão",
                Text =
@"\paragraph{A \variable{@IES.@ACRONIMO} através de suas Pró-Reitorias desenvolve programas de apoio didático–pedagógico aos docentes através de capacitações constantes com membros das comunidades externa e interna.}
\paragraph{O Programa de Capacitação e Qualificação Docente implantado na instituição desenvolve suas ações, objetivando qualificar e capacitar os docentes em três modalidades: Capacitação Interna; Capacitação Externa e Estudos Pós-Graduados.}
\paragraph{Na \variable{@IES.@ACRONIMO} a formação continuada dos docentes constitui-se em um processo de atualização dos conhecimentos e saberes relevantes para o aperfeiçoamento da qualidade do ensino, constituindo-se numa exigência não apenas da instituição como também da sociedade contemporânea com vistas ao desenvolvimento de competências, habilidades e valores necessários à prática docente.}
\paragraph{Nesse contexto, a Pró-Reitoria de Graduação em parceria com a Pró-Reitoria Adjunta de Graduação Presencial, priorizando o processo pedagógico como forma de garantir a qualidade no ensino, na pesquisa e na extensão, desenvolve o Programa Formação Docente para o Ensino Superior, com o objetivo promover ações pedagógicas que possibilitem aos docentes da uma formação permanente, como meio de reflexão do trabalho teórico–metodológico e aprimoramento da práxis, através de discussão e troca de experiências.}
\paragraph{Devidamente articulado com programas de auxílio financeiro, busca estimular e aperfeiçoar o seu quadro docente possibilitando o acesso a informações, métodos, tecnologias educacionais/pedagógicas modernas.}
\paragraph{Os Projetos Pedagógicos dos cursos de graduação ofertados pela \variable{@IES.@ACRONIMO} obedecem a uma política educacional centrada na visão global do conhecimento humano, realizada através do exercício da interdisciplinaridade e indissociabilidade entre o ensino, a pesquisa e a extensão. Nessa direção, esse documento é constantemente acompanhado e atualizado por todos seus atores nas diversas instâncias de representações.}
\paragraph{A Pró-Reitoria Adjunta de Graduação Presencial tem como finalidade: acompanhar sistemática e qualitativamente as atividades do ensino de graduação, assessorando o NDE na elaboração/execução/avaliação dos respectivos projetos pedagógicos; prestar apoio pedagógico aos docentes e coordenadores de cursos – inclusive na elaboração/execução/avaliação dos Planos Individuais de Trabalho (PIT''s); desenvolver programas de educação continuada do corpo docente e desenvolvimento das competências deles demandadas pela sociedade contemporânea, dentre outros.}
\paragraph{A coordenação e os docentes do curso de \variable{@CURSO.@NOME} estimulam a participação dos discentes nas diferentes atividades que dizem respeito à vida acadêmica, como o envolvimento dos alunos nas atividades promovidas pela coordenação do curso como, por exemplo, os projetos de extensão no planejamento, execução e avaliação.}
\paragraph{A participação política dos discentes na instância do Curso de \variable{@CURSO.@NOME} também é valorizada e se dá de forma efetiva nas atividades acadêmicas realizadas. Os discentes são incentivados a participar de forma democrática e ativa na construção do Curso, seja pela participação dos representantes discentes nas reuniões pedagógicas, seja informalmente, através de críticas e sugestões diretamente manifestadas à coordenação do curso.}
\paragraph{São promovidos encontros, seminários, entre outros com a participação de multiprofissionais no sentido de discutir temas relevantes no que diz respeito à educação, saúde, ética, cidadania e política, entre outros.}
\paragraph{Na reunião de planejamento, que acontece no final de cada semestre letivo, o Coordenador convoca todos os professores do Curso para discutir, entre outros pontos, a atuação dos docentes em sala de aula; avaliações realizadas via Internet pelos alunos; mecanismos de aperfeiçoamento da atuação do docente em sala de aula (planejamento da prática ensino-aprendizagem); atualização dos conteúdos programáticos; elaboração do plano de ação do curso; avaliação do mercado profissional; além de avaliar o Projeto Pedagógico do Curso.}
\paragraph{A Coordenação do Curso de \variable{@CURSO.@NOME} procura adotar elementos e procedimentos que aproximem educadores e educandos das realidades geográficas locais, regionais e nacionais, posicionando-se como instrumento de integração.}"
            },
            new BookEntity
            {
                Title = "Apoio ao Discente",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} empreende sua política de orientação e acompanhamento ao discente, oferecendo
    condições favoráveis à continuidade dos estudos independentemente de sua condição física ou socioeconômica.
    Tais preceitos estão contemplados nos documentos institucionais e em particular no PPI, quando expressa que:
    “A educação como um todo deve ter como objetivo fundamental fazer crescer as pessoas em dignidade,
    autoconhecimento, autonomia e no reconhecimento e afirmação dos direitos da alteridade” (principalmente
    entendidos como o direito à diferença e à inclusão social).
    }
\paragraph{
    A implementação desse princípio se consubstanciou na elaboração de políticas e programas, dentre os quais se destacam:
    }
\begin{itemize}
    \paragraph{
        \run[BOLD]{Financiamento da Educação}: FIEP, FIES, PROUNI e bolsas de desconto ofertadas pela própria instituição;
        }
    \paragraph{
        \run[BOLD]{Apoio Pedagógico}: Programa de Integração de Calouros, Política de Monitoria, Programa de Bolsas de
        Iniciação Científica, Intercâmbio, Atividades de Participação em Centros Acadêmicos, Programa de Inclusão Digital,
        curso de línguas, Política Geral de Extensão, Política de Publicações Acadêmicas e Política de Estágio;
        }
    \paragraph{
        \run[BOLD]{Apoio Médico}: Departamento Médico, Programa de Acompanhamento de Egressos e o Núcleo de
        Atendimento Pedagógico e Psicossocial – NAPPS.
        }
\end{itemize}"
            },
            new BookEntity
            {
                Title = "Ferramentas de Tecnologias Previstas e Implementadas",
                Text = ""
            },
            new BookEntity
            {
                Title = "Conteúdos Curriculares",
                Text = ""
            },
            new BookEntity
            {
                Title = "Instalações do Curso",
                Text = ""
            },
            new BookEntity
            {
                Title = "Biblioteca",
                Text = ""
            },
            new BookEntity
            {
                Title = "Laboratórios Específicos",
                Text = ""
            },
            new BookEntity
            {
                Title = "Referências",
                Text = ""
            }
        };

        public static async Task<Dictionary<int, Guid>> Initialize(Guid instituteId, IChapterListService service)
        {
            Dictionary<int, Guid> chapters = new();

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
                        var chapter = JsonConvert.DeserializeObject<BookEntity>(await response.Content.ReadAsStringAsync());
                        Guard.Against.Null(chapter);
                        chapters.Add(id++, chapter.Id);
                    }
                }
            }

            return chapters;
        }
    }
}
