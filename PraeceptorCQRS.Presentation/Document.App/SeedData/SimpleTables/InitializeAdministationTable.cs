using Document.App.Interfaces;

using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.App.SeedData.SimpleTables
{
    public class InitializeAdministationTable
    {
        public static async Task Initialize(Guid instituteId, ISimpleTableService service)
        {
            var request = new CreateSimpleTableRequest(
                "ADMINISTRACAO",
                "Estrutura Acadêmica e Administrativa da \\variable{@IES.@ACRONIMO}",
                "&",
                @"
\\paragraph{\\run[FONTSIZE = 8]{Reitor:}}
\\paragraph{\\run[Bold]{JOUBERTO UCHÔA DE MENDONÇA}}
&
\\paragraph{Especialista em Administração e Gerência de Unidade de Ensino – FIT’s / SE / 1992}
&&
\\paragraph{\\run[FONTSIZE = 8]{Vice - Reitora:}}
\\paragraph{\\run[Bold]{AMÉLIA MARIA CERQUEIRA UCHÔA}}
&
\\paragraph{Especialista em Administração e Gerência de Unidade de Ensino – FIT’s / SE / 1992}
&&
\\paragraph{\\run[FONTSIZE = 8]{Vice - Reitora Adjunta:}}
\\paragraph{\\run[Bold]{MARÍLIA CERQUEIRA UCHÔA SANTA ROSA}}
&
\\paragraph{Especialista em Medicina Preventiva e Social – HCFMRP / USP / 1995}
&&
\\paragraph{\\run[FONTSIZE = 8]{Superintendente Acadêmico:}}
\\paragraph{\\run[Bold]{TEMISSON JOSÉ DOS SANTOS}}
&
\\paragraph{Doutor em Engenharia Química pela Universidade Federal do Rio de Janeiro (2000)}
&&
\\paragraph{\\run[FONTSIZE = 8]{Pró - Reitora de Graduação:}}
\\paragraph{\\run[Bold]{ARLEIDE BARRETO SILVA}}
&
\\paragraph{Mestre em Administração pela Universidade Federal da Paraíba (2003)}
&&
\\paragraph{\\run[FONTSIZE = 8]{Pró - Reitora de Pós-Graduação, Pesquisa e Extensão:}}
\\paragraph{\\run[Bold]{JULIANA CORDEIRO CARDOSO}}
&
\\paragraph{Doutora em Ciências Farmacêuticas – Universidade de São Paulo (2005)}
&&
\\paragraph{\\run[FONTSIZE = 8]{Coordenação de Extensão:}}
\\paragraph{\\run[Bold]{GERALDO CALASANS BARRETO JUNIOR}}
&
\\paragraph{Especialização para Gestores de Instituições de Ensino Técnico – UFSC, 2000}
&&
\\paragraph{\\run[FONTSIZE = 8]{Diretora do Sistema de Bibliotecas:}}
\\paragraph{\\run[Bold; COLOR = red]{MARIA EVELI PIERUZI DE BARROS FREIRE}}
&
\\paragraph{Especialista em Administração / Universidade São Judas Tadeu – SP / 1988}
&&
\\paragraph{\\run[FONTSIZE = 8]{Diretor de Saúde:}}
\\paragraph{\\run[Bold]{HESMONEY RAMOS DE SANTA ROSA}}
&
\\paragraph{Mestre em Saúde e Ambiente – UNIT, 2009}
&&
\\paragraph{\\run[FONTSIZE = 8]{Coordenação da Clínica Odontológica:}}
\\paragraph{\\run[Bold]{ISABELA DE AVELAR BRANDÃO MACEDO}}
&
\\paragraph{Mestrado em Saúde Ambiente – Unit, 2011}
&&
\\paragraph{\\run[FONTSIZE = 8]{Diretora da Clínica de Psicologia:}}
\\paragraph{\\run[Bold]{JACQUELINE MARIA DE SANTANA CALDEIRA}}
&
\\paragraph{Especialização em Didática do Ensino Superior – Faculdade Pio Décimo, 2010}
&&
\\paragraph{\\run[FONTSIZE = 8]{Coordenadora Administrativa do Laboratório Central de Biomedicina:}}
\\paragraph{\\run[Bold]{SIMONE ALMEIDA SANTOS RODRIGUES}}
&
\\paragraph{Graduada em Administração – Faculdade São Judas Tadeu}
&&
\\paragraph{\\run[FONTSIZE = 8]{Responsável Técnico do Laboratório Central de Biomedicina:}}
\\paragraph{\\run[Bold]{ADRIANA DE OLIVEIRA GUIMARÃES}}
&
\\paragraph{Especialização em Gestão Pública e da Família.}
&&
\\paragraph{\\run[FONTSIZE = 8]{Coordenador do Curso de Engenharia Mecatrônica:}}
\\paragraph{\\run[Bold]{@CURSO.@COORDENADOR}}
&
\\paragraph{Doutor em Engenharia Elétrica – UFSC, 2005}
",
                null!,
                instituteId
                );

            var response = await service.CreateTable(request);
        }
    }
}