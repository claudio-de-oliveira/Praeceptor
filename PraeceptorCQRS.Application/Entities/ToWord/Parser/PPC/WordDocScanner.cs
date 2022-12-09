using AbstractLL;

using PraeceptorCQRS.Application.Entities.ToWord.Models;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC;

public class WordDocScanner : AbstractScanner<WordDocument>
{
    private static readonly Dictionary<string, Token> reservedWords = new()
    {
        { @"\PART", Token.PART },
        { @"\CHAPTER", Token.CHAPTER },
        { @"\SECTION", Token.SECTION },
        { @"\SUBSECTION", Token.SUBSECTION },
        { @"\SUBSUBSECTION", Token.SUBSUBSECTION },
        { @"\PARAGRAPH", Token.PARAGRAPH },
        { @"\INCLUDETABLE", Token.INCLUDETABLE },
        { @"\INCLUDEGRAPHICS", Token.INCLUDEGRAPHICS },
        { @"\CAPTION", Token.CAPTION },
        { @"\LABEL", Token.LABEL },
        { @"\ROW", Token.ROW },
        { @"\CELL", Token.CELL },
        { @"\COMMENT", Token.COMMENT },
        { @"\VARIABLE", Token.VARIABLE },
        { @"\FOOTNOTE", Token.FOOTNOTE },
        { @"\REF", Token.REF },
        { @"\PAGEREF", Token.PAGEREF },
        { @"\SQRT", Token.SQRT },
        { @"\BEGIN", Token.BEGIN },
        { @"\END", Token.END },
        { @"\TABLE", Token.TABLE },
        { @"\FIGURE", Token.FIGURE },
        { @"\EQUATION", Token.EQUATION },
        { @"\RUN", Token.RUN },
        { @"\DEFAULT", Token.DEFAULT },
        { @"\CLEARPAGE", Token.CLEARPAGE },
        { @"\CODE", Token.CODE },
        { @"\VERBATIM", Token.VERBATIM },
    };

    private AbstractToken NextString(AbstractEnvironment<WordDocument> environment)
    {
        string lexema = "";
        int state = 0;
        char ch = '?';

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (char.IsWhiteSpace(ch))
                    {
                        state = 0;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 202;
                        break;
                    }
                    if ("[]{=".Contains("" + ch)) // Caracteres que não podem estar dentros dos strings
                    {
                        state = 205;
                        break;
                    }

                    lexema = "" + ch;
                    state = 200;
                    break;

                case 100:
                    ch = environment.NextChar();

                    lexema += ch;
                    state = 200;
                    break;

                case 200:
                    ch = environment.NextChar();

                    if ("[]{=".Contains("" + ch)) // Caracteres que não podem estar dentros dos strings
                    {
                        state = 205;
                        break;
                    }
                    if (char.IsWhiteSpace(ch))
                    {
                        state = 201;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 201;
                        break;
                    }

                    if (ch == '\\')
                    {
                        state = 100;
                        break;
                    }

                    lexema += ch;
                    state = 200;
                    break;

                case 201:
                    environment.Retract();

                    return new StringToken(lexema.Trim());

                case 202:
                    return Token.RBRA;

                case 205:
                    environment.Retract();
                    return new UnknowToken("" + ch);
            }
        }
    }

    private AbstractToken NextPath(AbstractEnvironment<WordDocument> environment)
    {
        string lexema = "";
        int state = 0;
        char ch = '?';

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (char.IsWhiteSpace(ch))
                    {
                        state = 0;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 202;
                        break;
                    }

                    lexema = "" + ch;
                    state = 200;
                    break;

                case 200:
                    ch = environment.NextChar();

                    if (ch == '}')
                    {
                        state = 201;
                        break;
                    }
                    lexema += ch;
                    state = 200;
                    break;

                case 201:
                    environment.Retract();
                    return new StringToken(lexema.Trim());

                case 202:
                    return Token.RBRA;

                case 205:
                    // Invalid Number Format
                    environment.Retract();
                    return new UnknowToken("" + ch);
            }
        }
    }

    private AbstractToken NextParameter(AbstractEnvironment<WordDocument> environment)
    {
        string lexema = "";
        double number = 0.0;
        double divisor = 10.0;
        char ch = '?';
        int signal = 1;

        int state = 0;

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (char.IsWhiteSpace(ch))
                    {
                        state = 0;
                        break;
                    }
                    if (char.IsLetter(ch))
                    {
                        lexema = "" + ch;
                        state = 201;
                        break;
                    }
                    if (ch == '=')
                    {
                        state = 200;
                        break;
                    }
                    if (ch == ']')
                    {
                        state = 203;
                        break;
                    }
                    if (char.IsDigit(ch))
                    {
                        number = ch - '0';
                        state = 300;
                        break;
                    }
                    if (ch == '-')
                    {
                        signal = -1;
                        number = 0;
                        state = 299;
                        break;
                    }
                    if (ch == ';')
                    {
                        state = 306;
                        break;
                    }

                    state = 20;
                    break;

                case 200:
                    return Token.EQUAL;

                case 201:
                    ch = environment.NextChar();

                    if (char.IsLetterOrDigit(ch))
                    {
                        lexema += ch;
                        state = 201;
                        break;
                    }
                    state = 202;
                    break;

                case 202:
                    environment.Retract();
                    return new StringToken(lexema);

                case 203:
                    return Token.RCOL;

                case 299:
                    ch = environment.NextChar();

                    if (char.IsDigit(ch))
                    {
                        number = ch - '0';
                        state = 300;
                        break;
                    }
                    state = 305;
                    break;

                case 300:
                    ch = environment.NextChar();

                    if (char.IsDigit(ch))
                    {
                        number = number * 10 + (ch - '0');
                        state = 300;
                        break;
                    }
                    if (ch == ',')
                    {
                        divisor = 10.0;
                        state = 301;
                        break;
                    }
                    state = 304;
                    break;

                case 301:
                    ch = environment.NextChar();

                    if (char.IsDigit(ch))
                    {
                        number += (ch - '0') / divisor;
                        divisor *= 10;
                        state = 302;
                        break;
                    }
                    state = 305;
                    break;

                case 302:
                    ch = environment.NextChar();

                    if (char.IsDigit(ch))
                    {
                        number += (ch - '0') / divisor;
                        divisor *= 10;
                        state = 302;
                        break;
                    }
                    state = 303;
                    break;

                case 303:
                    environment.Retract();
                    return new RealToken(signal * number);

                case 304:
                    environment.Retract();
                    return new IntegerToken(signal * (int)number);

                case 305:
                    // Invalid Number Format
                    environment.Retract();
                    return new UnknowToken("Formato errado de um número!");

                case 20:
                    // Invalid Number Format
                    environment.Retract();
                    return new UnknowToken(string.Format("Caracter '{0}' não permitido!", ch));

                case 306:
                    return Token.SEMICOLON;
            }
        }
    }

    private AbstractToken NextRun(AbstractEnvironment<WordDocument> environment)
    {
        int state = 0;
        string lexema = "";
        char ch = '?';

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (ch == '\\')
                    {
                        state = 1;
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
                    if (ch == '{')
                    {
                        state = 6;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 7;
                        break;
                    }

                    lexema += ch;
                    state = 100;
                    break;

                case 1:
                    ch = environment.NextChar();

                    // \text{????}
                    if (char.IsLetter(ch))
                    {
                        lexema = "\\" + ch;
                        state = 2;
                        break;
                    }

                    // ERROR
                    environment.Retract();
                    return new UnknowToken("O caracter \\ não é permitido!");

                case 2:
                    ch = environment.NextChar();

                    // #text{????}
                    if (char.IsLetter(ch))
                    {
                        lexema += ch;
                        state = 2;
                        break;
                    }
                    state = 3;
                    break;

                case 3:
                    environment.Retract();

                    lexema = lexema.ToUpper();

                    if (reservedWords.ContainsKey(lexema))
                        return reservedWords[lexema];

                    environment.Retract();
                    return new UnknowToken(string.Format("{0}: indefinido!", lexema));

                case 4:
                    return Token.LCOL;

                case 5:
                    return Token.RCOL;

                case 6:
                    return Token.LBRA;

                case 7:
                    return Token.RBRA;

                case 100:
                    ch = environment.NextChar();

                    if (ch == '\\')
                    {
                        state = 107;
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
                    if (ch == '{')
                    {
                        state = 6;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 107;
                        break;
                    }

                    lexema += ch;
                    state = 100;
                    break;

                case 107:
                    environment.Retract();
                    return new TextToken(lexema);

                case 200:
                    // Invalid Number Format
                    environment.Retract();
                    return new UnknowToken("" + ch);
            }
        }
    }

    private AbstractToken NextCode(AbstractEnvironment<WordDocument> environment)
    {
        int state = 0;
        string txt = "";
        char ch;

        //AbsText text = configuration.text;

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (ch == '}')
                    {
                        state = 1;
                        break;
                    }

                    txt += ch;
                    state = 2;
                    break;

                case 1:
                    return Token.RBRA;

                case 2:
                    ch = environment.NextChar();

                    if (ch != '}')
                    {
                        txt += ch;
                        state = 2;
                        break;
                    }

                    state = 3;
                    break;

                case 3:
                    environment.Retract();
                    return new StringToken(txt.Trim());
            }
        }
    }

    private AbstractToken NextVerbatim(AbstractEnvironment<WordDocument> environment)
    {
        int state = 0;
        string txt = "";
        char ch;

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (ch == '\"')
                    {
                        state = 4;
                        break;
                    }

                    if (ch == '}')
                    {
                        state = 1;
                        break;
                    }

                    txt += ch;
                    state = 2;
                    break;

                case 1:
                    return Token.RBRA;

                case 2:
                    ch = environment.NextChar();

                    if (ch == '\"')
                    {
                        state = 4;
                        break;
                    }

                    if (ch != '}')
                    {
                        txt += ch;
                        state = 2;
                        break;
                    }

                    state = 3;
                    break;

                case 3:
                    environment.Retract();
                    return new StringToken(txt.Trim());

                case 4:
                    ch = environment.NextChar();

                    if (ch == '\"')
                    {
                        state = 2;
                        break;
                    }

                    txt += ch;
                    state = 4;
                    break;
            }
        }
    }

    private AbstractToken NextTokenAux(AbstractEnvironment<WordDocument> environment)
    {
        int state = 0;
        string lexema = "";

        char ch;

        while (true)
        {
            switch (state)
            {
                case 0:
                    if (environment.EndOfText)
                        return Token.EMPTY;

                    ch = environment.NextChar();

                    if (char.IsWhiteSpace(ch))
                    {
                        state = 0;
                        break;
                    }

                    if (ch == '\\')
                    {
                        state = 1;
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
                    if (ch == '{')
                    {
                        state = 6;
                        break;
                    }
                    if (ch == '}')
                    {
                        state = 7;
                        break;
                    }
                    if (ch == '#')
                    {
                        return Token.ENDMARK;
                    }

                    environment.Retract();
                    return new UnknowToken($"O caracter '{ch}' não é permitido!");

                case 1:
                    ch = environment.NextChar();

                    // \text{????}
                    if (char.IsLetter(ch))
                    {
                        lexema = "\\" + ch;
                        state = 2;
                        break;
                    }

                    // ERROR
                    environment.Retract();
                    return new UnknowToken("O caracter \\ não é permitido!");

                case 2:
                    ch = environment.NextChar();

                    // #text{????}
                    if (char.IsLetter(ch))
                    {
                        lexema += ch;
                        state = 2;
                        break;
                    }
                    state = 3;
                    break;

                case 3:
                    environment.Retract();

                    lexema = lexema.ToUpper();

                    if (reservedWords.ContainsKey(lexema))
                        return reservedWords[lexema];

                    ////////////////////// position--; // RETRACT
                    return new UnknowToken(string.Format("{0}: indefinido!", lexema));

                case 4:
                    return Token.LCOL;

                case 5:
                    return Token.RCOL;

                case 6:
                    return Token.LBRA;

                case 7:
                    return Token.RBRA;
            }
        }
    }

    public override AbstractToken NextToken(AbstractEnvironment<WordDocument> environment)
        => ((WordDocEnvironment)environment).Context switch
        {
            Context.CodeEnv => NextCode(environment),
            Context.VerbatimEnv => NextVerbatim(environment),
            Context.ParameterEnv => NextParameter(environment),
            Context.RunEnv => NextRun(environment),
            Context.StringEnv => NextString(environment),
            Context.PathEnv => NextPath(environment),
            _ => NextTokenAux(environment),
        };
}