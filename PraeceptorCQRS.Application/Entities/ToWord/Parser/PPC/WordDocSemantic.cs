using AbstractLL;

using DocumentFormat.OpenXml;

using DocumentFormat.OpenXml.Wordprocessing;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Packaging;
using Ardalis.GuardClauses;
using PraeceptorCQRS.Application.Entities.ToWord.Models;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.PPC
{
    public class WordDocSemantic : AbstractSemantic<WordDocument>
    {
        private enum CaptionType
        { FIGURE, TABLE, EQUATION };

        private int _absNumberID = -1;
        private int _level = -1;

        public override void Inicializa()
        {
            // Nothing todo
        }

        public override void Execute(AbstractTAG action, Stack<AbstractTAG> stk, Stack<AbstractToken> tokens, AbstractEnvironment<WordDocument> environment)
        {
            try
            {
                if (action == Tag._Echo)
                {
                    stk.Peek().SetAttribute(0, action.GetAttribute(0));
                    return;
                }
                if (action == Tag._IncludeTable)
                {
                    string key = (string)action.GetAttribute(0);

                    key = key.ToUpper();

                    string text;

                    var instituteId = ((PPCModel)((WordDocEnvironment)environment).WordDocument).Institute.Id;

                    var response = ((WordDocEnvironment)environment).WordDocument.GetTableByCode(instituteId, key).GetAwaiter().GetResult();

                    if (!response.IsError)
                        text = response.Value.Encode();
                    else
                        text = TableNotFound(key);

                    // if (((PPCModel)((WordDocEnvironment)environment).WordDocument).tables.ContainsKey(key))
                    //     text = ((PPCModel)((WordDocEnvironment)environment).WordDocument).tables[key];
                    // else
                    //     text = TableNotFound(key);

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        var result = PPCModel.CreateFromText(text, new WordDocEnvironment((WordDocEnvironment)environment));

                        if (!result)
                        {
                            PPCModel.CreateFromText(
                                "\\paragraph[JUSTIFICATION=left;FIRSTLINE=0]{\\run[BOLD;COLOR=16711680]{Sintaxe incorreta em \"" + key + "\"!}}",
                                new WordDocEnvironment((WordDocEnvironment)environment));
                        }
                    }
                    else
                    {
                        PPCModel.CreateFromText(
                            "\\paragraph[JUSTIFICATION=left;FIRSTLINE=0]{\\run[BOLD;COLOR=16711680]{TABELA \"" + key + "\" NÃO ENCONTRADA!}}",
                            new WordDocEnvironment((WordDocEnvironment)environment));
                    }

                    stk.Peek().SetAttribute(0, new List<object>());
                    return;
                }
                if (action == Tag._Variable)
                {
                    StringToken key = (StringToken)tokens.Pop();

                    string str;

                    RunProperties rPr;
                    Run run;

                    if (((WordDocEnvironment)environment).WordDocument.variables.ContainsKey(key.str.ToUpper()))
                    {
                        rPr = new(
                            new Underline() { Val = UnderlineValues.Dotted },
                            new Color() { Val = "200040" }
                            );

                        str = ((WordDocEnvironment)environment).WordDocument.variables[key.str.ToUpper()];
                    }
                    else
                    {
                        // RunProperties rPr = new RunProperties(new Bold());
                        rPr = new(
                            new Color() { Val = "FF0000" }
                            );
                        str = $"({key.str}: Não encontrado)";
                    }

                    run = new Run(rPr);
                    run.Append(new Text(str) { Space = SpaceProcessingModeValues.Preserve });

                    stk.ElementAt(2).SetAttribute(0, run);
                    return;
                }
                if (action == Tag._Skip7)
                {
                    stk.ElementAt(7).SetAttribute(0, action.GetAttribute(0));
                    return;
                }

                #region Diversos

                if (action == Tag._Initialize)
                {
                    return;
                }
                if (action == Tag._Ignore)
                {
                    // Do nothing
                    return;
                }
                if (action == Tag._Done)
                {
                    Console.WriteLine("Cheguei ao @Done!");
                    return;
                }
                if (action == Tag._ToBody)
                {
                    if (action.TestNullAttribute(0)) // PARA RESOLVER O PROBLEMA DO Run VAZIO { }
                    {
                        List<object> runList = new() { new Run(new Text()) };
                        action.SetAttribute(0, runList);
                    }

                    List<object> elements = (List<object>)action.GetAttribute(0);

                    foreach (OpenXmlElement element in elements.Cast<OpenXmlElement>())
                    {
                        Guard.Against.Null(environment);
                        ((WordDocEnvironment)environment).WordDocument.TheBody()?.AppendChild(element);
                    }
                    return;
                }
                if (action == Tag._Clearpage)
                {
                    List<object> list =
                        new()
                        {
                            new Paragraph(new Run(new Break() { Type = BreakValues.Page }))
                        };

                    // Envia uma lista vazia para o ToBody
                    stk.Peek().SetAttribute(0, list);
                    return;
                }

                #endregion Diversos

                #region Listas

                if (action == Tag._CreateList || action == Tag._CreateRunList)
                {
                    object obj = action.GetAttribute(0);
                    List<object> list = new();

                    if (obj is List<object>)
                        list.AddRange((IEnumerable<object>)obj);
                    else
                    {
                        if (obj is Run run)
                        {
                            string str = run.Descendants<Text>().First().Text;

                            str = str.TrimStart(new char[] { ' ', '\t', '\n', '\r', '\f' });

                            run.Descendants<Text>().First().Text = str;

                            list.Add(obj);
                        }
                        else
                            list.Add(obj);
                    }

                    stk.Peek().SetAttribute(0, list);
                    return;
                }
                if (action == Tag._InsertList || action == Tag._InsertRunList)
                {
                    object obj = action.GetAttribute(0);
                    List<object> list = (List<object>)action.GetAttribute(1);

                    if (obj is List<object> list1)
                    {
                        foreach (object o in list1)
                            list.Add(o);
                    }
                    else
                        list.Add(obj);

                    stk.Peek().SetAttribute(0, list);
                    return;
                }
                if (action == Tag._EmptyList)
                {
                    // Envia uma lista vazia para o ToBody
                    stk.Peek().SetAttribute(0, new List<object>());
                    return;
                }

                #endregion Listas

                #region Parametros

                if (action == Tag._CreateDictionary)
                {
                    object obj = action.GetAttribute(0);
                    Dictionary<string, object> parameters = new();

                    if (obj is Dictionary<string, object> dictionary)
                    {
                        foreach (KeyValuePair<string, object> o in dictionary)
                            parameters.Add(o.Key, o.Value);
                    }
                    else
                    {
                        KeyValuePair<string, object> o = (KeyValuePair<string, object>)obj;
                        parameters.Add(o.Key, o.Value);
                    }

                    stk.Peek().SetAttribute(0, parameters);
                    return;
                }
                if (action == Tag._InsertDictionary)
                {
                    object obj = action.GetAttribute(0);
                    Dictionary<string, object> parameters = (Dictionary<string, object>)action.GetAttribute(1);

                    if (obj is Dictionary<string, object> dictionary)
                    {
                        foreach (KeyValuePair<string, object> o in dictionary)
                            parameters.Add(o.Key, o.Value);
                    }
                    else
                    {
                        KeyValuePair<string, object> o = (KeyValuePair<string, object>)obj;
                        parameters.Add(o.Key, o.Value);
                    }

                    stk.Peek().SetAttribute(0, parameters);
                    return;
                }
                if (action == Tag._BeginParameter)
                {
                    ((WordDocEnvironment)environment).ChangeContext(Context.ParameterEnv);
                    return;
                }
                if (action == Tag._EndParameter)
                {
                    ((WordDocEnvironment)environment).RestoreContext();
                    return;
                }
                if (action == Tag._NoParameters)
                {
                    stk.Peek().SetAttribute(0, new Dictionary<string, object>());
                    return;
                }
                if (action == Tag._ParName)
                {
                    stk.Peek().SetAttribute(0, ((StringToken)tokens.Pop()).str.ToUpper());
                    return;
                }
                if (action == Tag._ParValue)
                {
                    string name = (string)action.GetAttribute(1);
                    object value = (object)action.GetAttribute(0);
                    KeyValuePair<string, object> pair =
                        new(name.ToUpper(), value);

                    stk.Peek().SetAttribute(0, pair);
                    return;
                }
                if (action == Tag._DefaultValue)
                {
                    string name = (string)action.GetAttribute(0);
                    KeyValuePair<string, object> pair = new(name.ToUpper(), true);

                    stk.Peek().SetAttribute(0, pair);
                    return;
                }
                if (action == Tag._Integer)
                {
                    stk.Peek().SetAttribute(0, ((IntegerToken)tokens.Pop()).val);
                    return;
                }
                if (action == Tag._Real)
                {
                    stk.Peek().SetAttribute(0, ((RealToken)tokens.Pop()).val);
                    return;
                }
                if (action == Tag._String)
                {
                    stk.Peek().SetAttribute(0, ((StringToken)tokens.Pop()).str.ToUpper());
                    return;
                }
                if (action == Tag._DefaultParameter)
                {
                    Dictionary<string, object> list =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    stk.ElementAt(3).SetAttribute(0, list);
                    return;
                }
                if (action == Tag._SetDefaultParameters)
                {
                    string key = ((StringToken)tokens.Pop()).str.ToUpper();
                    Dictionary<string, object> list =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    if (key == "PARAGRAPH")
                    {
                        ((WordDocEnvironment)environment).WordDocument.DefaultParagraphProperties =
                            new ParagraphProperties();
                        ((WordDocEnvironment)environment).WordDocument.DefaultParagraphProperties =
                            ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(list);
                    }
                    else if (key == "RUN")
                    {
                        ((WordDocEnvironment)environment).WordDocument.DefaultRunProperties =
                            new RunProperties();
                        ((WordDocEnvironment)environment).WordDocument.DefaultRunProperties =
                            ((WordDocEnvironment)environment).WordDocument.RunPropertiesFromParameters(list);
                    }
                    else if (key == "TABLE")
                    {
                        ((WordDocEnvironment)environment).WordDocument.DefaultTableProperties =
                            new TableProperties();
                        ((WordDocEnvironment)environment).WordDocument.DefaultTableProperties =
                            ((WordDocEnvironment)environment).WordDocument.TablePropertiesFromParameters(list);
                    }
                    else if (key == "TABLECELL")
                    {
                        ((WordDocEnvironment)environment).WordDocument.DefaultTableCellProperties =
                            new TableCellProperties();
                        ((WordDocEnvironment)environment).WordDocument.DefaultTableCellProperties =
                            ((WordDocEnvironment)environment).WordDocument.CellPropertiesFromParameters(list);
                    }
                    return;
                }
                if (action == Tag._ParagraphParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    ParagraphProperties paragraphProperties =
                        ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(parameters);

                    if (((WordDocEnvironment)environment).ListType == ListTypeValues.Enumerate)
                    {
                        SpacingBetweenLines sblUl = new() { After = "0" };

                        int id = ((WordDocEnvironment)environment).WordDocument.numberingId;

                        NumberingProperties npUl = new(
                            new NumberingLevelReference() { Val = ((WordDocEnvironment)environment).Level },
                            new NumberingId() { Val = id }
                        );

                        // if (_absNumberID != -1)
                        {
                            // NumberingProperties npUl = new(
                            //     new NumberingLevelReference() { Val = ((WordDocEnvironment)environment).Level },
                            //     new NumberingId() { Val = _absNumberID }
                            // );

                            paragraphProperties.Append(npUl);
                        }
                        paragraphProperties.Append(sblUl);

                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Numerado" };
                    }
                    else if (((WordDocEnvironment)environment).ListType == ListTypeValues.Itemize)
                    {
                        SpacingBetweenLines sblUl = new() { After = "0" };

                        int id = ((WordDocEnvironment)environment).WordDocument.numberingId;

                        NumberingProperties npUl = new(
                            new NumberingLevelReference() { Val = ((WordDocEnvironment)environment).Level },
                            new NumberingId() { Val = id }
                        );

                        // if (_absNumberID != -1)
                        {
                            // NumberingProperties npUl = new(
                            //     new NumberingLevelReference() { Val = ((WordDocEnvironment)environment).Level },
                            //     new NumberingId() { Val = _absNumberID }
                            // );

                            paragraphProperties.Append(npUl);
                        }
                        paragraphProperties.Append(sblUl);

                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Marcado" };
                    }

                    stk.ElementAt(5).SetAttribute(1, paragraphProperties);
                    return;
                }
                if (action == Tag._CaptionParameters)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    stk.ElementAt(5).SetAttribute(1, parameters);
                    return;
                }
                if (action == Tag._RowParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    stk.ElementAt(4).SetAttribute(1, parameters);
                    return;
                }
                if (action == Tag._CellParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    stk.ElementAt(4).SetAttribute(1, parameters);
                    return;
                }
                if (action == Tag._GraphicsParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    stk.ElementAt(6).SetAttribute(1, parameters);
                    return;
                }
                if (action == Tag._FigureParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    if (!parameters.ContainsKey("JUSTIFICATION"))
                        parameters.Add("JUSTIFICATION", "CENTER");

                    ParagraphProperties paragraphProperties =
                        ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(parameters);

                    stk.ElementAt(1).SetAttribute(0, paragraphProperties);
                    stk.ElementAt(2).SetAttribute(0, parameters);
                    stk.ElementAt(2).SetAttribute(1, CaptionType.FIGURE);
                    return;
                }
                if (action == Tag._TableParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    TableProperties tableProperties =
                        ((WordDocEnvironment)environment).WordDocument.TablePropertiesFromParameters(parameters);

                    stk.ElementAt(1).SetAttribute(0, tableProperties);
                    stk.ElementAt(2).SetAttribute(0, parameters);
                    stk.ElementAt(2).SetAttribute(1, CaptionType.TABLE);
                    return;
                }
                if (action == Tag._EquationParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    if (!parameters.ContainsKey("JUSTIFICATION"))
                        parameters.Add("JUSTIFICATION", "CENTER");

                    ParagraphProperties paragraphProperties =
                        ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(parameters);

                    stk.ElementAt(1).SetAttribute(0, paragraphProperties);
                    stk.ElementAt(2).SetAttribute(0, parameters);
                    stk.ElementAt(2).SetAttribute(1, CaptionType.EQUATION);
                    return;
                }
                if (action == Tag._Parameters)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);
                    stk.ElementAt(2).SetAttribute(0, parameters);
                    return;
                }

                #endregion Parametros

                #region Partes

                if (action == Tag._HeaderParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    ParagraphProperties paragraphProperties =
                        ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(parameters);

                    stk.ElementAt(5).SetAttribute(1, paragraphProperties);
                    return;
                }
                if (action == Tag._Part)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    if (!paragraphProperties.HasChildren)
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Ttulo1" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheBody()?.AppendChild(paragraph);
                    return;
                }
                if (action == Tag._Chapter)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    if (!paragraphProperties.Descendants<ParagraphStyleId>().Any())
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Capitulo" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheBody()?.AppendChild(paragraph);
                    return;
                }
                if (action == Tag._Section)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    if (!paragraphProperties.Descendants<ParagraphStyleId>().Any())
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Secao" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheBody()?.AppendChild(paragraph);
                    return;
                }
                if (action == Tag._SubSection)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    if (!paragraphProperties.Descendants<ParagraphStyleId>().Any())
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Subsecao" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheBody()?.AppendChild(paragraph);
                    return;
                }
                if (action == Tag._SubSubSection)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    if (!paragraphProperties.Descendants<ParagraphStyleId>().Any())
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId() { Val = "Subsubsecao" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheBody()?.AppendChild(paragraph);
                    return;
                }

                #endregion Partes

                #region Run

                if (action == Tag._BeginRun)
                {
                    ((WordDocEnvironment)environment).ChangeContext(Context.RunEnv);
                    return;
                }
                if (action == Tag._Text)
                {
                    string str = ((TextToken)tokens.Pop()).val;

                    Run run = new();

                    if (str.TrimStart(new char[] { ' ', '\t', '\n', '\r', '\f' }) != "")
                        run.Append(new Text(str) { Space = SpaceProcessingModeValues.Preserve });
                    else
                        run.Append(new Text(str) { Space = SpaceProcessingModeValues.Default });

                    stk.Peek().SetAttribute(0, run);
                    return;
                }
                if (action == Tag._RunParameter)
                {
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(0);

                    RunProperties runProperties =
                        ((WordDocEnvironment)environment).WordDocument.RunPropertiesFromParameters(parameters);

                    stk.ElementAt(5).SetAttribute(1, runProperties);
                    return;
                }
                if (action == Tag._Run)
                {
                    RunProperties runProperties = (RunProperties)action.GetAttribute(1);
                    List<object> runList = (List<object>)action.GetAttribute(0);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                    {
                        var item = run.Elements<Text>();
                        foreach (Text text in item)
                            run.InsertBefore((RunProperties)runProperties.Clone(), text);
                    }

                    stk.Peek().SetAttribute(0, runList);
                    return;
                }
                if (action == Tag._EndRun)
                {
                    ((WordDocEnvironment)environment).RestoreContext();

                    List<object> list = (List<object>)action.GetAttribute(0);

                    stk.ElementAt(1).SetAttribute(0, list);
                    return;
                }

                #endregion Run

                #region Paragrafo

                if (action == Tag._EndParagraph)
                {
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);

                    List<object> runList =
                        (List<object>)action.GetAttribute(0);

                    Paragraph paragraph =
                        new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((OpenXmlElement)(run).Clone()); // Com o Clone, tem que usar o cast

                    List<object> elements =
                        new() { paragraph };

                    stk.Peek().SetAttribute(0, elements);
                    return;
                }

                #endregion Paragrafo

                #region String

                if (action == Tag._BeginString)
                {
                    ((WordDocEnvironment)environment).ChangeContext(Context.StringEnv);
                    return;
                }
                if (action == Tag._EndString)
                {
                    ((WordDocEnvironment)environment).RestoreContext();
                    return;
                }

                #endregion String

                #region Path

                if (action == Tag._Path)
                {
                    string path = ((StringToken)tokens.Pop()).str;
                    stk.ElementAt(2).SetAttribute(0, path);
                    return;
                }
                if (action == Tag._BeginPath)
                {
                    ((WordDocEnvironment)environment).ChangeContext(Context.PathEnv);
                    return;
                }
                if (action == Tag._EndPath)
                {
                    ((WordDocEnvironment)environment).RestoreContext();
                    return;
                }

                #endregion Path

                #region Comentário

                if (action == Tag._CommentStart)
                {
                    List<object> runList = (List<object>)action.GetAttribute(0);

                    Comment comment = ((WordDocEnvironment)environment).WordDocument.CreateComment();

                    Paragraph paragraph = new();

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    comment.Append(paragraph);

                    Run mark = new();

                    mark.Append(new RunProperties() { RunStyle = new RunStyle() { Val = "Refdecomentrio" } });

                    AnnotationReferenceMark annotationReferenceMark = new();
                    mark.Append(annotationReferenceMark);

                    var list = new List<object> { mark, new CommentRangeStart() { Id = comment.Id } };

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheMainDocumentPart?.WordprocessingCommentsPart?.Comments.AppendChild(comment);

                    stk.ElementAt(5).SetAttribute(1, comment);

                    stk.ElementAt(5).SetAttribute(2, list);
                    return;
                }
                if (action == Tag._CommentEnd)
                {
                    List<object> list = (List<object>)action.GetAttribute(2);
                    Comment comment = (Comment)action.GetAttribute(1);
                    List<object> innerList = (List<object>)action.GetAttribute(0);

                    foreach (object obj in innerList)
                        list.Add(obj);

                    list.Add(new CommentRangeEnd() { Id = comment.Id });

                    Run commentRun = new();

                    commentRun.Append(
                        new RunProperties()
                        {
                            RunStyle = new RunStyle() { Val = "Refdecomentrio" }
                        });

                    CommentReference commentReference = new() { Id = comment.Id };
                    commentRun.Append(commentReference);

                    list.Add(commentRun);

                    stk.Peek().SetAttribute(0, list);
                    return;
                }

                #endregion Comentário

                #region Caption

                if (action == Tag._Caption)
                {
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(1);
                    Dictionary<string, object> parameters0 =
                        (Dictionary<string, object>)action.GetAttribute(2);
                    CaptionType captionType =
                        (CaptionType)action.GetAttribute(3);

                    foreach (string key in parameters0.Keys)
                        if (!parameters.ContainsKey(key))
                            parameters.Add(key, parameters0[key]);

                    Paragraph paragraph = new();

                    ParagraphProperties mainParagraphProperties =
                        ((WordDocEnvironment)environment).WordDocument.ParagraphPropertiesFromParameters(parameters);
                    mainParagraphProperties.Append(new ParagraphStyleId() { Val = "Caption" });

                    int captionNumber;
                    string captionTitle;

                    switch (captionType)
                    {
                        case CaptionType.EQUATION:
                            captionNumber = ((WordDocEnvironment)environment).WordDocument.equationId;
                            captionTitle = "Equation ";
                            break;

                        case CaptionType.FIGURE:
                            captionNumber = ((WordDocEnvironment)environment).WordDocument.figureId;
                            captionTitle = "Figura ";
                            break;

                        case CaptionType.TABLE:
                            captionNumber = ((WordDocEnvironment)environment).WordDocument.tableId;
                            captionTitle = "Tabela ";
                            break;

                        default:
                            captionNumber = 0;
                            captionTitle = "Indefinido ";
                            break;
                    }

                    Run run1 = new();
                    Text text1 = new()
                    {
                        Space = SpaceProcessingModeValues.Preserve,
                        Text = captionTitle
                    };
                    run1.Append(text1);

                    SimpleField simpleField = new() { Instruction = " SEQ Figura \\* ARABIC " };

                    Run run2 = new();

                    RunProperties runProperties = new();
                    NoProof noProof = new();

                    runProperties.Append(noProof);
                    Text text2 = new() { Text = string.Format("{0}", captionNumber) };

                    run2.Append(runProperties);
                    run2.Append(text2);

                    simpleField.Append(run2);

                    string bookmarkId = ((WordDocEnvironment)environment).WordDocument.NewBookmark();

                    BookmarkStart bookmarkStart =
                        new() { Name = "_GoBack", Id = bookmarkId };
                    BookmarkEnd bookmarkEnd =
                        new() { Id = bookmarkId.ToString() };

                    paragraph.Append(mainParagraphProperties);
                    paragraph.Append(run1);

                    if (simpleField != null)
                        paragraph.Append(simpleField);

                    Run run3 = new();
                    Text text3 = new()
                    {
                        Space = SpaceProcessingModeValues.Preserve,
                        Text = ". "
                    };

                    run3.Append(text3);

                    paragraph.Append(run3);

                    paragraph.Append(bookmarkStart);
                    paragraph.Append(bookmarkEnd);

                    foreach (OpenXmlElement element in runList.Cast<OpenXmlElement>())
                        paragraph.Append(element);

                    stk.ElementAt(2).SetAttribute(1, paragraph);
                    return;
                }
                if (action == Tag._NoCaption)
                {
                    // Nothing todo
                    return;
                }

                #endregion Caption

                #region Figure

                if (action == Tag._Includegraphics)
                {
                    ParagraphProperties paragraphProperties = (ParagraphProperties)action.GetAttribute(2);
                    Dictionary<string, object> properties = (Dictionary<string, object>)action.GetAttribute(1);
                    string imageKey = (string)action.GetAttribute(0);

                    ImagePart? imagePart = ((WordDocEnvironment)environment).WordDocument.TheMainDocumentPart?.AddImagePart(ImagePartType.Jpeg);

                    double scale = 1.0;

                    UInt32Value distanceFromTop = 0U;
                    UInt32Value distanceFromBottom = 0U;
                    UInt32Value distanceFromLeft = 0U;
                    UInt32Value distanceFromRight = 0U;

                    long leftEdge = 0L;
                    long topEdge = 0L;
                    long rightEdge = 0L;
                    long bottomEdge = 0L;

                    foreach (string key in properties.Keys)
                    {
                        if (key == "SCALE")
                        {
                            if (properties[key] is double @double)
                                scale = @double;
                        }
                        else if (key == "DISTANCEFROMTOP")
                        {
                            if (properties[key] is int)
                                distanceFromTop = (UInt32Value)properties[key];
                        }
                        else if (key == "DISTANCEFROMBOTTOM")
                        {
                            if (properties[key] is int)
                                distanceFromBottom = (UInt32Value)properties[key];
                        }
                        else if (key == "DISTANCEFROMLEFT")
                        {
                            if (properties[key] is int)
                                distanceFromLeft = (UInt32Value)properties[key];
                        }
                        else if (key == "DISTANCEFROMRIGHT")
                        {
                            if (properties[key] is int)
                                distanceFromRight = (UInt32Value)properties[key];
                        }
                        else if (key == "LEFTEDGE")
                        {
                            if (properties[key] is int)
                                leftEdge = (long)properties[key];
                        }
                        else if (key == "TOPEDGE")
                        {
                            if (properties[key] is int)
                                topEdge = (long)properties[key];
                        }
                        else if (key == "RIGHTEDGE")
                        {
                            if (properties[key] is int)
                                rightEdge = (long)properties[key];
                        }
                        else if (key == "BOTTOMEDGE")
                        {
                            if (properties[key] is int)
                                bottomEdge = (long)properties[key];
                        }
                    }

                    try
                    {
                        Paragraph paragraph = new();

                        long widthEmus = 0;
                        long heightEmus = 0;

                        var instituteId = ((PPCModel)((WordDocEnvironment)environment).WordDocument).Institute.Id;

                        var fileStream = ((WordDocEnvironment)environment).WordDocument.GetImageByCode(instituteId, imageKey).GetAwaiter().GetResult();

                        if (fileStream.IsError)
                            fileStream = ((WordDocEnvironment)environment).WordDocument.GetImageByCode(instituteId, "NotFound").GetAwaiter().GetResult();

                        if (!fileStream.IsError)
                        {
                            byte[] buffer = fileStream.Value.Data;// Convert.FromBase64String(image.VectorImage["data:image/gif;base64,".Length..]);
                            SixLabors.ImageSharp.Image img = SixLabors.ImageSharp.Image.Load(buffer);

                            long widthPx = img.Width;
                            long heightPx = img.Height;
                            var horzRezDpi = img.Metadata.HorizontalResolution;
                            var vertRezDpi = img.Metadata.VerticalResolution;

                            // An English Metric Unit (EMU) is defined as 1/360000 of a
                            // centimeter and thus there are 914400 EMUs per inch, and
                            // 12700 EMUs per point.
                            // const int emusPerInch = 914400;
                            const int emusPerCm = 360000;

                            widthEmus = (long)(widthPx * emusPerCm / horzRezDpi);
                            heightEmus = (long)(heightPx * emusPerCm / vertRezDpi);

                            var maxWidthCm = 15;
                            var maxWidthEmus = (long)(maxWidthCm * emusPerCm);

                            // Ajusta tamanho da imagem de modo que sua largura coincida com
                            // a largura do texto
                            var ratio = (heightEmus * 1.0m) / widthEmus;
                            var s = (1.0 * maxWidthEmus) / widthEmus;
                            widthEmus = (long)(s * widthEmus);
                            heightEmus = (long)(widthEmus * ratio);

                            MemoryStream memory = new(buffer);
                            imagePart?.FeedData(memory);

                            // A escala é um percentual da largura do texto
                            widthEmus = (long)(scale * widthEmus);
                            heightEmus = (long)(scale * heightEmus);

                            string Uri = Guid.NewGuid().ToString();

                            string bookmarkId = ((WordDocEnvironment)environment).WordDocument.NewBookmark();
                            BookmarkStart bookmarkStart1 = new() { Name = "_GoBack", Id = bookmarkId };

                            Run run1 = new();

                            RunProperties runProperties1 = new();
                            NoProof noProof1 = new();

                            runProperties1.Append(noProof1);

                            int fig = ++((WordDocEnvironment)environment).WordDocument.figureId;

                            // Define the reference of the image.
                            var drawing1 =
                                 new Drawing(
                                     new DW.Inline(
                                         // new DW.Extent() { Cx = 990000L, Cy = 792000L },
                                         new DW.Extent() { Cx = widthEmus, Cy = heightEmus },
                                         new DW.EffectExtent()
                                         {
                                             LeftEdge = leftEdge,
                                             TopEdge = topEdge,
                                             RightEdge = rightEdge,
                                             BottomEdge = bottomEdge
                                         },
                                         new DW.DocProperties()
                                         {
                                             Id = (UInt32Value)1U,
                                             Name = string.Format("Imagem {0}", fig)
                                         },
                                         new DW.NonVisualGraphicFrameDrawingProperties(
                                             new A.GraphicFrameLocks() { NoChangeAspect = true }
                                         ),

                                         new A.Graphic(
                                             new A.GraphicData(
                                                 new PIC.Picture(
                                                     new PIC.NonVisualPictureProperties(
                                                         new PIC.NonVisualDrawingProperties()
                                                         {
                                                             Id = (UInt32Value)0U,
                                                             Name = "New Bitmap Image"
                                                         },
                                                         new PIC.NonVisualPictureDrawingProperties()),
                                                     new PIC.BlipFill(
                                                         new A.Blip(
                                                             new A.BlipExtensionList(
                                                                 new A.BlipExtension(
                                                                     new A14.UseLocalDpi() { Val = false }
                                                                 // AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main")
                                                                 )
                                                                 { Uri = String.Format("{{{0}}}", Uri) }
                                                             )
                                                         )

                                                         {
                                                             Embed = ((WordDocEnvironment?)environment)?.WordDocument?.TheMainDocumentPart?.GetIdOfPart(imagePart!),
                                                             CompressionState = A.BlipCompressionValues.Print
                                                         },
                                                         new A.Stretch(
                                                             new A.FillRectangle()
                                                         )
                                                     ),
                                                     new PIC.ShapeProperties(
                                                         new A.Transform2D(
                                                             new A.Offset() { X = 0L, Y = 0L },
                                                             // new A.Extents() { Cx = 990000L, Cy = 792000L }
                                                             new A.Extents() { Cx = widthEmus, Cy = heightEmus }
                                                         ),
                                                         new A.PresetGeometry(
                                                             new A.AdjustValueList()
                                                         )
                                                         {
                                                             Preset = A.ShapeTypeValues.Rectangle
                                                         }
                                                     )
                                                 )
                                             )
                                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }
                                         )
                                     )
                                     {
                                         DistanceFromTop = distanceFromTop,
                                         DistanceFromBottom = distanceFromBottom,
                                         DistanceFromLeft = distanceFromLeft,
                                         DistanceFromRight = distanceFromRight,
                                         AnchorId = "63B56EF4",
                                         EditId = "50D07946"
                                     }
                                 );

                            run1.Append(runProperties1);
                            run1.Append(drawing1);

                            BookmarkEnd bookmarkEnd1 = new() { Id = bookmarkId.ToString() };

                            paragraph.Append(paragraphProperties);
                            paragraph.Append(bookmarkStart1);
                            paragraph.Append(run1);
                            paragraph.Append(bookmarkEnd1);
                        }
                        else
                        {
                            RunProperties rPr = new(new Color() { Val = "FF0000" });
                            Run run = new(rPr);
                            run.Append(new Text($"Imagem {imageKey} não encontrada!") { Space = SpaceProcessingModeValues.Preserve });
                            paragraph.Append(run);
                        }

                        // Append the reference to body, the element should be in a Run.
                        stk.ElementAt(3).SetAttribute(2, paragraph);
                    }
                    catch (Exception ex)
                    {
                        RunProperties rPr1 = new(new Color() { Val = "FF0000" });
                        Run run1 = new(rPr1);
                        run1.Append(new Text($"Aconteceu uma excessão durante a obtenção da imagem {imageKey}!") { Space = SpaceProcessingModeValues.Preserve });
                        Paragraph paragraph = new();
                        paragraph.Append(run1);
                        RunProperties rPr2 = new(new Color() { Val = "FF0000" });
                        Run run2 = new(rPr2);
                        run2.Append(new Text(ex.Message) { Space = SpaceProcessingModeValues.Preserve });
                        paragraph.Append(run2);
                        stk.ElementAt(3).SetAttribute(2, paragraph);
                    }
                    finally
                    {
                        // Do nothing
                    }
                    return;
                }
                if (action == Tag._EndFigure)
                {
                    Paragraph paragraph = (Paragraph)action.GetAttribute(2);

                    List<object> elements = new() { paragraph };

                    if (!action.TestNullAttribute(1))
                        elements.Add((Paragraph)action.GetAttribute(1));

                    stk.Peek().SetAttribute(0, elements);
                    return;
                }

                #endregion Figure

                #region Table

                if (action == Tag._CellEndBegin)
                {
                    // VAI PARA LISTA DE ITENS

                    stk.Peek().SetAttribute(0, action.GetAttribute(0));
                    return;
                }
                if (action == Tag._CellElement)
                {
                    List<object> elements =
                        (List<object>)action.GetAttribute(0);

                    stk.ElementAt(1).SetAttribute(0, elements);
                    return;
                }
                if (action == Tag._CellEndParagraph)
                {
                    ParagraphProperties paragraphProperties =
                        (ParagraphProperties)action.GetAttribute(1);
                    List<object> runList =
                        (List<object>)action.GetAttribute(0);

                    // If a ParagraphStyleId object doesn't exist, create one.
                    if (!paragraphProperties.Descendants<ParagraphStyleId>().Any())
                        paragraphProperties.ParagraphStyleId =
                            new ParagraphStyleId { Val = "Normal" };

                    Paragraph paragraph = new(paragraphProperties);

                    foreach (OpenXmlElement run in runList.Cast<OpenXmlElement>())
                        paragraph.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    stk.Peek().SetAttribute(0, paragraph);

                    // VAI PARA LISTA DE ITENS
                    return;
                }
                if (action == Tag._CellEndTable)
                {
                    Table table = (Table)action.GetAttribute(2);
                    Paragraph caption = (Paragraph)action.GetAttribute(1);

                    // VAI PARA LISTA DE ITENS
                    return;
                }
                if (action == Tag._CellEndFigure)
                {
                    // Label
                    // Caption
                    // Parameters
                    Paragraph paragraph = (Paragraph)action.GetAttribute(2);

                    List<object> elements = new() { paragraph };

                    // Legenda de tabelas em baixo
                    if (!action.TestNullAttribute(1))
                        elements.Add((Paragraph)action.GetAttribute(1));

                    stk.Peek().SetAttribute(0, elements);
                    return;
                }
                if (action == Tag._CellEndEquation)
                {
                    Paragraph paragraph = (Paragraph)action.GetAttribute(2);
                    Paragraph caption = (Paragraph)action.GetAttribute(1);

                    // VAI PARA LISTA DE ITENS
                    return;
                }
                if (action == Tag._Table)
                {
                    List<object> rowList =
                        (List<object>)action.GetAttribute(0);
                    TableProperties tableProperties =
                        (TableProperties)action.GetAttribute(1);

                    Table table = new();

                    int columns = ((TableRow)rowList[0]).Descendants<TableCell>().Count();

                    int tbWidth = 5000;

                    // Make the table width 100% of the page width (50 * 100).
                    TableWidth tableWidth =
                        new() { Width = ((int)tbWidth).ToString(), Type = TableWidthUnitValues.Pct };
                    tableProperties.Append(tableWidth);

                    TableCaption tableCaption =
                        new() { Val = "Caption Table" };
                    tableProperties.Append(tableCaption);

                    TableGrid tableGrid = new();

                    for (int w = 0; w < columns; w++)
                        tableGrid.Append(new GridColumn());

                    table.Append(tableProperties);
                    table.Append(tableGrid);

                    foreach (TableRow row in rowList.Cast<OpenXmlElement>())
                        table.Append(row);

                    stk.ElementAt(3).SetAttribute(2, table);

                    ++((WordDocEnvironment)environment).WordDocument.tableId;
                    return;
                }
                if (action == Tag._Cell)
                {
                    List<object> elements =
                        (List<object>)action.GetAttribute(0);
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(1);

                    TableCellProperties properties =
                        ((WordDocEnvironment)environment).WordDocument.CellPropertiesFromParameters(parameters);

                    TableCell cell = new();

                    cell.Append(properties);

                    foreach (OpenXmlElement element in elements.Cast<OpenXmlElement>())
                        cell.Append(element);

                    stk.Peek().SetAttribute(0, cell);
                    return;
                }
                if (action == Tag._Row)
                {
                    List<object> cellList =
                        (List<object>)action.GetAttribute(0);
                    Dictionary<string, object> parameters =
                        (Dictionary<string, object>)action.GetAttribute(1);

                    TableRowProperties properties =
                        ((WordDocEnvironment)environment).WordDocument.RowPropertiesFromParameters(parameters);

                    TableRow row = new();

                    row.Append(properties);

                    foreach (TableCell cell in cellList.Cast<OpenXmlElement>())
                        row.Append(cell);

                    stk.Peek().SetAttribute(0, row);
                    return;
                }
                if (action == Tag._CellList)
                {
                    List<object> cellList =
                        (List<object>)action.GetAttribute(0);
                    stk.ElementAt(1).SetAttribute(0, cellList);
                    return;
                }
                if (action == Tag._EndTable)
                {
                    Table table = (Table)action.GetAttribute(2);

                    List<object> elements = new();

                    // Legenda de tabelas em cima
                    if (!action.TestNullAttribute(1))
                        elements.Add((Paragraph)action.GetAttribute(1));

                    elements.Add(table);

                    stk.Peek().SetAttribute(0, elements);
                    return;
                }

                #endregion Table

                #region Footnote

                if (action == Tag._StartFootnote)
                {
                    #region Separator

                    Footnote footnote1 =
                        new() { Type = FootnoteEndnoteValues.Separator, Id = ((WordDocEnvironment)environment).WordDocument.footnoteId++ };

                    Paragraph paragraph1 =
                        new();

                    ParagraphProperties paragraphProperties1 =
                        new();
                    SpacingBetweenLines spacingBetweenLines1 =
                        new() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

                    paragraphProperties1.Append(spacingBetweenLines1);

                    Run run1 = new();
                    SeparatorMark separatorMark1 = new();

                    run1.Append(separatorMark1);

                    paragraph1.Append(paragraphProperties1);
                    paragraph1.Append(new Run(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" }));
                    paragraph1.Append(run1);
                    paragraph1.Append(new Run(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" }));

                    footnote1.Append(paragraph1);

                    #endregion Separator

                    #region Continuation Separator

                    Footnote footnote2 =
                        new() { Type = FootnoteEndnoteValues.ContinuationSeparator, Id = ((WordDocEnvironment)environment).WordDocument.footnoteId++ };

                    Paragraph paragraph2 =
                        new();

                    ParagraphProperties paragraphProperties2 =
                        new();
                    SpacingBetweenLines spacingBetweenLines2 =
                        new() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

                    paragraphProperties2.Append(spacingBetweenLines2);

                    Run run2 = new();

                    ContinuationSeparatorMark continuationSeparatorMark1 =
                        new();
                    run2.Append(continuationSeparatorMark1);

                    paragraph2.Append(paragraphProperties2);
                    paragraph2.Append(new Run(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" }));
                    paragraph2.Append(run2);
                    paragraph2.Append(new Run(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" }));

                    footnote2.Append(paragraph2);

                    #endregion Continuation Separator

                    Footnotes footnotes =
                        ((WordDocEnvironment)environment).WordDocument.AddFootnotesPartToPackage();

                    footnotes.Append(footnote1);
                    footnotes.Append(footnote2);

                    stk.ElementAt(5).SetAttribute(1, new Footnote() { Id = ((WordDocEnvironment)environment).WordDocument.footnoteId++ });
                    return;
                }
                if (action == Tag._Footnote)
                {
                    #region Criação da nota de rodapé (footnote)

                    Paragraph paragraph3 = new();

                    ParagraphProperties paragraphProperties3 =
                        new();
                    ParagraphStyleId paragraphStyleId1 =
                        new() { Val = "Textodenotaderodap" };

                    paragraphProperties3.Append(paragraphStyleId1);

                    paragraph3.Append(paragraphProperties3);

                    #region Inserção da marca de rodapé dentro do rodapé

                    FootnoteReferenceMark footnoteReferenceMark1 =
                        new();

                    List<object> runList1 = FootnoteMarkFormat(footnoteReferenceMark1);

                    foreach (OpenXmlElement run in runList1.Cast<OpenXmlElement>())
                        paragraph3.Append((Run)((Run)run).Clone()); // Com o Clone, tem que usar o cast

                    #endregion Inserção da marca de rodapé dentro do rodapé

                    #region Inserção de um espaço entre a marca de rodapé e o texto de rodapé

                    Run run6 = new();
                    Text text1 = new(" ") { Space = SpaceProcessingModeValues.Preserve };
                    run6.Append(text1);

                    paragraph3.Append(run6);

                    #endregion Inserção de um espaço entre a marca de rodapé e o texto de rodapé

                    #region Inserção do texto de rodapé

                    List<object> runList = (List<object>)action.GetAttribute(0);

                    foreach (OpenXmlElement element in runList.Cast<OpenXmlElement>())
                        paragraph3.Append(element);

                    #endregion Inserção do texto de rodapé

                    Footnote footnote = (Footnote)action.GetAttribute(1);

                    footnote.Append(paragraph3);

                    ((WordDocEnvironment?)environment)?.WordDocument?.TheMainDocumentPart?.FootnotesPart?.Footnotes.Append(footnote);

                    #endregion Criação da nota de rodapé (footnote)

                    #region Inserção da marca de rodapé dentro do texto

                    List<object> runList2 = FootnoteMarkFormat(new FootnoteReference() { Id = footnote.Id });

                    #endregion Inserção da marca de rodapé dentro do texto

                    stk.Peek().SetAttribute(0, runList2);
                    return;
                }

                #endregion Footnote

                #region Environment

                if (action == Tag._EnvironmentParameter)
                {
                    if (action.GetAttribute(0) is not null)
                    {
                        Dictionary<string, object> dic = (Dictionary<string, object>)action.GetAttribute(0);

                        if (dic.ContainsKey("NUMBER"))
                            _absNumberID = (int)dic["NUMBER"];

                        if (dic.ContainsKey("LEVEL"))
                            _level = (int)dic["LEVEL"];
                    }
                    return;
                }
                if (action == Tag._BeginEnvironment)
                {
                    string env = ((StringToken)tokens.Pop()).str;
                    stk.ElementAt(8).SetAttribute(0, env);

                    switch (((WordDocEnvironment)environment).ListType)
                    {
                        case ListTypeValues.None:
                            ((WordDocEnvironment)environment).Save();
                            ((WordDocEnvironment)environment).Level = _level >= 0 && _level <= 9 ? _level : 0;

                            ((WordDocEnvironment)environment).ListType =
                                WordDocument.ParseEnum<ListTypeValues>(env.ToUpper());

                            if (((WordDocEnvironment)environment).ListType == ListTypeValues.Enumerate)
                            {
                                _absNumberID = 1; // ((WordDocEnvironment)environment).WordDocument.GetNumberingId("Numerado");
                                ((WordDocEnvironment)environment).WordDocument.numberingId =
                                    ((WordDocEnvironment)environment).WordDocument.CreateNumberingInstance(
                                        _absNumberID >= 0 && _absNumberID <= 9 ? _absNumberID : 1
                                        );
                            }
                            else if (((WordDocEnvironment)environment).ListType == ListTypeValues.Itemize)
                            {
                                _absNumberID = 4; //  ((WordDocEnvironment)environment).WordDocument.GetNumberingId("Marcado");
                                ((WordDocEnvironment)environment).WordDocument.numberingId =
                                    ((WordDocEnvironment)environment).WordDocument.CreateNumberingInstance(
                                        _absNumberID >= 0 && _absNumberID <= 9 ? _absNumberID : 2
                                        );
                            }
                            break;

                        case ListTypeValues.Enumerate:
                            ((WordDocEnvironment)environment).Save();

                            if (((WordDocEnvironment)environment).ListType == ListTypeValues.Enumerate)
                            {
                                ((WordDocEnvironment)environment).IncrementLevel();
                            }
                            else if (((WordDocEnvironment)environment).ListType == ListTypeValues.Itemize)
                            {
                                _absNumberID = ((WordDocEnvironment)environment).WordDocument.GetNumberingId("Marcado");

                                ((WordDocEnvironment)environment).WordDocument.numberingId =
                                    ((WordDocEnvironment)environment).WordDocument.CreateNumberingInstance(
                                        _absNumberID >= 0 && _absNumberID <= 9 ? _absNumberID : 4
                                        );

                                ((WordDocEnvironment)environment).Level = _level >= 0 && _level <= 9 ? _level : 0;
                                ((WordDocEnvironment)environment).ListType = ListTypeValues.Enumerate;
                            }
                            break;

                        case ListTypeValues.Itemize:
                            ((WordDocEnvironment)environment).Save();

                            if (((WordDocEnvironment)environment).ListType == ListTypeValues.Itemize)
                            {
                                ((WordDocEnvironment)environment).IncrementLevel();
                            }
                            else if (((WordDocEnvironment)environment).ListType == ListTypeValues.Enumerate)
                            {
                                _absNumberID = ((WordDocEnvironment)environment).WordDocument.GetNumberingId("Numerado");
                                ((WordDocEnvironment)environment).WordDocument.numberingId =
                                    ((WordDocEnvironment)environment).WordDocument.CreateNumberingInstance(
                                        _absNumberID >= 0 && _absNumberID <= 9 ? _absNumberID : 0
                                        );

                                ((WordDocEnvironment)environment).Level = _level >= 0 && _level <= 9 ? _level : 0;
                                ((WordDocEnvironment)environment).ListType = ListTypeValues.Itemize;
                            }
                            break;
                    }
                    return;
                }
                if (action == Tag._EndEnvironment)
                {
                    string previous = (string)action.GetAttribute(0);
                    string env = ((StringToken)tokens.Pop()).str;

                    if (env != previous)
                    {
                        throw new NotImplementedException("Não implementada!");
                    }
                    else
                    {
                        ((WordDocEnvironment)environment).Restore();
                        _absNumberID = -1;
                        _level = -1;
                    }
                    return;
                }
                if (action == Tag._EndBegin)
                {
                    stk.Peek().SetAttribute(0, action.GetAttribute(0));
                    return;
                }

                #endregion Environment

                #region Code

                if (action == Tag._Code)
                {
                    string key = (string)action.GetAttribute(0);

                    key = key.ToUpper();

                    if (((WordDocEnvironment)environment).WordDocument.codes.ContainsKey(key))
                    {
                        string text = ((WordDocEnvironment)environment).WordDocument.codes[key];

                        text += "\\paragraph[AFTER=100]{\\run{ }}\n";

                        // var result = ((PPCModel)((WordDocEnvironment)environment).WordDocument).CreateFromText(text, new WordDocEnvironment((WordDocEnvironment)environment));
                        var result = PPCModel.CreateFromText(text, new WordDocEnvironment((WordDocEnvironment)environment));

                        if (!result)
                        {
                            PPCModel.CreateFromText(
                                "\\paragraph[JUSTIFICATION=left;FIRSTLINE=0]{\\run[BOLD;COLOR=16711680]{Sintaxe incorreta em \"" + key + "\"!}}",
                                new WordDocEnvironment((WordDocEnvironment)environment));
                        }
                    }
                    else
                    {
                        PPCModel.CreateFromText(
                            "\\paragraph[JUSTIFICATION=left;FIRSTLINE=0]{\\run[BOLD;COLOR=16711680]{Código \"" + key + "\" NÃO ENCONTRADO!}}",
                            new WordDocEnvironment((WordDocEnvironment)environment));
                    }

                    stk.Peek().SetAttribute(0, new List<object>());
                    return;
                }
                // if (action == Tag._BeginCode)
                // {
                //     ((WordDocEnvironment)environment).ChangeContext(Context.CodeEnv);
                //     return;
                // }
                // if (action == Tag._Code)
                // {
                //     StringToken key = (StringToken)tokens.Pop();
                //
                //     string str;
                //
                //     RunProperties rPr;
                //     Run run;
                //
                //     if (((WordDocEnvironment)environment).WordDocument.codes.ContainsKey(key.str.ToUpper()))
                //     {
                //         rPr = new(
                //             new Underline() { Val = UnderlineValues.Dotted },
                //             new Color() { Val = "00FF00" }
                //             );
                //
                //         str = ((WordDocEnvironment)environment).WordDocument.codes[key.str.ToUpper()];
                //         str = $"(Tabela: {key.str})";
                //     }
                //     else
                //     {
                //         // RunProperties rPr = new RunProperties(new Bold());
                //         rPr = new(
                //             new Color() { Val = "FF0000" }
                //             );
                //         str = $"({key.str}: Não encontrado)";
                //     }
                //
                //     run = new Run(rPr);
                //     run.Append(new Text(str) { Space = SpaceProcessingModeValues.Preserve });
                //
                //     stk.ElementAt(2).SetAttribute(0, new List<object>());
                //     return;
                // }
                // if (action == Tag._EndCode)
                // {
                //     ((WordDocEnvironment)environment).RestoreContext();
                //     string txt = ((StringToken)tokens.Pop()).str;
                //
                //     stk.ElementAt(1).SetAttribute(0, txt);
                //     return;
                // }

                #endregion Code

                #region Label (Não implementado)

                if (action == Tag._Label)
                {
                    _ = ((StringToken)tokens.Pop()).str;
                    return;
                }
                if (action == Tag._NoLabel)
                {
                    return;
                }

                #endregion Label (Não implementado)

                #region Equation (Não implementado)

                if (action == Tag._EndEquation)
                {
                    Paragraph paragraph = (Paragraph)action.GetAttribute(2);

                    List<object> elements = new() { paragraph };

                    if (!action.TestNullAttribute(1))
                        elements.Add((Paragraph)action.GetAttribute(1));

                    stk.Peek().SetAttribute(0, elements);
                    return;
                }

                #endregion Equation (Não implementado)

                #region Não Implementadas

                if (action == Tag._Ref)
                {
                    string str = ((StringToken)tokens.Pop()).str;

                    throw new NotImplementedException("Não implementada!");
                }
                if (action == Tag._PageRef)
                {
                    string str = ((StringToken)tokens.Pop()).str;

                    throw new NotImplementedException("Não implementada!");
                }
                if (action == Tag._BeginVerbatim)
                {
                    ((WordDocEnvironment)environment).ChangeContext(Context.VerbatimEnv);

                    throw new NotImplementedException("Não implementada!");
                }
                if (action == Tag._EndVerbatim)
                {
                    ((WordDocEnvironment)environment).RestoreContext();
                    stk.ElementAt(1).SetAttribute(0, ((StringToken)tokens.Pop()).str);

                    throw new NotImplementedException("Não implementada!");
                }
                if (action == Tag._Verbatim)
                {
                    string xml = (string)action.GetAttribute(0);

                    throw new NotImplementedException("Não implementada!");
                }

                #endregion Não Implementadas
            }
            catch (Exception ex)
            {
                Console.WriteLine(action);
                Console.WriteLine(ex.Message);
            }
        }

        private static List<object> FootnoteMarkFormat(OpenXmlElement element)
        {
            List<object> runList = new();

            //
            Run run1 = new();

            RunProperties runProperties1 = new();
            RunStyle runStyle1 = new() { Val = "Refdenotaderodap" };

            runProperties1.Append(runStyle1);
            // FootnoteReferenceMark footnoteReferenceMark1 = new FootnoteReferenceMark();

            VerticalTextAlignment verticalMarkAlignment1 =
                new() { Val = VerticalPositionValues.Superscript };

            runProperties1.Append(verticalMarkAlignment1);

            run1.Append(runProperties1);
            // run1.Append(footnoteReferenceMark1);

            run1.Append(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" });

            //
            Run run2 = new();

            RunProperties runProperties2 = new();
            RunStyle runStyle2 =
                new() { Val = "Refdenotaderodap" };

            runProperties2.Append(runStyle2);

            VerticalTextAlignment verticalMarkAlignment2 =
                new() { Val = VerticalPositionValues.Superscript };

            runProperties2.Append(verticalMarkAlignment2);

            run2.Append(runProperties2);
            run2.Append(element);

            //
            Run run3 = new();

            RunProperties runProperties3 = new();
            RunStyle runStyle3 = new() { Val = "Refdenotaderodap" };

            runProperties3.Append(runStyle3);
            // FootnoteReferenceMark footnoteReferenceMark3 = new FootnoteReferenceMark();

            VerticalTextAlignment verticalMarkAlignment3 =
                new() { Val = VerticalPositionValues.Superscript };

            runProperties3.Append(verticalMarkAlignment3);

            run3.Append(runProperties3);

            run3.Append(new Text() { Space = SpaceProcessingModeValues.Preserve, Text = "-" });

            runList.Add(run1);
            runList.Add(run2);
            runList.Add(run3);

            return runList;
        }

        private static string TableNotFound(string key)
            => "\\default[\n" +
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
               "\n" +
               "\\table{\n" +
               "    \\row{\n" +
               "        \\cell[SHADING=Percent10; TABLECELLWIDTH=700; TABLECELLVERTICALALIGNMENT=Center]{\n" +
               "            \\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{ }}\n" +
               "        }\n" +
               "    }\n" +
               "\n" +
               $"    \\caption[BEFORE=300;JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=360]{{Tabela {key} não encontrada!}}\n" +
               "\n" +
               "}\n" +
               "\n" +
               "\\default{paragraph}\n" +
               "\\default{tablecell}\n" +
               "\\default{table}\n";
    }
}