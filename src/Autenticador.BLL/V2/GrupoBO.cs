using System.Data;
using Autenticador.DAL;
using Autenticador.DAL.Interfaces;
using Autenticador.Entities;
using System;
using System.Collections.Generic;


namespace Autenticador.BLL.V2
{
    public class GrupoBO
    {
        private INewDALUsuario grupoRepositorioUsuario;

        public GrupoBO(INewDALUsuario dal)
        {
            grupoRepositorioUsuario = dal;
        }

        public List<SYS_Usuario> SelecionaUsuariosDaUnidadeAdministrativa(Guid entidadeId, Guid idUnidade)
        {
            List<SYS_Usuario> list = grupoRepositorioUsuario.SelecionarUsuariosDaUnidadeAdministrativa(entidadeId, idUnidade);

            return list;
        }

        public static List<SYS_Grupo> SelecionarGrupoPorId(Guid grupoId)
        {
            SYS_GrupoDAO dao = new SYS_GrupoDAO();
            try
            {
                return dao.SelecionarGrupoPorId(grupoId);
            }
            catch
            {
                throw;
            }
        }

        public static List<SYS_Grupo> SelecionarGruposPorIdSistema(int sistemaId)
        {
            SYS_GrupoDAO dao = new SYS_GrupoDAO();
            try
            {
                return dao.SelecionarGruposPorIdSistema(sistemaId);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable SelecionarPermissoesGrupoPorIdGrupo(Guid grupoId)
        {
            SYS_GrupoPermissaoDAO dao = new SYS_GrupoPermissaoDAO();
            try
            {
                using (DataTable dt = dao.SelecionarPermissoesGrupoPorIdGrupo(grupoId)) { }
                    return dao.SelecionarPermissoesGrupoPorIdGrupo(grupoId);
            }
            catch
            {
                throw;
            }
        }

        //public static DataTable SelecionarPermissoesGrupoPorIdSistema(int sistemaId)
        //{
        //    SYS_GrupoPermissaoDAO dao = new SYS_GrupoPermissaoDAO();
        //    try
        //    {
        //        DataTable dt = dao.SelecionarPermissoesGrupoPorIdSistema(sistemaId);
        //        return dt;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}