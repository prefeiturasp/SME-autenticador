using System.ComponentModel;
using System.Data;
using Autenticador.DAL;
using Autenticador.Entities;
using CoreLibrary.Business.Common;

namespace Autenticador.BLL
{
    /// <summary>
    /// Visões do sistema.
    /// </summary>
    public static class SysVisaoID
    {
        // Enumeradores que representam as visões do sistema.
        internal enum SYS_VisaoID : byte
        {
            Administracao = 1,
            Gestao = 2,
            UnidadeAdministrativa = 3,
            Individual = 4
        }

        /// <summary>
        /// ID da visão Administração.
        /// </summary>
        public static int Administracao
        {
            get
            {
                return (byte)SYS_VisaoID.Administracao;
            }
        }

        /// <summary>
        /// ID da visão Gestão.
        /// </summary>
        public static int Gestao
        {
            get
            {
                return (byte)SYS_VisaoID.Gestao;
            }
        }

        /// <summary>
        /// ID da visão Unidade Administrativa.
        /// </summary>
        public static int UnidadeAdministrativa
        {
            get
            {
                return (byte)SYS_VisaoID.UnidadeAdministrativa;
            }
        }

        /// <summary>
        /// ID da visão Individual.
        /// </summary>
        public static int Individual
        {
            get
            {
                return (byte)SYS_VisaoID.Individual;
            }
        }
    }

    public class SYS_VisaoBO : BusinessBase<SYS_VisaoDAO, SYS_Visao>
    {
        /// <summary>
        /// Retorna todas as visões.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelectAll()
        {
            SYS_VisaoDAO dal = new SYS_VisaoDAO();
            try
            {
                return dal.GetSelectAll();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o id da visão pelo nome.
        /// </summary>
        /// <param name="vis_nome"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        internal static int GetSelect_vis_id(string vis_nome)
        {


            SYS_VisaoDAO dao = new SYS_VisaoDAO();
            try
            {
                return dao.GetSelect_vis_id(vis_nome);
            }
            catch
            {
                throw;
            }
        }

        ///// <summary>
        ///// Retorna o ID da visão de acordo com o tipo.
        ///// </summary>
        ///// <param name="visao"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public static int GetID_By_Visao(SysVisao visao)
        //{
        //    SYS_VisaoDAO dao = new SYS_VisaoDAO();
        //    try
        //    {
        //        string vis_nome = "";

        //        switch (visao)
        //        {
        //            case SysVisao.Administracao:
        //                {
        //                    vis_nome = "Administração";
        //                    break;
        //                }
        //            case SysVisao.Gestao:
        //                {
        //                    vis_nome = "Gestão";
        //                    break;
        //                }
        //            case SysVisao.UnidadeAdministrativa:
        //                {
        //                    vis_nome = "Unidade Administrativa";
        //                    break;
        //                }
        //            case SysVisao.Individual:
        //                {
        //                    vis_nome = "Individual";
        //                    break;
        //                }
        //        }

        //        return dao.GetSelect_vis_id(vis_nome);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
