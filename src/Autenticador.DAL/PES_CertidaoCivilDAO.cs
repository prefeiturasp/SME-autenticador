/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Data;
using CoreLibrary.Data.Common;
using Autenticador.DAL.Abstracts;
using System.Collections.Generic;
using Autenticador.Entities;

namespace Autenticador.DAL
{
	
	/// <summary>
	/// 
	/// </summary>
	public class PES_CertidaoCivilDAO : Abstract_PES_CertidaoCivilDAO
	{
        /// <summary>
        /// Retorna a entidade PES_CertidaoCivil 
        /// filtrado por pessoa e tipo da certidão
        /// </summary>
        /// <param name="pes_id">Id da pessoa</param>
        /// <param name="ctc_tipo">Tipo da certidão</param>
        /// <returns></returns>
        public PES_CertidaoCivil SelecionaPorTipoCertidao(Guid pes_id, byte ctc_tipo)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_CertidaoCivil_SelecionaTipoCertidao", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                Param.Value = pes_id;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ctc_tipo";
                Param.Size = 1;
                Param.Value = ctc_tipo;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                DataTable dt = qs.Return;
                PES_CertidaoCivil entity = new PES_CertidaoCivil();

                if (dt.Rows.Count > 0)
                    return DataRowToEntity(dt.Rows[0], entity);
                else
                    return entity;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }


        /// <summary>
        /// Retorna um datatable contendo todas as certidoes da pessoa
        /// que não foram excluídos logicamente, filtrados por 
        /// id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da tabela PES_Pessoa do bd</param>
        /// <param name="paginado">Indica se o datatable será paginado ou não</param>
        /// <param name="currentPage">Página atual do grid</param>
        /// <param name="pageSize">Total de registros por página do grid</param>
        /// <param name="totalRecords">Total de registros retornado na busca</param>
        /// <returns>DataTable com as certidoes da pessoa</returns>
        public DataTable SelectBy_pes_id
        (
            Guid pes_id
            , bool paginado
            , int currentPage
            , int pageSize
            , out int totalRecords
        )
        {
            DataTable dt = new DataTable();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_CertidaoCivil_SelectBy_pes_id", _Banco);
            try
            {
                #region PARAMETROS

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                if (pes_id != Guid.Empty)
                    Param.Value = pes_id;
                else
                    Param.Value = DBNull.Value;
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
        /// Retorna um list de entidade PES_CertidaoCivil contendo 
        /// todas as certidoes da pessoa que não foram excluídos 
        /// logicamente, filtrados pelo id da pessoa
        /// </summary>
        /// <param name="pes_id">Id da pessoa</param>
        /// <returns></returns>
        public List<PES_CertidaoCivil> SelectBy_pes_id(Guid pes_id)
        {
            List<PES_CertidaoCivil> lt = new List<PES_CertidaoCivil>();
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_PES_CertidaoCivil_SelectBy_Pessoa", _Banco);
            try
            {
                #region Parâmetros

                Param = qs.NewParameter();
                Param.DbType = DbType.Guid;
                Param.ParameterName = "@pes_id";
                Param.Size = 16;
                Param.Value = pes_id;
                qs.Parameters.Add(Param);

                #endregion

                qs.Execute();

                foreach (DataRow dr in qs.Return.Rows)
                {
                    PES_CertidaoCivil entity = new PES_CertidaoCivil();
                    lt.Add(DataRowToEntity(dr, entity));
                }
                return lt;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        /// <summary>
        /// Parâmetros para efetuar a inclusão com data de criação e de alteração fixas
        /// </summary>
        protected override void ParamInserir(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_CertidaoCivil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ctc_tipo";
            Param.Size = 1;
            Param.Value = entity.ctc_tipo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_numeroTermo";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ctc_numeroTermo))
                Param.Value = entity.ctc_numeroTermo;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_folha";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.ctc_folha))
                Param.Value = entity.ctc_folha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_livro";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.ctc_livro))
                Param.Value = entity.ctc_livro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@ctc_dataEmissao";
            Param.Size = 20;
            if (entity.ctc_dataEmissao != new DateTime())
                Param.Value = entity.ctc_dataEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_nomeCartorio";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ctc_nomeCartorio))
                Param.Value = entity.ctc_nomeCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_idCartorio";
            Param.Size = 16;
            if (entity.cid_idCartorio != Guid.Empty)
                Param.Value = entity.cid_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idCartorio";
            Param.Size = 16;
            if (entity.unf_idCartorio != Guid.Empty)
                Param.Value = entity.unf_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_distritoCartorio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ctc_distritoCartorio))
                Param.Value = entity.ctc_distritoCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ctc_situacao";
            Param.Size = 1;
            Param.Value = entity.ctc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ctc_dataCriacao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ctc_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_matricula";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.ctc_matricula))
                Param.Value = entity.ctc_matricula;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_gemeo";
            Param.Size = 1;
            Param.Value = entity.ctc_gemeo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_modeloNovo";
            Param.Size = 1;
            Param.Value = entity.ctc_modeloNovo;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Parâmetros para efetuar a alteração preservando a data de criação
        /// </summary>
        protected override void ParamAlterar(QueryStoredProcedure qs, Autenticador.Entities.PES_CertidaoCivil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ctc_id";
            Param.Size = 16;
            Param.Value = entity.ctc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ctc_tipo";
            Param.Size = 1;
            Param.Value = entity.ctc_tipo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_numeroTermo";
            Param.Size = 50;
            if (!string.IsNullOrEmpty(entity.ctc_numeroTermo))
                Param.Value = entity.ctc_numeroTermo;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_folha";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.ctc_folha))
                Param.Value = entity.ctc_folha;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_livro";
            Param.Size = 20;
            if (!string.IsNullOrEmpty(entity.ctc_livro))
                Param.Value = entity.ctc_livro;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ctc_dataEmissao";
            Param.Size = 20;
            if (entity.ctc_dataEmissao != new DateTime())
                Param.Value = entity.ctc_dataEmissao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_nomeCartorio";
            Param.Size = 200;
            if (!string.IsNullOrEmpty(entity.ctc_nomeCartorio))
                Param.Value = entity.ctc_nomeCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@cid_idCartorio";
            Param.Size = 16;
            if (entity.cid_idCartorio != Guid.Empty)
                Param.Value = entity.cid_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@unf_idCartorio";
            Param.Size = 16;
            if (entity.unf_idCartorio != Guid.Empty)
                Param.Value = entity.unf_idCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_distritoCartorio";
            Param.Size = 100;
            if (!string.IsNullOrEmpty(entity.ctc_distritoCartorio))
                Param.Value = entity.ctc_distritoCartorio;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ctc_situacao";
            Param.Size = 1;
            Param.Value = entity.ctc_situacao;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ctc_dataAlteracao";
            Param.Size = 16;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@ctc_matricula";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.ctc_matricula))
                Param.Value = entity.ctc_matricula;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_gemeo";
            Param.Size = 1;
            Param.Value = entity.ctc_gemeo;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Boolean;
            Param.ParameterName = "@ctc_modeloNovo";
            Param.Size = 1;
            Param.Value = entity.ctc_modeloNovo;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o update não faça a alteração da data de criação
        /// </summary>
        /// <param name="entity"> Entidade PES_CertidaoCivil</param>
        /// <returns>true = sucesso | false = fracasso</returns>  
        protected override bool Alterar(Autenticador.Entities.PES_CertidaoCivil entity)
        {
            this.__STP_UPDATE = "NEW_PES_CertidaoCivil_UPDATE";
            return base.Alterar(entity);
        }

        /// <summary>
        /// Parâmetros para efetuar a exclusão lógica.
        /// </summary>
        protected override void ParamDeletar(QueryStoredProcedure qs, Autenticador.Entities.PES_CertidaoCivil entity)
        {
            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pes_id";
            Param.Size = 16;
            Param.Value = entity.pes_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@ctc_id";
            Param.Size = 16;
            Param.Value = entity.ctc_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Byte;
            Param.ParameterName = "@ctc_situacao";
            Param.Size = 1;
            Param.Value = 3;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime;
            Param.ParameterName = "@ctc_dataAlteracao";
            Param.Size = 8;
            Param.Value = DateTime.Now;
            qs.Parameters.Add(Param);
        }

        /// <summary>
        /// Método alterado para que o delete não faça exclusão física e sim lógica (update).
        /// </summary>
        /// <param name="entity"> Entidade PES_CertidaoCivil</param>
        /// <returns>true = sucesso | false = fracasso</returns>        
        public override bool Delete(Autenticador.Entities.PES_CertidaoCivil entity)
        {
            this.__STP_DELETE = "NEW_PES_CertidaoCivil_Update_Situacao";
            return base.Delete(entity);
        }

        /// <summary>
        /// Recebe o valor do auto incremento e coloca na propriedade 
        /// </summary>
        /// <param name="qs">Objeto da Store Procedure</param>
        protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, Autenticador.Entities.PES_CertidaoCivil entity)
        {
            return true;
        }	

		///// <summary>
        ///// Inseri os valores da classe em um registro ja existente
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem modificados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool Alterar(PES_CertidaoCivil entity)
        //{
        //    return base.Alterar(entity);
        //}
        ///// <summary>
        ///// Inseri os valores da classe em um novo registro
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem inseridos</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool Inserir(PES_CertidaoCivil entity)
        //{
        //    return base.Inserir(entity);
        //}
        ///// <summary>
        ///// Carrega um registro da tabela usando os valores nas chaves
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem carregados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Carregar(PES_CertidaoCivil entity)
        //{
        //    return base.Carregar(entity);
        //}
        ///// <summary>
        ///// Exclui um registro do banco
        ///// </summary>
        ///// <param name="entity">Entidade com os dados a serem apagados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Delete(PES_CertidaoCivil entity)
        //{
        //    return base.Delete(entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Alterar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamAlterar(QueryStoredProcedure qs, PES_CertidaoCivil entity)
        //{
        //    base.ParamAlterar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Carregar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamCarregar(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
        //{
        //    base.ParamCarregar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Deletar
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamDeletar(QueryStoredProcedure qs, PES_CertidaoCivil entity)
        //{
        //    base.ParamDeletar(qs, entity);
        //}
        ///// <summary>
        ///// Configura os parametros do metodo de Inserir
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados para preenchimento dos parametros</param>
        //protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
        //{
        //    base.ParamInserir(qs, entity);
        //}
        ///// <summary>
        ///// Salva o registro no banco de dados
        ///// </summary>
        ///// <param name="entity">Entidade com os dados para preenchimento para inserir ou alterar</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //public override bool Salvar(PES_CertidaoCivil entity)
        //{
        //    return base.Salvar(entity);
        //}
        ///// <summary>
        ///// Realiza o select da tabela
        ///// </summary>
        ///// <returns>Lista com todos os registros da tabela</returns>
        //public override IList<PES_CertidaoCivil> Select()
        //{
        //    return base.Select();
        //}
        ///// <summary>
        ///// Realiza o select da tabela com paginacao
        ///// </summary>
        ///// <param name="currentPage">Pagina atual</param>
        ///// <param name="pageSize">Tamanho da pagina</param>
        ///// <param name="totalRecord">Total de registros na tabela original</param>
        ///// <returns>Lista com todos os registros da p�gina</returns>
        //public override IList<PES_CertidaoCivil> Select_Paginado(int currentPage, int pageSize, out int totalRecord)
        //{
        //    return base.Select_Paginado(currentPage, pageSize, out totalRecord);
        //}
        ///// <summary>
        ///// Recebe o valor do auto incremento e coloca na propriedade 
        ///// </summary>
        ///// <param name="qs">Objeto da Store Procedure</param>
        ///// <param name="entity">Entidade com os dados</param>
        ///// <returns>True - Operacao bem sucedida</returns>
        //protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, PES_CertidaoCivil entity)
        //{
        //    return base.ReceberAutoIncremento(qs, entity);
        //}
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados</param>
        ///// <returns>Entidade preenchida</returns>
        //public override PES_CertidaoCivil DataRowToEntity(DataRow dr, PES_CertidaoCivil entity)
        //{
        //    return base.DataRowToEntity(dr, entity);
        //}
        ///// <summary>
        ///// Passa os dados de um datatable para uma entidade
        ///// </summary>
        ///// <param name="dr">DataRow do datatable preenchido</param>
        ///// <param name="entity">Entidade onde ser�o transferidos os dados</param>
        ///// <param name="limparEntity">Indica se a entidade deve ser limpada antes da transferencia</param>
        ///// <returns>Entidade preenchida</returns>
        //public override PES_CertidaoCivil DataRowToEntity(DataRow dr, PES_CertidaoCivil entity, bool limparEntity)
        //{
        //    return base.DataRowToEntity(dr, entity, limparEntity);
        //}
	}
}