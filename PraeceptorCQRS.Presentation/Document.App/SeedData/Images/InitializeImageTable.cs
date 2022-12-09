using Ardalis.GuardClauses;

using Document.App.Interfaces;

using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;

using Serilog;

namespace Document.App.SeedData.Images
{
    public static class InitializeImageTable
    {
        private static readonly string[] imageFiles = {
            "Acesso1.png",              "Acesso2.png",              "Acesso3.png",              "Biblioteca.png",
            "Campanha.jpg",             "Clima.png",                "Concluintes.png",          "Desenvolvimento.png",
            "Eixos.jpg",                "Evolucao_Concluintes.png", "Evolucao_Matriculas.png",  "Evolucao_Superior.png",
            "Logo.jpg",                 "Matriculas.png",           "MiniLogo.png",             "Organograma.png",
            "PIB.png",                  "PIB1.png",                 "PIB2.png",                 "PIB_Crescimento.png",
            "PontosExtremos.png",       "Riquesas.png",             "saude1.png",               "saude2.png",
            "saude3.png",               "saude4.png",               "saude5.png",               "superior.png",
            "Unit.jpg",                 "Unit1.jpg",
            "NotFound.png"
        };

        private static byte[] LoadImage(string strFn)
        {
            try
            {
                FileInfo arqImagem = new(strFn);
                long tamanhoArquivoImagem = arqImagem.Length;

                var fileStream = new FileStream(strFn, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] vetorImagens = new byte[Convert.ToInt32(tamanhoArquivoImagem)];
                fileStream.Read(vetorImagens, 0, Convert.ToInt32(tamanhoArquivoImagem));
                fileStream.Close();
                return vetorImagens;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Array.Empty<byte>();
        }

        public static async Task Initialize(Guid instituteId, IFileStreamService service)
        {
            foreach (var fname in imageFiles)
            {
                string[] vs = fname.Split('.');
                int len = vs.Length;
                Guard.Against.AgainstExpression(x => x == 2, len, "Nome da imagem não está no formato correto: 'nome.ext'");

                string fullpath = $"D:\\users\\clalu\\Source\\repos\\testeB\\PraeceptorCQRS\\PraeceptorCQRS.Presentation\\Document.App\\SeedData\\Images\\Files\\{fname}";
                var data = LoadImage(fullpath);

                if (data is null || data.Length == 0)
                    continue;

                bool exist = await service.ExistFileStream(instituteId, fname);

                if (!exist)
                {
                    CreateFileRequest request = new(
                        $"{vs[0]}.{vs[1]}",
                        fname,
                        fullpath,
                        null,
                        data,
                        $"image/{vs[1]}",
                        instituteId
                        );

                    var response = await service.CreateFileStream(request);

                    if (!response.IsSuccessStatusCode)
                        Log.Error($"A imagem {fname} não foi criada! (Erro: {response.StatusCode})");
                }
                else
                    Log.Information($"A imagem {fname} já existe!");
            }
        }
    }
}