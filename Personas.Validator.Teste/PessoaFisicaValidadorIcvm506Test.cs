using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Personas.Entidade;

namespace Personas.Validator.Teste
{
    [TestClass]
    public class PessoaFisicaValidadorIcvm506Test
    {

        PessoaFisica pessoa;

        [TestInitialize]
        public void Setup()
        {
            pessoa = new PessoaFisica()
            {
                Nome = "Joao da Silva",
                Sexo = Sexo.Masculino,
                TipoNacionalidade = TipoNacionalidade.BrasileiroNato,
                Naturalidade = "Serra Branca",
                Nacionalidade = "Brasileiro",
                NomePai = "Jose da Silva",
                NomeMae = "Maria da Silva",
                EstadoCivil = EstadoCivil.Casado,
                NomeConjuge = "Joana dos Santos Silva",
                DataNascimento = new DateTime(1981, 2, 11),
                Identidade = new Identidade
                {
                    DataEmissaoDocumento = new DateTime(1994, 12, 1),
                    NumeroIdentidade = "Abc123",
                    TipoIdentidade = TipoIdentidade.CNH,
                    OrgaoEmissorIdentidade = "DETRAN/RJ",
                    CodigoVerificacao = "ABC"
                },
                Cpf = "467.272.206-62",
                Logradouro = new Logradouro
                {
                    Endereco = "Rua das Flores",
                    Complemento = "Complemento",
                    Estado = "Paraiba",
                    Cidade = "Joao Pessoa",
                    Bairro = "Bairro",
                    Numero = "123",
                    Telefone = "(53)1234-5678"
                },
                Ocupacao = "Analista de Sistemas",
                Empregador = "Personas Tech",
                Rendimento = 0,
                SituacaoFinanceiraPatrimonial = new SituacaoFinanceiraPatrimonial
                {
                    AplicacoesFinanceiras = 0,
                    BensImoveis = 0,
                    BensMoveis = 0,
                    OutrosRendimentos = 0
                },
                PerfilDeRisco = PerfilDeRisco.Baixo,
                OperaPorTerceiros = false,
                PossuiProcurador = true,
                Procurador = new Procurador
                {
                    Nome = "Joana da SIlva",
                    Cpf = "718.941.089-90",
                    Email = "joana@personas.tech",
                    Poderes = PoderesDoProcurador.PlenosPoderes,
                    PossuiCopiaDocumentoIdentidadeProcurador = true,
                    PossuiCopiaProcuracao = true,
                    AutorizaTransmissaoDeOrdemPorProcurador = true,
                    Identidade = new Identidade
                    {
                        TipoIdentidade = TipoIdentidade.CNH,
                        NumeroIdentidade = "123",
                        CodigoVerificacao = "A",
                        OrgaoEmissorIdentidade = "DETRAN/PB",
                        DataEmissaoDocumento = new DateTime(2000, 01, 01)
                    },
                },
                DataAtualizacaoCadastral = new DateTime(2016, 01, 01),
                Email = "joao@personas.tech",
                PossuiAssinaturaDoCliente = true,
                PossuiCopiaComprovanteResidencia = true,
                PossuiCopiaDocumentoIdentidade = true,
            };
        }

        [TestMethod]
        public void Icvm506PessoaDeveSerValida()
        {
            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeTrue();

        }

        [TestMethod]
        public void Icvm506PessoaSemNomeNaoDeveSerValida()
        {
            pessoa.Nome = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemSexoNaoDeveSerValida()
        {
            pessoa.Sexo = (Sexo)4;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemNacionalidadeNaturalidadeNaoDeveSerValida()
        {
            pessoa.TipoNacionalidade = (TipoNacionalidade)99;
            pessoa.Nacionalidade = null;
            pessoa.Naturalidade = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemFiliacaoNaoDeveSerValida()
        {
            pessoa.NomePai = null;
            pessoa.NomeMae = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaCasadoSemConjugeNaoDeveSerValida()
        {
            pessoa.EstadoCivil = EstadoCivil.Casado;
            pessoa.NomeConjuge = null;
            
            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSolteiroComConjugeNaoDeveSerValida()
        {
            pessoa.EstadoCivil = EstadoCivil.Solteiro;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaComIdentidadeCNHSemCodigoVerificacaoNaoDeveSerValida()
        {
            pessoa.Identidade.CodigoVerificacao = null;
            
            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaComIdentidadeAnteriorADataDeNascimentoNaoDeveSerValida()
        {
            pessoa.Identidade.DataEmissaoDocumento = new DateTime(1970, 12, 1);

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemIdentidadeNaoDeveSerValida()
        {
            pessoa.Identidade = new Identidade();

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaCPFInvalidoNaoDeveSerValida()
        {
            pessoa.Cpf = "067.272.206-62";
                
            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemCPFNaoDeveSerValida()
        {
            pessoa.Cpf = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemLogradouroNaoDeveSerValida()
        {
            pessoa.Logradouro = new Logradouro();

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemOcupacaoEEmpregadorNaoDeveSerValida()
        {
            pessoa.Ocupacao = null;
            pessoa.Empregador = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();

        }

        [TestMethod]
        public void Icvm506PessoaSemRendimentoNaoDeveSerValida()
        {
            pessoa.Rendimento = -1;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSemSituacaoFinanceiraPatrimonialNaoDeveSerValida()
        {
            pessoa.SituacaoFinanceiraPatrimonial = new SituacaoFinanceiraPatrimonial()
            {
                AplicacoesFinanceiras = -1,
                BensImoveis = -2,
                BensMoveis = -3,
                OutrosRendimentos = -4
            };

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSemPerfilDeRiscoNaoDeveSerValida()
        {
            pessoa.PerfilDeRisco = (PerfilDeRisco)10;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComEmailInvalidoNaoDeveSerValida()
        {
            pessoa.Email = "joaoaAtPersonasTech";

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorSemDadosDeProcuradorNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador = new Procurador();

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSemProcuradorESemDadosDeProcuradorDeveSerValida()
        {
            pessoa.PossuiProcurador = false;
            pessoa.Procurador = new Procurador();

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorECPFProcuradorInvalidoNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.Cpf = "000.000.111-10";

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorESemCPFProcuradorNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.Cpf = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorESemIdentidadeDoProcuradorNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.Identidade = new Identidade();

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorEIdentidadeDoProcuradorCNHSemCodigoVerificacaoNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.Identidade.CodigoVerificacao = null;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }
        
        [TestMethod]
        public void Icvm506PessoaComProcuradorESemCopiaProcuracaoNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.PossuiCopiaProcuracao = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComProcuradorESemCopiaDocumentoIdentidadeProcuradorNaoDeveSerValida()
        {
            pessoa.PossuiProcurador = true;
            pessoa.Procurador.PossuiCopiaDocumentoIdentidadeProcurador = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSomProcuradorESemCopiaProcuracaoDeveSerValida()
        {
            pessoa.PossuiProcurador = false;
            pessoa.Procurador.PossuiCopiaProcuracao = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Icvm506PessoaSemProcuradorESemCopiaDocumentoIdentidadeProcuradorDeveSerValida()
        {
            pessoa.PossuiProcurador = false;
            pessoa.Procurador.PossuiCopiaDocumentoIdentidadeProcurador = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Icvm506PessoaSemAssinaturaNaoDeveSerValida()
        {
            pessoa.PossuiAssinaturaDoCliente = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSemCopiaDocumentoIdentidadeNaoDeveSerValida()
        {
            pessoa.PossuiCopiaDocumentoIdentidade = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaSemCopiaComprovanteDeResidenciaNaoDeveSerValida()
        {
            pessoa.PossuiCopiaComprovanteResidencia = false;

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Icvm506PessoaComDataDeAtualizacaoCadastralAnteriorADataDeNascimentoNaoDeveSerValida()
        {
            pessoa.DataAtualizacaoCadastral = new DateTime(1964, 08, 10);

            var validator = new PessoaFisicaValidadorIcvm506();

            var result = validator.Validate(pessoa);

            result.IsValid.Should().BeFalse();
        }

    }
}
