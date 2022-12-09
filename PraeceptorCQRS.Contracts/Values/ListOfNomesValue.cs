namespace PraeceptorCQRS.Contracts.Values
{
    public class ListOfNomesValue
    {
        private readonly List<NomeItem> _list = new();

        public List<NomeItem> Nomes => _list;

        // public ListOfNomes()
        // { /* Nothing more todo */ }
        //
        // public ListOfNomes(string text)
        // {
        //     string txt = text + "#";
        //     string nome = string.Empty, sobrenome = string.Empty;
        //     int pos = 0;
        //     int state = 0;
        //
        //     while (true)
        //     {
        //         if (txt[pos] == '#')
        //         {
        //             if (state == 10)
        //                 _list.Add(new NomeItem { Nome = nome.Trim(), Sobrenome = sobrenome.Trim() });
        //             return;
        //         }
        //
        //         switch (state)
        //         {
        //             case 0:
        //                 nome = string.Empty;
        //                 sobrenome = string.Empty;
        //
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     pos++;
        //                     state = 0;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 1;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 1: // sobrenome
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 1;
        //                     break;
        //                 }
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 2;
        //                     break;
        //                 }
        //                 if (txt[pos] == '.')
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 3;
        //                     break;
        //                 }
        //                 if (txt[pos] == ',')
        //                 {
        //                     pos++;
        //                     state = 5;
        //                     break;
        //                 }
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 2:
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     pos++;
        //                     state = 2;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 1;
        //                     break;
        //                 }
        //                 if (txt[pos] == ',')
        //                 {
        //                     pos++;
        //                     state = 5;
        //                     break;
        //                 }
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 3:
        //                 if (txt[pos] == ',')
        //                 {
        //                     pos++;
        //                     state = 5;
        //                     break;
        //                 }
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 4;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     // insere um espaço entre o ponto e a próxima letra
        //                     sobrenome += ' ';
        //                     //
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 1;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 4:
        //                 if (txt[pos] == ',')
        //                 {
        //                     pos++;
        //                     state = 5;
        //                     break;
        //                 }
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     sobrenome += txt[pos];
        //                     pos++;
        //                     state = 1;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 5:
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     pos++;
        //                     state = 5;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 6;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 6: // nome
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 6;
        //                     break;
        //                 }
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 7;
        //                     break;
        //                 }
        //                 if (txt[pos] == '.')
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 8;
        //                     break;
        //                 }
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 7:
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     pos++;
        //                     state = 7;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 6;
        //                     break;
        //                 }
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 8:
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 if (char.IsWhiteSpace(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 9;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     // insere um espaço entre o ponto e a próxima letra
        //                     nome += ' ';
        //                     //
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 6;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 9:
        //                 if (txt[pos] == ';')
        //                 {
        //                     pos++;
        //                     state = 10;
        //                     break;
        //                 }
        //                 if (char.IsLetter(txt[pos]))
        //                 {
        //                     nome += txt[pos];
        //                     pos++;
        //                     state = 6;
        //                     break;
        //                 }
        //                 // OUTRO
        //                 pos++;
        //                 state = 0;
        //                 break;
        //
        //             case 10:
        //                 _list.Add(new NomeItem { Nome = nome.Trim(), Sobrenome = sobrenome.Trim() });
        //                 pos++;
        //                 state = 0;
        //                 break;
        //         }
        //     }
        // }
        //
        // public override string ToString()
        // {
        //     string str = string.Empty;
        //     foreach (NomeItem nome in _list)
        //         str += $"{nome}; ";
        //     return str;
        // }
        //
        // protected override IEnumerable<object> GetEqualityComponents()
        // {
        //     foreach (var nome in _list)
        //         yield return nome;
        // }
    }
}