using System;

namespace Personas.Entidade
{
    public class PessoaFisica
    {

        public string Nome { get; set; }

        public Sexo Sexo { get; set; }
        
        public DateTime DataNascimento { get; set; }

        public TipoNacionalidade TipoNacionalidade { get; set; }

        public string Naturalidade { get; set; }

        public string Nacionalidade { get; set; }

        public EstadoCivil EstadoCivil { get; set; }

        public string NomePai { get; set; }

        public string NomeMae { get; set; }

        public string NomeConjuge { get; set; }

        public Identidade Identidade { get; set; } = new Identidade();

        public string Cpf { get; set; }

        public Logradouro Logradouro { get; set; } = new Logradouro();

        public string Email { get; set; }

        public string Ocupacao { get; set; }

        public string Empregador { get; set; }

        public decimal Rendimento { get; set; }

        public SituacaoFinanceiraPatrimonial SituacaoFinanceiraPatrimonial { get; set; } = new SituacaoFinanceiraPatrimonial();

        public PerfilDeRisco PerfilDeRisco { get; set; }

        public bool PossuiPerfilDoInvestidor { get; set; }

        public bool OperaPorTerceiros { get; set; }

        public bool PossuiProcurador { get; set; }

        public Procurador Procurador { get; set; } = new Procurador();
        
        public DateTime DataAtualizacaoCadastral { get; set; }

        public bool PossuiAssinaturaDoCliente { get; set; }

        public bool PossuiCopiaDocumentoIdentidade { get; set; }

        public bool PossuiCopiaComprovanteResidencia { get; set; }

    }
}
