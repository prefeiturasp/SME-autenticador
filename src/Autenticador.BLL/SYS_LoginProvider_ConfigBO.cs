/*
	Classe gerada automaticamente pelo Code Creator
*/
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using System.Data;
using System;

namespace Autenticador.BLL
{
    /// <summary>
    /// Description: SYS_LoginProvider_Config Business Object. 
    /// </summary>
    public class SYS_LoginProvider_ConfigBO : BusinessBase<SYS_LoginProvider_ConfigDAO, SYS_LoginProvider_Config>
    {
        public SYS_LoginProvider_ConfigDAO dao;
        public SYS_LoginProvider_ConfigBO()
        {
            try
            {
                dao = new SYS_LoginProvider_ConfigDAO();
            }
            catch (Exception err)
            {
                throw;
            }
        }
        /// <summary>
        ///  Retorna todas as configura��es de Autentica��o Externa
        /// </summary>
        /// <returns></returns>
        public DataTable CaregaTodasConfiguracoes()
        {
            try
            {
                return dao.CaregaTodasConfiguracoes();
            }
            catch (Exception err)
            {
                throw;
            }

        }
        /// <summary>
        ///  Retorna um SYS_LoginProvider_Config e os Dominios desta configura��o
        /// </summary>
        /// <param name="ent_id">ID da Entidade</param>
        /// <param name="LoginProvider">ID do provedor de Login</param>
        /// <returns>DataTable</returns>
        public DataTable CarregaConfiguracao(Guid ent_id, string LoginProvider)
        {
            try
            {
                return dao.CarregaConfiguracao(ent_id, LoginProvider);
            }
            catch (Exception err)
            {
                throw;
            }

        }
        public bool Carregar(SYS_LoginProvider_Config entity)
        {
            try
            {
                return dao.Carregar(entity);
            }
            catch (Exception err)
            {
                throw;
            }

        }
        /// <summary>
        /// Remove SYS_LoginProvider_Config
        /// </summary>
        /// <param name="entity">SYS_LoginProvider_Config</param>
        /// <returns>Bool</returns>
        public new bool Delete(SYS_LoginProvider_Config entity)
        {
            try
            {
                return dao.Delete(entity);
            }
            catch (Exception err)
            {
                throw;
            }
        }
        /// <summary>
        /// Deleta uma configura��o com sua lista de Dominios
        /// </summary>
        /// <param name="configEntity">SYS_LoginProvider_Config</param>
        /// <param name="dominioEntity">SYS_LoginProviderDominioPermitido</param>
        /// <returns>TRUE - se dele��o foi feita com sucesso e caso contrario FALSE</returns>
        public bool DeleteConfiguracoes(SYS_LoginProvider_Config configEntity, SYS_LoginProviderDominioPermitido dominioEntity)
        {
            SYS_LoginProviderDominioPermitidoDAO daoDominio = new SYS_LoginProviderDominioPermitidoDAO();

            bool ret = false;

            try
            {
                if (daoDominio._Banco == null)
                    daoDominio._Banco.Open(IsolationLevel.ReadCommitted);
                dao._Banco = daoDominio._Banco;

                if (daoDominio.Delete(dominioEntity))
                    if (dao.Delete(configEntity))
                        ret = true;

            }
            catch (Exception err)
            {
                if (dao._Banco == null)
                    dao._Banco.Close(err);
                if (daoDominio._Banco == null)
                    daoDominio._Banco.Close(err);
                throw;
            }
            finally
            {
                if (dao._Banco == null)
                    dao._Banco.Close();
                if (daoDominio._Banco == null)
                    daoDominio._Banco.Close();
            }

            return ret;
        }
        /// <summary>
        /// Salva ou atualiza dados da Configura��o e da Lista de Dom�nios
        /// </summary>
        /// <param name="configEntity">SYS_LoginProvider_Config</param>
        /// <param name="dominioEntity">SYS_LoginProviderDominioPermitido</param>
        /// <returns>Bool - TRUE se salvou ambos e caso contr�rio, FALSE</returns>
        public bool SalvarConfiguracoes(SYS_LoginProvider_Config configEntity, SYS_LoginProviderDominioPermitido dominioEntity)
        {
            SYS_LoginProviderDominioPermitidoDAO daoDominio = new SYS_LoginProviderDominioPermitidoDAO();

            bool ret = false;
            try
            {
                if (dao._Banco == null)
                    dao._Banco.Open(IsolationLevel.ReadCommitted);


                daoDominio._Banco = dao._Banco;

                if (dao.Salvar(configEntity))
                    if (daoDominio.Salvar(dominioEntity))
                        ret = true;
            }
            catch (Exception err)
            {
                if (dao._Banco == null)
                    dao._Banco.Close(err);
                if (daoDominio._Banco == null)
                    daoDominio._Banco.Close(err);
            }
            finally
            {
                if (dao._Banco == null)
                    dao._Banco.Close();
                if (daoDominio._Banco == null)
                    daoDominio._Banco.Close();
            }
            return ret;

        }
    }
}
