
namespace Personas.Entidade
{
    public class Procurador
    {

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public Identidade Identidade { get; set; } = new Identidade();

        public PoderesDoProcurador Poderes { get; set; }

        public bool AutorizaTransmissaoDeOrdemPorProcurador { get; set; }

        public bool PossuiCopiaProcuracao { get; set; }

        public bool PossuiCopiaDocumentoIdentidadeProcurador { get; set; }

    }
}
