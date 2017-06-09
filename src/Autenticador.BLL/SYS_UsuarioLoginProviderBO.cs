/*
	Classe gerada automaticamente pelo Code Creator
*/
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System.Data;
using System;
using System.Collections.Generic;

namespace Autenticador.BLL
{


    /// <summary>
    /// Description: SYS_UsuarioLoginProvider Business Object. 
    /// </summary>
    public class SYS_UsuarioLoginProviderBO : BusinessBase<SYS_UsuarioLoginProviderDAO, SYS_UsuarioLoginProvider>
    {
        /// <summary>
        /// Checa o identificador do usu�rio no provider 
        /// </summary>
        /// <param name="usu_id">Id do usu�rio</param>
        /// <returns></returns>
        public static SYS_UsuarioLoginProvider SelectBy_usu_id_LoginProvider(Guid usu_id, string loginProvider)
        {
            // Deve retornar o usuarioLoginProvider conforme usu_id.   
            // Tabela SYS_UsuarioLoginProvider              
            // Se n�o encontrar retornar null. 

            SYS_UsuarioLoginProviderDAO dal = new SYS_UsuarioLoginProviderDAO();

            return dal.SelectBy_usu_id_LoginProvider(usu_id, loginProvider);

        }

        /// <summary>
        /// Retorna a lista de contas externas vinculadas ao usu�rio.
        /// </summary>
        /// <param name="usu_id"></param>
        /// <returns></returns>
        public static List<SYS_UsuarioLoginProvider> SelectBy_usu_id(Guid usu_id)
        {

            SYS_UsuarioLoginProviderDAO dal = new SYS_UsuarioLoginProviderDAO();

            return dal.SelectBy_usu_id(usu_id);

        }

        /// <summary>
        /// Delete o UsuarioLoginProvider pelo Id do usu�rio
        /// </summary>
        /// <param name="usu_id">Id do usu�rio</param>
        /// <returns>true = deletado / false = n�o deletado</returns>
        public static bool DeleteBy_usu_id(Guid usu_id)
        {
            SYS_UsuarioLoginProviderDAO dal = new SYS_UsuarioLoginProviderDAO();
            try
            {
                return dal.DeleteBy_usu_id(usu_id);
            }
            catch (Exception err)
            {
                throw;
            }

        }
    }
}