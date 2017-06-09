using System;
using System.Xml;
using System.Data;
using Autenticador.Entities;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using System.Collections.Generic;
using System.Linq;

namespace Autenticador.DAL
{
    public class SYS_UsuarioGrupoUADAO : Abstract_SYS_UsuarioGrupoUADAO
    {
        /// <summary>
        /// Retorna uma lista de unidades administrativas/entidades não paginadas 
        /// ordenado pelo nome da unidade administrativa ou entidade filtrados por
        /// grupos e usuário.
        /// </summary>
        /// <param name="usu_id">ID do usuário</param>
        /// <param name="gru_id">ID do grupos</param>
        /// <returns>Lista de unidades/entidade do grupo do usuário</returns>
        public DataTable SelectBy_UsuarioGrupo(Guid usu_id, Guid gru_id)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioGrupoUA_SelectBy_UsuarioGrupo", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                if (usu_id != Guid.Empty)
                    Param.Value = usu_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;
                if (gru_id != Guid.Empty)
                    Param.Value = gru_id;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return.Rows.Count > 0)
                    dt = qs.Return;

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
        public bool _SalvarXml(XmlNode xml, Guid usu_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_UsuarioGrupoUA_SalvarBy_XML", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Xml;
                Param.ParameterName = "@entidadeUaXml";
                XmlNode node = xml.SelectSingleNode("/ArrayOfTmpEntidadeUA/TmpEntidadeUA");
                if (node != null)
                    Param.Value = xml.OuterXml;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                if (qs.Return == 0)
                    return false;
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Deleta a ligação da ua com o grupo de usuário
        /// </summary>
        /// <param name="usu_id">ID do usuário - Obrigatório</param>
        /// <param name="gru_id">ID do grupo - Obrigatório</param>
        /// <param name="ent_id">ID da entidade - Obrigatório</param>
        /// <param name="uad_id">ID da unidade administrativa - Obrigatório</param>
        public void DeletarPorUsuarioGrupoUA
        (
            Guid usu_id
            , Guid gru_id
            , Guid ent_id
            , Guid uad_id
        )
        {            
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioGrupoUA_DeletarPorUsuarioGrupoUA", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;                
                Param.Value = usu_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@gru_id";
                Param.Size = 16;                
                Param.Value = gru_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@ent_id";
                Param.Size = 16;
                Param.Value = ent_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@uad_id";
                Param.Size = 16;
                Param.Value = uad_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona as entidades e unidades administrativas por login do usuário.
        /// </summary>
        /// <param name="usu_login">Login do usuário.</param>
        /// <returns></returns>
        public List<SYS_UsuarioGrupoUA> SelecionaPorLogin(string usu_login)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioGrupoUA_SelecionaPorLogin", _Banco);

            try
            {
                #region Parâmetro

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@usu_login";
                Param.Size = 500;
                Param.Value = usu_login;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return qs.Return.Rows.Count > 0 ?
                    qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new SYS_UsuarioGrupoUA())).ToList() :
                    new List<SYS_UsuarioGrupoUA>();
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Parâmetros para efetuar a inclusão sem o ID da PK gerado automaticamente
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_UsuarioGrupoUA entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = entity.usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@gru_id";
            Param.Size = 16;
            Param.Value = entity.gru_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ent_id";
            Param.Size = 16;
            Param.Value = entity.ent_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@uad_id";
            Param.Size = 16;
            if (entity.uad_id != Guid.Empty)
                Param.Value = entity.uad_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        /// <param name="entity"></param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioGrupoUA entity)
        {
            return true;
        }	
    }
}
