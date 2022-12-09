using AbstractLL;

namespace PraeceptorCQRS.Infrastructure.Parsers.Pea;

public class Token : AbstractToken
{
    public Token(AbstractTAG tag)
        : base(tag)
    { /* Nothing more todo */ }

    public static readonly Token
        END = new(Tag.ENDMARK),
        PEA = new(Tag.PEA),
        UNIDADE1 = new(Tag.UNIDADE1),
        LCOL = new(Tag.LCOL),
        RCOL = new(Tag.RCOL),
        UNIDADE2 = new(Tag.UNIDADE2),
        STAR = new(Tag.STAR),
        BASICA = new(Tag.BASICA),
        COMPLEMENTAR = new(Tag.COMPLEMENTAR),
        AUTOR = new(Tag.AUTOR),
        TRADUTOR = new(Tag.TRADUTOR),
        ORGANIZADOR = new(Tag.ORGANIZADOR),
        TITULO = new(Tag.TITULO),
        EDICAO = new(Tag.EDICAO),
        DETALHE = new(Tag.DETALHE),
        SERIE = new(Tag.SERIE),
        ANO = new(Tag.ANO),
        EDITOR = new(Tag.EDITOR),
        EXEMPLARES = new(Tag.EXEMPLARES),
        NOTE = new(Tag.NOTA),
        ISBN = new(Tag.ISBN),
        EDITORA = new(Tag.EDITORA),
        VOLUME = new(Tag.VOLUME),
        CHAVE = new(Tag.CHAVE),
        empty = new(Tag.empty);

    public override string ToString() => $"<{GetTag()}>";
}

class StringToken : AbstractToken
{
    public string Value { get; private set; }

    public StringToken(string txt)
        : base(Tag.STRING)
    {
        Value = txt;
    }

    public override bool HasComplement() => true;

    public override string ToString() => $"{GetTag()}: {Value}";
}

class OutroToken : AbstractToken
{
    public string Value { get; private set; }

    public OutroToken(string txt)
        : base(Tag.OUTRO)
    {
        Value = txt;
    }

    public override bool HasComplement() => true;

    public override string ToString() => $"{GetTag()}: {Value}";
}

internal class UnknowToken : AbstractToken
{
    public char ch;

    public UnknowToken(char ch)
        : base(Tag.unknow)
    {
        this.ch = ch;
    }

    public override string ToString() => $"{GetTag()}: {ch}";
}
