/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL.Abstracts
{
	
	/// <summary>
	/// Classe abstrata de PES_PessoaDocumento
	/// </summary>
	public abstract class Abstract_PES_PessoaDocumentoDAO : Abstract_DAL<PES_PessoaDocumento>
	{
	
        protected override string ConnectionStringName
        {
            get
            {
                return "AutenticadorDB";
            }
        }
        	
		/// <summary>
		/// Configura os parametros do metodo de carregar
		/// </ssummary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamCarregar(QuerySelectStoredProcedure qs, PES_PessoaDocumento entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tdo_id";
			Param.Size = 16;
			Param.Value = entity.tdo_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Configura os parametros do metodo de Inserir
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamInserir(QuerySelectStoredProcedure qs, PES_PessoaDocumento entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tdo_id";
			Param.Size = 16;
			Param.Value = entity.tdo_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_numero";
			Param.Size = 50;
			Param.Value = entity.psd_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Date;
			Param.ParameterName = "@psd_dataEmissao";
			Param.Size = 20;
			if( entity.psd_dataEmissao!= new DateTime() )
				Param.Value = entity.psd_dataEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_orgaoEmissao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.psd_orgaoEmissao) )
				Param.Value = entity.psd_orgaoEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@unf_idEmissao";
			Param.Size = 16;
				Param.Value = entity.unf_idEmissao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_infoComplementares";
			Param.Size = 1000;
			if( !string.IsNullOrEmpty(entity.psd_infoComplementares) )
				Param.Value = entity.psd_infoComplementares;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@psd_situacao";
			Param.Size = 1;
			Param.Value = entity.psd_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@psd_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.psd_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@psd_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.psd_dataAlteracao;
			qs.Parameters.Add(Param);

            //NOVOS CAMPOS
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_categoria";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_categoria))
            {
                Param.Value = entity.psd_categoria;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_classificacao";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_classificacao))
            {
                Param.Value = entity.psd_classificacao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_csm";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_csm))
            {
                Param.Value = entity.psd_csm;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataEntrada";
            Param.Size = 20;
            if (entity.psd_dataEntrada != new DateTime())
            {
                Param.Value = entity.psd_dataEntrada;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataValidade";
            Param.Size = 20;
            if (entity.psd_dataValidade != new DateTime())
            {
                Param.Value = entity.psd_dataValidade;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_idOrigem";
            Param.Size = 16;
            Param.Value = entity.pai_idOrigem;
            if (entity.pai_idOrigem != Guid.Empty)
                Param.Value = entity.pai_idOrigem;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_serie";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_serie))
            {
                Param.Value = entity.psd_serie;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_tipoGuarda";
            Param.Size = 128;
            if (!string.IsNullOrEmpty(entity.psd_tipoGuarda))
            {
                Param.Value = entity.psd_tipoGuarda;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_via";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_via))
            {
                Param.Value = entity.psd_via;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_secao";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_secao))
            {
                Param.Value = entity.psd_secao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_zona";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_zona))
            {
                Param.Value = entity.psd_zona;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_regiaoMilitar";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_regiaoMilitar))
            {
                Param.Value = entity.psd_regiaoMilitar;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numeroRA";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_numeroRA))
            {
                Param.Value = entity.psd_numeroRA;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@psd_dataExpedicao";
            Param.Size = 20;
            if (entity.psd_dataEmissao != new DateTime())
                Param.Value = entity.psd_dataExpedicao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

        }
		
		/// <summary>
		/// Configura os parametros do metodo de Alterar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamAlterar(QueryStoredProcedure qs, PES_PessoaDocumento entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tdo_id";
			Param.Size = 16;
			Param.Value = entity.tdo_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_numero";
			Param.Size = 50;
			Param.Value = entity.psd_numero;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Date;
			Param.ParameterName = "@psd_dataEmissao";
			Param.Size = 20;
			if( entity.psd_dataEmissao!= new DateTime() )
				Param.Value = entity.psd_dataEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_orgaoEmissao";
			Param.Size = 200;
			if( !string.IsNullOrEmpty(entity.psd_orgaoEmissao) )
				Param.Value = entity.psd_orgaoEmissao;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@unf_idEmissao";
			Param.Size = 16;
				Param.Value = entity.unf_idEmissao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.AnsiString;
			Param.ParameterName = "@psd_infoComplementares";
			Param.Size = 1000;
			if( !string.IsNullOrEmpty(entity.psd_infoComplementares) )
				Param.Value = entity.psd_infoComplementares;
			else
				Param.Value = DBNull.Value;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Byte;
			Param.ParameterName = "@psd_situacao";
			Param.Size = 1;
			Param.Value = entity.psd_situacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@psd_dataCriacao";
			Param.Size = 16;
			Param.Value = entity.psd_dataCriacao;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.DateTime;
			Param.ParameterName = "@psd_dataAlteracao";
			Param.Size = 16;
			Param.Value = entity.psd_dataAlteracao;
			qs.Parameters.Add(Param);

            //NOVOS CAMPOS
            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_categoria";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_categoria))
            {
                Param.Value = entity.psd_categoria;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_classificacao";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_classificacao))
            {
                Param.Value = entity.psd_classificacao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_csm";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_csm))
            {
                Param.Value = entity.psd_csm;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataEntrada";
            Param.Size = 20;
            if (entity.psd_dataEntrada != new DateTime())
            {
                Param.Value = entity.psd_dataEntrada;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.DateTime2;
            Param.ParameterName = "@psd_dataValidade";
            Param.Size = 20;
            if (entity.psd_dataValidade != new DateTime())
            {
                Param.Value = entity.psd_dataValidade;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Guid;
            Param.ParameterName = "@pai_idOrigem";
            Param.Size = 16;
            Param.Value = entity.pai_idOrigem;
            if (entity.pai_idOrigem != Guid.Empty)
                Param.Value = entity.pai_idOrigem;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_serie";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_serie))
            {
                Param.Value = entity.psd_serie;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_tipoGuarda";
            Param.Size = 128;
            if (!string.IsNullOrEmpty(entity.psd_tipoGuarda))
            {
                Param.Value = entity.psd_tipoGuarda;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_via";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_via))
            {
                Param.Value = entity.psd_via;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_secao";
            Param.Size = 32;
            if (!string.IsNullOrEmpty(entity.psd_secao))
            {
                Param.Value = entity.psd_secao;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_zona";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_zona))
            {
                Param.Value = entity.psd_zona;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_regiaoMilitar";
            Param.Size = 16;
            if (!string.IsNullOrEmpty(entity.psd_regiaoMilitar))
            {
                Param.Value = entity.psd_regiaoMilitar;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.AnsiString;
            Param.ParameterName = "@psd_numeroRA";
            Param.Size = 64;
            if (!string.IsNullOrEmpty(entity.psd_numeroRA))
            {
                Param.Value = entity.psd_numeroRA;
            }
            else
            {
                Param.Value = DBNull.Value;
            }
            qs.Parameters.Add(Param);

            Param = qs.NewParameter();
            Param.DbType = DbType.Date;
            Param.ParameterName = "@psd_dataExpedicao";
            Param.Size = 20;
            if (entity.psd_dataEmissao != new DateTime())
                Param.Value = entity.psd_dataExpedicao;
            else
                Param.Value = DBNull.Value;
            qs.Parameters.Add(Param);

        }

		/// <summary>
		/// Configura os parametros do metodo de Deletar
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override void ParamDeletar(QueryStoredProcedure qs, PES_PessoaDocumento entity)
		{
			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@pes_id";
			Param.Size = 16;
			Param.Value = entity.pes_id;
			qs.Parameters.Add(Param);

			Param = qs.NewParameter();
			Param.DbType = DbType.Guid;
			Param.ParameterName = "@tdo_id";
			Param.Size = 16;
			Param.Value = entity.tdo_id;
			qs.Parameters.Add(Param);


		}
		
		/// <summary>
		/// Recebe o valor do auto incremento e coloca na propriedade 
		/// </summary>
		/// <param name="qs">Objeto da Store Procedure</param>
		protected override bool ReceberAutoIncremento(QuerySelectStoredProcedure qs, PES_PessoaDocumento entity)
		{
            entity.tdo_id = new Guid(qs.Return.Rows[0][0].ToString());
            return (entity.tdo_id != Guid.Empty); 
		}		
	}
}

