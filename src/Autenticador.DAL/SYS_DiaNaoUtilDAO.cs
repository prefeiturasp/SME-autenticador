using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_DiaNaoUtilDAO : Abstract_SYS_DiaNaoUtilDAO
    {
        /// <summary>
        /// Retorna uma list contendo todos os Dias Não Úteis
        /// que não foram excluídos logicamente, filtrados por cidade.
        /// </summary>
        /// <param name="cid_id">Id da cidade</param>
        /// <returns>List com as entidades</returns>
        public List<SYS_DiaNaoUtil> SelecionaTodosPorCidade
        (
             Guid cid_id
        )
        {
            List<SYS_DiaNaoUtil> lt = new List<SYS_DiaNaoUtil>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_DiaNaoUtil_SelecionaTodosPorCidade", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@cid_id";
                Param.Size = 16;
                Param.Value = cid_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    SYS_DiaNaoUtil entity = new SYS_DiaNaoUtil();
                    lt.Add(DataRowToEntity(dr, entity));
                }

                return lt;
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
        /// Retorna um datatable contendo todos os Dias Não Uteis
        /// que não foram excluídas logicamente, filtradas por 
        /// Nome, Abrangencia, Data, paginado.
        /// </summary>
        /// <param name="dnu_nome">Nome do Dia Não Util</param>
        /// <param name="dnu_abrangencia">Abrangência do Dia Não Util</param>
        /// <param name="dnu_data">Data do Dia Não Util</param>
        /// <param name="dnu_recorrencia"></param>
        /// <param name="paginado">Indica se vai exibir os registros paginados ou não.</param>
        /// <param name="currentPage">Página atual do gridview</param>
        /// <param name="pageSize">Total de registros por página</param>
        /// <param name="totalRecords"></param>
        /// <returns>DataTable com as entidades</returns>
        public DataTable Select
        (
             string dnu_nome
            , DateTime dnu_data
            , byte dnu_abrangencia
            , byte dnu_recorrencia
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_DiaNaoUtil_Select", this._Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@dnu_nome";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(dnu_nome))
                    Param.Value = dnu_nome;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@dnu_abrangencia";
                Param.Size = 1;
                if (dnu_abrangencia > 0)
                    Param.Value = dnu_abrangencia;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@dnu_data";
                Param.Size = 8;
                if (!dnu_data.Equals(new DateTime()))
                    Param.Value = dnu_data;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@dnu_recorrencia";
                Param.Size = 1;
                Param.Value = dnu_recorrencia;
                qs.Parameters.Add(Param);

                #endregion

                if (paginado)
                    totalRecords = qs.Execute(currentPage, pageSize);
                else
                {
                    qs.Execute();
                    totalRecords = qs.Return.Rows.Count;
                }

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

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.SYS_DiaNaoUtil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@dnu_nome";
            Param.Size = 100;
            Param.Value = entity.dnu_nome;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@dnu_abrangencia";
            Param.Size = 1;
            Param.Value = entity.dnu_abrangencia;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@dnu_descricao";
            Param.Size = 400;
            if (!string.IsNullOrEmpty(entity.dnu_descricao))
                Param.Value = entity.dnu_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@dnu_data";
            Param.Size = 20;
            Param.Value = entity.dnu_data;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@dnu_recorrencia";
            Param.Size = 1;
            Param.Value = entity.dnu_recorrencia;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_vigenciaInicio";
            Param.Size = 20;
            Param.Value = entity.dnu_vigenciaInicio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_vigenciaFim";
            Param.Size = 20;
            if (entity.dnu_vigenciaFim != new DateTime())
                Param.Value = entity.dnu_vigenciaFim;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_id";
            Param.Size = 16;
            if (entity.cid_id != Guid.Empty)
                Param.Value = entity.cid_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_id";
            Param.Size = 16;
            if (entity.unf_id != Guid.Empty)
                Param.Value = entity.unf_id;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@dnu_situacao";
            Param.Size = 1;
            Param.Value = entity.dnu_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação e a integridade
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.SYS_DiaNaoUtil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@dnu_id";
            Param.Size = 16;
            Param.Value = entity.dnu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@dnu_descricao";
            Param.Size = 400;
            Param.Value = entity.dnu_descricao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_vigenciaInicio";
            Param.Size = 20;
            Param.Value = entity.dnu_vigenciaInicio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_vigenciaFim";
            Param.Size = 3;
            if (!entity.dnu_vigenciaFim.Equals(new DateTime()))
                Param.Value = entity.dnu_vigenciaFim;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
        /// <summary>
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade com dados preenchidos</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(Autenticador.Entities.SYS_DiaNaoUtil entity)
        {
            this.__STP_UPDATE = "NEW_SYS_DiaNaoUtil_Update";
            return base.Alterar(entity);
        }

        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.SYS_DiaNaoUtil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@dnu_id";
            Param.Size = 16;
            Param.Value = entity.dnu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@dnu_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@dnu_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }
        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Delete(Autenticador.Entities.SYS_DiaNaoUtil entity)
        {
            this.__STP_DELETE = "NEW_SYS_DiaNaoUtil_Update_Situacao";
            return base.Delete(entity);
        }
    }
}
