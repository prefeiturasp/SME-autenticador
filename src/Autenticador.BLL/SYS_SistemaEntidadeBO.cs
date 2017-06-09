using System;
using System.Collections.Generic;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;

namespace Autenticador.BLL
{
    public class SYS_SistemaEntidadeBO : BusinessBase<SYS_SistemaEntidadeDAO, SYS_SistemaEntidade>
    {
        /// <summary>
        /// Inclui um novo contato para a entidade
        /// </summary>
        /// <param name="entity">Entidade SYS_EntidadeContato</param>        
        /// <param name="banco">Transação</param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            SYS_SistemaEntidade entity
            ,CoreLibrary.Data.Common.TalkDBTransaction banco
        )
        {
            SYS_SistemaEntidadeDAO dao = new SYS_SistemaEntidadeDAO();
            dao._Banco = banco;

            if (entity.Validate())
            {
                dao.Salvar(entity);
            }
            else
            {
                throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
            }

            return true;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SelectEntidade
        (
            int sis_id
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_SistemaEntidadeDAO entdal = new SYS_SistemaEntidadeDAO();
            return entdal.SelectBy_UrlChave(sis_id, paginado, currentPage/pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Verifica se já existe a entidade cadastrada para o sistema
        /// e excluido logicamente
        /// filtrados por sis_id, ent_id
        /// </summary>
        /// <param name="sis_id">Campo sis_id da tabela SYS_SitemaEntidade do bd</param>        
        /// <param name="ent_id">Campo ent_id da tabela SYS_SitemaEntidade do bd</param>        
        /// <returns>true ou false</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaEntidadeExistente
        (
            int sis_id
            , Guid ent_id
        )
        {
            SYS_SistemaEntidadeDAO dal = new SYS_SistemaEntidadeDAO();
            return dal.SelectBy_sis_id_ent_id_excluido(sis_id, ent_id);
        }

        /// <summary>
        /// Verifica existencia do código ent_id no DataTable.
        /// </summary>
        /// <param name="ent_id">ID de ent_id</param>
        /// <param name="dtSistemaEntidade">DataTable de Sistema Entidade</param>
        /// <returns>True - caso exista / False - caso não exista</returns>
        public static bool VerificaEntidadeExistente(Guid ent_id, DataTable dtSistemaEntidade)
        {
            try
            {
                for (int i = 0; i < dtSistemaEntidade.Rows.Count; i++)
                {
                    if (dtSistemaEntidade.Rows[i].RowState != DataRowState.Deleted)
                    {
                        if (dtSistemaEntidade.Rows[i]["ent_id"].ToString().Equals(ent_id.ToString()))
                            return true;
                    }
                }
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um List de entidades de SistemaEntidade, baseado em datatable de SistemaEntidade.
        /// Para cada linha do datatable cria-se uma entidade.
        /// </summary>
        /// <param name="dtSistemaEntidade">DataTable de SistemaEntidade</param>
        /// <returns></returns>
        static public List<SYS_SistemaEntidade> CriaList_Entities_SistemaEntidade(DataTable dtSistemaEntidade)
        {
            try
            {
                //cria List
                List<SYS_SistemaEntidade> lt = new List<SYS_SistemaEntidade>();
                for (int i = 0; i < dtSistemaEntidade.Rows.Count; i++)
                {
                    //cria entidade
                    SYS_SistemaEntidade entitySistemaEntidade = new SYS_SistemaEntidade();

                    //verifica se registro do DataTable é um novo registro
                    if (dtSistemaEntidade.Rows[i].RowState == DataRowState.Added)
                    {
                        entitySistemaEntidade.sis_id = Convert.ToInt32(dtSistemaEntidade.Rows[i]["sis_id"].ToString());
                        entitySistemaEntidade.ent_id = new Guid(dtSistemaEntidade.Rows[i]["ent_id"].ToString());
                        entitySistemaEntidade.sen_chaveK1 = dtSistemaEntidade.Rows[i]["sen_chaveK1"].ToString();
                        entitySistemaEntidade.sen_urlAcesso = dtSistemaEntidade.Rows[i]["sen_urlAcesso"].ToString();
                        entitySistemaEntidade.sen_logoCliente = dtSistemaEntidade.Rows[i]["sen_logoCliente"].ToString();
                        entitySistemaEntidade.sen_urlCliente = dtSistemaEntidade.Rows[i]["sen_urlCliente"].ToString();
                        entitySistemaEntidade.sen_situacao = Convert.ToByte(1);

                        if (VerificaEntidadeExistente(entitySistemaEntidade.sis_id, entitySistemaEntidade.ent_id))
                            entitySistemaEntidade.IsNew = false;
                        else
                            entitySistemaEntidade.IsNew = true;
                        
                        //adiciona entidade na List
                        lt.Add(entitySistemaEntidade);
                    }
                    //verifica se registro do Datable foi deletado.
                    else if (dtSistemaEntidade.Rows[i].RowState == DataRowState.Deleted)
                    {
                        entitySistemaEntidade.sis_id = Convert.ToInt32(dtSistemaEntidade.Rows[i]["sis_id", DataRowVersion.Original].ToString());
                        entitySistemaEntidade.ent_id = new Guid(dtSistemaEntidade.Rows[i]["ent_id", DataRowVersion.Original].ToString());
                        entitySistemaEntidade.sen_chaveK1 = dtSistemaEntidade.Rows[i]["sen_chaveK1", DataRowVersion.Original].ToString();
                        entitySistemaEntidade.sen_urlAcesso = dtSistemaEntidade.Rows[i]["sen_urlAcesso", DataRowVersion.Original].ToString();
                        entitySistemaEntidade.sen_logoCliente = "";
                        entitySistemaEntidade.sen_urlCliente = "";
                        entitySistemaEntidade.sen_situacao = Convert.ToByte(3);
                        entitySistemaEntidade.IsNew = false;
                        //adiciona entidade na List
                        lt.Add(entitySistemaEntidade);
                    }
                    else
                    {
                        entitySistemaEntidade.sis_id = Convert.ToInt32(dtSistemaEntidade.Rows[i]["sis_id"].ToString());
                        entitySistemaEntidade.ent_id = new Guid(dtSistemaEntidade.Rows[i]["ent_id"].ToString());
                        entitySistemaEntidade.sen_chaveK1 = dtSistemaEntidade.Rows[i]["sen_chaveK1"].ToString();
                        entitySistemaEntidade.sen_urlAcesso = dtSistemaEntidade.Rows[i]["sen_urlAcesso"].ToString();
                        entitySistemaEntidade.sen_logoCliente = dtSistemaEntidade.Rows[i]["sen_logoCliente"].ToString();
                        entitySistemaEntidade.sen_urlCliente = dtSistemaEntidade.Rows[i]["sen_urlCliente"].ToString();
                        entitySistemaEntidade.sen_situacao = Convert.ToByte(1);
                        entitySistemaEntidade.IsNew = false;

                        //adiciona entidade na List
                        lt.Add(entitySistemaEntidade);
                    }
                }
                //retorna List
                return lt;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
