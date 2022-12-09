using Document.App.Interfaces;

using PraeceptorCQRS.Contracts.Entities.SimpleTable;

namespace Document.App.SeedData.SimpleTables
{
    public class InitializeSecurityTable
    {
        public static async Task Initialize(Guid instituteId, ISimpleTableService service)
        {
            var request = new CreateSimpleTableRequest(
                "SEGURANCA",
                "Segurança",
                @"
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{ATIVIDADE}}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{DESENVOLVIMENTO}}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{\\run[BOLD; FONTSIZE=8]{SETORES ENVOLVIDOS}}
",
                @"
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					EPI – Equipamento de Proteção Individual
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					O empregado que irá executar atividades em áreas de risco, quando contratado, passa por um treinamento em que ele será informado quanto aos riscos que estará exposto e dos equipamentos de proteção a serem usados.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Será fornecido ao empregado recém-admitido todos os EPI’s para realização de suas atividades, onde ele deverá assinar uma ficha de recebimento e responsabilidade. Deverá o empregado deslocar-se ao Setor de Segurança do Trabalho para troca dos EPI’s ou dúvidas referentes aos mesmos. “No ato da entrega dos EPI’s os empregados recebem orientações específicas para cada equipamento quanto ao uso e manutenção”.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Quanto à solicitação de EPI’s deverá ser feita por escrito (e-mail) pelo Coordenador, Gerente ou responsável do setor, ao Setor de Segurança do Trabalho, para ser avaliado e em seguida encaminhado ao setor de compras com suas respectivas referências.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Estão autorizados a solicitar Equipamento de Proteção Individual – EPI ao setor de compras, os Técnicos de Segurança do Trabalho, devido ao conhecimento e especificações técnicas.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT – Serviço Especializa em Segurança e Medicina do Trabalho
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM – Departamento de Infraestrutura de Manutenção
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH – Diretoria de Recursos Humanos
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Equipamento de Combate a Incêndio
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os extintores e hidrantes em toda a Instituição foram dimensionados para as diversas áreas e setores, sendo feita um redimensionamento quando a mudança de layout ou construção de novas instalações.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os extintores obedecem a um cronograma de recarga dentro das datas de vencimentos e testes hidrostáticos.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					São realizados treinamentos específicos (teoria e prática) de princípio e combate a incêndio, utilizando os extintores vencidos que estão indo para recarga.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os extintores são identificados por número de ordem e posto. Os hidrantes são testados semestralmente quanto ao estado de conservação das mangueiras, bicos, bomba de incêndio e a vazão da água se atende à necessidade.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT – Serviço Especializa em Segurança e Medicina do Trabalho
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM – Departamento de Infraestrutura de Manutenção
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Empresa responsável pela manutenção
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Equipamento de Medição Ambiental
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					O setor de Segurança do Trabalho dispõe de equipamentos de medição, facilitando os trabalhos de avaliação de ruído, temperatura e luminosidade para adicionais de insalubridade e aposentadoria especial.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Dos equipamentos temos 01 Decibelímetro, Luxímetro e um Termômetro de Globo (IBUTG).
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os equipamentos são usados também na confecção do PPRA – Programa de Prevenção de Riscos Ambientais, no PPA – Programa de Proteção Auditiva.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Treinamento
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os treinamentos seguem um cronograma, em que são divididos por área, dando prioridade às atividades de maior risco de acidente.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Os treinamentos são ministrados no setor de trabalho, na sala de treinamento do DRH, nos auditórios etc.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					São utilizados nos treinamentos efeitos visuais como retroprojetor, data show, slides etc.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					O SESMT, convidado pelos coordenadores da área da saúde, realiza treinamento sobre Biossegurança em laboratórios para os alunos dos cursos de: Fisioterapia, Farmácia, Biomedicina e enfermagem, orientando sobre como se proteger dos riscos biológicos e acerca da necessidade de adotar uma conduta profissional segura nos diversos laboratórios, evitando acidentes e doenças do trabalho.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Nos treinamentos de combate a princípio de incêndio a parte prática está sendo realizada em uma área aberta, onde são realizadas as simulações com os tambores cheios de combustível em chamas.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Sinalização
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					As sinalizações da Instituição dividem-se em:
					}
				}
			\\begin{itemize}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						Horizontais – São sinalizados pisos com diferença de níveis, pisos escorregadios (fitas antiderrapante), sinalização das áreas de limitação de hidrantes e extintores, demarcações em volta das máquinas que oferecem risco de acidente etc.
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						Verticais – São vistas em toda área externa do Campus como placas de indicação de estacionamento, quebra mola, faixa de pedestre, placas de velocidade etc.
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						Placas e Cartazes Indicativos e Educativos – São placas que indicam condição de risco, de perigo, de higiene, de material contaminante etc.
						}
					}
			\\end{itemize}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Gráfica
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					PROAD
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Serviços Terceirizados
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Toda contratação de prestadores de serviços (empreiteiros) que envolvam em construção, manutenção, reparos e mudanças no ambiente físico e equipamentos da Instituição, deverá ser comunicado ao SESMT antes que estas iniciem suas atividades.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					O SESMT solicitara a empresa contratada, documentações necessárias, equipamento de proteção individual e outros dispositivos que as tornem aptas para realização de suas atividades dentro dos padrões de Segurança normatizados pelo SESMT e preceitos exigidos pelo Ministério do Trabalho.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Dos Programas de Segurança do Trabalho
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					A Instituição dispõe de programas de segurança que possibilitam a realização de suas atividades, evitando riscos de acidentes. Onde temos:
					}
				}
			\\begin{itemize}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						PPRA – Programa de Prevenção a Riscos Ambientais;
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						PCMSO – Programa de Controle Médico e Saúde Ocupacional;
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						PGRSS – Programa de Gerenciamento de Resíduos de Serviço e Saúde;
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						Programa Qualidade de vida no Trabalho – Programa de reeducação postural e ginástica laboral;
						}
					}
				\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
					\\run[FONTSIZE=8]{
						SIPAT – Semana Interna de Prevenção de Acidentes com o objetivo de conscientizar os colaboradores sobre a necessidade de se proteger, abordando temas de interesses gerais com a participação dos colaboradores.
						}
					}
			\\end{itemize}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					CIPA
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Colaboradores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Acidente do Trabalho
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Todos os acidentes de trabalho ocorridos, seja ele típico ou de trajeto, devem comparecer ao setor Médico para atendimento dos primeiros socorros e em seguida ao setor de Segurança do trabalho para prestar informações necessárias para investigação do acidente.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					A emissão da CAT – Comunicação de Acidente do Trabalho, será preenchida a parte médica no ato do atendimento e em seguida complementará a outra parte, onde pode ser preenchida no próprio setor médico ou encaminhada ao setor de Segurança do Trabalho.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Colaboradores
					}
				}
&&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Inspeções
					}
				}
&
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Regularmente e obedecendo a cronograma de visitas, serão realizadas inspeções de Segurança nos diversos setores da Instituição a fim de anteciparem-se aos acontecimentos inesperados pela consequência da exposição aos agentes/riscos contidos nos setores.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					As inspeções periódicas de Segurança serão realizadas nos horários relativos à execução das atividades desenvolvidas pelos setores para avaliar a eficiência das ações aplicadas pelo SESMT.
					}
				}
			\\paragraph[AFTER=200;JUSTIFICATION=left;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Poderão ser solicitadas inspeções ou visitas em caráter de urgência pelos coordenadores por escrito (e-mail) informando a necessidade da visita. Esta será avaliada e priorizada.
					}
				}
&
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					SESMT
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DRH
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					Coordenadores
					}
				}
			\\paragraph[JUSTIFICATION=center;FIRSTLINE=0;SPACINGBETWEENLINES=240]{
				\\run[FONTSIZE=8]{
					DIM
					}
				}
",
                null!,
                instituteId
                );

            var response = await service.CreateTable(request);
        }
    }
}