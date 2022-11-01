using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;

using Newtonsoft.Json;

namespace Document.App.SeedData.SubSections
{
    public static class InitializeSubSectionTable
    {
        private static readonly List<BookEntity> entities = new()
        {
	        new BookEntity
            {
                Title = "Campi, infraestrutura e cursos",
                Text =
@"\paragraph{
	\run[Bold]{Campus Aracaju – Centro} – Localizado à rua Lagarto, nº 264, Centro, CEP: 49010-390,
		telefax: (79) 3218-2100, Aracaju/SE; tem Biblioteca Setorial, Teatro Tiradentes, laboratórios
		de informática e laboratórios de última geração para os cursos de Licenciaturas em Letras-Português,
		Letras- Inglês, Pedagogia, História e Geografia.}

\paragraph{
	\run[Bold]{Campus Aracaju – Farolândia} – Localizado a av. Murilo Dantas, 300, Farolândia,
	CEP 49032-490, telefax: (79) 3218- 2100, Aracaju/SE, foi implantado em 1994; tem uma Vila
	Olímpica com quadras poliesportivas, pista de atletismo, campo de futebol, piscinas;
	laboratórios de informática; Complexo Laboratorial Interdisciplinar para as áreas de Ciências
	Biológicas e da Saúde, Ciências Humanas e Sociais Aplicadas e Ciências Exatas e Tecnológicas.
	Em funcionamento há os seguintes cursos: Bacharelados em: Administração, Ciências Contábeis,
	Ciência da Computação, Sistemas para Internet, Sistemas de Informação, Comunicação Social:
	Jornalismo e Publicidade e Propaganda, Biomedicina, Medicina, Ciências Biológicas,
	Educação Física, Enfermagem, Farmácia, Fisioterapia, Nutrição, Odontologia, Medicina,
	Psicologia, Direito, Serviço Social, Engenharia Ambiental, Engenharia de Produção,
	Engenharia Civil, Engenharia Mecatrônica, Engenharia Elétrica, Engenharia Mecânica,
	Arquitetura e Urbanismo, Design Gráfico, Licenciaturas em Matemática, Informática e
	Educação Física, e os Tecnológicos nas áreas de  Gestão em Recursos Humanos, Petróleo e Gás,
	Gestão Financeira e Sistemas para Internet, Gastronomia, Design de Interiores, Estética e
	Cosmética e Design de Modas.
	}

\paragraph{
	Nesse campus, ainda está localizado o Instituto de Tecnologia e Pesquisa – ITP, integrante do
	seleto grupo dos Institutos do Milênio/CNPq, que facilita o desenvolvimento da pesquisa e
	tecnologia da Instituição. Esse espaço também tem uma estrutura oferecendo serviços que contemplam
	uma academia de ginástica, um mini shopping com restaurantes, lanchonetes, banca de revista,
	salão de beleza, vídeo locadora, livraria e agência bancária.
	}

\paragraph{
	\run[Bold]{Campus Estância} – Localizado à travessa Tenente Eloy, s/nº CEP: 49200-000,
	telefax: (79) 3522-3030 e (79) 3522-1775, Estância/SE (a 68 km de Aracaju), foi implantado
	no segundo semestre de 1999. Dispõe de uma sede que privilegia uma ampla infraestrutura
	composta por: mini shopping com lojas de conveniência e lanchonetes; biblioteca setorial;
	laboratórios; amplas salas de aula e área de convivência. Oferta os cursos de Direito,
	Administração, Serviço Social e Enfermagem.
	}

\paragraph{
	\run[Bold]{Campus Itabaiana} – Localizado à rua José Paulo Santana, 1.254, bairro Sítio Porto,
	CEP: 49500-000, telefax: (79) 3431-5050, Itabaiana/SE (a 57 km de Aracaju), foi implantado em
	25 de fevereiro 2002. Tem uma sede constituída por uma ampla infraestrutura composta por:
	minishopping com lojas de conveniência e lanchonetes; biblioteca setorial; laboratório de
	informática; amplas salas de aula e área de convivência. Os cursos em funcionamento são:
	Administração, Enfermagem e Direito.}

\paragraph{
	\run[Bold]{Campus Propriá} – Localizado à praça Santa Luzia, nº 105, Centro, CEP: 49900-000,
	telefax: (79) 3322-2774, Propriá/SE, foi implantado no 1º semestre de 2004. Oferta os cursos
	de Direito e Administração. E a sua infraestrutura contempla mini shopping com lojas de
	conveniência e lanchonetes; biblioteca setorial; laboratório de informática; amplas salas
	de aula e área de convivência.
	}
"
            },
            new BookEntity
            {
                Title = "Geral",
                Text =
@"
\Paragraph{
	\comment{O objetivo geral de um curso, geralmente, aparece nas Diretrizes Curriculares. 
		São decorrentes da análise da realidade e dos posicionamentos assumidos nos 
		referenciais e expressam as finalidades da ação educativa proposta pelo curso. 
		Deve ser construído tendo em vista a missão, o perfil profissiográfico, as 
		competências (DCNs do curso).
		}
		{Exercer de forma plena a \variable{@CURSO.@NOME}, atuando em diversos setores 
		e que sejam capazes de considerar os problemas em sua totalidade, com visão 
		sistêmica de processos em geral, antecipando-se e propondo soluções que sejam 
		corretas dos pontos de vista técnico, econômico, social e ambiental, sendo 
		estes profissionais conscientes dos seus direitos e deveres, com amplos e sólidos 
		conhecimentos teórico-práticos, fundamentados nas ciências básicas, ciências da 
		engenharia, projetos e humanidades, com um perfil generalista, humanista, 
		científico e empreendedor, capaz de solucionar problemas, aptos a atuar 
		profissionalmente em espaços com uma visão ampla e global, respeitando os 
		princípios legais e éticos com fácil adaptação às rápidas mutações do universo 
		tecnológico no qual a \variable{@CURSO.@NOME} está inserida.
		}
}
"
            },
            new BookEntity
            {
				Title = "Específicos",
				Text =
@"
\begin{itemize}
\Paragraph{
	\comment{
		Os objetivos deverão ser claros e concisos e expressar apenas uma ideia, devendo constar 
		somente um sujeito e um complemento, para cada objetivo. O conjunto de objetivos deve 
		expressar como o objetivo geral será atingido.
		}
		{
		Incentivar o trabalho de pesquisa e investigação científica que contribua para a compreensão 
		da questão ambiental podendo, assim, interferir no processo de maneira proativa;
		}
}
\Paragraph{
	Capacitar o aluno para compartilhar ações, discursos e conhecimentos que possam resultar 
	num exercício permanente de cidadania responsável;}
\Paragraph{Utilizar valores e atitudes baseados em princípios éticos pertinentes ao exercício 
	profissional;}
\Paragraph{Despertar o interesse pela inovação e empreendedorismo por meio do incentivo a 
	pesquisa, desenvolvimento, adaptação e utilização de novas tecnologias;}
\Paragraph{Promover um ambiente multidisciplinar e transdisciplinar que facilite o reconhecimento 
	de problemas da engenharia, bem como a formulação, análise e resolução criativa, holística e 
	humanista destes.}
\end{itemize}
"
            },
            new BookEntity
            {
				Title = "Acessibilidade Metodológica",
				Text =
@"
\Paragraph{No currículo do curso de \variable{@CURSO.@NOME} a acessibilidade metodológica é entendida como condição para utilização, com segurança e autonomia, total ou assistida, de diferentes metodologias que favoreçam o processo de aprendizagem. Neste sentido, no curso de \variable{@CURSO.@NOME} as atividades desenvolvidas observam as necessidades individuais e os diferentes ritmos e estilos de aprendizagem dos estudantes.}
\Paragraph{A comunidade acadêmica, em especial, os professores, concebem o conhecimento, a avaliação e a inclusão educacional promovendo processos e recursos diversificados a fim de viabilizar a aprendizagem significativa dos estudantes Desta forma, concebe-se que a acessibilidade metodológica no curso de \variable{@CURSO.@NOME} deve considerar a heterogeneidade de características dos alunos para que se possa derrubar os obstáculos no processo de ensino aprendizagem promovendo assim a efetiva participação do estudante nas atividades pedagógicas e na apropriação dos conhecimentos e saberes que favoreçam uma formação integral no seu itinerário acadêmico.}
\Paragraph{Atentos a esses princípios, os conteúdos curriculares a serem abordados no Curso de \variable{@CURSO.@NOME} encontram-se organizados de modo a constituírem-se elementos que possibilitem o desenvolvimento do perfil profissional do egresso, considerando as características individuais. No que se refere à ampliação no atendimento educacional especializado ligado as questões de acessibilidade, o acadêmico da Universidade Tiradentes conta com as ações desenvolvidas pelo Núcleo de Atendimento Pedagógico e Psicossocial – NAPPS que oferece aos estudantes um serviço que objetiva acolhê-lo e auxiliá-lo a resolver, refletir e enfrentar seus conflitos emocionais, bem como suas dificuldades a nível pedagógico.}
"
            },
            new BookEntity
            {
				Title = "Flexibilização na Estrutura Curricular",
				Text =
@"
\Paragraph{A flexibilização curricular está fundamentada no PDI por mecanismos presentes no currículo do curso que se consolidam por meio de disciplinas optativas, eletivas e atividades complementares à formação acadêmica. Desta forma, as disciplinas optativas e eletivas, além das ATCs objetivam:}
\begin{itemize}
	\Paragraph{Proporcionar a construção do percurso acadêmico, enriquecendo e ampliando o currículo;}
	\Paragraph{Oportunizar a vivência teórico-prática de disciplinas específicas em cursos que pertencem à mesma área ou área afim;}
	\Paragraph{Possibilitar a ampliação de conhecimentos teórico-práticos que aprimorem a qualificação acadêmico-profissional;}
	\Paragraph{Oportunizar a vivência de situações de aprendizagem que extrapolam as exposições verbais em sala de aula.}
\end{itemize}
\Paragraph{Assim posto, tais componentes flexibilizam o currículo, propiciando a organização de trajetórias individuais de formação. Essas atividades promovem ao discente o contato com conhecimentos, que transcendam os programas disciplinares, o que viabiliza vivências voltadas ao mundo da ciência e do trabalho, tendo em vista a busca da sua autonomia acadêmica, ao efetuar escolhas, que permitem a organização de trajetórias individuais, no decorrer da formação profissional.}
\Paragraph{Acompanhando os avanços na profissão, estão inseridas na estrutura curricular disciplinas de formação geral: Fundamentos Antropológicos e Sociológicos, e Filosofia e Cidadania, Metodologia Científica e ainda a disciplina de Língua Brasileira de Sinais – LIBRAS. As disciplinas mencionadas utilizam mecanismos de EAD possibilitando aos estudantes o contato e o uso das TICs, adaptando-se ao espírito do aprendizado aberto e semipresencial centradas na autoaprendizagem por meio de ferramentas tecnológicas facilitadoras da construção do conhecimento, contribuindo, dessa forma, para a autonomia do aluno.}
"
            },
            new BookEntity
            {
				Title = "Interdisciplinaridade na Estrutura Curricular",
				Text =
@"
\Paragraph{A interdisciplinaridade é operacionalizada por meio da complementaridade de conceitos e intervenções entre as unidades programáticas de um mesmo campo do saber e entre diferentes campos, dialeticamente provocada através de conteúdos e práticas que possibilitam a diminuição da fragmentação do conhecimento e saberes, em prol de um conhecimento relacional e aplicado à realidade profissional e social. Busca, desse modo, favorecer uma visão contextualizada e uma percepção sistêmica da realidade, de modo a propiciar uma compreensão mais abrangente.}
\Paragraph{As disposições das disciplinas na estrutura curricular possibilitam um percurso formativo que contribui com a transversalidade e com a interdisciplinaridade, dessa forma, há uma busca permanente de aproximação da teoria à prática, à medida que se proporcionam paulatinamente no transcorrer do curso, oportunidades de vivenciar situações de aprendizagem diferenciadas. Dentre tais atividades interdisciplinares podemos mencionar as que são desenvolvidas pelas componentes curriculares de \comment{Adaptar!}{Práticas de Engenharia Mecatrônica I, II, III e IV}, que são disciplinas integradoras do período, cujas unidades curriculares devem apresentar conteúdos de integração, sendo o principal catalisador da integração os conteúdos das matérias conceituais e instrumentais que antecedem as mesmas. Os blocos disciplinares das Práticas de Engenharia Mecatrônica terão à sua disposição espaços de experimentação, onde serão desenvolvidas aplicações práticas das competências desenvolvidas. Essa experimentação culmina na apresentação de trabalhos na Mostra de Projetos Integradores realizados ao final de cada semestre letivo e ainda em atividades durante a realização de eventos de extensão que envolve alunos de períodos e inclusive outras áreas de conhecimento, como por exemplo, o projeto Cabuto (Fórmula SAE AeroDesign).}
"
            },
            new BookEntity
            {
				Title = "Educação das Relações Étnico-Raciais e o Ensino Da História e Cultura Afro-Brasileira, Africana e Indígena",
				Text =
@"
\Paragraph{Em relação ao preconizado nas Diretrizes Curriculares Nacionais para a Educação das Relações Étnico-raciais e para o Ensino da História e Cultura Afro-Brasileira e Indígena – (CNE/CP Resolução 1/2004), o curso trata destas questões:}
\begin{itemize}
\Paragraph{No projeto pedagógico e na matriz curricular estão incluídos em conteúdos de disciplinas e atividades curriculares pertinentes;}
\Paragraph{Nas Atividades Complementares patrocinadas pelo curso e pela Universidade, como tema de iniciação científica e pesquisa, extensão, entre outros;}
\Paragraph{Em disciplinas como Fundamentos Antropológicos e Sociológicos, que trata de questões socioculturais, por meio de desenvolvimento de temas que abordarão as questões socioculturais e História dos Povos Indígenas e Afrodescendentes, dos Movimentos sociais como fruto do comportamento coletivo, a plurietnia e o multiculturalismo no Brasil, entre outros, de modo a promover a ampliação dos conhecimentos acerca da formação destas sociedades e da sua integração nos processos físico, econômico, social e cultural da Nação Brasileira, além de disciplinas optativas em que tais questões também são tratadas.}
\end{itemize}
"
            },
            new BookEntity
            {
				Title = "Educação Ambiental",
				Text =
@"
\Paragraph{De acordo com a Lei Federal de 27/04/1999, que dispõe sobre a educação ambiental, instituindo a Política Nacional de Educação Ambiental, o Parecer CNE/CP nº 14/2012, de 6 de junho de 2012, a educação ambiental (EA) e a Resolução Nº 2 de 15 de junho de 2012 que estabelece as Diretrizes Curriculares Nacionais para a Educação Ambiental. Esta, se constitui como uma dimensão representada por processos nos quais cada indivíduo e coletividade edificam valores sociais, conhecimentos, habilidades, atitudes e valores voltados para a construção de uma consciência ambiental, pautada na ética e sustentabilidade.}
\Paragraph{Desta forma, o Projeto Pedagógico e estrutura curricular do curso de Engenharia Mecatrônica a apresenta a Educação Ambiental, que será desenvolvida de diferentes formas, tais como:}
\begin{itemize}
\Paragraph{Transversalmente nos diversos componentes curriculares, como temática a ser desenvolvida nas disciplinas.}
\Paragraph{Nas Práticas de Pesquisa e Extensão na Área da Engenharia, Engenharia de Sustentabilidade e nas demais ações a serem desenvolvidas no curso, a exemplo das Semanas Acadêmicas e outras ações institucionais, como o Programa “Conduta Consciente”.}
\end{itemize}
"
            },
            new BookEntity
            {
				Title = "Educação em Direitos Humanos",
				Text =
@"\Paragraph{No tocante a Resolução nº 1, de 30 de maio de 2012, que estabelece Diretrizes Nacionais para a Educação em Direitos Humanos, cujo objetivo central é a formação para a vida e para a convivência no exercício cotidiano, consubstanciado como forma de vida e de organização social, política, econômica e cultural, no curso de Engenharia Mecatrônica, a inserção dos conhecimentos concernentes à Educação em Direitos Humanos ocorrerá das seguintes formas:}
\begin{itemize}
\Paragraph{Pela transversalidade, por meio de temas relacionados aos Direitos Humanos e tratados interdisciplinarmente;}
\Paragraph{Como um conteúdo específico na disciplina Filosofia e Cidadania;}
\Paragraph{De maneira mista, ou seja, combinando transversalidade e interdisciplinaridade, nos demais componentes, a exemplo das atividades complementares, de extensão, e de pesquisa, desenvolvidas ao longo do curso;}
\Paragraph{Ações institucionais como Seminários e Fóruns de discussão.}
\end{itemize}
"
            },
            new BookEntity
            {
                Title = "O eixo de fenômenos e processos básicos", 
				Text = ""
			},
            new BookEntity
            {
                Title = "Eixos de formação profissional e específico", 
				Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de formação específica (PPI)", 
				Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de práticas de pesquisa", 
				Text = ""
            },
            new BookEntity
            {
                Title = "Eixo de práticas profissionais (PPI)", 
				Text = ""
            },
            new BookEntity
            {
				Title = "Eixo de formação complementar", 
				Text = ""
            },
            new BookEntity
            {
                Title = "O eixo de fenômenos e processos básicos", 
				Text = ""
            },
            new BookEntity
            {
				Title = "Estágio curricular supervisionado obrigatório",
				Text =
@"\paragraph{
	Para o currículo por competências o Estágio Supervisionado faz parte do eixo articulador entre 
	teoria e prática e como tal será desenvolvido atendendo a diferentes etapas. Nesse momento de sua 
	formação, o estudante terá contato com a realidade profissional onde irá atuar não apenas para 
	conhecê-la, mas também para desenvolver as competências e habilidades específicas a formação 
	profissional.
	}
\paragraph{
	Seguindo o que recomenda as Diretrizes Curriculares Nacionais, os estágios curriculares são 
	desenvolvidos sob supervisão docente de forma articulada ao longo do processo de formação. 
	Este deverá ser desenvolvido quando possível no âmbito interno e ainda no âmbito externo a 
	universidade sempre através de convênios previamente estabelecidos e em ambientes que permitam 
	o desenvolvimento de práticas relacionadas ao exercício da \variable{@CURSO.@NOME}.
	}
\paragraph{
	As atividades de estágio estão ligadas ao Eixo Estruturante de Práticas Profissionais (PPI) 
	que compreende as unidades orientadas para o exercício e inserção dos estudantes em atividades 
	inerentes a sua profissão, bem como promover a interação multiprofissional, culminando na 
	apreensão de habilidades e competências do seu campo de atuação.
	}
\paragraph{
	De acordo com o artigo 7º da Resolução CNE/CES 11/2002, a formação do engenheiro incluirá, 
	como etapa integrante da graduação, estágios curriculares obrigatórios sob supervisão direta 
	da instituição de ensino, através de relatórios técnicos e acompanhamento individualizado 
	durante o período de realização da atividade. A carga horária mínima do estágio deverá atingir 
	\variable{@CURSO.@CR_ESTAGIO} horas.
	}
\paragraph{
	Seguindo as Diretrizes Curriculares Nacionais, o estágio supervisionado é desenvolvido sob 
	supervisão docente de forma articulada ao longo do processo de formação, mediante acompanhamento 
	do professor orientador que aprovará os programas de atividades, planos e projetos a serem 
	desenvolvidos pelos alunos durante o estágio.
	}
\paragraph{
	O estudante do Curso de \variable{@CURSO.@NOME} da \variable{@IES.@ACRONIMO} deverá cumprir 
	\variable{@CURSO.@CR_ESTAGIO} de Estágio Supervisionado, no décimo período do curso, organizado 
	com o objetivo de atender os níveis e as especificidades inerentes a formação profissional. A 
	efetivação do estágio ocorrerá mediante formalização mediante Termo de Compromisso celebrado 
	entre a Empresa e a Instituição de Ensino, com interveniência obrigatória da instituição de ensino.
	}
\paragraph{
	A caracterização e a definição dependem de instrumentos jurídicos (acordo de cooperação ou 
	convênio), celebrado entre a parte concedente (empresa/instituição) e a instituição de ensino, 
	no qual se acordam as condições de realização do estágio. Nessa direção, o estágio funcionará mediante 
	a aplicação e a utilização dos seguintes instrumentos: matrícula na disciplina de Estágio Supervisionado, 
	termo de Compromisso, Programa de Atividades, Ficha de Avaliação e Relatórios Atividades.
	}
\paragraph{
	No programa de atividades são explicitadas todas as tarefas a serem desenvolvidas no período 
	de estágio, bem como os prazos de sua conclusão. A jornada de atividades do Estágio Supervisionado 
	Curricular é cumprida em horário fixo ou variável durante a semana e em qualquer hipótese, o 
	horário estabelecido não poderá conflitar com o horário do estudante, devendo ser fixado de comum 
	acordo entre a Coordenação de Estágio do Curso, o estudante e a empresa, e constar no termo de 
	compromisso.
	}
\paragraph{
	A Avaliação Final do aluno será feita pelo coordenador de estágio obedecendo à sistemática da 
	\variable{@IES.@ACRONIMO} e ocorrerá da seguinte forma:
	}

\begin{itemize}
	\paragraph{
		\run[BOLD]{Primeira Unidade (peso 4)}: serão avaliados Programa de Estágio, Relatório de Atividades e 
		Relatório Parcial (80 horas).
		}
	\paragraph{
		\run[BOLD]{Segunda Unidade (peso 6)}: será avaliado pela prova colegiada.
		}
\end{itemize}

\paragraph{
	Ao término do Estágio o aluno deverá apresentar ao Supervisor de Estágio da Empresa o relatório 
	conclusivo das atividades desenvolvidas respeitando-se os prazos definidos no Programa de Atividades. 
	O Relatório de Estágio deverá ser entregue em CD (arquivo PDF) ao professor orientador em (02) duas 
	vias digitadas obedecendo a estrutura segundo as Normas de Estágio da \variable{@IES.@ACRONIMO} e as 
	regras da ABNT.
	}
\paragraph{
	A cada semestre, a coordenação de estágio definirá o quantitativo de alunos estagiários por 
	professor-orientador que irão desenvolver as atividades de supervisão do estágio, em consonância com 
	as normas internas da Instituição.
	}
\paragraph{
	Os procedimentos de acompanhamento e avaliação se darão sob a supervisão de um professor vinculado a 
	disciplina de Estágio Supervisionado e se constituirá na elaboração de relatórios escritos conforme 
	orientação do professor. Todas as informações, etapas e procedimentos encontram-se no Regulamento de 
	Estágio Supervisionado do Curso.
	}
"
            },
            new BookEntity
            {
				Title = "Estágio não obrigatório",
				Text =
@"\paragraph{
	O Estágio Supervisionado não obrigatório, destinado a alunos regularmente matriculados no curso de 
	\variable{@CURSO.@NOME} da \variable{@IES.@ACRONIMO}, tem sua base legal na Lei 11.788 de 25 de setembro de 
	2008, § 2º do Art. 2º, que define estágio não obrigatório como “aquele desenvolvido como atividade opcional, 
	acrescida à carga horária regular e obrigatória”.
	}
\paragraph{
	A caracterização e a definição do estágio em tela requerem obrigatoriamente a existência de um contrato entre 
	a \variable{@IES.@ACRONIMO} e pessoas jurídicas de direito público ou privado, coparticipantes do Estágio 
	Supervisionado não obrigatório, mediante assinatura de Termo de Compromisso celebrado com o educando e com a 
	parte concedente, em que devem estar acordadas todas as condições, dentre as quais: matrícula e frequência 
	regular do educando e compatibilidade entre as atividades desenvolvidas no estágio e aquelas previstas no 
	termo de compromisso; e acompanhamento da instituição e da parte concedente.
	}
\paragraph{
	O acompanhamento do referido estágio ocorrerá através da Central de Estágio da instituição e a validação 
	como atividade complementar será norteada pelos procedimentos e normas previstas na Portaria Institucional 
	que estabelece o Regulamento das Atividades Complementares.
	}
"
            },
            new BookEntity
            {
				Title = "Procedimentos e acompanhamento dos processos de avaliação de ensino e aprendizagem",
				Text =
@"\paragraph{
	Consonante aos princípios defendidos na prática acadêmica, a sistemática de avaliação do processo 
	ensino/aprendizagem concebida pela \variable{@IES.@ACRONIMO}, no curso de \variable{@CURSO.@NOME} 
	resguarda a contextualização para estimular o desenvolvimento de competências, através de metodologias 
	de intervenção.
	}
\paragraph{
	A avaliação não é utilizada para punir ou premiar o aluno, ela é um instrumento que verifica a 
	intensidade ou nível de aprendizagem, permitindo ao docente planejar intervenções pedagógicas que 
	possibilitem a superação de dificuldades e os desvios observados. Neste processo, valoriza-se a 
	autonomia, a participação e o desenvolvimento de competências focadas no aprendizado previstos no 
	planejamento das disciplinas.
	}
\paragraph{
	Avaliar, neste Projeto Pedagógico do Curso, não significa verificar a classificação dos estudantes e 
	sim verificar a produção de conhecimentos, a redefinição pessoal, o posicionamento e a postura do 
	educando frente às relações entre conhecimento existente nesta determinada área de estudo e a realidade 
	socioeducacional em desenvolvimento. A avaliação deve estar voltada para as competências, traduzidas 
	no desempenho, deixando de ser pontual, punitiva e discriminatória, orientada à esfera da cognição e 
	memorização; para transformar-se num instrumento de acompanhamento de todo o processo ensino-aprendizagem, 
	como forma de garantir o desenvolvimento das competências necessárias à formação profissional.
	}
\paragraph{
	As avaliações são efetuadas ao final das unidades programáticas, sendo 02 a cada período letivo 
	conforme calendário acadêmico. A composição é expressa em notas, abrangendo Prova Contextualizada, 
	que aborda os conteúdos ministrados, verificada por meio de exame aplicado e a Medida de Eficiência, 
	obtida através da verificação processual do rendimento (individual ou em grupo) de investigação 
	(pesquisa, iniciação científica), de extensão, trabalhos de campo, seminários, resenhas e fichamentos.
	}
\paragraph{
	O sistema de avaliação adotado pelo curso obedece aos princípios norteadores do PPI, tais como: a 
	quantidade de avaliações, suas modalidades, média para aprovação, número de provas entre outros. 
	Nessa direção, são adotados os procedimentos que objetivam verificar a aprendizagem através de instrumentos 
	que estejam em sintonia com técnicas e metodologias de intervenção profissional além de buscar mecanismos 
	de superação de desvios, explicitadas as premissas iniciais sobre a avaliação do processo ensino/aprendizagem. 
	Seguem a seguir (entre outros) os diferentes meios de avaliação que poderão ser utilizados no processo de 
	ensino-aprendizagem e que deverão constar do Plano Integrado de Trabalho do professor elaborado a cada semestre:
	}

\begin{itemize}
	\paragraph{
		\run[SMALLCAPS]{Avaliação Objetiva (Múltipla Escolha)}: Possibilita maior cobertura dos assuntos ministrados em aula, 
		satisfazendo ao mesmo tempo o critério da objetividade e permitindo que examinadores independentes e 
		qualificados cheguem a resultados idênticos. Entretanto, as questões de múltipla escolha não podem 
		ultrapassar 20% do total da avaliação.
		}
	\paragraph{
		\run[SMALLCAPS]{Avaliação Contextualizada}: Possibilita ao estudante a formulação de respostas de maneira livre, 
		facilitando a crítica, correlação de ideias, síntese ou análise do tema discutido. Permite, ainda, 
		a avaliação da amplitude do conhecimento, lógica dos processos mentais, organização, capacidade de 
		síntese, racionalização de ideias e clareza de expressão.
		}
	\paragraph{
		\run[SMALLCAPS]{Participação em Seminários}: Possibilita o desenvolvimento da capacidade de observação e crítica do 
		desempenho do grupo, bem como de estudar um problema, em diferentes ângulos, em equipe e de forma 
		sistemática. Além disso, permite o aprofundamento de um tema, facilitando a chegada a conclusões 
		relativas ao mesmo.
		}
	\paragraph{
		\run[SMALLCAPS]{Relatórios de Atividades Práticas}: representa uma descrição sintética e organizada dos procedimentos 
		realizados durante as atividades práticas, possibilitando a análise e discussão desses procedimentos.
		}
	\paragraph{
		\run[SMALLCAPS]{Estudos de Casos}: Desenvolve nos alunos a capacidade de analisar problemas e criar soluções 
		hipotéticas, preparando-os para enfrentar situações reais e complexas, mediante o estudo de situações 
		problemas.
		}
	\paragraph{
		\run[SMALLCAPS]{Avaliação Prática}: Possibilita avaliar os conhecimentos práticos adquiridos, que complementam os 
		conteúdos teóricos e que poderão dar subsídios para a resolução de problemas.
		}
\end{itemize}

\paragraph{
	Destaca-se que todas as orientações relacionadas aos critérios de avaliação ao que se refere a aprovação 
	estão descritas no PPC do curso assim como no regulamento acadêmico que é de livre acesso do estudante 
	através da página da Universidade, do repositório institucional e ainda na forma impressa no ato da 
	matrícula no Informe DAA.
	}
\paragraph{
	Destaca-se que todas as orientações relacionadas aos critérios de avaliação ao que se refere a aprovação 
	estão descritas no PPC do curso assim como no regulamento acadêmico que é de livre acesso do estudante 
	através da página da Universidade, do repositório institucional e ainda na forma impressa no ato da 
	matrícula no Informe DAA.
	}
"
            },
            new BookEntity
            {
				Title = "Avaliação do processo ensino/aprendizagem",
				Text =
@"
\paragraph{A definir...}
"
            },
            new BookEntity
            {
				Title = "Articulação da Autoavaliação do curso com a Autoavaliação Institucional",
				Text =
@"\paragraph{
	Com o objetivo de instaurar um processo sistemático e contínuo de autoconhecimento e melhoria do seu 
	desempenho acadêmico a \variable{@IES.@ACRONIMO} iniciou em 1998 o Programa de Avaliação Institucional, 
	envolvendo toda a comunidade universitária, coordenado pela Comissão Própria de Avaliação – CPA.
	}
\paragraph{
	O processo de autoavaliação implementado reflete adequadamente o compromisso da \variable{@IES.@ACRONIMO} 
	e do curso de \variable{@CURSO.@NOME} com a qualidade dos serviços prestados a comunidade acadêmica, bem 
	como com a formação profissional.
	}
\paragraph{
	O curso de \variable{@CURSO.@NOME} realiza periodicamente ações que decorrem dos processos de avaliação 
	dirigidas pela CPA (autoavaliação e avaliação nominal docente), mas também fundamenta suas ações a partir 
	dos resultados dos processos de avaliações externas a exemplo do ENADE, e relatórios de avaliação interna 
	simulados. Nessa direção, a partir das observações colhidas nos processos de avaliação descritos acima 
	muitas mudanças foram introduzidas no curso, como por exemplo, a reestruturação da matriz curricular, 
	adequando aos objetivos desejados no PPC e às mudanças da própria da \variable{@CURSO.@NOME} no que se 
	refere às normas e legislações, num contexto globalizado.
	}
\paragraph{
	Assim, podemos afirmar que se encontram previstas e implementadas as ações decorrentes dos processos de 
	avaliação do curso conforme descrição:
	}
\begin{itemize}
	\paragraph{Redimensionamento das Disciplinas de Práticas de Pesquisa e de Extensão;}
	\paragraph{Intensificação das ações voltadas à política de monitoria;}
	\paragraph{Ampliação da participação dos alunos no Programa de Nivelamento e Formação Complementar;}
	\paragraph{Divulgação do Núcleo de Apoio Psicossocial e Pedagógico - NAPPS, para alunos e docentes;}
	\paragraph{Ampliação no número de professores do curso no Programa de Capacitação Docente;}
	\paragraph{Ampliação à participação de professores e alunos no processo de avaliação interna;}
	\paragraph{Ampliação do campo de estágio dos alunos do curso;}
	\paragraph{Ampliação do número de mestres e doutores e o regime de trabalho dos docentes do curso, com 
		vistas ao atendimento do referencial de qualidade;
		}
	\paragraph{Atualização e ampliação do acervo bibliográfico do curso e intensificação de sua utilização;}
	\paragraph{Ampliação do acervo do laboratório e ações efetivas de utilização e acompanhamento.}
\end{itemize}

\paragraph{
	A atenção a tais aspectos contribui para percepção do curso através do olhar do aluno e do docente. 
	Destaca-se que a CPA disponibiliza a gestão do curso relatório dos resultados dos processos internos e 
	que estes servem de instrumento norteador de ações futuras desenvolvidas pelo curso de \variable{@CURSO.@NOME} 
	na busca pelo acompanhamento contínuo e pela excelência nos serviços prestados a comunidade acadêmica.
	}
\paragraph{
	A avaliação institucional é entendida como um processo criativo de autocrítica da Instituição, como política 
	de autoavaliação para garantir a qualidade da ação universitária e para prestar contas à sociedade da 
	consonância dessa ação com as demandas científicas e sociais da atualidade.
	}
\paragraph{
	A operacionalização da avaliação institucional dá-se através da elaboração/revisão e aplicação de 
	questionários eletrônicos para aferição de percepções ou de graus de satisfação com relação com relação à 
	prática docente, a gestão da coordenação do curso, serviços oferecidos pela IES e política/programas 
	institucionais, as dimensões estabelecidas pelo Sistema Nacional de Avaliação da Educação Superior – 
	SINAES envolvendo todos os segmentos partícipes em consonância com o Projeto Pedagógico do Curso.
	}
\paragraph{
	A avaliação sistematizada dos cursos e dos professores é elaborada pela CPA, cuja composição contempla 
	a participação de segmentos representativos da comunidade acadêmica, tais como: docentes, discentes, 
	coordenadores de cursos, representantes de áreas, funcionários técnico-administrativos e representante 
	da sociedade. Em consonância com a meritocracia, a \variable{@IES.@ACRONIMO} tem premiado os melhores 
	docentes avaliados semestralmente.
	}
\paragraph{
	Os resultados da avaliação docente, avaliação dos coordenadores de cursos e da avaliação institucional 
	são disponibilizados no portal Magister dos alunos, dos docentes e amplamente divulgados pela instituição.
	}
\paragraph{
	Além disso, o Projeto Pedagógico é avaliado a cada semestre letivo por meio de reuniões sistemáticas da 
	Coordenação com o Núcleo Docente Estruturante, Colegiado de Curso, corpo docente, corpo discente, direção 
	e técnicos dos diversos setores envolvidos. Essa ação objetiva avaliar e atualizar o Projeto Pedagógico 
	do Curso - PPC, identificando fragilidade para que possam ser planejadas novas estratégicas e ações, com 
	vistas ao aprimoramento das atividades acadêmicas, necessárias ao atendimento das expectativas da 
	comunidade universitária.
	}
\paragraph{
	Aspectos como concepção, objetivos, perfil profissiográfico, ementas, conteúdos, metodologias de ensino 
	e avaliação, bibliografia, recursos didáticos, laboratórios, infraestrutura física e recursos humanos 
	são discutidos por todos que fazem parte da unidade acadêmica, visando alcançar os objetivos propostos, 
	e adequando-os ao perfil do egresso.
	}
\paragraph{
	Essas ações visam à coerência dos objetivos e princípios preconizados no curso e sua consonância com o 
	Projeto Pedagógico Institucional (PPI), as Diretrizes Curriculares Nacionais (DCN) e as reflexões 
	empreendidas com base nos relatórios de avaliação externa, além de formar profissionais comprometidos 
	com o desenvolvimento econômico, social e político do Estado, da Região e do País.
	}
\paragraph{
	Nesse contexto, o corpo docente é avaliado, semestralmente, através de instrumentos de avaliação 
	planejados e implementados pela CPA e aplicados com os discentes via Internet. Nessa perspectiva, são 
	observados os seguintes indicadores de qualidade do processo de ensino-aprendizagem:
	}

\begin{itemize}
	\paragraph{Domínio de conteúdo;}
	\paragraph{Prática docente (didática);}
	\paragraph{Cumprimento do conteúdo programático;}
	\paragraph{Pontualidade;}
	\paragraph{Assiduidade;}
	\paragraph{Relacionamento com os alunos.}
\end{itemize}

\paragraph{
	Além da avaliação realizada pelo corpo discente, os professores também são avaliados pelas respectivas 
	coordenações de curso que observam os seguintes indicadores:
	}

\begin{itemize}
	\paragraph{Elaboração do Plano de Curso;}
	\paragraph{Cumprimento do conteúdo programático;}
	\paragraph{Pontualidade e assiduidade (sala de aula e reuniões);}
	\paragraph{Utilização de recursos didáticos e multimídia;}
	\paragraph{Escrituração do diário de classe e entrega dos diários eletrônicos;}
	\paragraph{Pontualidade na entrega dos trabalhos acadêmicos;}
	\paragraph{Atividades de pesquisa;}
	\paragraph{Atividades de extensão;}
	\paragraph{Participação em eventos;}
	\paragraph{Atendimento as solicitações do curso;}
	\paragraph{Relacionamento com os discentes.}
\end{itemize}

\paragraph{
	O comprometimento de todos com o Projeto Pedagógico do Curso é obtido através de uma ampla divulgação 
	do seu conteúdo nas discussões, encontros, reuniões e na própria dinâmica do curso, buscando cada vez 
	mais a participação, o envolvimento dos professores e dos alunos quanto à conduta pedagógica e 
	acadêmica mais adequada para alcançar os objetivos propostos.
	}
\paragraph{
	O envolvimento da comunidade acadêmica no processo de construção, aprimoramento e avaliação do curso 
	vêm imbuídos do entendimento de que a participação possibilita seu aperfeiçoamento. Nessa 
	direção, cabe ao cabe ao Colegiado, a partir da dinâmica em que o Projeto Pedagógico é vivenciado, 
	acompanhar a sua efetivação e coerência junto ao Plano de Desenvolvimento Institucional e Projeto 
	Pedagógico Institucional, constituindo-se etapa fundamental para o processo de aprimoramento.
	}
\paragraph{
	A divulgação, socialização e transparência do PPC contribuem para criação de consciência e ética 
	profissional, no aluno e no professor, levando–os a compreender que fazem parte da Instituição e a 
	desenvolver ações coadunadas ao que preconiza o referido documento.
	}
\paragraph{
	Visando ao aperfeiçoamento do processo, os resultados das avaliações são analisados pela Diretoria 
	de Graduação - DG, para implementação de alternativas que contribuam à melhoria das ações. Nesse 
	sentido, as dificuldades evidenciadas são trabalhadas pela Coordenação do Curso e pela DG, que 
	orienta os professores com vistas ao aprimoramento de suas atividades, promovem cursos de aperfeiçoamento 
	e dão suporte nas fragilidades didático-pedagógicas.
	}
\paragraph{
	A Pró-Reitoria Adjunta de Graduação Presencial também é responsável pela análise e implementação de 
	modelos acadêmicos, desenvolvimento de capacitações, tecnologias educacionais, organização de 
	Jornadas e Semanas Pedagógicas, acompanhamento e atualizações do Projeto Pedagógico Institucional e 
	Projeto Pedagógico de Curso junto às coordenações, garantindo qualidade e adequação às diretrizes 
	curriculares e normas institucionais.
	}
"
            },
            new BookEntity
            {
				Title = "Enade",
				Text =
@"\paragraph{
	A Instituição considera os resultados da autoavaliação e a avaliação externa para o aperfeiçoamento e 
	melhoria da qualidade dos cursos. Nessa direção, o Exame Nacional de Desempenho de Estudantes – ENADE, 
	que integra o Sistema Nacional de Avaliação da Educação Superior - SINAES, constitui-se elemento 
	balizador da qualidade da educação superior.
	}
\paragraph{
	A Coordenação do curso, o Colegiado e o NDE realizam análise detalhada dos resultados dos Relatórios 
	do Curso e da Instituição, Questionário Socioeconômico, Autoavaliação Institucional do Curso, 
	identificando fragilidades e potencialidades, com a finalidade de atingir metas previstas no planejamento 
	estratégico institucional, bem como, elevar o conceito do curso e da instituição junto ao Ministério da 
	Educação.
	}
\paragraph{
	Visando conscientizar os alunos da importância da avaliação, a Unit implantou o Projeto ENADE 
	constituído de atividades que envolvem orientação e preparação, nos aspectos acadêmicos e psicológicos.
	}
\paragraph{
	Além disso, visando o aperfeiçoamento do processo, os resultados das avaliações são analisados pela 
	Coordenação de Avaliação e Acreditação e Pró-Reitoria Adjunta de Graduação Presencial, para implementação 
	de alternativas que contribuam à melhoria das ações. Nesse sentido, as dificuldades evidenciadas são 
	trabalhadas pela Coordenação do Curso que orientam os professores com vistas ao aprimoramento de suas 
	atividades, promovem cursos de aperfeiçoamento e dão suporte nas fragilidades didático-pedagógicas.
	}
\paragraph{
	Desse modo, encontram-se previstas e implementadas as ações decorrentes dos processos de avaliação do 
	curso conforme descrição: Ampliação da participação dos alunos no Programa de Nivelamento e Formação 
	Complementar; Divulgação do Núcleo de Apoio Psicossocial e Pedagógico - NAPPS, para alunos e docentes; 
	Ampliação do número de professores do curso no Programa de Capacitação Docente; Ampliação à participação 
	de professores e alunos no processo de avaliação interna; Ampliação do número de mestres e doutores e 
	o regime de trabalho dos docentes do curso, com vistas ao atendimento do referencial de qualidade; 
	Atualização e ampliação do acervo bibliográfico do curso e intensificar a sua utilização; Ampliação do 
	acervo do laboratório e promover ações efetivas de utilização e acompanhamento.
	}
"
            },
            new BookEntity
            {
				Title = "Primeiro período",
				Text = @"\code{@TABELA.@PEAS1}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Segundo período",
				Text = @"\code{@TABELA.@PEAS2}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Terceiro período",
				Text = @"\code{@TABELA.@PEAS3}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Quarto período",
				Text = @"\code{@TABELA.@PEAS4}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Quinto período",
				Text = @"\code{@TABELA.@PEAS5}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Sexto período",
				Text = @"\code{@TABELA.@PEAS6}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Sétimo período",
				Text = @"\code{@TABELA.@PEAS7}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Oitavo período",
				Text = @"\code{@TABELA.@PEAS8}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Nono período",
				Text = @"\code{@TABELA.@PEAS9}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Décimo período",
				Text = @"\code{@TABELA.@PEAS10}\paragraph[AFTER=500]{ }"
            },
            new BookEntity
            {
				Title = "Laboratório G52", 
				Text = "" 
            },
            new BookEntity
            {
                Title = "Laboratório G54", 
				Text = ""
            },
            new BookEntity
            {
                Title = "Laboratório G55", 
				Text = ""
            },
            new BookEntity
			{
				Title = "Laboratório G56", 
				Text = ""
			}
		};

        public static async Task<Dictionary<int, Guid>> Initialize(Guid instituteId, ISubSectionListService service)
        {
            Dictionary<int, Guid> subSections = new();

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
						subSections.Add(id++, section.Id);
                    }
				}
			}

            return subSections;
        }
    }
}
