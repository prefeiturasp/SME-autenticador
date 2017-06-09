/*
	Classe gerada automaticamente pelo Code Creator
*/
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System.Data;
using System.Collections.Generic;
using System;

namespace Autenticador.BLL
{

    /// <summary>
    /// Tipo da Integração
    /// </summary>
	public enum SYS_IntegracaoExternaTipoEnum : byte
    {
        IntegracaoExterna = 1
    }

    /// <summary>
    /// Description: SYS_IntegracaoExternaTipo Business Object. 
    /// </summary>
    public class SYS_IntegracaoExternaTipoBO : BusinessBase<SYS_IntegracaoExternaTipoDAO, SYS_IntegracaoExternaTipo>
    {

        public DataTable getAll()
        {
            SYS_IntegracaoExternaTipoDAO dao = new SYS_IntegracaoExternaTipoDAO();
            return dao.SelectAll();
        }

        public bool getById(SYS_IntegracaoExternaTipo entity)
        {
            SYS_IntegracaoExternaTipoDAO dao = new SYS_IntegracaoExternaTipoDAO();
             return dao.Carregar(entity);
        }


    }
}