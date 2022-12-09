// using Ardalis.GuardClauses;
//
// using DocumentFormat.OpenXml;
// using DocumentFormat.OpenXml.Packaging;
// using DocumentFormat.OpenXml.Wordprocessing;
//
// using System.Xml;
//
// namespace PraeceptorCQRS.Infrastructure.Parsers.Document;
//
// public partial class WordDocument
// {
//     public static string NSID
//         => Guid.NewGuid().ToString("D")[..8].ToUpper();
//
//     public int footnoteId = 0;
//     public int figureId = 0;
//     public int tableId = 0;
//     public int equationId = 0;
//     public int numberingId = 0;
//
//     private int bookmark = 0;
//     public string NewBookmark() { return (bookmark++).ToString(); }
//
//     #region Default Properties
//     public ParagraphProperties DefaultParagraphProperties { get; set; } = new ParagraphProperties();
//     public TableCellProperties DefaultTableCellProperties { get; set; } = new TableCellProperties();
//     public TableRowProperties DefaultTableRowProperties { get; set; } = new TableRowProperties();
//     public RunProperties DefaultRunProperties { get; set; } = new RunProperties();
//     public TableProperties DefaultTableProperties { get; set; } = new TableProperties();
//     #endregion
//
//     public int GetNumberingId(string styleName)
//     {
//         var styles = MainDocumentPart.StyleDefinitionsPart!.Styles;
//
//         foreach (var s in styles!)
//             if (s is Style style)
//                 if (style.StyleId == styleName)
//                     foreach (var e in style.ChildElements)
//                         if (e is StyleParagraphProperties element)
//                             foreach (var c in element.ChildElements)
//                                 if (c is NumberingProperties child)
//                                     return child.NumberingId?.Val!;
//         return -1;
//     }
//
//     #region Add Parts to Package
//     public string AddCommentPartToPackage()
//     {
//         string? id = "0";
//
//         // Verify that the document contains a
//         // WordProcessingCommentsPart part; if not, add a new one.
//         if (MainDocumentPart.WordprocessingCommentsPart != null)
//         {
//             Comments comments =
//                 MainDocumentPart.WordprocessingCommentsPart.Comments;
//
//             if (comments.HasChildren)
//             {
//                 // Obtain an unused ID.
//                 id = comments.Descendants<Comment>().Select(e => e.Id?.Value).Max();
//             }
//         }
//         else
//         {
//             // No WordprocessingCommentsPart part exists, so add one to the package.
//             WordprocessingCommentsPart commentPart =
//                 MainDocumentPart.AddNewPart<WordprocessingCommentsPart>();
//             commentPart.Comments = new Comments();
//         }
//
//         Guard.Against.Null(id);
//
//         return id;
//     }
//
//     public StyleDefinitionsPart AddStylesPartToPackage(Styles root = null!)
//     {
//         StyleDefinitionsPart part;
//         part = MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
//         if (root == null)
//             root = new Styles();
//         root.Save(part);
//         return part;
//     }
//
//     public string AddNumberingPartToPackage()
//     {
//         int id = 0;
//
//         // Verify that the document contains a
//         // WordProcessingCommentsPart part; if not, add a new one.
//         if (MainDocumentPart.NumberingDefinitionsPart is not null)
//         {
//             Numbering numbering =
//                 MainDocumentPart.NumberingDefinitionsPart.Numbering;
//
//             if (numbering.HasChildren)
//             {
//                 id = numbering.Descendants<AbstractNum>().Select(e => e.AbstractNumberId!.Value).Max();
//             }
//         }
//         else
//         {
//             // No WordprocessingCommentsPart part exists, so add one to the package.
//             NumberingDefinitionsPart numberingPart =
//                 MainDocumentPart.AddNewPart<NumberingDefinitionsPart>();
//             numberingPart.Numbering = new Numbering();
//         }
//
//         return id.ToString();
//     }
//
//     public Footnotes AddFootnotesPartToPackage(Footnotes _ = null!)
//     {
//         Footnotes footnotes;
//
//         // Verify that the document contains a
//         // WordProcessingCommentsPart part; if not, add a new one.
//         if (MainDocumentPart.FootnotesPart != null)
//         {
//             footnotes =
//                 MainDocumentPart.FootnotesPart.Footnotes;
//         }
//         else
//         {
//             // No WordprocessingCommentsPart part exists, so add one to the package.
//             FootnotesPart footnotesPart =
//                 MainDocumentPart.AddNewPart<FootnotesPart>();
//             footnotes = new Footnotes();
//             footnotesPart.Footnotes = footnotes;
//         }
//
//         return footnotes;
//     }
//
//     public string AddGlossaryPartToPackage()
//     {
//         int id = 0;
//
//         if (MainDocumentPart.GlossaryDocumentPart is not null)
//         {
//             GlossaryDocument glossary =
//                 MainDocumentPart.GlossaryDocumentPart.GlossaryDocument;
//
//             if (glossary.HasChildren)
//             {
//                 id = glossary.Descendants<AbstractNum>().Select(e => e.AbstractNumberId!.Value).Max();
//             }
//         }
//         else
//         {
//             GlossaryDocumentPart glossaryPart =
//                 MainDocumentPart.AddNewPart<GlossaryDocumentPart>();
//             glossaryPart.GlossaryDocument = new GlossaryDocument();
//         }
//
//         return id.ToString();
//     }
//
//     public HeaderPart AddHeaderPartToPackage(List<Header> root = null!)
//     {
//         HeaderPart part =
//             MainDocumentPart.AddNewPart<HeaderPart>();
//
//         root ??= new List<Header>();
//
//         foreach (Header header in root)
//             header.Save(part);
//
//         return part;
//     }
//
//     public string AddStyleDefinitionsPartToPackage()
//     {
//         string? id = "";
//
//         // Verify that the document contains a
//         // WordProcessingCommentsPart part; if not, add a new one.
//         if (MainDocumentPart.StyleDefinitionsPart != null)
//         {
//             Styles? styles =
//                 MainDocumentPart.StyleDefinitionsPart.Styles;
//
//             if (styles is not null && styles.HasChildren)
//             {
//                 id = styles.Descendants<Style>().Select(e => e.StyleId?.Value).Max();
//             }
//         }
//         else
//         {
//             // No WordprocessingCommentsPart part exists, so add one to the package.
//             StyleDefinitionsPart styleDefinitionsPart =
//                 MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
//             styleDefinitionsPart.Styles = new Styles();
//         }
//
//         Guard.Against.Null(id);
//
//         return id;
//     }
//     #endregion
//
//     #region Properties from Parameters
//     public static T ParseEnum<T>(string value)
//     {
//         try
//         {
//             return (T)Enum.Parse(typeof(T), value, true);
//         }
//         catch (ArgumentNullException)
//         {
//             return default!;
//         }
//         catch (ArgumentException)
//         {
//             return default!;
//         }
//         catch (OverflowException)
//         {
//             return default!;
//         }
//     }
//
//     public ParagraphProperties ParagraphPropertiesFromParameters(Dictionary<string, object> list)
//     {
//         ParagraphProperties paragraphProperties = new();
//
//         Indentation indentation = default!;
//         SpacingBetweenLines spacingBetweenLines = default!;
//
//         foreach (string key in list.Keys)
//         {
//             if (key == "KEEPNEXT")
//                 paragraphProperties.Append(new KeepNext());
//             else if (key == "KEEPLINES")
//                 paragraphProperties.Append(new KeepLines());
//             else if (key == "PAGEBREAKBEFORE")
//                 paragraphProperties.Append(new PageBreakBefore() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "SHADING" && list[key] is string @string1)
//                 paragraphProperties.Append(new Shading() { Val = ParseEnum<ShadingPatternValues>(@string1) });
//             else if (key == "WIDOWCONTROL")
//                 paragraphProperties.Append(new WidowControl() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "SUPPRESSLINENUMBERS")
//                 paragraphProperties.Append(new SuppressLineNumbers() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "SUPPRESSAUTOHYPHENS")
//                 paragraphProperties.Append(new SuppressAutoHyphens() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "WIDOWCONTROL")
//                 paragraphProperties.Append(new Kinsoku() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "WORDWRAP")
//                 paragraphProperties.Append(new WordWrap() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "OVERFLOWPUNCTUATION")
//                 paragraphProperties.Append(new OverflowPunctuation() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "TOPLINEPUNCTUATION")
//                 paragraphProperties.Append(new TopLinePunctuation() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "AUTOSPACEDE")
//                 paragraphProperties.Append(new AutoSpaceDE() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "AUTOSPACEDN")
//                 paragraphProperties.Append(new AutoSpaceDN() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "BIDI")
//                 paragraphProperties.Append(new BiDi() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "ADJUSTRIGHTINDENT")
//                 paragraphProperties.Append(new AdjustRightIndent() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "SNAPTOGRID")
//                 paragraphProperties.Append(new SnapToGrid() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "CONTEXTUALSPACING")
//                 paragraphProperties.Append(new ContextualSpacing() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "MIRRORINDENTS")
//                 paragraphProperties.Append(new MirrorIndents() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "SUPPRESSOVERLAP")
//                 paragraphProperties.Append(new SuppressOverlap() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "JUSTIFICATION" && list[key] is string @string2)
//                 paragraphProperties.Append(new Justification() { Val = ParseEnum<JustificationValues>(@string2) });
//             else if (key == "TEXTALIGNMENT" && list[key] is string @string3)
//                 paragraphProperties.Append(new TextAlignment() { Val = ParseEnum<VerticalTextAlignmentValues>(@string3) });
//             else if (key == "TEXTBOXTIGHTWRAP" && list[key] is string @string4)
//                 paragraphProperties.Append(new TextBoxTightWrap() { Val = ParseEnum<TextBoxTightWrapValues>(@string4) });
//             else if (key == "OUTLINELEVEL" && list[key] is int @int1)
//                 paragraphProperties.Append(new OutlineLevel() { Val = (Int32Value)@int1 });
//             else if (key == "TOPBORDER" && list[key] is string @string5)
//                 paragraphProperties.Append(new TopBorder() { Val = ParseEnum<BorderValues>(@string5) });
//             else if (key == "BOTTOMBORDER" && list[key] is string @string6)
//                 paragraphProperties.Append(new BottomBorder() { Val = ParseEnum<BorderValues>(@string6) });
//             else if (key == "LEFTBORDER" && list[key] is string @string7)
//                 paragraphProperties.Append(new LeftBorder() { Val = ParseEnum<BorderValues>(@string7) });
//             else if (key == "RIGHTBORDER" && list[key] is string @string8)
//                 paragraphProperties.Append(new RightBorder() { Val = ParseEnum<BorderValues>(@string8) });
//             else if (key == "TEXTDIRECTION" && list[key] is string @string9)
//             {
//                 TextDirectionValues val = ParseEnum<TextDirectionValues>(@string9);
//                 if (val != default)
//                     paragraphProperties.Append(new TextDirection() { Val = val });
//             }
//             else if (key == "BORDER" && list[key] is string @string10)
//             {
//                 BorderValues val = ParseEnum<BorderValues>(@string10);
//                 paragraphProperties.Append(new TopBorder() { Val = val });
//                 paragraphProperties.Append(new BottomBorder() { Val = val });
//                 paragraphProperties.Append(new LeftBorder() { Val = val });
//                 paragraphProperties.Append(new RightBorder() { Val = val });
//             }
//             else if (key == "CONDITIONALFORMATSTYLE" && list[key] is string)
//             {
//                 throw new NotImplementedException();
//             }
//             else if (key == "FIRSTLINE" && list[key] is int @int2)
//             {
//                 indentation ??= new();
//                 indentation.FirstLine = @int2.ToString();
//             }
//             else if (key == "HANGING" && list[key] is int @int3)
//             {
//                 indentation ??= new();
//                 indentation.Hanging = @int3.ToString();
//             }
//             else if (key == "LEFT" && list[key] is int @int4)
//             {
//                 indentation ??= new();
//                 indentation.Left = @int4.ToString();
//             }
//             else if (key == "RIGHT" && list[key] is int @int5)
//             {
//                 indentation ??= new();
//                 indentation.Right = @int5.ToString();
//             }
//             else if (key == "BEFORE" && list[key] is int @int6)
//             {
//                 spacingBetweenLines ??= new();
//                 spacingBetweenLines.Before = @int6.ToString();
//             }
//             else if (key == "BEFORELINES" && list[key] is int @int7)
//             {
//                 spacingBetweenLines ??= new();
//                 spacingBetweenLines.BeforeLines = @int7;
//             }
//             else if (key == "AFTER" && list[key] is int @int8)
//             {
//                 spacingBetweenLines ??= new();
//                 spacingBetweenLines.After = @int8.ToString();
//             }
//             else if (key == "AFTERLINES" && list[key] is int @int9)
//             {
//                 if (spacingBetweenLines == null)
//                     spacingBetweenLines = new();
//                 spacingBetweenLines.AfterLines = @int9;
//             }
//             else if (key == "SPACINGBETWEENLINES" && list[key] is int @int10)
//             {
//                 paragraphProperties.Append(
//                     new SpacingBetweenLines() { After = "0", Line = @int10.ToString(), LineRule = LineSpacingRuleValues.Auto }
//                     );
//             }
//
//             // new FrameProperties            () { };
//             // new NumberingProperties        () { };
//             // new Tabs                       () { };
//             // new ParagraphMarkRunProperties () { };
//             // new SectionProperties          () { };
//             // new ParagraphPropertiesChange  () { };
//         }
//
//         // for (int i = 0; i < 10; i++)
//         //     paragraphProperties.Append(new Indentation() { Hanging = (-200 * i).ToString() });
//
//         if (indentation != null)
//             paragraphProperties.Append(indentation);
//         if (spacingBetweenLines != null)
//             paragraphProperties.Append(spacingBetweenLines);
//
//         if (!paragraphProperties.HasChildren && DefaultParagraphProperties.HasChildren)
//             paragraphProperties = (ParagraphProperties)DefaultParagraphProperties.Clone();
//
//         return paragraphProperties;
//     }
//
//     public RunProperties RunPropertiesFromParameters(Dictionary<string, object> list)
//     {
//         RunProperties runProperties = new();
//
//         foreach (string key in list.Keys)
//         {
//             if (key == "BOLD")
//                 runProperties.Append(new Bold());
//             else if (key == "CAPS")
//                 runProperties.Append(new Caps());
//             else if (key == "BORDER" && list[key] is string string1)
//                 runProperties.Append(new Border() { Val = ParseEnum<BorderValues>(string1) });
//             else if (key == "COLOR" && list[key] is int @int)
//                 runProperties.Append(new Color() { Val = string.Format("{0:X6}", @int) });
//             else if (key == "DOUBLESTRIKE")
//                 runProperties.Append(new DoubleStrike());
//             else if (key == "EMBOSS")
//                 runProperties.Append(new Emboss());
//             else if (key == "CHARACTERSCALE" && (list[key] is double || list[key] is int) && (double)list[key] > 0 && (double)list[key] <= 1)
//                 runProperties.Append(new CharacterScale() { Val = new IntegerValue((long)((double)list[key] * 100)) });
//             else if (key == "EMPHASIS" && list[key] is string @string)
//                 runProperties.Append(new Emphasis() { Val = ParseEnum<EmphasisMarkValues>(@string) });
//             else if (key == "FONTSIZE" && list[key] is int int1)
//                 runProperties.Append(new FontSize() { Val = string.Format("{0}pt", int1) });
//             else if (key == "IMPRINT")
//                 runProperties.Append(new Imprint());
//             else if (key == "ITALIC")
//                 runProperties.Append(new Italic());
//             else if (key == "OUTLINE")
//                 runProperties.Append(new Outline());
//             else if (key == "SHADING" && list[key] is string string2)
//                 runProperties.Append(new Shading() { Val = ParseEnum<ShadingPatternValues>(string2) });
//             else if (key == "SHADOW")
//                 runProperties.Append(new Shadow());
//             else if (key == "SMALLCAPS")
//                 runProperties.Append(new SmallCaps());
//             else if (key == "STRIKE")
//                 runProperties.Append(new Strike());
//             else if (key == "UNDERLINE" && list[key] is string string3)
//                 runProperties.Append(new Underline() { Val = ParseEnum<UnderlineValues>(string3) });
//             else if (key == "VANISH")
//                 runProperties.Append(new Vanish());
//             else if (key == "SPACING" && list[key] is int int2)
//                 runProperties.Append(new Spacing() { Val = int2 });
//             else if (key == "SPECVANISH")
//                 runProperties.Append(new SpecVanish());
//             else if (key == "VERTICALTEXTALIGNMENT" && list[key] is string string4)
//                 runProperties.Append(new VerticalTextAlignment() { Val = ParseEnum<VerticalPositionValues>(string4) });
//             else if (key == "POSITION" && list[key] is int int3)
//                 runProperties.Append(new Position() { Val = int3.ToString() });
//             else if (key == "FONTSIZECOMPLEXSCRIPT" && list[key] is int int4)
//                 runProperties.Append(new FontSizeComplexScript() { Val = int4.ToString() });
//         }
//
//         if (!runProperties.HasChildren && DefaultRunProperties.HasChildren)
//             runProperties = (RunProperties)DefaultRunProperties.Clone();
//
//         return runProperties;
//     }
//
//     public TableProperties TablePropertiesFromParameters(Dictionary<string, object> list)
//     {
//         TableProperties properties = new();
//
//         TableBorders tblBorders = new();
//         TableCellMarginDefault tableCellMarginDefault = new();
//
//         foreach (string key in list.Keys)
//         {
//             if (key == "TOPBORDER" && list[key] is string @string1)
//                 tblBorders.Append(new TopBorder() { Val = ParseEnum<BorderValues>(@string1) });
//             else if (key == "BOTTOMBORDER" && list[key] is string @string2)
//                 tblBorders.Append(new BottomBorder { Val = ParseEnum<BorderValues>(@string2) });
//             else if (key == "LEFTBORDER" && list[key] is string @string3)
//                 tblBorders.Append(new LeftBorder { Val = ParseEnum<BorderValues>(@string3) });
//             else if (key == "RIGHTBORDER" && list[key] is string @string4)
//                 tblBorders.Append(new RightBorder { Val = ParseEnum<BorderValues>(@string4) });
//             else if (key == "INSIDEHORIZONTALBORDER" && list[key] is string @string5)
//                 tblBorders.Append(new InsideHorizontalBorder { Val = ParseEnum<BorderValues>(@string5) });
//             else if (key == "INSIDEVERTICALBORDER" && list[key] is string @string6)
//                 tblBorders.Append(new InsideVerticalBorder { Val = ParseEnum<BorderValues>(@string6) });
//             else if (key == "TABLECELLLEFTMARGIN" && list[key] is int @int1)
//                 tableCellMarginDefault.Append(new TableCellLeftMargin() { Width = Int16Value.FromInt16((short)@int1), Type = TableWidthValues.Dxa });
//             else if (key == "TABLECELLRIGHTMARGIN" && list[key] is int @int2)
//                 tableCellMarginDefault.Append(new TableCellRightMargin() { Width = Int16Value.FromInt16((short)@int2), Type = TableWidthValues.Dxa });
//             else if (key == "TABLEOVERLAP" && list[key] is string @string7)
//                 properties.Append(new TableOverlap() { Val = ParseEnum<TableOverlapValues>(@string7) });
//             else if (key == "JUSTIFICATION" && list[key] is string @string8)
//                 properties.Append(new Justification() { Val = ParseEnum<JustificationValues>(@string8) });
//             else if (key == "TABLECELLSPACING" && list[key] is int @int3)
//                 properties.Append(new TableCellSpacing() { Width = @int3.ToString() });
//             else if (key == "TABLEINDENTATION" && list[key] is int @int4)
//                 properties.Append(new TableIndentation() { Width = @int4, Type = TableWidthUnitValues.Dxa });
//             else if (key == "BIDIVISUAL")
//                 properties.Append(new BiDiVisual() { Val = OnOffOnlyValues.On });
//             else if (key == "TABLEDESCRIPTION" && list[key] is string @string9)
//                 properties.Append(new TableDescription() { Val = @string9 });
//
//             // TableLook
//             // TablePropertiesChange
//         }
//
//         if (tblBorders.HasChildren)
//             properties.Append(tblBorders);
//         if (tableCellMarginDefault.HasChildren)
//             properties.Append(tableCellMarginDefault);
//
//         // // Default Values
//         // if (properties.Descendants<TableBorders>().Count() == 0)
//         // {
//         //     TableBorders tableBorders =
//         //         new TableBorders
//         //         {
//         //             TopBorder = new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Double) },
//         //             BottomBorder = new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Double) },
//         //             LeftBorder = new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Double) },
//         //             RightBorder = new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Double) },
//         //             InsideHorizontalBorder = new InsideHorizontalBorder { Val = BorderValues.Single },
//         //             InsideVerticalBorder = new InsideVerticalBorder { Val = BorderValues.Single }
//         };
//     properties.Append(tableBorders);
// }

// if (properties.Descendants<TableCellMarginDefault>().Count() == 0)
// {
//     TableCellLeftMargin tableCellLeftMargin = new() { Width = 100, Type = TableWidthValues.Dxa };
//     TableCellRightMargin tableCellRightMargin = new() { Width = 100, Type = TableWidthValues.Dxa };
//
//     tableCellMarginDefault.Append(tableCellLeftMargin);
//     tableCellMarginDefault.Append(tableCellRightMargin);
//
//     properties.Append(tableCellMarginDefault);
// }
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
// if (!properties.HasChildren && DefaultTableProperties.HasChildren)
//             properties = (TableProperties)DefaultTableProperties.Clone();
//
//         // TableLook
//         if (!properties.Descendants<TableLook>().Any())
//             properties.Append(new TableLook()
//             {
//                 Val = "04A0",
//                 FirstRow = true,
//                 LastRow = false,
//                 FirstColumn = true,
//                 LastColumn = false,
//                 NoHorizontalBand = false,
//                 NoVerticalBand = true
//             });
//
//         // // TableOverlap
//         // if (properties.Descendants<TableOverlap>().Count() == 0)
//         //     properties.Append(new TableOverlap() { Val = TableOverlapValues.Never });
//
//         // if (properties.Descendants<Justification>().Count() == 0)
//         //     properties.Append(new Justification() { Val = JustificationValues.Center });
//
//         // NÃO PODE PORQUE SENÃO VAI MISTURAR COM O TEXTO
//         // TablePositionProperties tablePositionProperties = new TablePositionProperties()
//         // {
//         //     LeftFromText = 141,
//         //     RightFromText = 141,
//         //     VerticalAnchor = VerticalAnchorValues.Text,
//         //     TablePositionXAlignment = HorizontalAlignmentValues.Center,
//         //     TablePositionY = 1
//         // };
//
//         // properties.Append(tablePositionProperties);
//
//         return properties;
//     }
//
//     public TableCellProperties CellPropertiesFromParameters(Dictionary<string, object> list)
//     {
//         TableCellProperties properties = new();
//
//         TableCellMargin tableCellMargin = new();
//         TableCellBorders tableCellBorders = new();
//
//         foreach (string key in list.Keys)
//         {
//             #region TableCellMargin
//             if (key == "BOTTOMMARGIN" && list[key] is int @int1)
//                 tableCellMargin.Append(new BottomMargin() { Width = @int1.ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "TOPMARGIN" && list[key] is int @int2)
//                 tableCellMargin.Append(new TopMargin() { Width = @int2.ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "LEFTMARGIN" && list[key] is int @int3)
//                 tableCellMargin.Append(new LeftMargin() { Width = @int3.ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "RIGHTMARGIN" && list[key] is int @int4)
//                 tableCellMargin.Append(new RightMargin() { Width = @int4.ToString(), Type = TableWidthUnitValues.Dxa });
//             #endregion
//             else if (key == "TABLECELLWIDTH" && list[key] is int @int5)
//                 properties.Append(new TableCellWidth() { Width = @int5.ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "GRIDSPAN" && list[key] is int)
//                 properties.Append(new GridSpan() { Val = (Int32Value)list[key] });
//             #region Merge
//             else if (key == "HORIZONTALMERGE" && list[key] is string @string1)
//                 properties.Append(new HorizontalMerge() { Val = ParseEnum<MergedCellValues>(@string1) });
//             else if (key == "VERTICALMERGE" && list[key] is string @string2)
//                 properties.Append(new VerticalMerge() { Val = ParseEnum<MergedCellValues>(@string2) });
//             else if (key == "CELLMERGE" && list[key] is string @string3)
//                 properties.Append(new CellMerge() { VerticalMerge = ParseEnum<VerticalMergeRevisionValues>(@string3) });
//             #endregion
//             #region Borders
//             else if (key == "CELLBORDERS" && list[key] is string @string4)
//             {
//                 BorderValues val = ParseEnum<BorderValues>(@string4);
//                 tableCellBorders.Append(new TopBorder() { Val = val });
//                 tableCellBorders.Append(new BottomBorder() { Val = val });
//                 tableCellBorders.Append(new LeftBorder() { Val = val });
//                 tableCellBorders.Append(new RightBorder() { Val = val });
//             }
//             else if (key == "TOPBORDER" && list[key] is string @string5)
//                 tableCellBorders.Append(new TopBorder() { Val = ParseEnum<BorderValues>(@string5) });
//             else if (key == "BOTTOMBORDER" && list[key] is string @string6)
//                 tableCellBorders.Append(new BottomBorder() { Val = ParseEnum<BorderValues>(@string6) });
//             else if (key == "LEFTBORDER" && list[key] is string @string7)
//                 tableCellBorders.Append(new LeftBorder() { Val = ParseEnum<BorderValues>(@string7) });
//             else if (key == "RIGHTBORDER" && list[key] is string @string8)
//                 tableCellBorders.Append(new RightBorder() { Val = ParseEnum<BorderValues>(@string8) });
//             #endregion
//             else if (key == "SHADING" && list[key] is string @string9)
//                 properties.Append(new Shading() { Val = ParseEnum<ShadingPatternValues>(@string9) });
//             else if (key == "NOWRAP")
//                 properties.Append(new NoWrap());
//             else if (key == "TEXTDIRECTION" && list[key] is string @string10)
//                 properties.Append(new TextDirection() { Val = ParseEnum<TextDirectionValues>(@string10) });
//             else if (key == "TABLECELLFITTEXT" && list[key] is string @string11)
//                 properties.Append(new TableCellFitText() { Val = ParseEnum<OnOffOnlyValues>(@string11) });
//             else if (key == "TABLECELLVERTICALALIGNMENT" && list[key] is string @string12)
//                 properties.Append(new TableCellVerticalAlignment() { Val = ParseEnum<TableVerticalAlignmentValues>(@string12) });
//             else if (key == "HIDEMARK" && list[key] is string @string13)
//                 properties.Append(new HideMark() { Val = ParseEnum<OnOffOnlyValues>(@string13) });
//             else if (key == "CONDITIONALFORMATSTYLE")
//                 throw new NotImplementedException(); // cellProperties.Append(new ConditionalFormatStyle());
//             else if (key == "CELLINSERTION")
//                 throw new NotImplementedException(); // cellProperties.Append(new CellInsertion());
//             else if (key == "CELLDELETION")
//                 throw new NotImplementedException(); // cellProperties.Append(new CellDeletion());
//             else if (key == "TABLECELLPROPERTIESCHANGE")
//                 throw new NotImplementedException(); // cellProperties.Append(new TableCellPropertiesChange());
//         }
//
//         if (tableCellMargin.HasChildren)
//             properties.Append(tableCellMargin);
//         if (tableCellBorders.HasChildren)
//             properties.Append(tableCellBorders);
//
//         if (!properties.HasChildren && DefaultTableCellProperties.HasChildren)
//             properties = (TableCellProperties)DefaultTableCellProperties.Clone();
//
//         // Default
//         // if (properties.Descendants<TableCellMargin>().Count() == 0)
//         // {
//         //     tableCellMargin.Append(new LeftMargin() { Width = "100", Type = TableWidthUnitValues.Dxa });
//         //     tableCellMargin.Append(new RightMargin() { Width = "100", Type = TableWidthUnitValues.Dxa });
//         //     properties.Append(tableCellMargin);
//         // }
//
//         // if (properties.Descendants<TableCellVerticalAlignment>().Count() == 0)
//         // {
//         //     TableCellVerticalAlignment tableCellVerticalAlignment = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
//         //     properties.Append(tableCellVerticalAlignment);
//         // }
//
//         return properties;
//     }
//
//     public TableRowProperties RowPropertiesFromParameters(Dictionary<string, object> list)
//     {
//         TableRowProperties properties = new();
//
//         foreach (string key in list.Keys)
//         {
//             if (key == "CONDITIONALFORMATSTYLE")
//                 throw new NotImplementedException(); // properties.Append(new ConditionalFormatStyle() { Val });
//             else if (key == "DIVID")
//                 throw new NotImplementedException(); // properties.Append(new DivId() {  });
//             else if (key == "GRIDBEFORE" && list[key] is int)
//                 properties.Append(new GridBefore() { Val = (Int32Value)list[key] });
//             else if (key == "GRIDAFTER" && list[key] is int)
//                 properties.Append(new GridAfter() { Val = (Int32Value)list[key] });
//             else if (key == "WIDTHBEFORETABLEROW" && list[key] is int)
//                 properties.Append(new WidthBeforeTableRow() { Width = list[key].ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "WIDTHAFTERTABLEROW" && list[key] is int)
//                 properties.Append(new WidthAfterTableRow() { Width = list[key].ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "TABLEROWHEIGHT" && list[key] is int)
//                 properties.Append(new TableRowHeight() { Val = (UInt32Value)list[key] });
//             else if (key == "HIDDEN")
//                 properties.Append(new Hidden() { Val = OnOffValue.FromBoolean(true) });
//             else if (key == "CANTSPLIT")
//                 properties.Append(new CantSplit() { Val = OnOffOnlyValues.On });
//             else if (key == "TABLEHEADER")
//                 properties.Append(new TableHeader() { Val = OnOffOnlyValues.On });
//             else if (key == "TABLECELLSPACING" && list[key] is int)
//                 properties.Append(new TableCellSpacing() { Width = list[key].ToString(), Type = TableWidthUnitValues.Dxa });
//             else if (key == "TABLEJUSTIFICATION")
//                 properties.Append(new TableJustification() { Val = ParseEnum<TableRowAlignmentValues>((string)list[key]) });
//             else if (key == "INSERTED")
//                 throw new NotImplementedException(); // properties.Append(new Inserted() {  });
//             else if (key == "DELETED")
//                 throw new NotImplementedException(); // properties.Append(new Deleted());
//             else if (key == "TABLEROWPROPERTIESCHANGE")
//                 throw new NotImplementedException(); // properties.Append(new TableRowPropertiesChange() {  });
//         }
//
//         if (!properties.HasChildren && DefaultTableCellProperties.HasChildren)
//             properties = (TableRowProperties)DefaultTableRowProperties.Clone();
//
//         return properties;
//     }
//     #endregion
//
//     public Comment CreateComment()
//     {
//         string id = AddCommentPartToPackage();
//
//         Comment _comment =
//             new()
//             {
//                 Initials = _initials,
//                 Author = _author,
//                 Date = XmlConvert.ToDateTime(
//                     DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
//                     XmlDateTimeSerializationMode.RoundtripKind
//                     ),
//                 Id = id
//             };
//
//         return _comment;
//     }
//
//     public int CreateAbstractNum(AbstractNum newAbstractNum)
//     {
//         Guard.Against.Null(WDocument.MainDocumentPart);
//         Guard.Against.Null(WDocument.MainDocumentPart.NumberingDefinitionsPart);
//         if (WDocument.MainDocumentPart.NumberingDefinitionsPart.Numbering.HasChildren)
//         {
//             AbstractNum lastAbstract =
//                 WDocument.MainDocumentPart.NumberingDefinitionsPart.Numbering.Elements<AbstractNum>().Last();
//             Guard.Against.Null(lastAbstract.AbstractNumberId);
//             newAbstractNum.AbstractNumberId =
//                 lastAbstract.AbstractNumberId++;
//
//             WDocument.MainDocumentPart.NumberingDefinitionsPart.Numbering.InsertAfter(newAbstractNum, lastAbstract);
//         }
//         else
//         {
//             newAbstractNum.AbstractNumberId = 0;
//             WDocument.MainDocumentPart.NumberingDefinitionsPart.Numbering.AppendChild(newAbstractNum);
//         }
//         return newAbstractNum.AbstractNumberId;
//     }
//
//     public int CreateNumberingInstance(int absId)
//     {
//         Guard.Against.Null(MainDocumentPart.NumberingDefinitionsPart);
//         Numbering numbering = MainDocumentPart.NumberingDefinitionsPart.Numbering;
//
//         var numberId = numbering.Elements<NumberingInstance>().Count() + 1;
//
//         NumberingInstance newNumberingInstance =
//             new() { NumberID = numberId };
//         AbstractNumId absNumId =
//             new() { Val = absId };
//
//         LevelOverride levelOverride =
//             new()
//             {
//                 LevelIndex = 0,
//                 StartOverrideNumberingValue = new StartOverrideNumberingValue() { Val = 1 }
//             };
//
//         newNumberingInstance.Append(levelOverride);
//         newNumberingInstance.Append(absNumId);
//         numbering.Append(newNumberingInstance);
//
//         return numberId;
//     }
//
//     // Apply a style to a paragraph.
//     protected static void ApplyStyleToParagraph(MainDocumentPart mainDocumentPart, string styleid, string stylename, Paragraph p)
// {
//     Guard.Against.Null(mainDocumentPart.StyleDefinitionsPart);
//
//         // If the paragraph has no ParagraphProperties object, create one.
//         if (!p.Elements<ParagraphProperties>().Any())
//             p.PrependChild(new ParagraphProperties());
//
//         // Get the paragraph properties element of the paragraph.
//         ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();
//
//         // Get the Styles part for this document.
//         StyleDefinitionsPart part = mainDocumentPart.StyleDefinitionsPart;
//
//         // If the Styles part does not exist, add it and then add the style.
//         if (part == null)
//         {
//             part = AddStylesPartToPackage(mainDocumentPart);
//             AddNewStyle(part, styleid, stylename);
//         }
//         else
//         {
//             // If the style is not in the document, add it.
//             if (IsStyleIdInDocument(mainDocumentPart, styleid) != true)
//             {
//                 // No match on styleid, so let's try style name.
//                 string styleidFromName = GetStyleIdFromStyleName(mainDocumentPart, stylename);
//
//                 if (styleidFromName == null)
//                     AddNewStyle(part, styleid, stylename);
//                 else
//                     styleid = styleidFromName;
//             }
//         }
//
//         // Set the style of the paragraph.
//         pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
//     }
//
//     // Return true if the style id is in the document, false otherwise.
//     protected static bool IsStyleIdInDocument(MainDocumentPart mainDocumentPart, string styleid)
//     {
//         Guard.Against.Null(mainDocumentPart.StyleDefinitionsPart);
//         Guard.Against.Null(mainDocumentPart.StyleDefinitionsPart.Styles);
//
//         // Get access to the Styles element for this document.
//         Styles s = mainDocumentPart.StyleDefinitionsPart.Styles;
//
//         // Check that there are styles and how many.
//         int n = s.Elements<Style>().Count();
//         if (n == 0)
//             return false;
//
//         Style? style = s.Elements<Style>()
//             .Where(st => (st.StyleId! == styleid!) && (st.Type! == StyleValues.Paragraph!))
//             .FirstOrDefault();
//
//         if (style == null)
//             return false;
//
//         return true;
//     }
//
//     // Return styleid that matches the styleName, or null when there's no match.
//     protected static string GetStyleIdFromStyleName(MainDocumentPart mainDocumentPart, string styleName)
//     {
//         Guard.Against.Null(mainDocumentPart.StyleDefinitionsPart);
//
//         StyleDefinitionsPart stylePart = mainDocumentPart.StyleDefinitionsPart;
//         Guard.Against.Null(stylePart.Styles);
//
//         var descendants = stylePart.Styles.Descendants<StyleName>();
//         Guard.Against.Null(descendants);
//
//         var styles = descendants?
//             .Where(s => s.Val!.Value!.Equals(styleName) &&
//                 (((Style)s.Parent!).Type! == StyleValues.Paragraph));
//         Guard.Against.Null(styles);
//
//         string? styleId = styles
//             .Select(n => ((Style)n.Parent!).StyleId).FirstOrDefault();
//         Guard.Against.Null(styleId);
//
//         return styleId;
//     }
//
//     // Create a new style with the specified styleid and stylename and add it to the specified
//     // style definitions part.
//     protected static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename)
//     {
//         Guard.Against.Null(styleDefinitionsPart.Styles);
//
//         // Get access to the root element of the styles part.
//         Styles styles = styleDefinitionsPart.Styles;
//
//         // Create a new paragraph style and specify some of the properties.
//         var style = new Style()
//         {
//             Type = StyleValues.Paragraph,
//             StyleId = styleid,
//             CustomStyle = true
//         };
//         var styleName1 = new StyleName() { Val = stylename };
//         var basedOn1 = new BasedOn() { Val = "Normal" };
//         var nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
//         style.Append(styleName1);
//         style.Append(basedOn1);
//         style.Append(nextParagraphStyle1);
//
//         // Create the StyleRunProperties object and specify some of the run properties.
//         var styleRunProperties1 = new StyleRunProperties();
//         var bold1 = new Bold();
//         var color1 = new Color() { ThemeColor = ThemeColorValues.Accent2 };
//         var font1 = new RunFonts() { Ascii = "Lucida Console" };
//         var italic1 = new Italic();
//         // Specify a 12 point size.
//         var fontSize1 = new FontSize() { Val = "24" };
//         styleRunProperties1.Append(bold1);
//         styleRunProperties1.Append(color1);
//         styleRunProperties1.Append(font1);
//         styleRunProperties1.Append(fontSize1);
//         styleRunProperties1.Append(italic1);
//
//         // Add the run properties to the style.
//         style.Append(styleRunProperties1);
//
//         // Add the style to the styles part.
//         styles.Append(style);
//     }
//
//     // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
//     protected static StyleDefinitionsPart AddStylesPartToPackage(MainDocumentPart mainDocumentPart)
//     {
//         StyleDefinitionsPart part;
//         part = mainDocumentPart.AddNewPart<StyleDefinitionsPart>();
//         Styles root = new();
//         root.Save(part);
//         return part;
//     }
//
//     // public int CreateNumberingInstance(int abstractNumId)
//     // {
//     //     if (TheMainDocumentPart().NumberingDefinitionsPart is null)
//     //     {
//     //         AddNumberingPartToPackage(WDocument);
//     //     }
//     //     Numbering numbering = TheMainDocumentPart().NumberingDefinitionsPart.Numbering;
//     //
//     //     var numberId = numbering.Elements<NumberingInstance>().Count() + 1;
//     //
//     //     NumberingInstance numberingInstance =
//     //         new() { NumberID = numberId };
//     //     AbstractNumId absNumId =
//     //         new() { Val = abstractNumId };
//     //
//     //     LevelOverride levelOverride =
//     //         new() { LevelIndex = 0 };
//     //     levelOverride.StartOverrideNumberingValue =
//     //         new StartOverrideNumberingValue() { Val = 1 };
//     //
//     //     numberingInstance.Append(levelOverride);
//     //
//     //     numberingInstance.Append(absNumId);
//     //     numbering.Append(numberingInstance);
//     //
//     //     return numberId;
//     // }
// }