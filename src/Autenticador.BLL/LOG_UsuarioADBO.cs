using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using CoreLibrary.Validation.Exceptions;
using CoreLibrary.Security.Cryptography;
using System.IO;
using CoreLibrary.Data.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CoreLibrary.LDAP;
using System.DirectoryServices;

namespace Autenticador.BLL
{
    /// <summary>
    /// Description: LOG_UsuarioAD Business Object. 
    /// </summary>
    public class LOG_UsuarioADBO : BusinessBase<LOG_UsuarioADDAO, LOG_UsuarioAD>
    {
        #region Estrutura

        public struct sLOG_UsuarioAD
        {
            public LOG_UsuarioAD usuarioAD { get; set; }
            public LOG_DadosUsuarioAD dadosUsuario { get; set; }
            public SYS_Usuario usuario { get; set; }
            public PES_Pessoa pessoa { get; set; }
        }

        #endregion

        #region Xml

        [XmlRoot("data")]
        public class LOG_DadosUsuarioAD
        {
            [XmlElement]
            public Guid entidade;
            [XmlElement]
            public string login;
            [XmlElement]
            public string senha;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qtde"></param>
        /// <param name="banco"></param>
        /// <returns></returns>
        public static List<sLOG_UsuarioAD> SelecionaNaoProcessados(TalkDBTransaction banco = null)
        {
            List<sLOG_UsuarioAD> ltRetorno = new List<sLOG_UsuarioAD>();
            LOG_UsuarioADDAO dao = banco == null ?
                new LOG_UsuarioADDAO() :
                new LOG_UsuarioADDAO { _Banco = banco };

            using (DataTable dt = dao.SelecionaNaoProcessados())
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                        ltRetorno.Add
                        (
                            new sLOG_UsuarioAD
                            {
                                usuarioAD = dao.DataRowToEntity(dr, new LOG_UsuarioAD())
                                ,
                                dadosUsuario = GetDadosUsuarioADToEntity(dr["usa_dados"].ToString())
                                ,
                                usuario = (SYS_Usuario)UtilBO.DataRowToEntity(dr, new SYS_Usuario())
                                ,
                                pessoa = !string.IsNullOrEmpty(dr["pes_id"].ToString()) ?
                                            (PES_Pessoa)UtilBO.DataRowToEntity(dr, new PES_Pessoa()) :
                                            new PES_Pessoa()
                            }
                        );
                }
            }

            return ltRetorno;
        }

        /// <summary>
        /// Retorna os dados do log para entidade. 
        /// </summary>
        /// <param name="dados">Dados do log criptografado</param>
        /// <returns></returns>
        public static LOG_DadosUsuarioAD GetDadosUsuarioADToEntity(string dados)
        {
            SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
            dados = encript.Decrypt(dados);

            LOG_DadosUsuarioAD log = new LOG_DadosUsuarioAD();
            using (XmlReader reader = XmlReader.Create(new StringReader(dados)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LOG_DadosUsuarioAD));
                log = (LOG_DadosUsuarioAD)serializer.Deserialize(reader);
            }

            return log;
        }

        /// <summary>
        /// Retorna os dados do log criptografado.
        /// </summary>
        /// <param name="entityUsuario">Entidade do usu�rio</param>
        /// <param name="senha">Senha do usu�rio (Opcional)</param>
        /// <returns></returns>
        public static string GetDadosUsuarioAD(SYS_Usuario entityUsuario, string senha = null)
        {
            LOG_DadosUsuarioAD log = new LOG_DadosUsuarioAD()
            {
                entidade = entityUsuario.ent_id
                ,
                login = entityUsuario.usu_login
                ,
                senha = string.IsNullOrEmpty(senha) ? string.Empty : senha
            };

            // Remover o Namespace xsi, xsd do XML
            XmlSerializerNamespaces xmlNamespace = new XmlSerializerNamespaces();
            xmlNamespace.Add(string.Empty, string.Empty);

            // Omite a declara��o do XML: <?xml version="1.0" encoding="utf-8"?>
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LOG_DadosUsuarioAD));
                serializer.Serialize(writer, log, xmlNamespace);
            }

            SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
            return encript.Encrypt(sb.ToString());
        }

        /// <summary>
        /// Insere o log da a��o como pendente para processamento.
        /// </summary>
        /// <param name="entityUsuario">Entidade do usu�rio</param>
        /// <param name="acao">Enumerador da a��o</param>
        /// <param name="senha">Senha do usu�rio (Opcional)</param>
        /// <returns></returns>
        public static bool Insert(SYS_Usuario entityUsuario, LOG_UsuarioAD.eAcao acao, TalkDBTransaction banco = null, string senha = null)
        {
            // Verifica regras salvar o log da a��o "Alterar Senha" e "Incluir Usu�rio"
            if (LOG_UsuarioAD.eAcao.ExcluirUsuario != acao && string.IsNullOrEmpty(senha))
            {
                throw new ValidationException("Senha � obrigat�rio para esta a��o.");
            }

            // Verifica se usu�rio � do tipo "Usu�rio integrado com AD / Replica��o de senha".
            if ((byte)SYS_Usuario.eIntegracaoAD.IntegradoAD_ReplicacaoSenha != entityUsuario.usu_integracaoAD)
            {
                return false;
            }

            LOG_UsuarioAD entityLog = new LOG_UsuarioAD()
            {
                usu_id = entityUsuario.usu_id
                ,
                usa_acao = (short)acao
                ,
                usa_status = (short)LOG_UsuarioAD.eStatus.Pendente
                ,
                usa_dataAcao = DateTime.Now
                ,
                usa_origemAcao = (short)LOG_UsuarioAD.eOrigem.Autenticador
                ,
                usa_dados = GetDadosUsuarioAD(entityUsuario, senha)
            };

            return banco == null ?
                Save(entityLog) :
                Save(entityLog, banco);
        }

        /// <summary>
        /// Processa os hist�ricos de altera��o de senha e realiza a sincroniza��o entre o Autenticador e AD.
        /// </summary>
        /// <param name="ltLogUsuarioAD">Lista de hist�rico de altera��o de senha.</param>
        /// <returns></returns>
        public static bool ProcessaLogUsuarioAD(List<sLOG_UsuarioAD> ltLogUsuarioAD)
        {
            try
            {
                string dominio = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppDominioAD");
                string organizationalUnitPath = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppOrganizationalUnitPath");
                string descricao = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppUserDescriptionAD");

                LdapUsers userAD = new LdapUsers(LdapUtil.CheckPath(dominio));

                foreach (sLOG_UsuarioAD log in ltLogUsuarioAD)
                {
                    try
                    {
                        bool processou = false;

                        if (log.usuarioAD.usa_origemAcao == (short)LOG_UsuarioAD.eOrigem.Autenticador)
                            processou = SincronizaSenhaAlteradaAutenticador(userAD, dominio, organizationalUnitPath, log, descricao);
                        else
                            processou = SincronizaSenhaAlteradaAD(log);

                        if (processou)
                        {
                            LOG_UsuarioAD usuarioAD = log.usuarioAD;
                            usuarioAD.usa_status = (short)LOG_UsuarioAD.eStatus.Processado;
                            usuarioAD.usa_dataProcessado = DateTime.Now;
                            LOG_UsuarioADBO.Save(usuarioAD);
                        }
                        else
                        {
                            LOG_UsuarioAD usuarioAD = log.usuarioAD;
                            usuarioAD.usa_status = (short)LOG_UsuarioAD.eStatus.Falha;
                            usuarioAD.usa_dataProcessado = DateTime.Now;
                            LOG_UsuarioADBO.Save(usuarioAD);

                            LOG_UsuarioADErro erro = new LOG_UsuarioADErro
                            {
                                usa_id = log.usuarioAD.usa_id
                                ,
                                use_descricaoErro = "O log de altera��o de senha n�o foi processado."
                            };
                            LOG_UsuarioADErroBO.Save(erro);
                        }
                    }
                    catch (Exception ex)
                    {
                        LOG_UsuarioAD usuarioAD = log.usuarioAD;
                        usuarioAD.usa_status = (byte)LOG_UsuarioAD.eStatus.Falha;
                        usuarioAD.usa_dataProcessado = DateTime.Now;
                        LOG_UsuarioADBO.Save(usuarioAD);

                        LOG_UsuarioADErro erro = new LOG_UsuarioADErro
                        {
                            usa_id = log.usuarioAD.usa_id
                            ,
                            use_descricaoErro = ex.Message
                        };
                        LOG_UsuarioADErroBO.Save(erro);

                        UtilBO.GravarErro(ex);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ltLogUsuarioAD.ForEach(p =>
                {
                    LOG_UsuarioAD entityUsuarioAD = p.usuarioAD;
                    entityUsuarioAD.usa_status = (short)LOG_UsuarioAD.eStatus.Pendente;
                    LOG_UsuarioADBO.Save(entityUsuarioAD);
                });

                throw ex;
            }
        }

        /// <summary>
        /// Realiza a sincroniza��o das senhas alteradas no Autenticador com o AD.
        /// </summary>
        /// <param name="userAD">Objeto de conex�o com o Active Directory (AD).</param>
        /// <param name="dominio">Dom�nio do Active Directory (AD).</param>
        /// <param name="organizationalUnitsPath">Caminho de Organizational Units onde os usu�rios s�o criados no Active Directory (AD).</param>
        /// <param name="log">Dados do usu�rio.</param>
        /// <returns></returns>
        public static bool SincronizaSenhaAlteradaAutenticador(LdapUsers userAD, string dominio, string organizationalUnitsPath, sLOG_UsuarioAD log, string descricao)
        {
            bool processou = false;

            if (!LdapUtil.Exists(dominio))
                throw new ValidationException("Dom�nio n�o foi encontrado.");

            if (!LdapUtil.ExistsOrganizationalUnitPath(dominio, organizationalUnitsPath))
                throw new ValidationException("Caminho de Organizational Units n�o foi encontrado.");

            switch (log.usuarioAD.usa_acao)
            {
                case (short)LOG_UsuarioAD.eAcao.IncluirUsuario:
                    {
                        if (userAD.UserExists(log.usuario.usu_login))
                            throw new ArgumentException("Usu�rio j� existe.");

                        processou = userAD.CreateUser(organizationalUnitsPath, log.pessoa.pes_nome, log.usuario.usu_email, log.usuario.usu_login, log.dadosUsuario.senha, descricao);
                        if (log.usuario.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Senha_Expirada)
                        {
                            processou &= userAD.SetExpirePassword(log.usuario.usu_login, true);
                        }
                    }
                    break;
                case (short)LOG_UsuarioAD.eAcao.AlterarSenha:
                    {
                        if (!userAD.UserExists(log.usuario.usu_login))
                            throw new ArgumentException("Usu�rio n�o encontrado.");


                        processou = userAD.SetExpirePassword(log.usuario.usu_login, false);
                        processou &= userAD.SetPassword(log.usuario.usu_login, log.dadosUsuario.senha);

                        if (log.usuario.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Senha_Expirada)
                        {
                            processou &= userAD.SetExpirePassword(log.usuario.usu_login, true);
                        }
                    }
                    break;
                case (short)LOG_UsuarioAD.eAcao.ExcluirUsuario:
                    {
                        if (!userAD.UserExists(log.usuario.usu_login))
                            throw new ArgumentException("Usu�rio n�o encontrado.");

                        processou = userAD.DeleteUser(log.usuario.usu_login);
                    }
                    break;
                default:
                    throw new ValidationException("Opera��o inv�lida.");
            }

            return processou;
        }

        /// <summary>
        /// Realiza a sincroniza��o das senhas alteradas no AD com o Autenticador.
        /// </summary>
        /// <param name="log">Dados do usu�rio.</param>
        /// <returns></returns>
        public static bool SincronizaSenhaAlteradaAD(sLOG_UsuarioAD log)
        {
            bool processou = false;

            switch (log.usuarioAD.usa_acao)
            {
                case (short)LOG_UsuarioAD.eAcao.IncluirUsuario:
                case (short)LOG_UsuarioAD.eAcao.ExcluirUsuario:
                    break;
                case (short)LOG_UsuarioAD.eAcao.AlterarSenha:
                    {
                        SYS_Usuario entityUsuario = log.usuario;
                        entityUsuario.IsNew = false;
                        entityUsuario.usu_senha = log.dadosUsuario.senha;
                        processou = SYS_UsuarioBO.AlterarSenhaAtualizarUsuario(entityUsuario, false, true);
                    }
                    break;
                default:
                    throw new ValidationException("Opera��o inv�lida.");
            }

            return processou;
        }
    }
}