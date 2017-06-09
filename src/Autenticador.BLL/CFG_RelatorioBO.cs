/*
    Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Web;
using System.Web.SessionState;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Security.Cryptography;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    #region ENUM

    /// <summary>
    /// Enum com Ids e nomes dos documentos customizados para os clientes do gestão escolar.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameDocumentos
    {
        BoletimEscolar = 40,
        DeclaracaoMatricula = 41,
        DeclaracaoMatriculaExAluno = 42,
        DeclaracaoPedidoTransferencia = 43,
        DeclaracaoConclusaoCurso = 44,
        DeclaracaoExAlunoUnidadeEscolar = 45,
        DeclaracaoMatriculaPeriodo = 46,
        FichaIndividualAluno = 47,
        FichaCadastralAluno = 48,
        AutorizacaoPasseio = 49,
        ControleRecebimentoAPM = 50,
        TermoCompromisso = 51,
        ComprovanteEfetivacao = 52,
        DeclaracaoSolicitacaoVaga = 53,
        DeclaracaoSolicitacaoComparecimento = 54,
        DeclaracaoEscolaridade = 55,
        HistoricoEscolar = 61,
        CertificadoHabilitacao = 69,
        CertificadoConclusaoCurso = 70,
        ConviteReuniao = 104,
        RegistroRendimentoEscolar = 126,
        TermoCompromissoClasseEspecial = 140,
        TermoCompromissoSalaRecurso = 141,
        RespostaSalaRecurso = 142,
        ComprovanteMatricula = 145,
        DeclaracaoSolicitacaoTransferencia = 158,
        DeclaracaoTrabalho = 174,
        EncaminhamentoAlunoRemanejado = 200,
        CertidaoEscolaridadeAnterior1972 = 219,
        FichaIndividualAvaliacaoPeriodica = 237
    }

    /// <summary>
    /// Enum com Ids e nomes dos relatórios de acompanhamento do piloto do gestão escolar.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportName
    {
        QtdeAcessoPorEscola = 29,
        QtdeAlunosComTurma = 30,
        QtdeAlunosSemTurma = 31,
        QtdeAulasPorMesDisciplina = 32,
        QtdeAusenciasLancadasPorMesDisciplina = 33,
        QtdeNotasPorMesDisciplina = 34,
        QtdeTurmaPorEscola = 35,
        TurmasDisciplinasComCocFechado = 36,
        TurmasDisciplinasComProfessor = 37,
        TurmasDisciplinasSemProfessor = 38,
        TurmasDisciplinasSemAulasPlanejadas = 39,
        QtdeAcessosPorUsuarioEscola = 56,
        QtdeAtividadesAvaliativasPorMesDisciplina = 57
    }

    /// <summary>
    /// Enum com Ids e nomes dos relatórios do sistema de gestão acadêmica.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameGestaoAcademica
    {
        SituacaoPlanejamentoAulasNotas = 58,
        AlunosExcedentes = 59,
        EstatisticaAlunosExcedentes = 60,
        QuadroEstatisticoAlunosFalta = 62,
        QuadroEstatisticoFormacaoTurma = 63,
        QuadroEstatisticoQuantitativoAluno = 64,
        GestaoAcademicaEfetivacaoNotas = 71,
        GestaoAcademicaListaAlunosPorTurma = 72,
        QuadroEstatisticoMovimentosSerieSexo = 81,
        AlunosExcedentesEscolas = 83,
        GestaoAcademicaAlunosPorEscola = 1,
        GestaoAcademicaAlunosPorTurma = 2,
        GestaoAcademicaAlunosSemTurma = 3,
        GestaoAcademicaDocentesPorTurma = 4,
        GestaoAcademicaListaAlunos = 5,
        GestaoAcademicaListaEscolas = 6,
        GestaoAcademicaListaTurmas = 7,
        GestaoAcademicaTurmasPorEscola = 8,
        QTAvaliacaoDesempenhoEscolar = 88,
        QTFaixaPercentualAlunosPorConceito = 89,
        QTFaixaPercentualAlunosPorMedia = 90,
        QTGraficoDesempenhoAlunosPorSerieGrupamento = 91,
        QTGraficoDistribuicaoConceitosPorEscola = 92,
        QTGraficoDesempenhoDisciplinaGrupamento = 93,
        GestaoAcademicaRegistroAlunoReprovado = 94,
        QTGraficoDesempenhoDisciplina = 101,
        QTGraficoDesempenhoTurmaDisciplina = 102,
        GestaoAcademicaAlocacaoDocente = 103,
        ProgSocial_DescCondicionalidades = 115,
        ProgSocial_LancamentoFrequencia = 116,
        GestaoAcademicaPrevisaoSeriesProximoAnoLetivo = 117,
        GestaoAcademicaPrevisaoSeriesMudancaProximoAnoLetivo = 118,
        GestaoAcademicaPrevisaoSeriesSemPrevisaoCadastrada = 119,
        ProgSocial_NotificacaoDescumprimento = 120,
        ProgSocial_DeclaracaoComparecimentoReuniao = 121,
        GestaoAcademicaAlunosPorClasse = 123,
        GestaoAcademicaAnotacoes = 124,
        GestaoAcademicaAlunosEndereco = 125,
        ProgSocial_AcompanhamentoFrequencia = 127,
        ProgSocial_BonusDesempenho = 128,
        GestaoAcademicaPlanejamento = 129,
        QuantitativoAlunosMerenda = 139,
        ProjetoProgramaEspecialAlunos = 144,
        ProjetoProgramaEspecialNominal = 146,
        ProjetoProgramaEspecialQuantitativoAlunos = 147,
        QuadroEstatisticoEstruturacaoTurmas = 157,
        ListaPresencaReuniaoPais = 159,
        DocentePorEscola = 160,
        ColaboradorPorEscola = 161,
        RelatorioEfetivacaoNotasDocente = 173,
        EstatisticaGeralPorEscola = 176,
        EstatisticaAlunosPorIdadeSexo = 177,
        EstatisticaGemeosAlunosDeficientes = 178,
        RelatorioTotalAlunosBolsaFamiliaPorEscola = 179,
        QuadroMatriculadosEscola = 186,
        BolsaFamiliaProjetoPresenca = 189,
        ImportacaoMatriculaDigitalListagemAlunos = 203,
        QuadroEstatisticoResultadoMatricula = 205,
        AlunosDeficiencia = 220,
        AlunosIntencaoTransferencia = 221,
        AlunosEstatisticaIntencaoTransferencia = 223,
        AlunosCarometro = 228,
        AlunosEtiquetaEnderecamento = 229,
        AlunosIntencaoTransferenciaVariasEscolas = 233,
        Consolidado = 236,
        Carteirinha = 238,
        AlunoDispensaDisciplina = 239,
        PlanejamentoAnualOrientacoesCurriculares = 241,
        GraficoConsolidadoAtividadeAvaliativa = 252,
        AlunosBaixaFrequencia = 253,
        FechamentoInicioAnoLetivoFechamentoMatricula = 254,
        FechamentoInicioAnoLetivoRenovacao = 255,
        FechamentoInicioAnoLetivoFechamentoMatriculaNaoRealizado = 256,
        FechamentoInicioAnoLetivoFechamentoMatriculaNaoRealizadoPorPeriodoCurso = 257,
        AlunosTransporteEscolar = 258
    }

    /// <summary>
    /// Enum com Ids e nomes dos documentos da escola do sistema de gestão acadêmica.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameGestaoAcademicaDocumentosEscola
    {
        DocEscRelacaoAlunosTurma = 113,
        DocEscRelacaoAlunosAvaliados = 105,
        DocEscRelacaoAlunosTurmaEmailNIS = 108,
        DocEscRelacaoAlunosSemHistorico = 107,
        DocEscRelacaoAlunosMatriculadosForaFaixaEtaria = 106,
        DocEscRelacaoMovimentacaoAlunos = 109,
        DocEscRelacaoResponsaveis = 110,
        DocEscRelacaoResponsavelAlunoTurma = 111,
        DocEscRelacaoTurmas = 112,
        DocEscRelatorioAlunosMatriculadosEnsinoInfantil = 149,
        DocEscRelatoriodeFrequenciaProgramasSociais = 150,
        DocEscRelacaoComparativaConceitos = 195,
        DocEscRelacaoComparativaConceitosMapao = 196,
        DocEscRelacaoAlunosAvaliadosListao = 197,
        DocEscRelacaoRemanejados = 198,
        DocEscRelacaoAlunosConcluintes = 199,
        DocEscRelacaoMatriculadosFormacaoTurma = 204,
        DocEscRelacaoAlunosCamposEmBrancoFichaCadastral = 206,
        DocEscRelacaoAlunosDuplicados = 207,
        DocEscRegistroClasse = 209,
        DocEscRegistroAlunoReprovado = 213,
        DocEscRelatoriodeFrequenciaProgramasSociais_STZ = 234

    }

    /// <summary>
    /// Enum com Ids e nomes dos documentos do docente do sistema de gestão acadêmica.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameGestaoAcademicaDocumentosDocente
    {
        DocDctDiarioClasseFrequencia = 242,
        DocDctDiarioClasseAvaliacao = 243,
        DocDctGraficoAtividadeAvaliativa = 244,
        DocDctRelAtividadeAvaliativa = 245,
        DocDctRelTarjetaBimestral = 246,
        DocDctRelFrequenciaBimestral = 247,
        DocDctRelSinteseAula = 248,
        DocDctRelDadosPlanejamento = 249,
        DocDctRelOrientacaoAlcancada = 250,
        DocDctRelAnotacoesAula = 251
    }

    /// <summary>
    /// Enum com Ids e nomes dos relatórios do sistema de migração.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameMigracaoRio
    {
        QuantitativoGeral = 73,
        AlunosPorEscola = 74,
        DadosAluno = 75,
        DadosDocente = 76,
        MovimentacaoPorAluno = 77,
        HistoricoEscolarPorAluno = 78,
        NotasHistoricoPorAluno = 79,
        AvaliacaoPorAluno = 80
    }

    /// <summary>
    /// Enum com Ids e nomes dos relatórios do sistema de biblioteca.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameBiblioteca
    {
        QtdeTitulos = 65,
        Recibo = 66,
        Etiqueta = 67,
        CodigoBarras = 68,
        TituloEditora = 85,
        TituloAutor = 86,
        EtiquetaLombada = 87,
        HistoricoLeitor = 96,
        MaisSolicitados = 97,
        Movimentacao = 98,
        RankingTitulos = 99,
        SituacaoTitulos = 100,
        LeitoresAtrasados = 136,
        LeitoresMaisEmprestaram = 137,
        RankingAutores = 138,
        RankingTurmas = 143,
        NotificacaoAtraso = 148,
        Pesquisador = 192,
        AcervoMemorial = 193,
        AcervoMemoria = 194,
        EtiquetaLombada30 = 214,
        EtiquetaLeitor = 215,
        EstatisticaPorBiblioteca = 225,
        Topografico = 226,
        EtiquetaNotificacaoLeitor = 227,
        EstatisticaNovoLeitor = 230,
        ExemplaresBaixados = 232,
        ImpressaoSalaLeitura = 235,
        ListaReserva = 240
    }

    /// <summary>
    /// Enum com Ids e nomes dos relatórios do sistema de remoção de professores.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameRemocao
    {
        VagasGeraisEspecialista = 82,
        VagasGeraisDisciplina = 84,
        ProfessoresInscritos = 95,
        ResultadoAlocacaoVagas = 114
    }

    /// <summary>
    /// Enum dos relatórios do sistema de dupla regencia
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameDuplaRegencia
    {
        RelacaoDisponibilidades = 130,
        RelacaoAprovacoes = 131,
        RelacaoTerminos = 132,
        DeclaracaoCienciaCompromisso = 133,
        AutorizacaoDuplaRegenciaPI = 134,
        AutorizacaoDuplaRegenciaPII = 135,
        RelacaoReducoes = 169,
        RelatoriosTriplaSemDupla = 208,
        DadosIntegracao = 210,
        QuadrodeVagas = 211,
        ConsultaDadosDocente = 212,
        RelacaoAprovacoesOrgaoCentral = 216,
        RelacaoIrregularidades = 222,
        RelacaoTriplasPotenciaisDuplas = 224
    }

    /// <summary>
    /// Enum dos relatorios e documentos do Sistema de remocao
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameRemocaoSMERJ
    {
        PublicacaoDocNaoClassificados = 151,
        PublicacaoInscricaoPontuacao = 152,
        PublicacaoNovasEscolas = 153,
        PublicacaoQuadroVagasCRE = 154,
        RelatorioConferenciaVagas = 155,
        PublicacaoDocentesClassificados = 156,
        RelatorioInscricoes = 170,
        RelatorioConfirmacoes = 171,
        RelatorioInscricoesExcluidas = 172,
        RelatorioRepescagem = 175,
        RelatorioClassificacaoRepescagem = 188,
        QuantitativoGeralDocentes = 190,
        PublicacaoNovasEscolasRealocados = 191
    }

    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public enum ReportNameQuadroHorario
    {
        QuadroHorarioUE = 162,
        QuadroHorarioProfessor = 163,
        QuadroHorarioTurma = 164,
        RelacaoProfessores = 165,
        MapaAlocacaoDisciplinaProfessor = 166,
        MapaAlocacaoProfessorTurma = 167,
        MapaAlocacaoTurmaDisciplina = 168,
        DiferencaTurmaCurriculo = 180,
        SituacaoGeralExcedentes = 181,
        HistoricoAtribuicaoProfessor = 182,
        DisciplinaSemProfessor = 183,
        ProfessorSemDisciplina = 184,
        DeclaracaoRegenciaTurma = 185,
        DeclaracaoRegenciaTurmaProfessor = 187,
        DisciplinaSemAtendimento = 201,
        ColaboradoresPorLotacao = 202,
        AlocacoesProfessores = 217,
        CargoSerie = 218,
        RelacaoDeClasses = 231
    }


    #endregion ENUM

    /// <summary>
    /// Classe reponsável pelo gerenciamento dos relatório dos sistemas ligados ao sistema.
    /// </summary>
    [Obsolete("Esse recurso está obsoleto. A implementação deve ser realizada no próprio sistema.", false)]
    public class CFG_RelatorioBO : BusinessBase<CFG_RelatorioDAO, CFG_Relatorio>, IRequiresSessionState
    {
        #region CONSTANTES

        public const string SessReportName = "SessReportName";
        public const string SessParameters = "SessParameters";

        #endregion CONSTANTES

        #region PROPRIEDADES

        /// <summary>
        /// Grava o valor do id do relatório na session do usuário corrente.
        /// </summary>
        public static string CurrentReportID
        {
            get
            {
                object rptName = HttpContext.Current.Session[SessReportName];
                if (rptName != null)
                {
                    SymmetricAlgorithm sa = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                    return sa.Decrypt(rptName.ToString(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                }
                return String.Empty;
            }
            set
            {
                SymmetricAlgorithm sa = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                HttpContext.Current.Session[SessReportName] = sa.Encrypt(value, System.Text.Encoding.GetEncoding("iso-8859-1"));
            }
        }

        /// <summary>
        /// Grava o valor dos parâmetros do relatório na session do usuário corrente.
        /// </summary>
        public static string CurrentReportParameters
        {
            get
            {
                object rptParam = HttpContext.Current.Session[SessParameters];
                if (rptParam != null)
                {
                    SymmetricAlgorithm sa = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                    return sa.Decrypt(rptParam.ToString(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                }
                return String.Empty;
            }
            set
            {
                SymmetricAlgorithm sa = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                HttpContext.Current.Session[SessParameters] = sa.Encrypt(value, System.Text.Encoding.GetEncoding("iso-8859-1"));
            }
        }

        #endregion PROPRIEDADES

        /// <summary>
        /// Carrega a session corrente do usuário com as informações para a geração do relatório.
        /// </summary>
        /// <param name="reportID">id do relatório.</param>
        /// <param name="parameters">Parâmetros do relatório.</param>
        public static void SendParametersToReport(string reportID, string parameters)
        {
            #region VALIDA PARAMETROS DE ENTRADA

            if (String.IsNullOrEmpty(reportID))
                throw new ValidationException("O parâmetro reportID é obrigatório.");

            #endregion VALIDA PARAMETROS DE ENTRADA

            CurrentReportID = reportID;
            CurrentReportParameters = parameters;
        }

        /// <summary>
        /// Remover os valores da session com parâmetros para geração dos relatórios.
        /// </summary>
        public static void ClearSessionReportParameters()
        {
            HttpContext.Current.Session.Remove(SessReportName);
            HttpContext.Current.Session.Remove(SessParameters);
        }
    }
}
