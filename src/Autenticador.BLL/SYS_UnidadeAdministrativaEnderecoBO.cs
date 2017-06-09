using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;

namespace Autenticador.BLL
{
    public class SYS_UnidadeAdministrativaEnderecoBO : BusinessBase<SYS_UnidadeAdministrativaEnderecoDAO, SYS_UnidadeAdministrativaEndereco>
    {
        #region Estrutura

        public struct sUnidadeAdministrativaEndereco
        {
            public SYS_UnidadeAdministrativaEndereco unidadeAdministrativaEndereco { get; set; }
            public END_Endereco endereco { get; set; }
        }

        #endregion

        #region MÉTODOS
        /// <summary>
        /// Seleciona o valor o primeiro valor valido de uae_id para um ent_id e uad_id.
        /// </summary>
        /// <param name="ent_id">ID de entidade.</param>        
        /// <returns>valor de ene_id</returns>
        public static Guid Select_uae_idBy_ent_id_uad_id
        (
            Guid ent_id
            , Guid uad_id
        )
        {
            SYS_UnidadeAdministrativaEnderecoDAO dal = new SYS_UnidadeAdministrativaEnderecoDAO();
            try
            {
                return dal.SelectBy_ent_id_uad_id_top_one(ent_id, uad_id);
            }
            catch
            {
                throw;
            }
        }

        public static Guid Select_uae_idBy_ent_id_uad_id_uae_id (Guid ent_id, Guid uad_id, Guid uae_id)
        {
            SYS_UnidadeAdministrativaEnderecoDAO dal = new SYS_UnidadeAdministrativaEnderecoDAO();
            try
            {
                return dal.SelectUAE(ent_id, uad_id, uae_id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Seleciona o endereço de uma unidade administrativa.
        /// </summary>
        /// <param name="ent_id">ID da entidade da unidade administrativa.</param>
        /// <param name="uad_id">ID da unidade administrativa.</param>
        /// <returns></returns>
        //public static List<sUnidadeAdministrativaEndereco> SelecionaEndereco(Guid ent_id, Guid uad_id)
        //{
        //    SYS_UnidadeAdministrativaEnderecoDAO dao = new SYS_UnidadeAdministrativaEnderecoDAO();
        //    List<sUnidadeAdministrativaEndereco> list = new List<sUnidadeAdministrativaEndereco>();

        //    using (DataTable dt = dao.SelecionaEndereco(ent_id, uad_id))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                sUnidadeAdministrativaEndereco uaEndereco = new sUnidadeAdministrativaEndereco
        //                {
        //                    unidadeAdministrativaEndereco = new SYS_UnidadeAdministrativaEndereco()
        //                    ,
        //                    endereco = new END_Endereco()
        //                };

        //                uaEndereco.unidadeAdministrativaEndereco = dao.DataRowToEntity(dt.Rows[i], new SYS_UnidadeAdministrativaEndereco());
        //                uaEndereco.endereco = (new END_EnderecoDAO()).DataRowToEntity(dt.Rows[i], new END_Endereco());

        //                list.Add(uaEndereco);
        //            }
        //        }
        //    }

        //    return list;
        //}


        public static DataTable SelecionaEndereco(Guid ent_id, Guid uad_id)
        {
            SYS_UnidadeAdministrativaEnderecoDAO dao = new SYS_UnidadeAdministrativaEnderecoDAO();
            List<sUnidadeAdministrativaEndereco> list = new List<sUnidadeAdministrativaEndereco>();

            //using (DataTable dt = dao.SelecionaEndereco(ent_id, uad_id))
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            sUnidadeAdministrativaEndereco uaEndereco = new sUnidadeAdministrativaEndereco
            //            {
            //                unidadeAdministrativaEndereco = new SYS_UnidadeAdministrativaEndereco()
            //                ,
            //                endereco = new END_Endereco()
            //            };

            //            uaEndereco.unidadeAdministrativaEndereco = dao.DataRowToEntity(dt.Rows[i], new SYS_UnidadeAdministrativaEndereco());
            //            uaEndereco.endereco = (new END_EnderecoDAO()).DataRowToEntity(dt.Rows[i], new END_Endereco());

            //            list.Add(uaEndereco);
            //        }
            //    }
            //}

            //return list;
            return dao.SelecionaEndereco(ent_id, uad_id);
        }

        public static DataTable CarregaEnderecos(Guid ent_id, Guid uad_id)
        {
            SYS_UnidadeAdministrativaEnderecoDAO dao = new SYS_UnidadeAdministrativaEnderecoDAO();
            List<sUnidadeAdministrativaEndereco> list = new List<sUnidadeAdministrativaEndereco>();

            return dao.CarregaEnderecos(ent_id, uad_id);
        }
        #endregion
    }
}
