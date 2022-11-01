using Administrative.App.Interfaces;
using Administrative.App.Models;

using Ardalis.GuardClauses;

using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.AxisType;
using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Contracts.Entities.Component;
using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Contracts.Entities.Preceptor;
using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;
using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;

namespace Administrative.App
{
    public static class SeedData
    {
        private static HoldingModel? holding;
        private static readonly Dictionary<string, InstituteModel> institutes = new();
        private static readonly Dictionary<string, CourseModel> courses = new();
        private static readonly Dictionary<string, ClassTypeModel> classTypes = new();
        private static readonly Dictionary<string, ClassModel> classes = new();
        private static readonly Dictionary<string, AxisTypeModel> axis = new();
        private static readonly Dictionary<string, PreceptorDegreeTypeModel> preceptorDegreeTypeModels = new();
        private static readonly Dictionary<string, PreceptorRegimeTypeModel> preceptorRegimeTypeModels = new();

        public static async Task InitializeHoldingTable(IHoldingService service)
        {
            var count = await service.GetHoldingCount();
            if (count > 0)
                return;

            holding = await InitializeHolding(
                service,
                new CreateHoldingRequest(
                    "SET",
                    "Sociedade Educacional Tiradentes",
                    "Av. Murilo Dantas, 300 - Farolândia 49032-490 Aracaju - Sergipe - Brasil"
                    )
                );
            Guard.Against.Null(holding);
        }

        public static async Task InitializeInstituteTable(IInstituteService service)
        {
            if (holding is null)
                return;

            InstituteModel? institute;

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "FITS/Piedade",
                    "Faculdade Tiradentes de Jaboatão dos Guararapes",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "FITS/Goiana",
                    "Faculdade Tiradentes de Goiana",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "FSLF",
                    "Faculdade São Luiz de França",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "CUT",
                    "Centro Universitário Tiradentes",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "UNIT/SE",
                    "Universidate Tiradentes, Campus Sergipe",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "UNIT/AL",
                    "Universidate Tiradentes, Campus Alagoas",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);

            institute = await InitializeInstitute(
                service,
                new CreateInstituteRequest(
                    "UNIT/PE",
                    "Universidate Tiradentes, Campus Pernambuco",
                    null,
                    holding.Id
                    )
                );
            Guard.Against.Null(institute);
            institutes.Add(institute.Acronym, institute);
        }

        public static async Task InitializeCourseTable(ICourseService service)
        {
            if (holding is null)
                return;

            var institute = institutes["UNIT/SE"];
            Guard.Against.Null(institute);

            CourseModel? course;

            course = await InitializeCourse(
                service,
                new CreateCourseRequest(
                    "MECATRONICA",
                    "Engenharia Mecatrônica",
                    null,
                    200,
                    10,
                    3600,
                    null,
                    null,
                    null,
                    institute.Id
                    )
                );
            Guard.Against.Null(course);
            courses.Add(course.Code, course);

            course = await InitializeCourse(
                service,
                new CreateCourseRequest(
                    "MECANICA",
                    "Engenharia Mecânica",
                    null,
                    200,
                    10,
                    3600,
                    null,
                    null,
                    null,
                    institute.Id
                    )
                );
            Guard.Against.Null(course);
            courses.Add(course.Code, course);

            course = await InitializeCourse(
                service,
                new CreateCourseRequest(
                    "ELETRICA",
                    "Engenharia Elétrica",
                    null,
                    200,
                    10,
                    3600,
                    null,
                    null,
                    null,
                    institute.Id
                    )
                );
            Guard.Against.Null(course);
            courses.Add(course.Code, course);
        }

        public static async Task InitializeClassTypeTable(IClassTypeService service)
        {
            if (holding is null)
                return;

            string[] codes = { "NORMAL", "ONLINE", "HIBRIDO", "TCC", "ESTAGIO", "UNDEFINED" };

            foreach (var s in codes)
            {
                ClassTypeModel? item;

                item = await InitializeClassType(
                        service,
                        new CreateClassTypeRequest(
                            s,
                            institutes["UNIT/SE"].Id
                            )
                        );
                Guard.Against.Null(item);

                classTypes.Add(s, item);
            }
        }

        public static async Task InitializeClassTable(IClassService service)
        {
            if (holding is null)
                return;

            ClassModel[] eletrica = new[] {
                new ClassModel { Code = "F113603", Name = "BASES MATEMÁTICAS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113611", Name = "QUÍMICA TECNOLÓGICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113620", Name = "FUNDAMENTOS DE PROGRAMAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113638", Name = "MODELAGEM E SOLUÇÃO DE P EM ENGENHARIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113646", Name = "MODELAGEM E SIMULAÇÃO TRIDIMENSIONAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H118840", Name = "METODOLOGIA CIENTÍFICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F106038", Name = "CIÊNCIA E TECNOLOGIA DOS MATERIAIS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113611", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F113654", Name = "CÁLCULO DIFERENCIAL E INTEGRAL I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113662", Name = "FENÔMENOS MECÂNICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113670", Name = "VETORES E GEOMETRIA ANALÍTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F113689", Name = "ESTRUTURA DE DADOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113620", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113697", Name = "PROJETO DE ENGENHARIA I", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113638", TipoRequisito.NATURAL),
                new ClassModel { Code = "H113465", Name = "FILOSOFIA E CIDADANIA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F104108", Name = "ESTATÍSTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.NATURAL),
                new ClassModel { Code = "F108472", Name = "ÁLGEBRA LINEAR", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113700", Name = "TERMOFLUIDO-DINÂMICA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113611", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F113719", Name = "FENÔMENOS ELETROMAGNÉTICOS E ONDAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113603", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114022", Name = "CÁLCULO DIFERENCIAL E INTEGRAL II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H113341", Name = "FUNDAMENTOS ANTROPOLÓGICOS E SOCIOLÓGICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F109509", Name = "ELETRÔNICA DIGITAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113719", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F109924", Name = "ANÁLISE DE CIRCUITOS I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F108472", TipoRequisito.NATURAL),
                new ClassModel { Code = "F110116", Name = "MATERIAIS ELÉTRICOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F106038", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113751", Name = "PROJETO DE ENGENHARIA II", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113697", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114030", Name = "EQUAÇÕES DIFERENCIAIS ORDINÁRIAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114740", Name = "CÁLCULO VETORIAL", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114022", TipoRequisito.NATURAL).Requisitos("F113670", TipoRequisito.NATURAL),
                new ClassModel { Code = "H114127", Name = "EMPREENDEDORISMO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F113760", Name = "ENGENHARIA DA QUALIDADE", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F106038", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114324", Name = "SINAIS E SISTEMAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114030", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114340", Name = "SISTEMAS DIGITAIS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109509", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114758", Name = "ANÁLISE DE CIRCUITOS II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109924", TipoRequisito.ESSENCIAL).Requisitos("F113670", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114766", Name = "ELETROMAGNETISMO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114740", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "OPT0001", Name = "OPTATIVA 1", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H119315", Name = "HISTÓRIA E CULTURA AFRO-BRASILEIRA E AFRICANA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H118815", Name = "RELAÇÕES ÉTNICOS - RACIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H121956", Name = "CRIATIVIDADE E INOVAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F109274", Name = "ELETRÔNICA ANALÓGICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109924", TipoRequisito.NATURAL),
                new ClassModel { Code = "F113808", Name = "PROJETO DE ENGENHARIA III", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113751", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114782", Name = "CONTROLE DE SISTEMAS LINEARES", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114030", TipoRequisito.ESSENCIAL).Requisitos("F108472", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114790", Name = "FUNDAMENTOS DE REDES DE COMUNICAÇÃO", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114324", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114804", Name = "MÁQUINAS ELÉTRICAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114766", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H119714", Name = "ANÁLISE ECONÔMICA DE INVESTIMENTOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("H114127", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F109312", Name = "ACIONAMENTOS ELÉTRICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL).Requisitos("F114804", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114812", Name = "INSTRUMENTAÇÃO ELETRÔNICA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109274", TipoRequisito.NATURAL).Requisitos("F109509", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114820", Name = "FUNDAMENTOS DE COMUNICAÇÕES", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114324", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114839", Name = "SISTEMAS ELÉTRICOS DE POTÊNCIA I", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109312", TipoRequisito.NATURAL).Requisitos("F114758", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114847", Name = "GERAÇÃO DE ENERGIA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL).Requisitos("F114804", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F104779", Name = "HIGIENE E SEGURANÇA DO TRABALHO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, }, // .Requisitos("F113700", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F110175", Name = "INSTALAÇÕES ELÉTRICAS PREDIAIS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114855", Name = "DISTRIBUIÇÃO DE ENERGIA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114863", Name = "SISTEMAS DE COMUNICAÇÕES", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114820", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114871", Name = "SISTEMAS ELÉTRICOS DE POTÊNCIA II", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114839", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F109436", Name = "ELETRÔNICA DE POTÊNCIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109274", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114472", Name = "GESTÃO DA MANUTENÇÃO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114898", Name = "INSTALAÇÕES ELÉTRICAS INDUSTRIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL).Requisitos("F114812", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114901", Name = "PROTEÇÃO CONTRA DESCARGAS ATMOSFÉRICAS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "H119242", Name = "GESTÃO DE PROJETOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "OPT0002", Name = "OPTATIVA 2", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H120003", Name = "DIREITO AMBIENTAL", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H122820", Name = "FORMAÇÃO CIDADÃ", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H113457", Name = "LIBRAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F112062", Name = "TRABALHO DE CONCLUSÃO DE CURSO", Theory = 0, Practice = 2, PR = 166, TypeId = classTypes["TCC"].Id, },
                new ClassModel { Code = "F112828", Name = "ESTÁGIO SUPERVISIONADO", Theory = 0, Practice = 8, PR = 148, TypeId = classTypes["ESTAGIO"].Id, },
                new ClassModel { Code = "F114910", Name = "QUALIDADE DE ENERGIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114758", TipoRequisito.NATURAL).Requisitos("F114871", TipoRequisito.DESEJAVEL),
                new ClassModel { Code = "F114928", Name = "EQUIPAMENTOS ELÉTRICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109436", TipoRequisito.NATURAL),
                new ClassModel { Code = "F114936", Name = "PROTEÇÃO DE SISTEMAS ELÉTRICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114871", TipoRequisito.NATURAL),
                new ClassModel { Code = "ELETIVA", Name = "DISCIPLINA ELETIVA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["UNDEFINED"].Id, },
            };

            ClassModel[] mecanica = new[] {
                new ClassModel { Code = "F113603", Name = "BASES MATEMÁTICAS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113611", Name = "QUÍMICA TECNOLÓGICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113620", Name = "FUNDAMENTOS DE PROGRAMAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113638", Name = "MODELAGEM E SOLUÇÃO DE P EM ENGENHARIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113646", Name = "MODELAGEM E SIMULAÇÃO TRIDIMENSIONAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H118840", Name = "METODOLOGIA CIENTÍFICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F106038", Name = "CIÊNCIA E TECNOLOGIA DOS MATERIAIS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113654", Name = "CÁLCULO DIFERENCIAL E INTEGRAL I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113662", Name = "FENÔMENOS MECÂNICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113670", Name = "VETORES E GEOMETRIA ANALÍTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113689", Name = "ESTRUTURA DE DADOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113697", Name = "PROJETO DE ENGENHARIA I", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H113465", Name = "FILOSOFIA E CIDADANIA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F104108", Name = "ESTATÍSTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F108472", Name = "ÁLGEBRA LINEAR", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113700", Name = "TERMOFLUIDO-DINÂMICA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113719", Name = "FENÔMENOS ELETROMAGNÉTICOS E ONDAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114022", Name = "CÁLCULO DIFERENCIAL E INTEGRAL II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H113341", Name = "FUNDAMENTOS ANTROPOLÓGICOS E SOCIOLÓGICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F107271", Name = "METROLOGIA INDUSTRIAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113751", Name = "PROJETO DE ENGENHARIA II", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114030", Name = "EQUAÇÕES DIFERENCIAIS ORDINÁRIAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114049", Name = "ISOSTÁTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113662", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114502", Name = "ENGENHARIA DOS MATERIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H114127", Name = "EMPREENDEDORISMO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F112909", Name = "RESISTÊNCIA DOS MATERIAIS I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113760", Name = "ENGENHARIA DA QUALIDADE", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113778", Name = "FENÔMENOS DE TRANSPORTE I", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114499", Name = "TECNOLOGIA MECÂNICA I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114510", Name = "MÁQUINAS ELÉTRICAS ROTATIVAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "OPT0001", Name = "OPTATIVA 1", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H119315", Name = "HISTÓRIA E CULTURA AFRO-BRASILEIRA E AFRICANA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H118815", Name = "RELAÇÕES ÉTNICOS - RACIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H121956", Name = "CRIATIVIDADE E INOVAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F106780", Name = "FENÔMENOS DE TRANSPORTE II", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F112917", Name = "RESISTÊNCIA DOS MATERIAIS II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F112909", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F113808", Name = "PROJETO DE ENGENHARIA III", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114529", Name = "TECNOLOGIA MECÂNICA II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114499", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H119714", Name = "ANÁLISE ECONÔMICA DE INVESTIMENTOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F112216", Name = "ELEMENTOS DE MÁQUINAS I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114081", Name = "TERMODINÂMICA I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114375", Name = "INSTRUMENTAÇÃO", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114421", Name = "ENSAIOS MECÂNICOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114537", Name = "COMANDOS ELÉTRICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114545", Name = "MÁQUINAS DE FLUXO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F106780", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F104779", Name = "HIGIENE E SEGURANÇA DO TRABALHO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F111996", Name = "CONTROLE E INSTRUMENTAÇÃO DE PROCESSOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F112372", Name = "INSTALAÇÕES INDUSTRIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114456", Name = "ACIONAMENTOS PNEUMÁTICOS E HIDRÁULICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114553", Name = "ELEMENTOS DE MÁQUINAS II", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F112216", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114570", Name = "TERMODINÂMICA APLICADA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114081", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F109460", Name = "SISTEMAS INTEGRADOS DE MANUFATURA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114472", Name = "GESTÃO DA MANUTENÇÃO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114588", Name = "MÁQUINAS TÉRMICAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114570", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H119242", Name = "GESTÃO DE PROJETOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "OPT0002", Name = "OPTATIVA 2", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H120003", Name = "DIREITO AMBIENTAL", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H122820", Name = "FORMAÇÃO CIDADÃ", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H113457", Name = "LIBRAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F112062", Name = "TRABALHO DE CONCLUSÃO DE CURSO", Theory = 0, Practice = 2, PR = 164, TypeId = classTypes["TCC"].Id, },
                new ClassModel { Code = "F112267", Name = "TECNOLOGIA DE COMANDO NUMÉRICO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F112828", Name = "ESTÁGIO SUPERVISIONADO", Theory = 0, Practice = 8, PR = 146, TypeId = classTypes["ESTAGIO"].Id, },
                new ClassModel { Code = "F114596", Name = "MECANISMOS E DINÂMICA DAS MÁQUINAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "ELETIVA", Name = "DISCIPLINA ELETIVA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
            };

            ClassModel[] mecatronica = new[] {
                new ClassModel { Code = "F113603", Name = "BASES MATEMÁTICAS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113611", Name = "QUÍMICA TECNOLÓGICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113620", Name = "FUNDAMENTOS DE PROGRAMAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113638", Name = "MODELAGEM E SOLUÇÃO DE P EM ENGENHARIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113646", Name = "MODELAGEM E SIMULAÇÃO TRIDIMENSIONAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H118840", Name = "METODOLOGIA CIENTÍFICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F106038", Name = "CIÊNCIA E TECNOLOGIA DOS MATERIAIS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113654", Name = "CÁLCULO DIFERENCIAL E INTEGRAL I", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113662", Name = "FENÔMENOS MECÂNICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113670", Name = "VETORES E GEOMETRIA ANALÍTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113689", Name = "ESTRUTURA DE DADOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113697", Name = "PROJETO DE ENGENHARIA I", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H113465", Name = "FILOSOFIA E CIDADANIA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F104108", Name = "ESTATÍSTICA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F108472", Name = "ÁLGEBRA LINEAR", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113700", Name = "TERMOFLUIDO-DINÂMICA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113719", Name = "FENÔMENOS ELETROMAGNÉTICOS E ONDAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114022", Name = "CÁLCULO DIFERENCIAL E INTEGRAL II", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H113341", Name = "FUNDAMENTOS ANTROPOLÓGICOS E SOCIOLÓGICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F107271", Name = "METROLOGIA INDUSTRIAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F109240", Name = "CIRCUITOS ELÉTRICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F109509", Name = "ELETRÔNICA DIGITAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113751", Name = "PROJETO DE ENGENHARIA II", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114030", Name = "EQUAÇÕES DIFERENCIAIS ORDINÁRIAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F113654", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "H114127", Name = "EMPREENDEDORISMO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F113760", Name = "ENGENHARIA DA QUALIDADE", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113778", Name = "FENÔMENOS DE TRANSPORTE I", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114324", Name = "SINAIS E SISTEMAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114332", Name = "PROCESSOS DE FABRICAÇÃO MECÂNICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114340", Name = "SISTEMAS DIGITAIS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109509", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "OPT0001", Name = "OPTATIVA 1", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H119315", Name = "HISTÓRIA E CULTURA AFRO-BRASILEIRA E AFRICANA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H118815", Name = "RELAÇÕES ÉTNICOS - RACIAIS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H121956", Name = "CRIATIVIDADE E INOVAÇÃO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F109274", Name = "ELETRÔNICA ANALÓGICA", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F113808", Name = "PROJETO DE ENGENHARIA III", Theory = 0, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114359", Name = "CONTROLE DE SISTEMAS DINÂMICOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114030", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114367", Name = "ROBÓTICA INDUSTRIAL", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H119714", Name = "ANÁLISE ECONÔMICA DE INVESTIMENTOS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114375", Name = "INSTRUMENTAÇÃO", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114383", Name = "CONTROLE DIGITAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F109509", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114391", Name = "MECÂNISMOS E ELEMENTOS DE MÁQUINAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114405", Name = "CONTROLE E SERVOMECANISMOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, }, // .Requisitos("F114359", TipoRequisito.ESSENCIAL),
                new ClassModel { Code = "F114413", Name = "ACIONAMENTOS E MÁQUINAS ELÉTRICAS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114421", Name = "ENSAIOS MECÂNICOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F104779", Name = "HIGIENE E SEGURANÇA DO TRABALHO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F110051", Name = "AUTOMAÇÃO INDUSTRIAL", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114430", Name = "MODELAGEM E SIMULAÇÃO DE SEDS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114448", Name = "SISTEMA EMBARCADOS E MICROCONTROLADORES", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114456", Name = "ACIONAMENTOS PNEUMÁTICOS E HIDRÁULICOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114464", Name = "CONTROLE PREDITIVO", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F107310", Name = "REDES E PROTOCOLOS INDUSTRIAIS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F109436", Name = "ELETRÔNICA DE POTÊNCIA", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F109460", Name = "SISTEMAS INTEGRADOS DE MANUFATURA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F114472", Name = "GESTÃO DA MANUTENÇÃO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "H119242", Name = "GESTÃO DE PROJETOS", Theory = 2, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "OPT0002", Name = "OPTATIVA 2", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H122820", Name = "FORMAÇÃO CIDADÃ", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H120003", Name = "DIREITO AMBIENTAL", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "H113457", Name = "LIBRAS", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["ONLINE"].Id, },
                new ClassModel { Code = "F112062", Name = "TRABALHO DE CONCLUSÃO DE CURSO", Theory = 0, Practice = 2, PR = 164, TypeId = classTypes["TCC"].Id, },
                new ClassModel { Code = "F112267", Name = "TECNOLOGIA DE COMANDO NUMÉRICO", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "F112828", Name = "ESTÁGIO SUPERVISIONADO", Theory = 0, Practice = 8, PR = 144, TypeId = classTypes["ESTAGIO"].Id, },
                new ClassModel { Code = "F115002", Name = "SISTEMAS SUPERVISÓRIOS", Theory = 2, Practice = 2, PR = 0, TypeId = classTypes["NORMAL"].Id, },
                new ClassModel { Code = "ELETIVA", Name = "DISCIPLINA ELETIVA", Theory = 4, Practice = 0, PR = 0, TypeId = classTypes["NORMAL"].Id, },
            };

            foreach (var item in eletrica)
                if (!classes.ContainsKey(item.Code))
                    classes.Add(item.Code, item);

            foreach (var item in mecanica)
                if (!classes.ContainsKey(item.Code))
                    classes.Add(item.Code, item);

            foreach (var item in mecatronica)
                if (!classes.ContainsKey(item.Code))
                    classes.Add(item.Code, item);

            foreach (var code in classes.Keys)
            {
                var cls = await InitializeClass(
                    service,
                    new CreateClassRequest(
                        classes[code].Code,
                        classes[code].Name,
                        classes[code].Practice,
                        classes[code].Theory,
                        classes[code].PR,
                        institutes["UNIT/SE"].Id,
                        classes[code].TypeId
                        )
                    );

                Guard.Against.Null(cls);
                classes[code].Id = cls.Id;
            }
        }

        public static async Task InitializePreceptorDegreeTable(IPreceptorDegreeService service)
        {
            if (holding is null)
                return;

            string[] codes = { "GRADUADO", "ESPECIALISTA", "MESTRE", "DOUTOR" };

            foreach (var s in codes)
            {
                PreceptorDegreeTypeModel? item;

                item = await InitializePreceptorDegreeType(
                        service,
                        new CreatePreceptorDegreeTypeRequest(
                            s,
                            institutes["UNIT/SE"].Id
                            )
                        );
                Guard.Against.Null(item);

                preceptorDegreeTypeModels.Add(s, item);
            }
        }

        public static async Task InitializePreceptorRegimeTable(IPreceptorRegimeService service)
        {
            if (holding is null)
                return;

            string[] codes = { "HORISTA", "PARCIAL", "INTEGRAL", "UNDEFINED" };

            foreach (var s in codes)
            {
                PreceptorRegimeTypeModel? item;

                item = await InitializePreceptorRegimeType(
                        service,
                        new CreatePreceptorRegimeTypeRequest(
                            s,
                            institutes["UNIT/SE"].Id
                            )
                        );
                Guard.Against.Null(item);

                preceptorRegimeTypeModels.Add(s, item);
            }
        }

        public static async Task InitializePreceptorTable(IPreceptorService service)
        {
            if (holding is null)
                return;

            PreceptorModel[] preceptors = new[] {
                new PreceptorModel { Code = "003654", Name = "RENAN TAVARES FIGUEIREDO", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "003885", Name = "SERGIO BEZERRA DE SANTANNA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "004363", Name = "ALVARO SILVA LIMA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "004492", Name = "ERICA DANTAS PEREIRA MELO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "004559", Name = "ALEX SANDRO BARRETO MELO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "004729", Name = "IGOR LIBERTADOR SILVA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "004732", Name = "EZIO CHRISTIAN DEDA DE ARAUJO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "004851", Name = "DAYSE ARAUJO LAPA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005031", Name = "ROOSEMAN DE OLIVEIRA SILVA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005271", Name = "CASSIUS GOMES DE OLIVEIRA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005375", Name = "ROGERIO FREIRE GRACA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005535", Name = "NELSON ANTONIO SA SANTOS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005604", Name = "MARCOS WANDIR NERY LOBAO", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "005685", Name = "HILTON PORTO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "005721", Name = "CLEIDE MARA FARIA SOARES", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "005788", Name = "FABIO ROCHA ARAGAO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "006338", Name = "ELIANE BEZERRA CAVALCANTI", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "006441", Name = "RICARDO SOARES MASCARELLO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "006448", Name = "SANDRO LUIS MEDEIROS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "006703", Name = "ARIONALDO RODRIGUES MENEZES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "006704", Name = "CLAUDIO BORBA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "006941", Name = "ELAYNE EMILIA SANTOS SOUZA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "007162", Name = "MURILO SANTOS LACERDA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "007183", Name = "MARIA CLARA GIACOMET", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "007188", Name = "CLEITON JOSE RODRIGUES DOS SANTOS", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "007192", Name = "JOSENITO OLIVEIRA SANTOS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "007259", Name = "ELTON FRANCESCHI", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "007227", Name = "KATLIN IVON BARRIOS EGUILUZ", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "007228", Name = "GIANCARLO RICHARD SALAZAR BANDA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "007980", Name = "EDILBERTO MARCELINO DA GAMA NETO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "008096", Name = "EDILIO JOSE SOARES LIMA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "008124", Name = "CLAUDIO DE OLIVEIRA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "008205", Name = "PEDRIANNE BARBOSA DE SOUZA DANTAS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "008800", Name = "MARIA NOGUEIRA MARQUES", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "008944", Name = "JOSE RICARDO MENEZES OLIVEIRA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "008945", Name = "INGRID CAVALCANTI FEITOSA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "009187", Name = "CLAUDIA SANTANA ARCIERI MIRANDA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "009188", Name = "DORA NEUZA LEAL DINIZ", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "009250", Name = "EDULO DE ARAUJO VALENCA LINS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "009256", Name = "MANUELA SOUZA LEITE ARAUJO", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "009702", Name = "DENISE DE JESUS SANTOS", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "010569", Name = "KLEBER ANDRADE SOUZA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "010816", Name = "CLARISSE DE ALMEIDA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011017", Name = "CLAUBERTO RODRIGUES DE OLIVEIRA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011016", Name = "THIAGO PEREZ MACHADO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011082", Name = "DOUGLAS DE MOURA ANDRADE", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011109", Name = "RODRIGO CARVALHO LACERDA DE OLIVEIRA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011139", Name = "LYGIA NUNES CARVALHO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011215", Name = "IGOR FARO DANTAS DE SANTANNA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011303", Name = "PAULO EDUARDO SILVA MARTINS", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "011523", Name = "GUSTAVO RODRIGUES BORGES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011695", Name = "GABRIEL MENDONCA FRANCO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011698", Name = "CARLOS GUSTAVO PEREIRA MORAES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011805", Name = "RICARDO PORTO SANTOS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "011810", Name = "RODRIGO MALTA DA SILVA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "011858", Name = "ANDREA QUARANTA BARBOSA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012196", Name = "ALEX VIANA VELOSO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012232", Name = "JOSAN CARVALHO DE FIGUEIREDO FILHO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012234", Name = "LUIZ GOMES DA CUNHA NETO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012236", Name = "DIEGO MELO COSTA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012243", Name = "NAYARA BEZERRA CARVALHO", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012249", Name = "RANYERE LUCENA DE SOUZA", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
                new PreceptorModel { Code = "012263", Name = "MAGNO RANGEL ALVES DOS REIS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012694", Name = "JAQUELINE NEVES MOREIRA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012709", Name = "CESAR GARCIA PAVAO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012713", Name = "SIMONE ALVES PRADO MENEZES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "012874", Name = "ELIABE VITORIA NASCIMENTO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013144", Name = "ANDERSON DA CONCEICAO SANTOS SOBRAL", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013181", Name = "RAQUEL ALVES CABRAL SILVA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013576", Name = "RUBENS DIEGO BARBOSA DE CARVALHO", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013624", Name = "RENATA CAMPOS ESCARIZ", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013680", Name = "ROSANY ALBUQUERQUE MATOS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "013768", Name = "DAMI DORIA NARAYANA DUARTE", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014194", Name = "GUSTAVO DE BRITO CARDOSO", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014196", Name = "LEONARDO RIBEIRO MAIA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014241", Name = "HELOISA DINIZ DE REZENDE", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014735", Name = "FELIPE SANTANA SANTOS", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014841", Name = "JOSE FERNANDES DE LIMA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014847", Name = "GESSYCA MENEZES COSTA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014934", Name = "ISABELA GONCALVES DE MENEZES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014935", Name = "DIEGO FARO ALVES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "014955", Name = "LIVIA DO VALE GREGORIN", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "015423", Name = "COLETTE DULCE DANTAS GOMES", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "016086", Name = "JULIANE APOLINARIO DA SILVA", DegreeTypeId = preceptorDegreeTypeModels["MESTRE"].Id, RegimeTypeId = preceptorRegimeTypeModels["HORISTA"].Id },
                new PreceptorModel { Code = "016589", Name = "ANDERSON ALLES DE JESUS", DegreeTypeId = preceptorDegreeTypeModels["DOUTOR"].Id, RegimeTypeId = preceptorRegimeTypeModels["INTEGRAL"].Id },
            };

            foreach (var preceptor in preceptors)
            {
                await InitializePreceptor(
                    service,
                    new CreatePreceptorRequest(
                        preceptor.Code,
                        preceptor.Name,
                        preceptor.Email,
                        preceptor.Image,
                        preceptor.DegreeTypeId,
                        preceptor.RegimeTypeId,
                        institutes["UNIT/SE"].Id
                        )
                    );
            }
        }

        public static async Task InitializeAxisTypeTable(IAxisTypeService service)
        {
            if (holding is null)
                return;

            string[] codes = { "BASICO", "PROFISSIONALISANTE", "ESPECIFICO", "INSTITUCIONAL", "UNDEFINED" };

            foreach (var s in codes)
            {
                AxisTypeModel? item;

                item = await InitializeAxisType(
                        service,
                        new CreateAxisTypeRequest(
                            s,
                            institutes["UNIT/SE"].Id
                            )
                        );
                Guard.Against.Null(item);

                axis.Add(s, item);
            }
        }

        public static async Task InitializeSyllabusTable(IComponentService service)
        {
            if (holding is null)
                return;

            Dictionary<string, ComponentModel> components = new()
            {
                { "F113603", new ComponentModel { Season = 0, Optative = false, ClassId = classes["F113603"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113611", new ComponentModel { Season = 0, Optative = false, ClassId = classes["F113611"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113620", new ComponentModel { Season = 0, Optative = false, ClassId = classes["F113620"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113638", new ComponentModel { Season = 0, Optative = false, ClassId = classes["F113638"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113646", new ComponentModel { Season = 0, Optative = false, ClassId = classes["F113646"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H118840", new ComponentModel { Season = 0, Optative = false, ClassId = classes["H118840"].Id, AxisTypeId = axis["INSTITUCIONAL"].Id }  },
                { "F106038", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F106038"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113654", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F113654"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113662", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F113662"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113670", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F113670"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113689", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F113689"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113697", new ComponentModel { Season = 1, Optative = false, ClassId = classes["F113697"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H113465", new ComponentModel { Season = 1, Optative = false, ClassId = classes["H113465"].Id, AxisTypeId = axis["INSTITUCIONAL"].Id } },
                { "F104108", new ComponentModel { Season = 2, Optative = false, ClassId = classes["F104108"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F108472", new ComponentModel { Season = 2, Optative = false, ClassId = classes["F108472"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113700", new ComponentModel { Season = 2, Optative = false, ClassId = classes["F113700"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F113719", new ComponentModel { Season = 2, Optative = false, ClassId = classes["F113719"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F114022", new ComponentModel { Season = 2, Optative = false, ClassId = classes["F114022"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H113341", new ComponentModel { Season = 2, Optative = false, ClassId = classes["H113341"].Id, AxisTypeId = axis["INSTITUCIONAL"].Id } },
                { "F107271", new ComponentModel { Season = 3, Optative = false, ClassId = classes["F107271"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F109240", new ComponentModel { Season = 3, Optative = false, ClassId = classes["F109240"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F109509", new ComponentModel { Season = 3, Optative = false, ClassId = classes["F109509"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F113751", new ComponentModel { Season = 3, Optative = false, ClassId = classes["F113751"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F114030", new ComponentModel { Season = 3, Optative = false, ClassId = classes["F114030"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H114127", new ComponentModel { Season = 3, Optative = false, ClassId = classes["H114127"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F113760", new ComponentModel { Season = 4, Optative = false, ClassId = classes["F113760"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F113778", new ComponentModel { Season = 4, Optative = false, ClassId = classes["F113778"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "F114324", new ComponentModel { Season = 4, Optative = false, ClassId = classes["F114324"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114332", new ComponentModel { Season = 4, Optative = false, ClassId = classes["F114332"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114340", new ComponentModel { Season = 4, Optative = false, ClassId = classes["F114340"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "OPT0001", new ComponentModel { Season = 4, Optative = false, ClassId = classes["OPT0001"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H119315", new ComponentModel { Season = 4, Optative = true, ClassId = classes["H119315"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "H118815", new ComponentModel { Season = 4, Optative = true, ClassId = classes["H118815"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "H121956", new ComponentModel { Season = 4, Optative = true, ClassId = classes["H121956"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "F109274", new ComponentModel { Season = 5, Optative = false, ClassId = classes["F109274"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F113808", new ComponentModel { Season = 5, Optative = false, ClassId = classes["F113808"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F114359", new ComponentModel { Season = 5, Optative = false, ClassId = classes["F114359"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114367", new ComponentModel { Season = 5, Optative = false, ClassId = classes["F114367"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "H119714", new ComponentModel { Season = 5, Optative = false, ClassId = classes["H119714"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F114375", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114375"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114383", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114383"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114391", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114391"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114405", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114405"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114413", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114413"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114421", new ComponentModel { Season = 6, Optative = false, ClassId = classes["F114421"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F104779", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F104779"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F110051", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F110051"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114430", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F114430"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114448", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F114448"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114456", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F114456"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114464", new ComponentModel { Season = 7, Optative = false, ClassId = classes["F114464"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F107310", new ComponentModel { Season = 8, Optative = false, ClassId = classes["F107310"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F109436", new ComponentModel { Season = 8, Optative = false, ClassId = classes["F109436"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F109460", new ComponentModel { Season = 8, Optative = false, ClassId = classes["F109460"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F114472", new ComponentModel { Season = 8, Optative = false, ClassId = classes["F114472"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "H119242", new ComponentModel { Season = 8, Optative = false, ClassId = classes["H119242"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "OPT0002", new ComponentModel { Season = 8, Optative = false, ClassId = classes["OPT0002"].Id, AxisTypeId = axis["BASICO"].Id } },
                { "H122820", new ComponentModel { Season = 8, Optative = true, ClassId = classes["H122820"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "H120003", new ComponentModel { Season = 8, Optative = true, ClassId = classes["H120003"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "H113457", new ComponentModel { Season = 8, Optative = true, ClassId = classes["H113457"].Id,  AxisTypeId = axis["BASICO"].Id } },
                { "F112062", new ComponentModel { Season = 9, Optative = false, ClassId = classes["F112062"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F112267", new ComponentModel { Season = 9, Optative = false, ClassId = classes["F112267"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "F112828", new ComponentModel { Season = 9, Optative = false, ClassId = classes["F112828"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id } },
                { "F115002", new ComponentModel { Season = 9, Optative = false, ClassId = classes["F115002"].Id, AxisTypeId = axis["ESPECIFICO"].Id } },
                { "ELETIVA", new ComponentModel { Season = 9, Optative = false, ClassId = classes["ELETIVA"].Id, AxisTypeId = axis["PROFISSIONALISANTE"].Id, } }
            };

            int curriculum = 2022;
            Guid courseId = courses["ELETRICA"].Id;

            foreach (var key in components.Keys)
            {
                await InitializeComponent(service, new CreateComponentRequest(
                    courseId,
                    curriculum,
                    components[key].Season,
                    components[key].ClassId,
                    components[key].AxisTypeId,
                    components[key].Optative
                    )
                );
            }
        }

        private static async Task<HoldingModel?> InitializeHolding(IHoldingService service, CreateHoldingRequest request)
        {
            var count = await service.GetHoldingCount();
            if (count > 0)
                return null;

            HoldingModel? holding;

            var result = await service.CreateHolding(request);

            if (result.IsSuccessStatusCode)
            {
                var s = await result.Content.ReadAsStringAsync();
                holding = JsonConvert.DeserializeObject<HoldingModel>(s);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                holding = await service.GetHoldingByCode("SET");
            }
            else
            {
                throw new Exception("Não foi possível criar ou ler a Holding 'SET'");
            }

            return holding;
        }
        private static async Task<InstituteModel?> InitializeInstitute(IInstituteService service, CreateInstituteRequest request)
        {
            var result = await service.CreateInstitute(request);
            InstituteModel? institute;

            if (result.IsSuccessStatusCode)
            {
                institute = JsonConvert.DeserializeObject<InstituteModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                institute = await service.GetInstituteByCode(request.Acronym);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o instituto '{request.Acronym}'.");
            }

            return institute;
        }
        private static async Task<CourseModel?> InitializeCourse(ICourseService service, CreateCourseRequest request)
        {
            var result = await service.CreateCourse(request);
            CourseModel? course;

            if (result.IsSuccessStatusCode)
            {
                course = JsonConvert.DeserializeObject<CourseModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                course = await service.GetCourseByCode(request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o curso '{request.Code}'.");
            }

            return course;
        }
        private static async Task<ClassTypeModel?> InitializeClassType(IClassTypeService service, CreateClassTypeRequest request)
        {
            ClassTypeModel? classType;

            var result = await service.CreateClassType(request);

            if (result.IsSuccessStatusCode)
            {
                classType = JsonConvert.DeserializeObject<ClassTypeModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                classType = await service.GetClassTypeByCode(request.InstituteId, request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o tipo de disciplina '{request.Code}'.");
            }

            return classType;
        }
        private static async Task<ClassModel?> InitializeClass(IClassService service, CreateClassRequest request)
        {
            ClassModel? cls;

            var result = await service.CreateClass(request);

            if (result.IsSuccessStatusCode)
            {
                var str = await result.Content.ReadAsStringAsync();
                cls = JsonConvert.DeserializeObject<ClassModel>(str);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                cls = await service.GetClassByCode(request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler a disciplina '{request.Code}'.");
            }

            return cls;
        }
        private static async Task<PreceptorDegreeTypeModel?> InitializePreceptorDegreeType(IPreceptorDegreeService service, CreatePreceptorDegreeTypeRequest request)
        {
            PreceptorDegreeTypeModel? degreeType;

            var result = await service.CreatePreceptorDegreeType(request);

            if (result.IsSuccessStatusCode)
            {
                degreeType = JsonConvert.DeserializeObject<PreceptorDegreeTypeModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                degreeType = await service.GetPreceptorDegreeTypeByCode(request.InstituteId, request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler a titulação  '{request.Code}'.");
            }

            return degreeType;
        }
        private static async Task<PreceptorRegimeTypeModel?> InitializePreceptorRegimeType(IPreceptorRegimeService service, CreatePreceptorRegimeTypeRequest request)
        {
            PreceptorRegimeTypeModel? regimeType;

            var result = await service.CreatePreceptorRegimeType(request);

            if (result.IsSuccessStatusCode)
            {
                regimeType = JsonConvert.DeserializeObject<PreceptorRegimeTypeModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                regimeType = await service.GetPreceptorRegimeTypeByCode(request.InstituteId, request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o regime de trabalho '{request.Code}'.");
            }

            return regimeType;
        }
        private static async Task<PreceptorModel?> InitializePreceptor(IPreceptorService service, CreatePreceptorRequest request)
        {
            PreceptorModel? preceptor;

            var result = await service.CreatePreceptor(request);

            if (result.IsSuccessStatusCode)
            {
                preceptor = JsonConvert.DeserializeObject<PreceptorModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                preceptor = await service.GetPreceptorByCode(request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler a disciplina '{request.Code}'.");
            }

            return preceptor;
        }
        private static async Task<AxisTypeModel?> InitializeAxisType(IAxisTypeService service, CreateAxisTypeRequest request)
        {
            AxisTypeModel? axisType;

            var result = await service.CreateAxisType(request);

            if (result.IsSuccessStatusCode)
            {
                axisType = JsonConvert.DeserializeObject<AxisTypeModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                axisType = await service.GetAxisTypeByCode(request.InstituteId, request.Code);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o eixo '{request.Code}'.");
            }

            return axisType;
        }
        private static async Task<ComponentModel?> InitializeComponent(IComponentService service, CreateComponentRequest request)
        {
            ComponentModel? component;

            var result = await service.CreateComponent(request);

            if (result.IsSuccessStatusCode)
            {
                component = JsonConvert.DeserializeObject<ComponentModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                component = await service.GetComponentByCourseAndCurriculumAndClass(request.CourseId, request.Curriculum, request.ClassId);
            }
            else
            {
                throw new Exception($"Não foi possível criar ou ler o componente curricular '{request.ClassId}'.");
            }

            return component;
        }
    }
}
