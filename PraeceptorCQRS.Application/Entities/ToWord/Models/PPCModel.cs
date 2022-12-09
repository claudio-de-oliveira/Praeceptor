using ErrorOr;

using PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

using Serilog;

namespace PraeceptorCQRS.Application.Entities.ToWord.Models
{
    public class PPCModel : WordDocument
    {
        private readonly IFileStreamRepository _fileRepository;
        private readonly ISimpleTableRepository _tableRepository;

        public Domain.Entities.Holding Holding { get; set; } = default!;
        public Domain.Entities.Institute Institute { get; set; } = default!;
        public Domain.Entities.Course Course { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string MainText { get; set; } = default!;
        public List<Domain.Entities.Component> Syllabus { get; set; } = default!;
        public Dictionary<Guid, string> PlannerText = new();
        public List<SocialBodyEntry> SocialBody { get; set; } = default!;

        public PPCModel(
            Domain.Entities.Holding holding,
            Domain.Entities.Institute institute,
            Domain.Entities.Course course,
            Domain.Entities.SqlFileStream template,
            IFileStreamRepository fileRepository,
            ISimpleTableRepository tableRepository
            )
        {
            _fileRepository = fileRepository;
            _tableRepository = tableRepository;

            Holding = holding;
            variables.Add("@EMPRESA.@NOME", Holding.Acronym);
            variables.Add("@EMPRESA.@ACRONIMO", Holding.Name);

            Institute = institute;
            variables.Add("@IES.@ACRONIMO", Institute.Acronym);
            variables.Add("@IES.@NOME", Institute.Name);

            Course = course;
            variables.Add("@CURSO.@NOME", Course.Name);
            variables.Add("@CURSO.@PERIODOS", Course.NumberOfSeasons.ToString());

            if (Course.Email is not null)
                variables.Add("@CURSO.@EMAIL", Course.Email);
            if (Course.Telephone is not null)
                variables.Add("@CURSO.@TELEFONE", Course.Telephone);

            // variables.Add("@CURSO.@RAMAL", "Indefinido");

            base.Create(template.Data);
        }

        public static bool CreateFromText(string text, WordDocEnvironment environment)
        {
            WordDocParser parser = new(environment);

            var o = parser.Parse(text, null);

            if (o is null)
                Log.Error(text);

            return o is not null;
        }

        public override async Task<ErrorOr<SqlFileStream>> GetImageByCode(Guid instituteId, string code)
        {
            var entity = await _fileRepository.ReadFile(instituteId, code);

            if (entity is null)
                return Domain.Errors.Error.SqlFileStream.NotFound;

            return entity;
        }

        public override async Task<ErrorOr<Domain.Entities.SimpleTable>> GetTableByCode(Guid instituteId, string code)
        {
            var entity = await _tableRepository.GetTableByCode(instituteId, code);

            if (entity is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            return entity;
        }

        public void InitializeTables()
        {
            Log.Information("Criando tabela RESUMO");
            ResumoToTable(); // @TABELA.@RESUMO

            Log.Information("Criando tabela ESTRUTURA CURRICULAR");
            SyllabusScanner();

            List<Domain.Entities.Preceptor> preceptors = new();

            int numeroDeStrictoSensu = 0;
            int numeroDeDoutores = 0;
            int numeroDeDocentes = 0;
            foreach (var entry in SocialBody)
            {
                if (!preceptors.Contains(entry.Preceptor))
                {
                    preceptors.Add(entry.Preceptor);
                    numeroDeDocentes++;

                    if (entry.Preceptor.DegreeType.StrictoSensu)
                        numeroDeStrictoSensu++;
                    if (entry.Preceptor.DegreeType.Code == "DOUTOR")
                        numeroDeDoutores++;
                }
                if (entry.Role.Code == "COORDENADOR")
                    variables.Add("@CURSO.@COORDENADOR", entry.Preceptor.Name);
            }

            List<Domain.Entities.PreceptorRoleType> roles = new();

            Log.Information("Criando tabela CORPO SOCIAL");
            string text = RoleToTable(
                $"Docentes do curso de \\variable{{@CURSO.@NOME}} (Atualizado em {DateTime.Now:dd/MM/yyyy})",
                SocialBody,
                roles
                );
            codes.Add("@TABELA.@DOCENTES", text);
            variables.Add("@CURSO.@NUMDOCENTES", numeroDeDocentes.ToString());
            variables.Add("@CURSO.@PERCSTRICTU", (100 * numeroDeStrictoSensu / numeroDeDocentes).ToString());
            variables.Add("@CURSO.@PERCDOUTORES", (100 * numeroDeDoutores / numeroDeDocentes).ToString());

            Log.Information("Criando tabela COLEGIADO");
            roles = SocialBody.Select(o => o.Role).Where(o => o.Code == "COLEGIADO").ToList();
            text = RoleToTable(
                $"Colegiado do curso de \\variable{{@CURSO.@NOME}} (Atualizado em {DateTime.Now:dd/MM/yyyy})",
                SocialBody,
                roles
                );
            codes.Add("@TABELA.@COLEGIADO", text);

            Log.Information("Criando tabela NDE");
            roles = SocialBody.Select(o => o.Role).Where(o => o.Code == "NDE" || o.Code == "SUPLENTE" || o.Code == "COORDENADOR").ToList();
            text = RoleToTable(
                $"NDE do curso de \\variable{{@CURSO.@NOME}} (Atualizado em {DateTime.Now:dd/MM/yyyy})",
                SocialBody,
                roles
                );
            codes.Add("@TABELA.@NDE", text);
        }

        #region Tabelas

        private static readonly string Format =
            "\\default[\n" +
            "   KEEPNEXT;\n" +
            "   KEEPLINES;\n" +
            "   SPACINGBETWEENLINES=160;\n" +
            "   FIRSTLINE=0;\n" +
            "   JUSTIFICATION=center\n" +
            "]\n" +
            "{paragraph}\n" +
            "\n" +
            "\\default[\n" +
            "   TOPMARGIN=100;\n" +
            "   BOTTOMMARGIN=100;\n" +
            "   TableCellVerticalAlignment=Center\n" +
            "]\n" +
            "{tablecell}\n" +
            "\n" +
            "\\default[\n" +
            "   TOPBORDER=Double;\n" +
            "   BOTTOMBORDER=Double;\n" +
            "   LEFTBORDER=Double;\n" +
            "   RIGHTBORDER=Double;\n" +
            "   INSIDEHORIZONTALBORDER=Single;\n" +
            "   INSIDEVERTICALBORDER=Single;\n" +
            "   TABLECELLLEFTMARGIN=100;\n" +
            "   TABLECELLRIGHTMARGIN=100;\n" +
            "   JUSTIFICATION=Center;\n" +
            "   TABLEOVERLAP=Never\n" +
            "]\n" +
            "{table}\n" +
            "\n";

        #region R E S U M O

        private void ResumoToTable()
        {
            string text = Format;

            text +=
                "\\table{\n" +
                "    \\row{\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{CH Teórica em Horas}}\n" +
                "        }\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{CH Prática em Horas}}\n" +
                "        }\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Estágio Supervisionado em Horas (CH Prática)}}\n" +
                "        }\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária disciplinas Semipresenciais em Horas}}\n" +
                "        }\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Atividades Complementares em Horas}}\n" +
                "        }\n" +
                "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{CH Total em Horas}}\n" +
                "        }\n" +
                "    }\n" +
                "\n" +
                "    \\row{\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CTtotal60}}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CPtotal60}}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CR_TCC} + \\variable{@CURSO.@CR_ESTAGIO}}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CR_ONLINE}}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CHAC}}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{\n" +
                "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{\\variable{@CURSO.@CHT60}}}\n" +
                "        }\n" +
                "    }\n\n" +
                "\n" +
                $"    \\caption[BEFORE=300;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{Resumo da distribuição de carga horária do curso de \\variable{{@CURSO.@NOME}}. (Atualizado em {DateTime.Now:dd/MM/yyyy})}}\n" +
                "\n" +
                "}\n" +
                "\n" +
                "\\default{paragraph}\n" +
                "\\default{tablecell}\n" +
                "\\default{table}\n";

            codes.Add("@TABELA.@RESUMO", text);
        }

        #endregion R E S U M O

        #region P E R I O D O S

        private static string CreateHeader()
            => "    \\row{\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Código}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLVERTICALALIGNMENT=Center; JUSTIFICATION=Left]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Disciplina}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Eixo}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Pré – Requisito}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Teórica}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Prática}}\n" +
               "        }\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Total}}\n" +
               "        }\n" +
               "    }\n";

        private static string CreateMiddle(string codigo, string nome, string eixo, string PR, int CT, int CP, int CH)
            => $"    \\row{{\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLWIDTH=100; TABLECELLVERTICALALIGNMENT=Center]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{codigo}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLVERTICALALIGNMENT=Center]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=left;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{nome}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{eixo}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{PR}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{CT}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{CP}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{CH}}}}}\n" +
               $"        }}\n" +
               $"    }}\n";

        private static string CreateFooter(int CTparcial, int CPparcial, int CHparcial, int periodo)
            => $"    \\row{{\n" +
               $"        \\cell{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{ }}}}\n" + // Manter espaço
               $"        }}\n" +
               $"        \\cell{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{ }}}}\n" + // Manter espaço
               $"        }}\n" +
               $"        \\cell{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{ }}}}\n" + // Manter espaço
               $"        }}\n" +
               $"        \\cell[SHADING=Percent10]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[BOLD; FONTSIZE=8]{{TOTAL:}}}}\n" +
               $"        }}\n" +
               $"        \\cell[SHADING=Percent10]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[BOLD; FONTSIZE=8]{{{CTparcial}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[SHADING=Percent10]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[BOLD; FONTSIZE=8]{{{CPparcial}}}}}\n" +
               $"        }}\n" +
               $"        \\cell[SHADING=Percent10]{{\n" +
               $"            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[BOLD; FONTSIZE=8]{{{CHparcial}}}}}\n" +
               $"        }}\n" +
               $"    }}\n" +
               $"    \n" +
               $"    \\caption[BEFORE=200;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{Estrutura Curricular do {periodo}º Período}}\n";

        #endregion P E R I O D O S

        #region S Y L L A B U S

        private static readonly string optativaHeader =
            "    \\row{\n" +
            "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
            "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Código}}\n" +
            "        }\n" +
            "        \\cell[SHADING=Percent10; TABLECELLVERTICALALIGNMENT=Center; JUSTIFICATION=Left]{\n" +
            "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Disciplina Optativa}}\n" +
            "        }\n" +
            "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
            "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Teórica}}\n" +
            "        }\n" +
            "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
            "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Prática}}\n" +
            "        }\n" +
            "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
            "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Carga Horária Total}}\n" +
            "        }\n" +
            "    }\n";

        private static readonly string optativaBody =
            "    \\row{{\n" +
            "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLWIDTH=100; TABLECELLVERTICALALIGNMENT=Center]{{\n" +
            "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{0}}}}}\n" +
            "        }}\n" +
            "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLVERTICALALIGNMENT=Center]{{\n" +
            "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=left;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{1}}}}}\n" +
            "        }}\n" +
            "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
            "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{2}}}}}\n" +
            "        }}\n" +
            "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
            "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{3}}}}}\n" +
            "        }}\n" +
            "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50]{{\n" +
            "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{{\\run[FONTSIZE=8]{{{4}}}}}\n" +
            "        }}\n" +
            "    }}\n";

        private static readonly string optativaCaption =
            "	\\caption[BEFORE=200;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{Disciplinas Optativas do {0}º Período}}\n";

        public void SyllabusScanner()
        {
            int CargaHoráriaTeoricaTotalEmMinutos = 0;
            int CargaHoráriaPráticaTotalEmMinutos = 0;

            int CargaHorariaOnlineTotalEmMinutos = 0;
            int CargaHorariaTCCTotalEmMinutos = 0;
            int CargaHorariaEstagioTotalEmMinutos = 0;

            Dictionary<string, int> AxisCH = new();
            Dictionary<Guid, int> ClassTypeCH = new();

            var seasons = new List<Domain.Entities.Component>[Course.NumberOfSeasons];
            for (int season = 0; season < Course.NumberOfSeasons; season++)
            {
                seasons[season] = new List<Domain.Entities.Component>();
            }

            Syllabus.ForEach(o => seasons[o.Season].Add(o));

            string[] optativas =
                new string[Course.NumberOfSeasons];
            string[] periodos =
                new string[Course.NumberOfSeasons];

            string planners;

            foreach (var season in seasons)
            {
                if (season.Count == 0)
                    continue;

                int CargaHorariaTeoricaPeriodoInMinutes = 0;
                int CargaHorariaPraticaPeriodoInMinutes = 0;

                planners = "";

                foreach (var component in season)
                {
                    int durationInMinutes = component.Class.Type.DurationInMinutes;
                    int CargaHorariaTeoricaDisciplinaInMinutes = 20 * component.Class.Theory * durationInMinutes;
                    int CargaHorariaPraticaDisciplinaInMinutes = 20 * component.Class.Practice * durationInMinutes;
                    int CargaHorariaDisciplinaInMinutes = CargaHorariaPraticaDisciplinaInMinutes + CargaHorariaTeoricaDisciplinaInMinutes;

                    string nome = component.Class.Name + (component.Class.Type.IsRemote ? " (*)" : "");
                    string PR = component.Class.PR != 0 ? component.Class.PR + "C" : "-";

                    if (component.Optative)
                    {
                        if (optativas[component.Season] is null)
                        {
                            optativas[component.Season] = Format;
                            optativas[component.Season] += "\\table{\n" + optativaHeader;
                        }
                        optativas[component.Season] += string.Format(
                            optativaBody,
                            component.Class.Code,
                            nome,
                            ConverteMinutosParaHoras(CargaHorariaTeoricaDisciplinaInMinutes),
                            ConverteMinutosParaHoras(CargaHorariaPraticaDisciplinaInMinutes),
                            ConverteMinutosParaHoras(CargaHorariaDisciplinaInMinutes));
                    }
                    else
                    {
                        CargaHoráriaTeoricaTotalEmMinutos += CargaHorariaTeoricaDisciplinaInMinutes;
                        CargaHoráriaPráticaTotalEmMinutos += CargaHorariaPraticaDisciplinaInMinutes;

                        if (component.Class.Type.IsRemote)
                            CargaHorariaOnlineTotalEmMinutos += CargaHorariaDisciplinaInMinutes;
                        if (component.Class.Type.IsTCC)
                            CargaHorariaTCCTotalEmMinutos += CargaHorariaDisciplinaInMinutes;
                        if (component.Class.Type.IsEstagio)
                            CargaHorariaEstagioTotalEmMinutos += CargaHorariaDisciplinaInMinutes;

                        if (periodos[component.Season] is null)
                        {
                            periodos[component.Season] = Format;
                            periodos[component.Season] += "\\table{\n";
                            periodos[component.Season] += CreateHeader();
                        }
                        string axisKey = $"@CURSO.@{component.Axis.Code.ToUpper()}";
                        if (!AxisCH.ContainsKey(axisKey))
                            AxisCH.Add(axisKey, 0);

                        CargaHorariaPraticaPeriodoInMinutes += CargaHorariaPraticaDisciplinaInMinutes;
                        CargaHorariaTeoricaPeriodoInMinutes += CargaHorariaTeoricaDisciplinaInMinutes;

                        if (nome.Length == 0)
                            nome = "INDEFINIDO";

                        AxisCH[axisKey] += CargaHorariaDisciplinaInMinutes;

                        if (!ClassTypeCH.ContainsKey(component.Class.TypeId))
                            ClassTypeCH.Add(component.Class.TypeId, 0);

                        ClassTypeCH[component.Class.TypeId] += CargaHorariaDisciplinaInMinutes;

                        periodos[component.Season] += CreateMiddle(
                            component.Class.Code,
                            nome,
                            component.Axis.Code3,
                            PR,
                            ConverteMinutosParaHoras(CargaHorariaTeoricaDisciplinaInMinutes),
                            ConverteMinutosParaHoras(CargaHorariaPraticaDisciplinaInMinutes),
                            ConverteMinutosParaHoras(CargaHorariaDisciplinaInMinutes));
                    }

                    if (component.Class.HasPlanner)
                        planners += PlannerText[component.ClassId];
                }
                codes.Add($"@TABELA.@PEAS{season[0].Season + 1}", planners);

                periodos[season[0].Season] +=
                    CreateFooter(
                        ConverteMinutosParaHoras(CargaHorariaTeoricaPeriodoInMinutes),
                        ConverteMinutosParaHoras(CargaHorariaPraticaPeriodoInMinutes),
                        ConverteMinutosParaHoras(CargaHorariaTeoricaPeriodoInMinutes + CargaHorariaPraticaPeriodoInMinutes),
                        season[0].Season + 1);

                periodos[season[0].Season] += "}\n" +
                    "\\default{paragraph}\n" +
                    "\\default{tablecell}\n" +
                    "\\default{table}\n";

                if (optativas[season[0].Season] is not null)
                {
                    optativas[season[0].Season] +=
                        string.Format("\n" + optativaCaption + "\n}}\n\n", season[0].Season + 1);
                    optativas[season[0].Season] +=
                        "\\default{paragraph}\n" +
                        "\\default{tablecell}\n" +
                        "\\default{table}\n";
                }
            }

            int CargaHoráriaTotalEmMinutos = CargaHoráriaPráticaTotalEmMinutos + CargaHoráriaTeoricaTotalEmMinutos;

            variables.Add("@CURSO.@CHAC", (Course.AC * 20).ToString());

            CargaHoráriaTotalEmMinutos += Course.AC * 20 * 60;

            foreach (var key in AxisCH.Keys)
            {
                variables.Add(key, ConverteMinutosParaHoras(AxisCH[key]).ToString());
                if (CargaHoráriaTotalEmMinutos > 0)
                    variables.Add($"{key}_PERCENTUAL", (100 * ConverteMinutosParaHoras(AxisCH[key]) / ConverteMinutosParaHoras(CargaHoráriaTotalEmMinutos)).ToString());
            }

            variables.Add("@CURSO.@CR_ONLINE", ConverteMinutosParaHoras(CargaHorariaOnlineTotalEmMinutos).ToString());
            variables.Add("@CURSO.@CR_TCC", ConverteMinutosParaHoras(CargaHorariaTCCTotalEmMinutos).ToString());
            variables.Add("@CURSO.@CR_ESTAGIO", ConverteMinutosParaHoras(CargaHorariaEstagioTotalEmMinutos).ToString());

            variables.Add("@CURSO.@CPTOTAL60", ConverteMinutosParaHoras(CargaHoráriaPráticaTotalEmMinutos).ToString());
            variables.Add("@CURSO.@CTTOTAL60", ConverteMinutosParaHoras(CargaHoráriaTeoricaTotalEmMinutos).ToString());

            variables.Add("@CURSO.@CHT60", ConverteMinutosParaHoras(CargaHoráriaTotalEmMinutos).ToString());

            for (int i = 0, n = 1; i < optativas.Length; i++)
                if (optativas[i] is not null)
                    codes.Add($"@TABELA.@OPTATIVA{n++}", optativas[i]);
            for (int i = 0, n = 1; i < periodos.Length; i++)
                if (periodos[i] is not null)
                    codes.Add($"@TABELA.@PERIODO{n++}", periodos[i]);
        }

        #endregion S Y L L A B U S

        #region D O C E N T E S ,   N D E   E   C O L E G I A D O

        private static string DocenteRow(Domain.Entities.Preceptor preceptor, string msg = "")
            => "    \\row[CANTSPLIT]{\n" +
               "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=left;FIRSTLINE=0]{\\run[FONTSIZE=8]{" +
               $"{preceptor.Name} {msg}" +
               "}}\n" +
               "        }\n" +
               "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{" +
               preceptor.DegreeType.Code +
               "}}\n" +
               "        }\n" +
               "        \\cell[TOPMARGIN=50;BOTTOMMARGIN=50;TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[SPACINGBETWEENLINES=240;JUSTIFICATION=center;FIRSTLINE=0]{\\run[FONTSIZE=8]{" +
               preceptor.RegimeType.Code +
               "}}\n" +
               "        }\n" +
               "    }\n";

        public static string RoleToTable(string caption, List<SocialBodyEntry> socialBody, List<Domain.Entities.PreceptorRoleType> roles)
        {
            string text = Format;

            text +=
                "\\table{\n" +
                "    \\row[CANTSPLIT]{\n" +
                "        \\cell[TOPMARGIN=150;BOTTOMMARGIN=150;SHADING=Percent10; TABLECELLWIDTH=3000; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Docente}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=150;BOTTOMMARGIN=150;SHADING=Percent10; TABLECELLWIDTH=1000; TABLECELLVERTICALALIGNMENT=Center; JUSTIFICATION=Left]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Titulação}}\n" +
                "        }\n" +
                "        \\cell[TOPMARGIN=150;BOTTOMMARGIN=150;SHADING=Percent10; TABLECELLWIDTH=1000; TABLECELLVERTICALALIGNMENT=Center]{\n" +
                "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{Regime de Trabalho}}\n" +
                "        }\n" +
                "    }\n";

            List<Guid> used = new();

            foreach (var entry in socialBody)
            {
                if (roles.Any())
                {
                    if (roles.FirstOrDefault(o => o.Id == entry.RoleId) is not null)
                    {
                        if (!used.Contains(entry.PreceptorId))
                        {
                            text += DocenteRow(entry.Preceptor);
                            used.Add(entry.PreceptorId);
                        }
                    }
                }
                else
                {
                    if (!used.Contains(entry.PreceptorId))
                    {
                        text += DocenteRow(entry.Preceptor);
                        used.Add(entry.PreceptorId);
                    }
                }
            }

            text += $"    \\caption[BEFORE=200;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{{caption}}}\n";
            text += "}\n";
            text += "\n";
            text += "\\default{paragraph}\n";
            text += "\\default{tablecell}\n";
            text += "\\default{table}\n\n";

            return text;
        }

        #endregion D O C E N T E S ,   N D E   E   C O L E G I A D O

        #endregion Tabelas

        private static int ConverteMinutosParaHoras(int minutos)
            => minutos / 60;
    }
}