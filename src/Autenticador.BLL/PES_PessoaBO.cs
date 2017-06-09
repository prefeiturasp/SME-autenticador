using System;
using System.Data;
using System.ComponentModel;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.Xml;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
using System.Collections.Generic;

namespace Autenticador.BLL
{
    #region Enumeradores

    /// <summary>
    /// Tipo de raça/cor da pessoa.
    /// </summary>
    public enum PES_Pessoa_RacaCorTipo
    {
        Branca = 1
        ,
        Preta = 2
        ,
        Parda = 3
        ,
        Amarela = 4
        ,
        Indigena = 5
        ,
        NaoDeclarada = 6
    }

    #endregion Enumeradores

    public class PES_PessoaBO : BusinessBase<PES_PessoaDAO, PES_Pessoa>
    {
        #region Métodos que buscam a Foto da pessoa

        /// <summary>
        /// Retorna a entidade CFG_Arquivo com a foto da pessoa, através do id da pessoa.
        /// </summary>
        /// <param name="pes_id">ID da pessoa</param>
        /// <returns></returns>
        public static CFG_Arquivo RetornaFotoPor_Pessoa(Guid pes_id)
        {
            PES_Pessoa entPessoa = GetEntity(new PES_Pessoa { pes_id = pes_id });

            return CFG_ArquivoBO.GetEntity(new CFG_Arquivo { arq_id = entPessoa.arq_idFoto });
        }

        /// <summary>
        /// Retorna a entidade CFG_Arquivo com a foto da pessoa, através da entidade da pessoa.
        /// </summary>
        /// <param name="entPessoa">Entidade da pessoa</param>
        /// <param name="bancoCore">Transação com banco de dados do Core</param>
        /// <returns></returns>
        public static CFG_Arquivo RetornaFotoPor_Pessoa(PES_Pessoa entPessoa, TalkDBTransaction bancoCore)
        {
            if (entPessoa.IsNew)
                GetEntity(entPessoa, bancoCore);

            return CFG_ArquivoBO.GetEntity(new CFG_Arquivo { arq_id = entPessoa.arq_idFoto }, bancoCore);
        }

        /// <summary>
        /// Retorna a entidade CFG_Arquivo com a foto da pessoa, através do id da pessoa.
        /// </summary>
        /// <param name="pes_id">ID da pessoa</param>
        /// <param name="bancoCore">Transação com banco de dados do Core</param>
        /// <returns></returns>
        public static CFG_Arquivo RetornaFotoPor_Pessoa(Guid pes_id, TalkDBTransaction bancoCore)
        {
            PES_Pessoa entPessoa = GetEntity(new PES_Pessoa { pes_id = pes_id }, bancoCore);

            return CFG_ArquivoBO.GetEntity(new CFG_Arquivo { arq_id = entPessoa.arq_idFoto }, bancoCore);
        }

        /// <summary>
        /// Retorna a entidade CFG_Arquivo com a foto da pessoa, através da entidade da pessoa.
        /// </summary>
        /// <param name="entPessoa">Entidade da pessoa</param>
        /// <returns></returns>
        public static CFG_Arquivo RetornaFotoPor_Pessoa(PES_Pessoa entPessoa)
        {
            if (entPessoa.IsNew)
                GetEntity(entPessoa);

            return CFG_ArquivoBO.GetEntity(new CFG_Arquivo { arq_id = entPessoa.arq_idFoto });
        }

        #endregion Métodos que buscam a Foto da pessoa

        /// <summary>
        ///
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SelecionaPorNome(string pes_nome)
        {
            totalRecords = 0;
            PES_PessoaDAO dao = new PES_PessoaDAO();
            return dao.SelectBy_Nome(pes_nome);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="data"></param>
        /// <param name="cpf"></param>
        /// <param name="rg"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect(string nome, DateTime data, string cpf, string rg, int currentPage, int pageSize)
        {
            totalRecords = 0;
            PES_PessoaDAO dao = new PES_PessoaDAO();

            return dao.SelectBy_Busca(nome, data, cpf, rg, currentPage, pageSize, out totalRecords);
        }

        /// <summary>
        /// Salva a entidade Pessoa e os subcadastros da pessoa.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityPessoaDeficiencia"></param>
        /// <param name="dtEndereco"></param>
        /// <param name="dtContato"></param>
        /// <param name="dtDocumento"></param>
        /// <param name="dtCertidao"></param>
        /// <param name="pai_idAntigo"></param>
        /// <param name="cid_idAntigo"></param>
        /// <param name="pes_idPaiAntigo"></param>
        /// <param name="pes_idMaeAntigo"></param>
        /// <param name="tes_idAntigo"></param>
        /// <param name="tde_idAntigo"></param>
        /// <param name="arquivosPermitidos">Tipos de arquivos permitidos para a foto</param>
        /// <param name="tamanhoMaximoKB">Tamanho máximo de arquivos em KB</param>
        /// <param name="entFoto">Entidade da foto da pessoa</param>
        /// <param name="ExcluirImagemAtual">Flag que indica se será excluída a imagem atual da pessoa</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            PES_Pessoa entity
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , DataTable dtEndereco
            , DataTable dtContato
            , DataTable dtDocumento
            , DataTable dtCertidao
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
            , string[] arquivosPermitidos
            , int tamanhoMaximoKB
            , CFG_Arquivo entFoto
            , bool ExcluirImagemAtual
        )
        {
            TalkDBTransaction banco = new PES_PessoaDAO()._Banco.CopyThisInstance();
            banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                return Salvar_ComFoto(banco, entFoto, tamanhoMaximoKB, arquivosPermitidos, entity, ExcluirImagemAtual, entityPessoaDeficiencia, dtEndereco, dtContato, dtDocumento, dtCertidao, pai_idAntigo, cid_idAntigo, pes_idPaiAntigo, pes_idMaeAntigo, tes_idAntigo, tde_idAntigo);
            }
            catch (Exception err)
            {
                banco.Close(err);
                throw new Exception("Erro ao salvar dados da pessoa (Salvar_Comfoto)", err);
            }
            finally
            {
                if (banco.ConnectionIsOpen)
                    banco.Close();
            }
        }

        /// <summary>
        /// Salva a entidade Pessoa e os subcadastros da pessoa.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityPessoaDeficiencia"></param>
        /// <param name="dtEndereco"></param>
        /// <param name="dtContato"></param>
        /// <param name="dtDocumento"></param>
        /// <param name="dtCertidao"></param>
        /// <param name="pai_idAntigo"></param>
        /// <param name="cid_idAntigo"></param>
        /// <param name="pes_idPaiAntigo"></param>
        /// <param name="pes_idMaeAntigo"></param>
        /// <param name="tes_idAntigo"></param>
        /// <param name="tde_idAntigo"></param>
        /// <param name="arquivosPermitidos">Tipos de arquivos permitidos para a foto</param>
        /// <param name="tamanhoMaximoKB">Tamanho máximo de arquivos em KB</param>
        /// <param name="banco">Transação com banco</param>
        /// <param name="entFoto">Entidade da foto da pessoa</param>
        /// <param name="ExcluirImagemAtual">Flag que indica se será excluída a imagem atual da pessoa</param>
        /// <returns></returns>
        private static bool Salvar_ComFoto
        (
            TalkDBTransaction banco
            , CFG_Arquivo entFoto
            , int tamanhoMaximoKB
            , string[] arquivosPermitidos
            , PES_Pessoa entity
            , bool ExcluirImagemAtual
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , DataTable dtEndereco
            , DataTable dtContato
            , DataTable dtDocumento
            , DataTable dtCertidao
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
        )
        {
            CFG_Arquivo entArquivoExcluir = null;

            if (entFoto != null)
            {
                // Salva a foto da pessoa antes de salvar a pessoa.
                CFG_ArquivoBO.Save(entFoto, tamanhoMaximoKB, arquivosPermitidos, banco);

                if ((entity.arq_idFoto > 0) && (entity.arq_idFoto != entFoto.arq_id))
                {
                    // Exclui fisicamente a foto anterior.
                    CFG_Arquivo entArquivo = new CFG_Arquivo
                    {
                        arq_id = entity.arq_idFoto
                    };
                    CFG_ArquivoBO.Delete(entArquivo, banco);
                }

                // Seta o id do arquivo para a pessoa.
                entity.arq_idFoto = entFoto.arq_id;
            }
            else if (ExcluirImagemAtual)
            {
                entArquivoExcluir = new CFG_Arquivo
                {
                    arq_id = entity.arq_idFoto
                };
                entity.arq_idFoto = -1;
            }

            bool ret = Save
                (
                    entity
                    , entityPessoaDeficiencia
                    , dtEndereco
                    , dtContato
                    , dtDocumento
                    , dtCertidao
                    , pai_idAntigo
                    , cid_idAntigo
                    , pes_idPaiAntigo
                    , pes_idMaeAntigo
                    , tes_idAntigo
                    , tde_idAntigo
                    , banco
                );

            if ((ret) && (entArquivoExcluir != null))
            {
                if (entArquivoExcluir.arq_id > 0)
                {
                    // Exclui fisicamente a foto.
                    CFG_ArquivoBO.Delete(entArquivoExcluir, banco);
                }
            }

            return ret;
        }

        /// <summary>
        /// Salva a entidade Pessoa e os subcadastros da pessoa.
        /// </summary>
        /// <param name="entity">Entidade PES_Pessoa</param>
        /// <param name="entityPessoaDeficiencia">Entidade PES_PessoaDeficiencia</param>
        /// <param name="entityCertidaoCivil">Entidade PES_CertidaoCivil</param>
        /// <param name="dtEndereco">DataTable de endereços</param>
        /// <param name="dtContato">DataTable de contatos</param>
        /// <param name="dtDocumento">DataTable de documentos</param>
        /// <param name="banco">Transação do Core</param>
        /// <param name="arquivosPermitidos">Extensões de tipos de arquivos permitidos para a foto</param>
        /// <param name="tamanhoMaximoKB">Tamanho máximo da foto permitida</param>
        /// <param name="entFoto">Entidade da foto da pessoa</param>
        /// <param name="ExcluirImagemAtual">Indica se imagem atual será excluída</param>
        /// <returns></returns>
        public static bool Save
            (
                 PES_Pessoa entity
                , PES_PessoaDeficiencia entityPessoaDeficiencia
                , PES_CertidaoCivil entityCertidaoCivil
                , DataTable dtEndereco
                , DataTable dtContato
                , DataTable dtDocumento
                , TalkDBTransaction banco
                , string[] arquivosPermitidos
                , int tamanhoMaximoKB
                , CFG_Arquivo entFoto
                , bool ExcluirImagemAtual
            )
        {
            PES_PessoaDAO daoPessoa = new PES_PessoaDAO();
            if (banco == null)
                daoPessoa._Banco.Open(IsolationLevel.ReadCommitted);
            else
                daoPessoa._Banco = banco;

            try
            {
                CFG_Arquivo entArquivoExcluir = null;

                if (entFoto != null)
                {
                    // Salva a foto da pessoa antes de salvar a pessoa.
                    CFG_ArquivoBO.Save(entFoto, tamanhoMaximoKB, arquivosPermitidos, banco);

                    if ((entity.arq_idFoto > 0) && (entity.arq_idFoto != entFoto.arq_id))
                    {
                        // Exclui fisicamente a foto anterior.
                        CFG_Arquivo entArquivo = new CFG_Arquivo
                        {
                            arq_id = entity.arq_idFoto
                        };
                        CFG_ArquivoBO.Delete(entArquivo, banco);
                    }

                    // Seta o id do arquivo para a pessoa.
                    entity.arq_idFoto = entFoto.arq_id;
                }
                else if (ExcluirImagemAtual)
                {
                    entArquivoExcluir = new CFG_Arquivo
                    {
                        arq_id = entity.arq_idFoto
                    };
                    entity.arq_idFoto = -1;
                }

                bool ret =
                    Save(entity
                         , entityPessoaDeficiencia
                         , entityCertidaoCivil
                         , dtEndereco
                         , dtContato
                         , dtDocumento
                         , banco);

                if ((ret) && (entArquivoExcluir != null))
                {
                    if (entArquivoExcluir.arq_id > 0)
                    {
                        // Exclui fisicamente a foto.
                        CFG_ArquivoBO.Delete(entArquivoExcluir, banco);
                    }
                }

                return ret;
            }
            catch (Exception err)
            {
                if (banco == null)
                    daoPessoa._Banco.Close(err);
                throw new Exception("Erro ao salvar dados!", err);
            }
            finally
            {
                if (banco == null)
                    daoPessoa._Banco.Close();
            }
        }

        /// <summary>
        /// Salva a entidade Pessoa e os subcadastros da pessoa.
        /// </summary>
        /// <param name="entity">Entidade PES_Pessoa</param>
        /// <param name="entityPessoaDeficiencia">Entidade PES_PessoaDeficiencia</param>
        /// <param name="entityCertidaoCivil">Entidade PES_CertidaoCivil</param>
        /// <param name="dtEndereco">DataTable de endereços</param>
        /// <param name="dtContato">DataTable de contatos</param>
        /// <param name="dtDocumento">DataTable de documentos</param>
        /// <param name="banco">Transação do Core</param>
        /// <returns></returns>
        public static bool Save
            (
                 PES_Pessoa entity
                , PES_PessoaDeficiencia entityPessoaDeficiencia
                , PES_CertidaoCivil entityCertidaoCivil
                , DataTable dtEndereco
                , DataTable dtContato
                , DataTable dtDocumento
                , TalkDBTransaction banco
            )
        {
            PES_PessoaDAO daoPessoa = new PES_PessoaDAO();
            if (banco == null)
                daoPessoa._Banco.Open(IsolationLevel.ReadCommitted);
            else
                daoPessoa._Banco = banco;

            try
            {
                // Valida data de nascimento
                if (entity.pes_dataNascimento > DateTime.Now)
                    throw new ValidationException("A data de nascimento não pode ser maior que a data atual.");

                // Verifica se os dados da pessoa serão sempre salvos em maiúsculo.
                string sSalvarMaiusculo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.SALVAR_SEMPRE_MAIUSCULO);
                bool Salvar_Sempre_Maiusculo = (!string.IsNullOrEmpty(sSalvarMaiusculo) && Convert.ToBoolean(sSalvarMaiusculo));

                // Altera os nomes para maiúsculo
                if (Salvar_Sempre_Maiusculo)
                {
                    entity.pes_nome = entity.pes_nome.ToUpper();

                    if (!String.IsNullOrEmpty(entity.pes_nome_abreviado))
                        entity.pes_nome_abreviado = entity.pes_nome_abreviado.ToUpper();

                    if (!String.IsNullOrEmpty(entity.pes_nomeSocial))
                        entity.pes_nomeSocial = entity.pes_nomeSocial.ToUpper();
                }

                // Carrega a entidade antiga de PES_Pessoa e PES_PessoaDeficiencia para
                // atualizar integridade, caso necessário.
                PES_Pessoa entityPessoaAntigo = new PES_Pessoa { pes_id = entity.pes_id };
                PES_PessoaDeficiencia entityPessoaDeficienciaAntigo = new PES_PessoaDeficiencia { pes_id = entity.pes_id };
                PES_CertidaoCivil entityCertidaoCivilAntigo = new PES_CertidaoCivil { pes_id = entity.pes_id };

                // Exclui registros antigos de PessoaDeficiencia e CertidaoCivil.
                if (!entity.IsNew)
                {
                    PES_PessoaBO.GetEntity(entityPessoaAntigo, banco);

                    List<PES_PessoaDeficiencia> ltPessoaDeficiencia = PES_PessoaDeficienciaBO.SelecionaPorPessoa(entity.pes_id);
                    foreach (PES_PessoaDeficiencia ent in ltPessoaDeficiencia)
                    {
                        entityPessoaDeficienciaAntigo = ent;
                        PES_PessoaDeficienciaBO.Delete(ent, daoPessoa._Banco);
                    }

                    List<PES_CertidaoCivil> ltCertidaoCivil = PES_CertidaoCivilBO.SelecionaPorPessoa(entity.pes_id);
                    foreach (PES_CertidaoCivil ent in ltCertidaoCivil)
                    {
                        if (ent.ctc_tipo != entityCertidaoCivil.ctc_tipo)
                        {
                            PES_CertidaoCivilBO.Delete(ent, daoPessoa._Banco);
                        }
                        else
                        {
                            entityCertidaoCivilAntigo = ent;
                        }
                    }
                }

                // Salva os dados na tabela PES_Pessoa
                if (entity.Validate())
                {
                    daoPessoa.Salvar(entity);
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }

                // Salva os dados na tabela PES_PessoaDeficiencia.
                if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                {
                    entityPessoaDeficiencia.pes_id = entity.pes_id;
                    PES_PessoaDeficienciaBO.Save(entityPessoaDeficiencia, daoPessoa._Banco);
                }

                // Salva os dados na tabela PES_CertidaoCivil.
                if (entityCertidaoCivil.ctc_tipo > 0)
                {
                    entityCertidaoCivil.pes_id = entity.pes_id;
                    PES_CertidaoCivilBO.Save(entityCertidaoCivil, daoPessoa._Banco);
                }

                // Salva os dados na tabela PES_PessoaEndereco.
                PES_PessoaEnderecoBO.SaveEnderecosPessoa(daoPessoa._Banco, entity, dtEndereco);

                // Salva os dados na tabela PES_PessoaContato.
                PES_PessoaContatoBO.SaveContatosPessoa(daoPessoa._Banco, entity, dtContato);

                // Salva os dados na tabela PES_PessoaDocumento.
                PES_PessoaDocumentoBO.SaveDocumentosPessoa(daoPessoa._Banco, entity, dtDocumento);

                UpdateIntegridades
                    (
                        entity
                        , entityPessoaDeficiencia
                        , entityCertidaoCivil
                        , entityPessoaAntigo
                        , entityPessoaDeficienciaAntigo
                        , entityCertidaoCivilAntigo
                        , banco
                    );

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    daoPessoa._Banco.Close(err);
                throw new Exception("Erro ao salvar dados!", err);
            }
            finally
            {
                if (banco == null)
                    daoPessoa._Banco.Close();
            }
        }

        /// <summary>
        /// Salva a entidade Pessoa e os subcadastros da pessoa.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityPessoaDeficiencia"></param>
        /// <param name="dtEndereco"></param>
        /// <param name="dtContato"></param>
        /// <param name="dtDocumento"></param>
        /// <param name="dtCertidao"></param>
        /// <param name="pai_idAntigo"></param>
        /// <param name="cid_idAntigo"></param>
        /// <param name="pes_idPaiAntigo"></param>
        /// <param name="pes_idMaeAntigo"></param>
        /// <param name="tes_idAntigo"></param>
        /// <param name="tde_idAntigo"></param>
        /// <param name="banco"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool Save
        (
            PES_Pessoa entity
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , DataTable dtEndereco
            , DataTable dtContato
            , DataTable dtDocumento
            , DataTable dtCertidao
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
            , TalkDBTransaction banco
        )
        {
            PES_PessoaDAO pesDAL = new PES_PessoaDAO();

            if (banco == null)
                pesDAL._Banco.Open(IsolationLevel.ReadCommitted);
            else
                pesDAL._Banco = banco;

            try
            {
                if (entity.pes_dataNascimento > DateTime.Now)
                    throw new ValidationException("A data de nascimento não pode ser maior que a data atual.");

                //Verifica se os dados da pessoa serão sempre salvos em maiúsculo.
                string sSalvarMaiusculo = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.SALVAR_SEMPRE_MAIUSCULO);
                bool Salvar_Sempre_Maiusculo = !string.IsNullOrEmpty(sSalvarMaiusculo) && Convert.ToBoolean(sSalvarMaiusculo);

                //Altera os nomes para maiúsculo
                if (Salvar_Sempre_Maiusculo)
                {
                    entity.pes_nome = entity.pes_nome.ToUpper();

                    if (!String.IsNullOrEmpty(entity.pes_nome_abreviado))
                        entity.pes_nome_abreviado = entity.pes_nome_abreviado.ToUpper();

                    if (!String.IsNullOrEmpty(entity.pes_nomeSocial))
                        entity.pes_nomeSocial = entity.pes_nomeSocial.ToUpper();
                }

                //Salva os dados na tabela PES_Pessoa
                if (entity.Validate())
                {
                    pesDAL.Salvar(entity);
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }

                // Salva os dados na tabela PES_PessoaDeficiencia.
                if (entity.IsNew)
                {
                    if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                    {
                        entityPessoaDeficiencia.pes_id = entity.pes_id;
                        PES_PessoaDeficienciaBO.Save(entityPessoaDeficiencia, pesDAL._Banco);
                    }
                }
                else
                {
                    if (tde_idAntigo != Guid.Empty)
                    {
                        PES_PessoaDeficiencia tde = new PES_PessoaDeficiencia
                        {
                            pes_id = entity.pes_id
                            ,
                            tde_id = tde_idAntigo
                        };
                        PES_PessoaDeficienciaBO.Delete(tde, pesDAL._Banco);
                    }

                    if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                    {
                        entityPessoaDeficiencia.pes_id = entity.pes_id;
                        PES_PessoaDeficienciaBO.Save(entityPessoaDeficiencia, pesDAL._Banco);
                    }
                }

                // Salva os dados na tabela PES_PessoaEndereco.
                PES_PessoaEnderecoBO.SaveEnderecosPessoa(pesDAL._Banco, entity, dtEndereco);

                // Salva os dados na tabela PES_PessoaContato.
                PES_PessoaContatoBO.SaveContatosPessoa(pesDAL._Banco, entity, dtContato);

                // Salva os dados na tabela PES_PessoaDocumento.
                PES_PessoaDocumentoBO.SaveDocumentosPessoa(pesDAL._Banco, entity, dtDocumento);

                // Salva os dados na tabela PES_CertidaoCivil.
                PES_CertidaoCivilBO.SaveCertidoesPessoa(pesDAL._Banco, entity, dtCertidao);

                UpdateIntegridades(pesDAL, entity, entityPessoaDeficiencia, pai_idAntigo, cid_idAntigo, pes_idPaiAntigo, pes_idMaeAntigo, tes_idAntigo, tde_idAntigo);

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    pesDAL._Banco.Close(err);
                throw new Exception("Erro ao salvar dados da pessoa.", err);
            }
            finally
            {
                if (banco == null)
                    pesDAL._Banco.Close();
            }
        }

        /// <summary>
        /// Incrementa 1 no campo integridade da pessoa.
        /// </summary>
        /// <param name="pes_id">ID da pessoa - obrigatório</param>
        /// <param name="banco">Transação - obrigatório</param>
        /// <returns>Sucesso na operação</returns>
        public static bool IncrementaIntegridade(Guid pes_id, TalkDBTransaction banco)
        {
            PES_PessoaDAO dao = new PES_PessoaDAO()
            {
                _Banco = banco
            };
            return dao.Update_IncrementaIntegridade(pes_id);
        }

        /// <summary>
        /// Decrementa 1 do campo integridade da pessoa.
        /// </summary>
        /// <param name="pes_id">ID da pessoa - obrigatório</param>
        /// <param name="banco">Transação - obrigatório</param>
        /// <returns>Sucesso na operação</returns>
        public static bool DecrementaIntegridade(Guid pes_id, TalkDBTransaction banco)
        {
            PES_PessoaDAO dao = new PES_PessoaDAO()
            {
                _Banco = banco
            };
            return dao.Update_DecrementaIntegridade(pes_id);
        }

        /// <summary>
        ///  Incrementa/Decrementa as integridades necessárias para cadastrar a pessoa.
        /// </summary>
        /// <param name="entity">Entidade PES_Pessoa</param>
        /// <param name="entityPessoaDeficiencia">Entidade PES_PessoaDeficiencia</param>
        /// <param name="entityCertidaoCivil">Entidade PES_CertidaoCivil</param>
        /// <param name="dtEndereco">DataTable de endereços</param>
        /// <param name="dtContato">DataTable de contatos</param>
        /// <param name="dtDocumento">DataTable de documentos</param>
        /// <param name="banco">Transação do Core</param>
        private static void UpdateIntegridades
            (
                PES_Pessoa entityPessoa
                , PES_PessoaDeficiencia entityPessoaDeficiencia
                , PES_CertidaoCivil entityCertidaoCivil
                , PES_Pessoa entityPessoaAntigo
                , PES_PessoaDeficiencia entityPessoaDeficienciaAntigo
                , PES_CertidaoCivil entityCertidaoCivilAntigo
                , TalkDBTransaction banco
            )
        {
            PES_PessoaDAO daoPessoa = new PES_PessoaDAO { _Banco = banco };
            END_PaisDAO daoPais = new END_PaisDAO { _Banco = banco };
            END_CidadeDAO daoCidade = new END_CidadeDAO { _Banco = banco };
            PES_TipoEscolaridadeDAO daoTipoEscolaridade = new PES_TipoEscolaridadeDAO { _Banco = banco };
            PES_TipoDeficienciaDAO daoTipoDeficiencia = new PES_TipoDeficienciaDAO { _Banco = banco };
            END_UnidadeFederativaDAO daoUnidadeFederativa = new END_UnidadeFederativaDAO { _Banco = banco };

            if (entityPessoa.IsNew)
            {
                //Incrementa um na integridade do pais (se existir)
                if (entityPessoa.pai_idNacionalidade != Guid.Empty)
                    daoPais.Update_IncrementaIntegridade(entityPessoa.pai_idNacionalidade);

                //Incrementa um na integridade da cidade (se existir)
                if (entityPessoa.cid_idNaturalidade != Guid.Empty)
                    daoCidade.Update_IncrementaIntegridade(entityPessoa.cid_idNaturalidade);

                //Incrementa um na integridade da pessoa pai (se existir)
                if (entityPessoa.pes_idFiliacaoPai != Guid.Empty)
                    daoPessoa.Update_IncrementaIntegridade(entityPessoa.pes_idFiliacaoPai);

                //Incrementa um na integridade da pessoa mãe (se existir)
                if (entityPessoa.pes_idFiliacaoMae != Guid.Empty)
                    daoPessoa.Update_IncrementaIntegridade(entityPessoa.pes_idFiliacaoMae);

                //Incrementa um na integridade do tipo de escolaridade (se existir)
                if (entityPessoa.tes_id != Guid.Empty)
                    daoTipoEscolaridade.Update_IncrementaIntegridade(entityPessoa.tes_id);

                //Incrementa um na integridade do tipo de deficiência (se existir)
                if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                    daoTipoDeficiencia.Update_IncrementaIntegridade(entityPessoaDeficiencia.tde_id);

                //Incrementa um na integridade da Cidade da certidão civil (se existir)
                if (entityCertidaoCivil.cid_idCartorio != Guid.Empty)
                    daoCidade.Update_IncrementaIntegridade(entityCertidaoCivil.cid_idCartorio);

                //Incrementa um na integridade da Unidade Federativa da certidão civil (se existir)
                if (entityCertidaoCivil.unf_idCartorio != Guid.Empty)
                    daoUnidadeFederativa.Update_IncrementaIntegridade(entityCertidaoCivil.unf_idCartorio);
            }
            else
            {
                //Integridade Pais
                if (entityPessoaAntigo.pai_idNacionalidade != entityPessoa.pai_idNacionalidade)
                {
                    //Decrementa um na integridade do pais anterior (se existia)
                    if (entityPessoaAntigo.pai_idNacionalidade != Guid.Empty)
                        daoPais.Update_DecrementaIntegridade(entityPessoaAntigo.pai_idNacionalidade);

                    //Incrementa um na integridade do pais atual (se existir)
                    if (entityPessoa.pai_idNacionalidade != Guid.Empty)
                        daoPais.Update_IncrementaIntegridade(entityPessoa.pai_idNacionalidade);
                }

                //Integridade Cidade
                if (entityPessoaAntigo.cid_idNaturalidade != entityPessoa.cid_idNaturalidade)
                {
                    //Decrementa um na integridade da cidade anterior (se existia)
                    if (entityPessoaAntigo.cid_idNaturalidade != Guid.Empty)
                        daoCidade.Update_DecrementaIntegridade(entityPessoaAntigo.cid_idNaturalidade);

                    //Incrementa um na integridade da cidade atual (se existir)
                    if (entityPessoa.cid_idNaturalidade != Guid.Empty)
                        daoCidade.Update_IncrementaIntegridade(entityPessoa.cid_idNaturalidade);
                }

                //Integridade Pessoa Pai
                if (entityPessoaAntigo.pes_idFiliacaoPai != entityPessoa.pes_idFiliacaoPai)
                {
                    //Decrementa um na integridade da pessoa pai anterior (se existia)
                    if (entityPessoaAntigo.pes_idFiliacaoPai != Guid.Empty)
                        daoPessoa.Update_DecrementaIntegridade(entityPessoaAntigo.pes_idFiliacaoPai);

                    //Incrementa um na integridade da pessoa pai atual (se existir)
                    if (entityPessoa.pes_idFiliacaoPai != Guid.Empty)
                        daoPessoa.Update_IncrementaIntegridade(entityPessoa.pes_idFiliacaoPai);
                }

                //Integridade Pessoa Mãe
                if (entityPessoaAntigo.pes_idFiliacaoMae != entityPessoa.pes_idFiliacaoMae)
                {
                    //Decrementa um na integridade da pessoa mae anterior (se existia)
                    if (entityPessoaAntigo.pes_idFiliacaoMae != Guid.Empty)
                        daoPessoa.Update_DecrementaIntegridade(entityPessoaAntigo.pes_idFiliacaoMae);

                    //Incrementa um na integridade da pessoa mae atual (se existir)
                    if (entityPessoa.pes_idFiliacaoMae != Guid.Empty)
                        daoPessoa.Update_IncrementaIntegridade(entityPessoa.pes_idFiliacaoMae);
                }

                //Integridade Tipo de Escolaridade
                if (entityPessoaAntigo.tes_id != entityPessoa.tes_id)
                {
                    //Decrementa um na integridade do tipo de escolaridade anterior (se existia)
                    if (entityPessoaAntigo.tes_id != Guid.Empty)
                        daoTipoEscolaridade.Update_DecrementaIntegridade(entityPessoaAntigo.tes_id);

                    //Incrementa um na integridade do tipo de escolaridade atual (se existir)
                    if (entityPessoa.tes_id != Guid.Empty)
                        daoTipoEscolaridade.Update_IncrementaIntegridade(entityPessoa.tes_id);
                }

                //Integridade Tipo de Deficiência
                if (entityPessoaDeficienciaAntigo.tde_id != entityPessoaDeficiencia.tde_id)
                {
                    //Decrementa um na integridade do tipo de deficiência anterior (se existia)
                    if (entityPessoaDeficienciaAntigo.tde_id != Guid.Empty)
                        daoTipoDeficiencia.Update_DecrementaIntegridade(entityPessoaDeficienciaAntigo.tde_id);

                    //Incrementa um na integridade do tipo de deficiência atual (se existir)
                    if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                        daoTipoDeficiencia.Update_IncrementaIntegridade(entityPessoaDeficiencia.tde_id);
                }

                //Integridade Pais da Certidão Cívil
                if (entityCertidaoCivilAntigo.cid_idCartorio != entityCertidaoCivil.cid_idCartorio)
                {
                    //Decrementa um na integridade do pais anterior da certidão cívil  (se existia)
                    if (entityCertidaoCivilAntigo.cid_idCartorio != Guid.Empty)
                        daoCidade.Update_DecrementaIntegridade(entityCertidaoCivilAntigo.cid_idCartorio);

                    //Incrementa um na integridade do pais atual da certidão cívil  (se existir)
                    if (entityCertidaoCivil.cid_idCartorio != Guid.Empty)
                        daoCidade.Update_IncrementaIntegridade(entityCertidaoCivil.cid_idCartorio);
                }

                //Integridade Unidade Federativa da Certidão Cívil
                if (entityCertidaoCivilAntigo.unf_idCartorio != entityCertidaoCivil.unf_idCartorio)
                {
                    //Decrementa um na integridade da unidade federativa anterior da certidão cívil (se existia)
                    if (entityCertidaoCivilAntigo.unf_idCartorio != Guid.Empty)
                        daoUnidadeFederativa.Update_DecrementaIntegridade(entityCertidaoCivilAntigo.unf_idCartorio);

                    //Incrementa um na integridade da unidade federativa atual da certidão cívil  (se existir)
                    if (entityCertidaoCivil.unf_idCartorio != Guid.Empty)
                        daoUnidadeFederativa.Update_IncrementaIntegridade(entityCertidaoCivil.unf_idCartorio);
                }
            }
        }

        /// <summary>
        /// Incrementa/Decrementa as integridades necessárias para cadastrar a pessoa.
        /// </summary>
        /// <param name="pesDAL"></param>
        /// <param name="entity"></param>
        /// <param name="entityPessoaDeficiencia"></param>
        /// <param name="pai_idAntigo"></param>
        /// <param name="cid_idAntigo"></param>
        /// <param name="pes_idPaiAntigo"></param>
        /// <param name="pes_idMaeAntigo"></param>
        /// <param name="tes_idAntigo"></param>
        /// <param name="tde_idAntigo"></param>
        private static void UpdateIntegridades
        (
            PES_PessoaDAO pesDAL
            , PES_Pessoa entity
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
        )
        {
            if (entity.IsNew)
            {
                //Incrementa um na integridade do pais (se existir)
                END_PaisDAO paiDAL = new END_PaisDAO { _Banco = pesDAL._Banco };
                if (entity.pai_idNacionalidade != Guid.Empty)
                    paiDAL.Update_IncrementaIntegridade(entity.pai_idNacionalidade);

                //Incrementa um na integridade da cidade (se existir)
                END_CidadeDAO cidDAL = new END_CidadeDAO { _Banco = pesDAL._Banco };
                if (entity.cid_idNaturalidade != Guid.Empty)
                    cidDAL.Update_IncrementaIntegridade(entity.cid_idNaturalidade);

                //Incrementa um na integridade da pessoa pai (se existir)
                if (entity.pes_idFiliacaoPai != Guid.Empty)
                    pesDAL.Update_IncrementaIntegridade(entity.pes_idFiliacaoPai);

                //Incrementa um na integridade da pessoa mãe (se existir)
                if (entity.pes_idFiliacaoMae != Guid.Empty)
                    pesDAL.Update_IncrementaIntegridade(entity.pes_idFiliacaoMae);

                //Incrementa um na integridade do tipo de escolaridade (se existir)
                PES_TipoEscolaridadeDAO tesDAL = new PES_TipoEscolaridadeDAO { _Banco = pesDAL._Banco };
                if (entity.tes_id != Guid.Empty)
                    tesDAL.Update_IncrementaIntegridade(entity.tes_id);

                //Incrementa um na integridade do tipo de deficiência (se existir)
                PES_TipoDeficienciaDAO tdeDAL = new PES_TipoDeficienciaDAO { _Banco = pesDAL._Banco };
                if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                    tdeDAL.Update_IncrementaIntegridade(entityPessoaDeficiencia.tde_id);
            }
            else
            {
                //Integridade Pais
                END_PaisDAO paiDAL = new END_PaisDAO { _Banco = pesDAL._Banco };
                if (pai_idAntigo != entity.pai_idNacionalidade)
                {
                    //Decrementa um na integridade do pais anterior (se existia)
                    if (pai_idAntigo != Guid.Empty)
                        paiDAL.Update_DecrementaIntegridade(pai_idAntigo);

                    //Incrementa um na integridade do pais atual (se existir)
                    if (entity.pai_idNacionalidade != Guid.Empty)
                        paiDAL.Update_IncrementaIntegridade(entity.pai_idNacionalidade);
                }

                //Integridade Cidade
                END_CidadeDAO cidDAL = new END_CidadeDAO { _Banco = pesDAL._Banco };
                if (cid_idAntigo != entity.cid_idNaturalidade)
                {
                    //Decrementa um na integridade da cidade anterior (se existia)
                    if (cid_idAntigo != Guid.Empty)
                        cidDAL.Update_DecrementaIntegridade(cid_idAntigo);

                    //Incrementa um na integridade da cidade atual (se existir)
                    if (entity.cid_idNaturalidade != Guid.Empty)
                        cidDAL.Update_IncrementaIntegridade(entity.cid_idNaturalidade);
                }

                //Integridade Pessoa Pai
                if (pes_idPaiAntigo != entity.pes_idFiliacaoPai)
                {
                    //Decrementa um na integridade da pessoa pai anterior (se existia)
                    if (pes_idPaiAntigo != Guid.Empty)
                        pesDAL.Update_DecrementaIntegridade(pes_idPaiAntigo);

                    //Incrementa um na integridade da pessoa pai atual (se existir)
                    if (entity.pes_idFiliacaoPai != Guid.Empty)
                        pesDAL.Update_IncrementaIntegridade(entity.pes_idFiliacaoPai);
                }

                //Integridade Pessoa Mãe
                if (pes_idMaeAntigo != entity.pes_idFiliacaoMae)
                {
                    //Decrementa um na integridade da pessoa mae anterior (se existia)
                    if (pes_idMaeAntigo != Guid.Empty)
                        pesDAL.Update_DecrementaIntegridade(pes_idMaeAntigo);

                    //Incrementa um na integridade da pessoa mae atual (se existir)
                    if (entity.pes_idFiliacaoMae != Guid.Empty)
                        pesDAL.Update_IncrementaIntegridade(entity.pes_idFiliacaoMae);
                }

                //Integridade Tipo de Escolaridade
                PES_TipoEscolaridadeDAO tesDAL = new PES_TipoEscolaridadeDAO { _Banco = pesDAL._Banco };
                if (tes_idAntigo != entity.tes_id)
                {
                    //Decrementa um na integridade do tipo de escolaridade anterior (se existia)
                    if (tes_idAntigo != Guid.Empty)
                        tesDAL.Update_DecrementaIntegridade(tes_idAntigo);

                    //Incrementa um na integridade do tipo de escolaridade atual (se existir)
                    if (entity.tes_id != Guid.Empty)
                        tesDAL.Update_IncrementaIntegridade(entity.tes_id);
                }

                //Integridade Tipo de Deficiência
                PES_TipoDeficienciaDAO tdeDAL = new PES_TipoDeficienciaDAO { _Banco = pesDAL._Banco };
                if (tde_idAntigo != entityPessoaDeficiencia.tde_id)
                {
                    //Decrementa um na integridade do tipo de deficiência anterior (se existia)
                    if (tde_idAntigo != Guid.Empty)
                        tdeDAL.Update_DecrementaIntegridade(tde_idAntigo);

                    //Incrementa um na integridade do tipo de deficiência atual (se existir)
                    if (entityPessoaDeficiencia.tde_id != Guid.Empty)
                        tdeDAL.Update_IncrementaIntegridade(entityPessoaDeficiencia.tde_id);
                }
            }
        }

        /// <summary>
        /// Deleta logicamente uma Pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_Pessoa</param>
        /// <param name="banco">Conexão aberta com o banco de dados ou null para uma nova conexão</param>
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            PES_Pessoa entity
            , TalkDBTransaction banco
        )
        {
            PES_PessoaDAO dal = new PES_PessoaDAO();

            if (banco == null)
                dal._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dal._Banco = banco;

            try
            {
                //Verifica se a Pessoa pode ser deletada
                if (dal.Select_Integridade(entity.pes_id) > 0)
                    throw new Exception("Não é possível excluir a pessoa pois possui outros registros ligados a ela.");

                //Decrementa um na integridade de cada tipo de deficiencia da pessoa
                PES_PessoaDeficienciaDAO pdeDal = new PES_PessoaDeficienciaDAO { _Banco = dal._Banco };
                PES_TipoDeficienciaDAO tdeDal = new PES_TipoDeficienciaDAO { _Banco = dal._Banco };

                DataTable dtDeficiencia = pdeDal.SelectBy_pes_id(entity.pes_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dtDeficiencia.Rows.Count; i++)
                    tdeDal.Update_DecrementaIntegridade(new Guid(dtDeficiencia.Rows[i]["tde_id"].ToString()));

                //Decrementa um na integridade de cada endereço da pessoa
                PES_PessoaEnderecoDAO pseDal = new PES_PessoaEnderecoDAO { _Banco = dal._Banco };
                END_EnderecoDAO endDal = new END_EnderecoDAO { _Banco = dal._Banco };

                DataTable dtEndereco = pseDal.SelectBy_pes_id(entity.pes_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dtEndereco.Rows.Count; i++)
                    endDal.Update_DecrementaIntegridade(new Guid(dtEndereco.Rows[i]["end_id"].ToString()));

                //Decrementa um na integridade de cada tipo de contato da pessoa
                PES_PessoaContatoDAO pscDal = new PES_PessoaContatoDAO { _Banco = dal._Banco };
                SYS_TipoMeioContatoDAO tmcDal = new SYS_TipoMeioContatoDAO { _Banco = dal._Banco };

                DataTable dtContato = pscDal.SelectBy_pes_id(entity.pes_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dtContato.Rows.Count; i++)
                    tmcDal.Update_DecrementaIntegridade(new Guid(dtContato.Rows[i]["tmc_id"].ToString()));

                //Decrementa um na integridade de cada tipo de documento da pessoa
                //Decrementa um na integridade de cada estado do documento da pessoa (se existir)
                PES_PessoaDocumentoDAO pdoDal = new PES_PessoaDocumentoDAO { _Banco = dal._Banco };
                SYS_TipoDocumentacaoDAO tdoDal = new SYS_TipoDocumentacaoDAO { _Banco = dal._Banco };
                END_UnidadeFederativaDAO unfDal = new END_UnidadeFederativaDAO { _Banco = dal._Banco };

                DataTable dtDocumento = pdoDal.SelectBy_pes_id(entity.pes_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dtDocumento.Rows.Count; i++)
                {
                    tdoDal.Update_DecrementaIntegridade(new Guid(dtDocumento.Rows[i]["tdo_id"].ToString()));

                    if (new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString()) != Guid.Empty)
                        unfDal.Update_DecrementaIntegridade(new Guid(dtDocumento.Rows[i]["unf_idEmissao"].ToString()));
                }

                //Decrementa um na integridade de cada estado da certidão civil da pessoa (se existir)
                PES_CertidaoCivilDAO ctcDal = new PES_CertidaoCivilDAO { _Banco = dal._Banco };

                DataTable dtCertidao = ctcDal.SelectBy_pes_id(entity.pes_id, false, 1, 1, out totalRecords);
                for (int i = 0; i < dtCertidao.Rows.Count; i++)
                {
                    if (new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString()) != Guid.Empty)
                        unfDal.Update_DecrementaIntegridade(new Guid(dtCertidao.Rows[i]["unf_idCartorio"].ToString()));
                }

                //Decrementa um na integridade do pais (se existir)
                END_PaisDAO paiDal = new END_PaisDAO { _Banco = dal._Banco };
                if (entity.pai_idNacionalidade != Guid.Empty)
                    paiDal.Update_DecrementaIntegridade(entity.pai_idNacionalidade);

                //Decrementa um na integridade da cidade (se existir)
                END_CidadeDAO cidDal = new END_CidadeDAO { _Banco = dal._Banco };
                if (entity.cid_idNaturalidade != Guid.Empty)
                    cidDal.Update_DecrementaIntegridade(entity.cid_idNaturalidade);

                //Decrementa um na integridade da pessoa pai (se existir)
                if (entity.pes_idFiliacaoPai != Guid.Empty)
                    dal.Update_DecrementaIntegridade(entity.pes_idFiliacaoPai);

                //Decrementa um na integridade da pessoa mae (se existir)
                if (entity.pes_idFiliacaoMae != Guid.Empty)
                    dal.Update_DecrementaIntegridade(entity.pes_idFiliacaoMae);

                //Decrementa um na integridade do tipo de escolaridade (se existir)
                PES_TipoEscolaridadeDAO tesDal = new PES_TipoEscolaridadeDAO { _Banco = dal._Banco };
                if (entity.tes_id != Guid.Empty)
                    tesDal.Update_DecrementaIntegridade(entity.tes_id);

                //Deleta logicamente a Pessoa
                dal.Delete(entity);

                return true;
            }
            catch (Exception err)
            {
                if (banco == null)
                    dal._Banco.Close(err);
                throw new Exception("Erro ao apagar dados!", err);
            }
            finally
            {
                if (banco == null)
                    dal._Banco.Close();
            }
        }

        /// <summary>
        /// Associa diferentes pessoas em uma única pessoa
        /// </summary>
        /// <param name="entity>Entidade PES_Pessoa</param>
        /// <param name="entityPessoaDeficiencia">Entidade PES_PessoaDeficiencia</param>
        /// <param name="dtEndereco">Datatable Enderecos</param>
        /// <param name="dtContato">Datatable Contatos</param>
        /// <param name="dtDocumento">Datatable Documentos</param>
        /// <param name="dtCertidao">Datatable Certidoes</param>
        /// <param name="pai_idAntigo">Campo pai_idNacionalidade antigo</param>
        /// <param name="cid_idAntigo">Campo cid_idNaturalidade antigo</param>
        /// <param name="pes_idPaiAntigo">Campo pes_idFiliacaoPai antigo</param>
        /// <param name="pes_idMaeAntigo">Campo pes_idFiliacaoMae antigo</param>
        /// <param name="tes_idAntigo">Campo tes_id antigo</param>
        /// <param name="tde_idAntigo">Campo tes_id antigo</param>
        /// <param name="xDoc">XML com IDs dos endereços a serem associados</param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AssociarPessoas
        (
            PES_Pessoa entity
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , DataTable dtEndereco
            , DataTable dtContato
            , DataTable dtDocumento
            , DataTable dtCertidao
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
            , XmlDocument xDoc
        )
        {
            PES_PessoaDAO dao = new PES_PessoaDAO();
            dao._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                Save(entity, entityPessoaDeficiencia, dtEndereco, dtContato, dtDocumento, dtCertidao, pai_idAntigo, cid_idAntigo, pes_idPaiAntigo, pes_idMaeAntigo, tes_idAntigo, tde_idAntigo, dao._Banco);

                if (dao.AssociarPessoas(entity.pes_id, xDoc, "pes_id%", "PES_Pessoa"))
                    return true;
                else
                    throw new Exception();
            }
            catch (Exception err)
            {
                dao._Banco.Close(err);
                throw;
            }
            finally
            {
                dao._Banco.Close();
            }
        }

        /// <summary>
        /// Associa diferentes pessoas em uma única pessoa
        /// </summary>
        /// <param name="entity">Entidade PES_Pessoa</param>
        /// <param name="entityPessoaDeficiencia">Entidade PES_PessoaDeficiencia</param>
        /// <param name="dtEndereco">Datatable Enderecos</param>
        /// <param name="dtContato">Datatable Contatos</param>
        /// <param name="dtDocumento">Datatable Documentos</param>
        /// <param name="dtCertidao">Datatable Certidoes</param>
        /// <param name="pai_idAntigo">Campo pai_idNacionalidade antigo</param>
        /// <param name="cid_idAntigo">Campo cid_idNaturalidade antigo</param>
        /// <param name="pes_idPaiAntigo">Campo pes_idFiliacaoPai antigo</param>
        /// <param name="pes_idMaeAntigo">Campo pes_idFiliacaoMae antigo</param>
        /// <param name="tes_idAntigo">Campo tes_id antigo</param>
        /// <param name="tde_idAntigo">Campo tes_id antigo</param>
        /// <param name="xDoc">XML com IDs dos endereços a serem associados</param>
        /// <param name="arquivosPermitidos">Tipos de arquivos permitidos para a foto</param>
        /// <param name="tamanhoMaximoKB">Tamanho máximo de arquivos em KB</param>
        /// <param name="entFoto">Entidade da foto da pessoa</param>
        /// <param name="ExcluirImagemAtual">Flag que indica se será excluída a imagem atual da pessoa</param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool AssociarPessoas
        (
            PES_Pessoa entity
            , PES_PessoaDeficiencia entityPessoaDeficiencia
            , DataTable dtEndereco
            , DataTable dtContato
            , DataTable dtDocumento
            , DataTable dtCertidao
            , Guid pai_idAntigo
            , Guid cid_idAntigo
            , Guid pes_idPaiAntigo
            , Guid pes_idMaeAntigo
            , Guid tes_idAntigo
            , Guid tde_idAntigo
            , XmlDocument xDoc
            , string[] arquivosPermitidos
            , int tamanhoMaximoKB
            , CFG_Arquivo entFoto
            , bool ExcluirImagemAtual
        )
        {
            PES_PessoaDAO dao = new PES_PessoaDAO();
            dao._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                Salvar_ComFoto
                    (
                    dao._Banco
                    , entFoto
                    , tamanhoMaximoKB
                    , arquivosPermitidos
                    , entity
                    , ExcluirImagemAtual
                    , entityPessoaDeficiencia
                    , dtEndereco
                    , dtContato
                    , dtDocumento
                    , dtCertidao
                    , pai_idAntigo
                    , cid_idAntigo
                    , pes_idPaiAntigo
                    , pes_idMaeAntigo
                    , tes_idAntigo
                    , tde_idAntigo
                    );

                if (dao.AssociarPessoas(entity.pes_id, xDoc, "pes_id%", "PES_Pessoa"))
                    return true;
                else
                    throw new Exception();
            }
            catch (Exception err)
            {
                dao._Banco.Close(err);
                throw;
            }
            finally
            {
                dao._Banco.Close();
            }
        }

        /// <summary>
        /// Retorna entidade Pessoa filtrando pelos campos pes_nome, pes_dataNascimento e psd_numero
        /// </summary>
        /// <param name="pes_nome">Nome da pessoa</param>
        /// <param name="pes_dataNascimento">Data de nascimento da pessoa</param>
        /// <param name="tdo_id">Id do tipo de documento</param>
        /// <param name="psd_numero">Número do documento da pessoa</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static PES_Pessoa SelecionaPorNomeNascimentoDocumento(string pes_nome, DateTime pes_dataNascimento, Guid? tdo_id = null, string psd_numero = null)
        {
            PES_PessoaDAO dao = new PES_PessoaDAO();
            return dao.SelectBy_Nome_Nascimento_Documento(pes_nome, pes_dataNascimento, tdo_id, psd_numero);
        }
    }
}