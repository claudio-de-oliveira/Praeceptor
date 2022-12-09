using AbstractLL;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC
{
    public class Token : AbstractToken
    {
        public static readonly Token
            PART = new(Tag.PART),
            CHAPTER = new(Tag.CHAPTER),
            SECTION = new(Tag.SECTION),
            SUBSECTION = new(Tag.SUBSECTION),
            SUBSUBSECTION = new(Tag.SUBSUBSECTION),
            PARAGRAPH = new(Tag.PARAGRAPH),
            INCLUDETABLE = new(Tag.INCLUDETABLE),
            INCLUDEGRAPHICS = new(Tag.INCLUDEGRAPHICS),
            CAPTION = new(Tag.CAPTION),
            LABEL = new(Tag.LABEL),
            ROW = new(Tag.ROW),
            CELL = new(Tag.CELL),
            COMMENT = new(Tag.COMMENT),
            VARIABLE = new(Tag.VARIABLE),
            FOOTNOTE = new(Tag.FOOTNOTE),
            REF = new(Tag.REF),
            PAGEREF = new(Tag.PAGEREF),
            SQRT = new(Tag.SQRT),
            BEGIN = new(Tag.BEGIN),
            END = new(Tag.END),
            TABLE = new(Tag.TABLE),
            FIGURE = new(Tag.FIGURE),
            EQUATION = new(Tag.EQUATION),
            DEFAULT = new(Tag.DEFAULT),
            CLEARPAGE = new(Tag.CLEARPAGE),
            CODE = new(Tag.CODE),
            EQUAL = new(Tag.EQUAL),
            SEMICOLON = new(Tag.SEMICOLON),
            PLUS = new(Tag.PLUS),
            MINUS = new(Tag.MINUS),
            STAR = new(Tag.STAR),
            BAR = new(Tag.BAR),
            LBRA = new(Tag.LBRA),
            RBRA = new(Tag.RBRA),
            LCOL = new(Tag.LCOL),
            RCOL = new(Tag.RCOL),
            LPAR = new(Tag.LPAR),
            RPAR = new(Tag.RPAR),
            RUN = new(Tag.RUN),
            VERBATIM = new(Tag.VERBATIM),
            ENDMARK = new(Tag.ENDMARK),
            EMPTY = new(Tag.empty),
            NEWLINE = new(Tag.newline);

        public Token(AbstractTAG tag)
            : base(tag)
        { /* Nothing more todo */ }

        public override string ToString() => $"{GetTag()}";
    }

    // Implemente aqui os tokens COM informação complementar
    // ----------------------------------------------------
    internal class TextToken : Token
    {
        public string val;

        public TextToken(string val) : base(Tag.TEXT)
        {
            this.val = val;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{val}";
    }

    internal class StringToken : Token
    {
        public string str;

        public StringToken(string str) : base(Tag.STRING)
        {
            this.str = str;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{str}";
    }

    internal class IntegerToken : Token
    {
        public int val;

        public IntegerToken(int val) : base(Tag.INTEGER)
        {
            this.val = val;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{val}";
    }

    internal class RealToken : Token
    {
        public double val;

        public RealToken(double val) : base(Tag.REAL)
        {
            this.val = val;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{val}";
    }

    internal class UnknowToken : Token
    {
        public string str;

        public UnknowToken(string str) : base(Tag.unknow)
        {
            this.str = str;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{str}";
    }
}