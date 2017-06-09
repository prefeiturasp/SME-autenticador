using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Autenticador.BLL.V2
{
    public class EntidadeBO
    {
        public static List<SYS_Entidade> SelecionarPorIdGrupo(Guid entidadeId, Guid grupoId)
        {
            SYS_EntidadeDAO dao = new SYS_EntidadeDAO();

            List<SYS_Entidade> lista = dao.SelecionarPorIdGrupo(entidadeId, grupoId);

            return lista;
        }

        public static List<SYS_Entidade> SelecionarTodasLigadasPorSistemas()
        {
            SYS_EntidadeDAO dao = new SYS_EntidadeDAO();

            List<SYS_Entidade> lista = dao.SelecionarTodasLigadasPorSistemas();

            return lista;
        }

        /// <summary>
        /// Selecionar as entidades filhas (subentidades)
        /// </summary>
        /// <param name="entidadeId">Id da entidade</param>
        /// <returns>Lista de entidades</returns>
        public DataTable SelecionarEntidadesFilhas(Guid entidadeId)
        {
            SYS_EntidadeDAO dao = new SYS_EntidadeDAO();
            DataTable dt = dao.SelecionarEntidadesFilhas(entidadeId);

            return dt;
        }
    }
}