/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Data;
using Autenticador.Entities;
using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;

namespace Autenticador.DAL
{
	
	/// <summary>
	/// 
	/// </summary>
	public class SYS_UsuarioFalhaAutenticacaoDAO : Abstract_SYS_UsuarioFalhaAutenticacaoDAO
    {
        #region Métodos

        /// <summary>
        /// Zera a quantidade de falhas que o usuário teve, para não exibir mais o captcha.
        /// Utilizada após o usuário efetuar um login com sucesso.
        /// </summary>
        /// <param name="usu_id">ID do usuário que efetuou login com sucesso</param>
        /// <returns></returns>
        public bool ZeraFalhaAutenticacaoUsuario(Guid usu_id)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioFalhaAutenticacao_ZeraFalhaUsuario", this._Banco);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = usu_id;
            qs.Parameters.Add(Param);

            qs.Execute();

            return qs.Return.Rows.Count > 0;
        }

	    /// <summary>
	    /// Insere um registro de falha de autenticação para o usuário, ou incrementa 1
	    /// no contador de erros caso o usuário já tenha errado, no intervalo de minutos informado.
	    /// Caso o último erro do usuário tenha sido depois desse intervalo, reinicia o contador pra 1.
	    /// Retorna o registro do usuário com a quantidade erros efetuada.
	    /// </summary>
	    /// <param name="usu_id">ID do usuário que efetuou o login com falha</param>
        /// <param name="minutosIntervalo">Quantidade de minutos que deve considerar como intervalo de espera para reiniciar o contador</param>
	    /// <returns></returns>
	    public SYS_UsuarioFalhaAutenticacao InsereFalhaAutenticacaoUsuario(Guid usu_id, int minutosIntervalo)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_UsuarioFalhaAutenticacao_InsereFalhaUsuario", this._Banco);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@usu_id";
            Param.Size = 16;
            Param.Value = usu_id;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Int32;
            Param.ParameterName = "@minutosIntervalo";
            Param.Size = 4;
            Param.Value = minutosIntervalo;
            qs.Parameters.Add(Param);

            qs.Execute();

            DataTable dt = qs.Return;
            SYS_UsuarioFalhaAutenticacao entity = new SYS_UsuarioFalhaAutenticacao();

            if (dt.Rows.Count > 0)
            {
                entity = DataRowToEntity(dt.Rows[0], entity);
            }

            return entity;
        }

        #endregion

        #region Sobrescritos

        public override bool Carregar(SYS_UsuarioFalhaAutenticacao entity)
        {
            __STP_LOAD = "NEW_SYS_UsuarioFalhaAutenticacao_Load";
            return base.Carregar(entity);
        }

        #endregion

        #region Sobrescritos - obsoletos

        [Obsolete("Não utilizar.", true)]
        protected override bool Inserir(SYS_UsuarioFalhaAutenticacao entity)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Não utilizar.", true)]
        protected override bool Alterar(SYS_UsuarioFalhaAutenticacao entity)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Não utilizar.", true)]
        public override bool Delete(SYS_UsuarioFalhaAutenticacao entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}