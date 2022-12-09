using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;

using Newtonsoft.Json;

namespace Document.App.SeedData.Sections
{
    public static class InitializeSectionTable
    {
        private static readonly List<BookEntity> entities = new()
        {
            new BookEntity
            {
                Title = "Histórico institucional",
                Text =
@"\paragraph{A \variable{@IES.@ACRONIMO} é mantida pela \variable{@EMPRESA.@NOME}, também identificada
    pela sigla \variable{@EMPRESA.@ACRONIMO}, sociedade simples, com sede e foro na cidade de Aracaju/SE,
    registrada no Cartório de Registro Civil das Pessoas Jurídicas do 10º Ofício na mesma Cidade sob
    n° 2232, Livro A-15, fls. 42 a 45, em 9 de dezembro de 1971. Localizada na Avenida Murilo Dantas,
    300 – Bairro Farolândia. A \variable{@IES.@NOME} iniciou a sua história com o Colégio Tiradentes
    em 1962, ofertando o Ensino Fundamental e Médio – Profissionalizante: Pedagógico e Contabilidade.
    Em 1972, a Instituição foi autorizada pelo Ministério da Educação e do Desporto a ofertar os
    cursos de Graduação em Ciências Contábeis, Administração e Ciências Econômicas, sendo cognominada
    Faculdade Integrada Tiradentes (FIT’s), mantida pela Associação Sergipana de Administração – ASA,
    na época entidade de direito privado, sem fins lucrativos, reconhecida pela comunidade sergipana.
    Em 25 de agosto de 1994, a FIT’s foi reconhecida como Universidade através da Portaria Ministerial
    nº 1.274 publicada no Diário Oficial da União nº 164 em 26 de agosto de 1994, denominando-se
    \variable{@IES.@NOME} – \variable{@IES.@ACRONIMO}.}

\paragraph{Em 2000, a \variable{@IES.@NOME} passou a ofertar Educação a Distância – EAD, com a finalidade
    de proporcionar formação superior de qualidade às comunidades que dela necessitam. Desde então,
    desenvolve ações no sentido de dispor cursos de graduação, de extensão e disciplinas nos cursos
    presenciais (Portaria nº 2253/MEC/2003) nessa modalidade de ensino. Com esse credenciamento e
    visando à necessidade de qualificar profissionais do interior do Estado, através de convênios com
    prefeituras municipais, a \variable{@IES.@ACRONIMO} vem implantando, desde outubro de 2004, polos de
    Educação à Distância em Sergipe, nas cidades de: Aracaju, Carmópolis, Estância, Nossa Senhora da Glória,
    Itabaiana, Lagarto, Neópolis, Poço Verde, Porto da Folha, Propriá, Simão Dias, Nossa Senhora do
    Socorro, Tobias Barreto e Umbaúba além dos polos em outros Estados.}

\paragraph{No ano de 2004, a IES foi credenciada para ofertar o Programa Especial de Formação Pedagógica
    para Portadores de Diploma de Educação Superior – PROFOPE, destinado aos professores da Educação
    Básica, nas áreas de Letras/Português e Matemática, que quisessem obter o registro profissional
    equivalente à licenciatura.}

\paragraph{Atualmente, a Instituição, com 55 (cinquenta e cinco) anos de existência, disponibiliza um
    portfólio com 43 (quarenta e três) opções de cursos nas áreas de Humanas e Sociais, Exatas e
    Biológicas e da Saúde, dos quais 28 (trinta e oito) são bacharelados, 06 (seis) licenciaturas e
    09 (nove) são tecnológicos, ministrados em cinco campi: Aracaju – capital (Centro e Farolândia) e
    interior do Estado de Sergipe: Estância, Itabaiana e Propriá.}

\paragraph{A autonomia universitária permitiu a expansão da IES também no campo da pós-graduação.
    Na modalidade “lato sensu”, a comunidade sergipana dispõe de 40 (quarenta) cursos nas mais diversas
    áreas de conhecimento; 05 (cinco) cursos “stricto sensu” nas áreas de Engenharia de Processos,
    Saúde e Ambiente, Educação, Direitos Humanos e Biotecnologia, além de 04 (quatro) doutorados em
    Engenharia de Processos, Educação, Saúde e Ambiente e Biotecnologia Industrial em parceria com a
    Associação de Instituições de Ensino e Pesquisa da Região Nordeste do Brasil.}

\paragraph{A \variable{@IES.@NOME}, em sua macroestrutura, dispõe do Centro de Saúde e Educação
    Ninota Garcia, do Laboratório Central de Biomedicina, do Centro de Memória Lourival Batista, do
    Memorial de Sergipe, do Instituto Tobias Barreto de Menezes, da Farmácia-Escola e da Clínica de
    Odontologia, com o objetivo de apoiar as atividades de ensino, pesquisa e extensão, possibilitando
    aos acadêmicos os conhecimentos indispensáveis à sua formação, além de despertar e fomentar
    habilidades e aptidões para a produção de cultura.}

\paragraph{A IES ainda conta com o Complexo de Comunicação Social – CCS, que faz parte da estrutura
    do campus da Farolândia, disponibilizado para os alunos dos cursos de Jornalismo, Publicidade e
    Propaganda e Design Gráfico um dos mais completos centros de áudio e vídeo das escolas de
    comunicação do País; a Clínica de Psicologia, que objetiva oferecer orientação de estágio aos
    alunos, prestar serviços na área organizacional e no atendimento à comunidade; e com o Núcleo
    de Práticas Jurídicas do Curso de Direito, que funciona como escritório modelo, oportunizando
    aos discentes a prática profissional na área jurídica, através da prestação de serviços jurídicos
    gratuitos à sociedade.}

\paragraph{Para atender ao contexto apresentado, a \variable{@IES.@ACRONIMO} mantém um amplo quadro
    de colaboradores distribuídos em diversos departamentos e setores, além dos docentes; todos
    empenhados em promover um ensino de qualidade, prestar atendimento acadêmico aos discentes e
    manter em andamento os diversos projetos sociais, culturais e esportivos da Instituição,
    visando sempre o desenvolvimento regional.}"
            },
            new BookEntity
            {
                Title = "Missão, valores e objetivos da UNIT",
                Text =
@"
\paragraph[FirstLine=0]{\run[Bold]{Missão da instituição}}

\begin{itemize}
    \paragraph{Desenvolver a sociedade por meio de serviços de qualidade relacionados à educação e à cultura.}
\end{itemize}

\paragraph[FirstLine=0]{\run[Bold]{Valores}}

\begin{itemize}
    \paragraph{Valorização do Ser Humano;}
    \paragraph{Ética;}
    \paragraph{Humildade;}
    \paragraph{Honestidade;}
    \paragraph{Educação;}
    \paragraph{Disciplina;}
    \paragraph{Inovação;}
    \paragraph{Compromisso;}
    \paragraph{Eficiência/Eficácia;}
    \paragraph{Responsabilidade Social.}
\end{itemize}

\paragraph{Seus princípios norteadores expressam-se por meio das seguintes diretrizes:}

\begin{itemize}
    \paragraph{Autonomia universitária;}
    \paragraph{Fomento à indissociabilidade entre o ensino, a pesquisa e a extensão;}
    \paragraph{Gestão participativa e eficiente;}
    \paragraph{Pluralidade de ideias;}
    \paragraph{Compromisso com a qualidade da oferta educacional;}
    \paragraph{Interação constante com a comunidade;}
    \paragraph{Inserção regional, nacional e internacional;}
    \paragraph{Respeito à diversidade e direitos humanos;}
    \paragraph{Atuação voltada ao desenvolvimento sustentável.}
\end{itemize}

\paragraph[FirstLine=0]{\run[Bold]{Objetivos da \variable{@IES.@ACRONIMO}}}

\paragraph{A \variable{@IES.@NOME} está apta para ministrar cursos de graduação nas modalidades
    presencial e Educação a Distância (EAD), sequenciais, superiores de tecnologia, de pós–graduação
    “lato sensu” (presencial e EAD), “stricto sensu” e de extensão, fundamentados no desenvolvimento
    de pesquisas, estímulos à criação cultural e ao desenvolvimento científico, embasados no
    pensamento reflexivo, que propicie a promoção de intercâmbio e cooperação com instituições
    educacionais, científicas, técnicas e culturais, nacionais e internacionais. Em seu Estatuto,
    no Art. 2º, estabelece como objetivos:}

\begin{itemize}
    \paragraph{Formar profissionais e especialistas em nível superior;}
    \paragraph{Promover a criação e transmissão do saber e da cultura em todas as suas manifestações;}
    \paragraph{Participar do desenvolvimento socioeconômico do país, em particular do estado de
    Sergipe e da região Nordeste.}
\end{itemize}"
            },
            new BookEntity
            {
                Title = "Organograma da instituição",
                Text =
@"\paragraph{ }

\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics{Organograma.png}
    \caption{Organograma da \variable{@IES.@ACRONIMO}}
}"
            },
            new BookEntity
            {
                Title = "Estrutura acadêmica e administrativa",
                Text =
@"\paragraph{ }

\code{@TABELA.@ADMINISTRACAO}"
            },
            new BookEntity
            {
                Title = "Identificação",
                Text =
@"\begin{itemize}
 \paragraph{Instituição Mantenedora}
   \begin{itemize}
       \paragraph{\run[bold]{Nome: } \run{Sociedade de Educação Tiradentes}}
       \paragraph{\run[bold]{Endereço: } \run{Rua Murilo Dantas, 300 – Bairro Farolândia}}
       \paragraph{\run[bold]{Cidade: } \run{Aracaju}}
       \paragraph{\run[bold]{Estado: } \run{Sergipe}}
       \paragraph{\run[bold]{CEP: } \run{49032-490}}
       \paragraph{\run[bold]{Tel: } \run{(079) 3218-2133 / 3218-2134}}
       \paragraph{\run[bold]{Home Page: } \run{www.unit.br}}
       \paragraph{\run[bold]{e-mail: } \run{reitoria@unit.br}}
   \end{itemize}
 \paragraph{Instituição Mantida}
   \begin{itemize}
       \paragraph{\run[bold]{Nome: } \run{\variable{@IES.@NOME}}}
       \paragraph{\run[bold]{Endereço: } \run{Rua Murilo Dantas, 300 – Bairro Farolândia}}
       \paragraph{\run[bold]{Cidade: } \run{Aracaju}}
       \paragraph{\run[bold]{Estado: } \run{Sergipe}}
       \paragraph{\run[bold]{CEP: } \run{49032–490}}
       \paragraph{\run[bold]{Tel: } \run{\variable{@CURSO.@TELEFONE}, Ramal \variable{@CURSO.@RAMAL}}}
       \paragraph{\run[bold]{Home Page: } \run{www.unit.br}}
   \end{itemize}
 \paragraph{Dados de Identificação do Curso}
   \begin{itemize}
       \paragraph{\run[bold]{Coordenador: } \run{\variable{@CURSO.@COORDENADOR}}}
       \paragraph{\run[bold]{Identificação: } \run{Curso de Graduação em \variable{@CURSO.@NOME}}}
       \paragraph{\run[bold]{Habilitação: } \run{\variable{@CURSO.@HABILITACAO}}}
       \paragraph{\run[bold]{Modalidade: } \run{\variable{@CURSO.@MODALIDADE}}}
       \paragraph{\run[bold]{Vagas: } \run{\variable{@CURSO.@VAGAS}}}
       \paragraph{\run[bold]{Turno: } \run{\variable{@CURSO.@TURNO}}}
       \paragraph{\run[bold]{Regime de Matrícula: } \run{\variable{@CURSO.@REGIMEMATRICULA}}}
       \paragraph{\run[bold]{Duração: } \run{\variable{@CURSO.@DURACAO}}}
       \paragraph{\run[bold]{Carga Horária Total: } \run{O curso tem uma carga horária total de \variable{@CURSO.@CHT50}}}
   \end{itemize}
 \paragraph{Tempo de Integralização}
   \begin{itemize}
       \paragraph{\run[bold]{Tempo mínimo: } \run{\variable{@CURSO.@TEMPOMINIMO}}}
       \paragraph{\run[bold]{Tempo máximo: } \run{\variable{@CURSO.@TEMPOMAXIMO}}}
   \end{itemize}
 \paragraph{Dimensão das Turmas}
   \begin{itemize}
       \paragraph{\run[bold]{Teóricas: } \run{\variable{@CURSO.@DIMTURMASTEORICAS}}}
       \paragraph{\run[bold]{Práticas: } \run{\variable{@CURSO.@DIMTURMASPRATICAS}}}
   \end{itemize}
\end{itemize}"
            },
            new BookEntity
            {
                Title = "Legislação e normas que regem o curso",
                Text =
@"\paragraph{
    A Base Legal para a oferta do Curso de \variable{@CURSO.@NOME} tem sua sustentação na legislação educacional,
    nos atos legais dela derivados e na legislação específica do curso, dentre os quais citamos:
    }

\begin{itemize}
    \paragraph{\run{Lei de Diretrizes e Bases da Educação Nacional/LDBN (Lei nº 9.394/96).}}
    \paragraph{\run[bold]{\variable{@CURSO.@DCN}.}}
    \paragraph{\run{O Decreto nº 5.296/2004 – Regulamenta as Leis nº 10.048/2000, que dá prioridade de atendimento às pessoas que especifica, e nº10.098/2000, que estabelece normas gerais e critérios básicos para promoção da acessibilidade das pessoas portadoras de deficiências.}}
    \paragraph{\run{O Decreto nº 5.626/2005 – Regulamenta a Lei nº10436/2002, que dispões sobre a Língua Brasileira de Sinais, Libras, e o artigo 18 da Lei nº10098/2000.}}
    \paragraph{\run{A Resolução 01/2012 – Estabelece Diretrizes Nacionais para a Educação em Direitos Humanos.}}
    \paragraph{\run{A Resolução nº 01 de 17/06/2010 da Comissão Nacional de Avaliação da Educação Superior – Normatiza o Núcleo Docente Estruturante.}}
    \paragraph{\run{A Resolução CNE nº 1/2004 – Institui Diretrizes Curriculares Nacionais para a Educação das Relações Étnico–Raciais e para o Ensino de História e Cultura Afro–Brasileira e Africana.}}
    \paragraph{\run{A Lei 11.645/2008 – Altera a Lei no 9.394, de 20 de dezembro de 1996, para incluir no currículo oficial da rede de ensino a obrigatoriedade da temática “História e Cultura Afro–Brasileira e Indígena”.}}
    \paragraph{\run{Lei 9.795/99 – Dispõe sobre a Educação Ambiental, institui a Política Nacional de Educação Ambiental e dá outras providências.}}
    \paragraph{\run{Ainda o Decreto 4.281/2002 – Regulamenta a Lei no 9.795, de 27 de abril de 1999, que institui a Política Nacional de Educação Ambiental, e dá outras providências.}}
    \paragraph{\run{Plano de Diretrizes Institucional e o Plano Pedagógico Institucional.}}
\end{itemize}
"
            },
            new BookEntity
            {
                Title = "Formas de acesso ao curso",
                Text =
@"\paragraph{
    O acesso às informações do curso de \variable{@CURSO.@NOME} ocorre através do site da
    \variable{@IES.@ACRONIMO} (www.uni.br), disponibilizando no catálogo do curso os objetivos,
    o perfil do egresso, administração acadêmica, campo de atuação, estrutura física, e valor
    da mensalidade do curso; bem como através do telefone \variable{@CURSO.@TELEFONE},
    ramal: \variable{@CURSO.@RAMAL}, e do e-mail: \variable{@CURSO.@EMAIL}.
}

\paragraph{
    Para ingressar no curso de \variable{@CURSO.@NOME} o candidato poderá concorrer ao processo
    seletivo realizado semestralmente e organizado pela Comissão Permanente de Processo
    Seletivo da Instituição, como portador de diploma ou ainda solicitar transferência externa
    ou interna. Essas vagas são definidas por meio de política institucional consubstanciada
    pela Reitoria da \variable{@IES.@NOME}, Coordenação Acadêmica, e gerenciadas pelo
    Departamento de Assuntos Acadêmicos – DAA e pela coordenação de curso.
}
"
            },
            new BookEntity
            {
                Title = "Histórico do curso: sua criação e trajetória",
                Text =
@"\paragraph{\comment{Neste item devem ser evidenciados o histórico do curso ressaltando como o mesmo atende às características e às necessidades da região, justificando a importância no contexto local e regional, salientando a pertinência, relevância social e científica do curso. Ainda em relação a justificativa de oferta é pertinente destacar que a mesma está respaldada na demanda social, na existência de condições físicas e humanas, no crescimento da população em idade escolar, no crescimento social e econômico da região, entre outras questões do mesmo gênero. O TEXTO DEVE DEMONSTRAR O POTENCIAL DE OFERTA DO CURSO, DE FORMA CLARA E EXPRESSIVA.}{Sergipe tem se destacado pela crescente melhoria quanto à situação econômica e social, com mudanças contínuas no seu modo de vida, produção e distribuição de riquezas, dispondo da infraestrutura básica para o seu desenvolvimento, incluindo-se os espaços públicos, privados, urbanos e rurais necessários para a realização de atividades econômicas, sociais e culturais.}}
\paragraph{Neste contexto bastante promissor, a educação se traduz num fator fundamental para mudanças sociais, econômicas. Corroborando com este desafio, a \variable{@IES.@ACRONIMO} por meio dos serviços educacionais vem contribuindo para transformação da sociedade através da disseminação de conhecimentos culturais, científicos e técnicos, consubstanciando-se tanto nas ações de ensino e extensão, como também em atividades de pesquisa, desenvolvidos por meio da sua inserção social, mediante a articulação com o contexto local e regional, conhecendo os seus problemas, prestando serviços especializados e estabelecendo com a comunidade uma relação de reciprocidade.}
\paragraph{O curso proposto pela \variable{@IES.@ACRONIMO} traz consigo reflexões sobre a essência da \variable{@CURSO.@NOME} no atual contexto – era da informação, tecnologia e globalização, com grande desenvolvimento tecnológico nos processos produtivos industriais. Simultaneamente, exige o início de um processo de produção cada vez mais industrializado, com formas inovadoras, visando uma melhoria das condições de vida, criando soluções que demonstrem o compromisso do engenheiro com o exercício da cidadania, um profissional com formação generalista, capaz de avaliar criticamente o impacto social e a viabilidade econômica.}
\paragraph{Desse modo, a \variable{@IES.@ACRONIMO} vislumbra a preparação de profissionais para exercerem suas atribuições nos diversos campos de atuação, ampliando o mercado de trabalho, cuja relevância social e científica se traduzirá em um curso atualizado com o que se faz no mundo, calcado em uma dimensão investigativa, científica e, sobretudo realista das condições locais para as quais são necessárias propostas de mudança de um quadro social adverso. Trata-se, portanto, de um curso planejado e estruturado para oferecer ao estado e à região profissionais habilitados e preparados sob a égide de uma formação generalista balizada pelo compromisso com as questões sociais da região, levando em conta aspectos como: desenvolvimento sustentável; preservação do meio ambiente e desenvolvimento de projetos e ações mais econômicos, seguros e eficientes, e que contribuam para a diminuição do consumo de energia – aspectos que incidem diretamente na melhoria da qualidade de vida.}
\paragraph{Procurando acompanhar os avanços técnicos e científicos, a distribuição regular de créditos e disciplinas, a qualidade do conteúdo programático e uma carga horária compatível, o curso de \variable{@CURSO.@NOME} implantará uma matriz curricular que atenda às Diretrizes Curriculares do MEC, permitindo uma constante articulação entre teoria e prática considerando o contexto socioeconômico, as demandas atuais e a busca contínua pela melhoria de produtos e serviços.}
\paragraph{A inclusão dos conhecimentos básicos e decorrentes nas disciplinas da estrutura curricular do curso ocorreu no sentido de fazer uma opção pelo fundamento prático-teórico, sem, no entanto, deixar de lado a formação sociocultural. Outra característica desta proposta é a de concentrar as disciplinas que exploram as atividades práticas em \variable{@CURSO.@NOME} na formação do ciclo profissionalizante.}
\paragraph{O currículo pleno proposto guarda congruência com a filosofia da prática profissionalizante, ao propor conteúdos de formação humanística como: Filosofia, Cidadania e Fundamentos Antropológicos e Sociológicos, ao mesmo tempo em que aprofunda estudos na área dos conteúdos profissionalizantes que têm o papel de fornecer conhecimentos passíveis de aplicação profissional.}
\paragraph{A necessidade de se formar profissionais com visão interdisciplinar, habilitados a atender o mercado de automação e capaz de utilizar adequadamente as novas tecnologias da informática, confirma a relevância da oferta do curso de graduação em \variable{@CURSO.@NOME} se considerando que tais profissionais deverão conviver em contextos de mudanças sociais, tecnológicas e econômicas cada vez mais dinâmicas, tais como:}
\begin{itemize}
\paragraph{A globalização, desregulamentação dos mercados, aumento de incertezas, melhores oportunidades associadas a maiores riscos;}
\paragraph{A rápida mudança tecnológica, sendo a capacitação tecnológica e sua integração à estratégia de negócios os determinantes principais da competitividade das empresas;}
\paragraph{As novas oportunidades e novos problemas exigindo conhecimentos multidisciplinares, trabalho em equipe, visão de mercado e atitude empreendedora;}
\paragraph{O trabalho em equipes multidisciplinares, possuindo larga base científica e capacidade de comunicação;}
\paragraph{O gerenciamento de seu próprio fluxo de informações, autorreciclável;}
\paragraph{A criação, o projeto e o gerenciamento das intervenções tecnológicas como solucionador de problemas de base tecnológica;}
\paragraph{A capacidade de empreender e construir seu futuro, procurando seu nicho de trabalho;}
\paragraph{A atuação como transformador social que avalie os impactos sociais éticos e ambientais.}
\end{itemize}
\paragraph{Ao propor o curso de \variable{@CURSO.@NOME}, a \variable{@IES.@ACRONIMO} avança pedagogicamente através de uma proposta inovadora focada no desenvolvimento de competências e amparada na Lei de Diretrizes e Bases da Educação Nacional – nº 9.394/96. O Curso de \variable{@CURSO.@NOME} foi concebido com a premissa de ampliar e desenvolver o processo de conhecimento e de saberes ressaltando a interdisciplinaridade como procedimento metodológico, por se entender que o mundo atual exige não mais um especialista em uma determinada área, mas um profissional capaz de atuar de forma integrada, que seja dinâmico, generalista, consciente e ético.}
\paragraph{Diante da necessidade de atender o compromisso institucional de capacitar, qualificar e desenvolver recursos humanos competentes nas diversas áreas profissionais, a \variable{@IES.@ACRONIMO}, busca com o curso de \variable{@CURSO.@NOME}, oferecer um ensino de graduação de qualidade, capaz de estabelecer elementos da conjuntura, tanto econômica e social, quanto do desenvolvimento da área de conhecimento, enfatizando a formação acadêmica e profissional voltada para as necessidades de desenvolvimento do país. O curso disponibiliza ambientes de aprendizagem com recursos humanos e materiais para a formação de cidadãos profissionais socialmente responsáveis, comprometidos com a sustentabilidade do meio ambiente e com habilidades atitudinais que lhes conferem a eficácia pessoal necessária para um bom relacionamento com as pessoas e equipes de trabalho.}
\paragraph{A partir do exposto, a \variable{@IES.@ACRONIMO} apresenta o curso \variable{@CURSO.@NOME} visando suprir a necessidade social de formação profissional comprometida com os valores éticos e profissionais. Para este fim, o curso disponibiliza ambientes de aprendizagem com recursos humanos e materiais que possibilitam a formação de cidadãos profissionais socialmente responsáveis, comprometidos com a sustentabilidade do meio ambiente e com habilidades atitudinais que lhes conferem a eficácia pessoal necessária para um bom relacionamento com as pessoas e equipes de trabalho.}
"
            },
            new BookEntity
            {
                Title = "Objetivos do curso",
                Text = @""
            },
            new BookEntity
            {
                Title = "Perfil profissiográfico",
                Text =
@"
\Paragraph{
	O egresso do curso deverá ser capaz de atuar em setores industriais, comerciais e
	residenciais, por meio de prestação de serviços em empresas de consultoria e/ou de forma
	autônoma, planejando soluções de longo prazo, participando de pesquisa e desenvolvimento
	de instrumentos, modelos e sistemas, visando a otimização e a modernização de \variable{@PROFISSAO.@SISTEMA}.
	}
\Paragraph{
	O perfil profissiográfico do egresso do curso de \variable{@CURSO.@NOME} da
	\variable{@IES.@ACRONIMO} foi elaborado a partir da concepção e objetivos do curso, em
	consonância com as Diretrizes Curriculares Nacionais (DCN), tendo em vista as peculiaridades
	regionais e a necessidade do profissional em adaptar-se às constantes mudanças na sua área de
	formação.
	}
\Paragraph{
	Essa formação profissional é possibilitada pela aquisição de conhecimentos que envolvem
	dimensões distintas, \comment{Conferir se se trata das DCNs novas!}{destacando os perfis
	constantes nas DCN do curso de Engenharia em seu artigo 3º:}
	}

\begin{itemize}
	\Paragraph{ter visão holística e humanista, ser crítico, reflexivo, criativo, cooperativo
		e ético e com forte formação técnica;}
	\Paragraph{estar apto a pesquisar, desenvolver, adaptar e utilizar novas tecnologias,
		com atuação inovadora e empreendedora;}
	\Paragraph{ser capaz de reconhecer as necessidades dos usuários, formular, analisar e
		resolver, de forma criativa, os problemas de Engenharia;}
	\Paragraph{adotar perspectivas multidisciplinares e transdisciplinares em sua prática;}
	\Paragraph{considerar os aspectos globais, políticos, econômicos, sociais, ambientais,
		culturais e de segurança e saúde no trabalho;}
	\Paragraph{atuar com isenção e comprometimento com a responsabilidade social e com o
		desenvolvimento sustentável.}
\end{itemize}

\Paragraph{
	Desta forma, o curso de \variable{@CURSO.@NOME} deverá formar cidadãos profissionais
	com um perfil generalista, humanista, científico e empreendedor, para atuarem com
	criatividade e criticidade na identificação e resolução de problemas tecnológicos,
	considerando aspectos éticos, humanísticos, científicos, econômicos, sociais,
	ambientais, culturais e políticos, em atendimento às demandas da sociedade. Para
	tanto, o egresso do aluno de \variable{@CURSO.@NOME} será capacitado a exercer ações
	profissionais com resiliência, propositividade e proatividade, de forma individual ou
	em equipe, sempre atento às boas práticas na concepção, modelagem, implementação e no
	gerenciamento de projetos de produtos, processos e serviços, com visão multidisciplinar,
	inovadora e empreendedora.
}
"
            },
            new BookEntity
            {
                Title = "Campo de atuação",
                Text =
@"
\Paragraph{
	As áreas e espaços que os \variable{@PROFISSAO.@PLURAL} podem atuar estão relacionadas com o
	projeto, pesquisa e desenvolvimento de máquinas, equipamentos e instalações para o controle
	da energia térmica e/ou o controle do fluxo de gases e líquidos, tais como o bombeamento,
	refino, processamento e distribuição de produtos na petroquímica. Ainda no contexto das
	indústrias petroquímicas, o Engenheiro Mecânico pode atuar como gestor tanto da planta como
	de equipes de trabalho.}
\Paragraph{
	Outra grande área de atuação do \variable{@PROFISSAO.@SINGULAR} é a metalurgia, na qual
	o \variable{@PROFISSAO.@SINGULAR} realiza projetos, seleciona materiais, fabrica peças,
	organiza a produção, gerencia suprimentos, recursos e equipes.}
\Paragraph{
	Na indústria, particularmente na automobilística, o \variable{@PROFISSAO.@SINGULAR} é
	responsável por elementos da cadeia de produção, indo desde o projeto de componentes,
	gestão e manutenção da linha de produção. Em muitas indústrias, além das atuações típicas
	da gestão, os \variable{@PROFISSAO.@PLURAL} também podem ter a oportunidade de conceber,
	projetar, construir e testar protótipos, tais como na indústria de eletrodomésticos.}
\Paragraph{\comment{Precisa melhorar isso aqui!}{Na construção civil de grandes espaços, o
	\variable{@PROFISSAO.@SINGULAR} projeta sistemas de refrigeração e acondicionamento.}}
"
            },
            new BookEntity
            {
                Title = "Aspectos físicos e demográficos",
                Text =
@"\paragraph{
    O Estado de Sergipe possui uma área de 21.910,3 km, o equivalente a 0,26% do território nacional
    e 1,4% da região Nordeste, onde está localizado. Limita-se ao norte com o Estado de Alagoas,
    separados pelo Rio São Francisco, ao sul e a oeste com o Estado da Bahia e ao leste com o Oceano
    Atlântico. Sergipe possui 75 municípios agrupados em 13 microrregiões político-administrativas,
    que fazem parte de três mesorregiões, conforme definido pelo IBGE.
}

\paragraph{
    Algumas vantagens do Estado, tais como: posição geográfica, riqueza de patrimônio histórico e
    construído, beleza natural e paisagística e variada cultura popular, o potencializam como o
    portão de entrada para o turismo no Nordeste.
}

\paragraph{
    A capital sergipana, Aracaju, possui 35 km de litoral com praias de águas mornas e calmas e
    rios propícios para pesca artesanal. A vegetação predominante é o manguezal, que se concentra
    às margens dos rios; além dos mangues, também são consideradas áreas de preservação ambiental,
    algumas restingas e o Morro do Urubu, um dos últimos remanescentes de Mata Atlântica, que atrai
    turistas de todas as partes do Brasil e do exterior.
}

\paragraph{
    A população de Sergipe se caracteriza pela mestiçagem  resultante da presença de vários elementos
    étnicos, já que em seu histórico estão presentes indivíduos de origem europeia, indígena e africana,
    além de tipos humanos vindos de diversas partes do mundo.
}

;; Figura 2: Localização geográfica do Estado de Sergipe
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,6]{PontosExtremos.png}
    \caption{Localização geográfica do Estado de Sergipe \footnote{Fonte: Sergipe em Dados 2011.}.}
}

\paragraph{
    O Estado de Sergipe possui como característica climática principal a distribuição espacial da
    precipitação pluviométrica decrescente do Litoral Leste para o Sertão Semiárido.
}

;; Figura 3: Tipos Climáticos do Estado de Sergipe
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,6]{Clima.png}
    \caption{Tipos Climáticos do Estado de Sergipe \footnote{Fonte: Centro de Meteorologia de Sergipe – CEMESE/SRH/SEMARH}.}
}
"
            },
            new BookEntity
            {
                Title = "Aspectos econômicos",
                Text =
@"\paragraph{
    Apesar de sua pequena dimensão territorial Sergipe é um estado diferenciado dentro do Nordeste
    e possui os melhores indicadores econômicos e sociais da região. Nos últimos anos, tem
    apresentado desempenho superior à média do Brasil e do Nordeste em várias dimensões do
    desenvolvimento devido ao importante processo de transformação por que vem passando.
}

\paragraph{
    Sergipe, conforme dados do IBGE, tem nos setores de serviços e indústria, sua principal fonte
    de geração de riqueza. A participação destes setores no Valor Adicionado Bruto – VAB é,
    respectivamente, de 66,8% e 28,6%. O setor agropecuário, com menor expressividade, aparece
    com um percentual de 4,6%.
}

;; Figura 4: Participação destes setores no Valor Adicionado Bruto – VAB
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,5]{Riquesas.png}
    \caption{Participação destes setores no Valor Adicionado Bruto – VAB \footnote{Fonte: IBGE (2012) / Contas Regionais 2010.}.}
}

\paragraph{
    A extração de riquezas minerais ambientais, além de outros minérios como a silvinita e a
    carnalita, matérias-primas fundamentais para a fabricação de fertilizantes tem sido um dos
    fatores de crescimento do Estado. Sergipe dispõe também de importantes jazidas de calcário,
    que o tornaram o maior produtor de cimento do Nordeste e o sexto maior do Brasil.
}

\paragraph{
    Ao lado da riqueza mineral, que propiciou a formação de uma importante cadeia produtiva
    minero-química, Sergipe conta ainda com um parque produtivo diversificado, em que se
    destacam os segmentos de alimentos e bebidas; têxtil, calçados e confecções; produtos
    metalúrgicos e material elétrico.
}

\paragraph{
    Segundo dados divulgados pelo IBGE, no ano de 2011 o Produto Interno Bruto (PIB) de Sergipe,
    cresceu em volume 9,47% em relação ao ano de 2010. A economia sergipana apresentou um
    crescimento maior que os dos PIB''s do Brasil (2,7%) e do Nordeste (9,42%). Na base de 2011,
    o PIB sergipano é de R$ 26.199 milhões, o que representa 0,6% do PIB do país e coloca Sergipe,
    menor estado do país, na 22ª posição entre as unidades federativas.
}

\paragraph{
    Comparado ao restante dos estados nordestinos, o PIB per capita de Sergipe, de R$ 12.536,45,
    também permanece sendo superior e o coloca como o maior PIB per capita do Nordeste. É
    importante ressaltar que o PIB per capita do Brasil, foi de R$ 21.535,65 e o da Região
    Nordeste de R$ 10.379,55.
}

\paragraph{
    A eficiência econômica de Sergipe, também está refletida nos dados referentes à relação
    emprego/renda. No último relatório divulgado pela Federação das Indústrias do Rio de
    Janeiro (Firjan), o Estado aparece em 3º lugar no Índice de Desenvolvimento Municipal
    (IFDM) entre as capitais do Nordeste e na décima quinta posição em nível nacional.
}

\paragraph{
    Segundo dados do MTE-CAGED, o emprego formal em Sergipe aumentou 53% entre janeiro de
    2007 e dezembro de 2012, frente aos 46% de crescimento do Nordeste e 39% da média do
    Brasil. Em 2012, conforme dados fornecidos pelo governo estadual, o saldo de movimentações
    no mercado de trabalho sergipano fechou o ano, registrando um total de 6.583 empregos
    formais gerados na economia estadual. Um dos destaques em Sergipe foi o setor de construção
    civil, que gerou um saldo de 3.015 novos postos de trabalho no Estado.
}"
            },
            new BookEntity
            {
                Title = "Aspectos educacionais",
                Text =
@"\paragraph{
    Atualmente, segundo dados fornecidos pela Secretaria de estado da Educação – SEED, o Estado
    de Sergipe atendeu ao número de 57.582 matrículas no ensino médio. Desta forma, contamos
    com os inúmeros concludentes do ensino médio que ainda não tiveram acesso ao ensino superior.
    Isso, sem levar em conta os portadores de diploma que já se encontram inseridos no mercado
    de trabalho, mas que buscam outra graduação ou pós-graduação como forma de requalificação
    e ascensão na carreira profissional.
}

\paragraph{
    Conforme dados do Instituto Brasileiro de Geografia e Estatística (IBGE) a frequência do
    Ensino Médio entre os adolescentes sergipanos cresceu e que 40,9% deles estão cursando o
    Ensino Médio. Na faixa etária de 6 a 14 anos, Sergipe está mais próximo da universalização:
    98,1% de frequência escolar. No grupo de 0 a 5 anos, a frequência é maior entre aqueles com
    idade de 4 e 5 anos (87,2%) e muito menor no grupo de 0 a 3 anos (15,2%). A proporção de
    jovens estudantes com idade de 18 a 24 anos que cursavam o nível superior cresceu de 27%
    em 2001 para 51,3% em 2011. Outra informação registrada pelo estudo é que jovens estudantes
    pretos e pardos aumentaram a frequência no Ensino Superior – de 10,2% em 2001 para 35,8% em
    2011. Tais índices mostram a democratização do acesso à educação e o investimento que vem
    sendo demandado para área.
}"
            },
            new BookEntity
            {
                Title = "A UNIT frente ao desenvolvimento do estado de Sergipe e da região",
                Text =
@"\paragraph{
    O Plano Nacional de Educação – PNE propõe como meta universalizar até 2016, o atendimento
    escolar da população de 4 e 5 anos, e ampliar a oferta de educação infantil de forma a
    atender a 50% da população de até 3 anos. Trata-se de objetivo imprescindível para assegurar
    aprendizado efetivo no ensino fundamental e médio, reduzindo a repetência e aumentando a
    taxa de sucesso na educação básica.  Ainda na educação básica, prevê-se, como meta 2,
    universalizar o ensino fundamental de nove anos para toda população de 6 a 14 anos; e,
    como meta 3, universalizar, até 2016, o atendimento escolar para toda a população de 15 a
    17 anos e elevar, até o final da década, a taxa líquida de matrículas no ensino médio
    para 85%, nesta faixa etária.
}

\paragraph{
    O Estado de Sergipe, conta com 12 instituições de ensino superior, das quais uma
    Universidade pública e uma, particular (a \variable{@IES.@ACRONIMO}), um Instituto Federal de
    Educação, um Centro Universitário, sendo as demais constituídas por faculdades.
}

\paragraph{
    Neste cenário, a \variable{@IES.@ACRONIMO}, instituição de ensino superior com atuação em
    todos os níveis e áreas, assume o compromisso com a difusão e aplicação do conhecimento e
    do saber, promovendo a aquisição e desenvolvimento de habilidades e competências por meio
    da oferta de cursos de graduação e de pós-graduação além do incentivo ao desenvolvimento
    da pesquisa e da extensão em todas as áreas em que atua. Com tal perspectiva, a busca da
    excelência do ensino constitui-se numa diretriz basilar para permitir a implantação de
    propostas educacionais arrojadas e adequadas ao contexto contemporâneo, visando atender
    a amplitude e a diversidade da demanda por profissionais especializados aptos a atuarem
    em nossa sociedade.
}

\paragraph{
    A \variable{@IES.@ACRONIMO} tem como sede a Capital do Estado de Sergipe, onde se localizam
    os Campi Aracaju Centro e Aracaju Farolândia. Atua também no interior do Estado através de
    campi avançados, na cidade de Estância, região sul de Sergipe; no município de Itabaiana,
    leste sergipano e em Propriá, cidade fronteira situada na região norte do estado e nos 32
    polos de Educação à Distância localizados nos 22 municípios sergipanos e nos Estados de
    Alagoas, Bahia, Pernambuco e Rio Grande do Norte. Todos esses espaços revelam sua vocação
    empreendedora, tanto no que se refere ao serviço prestado quanto ao desenvolvimento social
    e urbano que provoca.
}

\paragraph{
    Diante das mudanças que tem se anunciado com o novo Plano Nacional de Educação, sancionado
    em 25 de junho de 2014, e publicado no Diário Oficial da União no dia 26, sob a Lei de
    no 13.005/2014 este cenário tende a se ampliar. No referido plano, uma das grandes
    conquistas foi a aprovação da destinação de no mínimo, 10% do Produto Interno Bruto – PIB,
    do investimento público, em educação pública até o final do decênio.
}

\paragraph{
    No campo da educação superior, por exemplo, estabelece que nos próximos 10 anos será
    necessário elevar a taxa bruta de matrícula para 50% e a taxa
    líquida para 33% da população de 18 a 24 anos, assegurada a qualidade da oferta e expansão para,
    pelo menos, 40% das novas matrículas, no segmento. A proposta é de universalizar o atendimento
    escolar da população ampliando a oferta da Educação Básica e Superior. Trata-se de
    objetivo imprescindível para assegurar aprendizado efetivo, reduzindo a repetência e
    aumentando a taxa de sucesso na educação básica e progressivamente na educação superior.
}

\paragraph{
    No que se refere ao Ensino Superior, os desafios são bastante significativos, sobretudo
    em relação ao papel de formação profissional atribuída às instituições de ensino.
}

;; Figura 5: Educação Superior – Matrículas por faixa etária, Brasil.
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,9]{superior.png}
    \caption{Educação Superior – Matrículas por faixa etária, Brasil \footnote{Fonte: INEP (2011).}.}
}"
            },
            new BookEntity
            {
                Title = "Dados sobre a saúde",
                Text =
@"\paragraph{
    Segundo dados fornecidos pela Secretaria de Estado do Planejamento a expansão da rede de
    atenção à saúde e na melhoria da gestão do SUS impactou fortemente nos indicadores de
    saúde em Sergipe. O número de casos de doenças associadas à miséria, como tuberculose,
    hanseníase, meningite, doenças diarreicas, entre outras, vem diminuindo constantemente.
    A mortalidade infantil sofreu uma queda de 57,2% na última década, estando muito
    próxima de atingir, antecipadamente, a meta dos Objetivos do Milênio (ODM) até 2015.
}

\paragraph{
    A esperança de vida ao nascer da população sergipana passou de 68,8 anos em 2001 para
    72,2 anos em 2011, um incremento de 3,4 anos. A população sergipana continua crescendo
    segundo o Instituto Brasileiro de Geografia e Estatística (IBGE). Um dado que comprova
    este crescimento é demonstrado em 2013 através do número de habitantes correspondente
    a 2.195.662, comparado ao ano anterior que chegou a marca de 2.110.867 pessoas,
    perfazendo um aumento de 4%.
}

\paragraph{
    Os cinco municípios mais populosos são Aracaju com 614.577 habitantes, Nossa Senhora
    do Socorro, com 172.547 pessoas, Lagarto com 100.330, Itabaiana tem 91.873 habitantes,
    São Cristóvão com 84.620 pessoas. O maior crescimento absoluto da população foi registrado
    na capital sergipana, um aumento de 26.876 habitantes, sendo que o maior crescimento
    relativo foi verificado na cidade de Carmópolis, com acréscimo de 807 na população.
}

;; Figura 6: Evolução esperança de vida ao nascer em Sergipe
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,8]{saude1.png}
    \caption{Evolução esperança de vida ao nascer em Sergipe \footnote{Fonte: IBGE/DPE/Coordenação de População e Indicadores Sociais – COPIS.}.}
}

\paragraph{
    Ainda segundo dados fornecidos pela Secretaria de Planejamento, o aumento da esperança
    de vida dos sergipanos é consequência da melhoria das condições e vida e no acesso a
    serviços de saúde, observado praticamente em todos os estados do nordeste, com destaque
    para Bahia e Sergipe que apresentam as maiores expectativas de vida da região,
    aproximando-se, na última década, da média nacional.
}

;; Figura 7: Esperança de vida ao nascer 2001 a 2011
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,8]{saude2.png}
    \caption{Esperança de vida ao nascer 2001 a 2011 \footnote{Fonte: IBGE/DPE/Coordenação de População e Indicadores Sociais – COPIS.}.}
}

\paragraph{
    Ações de prevenção e controle desenvolvidas pelas secretarias municipais e estaduais
    de saúde, com equipes multidisciplinares vêm colaborando para mudanças de hábitos da
    população, tais ações evidenciam a redução nos índices de mortalidade por AVC no estado
    que tem como fatores de risco a idade avançada, hipertensão arterial e hábitos não
    saudáveis, a mortalidade por AVC -Acidente Vascular Cerebral vem caindo nos últimos
    cinco anos. A mortalidade causada por este acidente, na faixa etária de até 70 anos,
    saiu de 8,26 em 2005, para 5,89 em 2010, representando uma queda de 28,7% no período.
}

;; Figura 8: Taxa de mortalidade ajustada (por 100mil) por AVC na população com menos de 70 anos
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,8]{saude5.png}
    \caption{Taxa de mortalidade ajustada (por 100 mil) por AVC na população com menos de 70 anos \footnote{Fonte: SIM/NSI/DIVEP/SES/IBGE.}.}
}

\paragraph{
    No que se refere à redução da mortalidade infantil no Estado de Sergipe se aproxima
    da meta de redução da mortalidade definida pelos Objetivos de Desenvolvimento do
    Milênio – ODM, a taxa de mortalidade infantil (menores de um ano de idade), recuou
    de 37,6 óbitos por mil nascidos vivos, em 2001, para16,1 por mil, em 2011. Com este
    resultado, Sergipe praticamente atingiu a meta da ODM, estipulada em 15,7 óbitos por
    mil nascidos vivos.
}

;; Figura 9: Mortalidade infantil por mil nascidos vivos 2001 a 2011
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,9]{saude3.png}
    \caption{Mortalidade infantil por mil nascidos vivos 2001 a 2011 \footnote{Fonte: MS/SVS – sistema de informações sobre nascidos vivos – SINASC/SIM.}.}
}

;; Figura 10: Taxa de mortalidade infantil por Estado
\figure[beforelines=3;FirstLine=0;Justification=center]{
    \includegraphics[scale=0,7]{saude4.png}
    \caption{Taxa de mortalidade infantil por Estado \footnote{Fonte: MS/SVS – sistema de informações sobre nascidos vivos – SINASC/SIM.}.}
}

\paragraph{
    O declínio na mortalidade infantil pode ser observado em todos os estados do Nordeste.
    No ano 2001 a média de óbitos da região, que girava em torno de 40 por mil nascidos vivos,
    cai para cerca de 15 por mil nascidos vivos em 2011, uma redução de mais de 62%. A taxa
    de redução média em Sergipe ficou em torno de 5,7% (a.a.).
}

\paragraph{
    Também muito significativo foi a diminuição no índice de mortalidade materna estadual,
    o número de óbitos por mortalidade materna diminui entre os anos de 2002 e 2010, a taxa
    saiu de 79,22 para 67,57, por 100 mil, com queda de 14,7% no período. Esta redução é
    ainda mais significativa se considerada a melhora na identificação dos óbitos associados
    à gravidez no estado, com o expressivo aumento de óbitos investigados de mulheres em
    idade fértil entre 2008 e 2010, saindo de 9 casos para 554 casos.
}

\paragraph{
    Diante de tal cenário, manter e melhorar ainda mais os índices apresentados torna-se
    um desafio para os administradores municipais e para o governo estadual, identifica-se
    que o estado de Sergipe vive um momento favorável para o desenvolvimento de políticas
    públicas de saúde o que torna imprescindível a necessidade de profissionais capacitados.
}"
            },
            new BookEntity
            {
                Title = "Políticas institucionais no âmbito do curso",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO}, em consonância com o contexto atual e atenta às novas
    tendências educacionais e profissionais, assume em seu Projeto Pedagógico o compromisso
    de formar profissionais dotados de um saber que se alicerça nas mais recentes teorizações
    da ciência, integradas com o desenvolvimento e melhoria das condições de vida das comunidades
    onde atua. Para tanto, busca na indissociabilidade entre ensino, pesquisa e extensão, o
    embasamento para uma atuação pedagógica qualificada. Nesta perspectiva concebe:
}

\begin{itemize}
    \paragraph{Ensino como processo de socialização e produção coletiva do conhecimento.}
    \paragraph{Pesquisa como princípio educativo a permear todas as ações acadêmicas da Universidade,
        bem como as atividades desenvolvidas no âmbito da iniciação científica.}
    \paragraph{Extensão como processo de interação com a comunidade, a partir de ações contextualizadas
        da aprendizagem e o cumprimento da função social da Instituição.}
\end{itemize}

\paragraph{
    Ao assumir o desafio de promover a educação para a autonomia, propõe o questionamento
    sistemático, crítico e criativo pelos agentes formadores e em formação dos processos e
    das práticas a serem empreendidas. Em consonância com o Projeto Pedagógico Institucional,
    que preconiza a articulação entre teoria e prática, o curso de \variable{@CURSO.@NOME}
    contempla, desde os primeiros períodos, ações que visam colocar o aluno em contato
    com a realidade social e profissional em que irá atuar, como forma de promover a
    ação-reflexão-ação sobre esta, a exemplo do eixo integrador e do eixo de práticas profissionais.
}"
            },
            new BookEntity
            {
                Title = "Políticas de ensino",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} adota como referencial pedagógico a prática da
    “\run[BOLD]{educação ao longo de toda a vida}”, conforme apresentado pela UNESCO no
    Relatório da Comissão Internacional sobre a Educação para o Século XXI. Com base neste
    referencial, a educação tem como objetivo proporcionar ao indivíduo um conhecimento
    dinâmico do mundo, dos outros e de si mesmos, capacitando-o para o exercício
    profissional em tempos de mudanças. À educação cabe orientar como uma bússola,
    os mapas que permitem a compreensão de um mundo complexo, dinâmico e em constante
    processo de mudança, permitindo ao educando navegar através dele e se posicionar
    diante das questões que lhes são postas.
}

\paragraph{
    A “\run[BOLD]{educação ao longo de toda a vida}” organiza-se em torno de quatro
    aprendizagens fundamentais, que constituem os pilares do conhecimento:
}

\begin{itemize}
    \paragraph{“\run[BOLD]{Aprender a conhecer}” significa, antes de tudo, o aprendizado dos
        métodos que nos ajudam a distinguir o que é real do que é ilusório e ter, assim, acesso
        aos saberes de nossa época. A iniciação precoce na ciência é salutar, pois ela dá
        acesso, desde o início da vida humana à não-aceitação de qualquer resposta sem
        fundamentação racional e/ou de qualquer certeza que esteja em contradição com os fatos;}
    \paragraph{“\run[BOLD]{Aprender a fazer}” é um aprendizado da criatividade. “\run[ITALIC]{Fazer}”
        também significa “criar algo novo”, trazer à luz as próprias potencialidades criativas,
        para que venha a exercer uma profissão em conformidade com suas predisposições interiores;}
    \paragraph{“\run[BOLD]{Aprender a viver juntos}” significa, em primeiro lugar, respeitar as
        normas que regulamentam as relações entre os seres que compõem uma coletividade.
        Porém, essas normas devem ser verdadeiramente compreendidas, admitidas interiormente
        por cada ser, e não sofridas como imposições exteriores. “\run[ITALIC]{Viver junto}” não quer
        dizer simplesmente tolerar o outro com suas diferenças embora permanecendo convencido da
        justeza absoluta das próprias posições;}
    \paragraph{“\run[BOLD]{Aprender a ser}” implica aprender que a palavra “\run[ITALIC]{existir}”
        significa descobrir os próprios condicionamentos, descobrir a harmonia ou a desarmonia
        entre a vida individual e social.}
\end{itemize}

\paragraph{
    Focada nessas premissas norteadoras, a \variable{@IES.@ACRONIMO} adota uma educação capaz
    de promover situações de ensino e aprendizagem com foco na construção de conhecimentos e
    no desenvolvimento de habilidades e competências. Nessa perspectiva, incorpora a realização
    das situações de ensino e vivências acadêmicas, abordagens que busquem:
}
\begin{itemize}
    \paragraph{O desenvolvimento curricular contextualizado e circunstanciado;}
    \paragraph{A busca da unidade entre teoria e prática;}
    \paragraph{A integração entre ensino, pesquisa e extensão;}
    \paragraph{A construção permanente da qualidade de ensino.}
\end{itemize}

\paragraph{
    A política de ensino da \variable{@IES.@ACRONIMO} fundamenta-se em um processo educativo que favorece o
    desenvolvimento de profissionais capacitados para atenderem às necessidades e expectativas
    do mercado de trabalho e da sociedade, com competência para formular, sistematizar e
    socializar conhecimentos em suas áreas de atuação. São princípios básicos dessa política:
}
\begin{itemize}
    \paragraph{Formação de profissionais nas diferentes áreas do conhecimento;}
    \paragraph{Cuidado e atenção às necessidades da sociedade e região no que concerne à oferta
        de cursos e programas para a formação e qualificação profissional;}
    \paragraph{Valorização dos princípios éticos;}
    \paragraph{Flexibilização dos currículos, de forma a proporcionar ao aluno a maior medida
        possível de autonomia na sua formação acadêmica;}
    \paragraph{Atualização permanente dos projetos pedagógicos, levando-se em consideração as
        Diretrizes Curriculares, a dinâmica dos perfis profissiográficos dos cursos ofertados,
        e as demandas da região onde a Instituição está inserida.}
\end{itemize}

\paragraph{
    A \variable{@IES.@ACRONIMO} se caracteriza como instituição de ensino superior – em todos
    os níveis e áreas, comprometida com a difusão e aplicação do conhecimento e do saber,
    promovendo a aquisição e desenvolvimento de habilidades e competências por meio da formação
    superior inicial, continuada e integral. Como instituição educacional, propõe-se promover
    a educação superior e a ciência, através da oferta de cursos de graduação e pós-graduação
    e o desenvolvimento da pesquisa e da extensão nas suas áreas de atuação. Nesse sentido, a
    busca da excelência do ensino constitui-se numa diretriz basilar para permitir a implantação
    de propostas educacionais arrojadas e adequadas ao contexto contemporâneo, visando atender
    a amplitude e a diversidade da demanda por profissionais especializados aptos a atuarem em
    nossa sociedade.
}

\paragraph{
    Nessa direção, a \variable{@IES.@ACRONIMO} oferta cursos de graduação, cursos de pós-graduação
    como caminho de formação continuada com atualização profissional e produção de conhecimento
    em diversas áreas, assim como também fortalece a pesquisa e a extensão numa política de
    articulação prevendo a indissociabilidade entre ensino, pesquisa e extensão com responsabilidade
    social.
}"
            },
            new BookEntity
            {
                Title = "Políticas de pesquisa",
                Text =
@"\paragraph{
    A pesquisa na \variable{@IES.@ACRONIMO} se constitui princípio pedagógico, de modo a
    incentivar a busca de informações nas atividades acadêmicas, assim como a realização de
    práticas investigativas por meio do Programa de Iniciação Científica. Desse modo, visa
    desenvolver uma ação contínua que, por meio da educação, da cultura e da ciência, busca
    unir o ensino e a investigação, propiciando, através dos seus resultados, uma ação
    transformadora entre a academia e a população.
}

\paragraph{
    Neste sentido, serão incentivadas as práticas investigativas que propiciem fomento ao aprofundamento
    do conhecimento científico, técnico, cultural e artístico por meio do incentivo permanente, em todas
    as práticas acadêmicas, da busca de informações nas mais diversas fontes de consulta disponíveis, de
    modo a desenvolver a curiosidade científica e o espírito investigativo dos alunos, dentre os quais:
}

\begin{itemize}
    \paragraph{Estímulo e incentivo ao pensar crítico em qualquer atividade didático-pedagógica.}
    \paragraph{Fomento à realização de práticas de investigação focada na temática da região onde a
        \variable{@IES.@ACRONIMO} se insere.}
    \paragraph{Manutenção de serviços de apoio indispensáveis às práticas de investigação, tais como,
        biblioteca, documentação e divulgação científica.}
    \paragraph{Promoção de iniciação científica através do Programa de Bolsas de Iniciação Científica – PROBIC
        e Programa Voluntário de Iniciação Científica – PROVIC.}
    \paragraph{Fomento às parcerias e convênios com organizações públicas e privadas para a realização
        das práticas investigativas de interesse mútuo.}
    \paragraph{Incentivo à programação de eventos científicos e a participação em congressos, simpósios,
        seminários e encontros, tais como a Semana de Pesquisa e de Extensão-SEMPESQ.}
    \paragraph{Apoio à divulgação dos trabalhos que foram e/ou estão sendo desenvolvidos em parceria
        entre os alunos e os professores.}
\end{itemize}

\paragraph{
    No âmbito do curso de \variable{@CURSO.@NOME}, são incentivadas as atividades de pesquisa,
    por meio de diversos mecanismos institucionais, a exemplo de atribuição pela IES de carga
    horária para orientação das atividades de iniciação científica. Ademais, haverá promoção e
    incentivo à apresentação de produção técnica e científica em eventos a exemplo da Mostra
    de Práticas Investigativas e Extensionistas.
}

\paragraph{
    Para o corpo discente, a Universidade Tiradentes oferece bolsas de iniciação científica,
    bem como os alunos poderão ser beneficiados com bolsas destinadas por órgãos conveniados.
    Considerando situações em que essa oferta não contemple a todos os alunos inscritos, a
    Instituição irá estimular a participação voluntária, sem prejuízo da legitimidade
    institucional do projeto de pesquisa, regida pelo Programa Voluntário de Iniciação
    Científica – PROVIC.
}"
            },
            new BookEntity
            {
                Title = "Políticas de extensão",
                Text =
@"\paragraph{
    A extensão é concebida como processo educativo, cultural e científico que se articula
    com o ensino e a investigação de forma indissociável, viabilizando a relação transformadora
    entre a Instituição e a sociedade. Nessa direção, serão implementadas ações, pautadas nas
    seguintes diretrizes:
}

\begin{itemize}
    \paragraph{Fomento ao desenvolvimento de habilidades e competências de discentes possibilitando
        condições para que esses ampliem, na prática, os aspectos teóricos e técnicos aprendidos e
        trabalhados ao longo do curso através das disciplinas e conteúdos programáticos.}
    \paragraph{Estímulo à participação dos discentes nos projetos idealizados para o curso e para
        a Instituição de modo geral, possibilitando a interdisciplinaridade e transversalidade do
        conhecimento.}
    \paragraph{Garantia da oferta de atividades de extensão de diferentes modalidades.}
    \paragraph{Estabelecimento de diretrizes de valorização da participação do aluno em atividades
        extensionistas.}
    \paragraph{Concretização de ações relativas à responsabilidade social da Universidade Tiradentes.}
\end{itemize}

\paragraph{
    Nessa direção, a extensão ocorre mediante articulação com o ensino e a pesquisa, sob a forma
    de atividades em projetos, garantindo a disponibilidade de algumas atividades de forma gratuita
    para a população de baixa renda, em especial para as comunidades circunvizinhas, reafirmando
    assim seu compromisso com uma inclusão social e com o desenvolvimento regional.
}

\paragraph{
    Pautada nestas diretrizes sustenta-se que a articulação entre a Instituição e a sociedade
    por meio da extensão é um processo que permite a socialização e a transformação dos conhecimentos
    produzidos com as atividades de ensino e a pesquisa, recuperando e (re) significando saberes
    gerados a partir das práticas sociais, contribuindo para o desenvolvimento regional. No âmbito
    do curso de \variable{@CURSO.@NOME}, são implementadas ações que propiciem a extensão, de modo a
    aproximar, cada vez mais, os estudantes da realidade regional e local.
}
"
            },
            new BookEntity
            {
                Title = "Outras características da estrutura curricular",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Estrutura curricular",
                Text =
@"

\paragraph{
	A estrutura curricular organiza-se de forma a comtemplar o eixo de formação previstos no Catalogo de Curso e
	devidamente alinhados ao PPI. Para tal, o seu PPC enfatiza as diferentes áreas do conhecimento permitindo o
	desenvolvimento do espírito científico e o aprimoramento das relações homem/natureza. Inspira-se nos pilares
	da educação contemporânea, formando profissionais capazes de: \run[BOLD;ITALIC]{aprender a conhecer},
	\run[ITALIC]{aprender a fazer}, \run[ITALIC]{aprender a ser} e \run[ITALIC]{aprender a viver juntos}, apostando
	no efeito multiplicador e transformador de suas práxis.
	}

\paragraph{
	A tabela abaixo apresenta a periodização da estrutura curricular referente ao curso \variable{@CURSO.@NOME}:
	}

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;            P E R Í O D O S
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
\code{@TABELA.@PERIODO1}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO2}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO3}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO4}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO5}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO6}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO7}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO8}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO9}
\paragraph[BEFORE=100]{\run{ }}
\code{@TABELA.@PERIODO10}
\paragraph[BEFORE=100]{\run{ }}

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;           O P T A T I V A S
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
\code{@TABELA.@OPTATIVA1}

\code{@TABELA.@OPTATIVA2}

\paragraph[BEFORE=200]{
	Nos quadros anteriores, (*) indica disciplina semipresencial, Trabalho de Conclusão de Curso, ou
	Estágio Supervisionado, todas especificadas em relação à hora/aula cheia (60min). Todas as demais
	disciplinas são especificadas em relação à hora/aula de 50min.
	}

\paragraph{
	No que se refere à compatibilização da carga horária do curso em horas aula, apresentamos o
	quadro síntese com a distribuição de horas que contempla de maneira excelente aos aspectos de
	distribuição entre teoria e prática.
	}

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;             R E S U M O
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
\code{@TABELA.@RESUMO}

\paragraph[BEFORE=200]{
	O curso foi formatado com \variable{@CURSO.@CHT60} horas distribuídas em \variable{@CURSO.@PERIODOS} semestres conforme
	demonstrado na estrutura curricular anterior, incluindo \variable{@CURSO.@CHAC} horas de atividades complementares. Considerando as atividades complementares integrantes do eixo profissionalizante,
	das \variable{@CURSO.@CHT60} horas, \variable{@CURSO.@BASICO} pertencem ao eixo básico (\variable{@CURSO.@BASICO_PERCENTUAL}%),
	\variable{@CURSO.@PROFISSIONALIZANTE} pertencem ao eixo profissionalizante (\variable{@CURSO.@PROFISSIONALIZANTE_PERCENTUAL}%)
	e \variable{@CURSO.@ESPECIFICO} pertencem ao eixo específico (\variable{@CURSO.@ESPECIFICO_PERCENTUAL}%).
}
"
            },
            new BookEntity
            {
                Title = "Eixos Interligados de Formação",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Eixos estruturantes",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Temas transversais",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Atividades complementares",
                Text =
@"
\paragraph{
	As Atividades Complementares \footnote{Anexo, o Regulamento das Atividades Complementares.} são componentes curriculares
	enriquecedores e implementadores do perfil do formando, possibilitam interação entre a teoria, a prática e a pesquisa,
	favorecendo ainda a flexibilização e formação complementar do aluno.
	}
\paragraph{
	Tais características propiciam a atualização constante do aluno, a criação do espírito crítico e que conduz a uma maior
	busca pelo saber na graduação, ampliando suas práticas profissionais possibilitando a articulando ensino/pesquisa/extensão.
	Deste modo a \variable{@IES.@ACRONIMO} entende que as atividades complementares fortalecem a formação do profissional em
	\variable{@CURSO.@NOME}, permitindo aos alunos trocas importantes, tanto no âmbito acadêmico quanto no aspecto profissional.
	}
\paragraph{
	Os discentes do curso de \variable{@CURSO.@NOME} da \variable{@IES.@ACRONIMO} são constantemente estimulados a participar
	das atividades e sua efetivação ocorrerá através de seminários, participação em eventos, monitoria, atividades acadêmicas
	a distância, iniciação a pesquisa, vivência profissional complementar, workshops, congressos, trabalhos orientados de
	campo, artigos científicos, dentre outras. Além das atividades propiciadas pela coordenação do curso e pela instituição,
	os alunos são também incentivados a participar fora do ambiente acadêmico, incluindo a prática de estudos, atividades
	independentes e transversais de interesse da formação do profissional em total consonância com a Resolução CNE/CES nº 5,
	de 07 de novembro de 2001.
	}
\paragraph{
	As Atividades Complementares possuem a característica de serem atemporais, respeitando o tempo de cada aluno, mantendo
	coerência com a proposta curricular institucional. Então, podem ser desenvolvidas durante todos os semestres, devendo
	estar contemplada até o final do curso de graduação, cujas normas foram apreciadas pela Coordenação e aprovadas pelo
	Colegiado do Curso.
	}
\paragraph{
	Ciente de que o conhecimento é construído em diferentes e variados cenários, e conforme Art. 4º do Regulamento das
	Atividades Complementares dos Cursos de Graduação da \variable{@IES.@NOME} serão consideradas Atividades Complementares
	as atividades, descritas abaixo:
	}

\begin{itemize}
	\paragraph{Monitorias (voluntária ou remunerada);}
	\paragraph{Disciplinas cursadas fora do âmbito da estrutura curricular do curso;}
	\paragraph{Estágios Extracurriculares;}
	\paragraph{Iniciação Científica;}
	\paragraph{Participação em Congressos, seminários, simpósios, jornadas, cursos, minicursos etc.;}
	\paragraph{Publicação de Trabalho científico em eventos de âmbito nacional, regional ou internacional;}
	\paragraph{Elaboração de trabalho científico (autoria ou coautoria) apresentado em eventos de âmbito regional, nacional ou internacional;}
	\paragraph{Publicação de artigo científico completo (artigo publicado ou aceite final da publicação) em periódico especializado;}
	\paragraph{Visitas técnicas fora do âmbito curricular;}
	\paragraph{Artigo em periódico;}
	\paragraph{Autoria ou coautoria de livro;}
	\paragraph{Participação na organização de eventos científicos;}
	\paragraph{Participação em programas de extensão promovidos ou não pela \variable{@IES.@ACRONIMO};}
	\paragraph{Participação em Cursos de extensão e similares patrocinados ou não pela \variable{@IES.@ACRONIMO};}
	\paragraph{Participação em jogos esportivos de representação estudantil;}
	\paragraph{Prestação de serviços e Atividades comunitárias, através de entidade beneficente ou organização não governamental, legalmente instituída, com a anuência da coordenação do curso e devidamente comprovada;}
	\paragraph{Participação em Palestra ou debate de mesas redondas e similares;}
	\paragraph{Fóruns de Desenvolvimento Regionais promovidos ou não pela \variable{@IES.@ACRONIMO};}
	\paragraph{Para reconhecimento e validação das atividades o aluno deverá comprovar por meio de certificados de valor reconhecido a sua atividade complementar junto ao grupo de responsabilidade técnica indicado pela coordenação do curso conforme quadro apresentado no regulamento.}
\end{itemize}

\paragraph{A carga horária das Atividades Complementares para o curso de \variable{@CURSO.@NOME} é de \variable{@CURSO.@CHAC} horas, obedecendo aos critérios estabelecidos no Regulamento da Instituição e o seu cumprimento é obrigatório para a integralização do currículo.}
"
            },
            new BookEntity
            {
                Title = "Atividades Práticas Supervisionadas Extraclasse – APS",
                Text =
@"
\paragraph{
	As Atividades Práticas Supervisionadas
	desenvolvidas no curso de \variable{@CURSO.@NOME} da \variable{@IES.@ACRONIMO}
	tem como fundamento a Resolução CES/CNE nº 3/2007, que  dispõe sobre procedimentos a serem adotados
	quanto ao conceito de hora-aula, e dá outras providências. Explicitado no seu Art. 2º em que
	\run[ITALIC]{“cabe às Instituições de Educação Superiores respeitadas o mínimo dos duzentos dias
	letivos de trabalho acadêmico efetivo, a definição da duração da atividade acadêmica ou do trabalho
	discente efetivo que compreenderá: I – preleções e aulas expositivas; II – atividades práticas
	supervisionadas, tais como laboratórios, atividades em biblioteca, iniciação científica, trabalhos
	individuais e em grupo, práticas de ensino e outras atividades no caso das licenciaturas”.}
	}
\paragraph{
	Em consonância com a legislação vigente a \variable{@IES.@ACRONIMO}, contemplou no PPC de
	\variable{@CURSO.@NOME} a complementaridade das horas de integralização do curso por meio
	da institucionalização e normatização das Atividades Práticas Supervisionadas Extraclasse – APSEC,
	através da Portaria Interna nº 013 de 11 de fevereiro de 2009 \footnote{Anexo, Portaria Interna nº 13 de 11 de fevereiro de 2009.}, emitida pela Reitoria,
	considerando as atividades práticas supervisionadas como trabalho acadêmico ou discente efetivo,
	que pode e deve ser contabilizado na carga horária total do curso, aliás, o que a própria resolução
	CES/CNE nº 3/2007, deixa claro.
	}
\paragraph{
	Nesse contexto, o conceito de aula, extrapola o espaço físico da sala de aula e consubstancia-se
	no conceito de atividades acadêmicas efetivas, para além da sala de aula, sendo promovidas e
	desenvolvidas atividades acadêmicas sob a orientação e supervisão docente, em horários e espaços
	diferentes dos encontros presenciais, bem como discriminadas nos planos de ensino e planos
	integrados de trabalho de todas as disciplinas do curso. Dentre as atividades inseridas em
	todos os componentes curriculares, encontram-se: visitas técnicas orientadas, atividades na
	biblioteca, estudos de caso, seminários, oficinas, aulas práticas de campo ou laboratório,
	trabalhos individuais ou em grupo, pesquisas, dentre outros.
	}
\paragraph{
	Tais atividades propiciam ao processo pedagógico a articulação entre o ensino, a pesquisa e a
	extensão, além da teoria com a prática, componentes indissociáveis do fazer pedagógico. As
	atividades desenvolvidas aliam o ensino, a pesquisa e a extensão preconizados no Projeto
	Pedagógico da Instituição, possibilitando aos estudantes a participação, por meio das práticas
	de pesquisa e de extensão promovidas nos diversos componentes curriculares, o desenvolvimento da
	autonomia intelectual e acadêmica dos estudantes, permitindo a constante interação entre o
	conteúdo trabalhado nas diversas disciplinas e a realidade na qual os estudantes desenvolverão
	suas atividades profissionais.
	}
\paragraph{
	Vale ressaltar que a \variable{@IES.@ACRONIMO} promove no período que antecede cada período
	letivo, Jornada de Mobilização Pedagógica, oficinas e minicursos, objetivando a reflexão do
	trabalho teórico-metodológico e aprimoramento da práxis docente, para que de modo a inseriram
	nos seus planos de ensino e planos Integrados de trabalho, atividades que propiciem o desenvolvimento
	de competências e habilidades necessárias a formação do perfil profissional do egresso, proposto no
	presente projeto.
	}
"
            },
            new BookEntity
            {
                Title = "Integração Ensino/Pesquisa/Extensão/Núcleo de Pesquisa e Geradores de Extensão",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Programas/Projetos/Atividades de Iniciação Científica",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Interação teoria e prática – Princípios e orientações quanto às práticas pedagógicas",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Práticas profissionais e estágio",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Trabalho de conclusão de curso",
                Text =
@"
\paragraph{
	O Trabalho de Conclusão de Curso (TCC) \footnote{Anexo: Regulamento de Trabalho de Conclusão de Curso.}
	é um componente curricular obrigatório e necessário para a integralização curricular.
	Configura-se como um momento de reflexão, crítica e aprofundamento da pesquisa e da descoberta de novos saberes na área de
	interesse do estudante, contemplando uma diversidade de aspectos fundamentais para a formação acadêmica e profissional.
}
\paragraph{
	O TCC possibilita a aplicação dos conceitos e teorias adquiridas ao longo do curso por meio da elaboração e execução do
	projeto de pesquisa, no qual o estudante experiência, com autonomia, o aprofundamento de um tema específico, além de
	estimular o espírito crítico e reflexivo.
}
\paragraph{
	Desenvolvido mediante orientação de um professor que faz parte do quadro docente da instituição, sua realização ocorre
	mediante matrícula na disciplina de Trabalho de Conclusão de Curso e acompanhamento de professor orientador.
}
\paragraph{
	A carga horária destinada a disciplina é de 40 horas, cujo horário estabelecido para orientação abrangerá 02 (duas)
	horas semanais, sendo que o mesmo não poderá conflitar com o horário do estudante, devendo ser definido de comum acordo
	entre a Coordenação do Curso, o estudante e o professor orientador.
}
\paragraph{
	O Colegiado do Curso de \variable{@CURSO.@NOME} estabelece calendário para entrega e apresentação do trabalho, que
	ocorrerá perante banca examinadora constituída por 03 (professores da área). A cada semestre, o Colegiado do Curso
	definirá o quantitativo de alunos por professor-orientador e o nome dos professores que irão desenvolver as atividades
	de orientação, em consonância com as normas internas da Instituição.
}
\paragraph{
	O aluno que não entregar o TCC, ou que não se apresentar para a sua defesa oral, sem motivo justificado, está
	automaticamente reprovado na disciplina, podendo apresentar novo projeto somente no semestre letivo seguinte,
	mediante matrícula na disciplina de acordo com o calendário institucional.
}
\paragraph{
	Ao concluir o TCC, o aluno terá a possibilidade de apresentá-lo na Semana de Pesquisa realizada pela Diretoria de
	Pesquisa e Extensão e desenvolver artigo científico sintetizando seu trabalho para publicação nos Cadernos de
	Graduação da \variable{@IES.@ACRONIMO}.
}
\paragraph{
	As Normas que regem o TCC do Curso de \variable{@CURSO.@NOME} possuem regulamento próprio e tem como objetivo
	inteirar alunos e professores orientadores sobre as suas disposições, orientando-os quanto às normas de funcionamento,
	horários, orientações quanto à apresentação dos trabalhos, avaliação, etc, a fim de terem um melhor aproveitamento
	desta experiência além de outros critérios.
}
"
            },
            new BookEntity
            {
                Title = "Sistemas de avaliação",
                Text = @""
            },
            new BookEntity
            {
                Title = "Núcleo docente estruturante (NDE)",
                Text =
@"\paragraph{
    Em conformidade com as orientações da Comissão Nacional de Avaliação da Educação Superior (CONAES) em sua
    Resolução nº 1 de 17/06/2010, o curso de Engenharia Mecânica da UNIT conta com o Núcleo Docente Estruturante – NDE
    que é um órgão consultivo da coordenação do curso, responsável pelo processo de concepção, implementação,
    consolidação e contínua atualização do Projeto Pedagógico do Curso. O Núcleo Docente Estruturante é constituído
    por 05 (cinco) docentes do curso, dos quais 80% possuem titulação obtida em programas de pós-graduação “stricto sensu”
    e 100% possuem tempo integral e ou parcial na IES. A nomeação é efetuada pela Reitoria para executar suas atribuições e
    atender a seus fins, tendo o coordenador do curso como presidente.
    }
\paragraph{São atribuições do Núcleo Docente Estruturante:}

\begin{itemize}
    \paragraph{Zelar pelo cumprimento das Diretrizes Curriculares Nacionais para os Cursos de graduação;}
    \paragraph{Participar da revisão e atualização periódica do projeto pedagógico do curso, submetendo-o à análise e aprovação do Colegiado de Curso;}
    \paragraph{Propor permanente revisão ao que se refere a concepção do curso, definição de objetivos e perfil de egressos, metodologia, componentes curriculares e formas de avaliação em consonância com as Diretrizes Curriculares Nacionais;}
    \paragraph{Contribuir para a consolidação do perfil profissional do egresso do curso;}
    \paragraph{Zelar pela integração curricular interdisciplinar entre as atividades de ensino constantes no currículo;}
    \paragraph{Indicar formas de incentivo ao desenvolvimento de linhas de pesquisa e extensão, oriundas das necessidades da graduação, de exigências do mercado de trabalho e afinadas com as Diretrizes Curriculares;}
    \paragraph{Analisar os planos de ensino dos componentes curriculares dos cursos, sugerindo melhorias e atualização;}
    \paragraph{Propor alternativas de melhoria a partir dos resultados das avaliações internas e externas dos cursos em consonância com o Colegiado;}
    \paragraph{Assessorar a coordenação do curso na condução dos trabalhos de alteração e reestruturação curricular, submetendo a aprovação no Colegiado de Curso, sempre que necessário;}
    \paragraph{Propor programas ou outras formas de capacitação docente, visando a sua formação continuada;}
    \paragraph{Acompanhar as atividades do corpo docente no que se refere às Práticas de Pesquisa e Práticas de Extensão;}
    \paragraph{Acompanhar as atividades desenvolvidas pelo corpo docente, sobretudo no que diz respeito à integralização dos Planos de Ensino e Aprendizagem e Plano Integrado de Trabalho;}
    \paragraph{Elaborar semestralmente cronograma de reuniões;}
    \paragraph{Encaminhar relatórios semestrais a coordenação do curso sobre suas atividades, recomendações e contribuições;}
    \paragraph{Propor alternativas de integração horizontal e vertical do curso, respeitando os eixos estabelecidos nos respectivos projetos pedagógicos e nas Diretrizes Curriculares Nacionais.}
\end{itemize}

\paragraph{Os docentes que compõem o NDE do curso de \variable{@CURSO.@NOME} são contratados em regime de tempo parcial ou integral, abaixo a composição:}

\code{@TABELA.@NDE}
"
            },
            new BookEntity
            {
                Title = "Colegiado de curso",
                Text =
@"\paragraph{O Colegiado do curso constitui-se instância de caráter consultivo e deliberativo,
    cuja participação dos professores e estudantes ocorre a partir dos representantes titulares
    e suplentes, os quais possuem mandatos e atribuições regulamentados pelo Regimento Interno da
    \variable{@IES.@ACRONIMO}.
    }
\paragraph{Composto pelo coordenador do curso, que o presidirá e por representantes docentes que desempenham atividades no curso, indicados pelo coordenador e referendada pela Reitoria, conta ainda com representantes do corpo discente, regularmente matriculados no curso. Todos os membros do Colegiado possuem um mandato de 1 (um) ano, podendo ser reconduzido, à exceção do seu presidente, o coordenador do curso, membro nato.}
\paragraph{Nessa direção, o comprometimento do corpo docente e discente ocorre através da participação dos professores e alunos no que se refere principalmente à determinação da conduta pedagógica e acadêmica mais adequada para alcançar os objetivos acadêmicos.}
\paragraph{São atribuições do Colegiado do curso de \variable{@CURSO.@NOME}:}

\begin{itemize}
    \paragraph{Assessorar na coordenação e supervisão do funcionamento do curso;}
    \paragraph{Avaliar e aprovar as proposições de atualização do Projeto Pedagógico de Curso – PPC, encaminhadas pelo NDE;}
    \paragraph{Apreciar e deliberar sobre as sugestões apresentadas pelo Núcleo Docente Estruturante – NDE, pelos demais docentes e discentes quanto aos assuntos de interesse do Curso;}
    \paragraph{Propor e validar alterações na estrutura curricular do curso observando os indicadores de qualidade determinados pelo MEC e pela instituição, quando for o caso;}
    \paragraph{Analisar e aprovar os Planos de Ensino e Aprendizagem, propondo alterações, quando necessário, encaminhadas pelo NDE;}
    \paragraph{Analisar e aprovar o desenvolvimento e aperfeiçoamento de metodologias próprias para o ensino das disciplinas do curso;}
    \paragraph{Garantir que sejam estabelecidas e mantidas as relações didático-pedagógicas das disciplinas do curso, respeitando os objetivos e o perfil do profissional, definido no projeto pedagógico do curso;}
    \paragraph{Definir e propor as estratégias e ações necessárias e/ou indispensáveis para a melhoria de qualidade da pesquisa, da extensão e do ensino ministrado no curso, a serem encaminhadas à Diretoria de Graduação;}
    \paragraph{Examinar e responder, quando possível, as questões suscitadas pelos docentes e discentes, ou encaminhar ao setor competente, cuja solução transcenda as suas atribuições;}
    \paragraph{Apresentar a coordenação propostas de atividades extracurriculares necessárias para o bom funcionamento do curso;}
    \paragraph{Avaliar e emitir parecer sobre o Plano Individual de Trabalho – PIT, quando solicitado;}
    \paragraph{Aprovar os projetos de pesquisa, de pós-graduação e de extensão relacionados ao Curso, submetendo-os à apreciação e deliberação;}
    \paragraph{Colaborar com os diversos órgãos acadêmicos nos assuntos de interesse do Curso;}
    \paragraph{Analisar e decidir os pleitos quebra de prerrequisitos e adaptação de disciplinas, mediante requerimento dos interessados;}
    \paragraph{Deliberar sobre aproveitamento de estudos quando solicitado pelos alunos;}
    \paragraph{Manter registrado todas as reuniões e deliberações, através de atas que devem ser devidamente arquivadas.}
\end{itemize}

\paragraph{Atualmente o corpo docente e discente do curso é representado pelos seguintes membros:}

\code{@TABELA.@COLEGIADO}"
            },
            new BookEntity
            {
                Title = "Corpo docente",
                Text =
@"\paragraph{
    O corpo docente do curso de \variable{@CURSO.@NOME} é constituído por profissionais dotados de experiência e
    conhecimento na área que leciona e a sua seleção leva em consideração a formação acadêmica e a titulação, bem
    como o aproveitamento das experiências profissionais no exercício de cargos ou funções relativas ao universo
    do campo de trabalho que o curso está inserido, valorizando o saber prático, teórico e especializado que
    contribui de forma significativa para a formação do perfil desejado do egresso do curso.
    }
\paragraph{
    A \variable{@IES.@ACRONIMO} dispõe de um Plano de Carreira do Magistério Superior, cujo objetivo é estimular
    o alcance das metas e missão de cada curso, bem como de programa de qualificação docente, motivando-os para
    o exercício do magistério superior, aperfeiçoando o exercício profissional.
    }
\paragraph{
    O Plano de Carreira da Instituição contempla ascensão profissional horizontal (promoção sem mudar de função,
    entretanto com aumento nos rendimentos) e vertical (crescimento profissional em cargo e rendimento), bem como
    motivar o corpo docente e ser justo com os profissionais nos aspectos de qualificação profissional e dedicação
    à instituição – tempo de atividade como professor universitário na IES.
    }
\paragraph{
    No sentido de motivar o professor à formação exigida para o exercício da docência, os dirigentes da
    \variable{@IES.@NOME}, tem se concentrado em aprofundar o conhecimento, seja ele prático (decorrente do
    exercício profissional) ou teórico/epistemológico (decorrente do exercício acadêmico), através de Programas
    de Formação docente por meio de jornadas pedagógicas, oficinas e minicursos desenvolvidos ao longo dos
    períodos, que contribuem na formação exigida para a docência no ensino superior.
    }
\paragraph{
    Estes programas voltados à formação pedagógica do professor universitário despertam naqueles que o realizam,
    o comprometimento com as questões educacionais, não se limitando aos aspectos práticos (didáticos ou
    metodológicos) do fazer docente, mas englobando dimensões relativas às questões éticas, afetivas e
    político-sociais envolvidas na docência, fundamentando-se numa concepção de práxis educativa e do ensino como
    uma atividade complexa, que demanda dos professores uma formação que supere o mero desenvolvimento de
    habilidades técnicas ou, simplesmente, conhecimento aprofundado de um conteúdo específico de uma área do saber.
    }
\paragraph{
    Dentro das políticas da instituição, são selecionados profissionais com formação adequada às atividades que
    irão desenvolver, objetivando o fiel atendimento e cumprimento de todas as ações necessárias ao bom andamento
    dos trabalhos acadêmicos. Assim, vislumbra-se nesse profissional o atendimento, conforme mencionado, de todas
    as necessidades em função também da experiência e atuação já adquirida no mercado de trabalho.
    }

;; \code{Corpo Docente}
\code{@TABELA.@DOCENTES}

\paragraph[BEFORE=200]{
    \run{O corpo docente do curso de \variable{@CURSO.@NOME} é composto por \variable{@CURSO.@NUMDOCENTES} docentes dos quais
    \variable{@CURSO.@PERCSTRICTU}% possuem titulação \run[ITALIC]{strictu sensu}, destes, \variable{@CURSO.@PERCDOUTORES}% são
    doutores. Dentre outras atividades são os responsáveis por analisar e atualizar os conteúdos dos componentes
    curriculares, além da bibliografia proposta para os respectivos planos de ensino relacionando-os a conteúdos
    de pesquisa de ponta, visando atingir os objetivos das disciplinas e ao perfil proposto de formação do egresso.}
    }"
            },
            new BookEntity
            {
                Title = "Administração acadêmica do curso",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Modos de integração entre a graduação e a pós-graduação",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Núcleo de atendimento pedagógico e psicossocial",
                Text =
@"\paragraph{
    O Núcleo de Atendimento Pedagógico e Psicossocial - NAPPS tem como finalidade atender ao corpo discente,
    integrando-os à vida acadêmica, a \variable{@IES.@ACRONIMO} oferece um importante serviço que objetiva
    acolhê-lo e auxiliá-lo a resolver, refletir e enfrentar seus conflitos emocionais, bem como suas
    dificuldades a nível pedagógico. O Núcleo de Atendimento Pedagógico e Psicossocial - NAPPS é constituído
    por uma equipe excelentemente preparada e multidisciplinar que busca contribuir para o desenvolvimento e
    adaptação do aluno à vida acadêmica, a partir de uma visão integradora dos aspectos emocionais e
    pedagógicos. Nessa perspectiva, desenvolve ações, dentre as quais:
    }

\begin{itemize}
    \paragraph{
        atendimento individualizado, destinado a estudantes com dificuldade de relacionamento interpessoal
        e de aprendizagem, envolvendo a escuta do docente quanto a situação problemática, visando a
        identificação das áreas (profissional, pedagógica, afetivo-emocional e/ou social),
        }
    \paragraph{
        acompanhamento extraclasse para estudantes que apresentam dificuldades em algum componente curricular,
        mediante reforço personalizado desenvolvido por professores das diferentes áreas;
        }
    \paragraph{
        encaminhamento para profissionais e serviços especializados, caso seja necessário, a exemplo da
        Clínica de Psicologia, vinculada ao curso de Formação de Psicólogo da instituição, onde os discentes
        podem receber atendimento especializado gratuito.
        }
\end{itemize}

\paragraph{
    Vale salientar que tal iniciativa se inscreve nos debates da \variable{@IES.@ACRONIMO} sobre o direito de
    todos à educação e na igualdade de oportunidades de acesso e permanência nessa modalidade de ensino. Outro
    aspecto que merece destaque é que a Universidade Tiradentes estruturou todos os seus campi no que se refere
    à mobilidade dos seus discentes disponibilizando rampas de acesso, elevadores, piso tátil, banheiros
    adaptados, vagas específicas de estacionamento, entre outros o que demonstra o olhar atento às questões
    de igualdade de oportunidades de acesso e permanência na Educação Superior bem como contemple a Educação
    em Direitos Humanos como parte do processo educativo, a IES adota como referência a Norma Técnica 9050/2015,
    da Associação Brasileira de Normas Técnicas.
    }
\paragraph{
    Em relação aos alunos com deficiência visual, a IES está comprometida, caso seja solicitada, desde o
    acesso até a conclusão do curso, a proporcionar sala de apoio contendo: máquina de datilografia Braille,
    impressora Braille acoplada a computador, sistema de síntese de voz; gravador e fotocopiadora que amplie
    textos; acervo bibliográfico em fitas de áudio; software de ampliação de tela; equipamento para ampliação
    de textos para atendimento a aluno com visão subnormal; lupas, réguas de leitura; scanner acoplado a
    computador; acervo bibliográfico dos conteúdos básicos em Braille. Quanto aos alunos com deficiência
    auditiva, a IES está igualmente comprometida desde o acesso até a conclusão do curso, e disponibiliza
    intérpretes de língua brasileira de sinais.
    }
\paragraph{
    Ressalta-se ainda que o NAPPS é o setor responsável por acompanhar e atender ao que estabelece a
    Lei nº 12.764, de 27 de dezembro de 2012 que institui a Política Nacional de Proteção dos Direitos da
    Pessoa com Transtorno do Espectro Autista fazendo o acompanhamento especializado dos estudantes com
    tais necessidades.
    }"
            },
            new BookEntity
            {
                Title = "Formação complementar e nivelamento discente",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} prevê em seu Plano de Desenvolvimento Institucional (PDI) ações e
    políticas para formação complementar e de nivelamento discente. O referido programa encontra-se na
    pauta das medidas tomadas pela \variable{@IES.@ACRONIMO} que buscam soluções educacionais que minimizem
    as variáveis que interferem nas condições de permanência dos alunos no ensino superior dados as
    fragilidades da educação básica, que interferem no desenvolvimento acadêmico. Neste sentido, sistematiza
    e fixa ações que já fazem parte do processo histórico da Universidade Tiradentes e que estão presentes
    na sua missão institucional, com o objetivo de contribuir tanto em termos de acesso, como de
    permanência dos alunos.
    }
\paragraph{
    O Programa de Formação Complementar e Nivelamento Discente da \variable{@IES.@NOME} se justifica, em
    razão das próprias políticas nacionais, para o ensino superior, que estabelecem condições institucionais
    mínimas para o atendimento processual e permanente aos discentes. Dessa forma, as políticas de apoio ao
    estudante na \variable{@IES.@ACRONIMO} são viabilizadas, fundamentalmente, pela Pró-reitora Acadêmica
    por intermédio do da sua equipe pedagógica, que implementa, junto às coordenações, as políticas de
    atendimento e relacionamento com os estudantes. Estas atividades são sistematizadas por meio da promoção,
    execução e acompanhamento de programas e projetos que contribuam para a formação dos alunos,
    proporcionando-lhes condições favoráveis à integração na vida universitária.
    }
\paragraph{
    Incorpora também a adoção de mecanismos de recepção e acompanhamento dos discentes, criando condições
    para o acesso e permanência no ensino superior. Para tal são objetivos do Programa:
    }

\paragraph[FIRSTLINE=0]{
    \run[BOLD]{Objetivo Geral:}
    }

\begin[NUMBER=5]{itemize}
    \paragraph{
        Promover a integração e a generalização de conhecimentos e saberes por meio de disciplinas, programas,
        projetos e outras atividades educacionais específicas relacionadas aos cursos ofertados pela instituição.
        }
\end{itemize}

\paragraph[FIRSTLINE=0]{
    \run[BOLD]{Específicos:}
    }

\begin{enumerate}
    \paragraph{
        I – Oferecer, disciplinas especiais e conteúdos básicos e complementares presenciais ou online
        através do Ambiente Virtual de Aprendizagem - AVA;
        }
    \paragraph{
        II – Promover a ampliação de conhecimentos por meio da constante atualização do processo formativo
        por meio de projetos, programas e outras atividades de formação complementar com vistas aos mecanismos
        de nivelamento;
        }
    \paragraph{
        III – Possibilitar o exercício da reflexão em grupos heterogêneos, quanto à formação básica e complementar.
        }
    \paragraph{
        IV - Identificar alunos com carências educacionais e realizar ações de superação das dificuldades;
        }
    \paragraph{
        V - Realizar ações de acompanhamento aos alunos que necessitam de atendimento especial;
        }
    \paragraph{
        VI - Contribuir para o desenvolvimento acadêmico dos alunos, visando à utilização de forma integrada
        dos recursos intelectuais, psíquicos e relacionais.
        }
\end{enumerate}

\paragraph{
    A \variable{@IES.@ACRONIMO} desenvolve mecanismos de nivelamentos e formação continuada com vistas a
    favorecer o desempenho de forma integral e continuada dos acadêmicos. Esse mecanismo é compreendido
    pelos seguintes serviços:
    }

\begin{itemize}
    \paragraph{
        Oferta de monitoria para disciplinas com maior percentual de evasão identificadas a partir de
        diagnóstico gerado pelo sistema Magister;
        }
    \paragraph{
        Oferta do Programa de Aperfeiçoamento em Língua Portuguesa, visando aprimorar o uso da língua
        portuguesa para desenvolvimento de competências e habilidades de interpretação e escrita de textos;
        }
    \paragraph{
        Oferta do programa de Aperfeiçoamento em Matemática Básica, utilizando as ferramentas do KAN ACADEMY
        }
    \paragraph{
        Oferta de disciplinas de formação complementar;
        }
    \paragraph{
        Oferta de cursos online, em Ambiente Virtual de Aprendizagem, em consonância com as demandas de nivelamento de estudos;
        }
    \paragraph{
        Oferta de minicursos e oficinas específicas por área de conhecimento nos eventos promovidos, tanto
        institucionalmente, quanto nas semanas de curso, de caráter acadêmico – científico – cultural;
        }
    \paragraph{
        Semana de Acolhimento Discente.
        }
\end{itemize}

\paragraph{
    A oferta de disciplinas de formação complementar, bem como da oferta de monitoria, será formalizada
    a partir das demandas específicas de cada curso de graduação da \variable{@IES.@ACRONIMO}.
    }"
            },
            new BookEntity
            {
                Title = "Programa de integração de calouros",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} empreende sua política de apoio e acompanhamento ao discente, oferecendo
    condições favoráveis à continuidade dos estudos independentemente de sua condição física ou socioeconômica.
    Para tal, oferta a todos os alunos ingressantes nos cursos de graduação da instituição o Programa de
    Integração de Calouros em auxílio ao discente em sua trajetória universitária, tal proposta tem como
    finalidade o enriquecimento do perfil do aluno nas mais variadas áreas do conhecimento, essências para
    a formação geral do indivíduo e a integração e generalização de conhecimentos e saberes por meio de
    disciplinas relacionadas aos cursos ofertados pela instituição.
    }
\paragraph{
    O Programa de Integração de Calouros tem como objetivo principal oferecer um acolhimento especial
    aos ingressantes, viabilizando sua rápida e efetiva integração ao meio acadêmico e encontra-se estruturado
    em dois módulos:
    }

\begin{itemize}
    \paragraph{
        \run[BOLD]{Módulo I} – Programa de Apoio Pedagógico Integrado – PAPI, ofertado através de cinco
        componentes básicos de estudo: Matemática, Língua Portuguesa, Biologia, Química e Fundamentos de
        Programação e Lógica Matemática. Neste módulo os discentes ingressantes têm acesso a um conjunto
        de conteúdos fundamentais para melhor aproveitamento dos seus estudos no âmbito da universidade
        e \comment{Completar!}{....}
        }
    \paragraph{
        \run[BOLD]{Módulo II} – Por dentro da \variable{@IES.@ACRONIMO}, que se caracteriza na socialização
        de informações imprescindíveis sobre o seu Curso e a Instituição. Neste módulo os alunos participaram
        de eventos e palestras onde podem conhecer o histórico, a infraestrutura, os processos acadêmicos,
        programas e projetos que a \variable{@IES.@ACRONIMO} desenvolve.
        }
\end{itemize}"
            },
            new BookEntity
            {
                Title = "Monitoria",
                Text =
@"\paragraph{
    A política de Monitoria da \variable{@IES.@ACRONIMO} tem como objetivos oportunizar aos discentes o
    desenvolvimento de atividades e experiências acadêmicas, visando aprimorar e ampliar conhecimentos,
    fundamentais para a formação profissional; aperfeiçoar e complementar, as atividades ligadas ao
    processo de ensino, pesquisa e extensão e estimular a vocação didático-pedagógica e científica
    inerente à atuação dos discentes.
    }
\paragraph{
    O Curso de \variable{@CURSO.@NOME} desenvolve semestralmente a política de Monitoria possibilitando aos
    alunos do curso, obter um aprimoramento dos conhecimentos adquiridos além de vivenciar com os professores
    orientadores, as atividades desenvolvidas em salas de aulas através do atendimento aos alunos tirando
    dúvidas referentes a disciplinas e trabalhos de pesquisa, entre outras atividades pertinentes ao programa
    de monitoria.
    }
\paragraph{
    O processo seletivo dá-se após a divulgação do Edital, expedido pela Pró-Reitoria de Graduação Presencial,
    onde os alunos submetem-se a provas escritas das disciplinas que foram divulgadas para terem a oportunidade
    de se tornarem monitores. A monitoria pode ser remunerada ou voluntária, na qual fica estabelecida uma
    carga horária semanal a ser cumprida pelo discente (monitor). Os professores orientadores, juntamente com
    a Coordenação elaboram todo o processo seletivo e são aprovados os alunos que obtiverem maior média.
    }"
            },
            new BookEntity
            {
                Title = "Internacionalização",
                Text =
@"\paragraph{
    O departamento de Internacionalização está vinculado à Reitoria da \variable{@IES.@NOME} e ao
    Grupo Tiradentes, e tem por missão ampliar as possibilidades de alunos, professores e corpo
    administrativo se mobilizarem internacionalmente, através da realização de intercâmbios acadêmicos
    e científicos, proporcionando informação e oportunidades internacionais de estudo.
    }
\paragraph{
    O setor de Internacionalização da \variable{@IES.@NOME} oportuniza aos discentes, através de
    diversos convênios e programas, como o Programa de Intercâmbio Fellow Mundus, o Programa de Bolsas
    Ibero-americanas para Estudantes de Graduação – Santander Universidades, e outras iniciativas, o
    ingresso em instituições do exterior, ampliando assim o seu desenvolvimento internacional e sua
    percepção sobre os diferentes matizes que compõem o mundo globalizado.
    }
\paragraph{
    Vale salientar que a \variable{@IES.@ACRONIMO}, no ano de 2017, tornou-se a primeira instituição a
    atuar fora do Brasil com um centro de Educação Superior, o Tiradentes Institute no campus da
    Universidade de Massachusetts – UMass Boston, que tem a missão de compartilhar conhecimento,
    inovação, ideias, cultura e línguas que ambas as instituições possuem. Vale salientar que A UMass
    Boston é referência em pesquisa e inovação no mundo.
    }"
            },
            new BookEntity
            {
                Title = "Unit Carreiras",
                Text =
@"\paragraph{
    Trata-se de um espaço com foco na capacitação profissional, no gerenciamento e divulgação de
    oportunidades profissionais e de estágios, na orientação individual ao plano de carreira e na
    interação social, por meio das redes sociais.
    }
\paragraph{
    O Serviço é destinado aos alunos e egressos da IES, de forma gratuita, que desejam colocação ou
    recolocação no mercado de trabalho. Sempre atuando de forma estratégica, a \variable{@IES.@ACRONIMO} Carreiras
    disponibiliza vagas de empregos e estágios, por meio de parcerias, com renomadas empresas no Estado
    e no país, além de oferecer diversos serviços, visando à capacitação profissional.
    }"
            },
            new BookEntity
            {
                Title = "Programa de Bolsas",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} possui programas de apoio aos seus discentes, nas diversas modalidades de ensino.
    Dentre as possibilidades, o Programa Universidade para Todos – PROUNI, do Governo Federal, além de outros de
    natureza própria, tais como bolsas de extensão para participação em atividades.
    }
\paragraph{
    Também, destacam-se:
    }

\begin{itemize}
    \paragraph{
        Programa de Bolsa de Iniciação Científica, permite introduzir os estudantes de graduação com vocação
        no âmbito da pesquisa científica;
        }
    \paragraph{
        Programa Institucional de Bolsas de Iniciação Científica e Extensão, que visa iniciar o estudante
        em atividades de iniciação científica e extensão desenvolvida pela IES;
        }
    \paragraph{
        Programa de Apoio a Eventos e Capacitação, que subsidia a participação de discentes e docentes em
        atividades de aperfeiçoamento contínuo;
        }
    \paragraph{
        Programa de Apoio Institucional à Pós-Graduação Stricto Sensu, que concede bolsas a discentes de
        mestrado e doutorado, contribuindo para a manutenção de padrões de excelência e eficiência dos
        Programas de Pós-graduação;
        }
\end{itemize}

\paragraph{
    Todos os programas e ações implementadas na instituição podem receber recursos oriundos da
    \variable{@IES.@NOME} e/ou de agências de fomento e/ou parceiros institucionais. A \variable{@IES.@NOME}
    também disponibiliza aos seus discentes, formas de financiamento da educação por meio do FIES,
    Financiamento Estudantil Facilitado – FIEF e o Pra-Valer, além de programas de descontos oriundos de
    convênios com empresas.
    }"
            },
            new BookEntity
            {
                Title = "Ouvidoria",
                Text =
@"\paragraph{
    A Ouvidoria da \variable{@IES.@NOME}, que se encontra implantada desde 2010, é órgão independente e tem a
    responsabilidade de tratar as manifestações dos cidadãos sejam eles alunos, fornecedores, colaboradores e
    sociedade em geral, registradas sob a forma de reclamações, denúncias, sugestões e/ou elogios. Trata-se
    de um canal de comunicação interna e externa.
    }
\paragraph{
    Tem como objetivo oferecer ao cidadão a possibilidade irrestrita da interatividade, de forma rápida e
    eficiente. É uma atividade institucional de representação autônoma, imparcial e independente, de caráter
    mediador, pedagógico e estratégico, que permite identificar tendências para orientação e recomendação
    preventiva ou reativa, fomentando assim a promoção da melhoria contínua dos processos institucionais.
    }
\paragraph{
    Os atendimentos efetuam-se presencialmente, ou via telefone e site. A Ouvidoria traduz, por meio da
    estratificação dos dados registrados, as principais manifestações e demandas em relatórios demonstrados
    às instâncias competentes, o que propicia análise e considerações para as providências necessárias, para
    a melhoria contínua das ações institucionais.
    }"
            },
            new BookEntity
            {
                Title = "Acompanhamento dos egressos",
                Text =
@"\paragraph{
    A \variable{@IES.@ACRONIMO} instituiu como política o Programa de Acompanhamento do Egresso com a
    finalidade de acompanhar os egressos e estabelecer um canal de comunicação permanente com os alunos
    que concluíram sua graduação na Instituição, mantendo-os informados acerca dos cursos de pós-graduação
    e extensão, valorizando a integração com a vida acadêmica, científica, política e cultural da IES.
    }
\paragraph{
    O programa também visa orientar, informar e atualizar os egressos sobre as novas tendências do mercado
    de trabalho, promover atividades e cursos de extensão, identificar situações relevantes dos egressos
    para o fortalecimento da imagem institucional e valorização da comunidade acadêmica.
    }
\paragraph{
    Destaca-se ainda o \variable{@IES.@ACRONIMO} Carreiras, espaço dedicado aos alunos da graduação, pós-graduação e egressos
    com foco na capacitação profissional, no gerenciamento e divulgação de oportunidades profissionais e
    de estágios, na orientação individual ao plano de carreira e na interação social por meio das redes
    sociais. O serviço oferecido pelo \variable{@IES.@ACRONIMO} Carreiras é destinado aos alunos de forma gratuita, que desejam
    colocação ou recolocação no mercado de trabalho, bem como empresas parceiras que buscam profissionais
    para seus quadros. \footnote{Anexo: Regulamento do Programa de Acompanhamento do Egresso.}
    }"
            },
            new BookEntity
            {
                Title = "As Tecnologias de Informação e Comunicação – TICs no processo ensino aprendizagem",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Ambiente Virtual de Aprendizagem (AVA)",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Adequação e atualização",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Dimensionamento da carga horária das disciplinas",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Adequação e atualização das ementas e Planos de Ensino",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Adequação, atualização e relevância da bibliografia",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Bibliografia básica",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Bibliografia complementar",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Periódicos especializados",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Planos de Ensino e Aprendizagem",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Programas de disciplinas e seus componentes pedagógicos",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Salas de aula",
                Text =
@"\default{paragraph}

\paragraph{
    O Curso disponibiliza para as aulas didáticas, salas com área de 63 m². O espaço físico é adequado ao
    tamanho das turmas possibilitando mobilidade, flexibilidade e adequação no seu arranjo organizacional
    o que facilita o desenvolvimento de atividades em grupo e a aplicação de metodologias ativas por parte
    dos professores, o que diversifica os cenários de aprendizagem.
    }
\paragraph{
    Na incorporação de avanços tecnológicos os professores buscam situações e alternativas didático-pedagógicas,
    tais como utilização de recursos audiovisuais e de multimídia em sala de aula, utilização de equipamentos
    de informática com acesso à Internet de alta velocidade, simulações por meio de softwares específicos às
    áreas de formação. Também é relevante as possibilidades oferecidas por inovações tecnológicas, advindas
    dos Serviços do Google Apps For Education. As salas são bem iluminadas, limpas, com ventiladores de parede,
    contam com Datashow e acesso à internet (WiFi) e possibilidade de colocação de equipamento de som, quando
    necessário.
    }
"
            },
            new BookEntity
            {
                Title = "Instalações administrativas",
                Text =
@"
\paragraph{
    O curso de \variable{@CURSO.@NOME} utiliza as seguintes instalações para as atividades administrativas,
    no Campus Farolândia, a saber:
    }

;; --------------------
;; \include{..\Comum\Instalações do Curso\Administrativas.tbl}
;; --------------------
\includetable{Administrativas.tbl}

\paragraph[BEFORE=240]{
    Esses espaços disponibilizam as condições necessárias ao desenvolvimento das funções administrativas
    do curso, bem como ao atendimento aos alunos e professores. As dependências são arejadas e apresentam
    boa iluminação natural e artificial com adequado sistema de ar refrigerado.
    }
"
            },
            new BookEntity
            {
                Title = "Instalações para docentes – Salas de Professores, Salas de Reuniões e Gabinetes de Trabalho",
                Text =
@"\paragraph{
    O curso de \variable{@CURSO.@NOME} utiliza as seguintes instalações para os docentes, no Campus Farolândia:
    }

;; --------------------
;; \include{..\Comum\Instalações do Curso\Docentes.tbl}
;; --------------------
\includetable{Docentes.tbl}

\paragraph[BEFORE=240]{
    As instalações indicadas acima atendem os docentes do Curso nas diversas atividades por eles realizadas.
    Apresentam boa iluminação natural e artificial com adequado sistema de ventilação. A manutenção destas
    é realizada frequentemente, mantendo condições adequadas de limpeza.
    }
"
            },
            new BookEntity
            {
                Title = "Espaço de trabalho para docentes em Tempo Integral – TI",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Espaço de trabalho para o coordenador",
                Text =
@"\paragraph{
    O curso de \variable{@CURSO.@NOME} conta com uma (01) sala, medindo 73m², localizada no bloco G do
    Campus Farolândia e suas instalações disponibilizam condições necessárias ao desenvolvimento das funções
    do Coordenador do Curso. Este, conta com Assistentes Acadêmicos que auxiliam no desenvolvimento das
    atividades acadêmicas, bem como ao atendimento aos alunos e professores. A exemplo da sala da
    coordenação, a sala dos Assistentes Acadêmicos e recepção de alunos mede 73m². As dependências de ambas
    as salas são arejadas e apresentam excelente iluminação natural e artificial com adequado sistema de ar
    refrigerado, acessibilidade e computadores com acesso à internet e intranet. A manutenção das salas é
    realizada de forma sistemática, proporcionando o ambiente limpo e os equipamentos em perfeitas condições
    de uso, atendendo de forma excelente aos seus usuários.
    }
"
            },
            new BookEntity
            {
                Title = "Sala coletiva de professores",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Auditório/Sala de conferência",
                Text =
@"\paragraph{
    O curso de \variable{@CURSO.@NOME} utiliza os diversos auditórios, localizados nos vários CAMPI
    da \variable{@IES.@ACRONIMO}. Os referidos ambientes apresentam boa iluminação natural e artificial
    com perfeito sistema de ar refrigerado. Possuem recursos audiovisuais adequados para as atividades
    desenvolvidas e sua manutenção é feita de forma sistemática, proporcionando aos seus usuários conforto
    e bem-estar.
    }
\paragraph{
    O quadro abaixo demonstra o quantitativo de auditórios disponibilizados para as atividades do curso.
    }

;; --------------------
;; \include{..\Comum\Instalações do Curso\Auditorios.tbl}
;; --------------------
\includetable{Auditorios.tbl}
"
            },
            new BookEntity
            {
                Title = "Instalações sanitárias – adequação e limpeza",
                Text =
@"\paragraph{
    O Campus Farolândia da \variable{@IES.@ACRONIMO} disponibiliza para os alunos e professores
    do curso de \variable{@CURSO.@NOME} instalações sanitárias adequadas às necessidades deles,
    conforme discriminação na tabela abaixo:
    }

;; --------------------
;; \include{..\Comum\Instalações do Curso\Sanitarios.tbl}
;; --------------------
\includetable{Sanitarios.tbl}

\paragraph[BEFORE=240]{
    As instalações são mantidas sistematicamente limpas, com ótimo nível de higienização e conservação.
    }
"
            },
            new BookEntity
            {
                Title = "Condições de acesso para portadores de necessidades especiais",
                Text =
@"\paragraph{
    Atendendo ao Decreto 5.296/2004, a \variable{@IES.@ACRONIMO} viabiliza as condições de acesso aos
    portadores de necessidades especiais. São disponibilizados elevadores, rampas de acesso, banheiros,
    barras de fixação e vagas de estacionamento, possibilitando o deslocamento dos que possuem
    dificuldade motora ou visual e, ainda, há monitores para auxiliar os alunos portadores de deficiências
    e intérpretes de Libras.
    }
\paragraph{
    No tocante à infraestrutura, a \variable{@IES.@ACRONIMO} assegura aos estudantes do Campus Farolândia
    condições plenas para utilização, com segurança e autonomia, total ou assistida, dos espaços,
    mobiliários e equipamentos além dos dispositivos, sistemas e meios de comunicação e informação para
    aqueles que assim necessitarem. Destarte, destacamos que a infraestrutura física da \variable{@IES.@ACRONIMO}
    conta com instalações e recursos de apoio aos portadores de deficiência física, visual e auditiva, a saber:
    }

\begin{itemize}
    \paragraph{
        Livre circulação dos estudantes nos espaços de uso coletivo (eliminação de barreiras arquitetônicas);
        }

        \figure[beforelines=3;FirstLine=0;Justification=center]{
            \includegraphics[scale=0,8]{Acesso1.png}
            \caption{Fonte: Acervo institucional ano de 2017}
        }
        \figure[beforelines=3;FirstLine=0;Justification=center]{
            \includegraphics[scale=0,8]{Acesso2.png}
            \caption{Fonte: Acervo institucional ano de 2017}
        }

    \paragraph{
        Banheiros, lavatórios e bebedouros adaptados;
        }

        \figure[beforelines=3;FirstLine=0;Justification=center]{
            \includegraphics[scale=0,8]{Acesso3.png}
            \caption{Fonte: Acervo institucional ano de 2017}
        }
\end{itemize}

\paragraph{
    Como pode ser observado pelas imagens, por toda a extensão do Campus Farolândia, lócus de funcionamento do
    Curso de \variable{@CURSO.@NOME} existem rampas de acesso para cadeirantes, sinalização na entrada dos blocos,
    elevadores em todos os blocos com teclado em Braille, vagas reservadas para pessoas com mobilidade reduzida e
    rampas com corrimão facilitando a circulação para todos os nossos estudantes, tornando assim o ambiente
    interno da instituição o menos restritivo possível, pois entendemos que a inclusão trata-se de uma ação
    política, cultural, social e pedagógica em defesa do direito de todos os alunos de estarem juntos,
    compartilhando conhecimento, aprendendo e participando sem nenhum tipo de barreira.
    }
"
            },
            new BookEntity
            {
                Title = "Infraestrutura de segurança",
                Text =
@"\paragraph{
    O setor de Segurança do Trabalho tem por objetivo desenvolver ações de prevenção, com vistas a uma melhor
    condição de trabalho, evitando acidentes e protegendo o trabalho, evitando acidentes e protegendo o
    trabalhador em seu local de trabalho, tanto no que se refere à segurança quanto à higiene.
    }

;; --------------------
;; \include{..\Comum\Instalações do Curso\Seguranca.tbl}
;; --------------------
\includetable{Seguranca.tbl}

\paragraph[BEFORE=400;JUSTIFICATION=left;FIRSTLINE=0]{
    \run[BOLD]{
        Técnico de segurança:
        }
    }

\begin{itemize}
    \paragraph[JUSTIFICATION=left;FIRSTLINE=0]{Carlson José Alves de Souza Filho – Engenheiro de Segurança}
    \paragraph[JUSTIFICATION=left;FIRSTLINE=0]{Elaine Mota Ferreira Costa – Técnico de Segurança}
\end{itemize}"
            },
            new BookEntity
            {
                Title = "Estrutura física",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Informatização da Biblioteca",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Acessibilidade informacional – biblioteca inclusiva",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Acervo Total da Biblioteca",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Base de Dados por Assinatura",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Política de aquisição, expansão e atualização do acervo",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Serviços",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Pessoal técnico e administrativo",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Serviço de acesso ao acervo",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Indexação",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Apoio na elaboração de trabalhos acadêmicos",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratórios de Informática",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratórios de Física",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratórios de Química",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratórios de Desenho Técnico – pranchetas",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratório de Eletrônica – G03",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratório de Automação – G04",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratório de Acionamentos Elétricos e Instrumentação – G05",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Laboratório de Microcontroladores – G06",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Condições de conservação das instalações",
                Text =
@"
"
            },
            new BookEntity
            {
                Title = "Manutenção e conservação dos equipamentos",
                Text =
@"
"
            }
        };

        public static async Task<Dictionary<int, Guid>> Initialize(Guid instituteId, ISectionListService service)
        {
            Dictionary<int, Guid> sections = new();

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
                        sections.Add(id++, section.Id);
                    }
                }
            }

            return sections;
        }
    }
}