using System;
using System.Xml;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_UsuarioGrupoDAO : Abstract_SYS_UsuarioGrupoDAO
    {
        public bool _SalvarXml(XmlNode xml, Guid usu_id)
        {
            QueryStoredProcedure qs = new QueryStoredProcedure("NEW_SYS_UsuarioGrupo_SalvarBy_XML", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Xml;
                Param.ParameterName = "@grupoXml";
                XmlNode node = xml.SelectSingleNode("/ArrayOfTmpGrupos/TmpGrupos");
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Deleta a ligação dos grupos com o usuário
        /// </summary>
        /// <param name="usu_id">ID do usuário - Obrigatório</param>
        public void DeletarPorUsuario
        (
            Guid usu_id
        )
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioGrupo_DeletarPorUsuario", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@usu_id";
                Param.Size = 16;
                Param.Value = usu_id;
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
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_UsuarioGrupo entity)
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
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@usg_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade SYS_UsuarioGrupo</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(SYS_UsuarioGrupo entity)
        {
            __STP_DELETE = "NEW_SYS_UsuarioGrupo_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        /// <param name="entity"></param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, SYS_UsuarioGrupo entity)
        {
            return true;
        }	
    }
}
