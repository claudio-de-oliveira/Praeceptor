using AbstractLL;

using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;

public class Scanner : AbstractScanner<PeaModel>
{
    private static readonly Dictionary<string, Token> reservedWords = new()
    {
        { "@PEA", Token.PEA },
        { "@UNIDADE1", Token.UNIDADE1 },
        { "@UNIDADE2", Token.UNIDADE2 },
        { "@CHAVE", Token.CHAVE },
        { "@BASICA", Token.BASICA },
        { "@COMPLEMENTAR", Token.COMPLEMENTAR },
        { "@AUTOR", Token.AUTOR },
        { "@TRADUTOR", Token.TRADUTOR },
        { "@ORGANIZADOR", Token.ORGANIZADOR },
        { "@EDITORA", Token.EDITORA },
        { "@VOLUME", Token.VOLUME },
        { "@EDICAO", Token.EDICAO },
        { "@ANO", Token.ANO },
        { "@TITULO", Token.TITULO },
        { "@SERIE", Token.SERIE },
        { "@DETALHE", Token.DETALHE },
        { "@DETALHES", Token.DETALHE },
        { "@EDITOR", Token.EDITOR },
        { "@EXEMPLARES", Token.EXEMPLARES },
        { "@NOTA", Token.NOTE },
        { "@ISBN", Token.ISBN },
    };

    public override AbstractToken NextToken(AbstractEnvironment<PeaModel> environment)
    {
        string lexema = "";
        int state = 0;
        char ch;

        if (environment.EndOfText)
            return Token.empty;

        while (true)
        {
            switch (state)
            {
                case 0:
                    ch = environment.NextChar();

                    if (char.IsWhiteSpace(ch))
                    {
                        if (environment.EndOfText)
                            return Token.empty;

                        state = 0;
                        break;
                    }
                    if (ch == '@')
                    {
                        lexema = "@";
                        state = 1;
                        break;
                    }
                    if (ch == '*')
                    {
                        state = 3;
                        break;
                    }
                    if (ch == '[')
                    {
                        state = 4;
                        break;
                    }
                    if (ch == ']')
                    {
                        state = 5;
                        break;
                    }
                    if (ch == '#')
                    {
                        state = 6;
                        break;
                    }
                    // TEXT
                    lexema = "" + ch;
                    state = 7;
                    break;

                case 1:
                    ch = environment.NextChar();

                    if (char.IsLetterOrDigit(ch))
                    {
                        lexema += ch;
                        state = 1;
                        break;
                    }
                    state = 2;
                    break;

                case 2:
                    environment.Retract();

                    lexema = lexema.ToUpper();
                    if (reservedWords.ContainsKey(lexema))
                        return reservedWords[lexema];
                    else
                        return new OutroToken(lexema);

                case 3:
                    return Token.STAR;

                case 4:
                    return Token.LCOL;

                case 5:
                    return Token.RCOL;

                case 6:
                    return Token.END;

                case 7:
                    ch = environment.NextChar();

                    if ("#[]".Contains(ch))
                    {
                        state = 8;
                        break;
                    }
                    lexema += ch;
                    state = 7;
                    break;

                case 8:
                    environment.Retract();
                    return new StringToken(lexema.Trim());

                case 99:
                    environment.Retract();

                    ch = environment.NextChar();

                    // Invalid Token
                    return new UnknowToken(ch);
            }
        }
    }
}