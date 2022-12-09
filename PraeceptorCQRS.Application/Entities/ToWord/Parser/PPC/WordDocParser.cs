using AbstractLL;

using PraeceptorCQRS.Application.Entities.ToWord.Models;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC;

public class WordDocParser : AbstractParser<WordDocument>
{
    private static readonly AbstractTAG[][] RHS = new AbstractTAG[][]
        {
        // 0. <Start> ::= @Initialize <ElementList> "#" .
        //  {PART, CHAPTER, SECTION, SUBSECTION, SUBSUBSECTION, PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag._Initialize, Tag.ElementList, Tag._Done, Tag.ENDMARK, },

        // 1. <ElementList> ::= <Element> <ElementList'> .
        //  {PART, CHAPTER, SECTION, SUBSECTION, SUBSUBSECTION, PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag.Element, Tag.ElementList_, },

        // 2. <ElementList'> ::= <Element> <ElementList'> .
        //  {PART, CHAPTER, SECTION, SUBSECTION, SUBSUBSECTION, PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag.Element, Tag.ElementList_, },
        // 3. <ElementList'> ::= .
        //  {ENDMARK}
        Array.Empty<AbstractTAG>(),

        // 4. <Element> ::= "\\part" @BeginRun { <RunList> @EndRun @Part } .
        //  {PART}
        new AbstractTAG[] { Tag.PART, Tag.ParameterSection, Tag._HeaderParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Part, },
        // 5. <Element> ::= "\\chapter" <ParameterSection> @HeaderParameter @BeginRun { <RunList> @EndRun } @Chapter .
        //  {CHAPTER}
        new AbstractTAG[] { Tag.CHAPTER, Tag.ParameterSection, Tag._HeaderParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Chapter, },
        // 6. <Element> ::= "\\section" <ParameterSection> @HeaderParameter @BeginRun { <RunList> @EndRun } @Section .
        //  {SECTION}
        new AbstractTAG[] { Tag.SECTION, Tag.ParameterSection, Tag._HeaderParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Section, },
        // 7. <Element> ::= "\\subsection" <ParameterSection> @HeaderParameter @BeginRun { <RunList> @EndRun } @SubSection .
        //  {SUBSECTION}
        new AbstractTAG[] { Tag.SUBSECTION, Tag.ParameterSection, Tag._HeaderParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._SubSection, },
        // 8. <Element> ::= "\\subsubsection" <ParameterSection> @HeaderParameter @BeginRun { <RunList> @EndRun } @SubSubSection .
        //  {SUBSUBSECTION}
        new AbstractTAG[] { Tag.SUBSUBSECTION, Tag.ParameterSection, Tag._HeaderParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._SubSubSection, },
        // 9. <Element> ::= <SimpleElement> @ToBody .
        //  {PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag.SimpleElement, Tag._ToBody, },

        // 10. <SimpleElement> ::= "\\paragraph" <ParameterSection> @ParagraphParameter @BeginRun { <RunList> @EndRun } @EndParagraph .
        //  {PARAGRAPH}
        new AbstractTAG[] { Tag.PARAGRAPH, Tag.ParameterSection, Tag._ParagraphParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._EndParagraph, },
        // 11. <SimpleElement> ::= "\\begin" <ParameterSection> @EnvironmentParameter @BeginString { "string" @EndString @BeginEnvironment } <SimpleElementList> @Skip7 "\\end" @BeginString { "string" @EndEnvironment @EndString } @EndBegin .
        //  {BEGIN}
        new AbstractTAG[] { Tag.BEGIN, Tag.ParameterSection, Tag._EnvironmentParameter, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._EndString, Tag._BeginEnvironment, Tag.RBRA, Tag.SimpleElementList, Tag._Skip7, Tag.END, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._EndString, Tag._EndEnvironment, Tag.RBRA, Tag._EndBegin, },

        // 12. <SimpleElement> ::= "\\table" <ParameterSection> @TableParameter { <Table> <Caption> <Label> } _EndTable .
        //  {TABLE}
        new AbstractTAG[] { Tag.TABLE, Tag.ParameterSection, Tag._TableParameter, Tag.LBRA, Tag.Table, Tag.Caption, Tag.Label, Tag.RBRA, Tag._EndTable, },
        // 13. <SimpleElement> ::= "\\figure" <ParameterSection> @FigureParameter { <Figure> <Caption> <Label> } .
        //  {FIGURE}
        new AbstractTAG[] { Tag.FIGURE, Tag.ParameterSection, Tag._FigureParameter, Tag.LBRA, Tag.Figure, Tag.Caption, Tag.Label, Tag.RBRA, Tag._EndFigure, },
        // 14. <SimpleElement> ::= "\\equation" <ParameterSection> @EquationParameter { <Equation> <Caption> <Label> } .
        //  {EQUATION}
        new AbstractTAG[] { Tag.EQUATION, Tag.ParameterSection, Tag._EquationParameter, Tag.LBRA, Tag.Equation, Tag.Caption, Tag.Label, Tag.RBRA, Tag._EndEquation, },
        // 15. <SimpleElement> ::= "\\includetable" @BeginPath { "string" @Path @EndPath } @IncludeTable.
        //  {INCLUDE}
        new AbstractTAG[] { Tag.INCLUDETABLE, Tag._BeginPath, Tag.LBRA, Tag.STRING, Tag._Path, Tag._EndPath, Tag.RBRA, Tag._IncludeTable, },
        // 16. <SimpleElement> ::= "\\default" <ParameterSection> @DefaultParameter @BeginString { "string" @SetDefaultParameters @EndString } @EmptyList .
        // {DEFAULT}
        new AbstractTAG[] { Tag.DEFAULT, Tag.ParameterSection, Tag._DefaultParameter, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._SetDefaultParameters, Tag._EndString, Tag.RBRA, Tag._EmptyList, },
        // 17. <SimpleElement> ::= "\\clearpage" @Clearpage .
        //  {CLEARPAGE}
        new AbstractTAG[] { Tag.CLEARPAGE, Tag._Clearpage, },
        // 18. <SimpleElement> ::= "\\code" @BeginCode { "string" @EndCode @Code } .
        //  {CODE}
        new AbstractTAG[] { Tag.CODE, Tag._BeginPath, Tag.LBRA, Tag.STRING, Tag._Path, Tag._EndPath, Tag.RBRA, Tag._Code, },
        // new AbstractTAG[] { Tag.CODE, Tag._BeginCode, Tag.LBRA, Tag.STRING, Tag._EndCode, Tag.RBRA, Tag._Code, },
        // 19. <SimpleElement> ::= "\\verbatim" @BeginVerbatim { "string" @EndVerbatim @Verbatim } .
        //  {VERBATIM}
        new AbstractTAG[] { Tag.VERBATIM, Tag._BeginVerbatim, Tag.LBRA, Tag.STRING, Tag._EndVerbatim, Tag.RBRA, Tag._Verbatim, },

        // 20. <SimpleElementList> ::= <SimpleElement> @CreateList <SimpleElementList'> .
        //  {PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag.SimpleElement, Tag._CreateList, Tag.SimpleElementList_, },
        // 21. <SimpleElementList'> ::= <SimpleElement> @InsertList, <SimpleElementList'> .
        //  {PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, INCLUDE, DEFAULT, CLEARPAGE, VERBATIM, CODE}
        new AbstractTAG[] { Tag.SimpleElement, Tag._InsertList, Tag.SimpleElementList_, },
        // 22. <SimpleElementList'> ::= @Echo .
        //  {END}
        new AbstractTAG[] { Tag._Echo, },

        // 23. <Table> ::= <RowList> @Table .
        //  {ROW}
        new AbstractTAG[] { Tag.RowList, Tag._Table, },

        // 24. <Figure> ::= "\\includegraphics" <ParameterSection> @GraphicsParameter @BeginPath "{" "string" @Path @EndPath "}" @Includegraphics .
        //  {INCLUDEGRAPHICS}
        new AbstractTAG[] { Tag.INCLUDEGRAPHICS, Tag.ParameterSection, Tag._GraphicsParameter, Tag._BeginPath, Tag.LBRA, Tag.STRING, Tag._Path, Tag._EndPath, Tag.RBRA, Tag._Includegraphics, },
        // 25. <Caption> ::= "\\caption" <ParameterSection> @CaptionParameters @BeginRun { <RunList> @EndRun } @Caption .
        //  {CAPTION}
        new AbstractTAG[] { Tag.CAPTION, Tag.ParameterSection, Tag._CaptionParameters, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Caption, },
        // 26. <Caption> ::= @NoCaption .
        //  {LABEL, RBRA}
        new AbstractTAG[] { Tag._NoCaption, },

        // 27. <Label> ::= "\\label" @BeginString { "string" @Label @EndString } .
        //  {LABEL}
        new AbstractTAG[] { Tag.LABEL, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._Label, Tag._EndString, Tag.RBRA, },
        // 28. <Label> ::= @NoLabel .
        //  {RBRA}
        new AbstractTAG[] { Tag._NoLabel, },

        // 29. <RowList> ::= <Row> @CreateList <RowList'> .
        //  {ROW}
        new AbstractTAG[] { Tag.Row, Tag._CreateList, Tag.RowList_, },
        // 30. <RowList'> ::= <Row> @InsertList, <RowList'> .
        //  {ROW}
        new AbstractTAG[] { Tag.Row, Tag._InsertList, Tag.RowList_, },
        // 31. <RowList'> ::= @Echo .
        //  {CAPTION, LABEL, RBRA}
        new AbstractTAG[] { Tag._Echo, },

        // 32. <Row> ::= "\\row" <ParameterSection> RowParameter { <CellList> @CellList } @Row .
        //  {ROW}
        new AbstractTAG[] { Tag.ROW, Tag.ParameterSection, Tag._RowParameter, Tag.LBRA, Tag.CellList, Tag._CellList, Tag.RBRA, Tag._Row, },

        // 33. <CellList> ::= <Cell> @CreateList <CellList'> .
        //  {CELL}
        new AbstractTAG[] { Tag.Cell, Tag._CreateList, Tag.CellList_, },
        // 34. <CellList'> ::= <Cell> @InsertList, <CellList'> .
        //  {CELL}
        new AbstractTAG[] { Tag.Cell, Tag._InsertList, Tag.CellList_, },
        // 35. <CellList'> ::= @Echo .
        //  {RBRA}
        new AbstractTAG[] { Tag._Echo, },

        // 36. <Cell> ::= "\\cell" <ParameterSection> @CellParameter { <CellElementList> @CellElement } @Cell .
        //  {CELL}
        new AbstractTAG[] { Tag.CELL, Tag.ParameterSection, Tag._CellParameter, Tag.LBRA, Tag.CellElementList, Tag._CellElement, Tag.RBRA, Tag._Cell, },

        // 37. <RunList> ::= <Run> @CreateRunList <RunList'> .
        //  {TEXT, RUN, COMMENT, VARIABLE, TEXTBF, TEXTIT, TEXTSC, TEXTSL, TEXTUP, TEXTMD, TEXTRM, TEXTSF, TEXTTT, SCRIPTSIZE, FOOTNOTESIZE, NORMALSIZE, TINY, SMALL, LARGE0, LARGE1, LARGE2, HUGE0, HUGE1, FOOTNOTE, SUPERSCRIPT, SUBSCRIPT, REF, PAGEREF}
        new AbstractTAG[] { Tag.Run, Tag._CreateRunList, Tag.RunList_, },
        // 38. <RunList'> ::= <Run> @InsertRunList, <RunList'> .
        //  {TEXT, RUN, COMMENT, VARIABLE, TEXTBF, TEXTIT, TEXTSC, TEXTSL, TEXTUP, TEXTMD, TEXTRM, TEXTSF, TEXTTT, SCRIPTSIZE, FOOTNOTESIZE, NORMALSIZE, TINY, SMALL, LARGE0, LARGE1, LARGE2, HUGE0, HUGE1, FOOTNOTE, SUPERSCRIPT, SUBSCRIPT, REF, PAGEREF}
        new AbstractTAG[] { Tag.Run, Tag._InsertRunList, Tag.RunList_, },
        // 39. <RunList'> ::= @Echo .
        //  {RBRA}
        new AbstractTAG[] { Tag._Echo, },

        // 40. <Run> ::= "\\run" <ParameterSection> @RunParameter @BeginRun { <RunList> @EndRun } @Run .
        //  {RUN}
        new AbstractTAG[] { Tag.RUN, Tag.ParameterSection, Tag._RunParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Run, },
        // 41. <Run> ::= "\\comment" @BeginRun { <RunList> @EndRun } @CommentStart @BeginRun { <RunList> @EndRun } @CommentEnd .
        //  {COMMENT}
        new AbstractTAG[] { Tag.COMMENT, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._CommentStart, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._CommentEnd, },
        // 42. <Run> ::= "\\variable" @BeginString { "string" @Variable @EndString } .
        //  {VARIABLE}
        new AbstractTAG[] { Tag.VARIABLE, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._Variable, Tag._EndString, Tag.RBRA, },
        // 43. <Run> ::= "\\footnote" @StartFootnote @BeginRun { <RunList> @EndRun } @Footnote .
        //  {FOOTNOTE}
        new AbstractTAG[] { Tag.FOOTNOTE, Tag._StartFootnote, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._Footnote, },
        // 44. <Run> ::= "\\ref" @BeginString { "string" @Ref @EndString } .
        //  {REF}
        new AbstractTAG[] { Tag.REF, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._Ref, Tag._EndString, Tag.RBRA, },
        // 45. <Run> ::= "\\pageref" @BeginString { "string" @PageRef @EndString } .
        //  {PAGEREF}
        new AbstractTAG[] { Tag.PAGEREF, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._PageRef, Tag._EndString, Tag.RBRA, },
        // 46. <Run> ::= "text" .
        //  {TEXT}
        new AbstractTAG[] { Tag.TEXT, Tag._Text, },

        // 47. <ParameterSection> ::= @BeginParameter [ <ParameterList> @Parameters @EndParameter ] .
        //  {LCOL}
        new AbstractTAG[] { Tag._BeginParameter, Tag.LCOL, Tag.ParameterList, Tag._Parameters, Tag._EndParameter, Tag.RCOL, },

        // 48. <ParameterSection> ::= @NoParameters .
        //  {LBRA}
        new AbstractTAG[] { Tag._NoParameters, },

        // 49. <ParameterList> ::= <Parameter> @CreateDictionary <ParameterList'> .
        //  {STRING}
        new AbstractTAG[] { Tag.Parameter, Tag._CreateDictionary, Tag.ParameterList_, },
        // 50. <ParameterList'> ::= ";" <Parameter> @InsertDictionary <ParameterList'> .
        //  {SEMICOLON}
        new AbstractTAG[] { Tag.SEMICOLON, Tag.Parameter, Tag._InsertDictionary, Tag.ParameterList_, },
        // 51. <ParameterList'> ::= @Echo .
        //  {RCOL}
        new AbstractTAG[] { Tag._Echo, },

        // 52. <Parameter> ::= "string" @ParName <ParameterValue> .
        //  {STRING}
        new AbstractTAG[] { Tag.STRING, Tag._ParName, Tag.ParameterValue, },

        // 53. <ParameterValue> ::= "=" <RValue> @ParValue .
        //  {EQUAL}
        new AbstractTAG[] { Tag.EQUAL, Tag.RValue, Tag._ParValue, },

        // 54. <ParameterValue> ::= @DefaultValue .
        //  {SEMICOLON RCOL}
        new AbstractTAG[] { Tag._DefaultValue },

        // 55. <RValue> ::= "integer" .
        //  {INTEGER}
        new AbstractTAG[] { Tag.INTEGER, Tag._Integer, },
        // 56. <RValue> ::= "real" .
        //  {REAL}
        new AbstractTAG[] { Tag.REAL, Tag._Real, },
        // 57. <RValue> ::= "string" .
        //  {STRING}
        new AbstractTAG[] { Tag.STRING, Tag._String, },

        // 58. <Equation> ::= <Assign> . *******
        //  {STRING}
        new AbstractTAG[] { Tag.Assign, },
        // 59. <Assign> ::= "string" "=" <E> .
        //  {STRING}
        new AbstractTAG[] { Tag.STRING, Tag.EQUAL, Tag.Expression, },

        // 60. <E> ::= <T> <E'> .
        //  {SQRT, LPAR, LBRA}
        new AbstractTAG[] { Tag.Term, Tag.Expression_, },

        // 61. <E'> ::= "+" <T> <E'> .
        //  {PLUS}
        new AbstractTAG[] { Tag.PLUS, Tag.Term, Tag.Expression_, },
        // 62. <E'> ::= "-" <T> <E'> .
        //  {MINUS}
        new AbstractTAG[] { Tag.MINUS, Tag.Term, Tag.Expression_, },
        // 63. <E'> ::= .
        //  {CAPTION, LABEL, LBRA}
        Array.Empty<AbstractTAG>(),

        // 64. <T> ::= <U> <T'> .
        //  {SQRT, LPAR, LBRA}
        new AbstractTAG[] { Tag.Unary, Tag.Term_, },

        // 65. <T'> ::= "*" <U> <T'> .
        //  {STAR}
        new AbstractTAG[] { Tag.STAR, Tag.Unary, Tag.Term_, },
        // 66. <T'> ::= "/" <U> <T'> .
        //  {BAR}
        new AbstractTAG[] { Tag.BAR, Tag.Unary, Tag.Term_, },
        // 67. <T'> ::= .
        //  {PLUS, MINUS, CAPTION, LABEL, LBRA}
        Array.Empty<AbstractTAG>(),

        // 68. <U> ::= "\\sqrt" <F> .
        //  {SQRT}
        new AbstractTAG[] { Tag.SQRT, Tag.Factor, },
        // 69. <U> ::= <F> .
        //  {LPAR, LBRA}

        new AbstractTAG[] { Tag.Factor, },
        // 70. <F> ::= @BeginRun { <RunList> @EndRun } .
        //  {LBRA}
        new AbstractTAG[] { Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, },
        // 71. <F> ::= "(" <E> ")" .
        //  {LPAR}
        new AbstractTAG[] { Tag.LPAR, Tag.Expression, Tag.RPAR, },

        // 72. <CellElementList> ::= <CellElement> @CreateList <CellElementList'> .
        //  {PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, VERBATIM, CODE}
        new AbstractTAG[] { Tag.CellElement, Tag._CreateList, Tag.CellElementList_, },
        // 73. <CellElementList'> ::= <CellElement> @InsertList <CellElementList'> .
        //  {PARAGRAPH, BEGIN, TABLE, FIGURE, EQUATION, VERBATIM, CODE}
        new AbstractTAG[] { Tag.CellElement, Tag._InsertList, Tag.CellElementList_, },
        // 74. <CellElementList'> ::= @Echo .
        //  {RBRA}
        new AbstractTAG[] { Tag._Echo, },

        // 75. <CellElement> ::= "\\paragraph" <ParameterSection> @ParagraphParameter @BeginRun { <RunList> @EndRun } @CellEndParagraph .
        //  {PARAGRAPH}
        new AbstractTAG[] { Tag.PARAGRAPH, Tag.ParameterSection, Tag._ParagraphParameter, Tag._BeginRun, Tag.LBRA, Tag.RunList, Tag._EndRun, Tag.RBRA, Tag._CellEndParagraph, },
        // 76. <CellElement> ::= "\\begin" <ParameterSection> @EnvironmentParameter @BeginString { "string" @EndString @BeginEnvironment } <SimpleElementList> @Skip7 "\\end" @BeginString { "string" @EndString @EndEnvironment } @CellEndBegin .
        //  {BEGIN}
        new AbstractTAG[] { Tag.BEGIN, Tag.ParameterSection, Tag._EnvironmentParameter, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._EndString, Tag._BeginEnvironment, Tag.RBRA, Tag.SimpleElementList, Tag._Skip7, Tag.END, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._EndString, Tag._EndEnvironment, Tag.RBRA, Tag._CellEndBegin, },
        // 77. <CellElement> ::= "\\table" <ParameterSection> @TableParameter { <Table> <Caption> <Label> } @CellEndTable .
        //  {TABLE}
        new AbstractTAG[] { Tag.TABLE, Tag.ParameterSection, Tag._TableParameter, Tag.LBRA, Tag.Table, Tag.Caption, Tag.Label, Tag.RBRA, Tag._CellEndTable, },
        // 78. <CellElement> ::= "\\figure" <ParameterSection> @FigureParameter { <Figure> <Caption> <Label> } @CellEndFigure .
        //  {FIGURE}
        new AbstractTAG[] { Tag.FIGURE, Tag.ParameterSection, Tag._FigureParameter, Tag.LBRA, Tag.Figure, Tag.Caption, Tag.Label, Tag.RBRA, Tag._CellEndFigure, },
        // 79. <CellElement> ::= "\\equation" <ParameterSection> @EquationParameter { <Equation> <Caption> <Label> } @CellEndEquation .
        //  {EQUATION}
        new AbstractTAG[] { Tag.EQUATION, Tag.ParameterSection, Tag._EquationParameter, Tag.LBRA, Tag.Equation, Tag.Caption, Tag.Label, Tag.RBRA, Tag._CellEndEquation, },
        // 80. <CellElement> ::= "\\code" @BeginString { "string" @Code @EndString } .
        //  {CODE}
        new AbstractTAG[] { Tag.CODE, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._Code, Tag._EndString, Tag.RBRA, },
        // 81. <CellElement> ::= "\\verbatim" @BeginString { "string" @Verbatim @EndString } .
        //  {VERBATIM}
        new AbstractTAG[] { Tag.VERBATIM, Tag._BeginString, Tag.LBRA, Tag.STRING, Tag._Verbatim, Tag._EndString, Tag.RBRA, },
        };

    private static readonly int[][] M = new int[][]
        {
        //           0   1   2   3   4   5   6   7   8   9  10   1   2   3   4   5   6   7   8   9  20   1   2   3   4   5   6   7  s8  i9 r30   1   2   3   4   5   6   7   8   9  40   1   2   3   4
        new int[] {  0,  0,  0,  0,  0,  0,  0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  0, -1,  0,  0,  0,  0,  0,  0,  0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Start
        new int[] {  1,  1,  1,  1,  1,  1,  1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  1, -1,  1,  1,  1,  1,  1,  1,  1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // ElementList
        new int[] {  2,  2,  2,  2,  2,  2,  2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  2, -1,  2,  2,  2,  2,  2,  2,  2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  3,  }, // ElementList'
        new int[] {  4,  5,  6,  7,  8,  9,  9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  9, -1,  9,  9,  9,  9,  9,  9,  9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Element
        new int[] { -1, -1, -1, -1, -1, 10, 15, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 11, -1, 16, 17, 18, 19, 12, 13, 14, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // SimpleElement
        new int[] { -1, -1, -1, -1, -1, 20, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 20, -1, 20, 20, 20, 20, 20, 20, 20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // SimpleElementList
        new int[] { -1, -1, -1, -1, -1, 21, 21, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 21, 22, 21, 21, 21, 21, 21, 21, 21, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // SimpleElementList'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 23, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Table
        new int[] { -1, -1, -1, -1, -1, -1, -1, 24, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Figure
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, 25, 26, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 26, -1, -1, -1, -1, -1,  }, // Caption
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, 27, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 28, -1, -1, -1, -1, -1,  }, // Label
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 29, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // RowList
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, 31, 31, 30, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 31, -1, -1, -1, -1, -1,  }, // RowList'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 32, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Row
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 33, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // CellList
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 34, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 35, -1, -1, -1, -1, -1,  }, // CellList'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 36, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Cell

        new int[] { -1, -1, -1, -1, -1, 72, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 72, -1, -1, -1, 72, 72, 72, 72, 72, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // CellElementList
        new int[] { -1, -1, -1, -1, -1, 73, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 73, -1, -1, -1, 73, 73, 73, 73, 73, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 74, -1, -1, -1, -1, -1,  }, // CellElementList'
        new int[] { -1, -1, -1, -1, -1, 75, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 76, -1, -1, -1, 80, 81, 77, 78, 79, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // CellElement

        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 37, 37, 37, 37, 37, 37, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 37, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // RunList
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 38, 38, 38, 38, 38, 38, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 38, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 39, -1, -1, -1, -1, -1,  }, // RunList'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 41, 42, 40, 43, 44, 45, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 46, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Run
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 48, -1, 47, -1, -1, -1, -1,  }, // ParameterSection
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 49, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // ParameterList
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 50, -1, -1, -1, -1, -1, -1, -1, 51, -1, -1, -1,  }, // ParameterList'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 52, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Parameter
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 53, 54, -1, -1, -1, -1, -1, -1, -1, 54, -1, -1, -1,  }, // ParameterValue
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 57, 55, 56, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // RValue
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 58, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Equation
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 59, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  }, // Assign

        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 60, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 60, -1, -1, -1, 60, -1, -1,  }, // Expression
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, 63, 63, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 61, 62, -1, -1, 63, -1, -1, -1, -1, -1, -1,  }, // Expression'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 64, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 64, -1, -1, -1, 64, -1, -1,  }, // Term
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, 67, 67, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 67, 67, 65, 66, 67, -1, -1, -1, -1, -1, -1,  }, // Term'
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 68, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 69, -1, -1, -1, 69, -1, -1,  }, // Unary
        new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 70, -1, -1, -1, 71, -1, -1,  }, // Factor
        };

    public WordDocParser(AbstractEnvironment<WordDocument> environment)
        : base(RHS, M, new WordDocScanner(), new WordDocSemantic(), environment)
    { /* Nothing more todo */ }

    protected override AbstractTAG EndMark => Tag.ENDMARK;
    protected override AbstractTAG EmptyTag => Tag.empty;

    public static string[] GetRules()
    {
        string[] varName = new string[] {
            "Start",
            "ElementList",
            "ElementList'",
            "Element",
            "SimpleElement",
            "SimpleElementList",
            "SimpleElementList'",
            "Table",
            "Figure",
            "Caption",
            "Label",
            "RowList",
            "RowList'",
            "Row",
            "CellList",
            "CellList'",
            "Cell",
            "CellElementList",
            "CellElementList'",
            "CellElement",
            "RunList",
            "RunList'",
            "Run",
            "ParameterSection",
            "ParameterList",
            "ParameterList'",
            "Parameter",
            "ParameterValue",
            "RValue",
            "Equation",
            "Assign",
            "Expression",
            "Expression'",
            "Term",
            "Term'",
            "Unary",
            "Factor",
        };

        string[] vs = new string[RHS.Length];
        int k = 0;

        for (int i = 0; i < M.Length; i++)
        {
            HashSet<int> rules = new();
            for (int j = 0; j < M[i].Length; j++)
                if (M[i][j] != -1)
                    rules.Add(M[i][j]);
            foreach (int rule in rules)
            {
                vs[k] = $"// {rule}. <{varName[i]}> ::= ";
                for (int j = 0; j < RHS[rule].Length; j++)
                    vs[k] += $"{RHS[rule][j]} ";
                vs[k] += ".";
                k++;
            }
        }

        return vs;
    }

    protected override void SaveAttributes(Stack<AbstractTAG> stk, AbstractTAG A, int p)
    {
        switch (p)
        {
            // 21. <SimpleElementList'> ::= <SimpleElement> @InsertList, <SimpleElementList'> .
            case 21:
            // 30. <RowList'> ::= <Row> @InsertList, <RowList'> .
            case 30:
            // 34. <CellList'> ::= <Cell> @InsertList, <CellList'> .
            case 34:
            // 38. <RunList'> ::= <Run> @InsertList, <RunList'> .
            case 38:
            // 73. <CellElementList'> ::= <CellElement> @InsertList <CellElementList'> .
            case 73:
                stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                break;

            // 50. <ParameterList'> ::= ";" <Parameter> @InsertList <ParameterList'> .
            case 50:
                stk.ToArray()[2].SetAttribute(1, A.GetAttribute(0));
                break;

            // 61. <E'> ::= "+" <T> @Add <E'> .
            case 61:
            // 62. <E'> ::= "-" <T> @Sub <E'> .
            case 62:
            // 65. <T'> ::= "*" <U> @Mul <T'> .
            case 65:
            // 66. <T'> ::= "/" <U> @Div <T'> .
            case 66:
                stk.ToArray()[2].SetAttribute(1, A.GetAttribute(0));
                break;

            // 22. <SimpleElementList'> ::= @Echo .
            case 22:
            // 31. <RowList'> ::= @Echo .
            case 31:
            // 35. <CellList'> ::= @Echo .
            case 35:
            // 39. <RunList'> ::= @Echo .
            case 39:
            // 51. <ParameterList'> ::= @Echo .
            case 51:
            // 63. <E'> ::= @Echo .
            case 63:
            // 67. <T'> ::= @Echo .
            case 67:
            // 74. <CellElementList'> ::= @Echo .
            case 74:
                stk.ToArray()[0].SetAttribute(0, A.GetAttribute(0));
                break;

            // 53. <ParameterValue> ::= "=" <RValue> @ParValue .
            case 53:
                stk.ToArray()[2].SetAttribute(1, A.GetAttribute(0));
                break;
            // 54. <ParameterValue> ::= @DefaultValue .
            case 54:
                stk.ToArray()[0].SetAttribute(0, A.GetAttribute(0));
                break;

            // 23. <Table> ::= <RowList> @Table .
            case 23:
                stk.ToArray()[1].SetAttribute(1, A.GetAttribute(0));
                break;
            // 24. <Figure> ::= "\\includegraphics" <ParameterSection> @GraphicsParameter @BeginPath "{" "string" @Path @EndPath "}" @Includegraphics .
            case 24:
                stk.ToArray()[9].SetAttribute(2, A.GetAttribute(0));
                break;
            // 25. <Caption> ::= "\\caption" <ParameterSection> @CaptionParameters @BeginRun { <RunList> @EndRun } @Caption .
            case 25:
                stk.ToArray()[8].SetAttribute(2, A.GetAttribute(0));
                stk.ToArray()[8].SetAttribute(3, A.GetAttribute(1));
                break;
        };
    }
}