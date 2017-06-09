using System;
using System.Collections.Generic;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.SAML20;

namespace Autenticador.BLL
{
    public class SYS_SistemaBO : BusinessBase<SYS_SistemaDAO, SYS_Sistema>
    {
        public enum TypePath
        {
            login
            , logout
        }
        /// <summary>
        ///  Valida se URL de redirecionamento
        /// </summary>
        /// <param name="urlRedirecionamento">URL a ser validada </param>
        /// <returns>bool - TRUE se URL for válida caso contrario FALSE</returns>
        public bool ValidarURLRedirect(string urlRedirecionamento)
        {
            SYS_SistemaDAO dal = new SYS_SistemaDAO();
            bool valid = false;

            Uri uri = new Uri(urlRedirecionamento);
            string redirecionamentoHost = uri.Host;

            IList<SYS_Sistema> entities = dal.Select();

            foreach (SYS_Sistema item in entities)
            {
                if (!string.IsNullOrEmpty(item.sis_caminho))
                {
                    Uri uriEntity = new Uri(item.sis_caminho);
                    if (uriEntity.Host.Equals(redirecionamentoHost))
                    {
                        valid = true;
                        break;
                    }
                }
            }

            return valid;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static IList<SYS_Sistema> GetSelectBy_usu_id(Guid usu_id)
        {
            SYS_SistemaDAO dal = new SYS_SistemaDAO();

            return dal.SelectBy_usu_id(usu_id);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectBy_ModuloVinculado
        (
            bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            SYS_SistemaDAO dal = new SYS_SistemaDAO();

            return dal.SelectBy_ModuloVinculado(paginado, currentPage / pageSize, pageSize, out totalRecords);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
              int sis_id
            , string sis_nome
            , byte sis_situacao
            , bool paginado
            , int currentPage
            , int pageSize
        )
        {
            if (pageSize == 0)
                pageSize = 1;

            totalRecords = 0;
            SYS_SistemaDAO dal = new SYS_SistemaDAO();
            return dal.SelectBy_Sistema_Situacao(sis_id, sis_nome, sis_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
        }

        /// <summary>
        /// Carrega os dados do sistema através do caminho de login ou logout
        /// </summary>
        /// <param name="entity">Entidade Sitema contendo o sis_caminho ou sis_caminhoLogout</param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool GetSelectBy_sis_caminho(SYS_Sistema entity, TypePath type)
        {
            SYS_SistemaDAO dao = new SYS_SistemaDAO();
            if (type == TypePath.login)
                return dao.SelectBy_sis_caminho(entity);
            else if (type == TypePath.logout)
                return dao.SelectBy_sis_caminhoLogout(entity);
            else
                return false;
        }

        /// <summary>
        /// Retorno booleano, após verificação caso não exista registro
        /// executa metodo Salvar para alteracao de registros
        /// <returns>True/False</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static bool SalvarUrlInte(SYS_Sistema entity, List<SYS_SistemaEntidade> List_Entities_SistemaEntidade)
        {
            SYS_SistemaDAO sistemadal = new SYS_SistemaDAO();
            sistemadal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                sistemadal.Salvar(entity);

                SYS_SistemaEntidadeDAO dao_SistemaEntidade = new SYS_SistemaEntidadeDAO();
                dao_SistemaEntidade._Banco = sistemadal._Banco;
                foreach (SYS_SistemaEntidade SistemaEntidade in List_Entities_SistemaEntidade)
                {
                    if (SistemaEntidade.sen_situacao == 3)
                        dao_SistemaEntidade.Delete(SistemaEntidade);
                    else
                    {
                        dao_SistemaEntidade.Salvar(SistemaEntidade);
                    }
                }

                return true;
            }

            catch (Exception err)
            {
                sistemadal._Banco.Close(err);
                throw;
            }
            finally
            {
                sistemadal._Banco.Close();
            }
        }
    }
}
