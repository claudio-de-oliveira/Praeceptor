using AbstractLL;

using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.ToWord.Parser.Planner;

public class PeaEnvironment : AbstractEnvironment<PeaModel>
{
    public PeaModel? Pea { get; set; }

    public override PeaModel? Result => Pea;

    public override void Inicializa()
    {
        // Nothing todo
    }
}