using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;
using Document.App.Requests;

using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Pea;

using System.Text;

namespace Document.App.SeedData.Planners
{
    public static class InitializePeaTable
    {
        private static readonly string[] files = new string[] {
            "F104108", "F102695", "F106038", "F106160", "F107549", "F107816",
            "F108472", "F109541", "F109592", "F109614", "F109622", "F111228",
            "F111252", "F112356", "F112828", "F112909", "F112917", "F113603",
            "F113611", "F113620", "F113638", "F113646", "F113654", "F113662",
            "F113670", "F113689", "F113697", "F113700", "F113719", "F113727",
            "F113735", "F113751", "F113760", "F113778", "F113794", "F113808",
            "F113824", "F113832", "F113840", "F113859", "F113875", "F113883",
            "F113891", "F113905", "F113913", "F113921", "F113930", "F113980",
            "F114022", "F114030", "F114049", "H106442", "H113341", "H113457",
            "H113465", "H114127", "H118815", "H118840", "H119242", "H119315",
            "H119714", "H120003", "H121956", "H122820", "F107271", "F114013",
            "F114502", "F114499", "F114510", "F106780", "F114529", "F112216",
            "F114081", "F114375", "F114421", "F114537", "F114545", "F104779",
            "F111996", "F112372", "F114456", "F114553", "F114570", "F109460",
            "F114472", "F114588", "F112062", "F112267", "F114596",
            };

        public static async Task Initialize(Guid instituteId, IClassService classService, IPlannerService plannerService)
        {
            Dictionary<string, string> dict = new();

            foreach (string fname in files)
            {
                string fullpath = $"D:\\users\\clalu\\Source\\repos\\testeB\\PraeceptorCQRS\\PraeceptorCQRS.Presentation\\Document.App\\SeedData\\Planners\\Files\\{fname}.pea";

                if (File.Exists(fullpath))
                {
                    string text = File.ReadAllText(fullpath, Encoding.UTF8);

                    var cls = await classService.GetClassByCode(fname);

                    if (cls is not null)
                    {
                        var response = await plannerService.CreatePlanner(new CreatePeaRequest(
                            cls.Id,
                            text,
                            "Testador"
                            ));

                        if (response.IsSuccessStatusCode)
                            dict.Add(fname, text);
                    }
                }
                else
                    Console.WriteLine($"O arquivo {fname}.PEA não foi encontrado!");
            }
        }
    }
}