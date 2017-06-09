using System;
using System.Data;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.ComponentModel;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.BLL
{
    public class SYS_UsuarioGrupoBO : BusinessBase<SYS_UsuarioGrupoDAO, SYS_UsuarioGrupo>    
    {
        /// <summary>
        /// Inclui ou Altera o Grupo do Usuário
        /// </summary>
        /// <param name="entity">Entidade SYS_UsuarioGrupo</param>                    
        /// <param name="banco">Conexão aberta com o banco de dados/Null para uma nova conexão</param>
        /// <returns>True = incluído/alterado | False = não incluído/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save
        (
            SYS_UsuarioGrupo entity
            , TalkDBTransaction banco
        )
        {
            SYS_UsuarioGrupoDAO dao = new SYS_UsuarioGrupoDAO();

            if (banco == null)
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dao._Banco = banco;

            try
            {
                //Salva dados na tabela SYS_UsuarioGrupo
                if (entity.Validate())
                {
                    SYS_UsuarioGrupo aux = new SYS_UsuarioGrupo {usu_id = entity.usu_id, gru_id = entity.gru_id};
                    GetEntity(aux, dao._Banco);

                    entity.IsNew = aux.IsNew;
                    dao.Salvar(entity);
                }
                else
                {
                    throw new ValidationException(entity.PropertiesErrorList[0].Message);
                }
              
                return true;
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
        /// Deleta logicamente um grupo do usuário
        /// </summary>
        /// <param name="entity">Entidade SYS_UsuarioGrupo</param>                
        /// <param name="banco">Conexão aberta com o banco de dados/Null para uma nova conexão</param>
        /// <returns>True = deletado/alterado | False = não deletado/alterado</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            SYS_UsuarioGrupo entity
            , TalkDBTransaction banco
        )
        {
            SYS_UsuarioGrupoDAO dao = new SYS_UsuarioGrupoDAO();

            if (banco == null)
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dao._Banco = banco;

            try
            {
                //Deleta logicamente o grupo do usuário
                dao.Delete(entity);

                return true;
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
        /// Deleta a ligação dos grupos com o usuário
        /// </summary>
        /// <param name="usu_id">ID do usuário</param>
        /// <param name="banco">Conexão aberta com o banco de dados/Null para uma nova conexão</param>        
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void DeletarPorUsuario
        (
            Guid usu_id
             , TalkDBTransaction banco
        )
        {
            SYS_UsuarioGrupoDAO dao = new SYS_UsuarioGrupoDAO();

            if (banco == null)
                dao._Banco.Open(IsolationLevel.ReadCommitted);
            else
                dao._Banco = banco;

            try
            {
                dao.DeletarPorUsuario(usu_id);
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
    }
}