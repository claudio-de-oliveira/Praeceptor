using AbstractLL;

using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;

public class Parser : AbstractParser<PeaModel>
{
    private static readonly AbstractTAG[][] RHS = new AbstractTAG[][]
        {
        // 0. start ::= header @PeaId statement_list @Done #
        // {@Pea}
        new Tag[] { Tag.Header, Tag._PeaId, Tag.Statement_list, Tag._Done, Tag.ENDMARK, },
        // 1. Header = @Pea text
        // {@Pea}
        new Tag[] { Tag.PEA, Tag.Text, },
        // 2. statement_list ::= statement statement_list'
        // {@Outro, @Unidade1, @Unidade2, @Basica, @Complementar}
        new Tag[] { Tag.Statement, Tag.Statement_list_, },
        // 3. statement_list ::= .
        // {#}
        Array.Empty<Tag>(),

        // 4. statement_list' ::= statement statement_list'
        // {@Outro, @Unidade1, @Unidade2, @Basica, @Complementar}
        new Tag[] { Tag.Statement, Tag.Statement_list_, },
        // 5. statement_list' ::=
        // {#}
        Array.Empty<Tag>(),
        // 6. statement ::= @OUTRO text_list @Outro
        // {@Outro}
        new Tag[] { Tag.OUTRO, Tag.Text_list, Tag._Outro, },
        // 7. statement ::= @Unidade1 chave_list
        // {@Unidade1}
        new Tag[] { Tag.UNIDADE1, Tag.Chave_list, Tag._Unidade1, },
        // 8. statement ::= @Unidade2 chave_list
        // {@Unidade2}
        new Tag[] { Tag.UNIDADE2, Tag.Chave_list, Tag._Unidade2, },
        // 9. statement ::= bib
        // {@Basica, @Complementar}
        new Tag[] { Tag.Bib, },
        // 10. text_list ::= text @CreateList text_list'
        // {[}
        new Tag[] { Tag.Text, Tag._CreateList, Tag.Text_list_, },
        // 11. text_list ::= @EmptyList
        // {@Chave, @Outro, @Unidade1, @Unidade2, @Basica, @Complementar, #}
        new Tag[] { Tag._EmptyList, },

        // 12. text_list' ::= text @InsertList text_list'
        // {[}
        new Tag[] { Tag.Text, Tag._InsertList, Tag.Text_list_, },
        // 13. text_list' ::= @Echo
        // {]}
        new Tag[] { Tag._Echo, },
        // 14. text ::= [ str ]
        // {[}
        new Tag[] { Tag.LCOL, Tag.Str, Tag.RCOL, },

        // 15. bib ::= type online bib_items
        // {@Basica, @Complementar}
        new Tag[] { Tag.Type, Tag.Online, Tag.Bib_items, Tag._Bibliografia, },
        // 16. online ::= *	@Online
        // {*}
        new Tag[] { Tag.STAR, Tag._Online, },
        // 17. online ::= @NotOnline
        // {@Autor ... @ISBN}
        new Tag[] { Tag._NotOnline, },
        // 18. type ::= @Basica
        // {@Basica}
        new Tag[] { Tag.BASICA, Tag._Basica, },
        // 19. type ::= @Complementar
        // {@Complementar}
        new Tag[] { Tag.COMPLEMENTAR, Tag._Complementar, },
        // 20. bib_items ::= bib_item bib_items'
        // {@Autor ... @ISBN}
        new Tag[] { Tag.Bib_item, Tag.Bib_items_, },

        // 21. bib_items' ::= bib_item bib_items'
        // {@Autor ... @ISBN}
        new Tag[] { Tag.Bib_item, Tag.Bib_items_, },
        // 22. bib_items' ::= @Echo
        // {@Outro, @Unidade1, @Unidade2, @Basica, @Complementar, #}
        new Tag[] { Tag._Echo, },

        // 23. bib_item ::= @Autor text @AutorLastName text @AutorFirstName
        // {@Autor}
        new Tag[] { Tag.AUTOR, Tag.Text, Tag._AutorLastName, Tag.Text, Tag._AutorFirstName, },
        // 24. bib_item ::= @Tradutor text @TradutorLastName text @TradutorFirstName
        // {@Tradutor}
        new Tag[] { Tag.TRADUTOR, Tag.Text, Tag._TradutorLastName, Tag.Text, Tag._TradutorFirstName, },
        // 25. bib_item ::= @Organizador text @OrganizadorLastName text @OrganizadorFirstName
        // {@Organizador}
        new Tag[] { Tag.ORGANIZADOR, Tag.Text, Tag._OrganizadorLastName, Tag.Text, Tag._OrganizadorFirstName, },
        // 26. bib_item ::= @Titulo text @Titulo
        // {@Titulo}
        new Tag[] { Tag.TITULO, Tag.Text, Tag._Titulo, },
        // 27. bib_item ::= @Volume text @Volume complement @VolumeText
        // {@Volume}
        new Tag[] { Tag.VOLUME, Tag.Text, Tag._Volume, Tag.Complement, Tag._VolumeText, },
        // 28. bib_item ::= @Edicao text @Edicao
        // {@Edicao}
        new Tag[] { Tag.EDICAO, Tag.Text, Tag._Edicao, },
        // 29. bib_item ::= @Editora text @Editora complement @EditoraAddress
        // {@Editora}
        new Tag[] { Tag.EDITORA, Tag.Text, Tag._Editora, Tag.Complement, Tag._EditoraAddress, },
        // 30. bib_item ::= @Detalhe text @Detalhe
        // {@Detalhe}
        new Tag[] { Tag.DETALHE, Tag.Text, Tag._Detalhe, },
        // 31. bib_item ::= @Serie text @Serie
        // {@Serie}
        new Tag[] { Tag.SERIE, Tag.Text, Tag._Serie, },
        // 32. bib_item ::= @Ano text @Ano
        // {@Ano}
        new Tag[] { Tag.ANO, Tag.Text, Tag._Ano, },
        // 33. bib_item ::= @Editor text @Editor
        // {@Editor}
        new Tag[] { Tag.EDITOR, Tag.Text, Tag._Editor, },
        // 34. bib_item ::= @Exemplares text @Exemplares
        // {@Exemplares}
        new Tag[] { Tag.EXEMPLARES, Tag.Text, Tag._Exemplares, },
        // 35. bib_item ::= @Nota text @Nota
        // {@Nota}
        new Tag[] { Tag.NOTA, Tag.Text, Tag._Nota, },
        // 36. bib_item ::= @ISBN text @ISBN
        // {@ISBN}
        new Tag[] { Tag.ISBN, Tag.Text, Tag._ISBN, },
        // 37. complement ::= text
        // {[}
        new Tag[] { Tag.Text, },
        // 38. complement ::= @None
        // {@Autor ... @ISBN, @Outro, @Unidade1, @Unidade2, @Basica, @Complementar, #}
        new Tag[] { Tag._None, },
        // 39. chave_list ::= chave @CreateList chave_list'
        // {@Chave}
        new Tag[] { Tag.Chave, Tag._CreateList, Tag.Chave_list_, },

        // 40. chave_list ::= @EmptyList
        // {@Outro, @Unidade1, @Unidade2, @Basica, @Complementar, #}
        new Tag[] { Tag._EmptyList, },

        // 41. chave_list' ::= chave @InsertList chave_list'
        // {@Chave}
        new Tag[] { Tag.Chave, Tag._InsertList, Tag.Chave_list_, },
        // 42. chave_list' ::= @Echo
        // {@Outro, @Unidade1, @Unidade2, @Basica, @Complementar, #}
        new Tag[] { Tag._Echo, },
        // 43. chave ::= @Chave text @KeyChave text_list @Chave
        // {\Chave}
        new Tag[] { Tag.CHAVE, Tag.Text, Tag._KeyChave, Tag.Text_list, Tag._Chave, },
        // 44. str ::= string
        // {string}
        new Tag[] { Tag.STRING, Tag._String, },
        // 45. str ::= @EmptyString
        // {]}
        new Tag[] { Tag._EmptyString, },
        };

    private static readonly int[][] M = new int[][]
        {
        new int[] {  0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2,  }, // Start
        new int[] {  1, -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Header
        new int[] { -1,  2,  2,  2,  2,  2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  3, -1,  }, // Statement_list
        new int[] { -1,  4,  4,  4,  4,  4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  5, -1,  }, // Statement_list'
        new int[] { -1,  6,  7,  8,  9,  9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -2, -1,  }, // Statement
        new int[] { -1, 11, 11, 11, 11, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 11, -1, -1, 10, -1, 11, -1,  }, // Text_list
        new int[] { -1, 13, 13, 13, 13, 13, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 13, -1, -1, 12, -2, 13, -1,  }, // Text_list'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 14, -2, -1, -1,  }, // Text
        new int[] { -1, -2, -2, -2, 15, 15, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2, -1, -1, -1, -1, -2, -1,  }, // Bib
        new int[] { -1, -1, -1, -1, -1, -1, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, -1, -1, 16, -1, -1, -1, -1,  }, // Online
        new int[] { -1, -1, -1, -1, 18, 19, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, -1, -1,  }, // Type
        new int[] { -1, -2, -2, -2, -2, -2, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, -1, -1, -1, -1, -1, -2, -1,  }, // Bib_list
        new int[] { -1, 22, 22, 22, 22, 22, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, -1, -1, -1, -1, -1, 22, -1,  }, // Bib_list'
        new int[] { -1, -2, -2, -2, -2, -2, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, -1, -1, -1, -1, -1, -2, -1,  }, // Bib_item
        new int[] { -1, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, -1, -1, -1, 37, -1, 38, -1,  }, // Complement
        new int[] { -1, 40, 40, 40, 40, 40, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 39, -1, -1, -1, -1, 40, -1,  }, // Chave_list
        new int[] { -1, 42, 42, 42, 42, 42, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 41, -1, -1, -1, -1, 42, -1,  }, // Chave_list'
        new int[] { -1, -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 43, -1, -1, -1, -1, -2, -1,  }, // Chave
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 44, -1, -1, 45, -1, -1,  }, // Str
        };

    protected override AbstractTAG EndMark => Tag.ENDMARK;

    protected override AbstractTAG EmptyTag => Tag.empty;

    public Parser()
        : base(RHS, M, new Scanner(), new Semantic(), new PeaEnvironment())
    {
    }

    protected override void SaveAttributes(Stack<AbstractTAG> stk, AbstractTAG A, int p)
    {
        switch (p)
        {
            // 5. statement_list' ::= @Echo
            case 5:
                break;
            // 12. text_list' ::= text @InsertList text_list'
            case 12:
                stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                break;
            // 13. text_list' ::= @Echo
            case 13:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
            // 16. online ::= *	@Online
            case 16:
                stk.ToArray()[1].SetAttribute(0, A.GetAttribute(0));
                break;
            // 17. online ::= @NotOnline
            case 17:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
            // 20. bib_items ::= bib_item bib_items'
            case 20:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
            // 21. bib_items' ::= bib_item bib_items'
            case 21:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
            // 22. bib_list' ::= @Echo
            case 22:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
            // 23. bib_item ::= @Autor text @AutorLastName text @AutorFirstName
            case 23:
                stk.ToArray()[4].SetAttribute(2, A.GetAttribute(0));
                break;
            // 24. bib_item ::= @Tradutor text @TradutorLastName text @TradutorFirstName
            case 24:
                stk.ToArray()[4].SetAttribute(2, A.GetAttribute(0));
                break;
            // 25. bib_item ::= @Organizador text @OrganizadorLastName text @OrganizadorFirstName
            case 25:
                stk.ToArray()[4].SetAttribute(2, A.GetAttribute(0));
                break;
            // 26. bib_item ::= @Titulo text @Titulo
            case 26:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 27. bib_item ::= @Volume text @Volume complement @VolumeText
            case 27:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 28. bib_item ::= @Edicao text @Edicao
            case 28:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 29. bib_item ::= @Editora text @Editora complement @EditoraAddress
            case 29:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 30. bib_item ::= @Detalhe text @Detalhe
            case 30:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 31. bib_item ::= @Serie text @Serie
            case 31:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 32. bib_item ::= @Ano text @Ano
            case 32:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 33. bib_item ::= @Editor text @Editor
            case 33:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 34. bib_item ::= @Exemplares text @Exemplares
            case 34:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 35. bib_item ::= @Nota text @Nota
            case 35:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 36. bib_item ::= @ISBN text @ISBN
            case 36:
                stk.ToArray()[2].SetAttribute(2, A.GetAttribute(0));
                break;
            // 41. chave_list' ::= chave @InsertList chave_list'
            case 41:
                stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                break;
            // 42. chave_list' ::= @Echo
            case 42:
                stk.Peek().SetAttribute(0, A.GetAttribute(0));
                break;
        }
    }
}