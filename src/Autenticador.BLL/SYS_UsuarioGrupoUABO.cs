using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using System.Collections.Generic;

namespace Autenticador.BLL
{
    public class SYS_UsuarioGrupoUABO : BusinessBase<SYS_UsuarioGrupoUADAO, SYS_UsuarioGrupoUA>    
    {
        /// <summary>
        /// Retorna uma lista de unidades administrativas/entidades não paginadas 
        /// ordenado pelo nome da unidade administrativa ou entidade filtrados por
        /// grupos e usuário.
        /// </summary>
        /// <param name="usu_id">ID do usuário</param>
        /// <param name="gru_id">ID do grupos</param>
        /// <returns>Lista de unidades/entidade do grupo do usuário</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
            Guid  usu_id
            , Guid gru_id
        )
        {
            SYS_UsuarioGrupoUADAO dao = new SYS_UsuarioGrupoUADAO();
            return dao.SelectBy_UsuarioGrupo(usu_id, gru_id);
        }

        /// <summary>
        /// Deleta a ligação da ua com o grupo de usuário
        /// </summary>
        /// <param name="entity">Entidade UA do grupo do usuário</param>
        /// <param name="banco">Conexão aberta com o banco de dados/Null para uma nova conexão</param>        
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void DeletarPorUsuarioGrupoUA
        (
            SYS_UsuarioGrupoUA entity
             , TalkDBTransaction banco
        )
        {
            SYS_UsuarioGrupoUADAO dao = new SYS_UsuarioGrupoUADAO();

            if (banco == null)
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dao._Banco = banco;

            try
            {
                dao.DeletarPorUsuarioGrupoUA(entity.usu_id, entity.gru_id, entity.ent_id, entity.uad_id);
            }
            catch (Exception err)
            {
                if (banco == null)
                    dao._Banco.Close(err);

                throw;
            }
            finally
            {
                if (banco == null)
                    dao._Banco.Close();
            }
        }

        /// <summary>
        /// Seleciona as entidades e unidades administrativas por login do usuário.
        /// </summary>
        /// <param name="usu_login">Login do usuário.</param>
        /// <returns></returns>
        public static List<SYS_UsuarioGrupoUA> SelecionaPorLogin(string usu_login)
        {
            return new SYS_UsuarioGrupoUADAO().SelecionaPorLogin(usu_login);
        }

    }
}