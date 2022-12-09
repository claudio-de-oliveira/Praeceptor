using AbstractLL;

namespace PraeceptorCQRS.Infrastructure.Parsers.Pea;

public class Semantic : AbstractSemantic<PeaModel>
{
    private PeaModel pea = default!;

    public override void Inicializa()
    {
        pea = new PeaModel();
    }

    public override void Execute(AbstractTAG action, Stack<AbstractTAG> stk, Stack<AbstractToken> tokens, AbstractEnvironment<PeaModel> environment)
    {
        if (action == Tag._Echo)
        {
            stk.Peek().SetAttribute(0, action.GetAttribute(0));
        }
        else if (action == Tag._String)
        {
            stk.ToArray()[1].SetAttribute(0, ((StringToken)tokens.Pop()).Value);
        }
        else if (action == Tag._EmptyString)
        {
            stk.ToArray()[1].SetAttribute(0, "");
        }
        else if (action == Tag._PeaId)
        {
            pea.DisciplinaId = (string)action.GetAttribute(0);
        }
        else if (action == Tag._CreateList)
        {
            List<object> list = new() { action.GetAttribute(0) };
            stk.Peek().SetAttribute(0, list);
        }
        else if (action == Tag._InsertList)
        {
            List<object> list = (List<object>)action.GetAttribute(1);
            object obj = action.GetAttribute(0);
            list.Add(obj);
            stk.Peek().SetAttribute(0, list);
        }
        else if (action == Tag._KeyChave)
        {
            string key = (string)action.GetAttribute(0);
            stk.ToArray()[1].SetAttribute(1, key);
        }
        else if (action == Tag._Chave)
        {
            string key = (string)action.GetAttribute(1);
            List<object> list = (List<object>)action.GetAttribute(0);
            ConceitoChave model = new()
            {
                Description = key,
                Key = Guid.NewGuid(),
                Conteudos = new List<string>()
            };
            foreach (object obj in list)
                model.Conteudos.Add(
                    ((string)obj).ToString()
                    );
            stk.Peek().SetAttribute(0, model);
        }
        else if (action == Tag._Outro)
        {
            string key = ((OutroToken)tokens.Peek()).Value;
            List<object> list = (List<object>)action.GetAttribute(0);

            if (key.ToUpper() == "@EMENTA")
            {
                pea.Ementa = string.Empty;
                foreach (object obj in list)
                    pea.Ementa += $"{obj} ";
            }
            else if (key.ToUpper() == "@OBJETIVO" || key.ToUpper() == "@OBJETIVOS" || key.ToUpper() == "@OBJETIVOGERAL")
            {
                pea.Objetivos = new List<string>();
                foreach (object obj in list)
                    pea.Objetivos.Add(((string)obj).ToString());
            }
            else if (key.ToUpper() == "@COMPETENCIAS" || key.ToUpper() == "@COMPETENCIA")
            {
                // planner.Competencias = new HashSet<string>();
                // foreach (object obj in list)
                //     planner.Competencias.Add(obj.ToString());
            }
            else if (key.ToUpper() == "@PROCEDIMENTOS" || key.ToUpper() == "@PROCEDIMENTO")
            {
                pea.Procedimentos = string.Empty;
                foreach (object obj in list)
                    pea.Procedimentos += $"{obj} ";
            }
            else if (key.ToUpper() == "@AVALIACAO")
            {
                pea.Avaliacao = string.Empty;
                foreach (object obj in list)
                    pea.Avaliacao += $"{obj} ";
            }
            else
            {
            }
        }
        else if (action == Tag._Unidade1)
        {
            List<object> v = (List<object>)action.GetAttribute(0);
            pea.Unidade1 = new List<ConceitoChave>();
            foreach (Object obj in v)
                pea.Unidade1.Add((ConceitoChave)obj);
        }
        else if (action == Tag._Unidade2)
        {
            List<object> v = (List<object>)action.GetAttribute(0);
            pea.Unidade2 = new List<ConceitoChave>();
            foreach (object obj in v)
                pea.Unidade2.Add((ConceitoChave)obj);
        }
        else if (action == Tag._Online)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(0);
            bitItem.Online = true;
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._NotOnline)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(0);
            bitItem.Online = false;
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Basica)
        {
            BibItem bitItem = new();
            pea.BibliografiaBasica ??= new List<BibItem>();
            pea.BibliografiaBasica.Add(bitItem);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Complementar)
        {
            BibItem bitItem = new();
            pea.BibliografiaComplementar ??= new List<BibItem>();
            pea.BibliografiaComplementar.Add(bitItem);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Bibliografia)
        {
        }
        else if (action == Tag._AutorLastName)
        {
            NomeItem autor = new() { Sobrenome = (string)action.GetAttribute(0) };
            stk.ToArray()[1].SetAttribute(1, autor);
        }
        else if (action == Tag._AutorFirstName)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            NomeItem autor = (NomeItem)action.GetAttribute(1);
            autor.Nome = (string)action.GetAttribute(0);
            bitItem.Autores.Add(autor);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._TradutorLastName)
        {
            NomeItem tradutor = new() { Sobrenome = (string)action.GetAttribute(0) };
            stk.ToArray()[1].SetAttribute(1, tradutor);
        }
        else if (action == Tag._TradutorFirstName)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            NomeItem tradutor = (NomeItem)action.GetAttribute(1);
            tradutor.Nome = (string)action.GetAttribute(0);
            bitItem.Tradutores.Add(tradutor);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._OrganizadorLastName)
        {
            NomeItem organizador = new() { Sobrenome = (string)action.GetAttribute(0) };
            stk.ToArray()[1].SetAttribute(1, organizador);
        }
        else if (action == Tag._OrganizadorFirstName)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            NomeItem organizador = (NomeItem)action.GetAttribute(1);
            organizador.Nome = (string)action.GetAttribute(0);
            bitItem.Organizadores.Add(organizador);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Titulo)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Title = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Volume)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Volume = new VolumeItem { Text1 = (string)action.GetAttribute(0) };
            stk.Peek().SetAttribute(0, bitItem);
            stk.ToArray()[1].SetAttribute(2, bitItem);
        }
        else if (action == Tag._VolumeText)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Volume.Text2 = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Edicao)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Edition = int.Parse((string)action.GetAttribute(0));
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Editora)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Publisher = new EditoraItem { Nome = (string)action.GetAttribute(0) };
            stk.ToArray()[1].SetAttribute(2, bitItem);
        }
        else if (action == Tag._EditoraAddress)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Publisher.Endereco = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Detalhe)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Details = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Serie)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Series = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Ano)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Year = int.Parse((string)action.GetAttribute(0));
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Editor)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Editor = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Exemplares)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Exemplares = int.Parse((string)action.GetAttribute(0));
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._Nota)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.Note = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._ISBN)
        {
            BibItem bitItem = (BibItem)action.GetAttribute(2);
            bitItem.ISBN = (string)action.GetAttribute(0);
            stk.Peek().SetAttribute(0, bitItem);
        }
        else if (action == Tag._None)
        {
            stk.Peek().SetAttribute(0, "");
        }
        else if (action == Tag._Done)
        {
            ((PeaEnvironment)environment).Pea = pea;
        }
        else if (action == Tag._EmptyList)
        {
            List<object> list = new();
            stk.Peek().SetAttribute(0, list);
        }
    }
}
