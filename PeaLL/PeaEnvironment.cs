using AbstractLL;

namespace PeaLL;

public class PeaEnvironment : AbstractEnvironment<PeaModel>
{
    public PeaModel? Pea { get; set; }

    public override PeaModel? Result => Pea;

    public override void Inicializa()
    {
        // Nothing todo
    }
}
