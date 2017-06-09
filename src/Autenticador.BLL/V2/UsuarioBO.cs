using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Autenticador.BLL.V2
{
    public class UsuarioBO
    {
        /// <summary>
        /// Seleciona os dados dos usuários pertencentes ao grupo
        /// </summary>
        /// <param name="grupoId">Id do grupo do usuário</param>
        /// <returns>DataTable os usuários</returns>
        public static List<SYS_Usuario> SelecionarPorIdGrupo(Guid grupoId)
        {
            try
            {
                SYS_UsuarioDAO usuarioDAO = new SYS_UsuarioDAO();
                DataTable dt = usuarioDAO.SelecionarPorIdGrupo(grupoId);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                List<SYS_Usuario> usuarios = (
                    from r in dt.AsEnumerable()
                    select (SYS_Usuario)UtilBO.DataRowToEntity(r, new SYS_Usuario())
                    ).ToList();

                return usuarios;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona os dados do usuário
        /// </summary>
        /// <param name="usuarioId">Id do usuário</param>
        /// <returns>DataTable os usuários</returns>
        public static List<SYS_Usuario> SelecionarPorIdUsuario(Guid usuarioId)
        {
            try
            {
                SYS_UsuarioDAO usuarioDAO = new SYS_UsuarioDAO();
                DataTable dt = usuarioDAO.SelecionarPorIdUsuario(usuarioId);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                List<SYS_Usuario> usuarios = (
                    from r in dt.AsEnumerable()
                    select (SYS_Usuario)UtilBO.DataRowToEntity(r, new SYS_Usuario())
                    ).ToList();

                return usuarios;
            }
            catch
            {
                throw;
            }
        }

        public static SYS_Usuario GetEntity(Guid entidadeId, Guid usuarioId)
        {
            SYS_UsuarioDAO dal = new SYS_UsuarioDAO();
            var entidade = new SYS_Usuario { ent_id = entidadeId, usu_id = usuarioId };

            if (dal.Carregar(entidade))
            {
                return entidade;
            }
            else
            {
                return null;
            }
        }

        public static SYS_Usuario GetEntity(Guid entidadeId, string Login)
        {
            SYS_UsuarioDAO usuarioDao = new SYS_UsuarioDAO();
            var usuario = new SYS_Usuario { ent_id = entidadeId, usu_login = Login };

            if (usuarioDao.CarregarBy_ent_id_usu_login(usuario))
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}