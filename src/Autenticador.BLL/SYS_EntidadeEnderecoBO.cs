using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.Data;
using CoreLibrary.Data.Common;

namespace Autenticador.BLL
{
    public class SYS_EntidadeEnderecoBO : BusinessBase<SYS_EntidadeEnderecoDAO, SYS_EntidadeEndereco>
    {
        #region MÉTODOS

        /// <summary>
        /// Retorna os dados do endereço, cidade e estado dos endereços cadastrados para
        /// a entidade.
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <returns></returns>
        public static DataTable SelectEnderecosBy_Entidade(Guid ent_id)
        {
            return new SYS_EntidadeEnderecoDAO().SelectEnderecosBy_Entidade(ent_id);
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <returns></returns>
        public static DataTable CarregarEnderecosBy_Entidade(Guid ent_id)
        {
            return new SYS_EntidadeEnderecoDAO().CarregarEnderecosBy_Entidade(ent_id);
        }

        /// <summary>
        /// Retorna os dados do endereço, cidade e estado dos endereços cadastrados para
        /// a entidade.
        /// </summary>
        /// <param name="ent_id">ID da entidade</param>
        /// <param name="banco">Transação com banco</param>
        /// <returns></returns>
        public static DataTable SelectEnderecosBy_Entidade(Guid ent_id, TalkDBTransaction banco)
        {
            return new SYS_EntidadeEnderecoDAO {_Banco = banco}.SelectEnderecosBy_Entidade(ent_id);
        }

        /// <summary>
        /// Seleciona o valor o primeiro valor valido de ene_id para um ent_id.
        /// </summary>
        /// <param name="ent_id">ID de entidade.</param>        
        /// <returns>valor de ene_id</returns>
        public static Guid Select_ene_idBy_ent_id
        (
            Guid ent_id
        )
        {
            SYS_EntidadeEnderecoDAO dal = new SYS_EntidadeEnderecoDAO();
            try
            {
                return dal.SelectBy_ent_id_top_one(ent_id);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}