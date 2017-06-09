using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Autenticador.BLL.V2
{
    public class UnidadeAdministrativaBO
    {
        public static List<SYS_UnidadeAdministrativa> SelecionarPorIdGrupo(Guid entidadeId, Guid grupoId)
        {
            SYS_UnidadeAdministrativaDAO unidadeDao = new SYS_UnidadeAdministrativaDAO();

            List<SYS_UnidadeAdministrativa> lista = unidadeDao.SelecionarPorIdGrupo(entidadeId, grupoId);

            return lista;
        }

        public IList<SYS_UnidadeAdministrativa> SelecionarUnidadesAdministrativasFilhas(Guid entidadeId, Guid unidadeId)
        {
            var dao = new SYS_UnidadeAdministrativaDAO();
            return dao.SelecionarUnidadesAdministrativasFilhas(entidadeId, unidadeId);
        }

        /// <summary>
        /// Selecionar as unidades administrativas filhas (subunidades)
        /// </summary>
        /// <param name="entidadeId">Id da entidade</param>
        /// <param name="unidadeId">Id da unidade</param>
        /// <returns></returns>
        public DataTable SelecionarUnidadesAdministrativasFilhasV2(Guid entidadeId, Guid unidadeId)
        {
            SYS_UnidadeAdministrativaDAO dao = new SYS_UnidadeAdministrativaDAO();
            DataTable dt = dao.SelecionarUnidadesAdministrativasFilhasV2(entidadeId, unidadeId);

            return dt;
        }
    }
}
