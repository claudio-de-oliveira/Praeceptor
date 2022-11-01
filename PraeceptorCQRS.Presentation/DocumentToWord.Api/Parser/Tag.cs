using AbstractLL;

namespace DocumentToWord.Api.Parser
{
    public class Tag : AbstractTAG
    {
        public static readonly Tag
            Start = new(NONTERMINAL | 0, "Start"),
            ElementList = new(NONTERMINAL | 1, "ElementList"),
            ElementList_ = new(NONTERMINAL | 2, "ElementList'"),
            Element = new(NONTERMINAL | 3, "Element"),
            SimpleElement = new(NONTERMINAL | 4, "SimpleElement"),
            SimpleElementList = new(NONTERMINAL | 5, "SimpleElementList"),
            SimpleElementList_ = new(NONTERMINAL | 6, "SimpleElementList'", 1),
            Table = new(NONTERMINAL | 7, "Table", 1),
            Figure = new(NONTERMINAL | 8, "Figure", 1),
            Caption = new(NONTERMINAL | 9, "Caption", 2),
            Label = new(NONTERMINAL | 10, "Label"),
            RowList = new(NONTERMINAL | 11, "RowList"),
            RowList_ = new(NONTERMINAL | 12, "RowList'", 1),
            Row = new(NONTERMINAL | 13, "Row"),
            CellList = new(NONTERMINAL | 14, "CellList"),
            CellList_ = new(NONTERMINAL | 15, "CellList'", 1),
            Cell = new(NONTERMINAL | 16, "Cell"),
            CellElementList = new(NONTERMINAL | 17, "CellElementList"),
            CellElementList_ = new(NONTERMINAL | 18, "CellElementList'", 1),
            CellElement = new(NONTERMINAL | 19, "CellElement"),
            RunList = new(NONTERMINAL | 20, "RunList"),
            RunList_ = new(NONTERMINAL | 21, "RunList'", 1),
            Run = new(NONTERMINAL | 22, "Run"),
            ParameterSection = new(NONTERMINAL | 23, "ParameterSection"),
            ParameterList = new(NONTERMINAL | 24, "ParameterList"),
            ParameterList_ = new(NONTERMINAL | 25, "ParameterList'", 1),
            Parameter = new(NONTERMINAL | 26, "Parameter"),
            ParameterValue = new(NONTERMINAL | 27, "ParameterValue", 1),
            RValue = new(NONTERMINAL | 28, "RValue"),
            Equation = new(NONTERMINAL | 29, "Equation"),
            Assign = new(NONTERMINAL | 30, "Assign"),
            Expression = new(NONTERMINAL | 31, "Expression"),
            Expression_ = new(NONTERMINAL | 32, "Expression'"),
            Term = new(NONTERMINAL | 33, "Term"),
            Term_ = new(NONTERMINAL | 34, "Term'"),
            Unary = new(NONTERMINAL | 35, "Unary"),
            Factor = new(NONTERMINAL | 36, "Factor")
            ;

        public static readonly Tag
            PART = new(TERMINAL | 0, "\\part"),
            CHAPTER = new(TERMINAL | 1, "\\chapter"),
            SECTION = new(TERMINAL | 2, "\\section"),
            SUBSECTION = new(TERMINAL | 3, "\\subsection"),
            SUBSUBSECTION = new(TERMINAL | 4, "\\subsubsection"),
            PARAGRAPH = new(TERMINAL | 5, "\\paragraph"),
            INCLUDETABLE = new(TERMINAL | 6, "\\includetable"),
            INCLUDEGRAPHICS = new(TERMINAL | 7, "\\includegraphics"),
            CAPTION = new(TERMINAL | 8, "\\caption"),
            LABEL = new(TERMINAL | 9, "\\label"),
            ROW = new(TERMINAL | 10, "\\row"),
            CELL = new(TERMINAL | 11, "\\cell"),
            COMMENT = new(TERMINAL | 12, "\\comment"),
            VARIABLE = new(TERMINAL | 13, "\\variable"),
            RUN = new(TERMINAL | 14, "\\run"),
            FOOTNOTE = new(TERMINAL | 15, "\\footnote"),
            REF = new(TERMINAL | 16, "\\ref"),
            PAGEREF = new(TERMINAL | 17, "\\pageref"),
            SQRT = new(TERMINAL | 18, "\\sqrt"),
            BEGIN = new(TERMINAL | 19, "\\begin"),
            END = new(TERMINAL | 20, "\\end"),
            DEFAULT = new(TERMINAL | 21, "\\default"),
            CLEARPAGE = new(TERMINAL | 22, "\\clearpage"),
            CODE = new(TERMINAL | 23, "\\code"),
            VERBATIM = new(TERMINAL | 24, "\\verbatim"),
            TABLE = new(TERMINAL | 25, "table"),
            FIGURE = new(TERMINAL | 26, "figure"),
            EQUATION = new(TERMINAL | 27, "equation"),
            TEXT = new(TERMINAL | 28, "text"),
            STRING = new(TERMINAL | 29, "string"),
            INTEGER = new(TERMINAL | 30, "integer"),
            REAL = new(TERMINAL | 31, "real"),
            EQUAL = new(TERMINAL | 32, "="),
            SEMICOLON = new(TERMINAL | 33, ";"),
            PLUS = new(TERMINAL | 34, "+"),
            MINUS = new(TERMINAL | 35, "-"),
            STAR = new(TERMINAL | 36, "*"),
            BAR = new(TERMINAL | 37, "/"),
            LBRA = new(TERMINAL | 38, "{"),
            RBRA = new(TERMINAL | 39, "}"),
            LCOL = new(TERMINAL | 40, "["),
            RCOL = new(TERMINAL | 41, "]"),
            LPAR = new(TERMINAL | 42, "("),
            RPAR = new(TERMINAL | 43, ")"),
            ENDMARK = new(TERMINAL | 44, "#"),

            empty = new(TERMINAL | 45, "empty"),
            newline = new(TERMINAL | 46, "\n")
            ;

        public static readonly Tag
            unknow = new(TERMINAL | 46, "Unknow");


        public static readonly Tag
            _BeginRun = new(SEMANTIC | 0, "@BeginRun"),
            _EndRun = new(SEMANTIC | 1, "@EndRun", 1),
            _BeginString = new(SEMANTIC | 2, "@BeginString"),
            _EndString = new(SEMANTIC | 3, "@EndString"),
            _BeginParameter = new(SEMANTIC | 4, "@BeginParameter"),
            _EndParameter = new(SEMANTIC | 5, "@EndParameter"),
            _IncludeTable = new(SEMANTIC | 6, "@IncludeTable", 1),
            _Part = new(SEMANTIC | 7, "@Part"),
            _Chapter = new(SEMANTIC | 8, "@Chapter", 2),
            _Section = new(SEMANTIC | 9, "@Section", 2),
            _SubSection = new(SEMANTIC | 10, "@SubSection", 2),
            _SubSubSection = new(SEMANTIC | 11, "@SubSubSection", 2),
            _CreateList = new(SEMANTIC | 12, "@CreateList", 1),
            _InsertList = new(SEMANTIC | 13, "@InsertList", 2),
            _CommentStart = new(SEMANTIC | 14, "@CommentStart", 1),
            _CommentEnd = new(SEMANTIC | 15, "@CommentEnd", 3),
            _Text = new(SEMANTIC | 16, "@Text"),
            _NoCaption = new(SEMANTIC | 17, "@NoCaption"),
            _NoLabel = new(SEMANTIC | 18, "@NoLabel"),
            _Variable = new(SEMANTIC | 19, "@Variable"),
            _DefaultValue = new(SEMANTIC | 20, "@DefaultValue", 1),
            _EndParagraph = new(SEMANTIC | 21, "@EndParagraph", 2),
            _NoParameters = new(SEMANTIC | 22, "@NoParameters"),
            _ParName = new(SEMANTIC | 24, "@ParName"),
            _ParValue = new(SEMANTIC | 25, "@ParValue", 2),
            _Integer = new(SEMANTIC | 26, "@Integer"),
            _Real = new(SEMANTIC | 27, "@Real"),
            _String = new(SEMANTIC | 28, "@String"),
            _Parameters = new(SEMANTIC | 29, "@Parameters", 1),
            _ParagraphParameter = new(SEMANTIC | 30, "@ParagraphParameter", 1),
            _FigureParameter = new(SEMANTIC | 31, "@FigureParameter", 1),
            _GraphicsParameter = new(SEMANTIC | 32, "@GraphicsParameter", 1),
            _RowParameter = new(SEMANTIC | 33, "@RowParameter", 1),
            _CellParameter = new(SEMANTIC | 34, "@CellParameter", 1),
            _EquationParameter = new(SEMANTIC | 35, "@EquationParameter", 1),
            _RunParameter = new(SEMANTIC | 36, "@RunParameter", 1),
            _Footnote = new(SEMANTIC | 37, "@Footnote", 2),
            _Run = new(SEMANTIC | 38, "@Run", 2),
            _Cell = new(SEMANTIC | 39, "@Cell", 2),
            _Row = new(SEMANTIC | 40, "@Row", 2),
            _Caption = new(SEMANTIC | 41, "@Caption", 4),
            _HeaderParameter = new(SEMANTIC | 42, "@HeaderParameter", 1),
            _TableParameter = new(SEMANTIC | 43, "@TableParameter", 1),
            _Label = new(SEMANTIC | 45, "@Label"),
            _StartFootnote = new(SEMANTIC | 46, "@StartFootnote"),
            _BeginEnvironment = new(SEMANTIC | 47, "@BeginEnvironment"),
            _EndEnvironment = new(SEMANTIC | 48, "@EndEnvironment", 1),
            _Ref = new(SEMANTIC | 49, "@Ref"),
            _PageRef = new(SEMANTIC | 50, "@PageRef"),
            _Path = new(SEMANTIC | 51, "@Path"),
            _Includegraphics = new(SEMANTIC | 52, "@Includegraphics", 3),
            _BeginPath = new(SEMANTIC | 53, "@BeginPath"),
            _EndPath = new(SEMANTIC | 54, "@EndPath"),
            _CaptionParameters = new(SEMANTIC | 55, "@CaptionParameters", 1),
            _CreateDictionary = new(SEMANTIC | 56, "@CreateDictionary", 1),
            _InsertDictionary = new(SEMANTIC | 57, "@InsertDictionary", 2),
            _CellList = new(SEMANTIC | 58, "@CellList", 1),
            _Table = new(SEMANTIC | 59, "@Table", 2),
            _EndBegin = new(SEMANTIC | 60, "@EndBegin", 1),
            _EndTable = new(SEMANTIC | 61, "@EndTable", 3),
            _EndFigure = new(SEMANTIC | 62, "@EndFigure", 3),
            _EndEquation = new(SEMANTIC | 63, "@EndEquation", 3),
            _CellEndParagraph = new(SEMANTIC | 64, "@CellEndParagraph", 2),
            _CellEndBegin = new(SEMANTIC | 65, "@CellEndBegin", 1),
            _CellEndTable = new(SEMANTIC | 66, "@CellEndTable", 3),
            _CellEndFigure = new(SEMANTIC | 67, "@CellEndFigure", 3),
            _CellEndEquation = new(SEMANTIC | 68, "@CellEndEquation", 3),
            _CellElement = new(SEMANTIC | 69, "@CellElement", 1),
            _EnvironmentParameter = new(SEMANTIC | 70, "@EnvironmentParameter", 1),
            _Ignore = new(SEMANTIC | 71, "@Ignore", 1),
            _Skip7 = new(SEMANTIC | 72, "@Skip7", 1),
            _ToBody = new(SEMANTIC | 73, "@ToBody", 1),
            _DefaultParameter = new(SEMANTIC | 74, "@DefaultParameter", 1),
            _SetDefaultParameters = new(SEMANTIC | 75, "@SetDefaultParameters", 1),
            _EmptyList = new(SEMANTIC | 76, "@EmptyList"),
            _Clearpage = new(SEMANTIC | 77, "@Clearpage"),
            _Code = new(SEMANTIC | 78, "@Code", 1),
            _BeginCode = new(SEMANTIC | 79, "@BeginCode"),
            _EndCode = new(SEMANTIC | 80, "@EndCode", 1),
            _Verbatim = new(SEMANTIC | 81, "@Verbatim"),
            _BeginVerbatim = new(SEMANTIC | 82, "@BeginVerbatim"),
            _EndVerbatim = new(SEMANTIC | 83, "@EndVerbatim", 1),
            _Initialize = new(SEMANTIC | 85, "@Initialize"),
            _CreateRunList = new(SEMANTIC | 86, "@CreateRunList", 1),
            _InsertRunList = new(SEMANTIC | 87, "@InsertRunList", 2),
            // 
            _Echo = new(SEMANTIC | 99, "@Echo", 1),
            _Done = new(SEMANTIC | 100, "@Done")
            ;

        /// <summary>
        /// Invoca o construtor da class básica AbstractTAG. 
        /// Todos os símbolos devem ser definidos em tempo de compilação.
        /// </summary>
        /// <param name="tag">Utilizado para indexação das tabelas de parsing</param>
        /// <param name="name">Informação para auxiliar a depuração</param>
        /// <param name="nattribs">Número de atributos associados ao símbolo</param>
        protected Tag(int Tag, string name, int nattribs = 0)
            : base(Tag, name, nattribs)
        { /* Nothing more todo */ }

        /// <summary>
        /// Cria uma cópia de um símbolo da gramática
        /// </summary>
        /// <returns>O próprio símbolo, caso ele não tenha atributos, ou uma cópia com novos atributos</returns>
        public override AbstractTAG Clone()
        {
            if (_inherited.Length > 0)
                return new Tag(_tag, _name, _inherited.Length);
            return this;
        }
    }
}
