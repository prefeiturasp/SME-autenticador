using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;
using Autenticador.Entities;

namespace Autenticador.DAL
{
    public class SYS_ParametroDAO : Abstract_SYS_ParametroDAO
    {
        #region Métodos

        /// <summary>
        /// Retorna os parâmetros ativos e vigentes.
        /// </summary>
        /// <returns>List de parâmetros</returns>
        public List<SYS_Parametro> SelectBy_ParametrosVigente()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_SelectBy_Vigente", _Banco);
            try
            {
                qs.Execute();

                List<SYS_Parametro> lt = qs.Return.Rows.Cast<DataRow>().Select(p => DataRowToEntity(p, new SYS_Parametro())).ToList();
                return lt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona o valor de um parâmetro filtrando pela par_chave.
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <returns>par_valor</returns>
        public string SelectBy_par_chave(string par_chave)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_SelectBy_par_chave", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@par_chave";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(par_chave))
                    Param.Value = par_chave;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                return qs.Return.Rows.Count > 0 ? qs.Return.Rows[0]["par_valor"].ToString() : string.Empty;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um datatable contendo todos os parametros cadastrados no BD
        /// não excluidos logicamente.
        /// </summary>
        /// <returns>Datatable com paramtros</returns>
        public DataTable Select(bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_Select", this._Banco);
            try
            {
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
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um Datatable com todos os valores cadastrados para um parametro nas quais não foram excluidas logicamente.
        /// filtrados por par_chave
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <param name="paginado"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns>Datatable com paramtros</returns>
        public DataTable SelectBy_par_chave2(string par_chave, bool paginado, int currentPage, int pageSize, out int totalRecords)
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_SelectBy_par_chave2", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@par_chave";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(par_chave))
                    Param.Value = par_chave;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

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
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Seleciona o valor de um parâmetro filtrando pela par_chave.
        /// </summary>
        /// <param name="par_chave"></param>
        /// <returns></returns>
        public bool SelectBy_par_chave3(string par_chave)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_SelectBy_par_chave2", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@par_chave";
                Param.Size = 100;
                if (!string.IsNullOrEmpty(par_chave))
                    Param.Value = par_chave;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                #endregion PARAMETROS

                qs.Execute();

                return qs.Return.Rows.Count > 1;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um Booleano na qual faz verificação de existencia de vigencia conflitante com relação as datas de vigencia
        /// na entidade do parametro.
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <param name="par_vigenciaInicio">Campo par_vigenciaInicio da tabela SYS_Parametro do bd</param>
        /// <param name="par_vigenciaFim">Campo par_vigenciaFim da tabela SYS_Parametro do bd</param>
        /// <param name="par_obrigatorio">Campo par_obrigatorio da tabela SYS_Parametro do bd</param>
        /// <returns>True - caso exista uma vigencia em conflito;</returns>
        public bool SelectBy_Vigencia(string par_chave, DateTime par_vigenciaInicio, DateTime par_vigenciaFim, Boolean par_obrigatorio)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_SelectBy_Vigencia", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@par_chave";
                Param.Size = 100;
                Param.Value = par_chave;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@par_vigenciaInicio";
                Param.Size = 3;
                Param.Value = par_vigenciaInicio;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@par_vigenciaFim";
                Param.Size = 3;
                if (!par_vigenciaFim.Equals(new DateTime()))
                    Param.Value = par_vigenciaFim;
                else
                    Param.Value = DBNull.Value;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Boolean;
                Param.ParameterName = "@par_obrigatorio";
                Param.Size = 1;
                Param.Value = par_obrigatorio;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return (qs.Return.Rows.Count > 0);
            }
            finally 
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Retorna um Booleano na qual faz atualização/adequação da data de vigencia final do ultimo parametro (anterior)
        /// ao parametro a ser inserido. Executado somente para parametros obrigatórios;
        /// </summary>
        /// <param name="par_chave">Campo par_chave da tabela SYS_Parametro do bd</param>
        /// <param name="par_vigenciaFim">Campo par_vigenciaFim da tabela SYS_Parametro do bd</param>
        /// <returns>True - caso realize a atualização;</returns>
        public bool Update_VigenciaFim(string par_chave, DateTime par_vigenciaFim)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_Parametro_UPDATE_VigenciaFim", this._Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.AnsiString;
                Param.ParameterName = "@par_chave";
                Param.Size = 100;
                Param.Value = par_chave;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@par_vigenciaFim";
                Param.Size = 3;
                Param.Value = par_vigenciaFim;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.DateTime;
                Param.ParameterName = "@par_dataAlteracao";
                Param.Size = 8;
                Param.Value = DateTime.Now;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                return (qs.Return.Rows.Count > 0);
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        #endregion

        #region Métodos sobreescritos

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="entity">Entidade SYS_Parametro</param>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, SYS_Parametro entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@par_chave";
            Param.Size = 100;
            Param.Value = entity.par_chave;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@par_valor";
            Param.Size = 1000;
            Param.Value = entity.par_valor;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@par_descricao";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.par_descricao))
                Param.Value = entity.par_descricao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@par_situacao";
            Param.Size = 1;
            Param.Value = entity.par_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_vigenciaInicio";
            Param.Size = 20;
            Param.Value = entity.par_vigenciaInicio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_vigenciaFim";
            Param.Size = 20;
            if (entity.par_vigenciaFim != new DateTime())
                Param.Value = entity.par_vigenciaFim;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@par_obrigatorio";
            Param.Size = 1;
            Param.Value = entity.par_obrigatorio;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação.
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="entity">Entidade SYS_Parametro</param>
        protected override void ParamAlterar(QueryStoredProcedure qs, SYS_Parametro entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@par_id";
            Param.Size = 16;
            Param.Value = entity.par_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@par_valor";
            Param.Size = 1000;
            Param.Value = entity.par_valor;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_vigenciaInicio";
            Param.Size = 3;
            Param.Value = entity.par_vigenciaInicio;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_vigenciaFim";
            Param.Size = 3;
            if (!entity.par_vigenciaFim.Equals(new DateTime()))
                Param.Value = entity.par_vigenciaFim;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Méotodo de update alterado para que não modificasse o valor do campo data da criação;
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns>True para alteração realizado com sucesso.</returns>
        protected override bool Alterar(SYS_Parametro entity)
        {
            this.__STP_UPDATE = "NEW_SYS_Parametro_Update";
            return base.Alterar(entity);
        }

        /// <summary>
        ///  Parâmetros para efetuar a exclusão logicamente.
        /// </summary>
        /// <param name="qs"></param>
        /// <param name="entity">Entidade SYS_Parametro</param>
        protected override void ParamDeletar(QueryStoredProcedure qs, SYS_Parametro entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@par_id";
            Param.Size = 16;
            Param.Value = entity.par_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@par_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@par_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica.
        /// </summary>
        /// <param name="entity">Entidade SYS_Parametro</param>
        /// <returns>True para exclusão realizado com sucesso.</returns>
        public override bool Delete(SYS_Parametro entity)
        {
            this.__STP_DELETE = "NEW_SYS_Parametro_Update_Situacao";
            return base.Delete(entity);
        }

        #endregion
    }
}