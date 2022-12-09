using AbstractLL;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;

public class Tag : AbstractTAG
{
    protected Tag(int Tag, string name, int nattribs = 0)
        : base(Tag, name, nattribs)
    { /* Nothing more todo */ }

    #region N Ã O   T E R M I N A I S

    public static readonly Tag
        Start = new(NONTERMINAL | 0, "Start"),
        Header = new(NONTERMINAL | 1, "Header"),
        Statement_list = new(NONTERMINAL | 2, "Statement_list"),
        Statement_list_ = new(NONTERMINAL | 3, "Statement_list'", 1),
        Statement = new(NONTERMINAL | 4, "Statement"),
        Text_list = new(NONTERMINAL | 5, "Text_list"),
        Text_list_ = new(NONTERMINAL | 6, "Text_list'", 1),
        Text = new(NONTERMINAL | 7, "Text"),
        Bib = new(NONTERMINAL | 8, "Bib"),
        Online = new(NONTERMINAL | 9, "Online", 1),
        Type = new(NONTERMINAL | 10, "Type"),
        Bib_items = new(NONTERMINAL | 11, "Bib_items", 1),
        Bib_items_ = new(NONTERMINAL | 12, "Bib_items'", 1),
        Bib_item = new(NONTERMINAL | 13, "Bib_item", 1),
        Complement = new(NONTERMINAL | 14, "Complement", 1),
        Chave_list = new(NONTERMINAL | 15, "Chave_list"),
        Chave_list_ = new(NONTERMINAL | 16, "Chave_list'", 1),
        Chave = new(NONTERMINAL | 17, "Chave"),
        Str = new(NONTERMINAL | 18, "Str")
        ;

    #endregion N Ã O   T E R M I N A I S

    #region T E R M I N A I S

    public static readonly Tag
        PEA = new(TERMINAL | 0, "@Pea"),
        OUTRO = new(TERMINAL | 1, "@Outro"),
        UNIDADE1 = new(TERMINAL | 2, "@Unidade1"),
        UNIDADE2 = new(TERMINAL | 3, "@Unidade2"),
        BASICA = new(TERMINAL | 4, "@Basica"),
        COMPLEMENTAR = new(TERMINAL | 5, "@Complementar"),
        AUTOR = new(TERMINAL | 6, "@Autor"),
        TRADUTOR = new(TERMINAL | 7, "@Tradutor"),
        ORGANIZADOR = new(TERMINAL | 8, "@Organizador"),
        TITULO = new(TERMINAL | 9, "@Titulo"),
        VOLUME = new(TERMINAL | 10, "@Volume"),
        EDICAO = new(TERMINAL | 11, "@Edicao"),
        EDITORA = new(TERMINAL | 12, "@Editora"),
        DETALHE = new(TERMINAL | 13, "@Detalhe"),
        SERIE = new(TERMINAL | 14, "@Serie"),
        ANO = new(TERMINAL | 15, "@Ano"),
        EDITOR = new(TERMINAL | 16, "@Editor"),
        EXEMPLARES = new(TERMINAL | 17, "@Exemplares"),
        NOTA = new(TERMINAL | 18, "@Nota"),
        ISBN = new(TERMINAL | 19, "@ISBN"),
        CHAVE = new(TERMINAL | 20, "@Chave"),
        STRING = new(TERMINAL | 21, "string"),
        STAR = new(TERMINAL | 22, "*"),
        LCOL = new(TERMINAL | 23, "["),
        RCOL = new(TERMINAL | 24, "]"),
        ENDMARK = new(TERMINAL | 25, "#"),

        empty = new(TERMINAL | 26, "empty")
        ;

    public static readonly Tag
        unknow = new(TERMINAL | 27, "Unknow");

    #endregion T E R M I N A I S

    #region A Ç Õ E S   S E M Â N T I C A S

    public static readonly Tag
        _CreateList = new(SEMANTIC | 0, "@CreateList", 1),
        _InsertList = new(SEMANTIC | 1, "@InsertList", 2),
        _String = new(SEMANTIC | 2, "@String"),
        _EmptyString = new(SEMANTIC | 3, "@EmptyString"),
        _PeaId = new(SEMANTIC | 4, "@PeaId", 1),
        _Outro = new(SEMANTIC | 5, "@Outro", 1),
        _Unidade1 = new(SEMANTIC | 6, "@Unidade1", 1),
        _Unidade2 = new(SEMANTIC | 7, "@Unidade2", 1),
        _Online = new(SEMANTIC | 8, "@Online", 1),
        _NotOnline = new(SEMANTIC | 9, "@NotOnline", 1),
        _Basica = new(SEMANTIC | 10, "@Basica"),
        _Complementar = new(SEMANTIC | 11, "@Complementar"),
        _Bibliografia = new(SEMANTIC | 12, "@Bibliografia", 1),
        _KeyChave = new(SEMANTIC | 13, "@KeyChave", 1),
        _Chave = new(SEMANTIC | 14, "@Chave", 2),
        _AutorLastName = new(SEMANTIC | 15, "@AutorLastName", 1),
        _AutorFirstName = new(SEMANTIC | 16, "@AutorFirstName", 3),
        _TradutorLastName = new(SEMANTIC | 17, "@TradutorLastName", 1),
        _TradutorFirstName = new(SEMANTIC | 18, "@TradutorFirstName", 3),
        _OrganizadorLastName = new(SEMANTIC | 19, "@OrganizadorLastName", 1),
        _OrganizadorFirstName = new(SEMANTIC | 20, "@OrganizadorFirstName", 3),

        _Titulo = new(SEMANTIC | 21, "@Titulo", 3),
        _Volume = new(SEMANTIC | 22, "@Volume", 3),
        _VolumeText = new(SEMANTIC | 23, "@VolumeText", 3),
        _Edicao = new(SEMANTIC | 24, "@Edicao", 3),
        _Editora = new(SEMANTIC | 25, "@Editora", 3),
        _EditoraAddress = new(SEMANTIC | 26, "@EditoraAddress", 3),
        _Detalhe = new(SEMANTIC | 27, "@Detalhe", 3),
        _Serie = new(SEMANTIC | 28, "@Serie", 3),
        _Ano = new(SEMANTIC | 29, "@Ano", 3),
        _Editor = new(SEMANTIC | 30, "@Editor", 3),
        _Exemplares = new(SEMANTIC | 31, "@Exemplares", 3),
        _Nota = new(SEMANTIC | 32, "@Nota", 3),
        _ISBN = new(SEMANTIC | 33, "@ISBN", 3),
        _None = new(SEMANTIC | 34, "@None"),
        _EmptyList = new(SEMANTIC | 35, "@EmptyList"),

        _Echo = new(SEMANTIC | 99, "@Echo", 1),
        _Done = new(SEMANTIC | 100, "@Done", 2)
        ;

    #endregion A Ç Õ E S   S E M Â N T I C A S

    public override AbstractTAG Clone()
    {
        if (_inherited.Length > 0)
            return new Tag(_tag, _name, _inherited.Length);
        return this;
    }
}