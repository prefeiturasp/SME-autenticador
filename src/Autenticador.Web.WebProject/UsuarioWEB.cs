using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autenticador.BLL;
using Autenticador.Entities;

namespace Autenticador.Web.WebProject
{
    public class UsuarioWEB : CoreLibrary.Web.WebProject.UsuarioWEB
    {
        #region ATRIBUTOS

        //Grupo logado
        private SYS_Grupo grupo;
        //Usuário logado
        private SYS_Usuario usuario;
        //UA ou entidades do usuário logado
        private IList<SYS_UsuarioGrupoUA> uasGrupo;
        //Permissao do módulo do usuário
        private SYS_GrupoPermissao grupoPermissao;

        #endregion

        #region PROPRIEDADES

        /// <summary>
        /// Retorna os dados do usuário logado
        /// </summary>
        public SYS_Usuario Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }
        /// <summary>
        /// Retorna o grupo na qual o usuário pertense
        /// naquele sistema.
        /// </summary>
        public SYS_Grupo Grupo
        {
            get
            {
                return grupo;
            }
            set
            {
                if (usuario != null)
                {
                    grupo = value;
                    uasGrupo = SYS_UsuarioBO.GetSelectByUsuarioGrupoUA(usuario.usu_id, grupo.gru_id);
                }
                else
                    throw new ArgumentException("objeto __SessionWEB.__UsuarioWEB.Usuario nulo");
            }
        }
        /// <summary>
        /// Retorna as unidades administrativa e entidades de um grupo do usuário.
        /// </summary>
        public IList<SYS_UsuarioGrupoUA> GrupoUA
        {
            get { return this.uasGrupo; }
        }
        /// <summary>
        /// Recebe a permissão do módulo do usuário logado.
        /// </summary>
        public SYS_GrupoPermissao GrupoPermissao
        {
            get 
            { 
                return this.grupoPermissao; 
            }
            set 
            {
                if (grupo != null)
                    this.grupoPermissao = value;
                else
                    throw new ArgumentException("objeto __SessionWEB.__UsuarioWEB.Grupo nulo");
            }
        }

        #endregion

    }
}
