using System;

namespace Personas.Entidade
{
    public class Identidade
    {
        public TipoIdentidade TipoIdentidade { get; set; }

        public string NumeroIdentidade { get; set; }

        public string OrgaoEmissorIdentidade { get; set; }

        public DateTime DataEmissaoDocumento { get; set; }

        public string CodigoVerificacao { get; set; }
    }
}