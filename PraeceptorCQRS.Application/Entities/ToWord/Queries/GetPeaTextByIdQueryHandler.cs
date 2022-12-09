using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Domain.Values.PeaValues;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetPeaTextByIdQueryHandler
        : IRequestHandler<GetPeaTextByIdQuery, ErrorOr<PlannerTextResult>>
    {
        private readonly IClassRepository _classRepository;
        private readonly IPeaRepository _peaRepository;

        public GetPeaTextByIdQueryHandler(IPeaRepository peaRepository, IClassRepository classRepository)
        {
            _peaRepository = peaRepository;
            _classRepository = classRepository;
        }

        public async Task<ErrorOr<PlannerTextResult>> Handle(GetPeaTextByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            var pea = await _peaRepository.GetPeaById(request.PeaId);
            if (pea is null)
                return Domain.Errors.Error.Pea.NotFound;

            var cls = await _classRepository.GetClassById(pea.ClassId);
            if (cls is null)
                return Domain.Errors.Error.Class.NotFound;

            Parser.Planner.Parser parser = new();
            var result = await Task.Run(() => parser.Parse(pea.Text, null));
            if (result is null)
                return Domain.Errors.Error.Pea.InvalidSyntax;

            PeaModel model = (PeaModel)result;

            string text = ConvertPeaToString(model, cls.Code, cls.Name, cls.Theory, cls.Practice, request.Season);

            return new PlannerTextResult(text);
        }

        private static string ConvertPeaToString(
            PeaModel planner,
            string classCode,
            string className,
            int theory,
            int practice,
            int season
            )
        {
            string str = HeaderToWord(classCode, className, theory, practice, season);

            int item = 1;

            str += "\\paragraph{ }\n";

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item++} Ementa}}}}\n";
            if (!string.IsNullOrWhiteSpace(planner.Ementa))
                str += $"\\paragraph[JUSTIFICATION=both]{{\\run{{{planner.Ementa}}}}}\n";
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";

            str += "\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{\\run[BOLD]{Unidades de Ensino (Conceitos-chave): }";
            if (planner.Unidade1.Count > 0 && planner.Unidade2.Count > 0)
            {
                str += "\\run{";
                for (int i = 0; i < planner.Unidade1.Count - 1; i++)
                    str += planner.Unidade1[i].Description + ", ";
                str += planner.Unidade1[^1].Description;
                for (int i = 0; i < planner.Unidade2.Count; i++)
                    str += ", " + planner.Unidade2[i].Description;
                str += "}\n";
            }
            else
            {
                str += "\n  \\run[BOLD;color=8388608]{I N C O M P L E T O}";
            }
            str += "}\n";

            str += "\\paragraph{ }\n";

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item++} OBJETIVO DA DISCIPLINA}}}}\n";
            str += "\\begin{itemize}\n";
            if (planner.Objetivos.Count > 0)
            {
                foreach (string text in planner.Objetivos)
                    str += $"\\paragraph[JUSTIFICATION=both]{{\\run{{{text}}}}}\n";
            }
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
            str += "\\end{itemize}\n";

            str += "\\paragraph{ }\n";

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=100]{{{item} DESENVOLVIMENTO DO PLANEJAMENTO DE ENSINO}}}}\n";
            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item}.1 SABERES POR UNIDADE DE ENSINO}}}}\n";

            str += "\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{\\run[CAPS;Color=200]{Unidade I}}\n";
            if (planner.Unidade1 is not null)
                str += ConceitosToWord(planner.Unidade1);

            str += "\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{\\run[CAPS;Color=200]{Unidade II}}\n";
            if (planner.Unidade2 is not null)
                str += ConceitosToWord(planner.Unidade2);

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item}.2 Procedimentos Metodológicos}}}}\n";
            if (!string.IsNullOrWhiteSpace(planner.Procedimentos))
                str += $"\\paragraph[JUSTIFICATION=both]{{\\run{{{planner.Procedimentos}}}}}\n";
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
            str += "\\paragraph{ }\n";

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item}.3 Procedimentos de Avaliação}}}}\n";
            if (!string.IsNullOrWhiteSpace(planner.Avaliacao))
                str += $"\\paragraph[JUSTIFICATION=both]{{\\run{{{planner.Avaliacao}}}}}\n";
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
            str += "\\paragraph{ }\n";

            item++;

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=100]{{{item} REFERÊNCIAS BIBLIOGRÁFICAS}}}}\n";
            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item}.1 Básica}}}}\n";

            str += "\\default[JUSTIFICATION=both; FIRSTLINE=-360;LEFT=720]{paragraph}\n";
            if (planner.BibliografiaBasica.Count > 0)
            {
                foreach (BibItem bib in planner.BibliografiaBasica)
                    str += $"\\paragraph{{{BibToWord(bib)}}}\n";
            }
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";

            str += $"\\paragraph[JUSTIFICATION=both; FIRSTLINE=0]{{\\run[CAPS;Color=200]{{{item}.2 Complementar}}}}\n";

            if (planner.BibliografiaComplementar.Count > 0)
            {
                foreach (BibItem bib in planner.BibliografiaComplementar)
                    str += $"\\paragraph{{{BibToWord(bib)}}}\n";
            }
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
            str += "\\default{paragraph}\n";

            str += "\\paragraph[AFTER=200]{\\run{ }}\n";

            // if (footer)
            // {
            //     str += History();
            //     str += "\\paragraph[AFTER=500]{\\run{ }}\n";
            // }

            return RemoveEspacos(str);
        }

        #region TO WORD AUXILIARY FUNCTIONS

        private static string BibToWord(BibItem bib)
        {
            string str = "";

            if (bib.Autores.Nomes.Count == 0 && bib.Tradutores.Nomes.Count == 0 && bib.Organizadores.Nomes.Count == 0)
            {
                str += $"______. ";
            }
            else
            {
                if (bib.Autores.Nomes.Count > 0)
                {
                    for (int i = 0; i < bib.Autores.Nomes.Count - 1; i++)
                        str += $"\\run[ITALIC;CAPS]{{{bib.Autores.Nomes[i].Sobrenome}}}, \\run[ITALIC]{{{bib.Autores.Nomes[i].Nome}; }}";
                    str += $"\\run[ITALIC;CAPS]{{{bib.Autores.Nomes[^1].Sobrenome}}}, \\run[ITALIC]{{{bib.Autores.Nomes[^1].Nome}. }}";
                }
                else if (bib.Tradutores.Nomes.Count > 0)
                {
                    for (int i = 0; i < bib.Tradutores.Nomes.Count - 1; i++)
                        str += $"\\run[ITALIC;CAPS]{{{bib.Tradutores.Nomes[i].Sobrenome}}}, \\run[ITALIC]{{{bib.Tradutores.Nomes[i].Nome}; }}";
                    str += $"\\run[ITALIC;CAPS]{{{bib.Tradutores.Nomes[^1].Sobrenome}}}, \\run[ITALIC]{{{bib.Tradutores.Nomes[^1].Nome} (Tradutor(es)). }}";
                }
                else if (bib.Organizadores.Nomes.Count > 0)
                {
                    for (int i = 0; i < bib.Organizadores.Nomes.Count - 1; i++)
                        str += $"\\run[ITALIC;CAPS]{{{bib.Organizadores.Nomes[i].Sobrenome}}}, \\run[ITALIC]{{{bib.Organizadores.Nomes[i].Nome}; }}";
                    str += $"\\run[ITALIC;CAPS]{{{bib.Organizadores.Nomes[^1].Sobrenome}}}, \\run[ITALIC]{{{bib.Organizadores.Nomes[^1].Nome} (Organizador(es)). }}";
                }
            }

            if (!string.IsNullOrEmpty(bib.Title))
                str += $"\\run[BOLD]{{{bib.Title}. }}";
            if (!string.IsNullOrEmpty(bib.Series))
                str += $"\\run{{Série: {bib.Series}. }}";

            if (!string.IsNullOrEmpty(bib.Editor))
                str += $"\\run{{{bib.Editor}. }}";

            if (bib.Volume is not null)
            {
                if (!string.IsNullOrEmpty(bib.Volume.Text2))
                {
                    str += $"\\run{{{bib.Volume.Text1}}}";
                    if (!string.IsNullOrEmpty(bib.Volume.Text2))
                        str += $"\\run{{ – {bib.Volume.Text2}. }}";
                    else
                        str += $"\\run{{. }}";
                }
            }
            if (bib.Edition > 0)
                str += $"\\run{{{bib.Edition}}}\\run[VERTICALTEXTALIGNMENT=superscript]{{a }}\\run{{edição. }}";

            if (bib.Publisher is not null)
            {
                if (!string.IsNullOrEmpty(bib.Publisher.Nome))
                {
                    str += $"\\run{{{bib.Publisher.Nome}}}";
                    if (!string.IsNullOrEmpty(bib.Publisher.Endereco))
                        str += $"\\run{{, {bib.Publisher.Endereco}. }}";
                    else
                        str += $"\\run{{. }}";
                }
            }
            if (bib.Year > 0)
                str += $"\\run{{{bib.Year}.}}";

            return str;
        }

        private static string HeaderToWord(string id, string nome, int CT, int CP, int periodo)
        {
            string str = "";

            string p = periodo >= 0 ? (periodo + 1).ToString() : "---";

            str += "\\default[\n";
            str += "    KEEPNEXT;\n";
            str += "    KEEPLINES;\n";
            str += "    SPACINGBETWEENLINES=240;\n";
            str += "    FIRSTLINE=0;\n";
            str += "    JUSTIFICATION=center\n";
            str += "    ]\n";
            str += "    {paragraph}\n";
            str += "\n";
            str += "\\default[\n";
            str += "    TOPMARGIN=100;\n";
            str += "    BOTTOMMARGIN=100;\n";
            str += "    TABLECELLVERTICALALIGNMENT=Center\n";
            str += "    ]\n";
            str += "    {tablecell}\n";
            str += "\n";
            str += "\\default[\n";
            str += "    TOPBORDER=Double;\n";
            str += "    BOTTOMBORDER=Double;\n";
            str += "    LEFTBORDER=Double;\n";
            str += "    RIGHTBORDER=Double;\n";
            str += "    INSIDEHORIZONTALBORDER=Single;\n";
            str += "    INSIDEVERTICALBORDER=Single\n";
            str += "    ]\n";
            str += "    {table}\n";
            str += "\n";

            str += "\\table{\n";
            str += "  \\row[CANTSPLIT; TABLEHEADER]{\n";
            str += "    \\cell[VERTICALMERGE=Restart; TOPMARGIN=100; BOTTOMMARGIN=100]{\n";
            str += "       \\figure[KEEPNEXT; KEEPLINES; JUSTIFICATION=center; FIRSTLINE=0]{\n";
            str += "          \\includegraphics[SCALE=0,4]{Unit.jpg}\n";
            str += "       }\n";
            str += "       \\paragraph{\\run[BOLD;CAPS;FONTSIZE=12]{SUPERINTENDÊNCIA ACADÊMICA}}\n";
            str += "       \\paragraph{\\run[BOLD;CAPS;FONTSIZE=12]{PRÓ-REITORIA DE GRADUAÇÃO}}\n";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Restart; TOPMARGIN=100; BOTTOMMARGIN=100]{\n";
            str += "      \\paragraph{\\run[CAPS; BOLD; FONTSIZE=12]{" + "CIÊNCIAS EXATAS E TECNOLÓGICAS" + "}}";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "  }\n";
            str += "  \\row[CANTSPLIT;TABLEHEADER]{\n";
            str += "    \\cell[VERTICALMERGE=Continue;TOPMARGIN=100;BOTTOMMARGIN=100]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HorizontalMerge=Restart]{\n";
            str += "      \\paragraph{\\run[CAPS;BOLD;FONTSIZE=12]{" + nome + "}}";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "  }\n";
            str += "  \\row[CANTSPLIT;TABLEHEADER]{\n";
            str += "    \\cell[VERTICALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[SHADING=Percent10]{\n";
            str += "      \\paragraph{\\run[CAPS]{Código}}";
            str += "    }\n";
            str += "    \\cell[SHADING=Percent10]{\n";
            str += "      \\paragraph{\\run[CAPS]{Créditos}}";
            str += "    }\n";
            str += "    \\cell[SHADING=Percent10]{\n";
            str += "      \\paragraph{\\run[CAPS]{Período}}";
            str += "    }\n";
            str += "    \\cell[SHADING=Percent10]{\n";
            str += "      \\paragraph{\\run[CAPS]{Carga Horária}}";
            str += "    }\n";
            str += "  }\n";
            str += "  \\row[CANTSPLIT;TABLEHEADER]{\n";
            str += "    \\cell[VERTICALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell{\n";
            str += "      \\paragraph{\\run[CAPS;BOLD;FONTSIZE=12]{" + id + "}}";
            str += "    }\n";
            str += "    \\cell{\n";
            str += "      \\paragraph{\\run[CAPS;BOLD;FONTSIZE=12]{" + (CP + CT) + "}}";
            str += "    }\n";
            str += "    \\cell{\n";
            str += "      \\paragraph{\\run[CAPS;BOLD;FONTSIZE=12]{" + p + "}}";
            str += "    }\n";
            str += "    \\cell{\n";
            str += "      \\paragraph{\\run[CAPS;BOLD;FONTSIZE=12]{" + 20 * (CP + CT) + "}}";
            str += "    }\n";
            str += "  }\n";
            str += "  \\row[CANTSPLIT;TABLEHEADER]{\n";
            str += "    \\cell[SHADING=Percent10;HORIZONTALMERGE=Restart;TOPMARGIN=200;BOTTOMMARGIN=90]{\n";
            str += "      \\paragraph{\\run[FONTSIZE=12;ITALIC]{PLANO DE ENSINO E APRENDIZAGEM – Cód. Acervo Acadêmico – 122.3}}";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "    \\cell[HORIZONTALMERGE=Continue]{\n";
            str += "      \\paragraph{ }";
            str += "    }\n";
            str += "  }\n";
            str += "}\n";
            str += "\n";
            str += "\\default{paragraph}\n";
            str += "\\default{tablecell}\n";
            str += "\\default{table}\n";
            str += "\n";

            return str;
        }

        private static string History()
        {
            string str =
                "\\default[\n" +
                "   KEEPNEXT;\n" +
                "   KEEPLINES;\n" +
                "	SPACINGBETWEENLINES=180;\n" +
                "	FIRSTLINE=0;\n" +
                "	JUSTIFICATION=center\n" +
                "]\n" +
                "{paragraph}\n" +
                "\n" +
                "\\default[\n" +
                "	TOPMARGIN=100;\n" +
                "	BOTTOMMARGIN=100;\n" +
                "	TableCellVerticalAlignment=Center\n" +
                "]\n" +
                "{tablecell}\n" +
                "\n" +
                "\\default[\n" +
                "	TOPBORDER=Double;\n" +
                "	BOTTOMBORDER=Double;\n" +
                "	LEFTBORDER=Double;\n" +
                "	RIGHTBORDER=Double;\n" +
                "	INSIDEHORIZONTALBORDER=Single;\n" +
                "	INSIDEVERTICALBORDER=Single;\n" +
                "	TABLECELLLEFTMARGIN=100;\n" +
                "	TABLECELLRIGHTMARGIN=100;\n" +
                "	JUSTIFICATION=Center;\n" +
                "	TABLEOVERLAP=Never\n" +
                "]\n" +
                "{table}\n" +
                "\n";

            str +=
                "\\table{\n";

            str +=
                "	\\row{\n" +
                "		\\cell[JUSTIFICATION=Center;SHADING=Percent10;HORIZONTALMERGE=RESTART]{\n" +
                "			\\paragraph[FirstLine=0;JUSTIFICATION=CENTER]{\\run[FONTSIZE=12;Bold]{Responsáveis pela elaboração e validação}}\n" +
                "		}\n" +
                "		\\cell[SHADING=Percent10;HORIZONTALMERGE=CONTINUE]{\n" +
                "			\\paragraph[FirstLine=0]{ }\n" +
                "		}\n" +
                "		\\cell[SHADING=Percent10;HORIZONTALMERGE=CONTINUE]{\n" +
                "			\\paragraph[FirstLine=0]{ }\n" +
                "		}\n" +
                "	}\n";

            str +=
                "	\\row{\n" +
                "		\\cell{\n" +
                "			\\paragraph{\\run[FONTSIZE=10;BOLD]{Docentes}}\n" +
                "           \\paragraph{ }\n" +
                "			\\paragraph{\\run[FONTSIZE=8]{Nome - CursoDbModel}}\n" +
                "		}\n" +
                "		\\cell{\n" +
                "			\\paragraph{\\run[FONTSIZE=10;BOLD]{Análise da Pró-reitoria}}\n" +
                "           \\paragraph{ }\n" +
                "			\\paragraph{\\run[FONTSIZE=8]{Betisabel Vilar Santos}}\n" +
                "			\\paragraph{\\run[FONTSIZE=8]{Dilma Balbino de Menezes}}\n" +
                "		}\n" +
                "		\\cell{\n" +
                "			\\paragraph{\\run[FONTSIZE=10;BOLD]{Validação}}\n" +
                "           \\paragraph{ }\n" +
                "			\\paragraph{\\run[FONTSIZE=10]{Pró-Reitoria e Coordenação}}\n" +
                "			\\paragraph{\\run[FONTSIZE=10]{Data: 18/11/2019}}\n" +
                "		}\n" +
                "	}\n";

            str +=
                "}\n";

            str +=
                "\\default{paragraph}\n" +
                "\\default{tablecell}\n" +
                "\\default{table}\n";

            str +=
                "\\paragraph{ }\n";

            return str;
        }

        private static string ConceitosToWord(List<ConceitoChave> conceitos)
        {
            string str = "";

            if (conceitos.Count > 0)
            {
                for (int i = 0; i < conceitos.Count; i++)
                {
                    str += $"\\paragraph[JUSTIFICATION=both]{{Conceito chave: \\run[BOLD]{{{conceitos[i].Description}}}}}\n";
                    str += "\\begin{itemize}\n";
                    if (conceitos[i].Conteudos.Count > 0)
                    {
                        conceitos[i].Conteudos.ForEach(
                            c => str += $"\\paragraph[JUSTIFICATION=both]{{\\run{{{c}}}}}\n"
                            );
                    }
                    else
                        str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
                    str += "\\end{itemize}\n";

                    str += "\\paragraph{ }\n";
                }
            }
            else
                str += "\\paragraph[JUSTIFICATION=both]{\\run[BOLD;color=8388608]{I N C O M P L E T O}}\n";
            return str;
        }

        #endregion TO WORD AUXILIARY FUNCTIONS

        #region Eliminação de espaços desnecessários e comentários

        private static string RemoveEspacos(string str)
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
                            // pos++;
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

        #endregion Eliminação de espaços desnecessários e comentários
    }
}