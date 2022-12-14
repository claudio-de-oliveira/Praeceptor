using AbstractLL;

namespace PraeceptorCQRS.Infrastructure.Parsers.Pea;

public class PeaEnvironment : AbstractEnvironment<PeaModel>
{
    public PeaModel? Pea { get; set; }

    public override PeaModel? Result => Pea;

    public override void Inicializa()
    {
        // Nothing todo
    }
}
