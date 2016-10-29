using FluentValidation;
using FluentValidation.Results;
using Personas.Entidade;

namespace Personas.Validator
{

    public class PessoaFisicaValidadorIcvm506 : AbstractValidator<PessoaFisica>
    {

        public PessoaFisicaValidadorIcvm506()
        {
            //Dados Básicos
            RuleFor(pessoa => pessoa.Nome).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Sexo).IsInEnum();
            RuleFor(pessoa => pessoa.Cpf).Must(ValidadorCpf.Validar).WithMessage("CPF inválido.");

            //Nacionalidade e Naturalidade
            RuleFor(pessoa => pessoa.TipoNacionalidade).IsInEnum();
            RuleFor(pessoa => pessoa.Nacionalidade).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Naturalidade).NotNull().NotEmpty();

            //Filiação
            RuleFor(pessoa => pessoa.NomePai).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.NomeMae).NotNull().NotEmpty();

            //Estado Civil
            RuleFor(pessoa => pessoa.EstadoCivil).IsInEnum();
            RuleFor(pessoa => pessoa.NomeConjuge).NotNull().NotEmpty()
                .When(pessoa => pessoa.EstadoCivil == EstadoCivil.Casado || pessoa.EstadoCivil == EstadoCivil.UniaoEstavel);
            RuleFor(pessoa => pessoa.NomeConjuge).Null().Empty()
                .When(pessoa => pessoa.EstadoCivil != EstadoCivil.Casado && pessoa.EstadoCivil != EstadoCivil.UniaoEstavel);

            //Identidade
            RuleFor(pessoa => pessoa.Identidade.OrgaoEmissorIdentidade).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Identidade.TipoIdentidade).IsInEnum();
            RuleFor(pessoa => pessoa.Identidade.NumeroIdentidade).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Identidade.DataEmissaoDocumento).GreaterThan(pessoa => pessoa.DataNascimento);
            RuleFor(pessoa => pessoa.Identidade.CodigoVerificacao).NotNull().NotEmpty()
                .When(pessoa => pessoa.Identidade.TipoIdentidade == TipoIdentidade.CNH);
            RuleFor(pessoa => pessoa.Identidade.CodigoVerificacao).Null().Empty()
                .When(pessoa => pessoa.Identidade.TipoIdentidade != TipoIdentidade.CNH);

            //Logradouro
            RuleFor(pessoa => pessoa.Logradouro.Endereco).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Logradouro.Estado).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Logradouro.Cidade).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Logradouro.Bairro).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Logradouro.Numero).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Logradouro.Telefone).NotNull().NotEmpty();

            //Rendimento e Situação Financeira Patrimonial
            RuleFor(pessoa => pessoa.Rendimento).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pessoa => pessoa.SituacaoFinanceiraPatrimonial.AplicacoesFinanceiras).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pessoa => pessoa.SituacaoFinanceiraPatrimonial.BensImoveis).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pessoa => pessoa.SituacaoFinanceiraPatrimonial.BensMoveis).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(pessoa => pessoa.SituacaoFinanceiraPatrimonial.OutrosRendimentos).NotNull().GreaterThanOrEqualTo(0);

            //E-mail
            RuleFor(pessoa => pessoa.Email).EmailAddress();

            //Ocupação e Empregador
            RuleFor(pessoa => pessoa.Ocupacao).NotNull().NotEmpty();
            RuleFor(pessoa => pessoa.Empregador).NotNull().NotEmpty();

            //Procurador
            RuleFor(pessoa => pessoa.PossuiProcurador).NotNull();
            RuleFor(pessoa => pessoa.Procurador.AutorizaTransmissaoDeOrdemPorProcurador).Equal(true)
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Nome).NotNull().NotEmpty()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Email).NotNull().NotEmpty().EmailAddress()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Cpf).Must(ValidadorCpf.Validar)
                .When(pessoa => pessoa.PossuiProcurador == true)
                .WithMessage("CPF do procurador inválido.");
            RuleFor(pessoa => pessoa.Procurador.PossuiCopiaProcuracao).Equal(true)
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.PossuiCopiaDocumentoIdentidadeProcurador).Equal(true)
                .When(pessoa => pessoa.PossuiProcurador == true);

            //Procurador - Identidade
            RuleFor(pessoa => pessoa.Procurador.Identidade.OrgaoEmissorIdentidade).NotNull().NotEmpty()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Identidade.TipoIdentidade).IsInEnum()
                .When(pessoa => pessoa.PossuiProcurador == true); ;
            RuleFor(pessoa => pessoa.Procurador.Identidade.NumeroIdentidade).NotNull().NotEmpty()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Identidade.DataEmissaoDocumento).NotNull().NotEmpty()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Poderes).IsInEnum()
                .When(pessoa => pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Identidade.CodigoVerificacao).NotNull().NotEmpty()
                .When(pessoa => pessoa.Procurador.Identidade.TipoIdentidade == TipoIdentidade.CNH && pessoa.PossuiProcurador == true);
            RuleFor(pessoa => pessoa.Procurador.Identidade.CodigoVerificacao).Null().Empty()
                .When(pessoa => pessoa.Procurador.Identidade.TipoIdentidade != TipoIdentidade.CNH && pessoa.PossuiProcurador == true);

            //Comprovantes e Indicadores
            RuleFor(pessoa => pessoa.PerfilDeRisco).IsInEnum();
            RuleFor(pessoa => pessoa.PossuiPerfilDoInvestidor).NotNull();
            RuleFor(pessoa => pessoa.OperaPorTerceiros).NotNull();
            RuleFor(pessoa => pessoa.PossuiAssinaturaDoCliente).Equal(true);
            RuleFor(pessoa => pessoa.PossuiCopiaDocumentoIdentidade).Equal(true);
            RuleFor(pessoa => pessoa.PossuiCopiaComprovanteResidencia).Equal(true);

            //Atualização Cadastral
            RuleFor(pessoa => pessoa.DataAtualizacaoCadastral).GreaterThan(pessoa => pessoa.DataNascimento)
                .GreaterThan(pessoa => pessoa.Identidade.DataEmissaoDocumento);
           
        }

    }

}
