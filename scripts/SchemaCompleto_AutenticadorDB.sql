
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UnidadeAdministrativaContato]'
GO
CREATE TABLE [dbo].[SYS_UnidadeAdministrativaContato]
(
[ent_id] [uniqueidentifier] NOT NULL,
[uad_id] [uniqueidentifier] NOT NULL,
[uac_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Unida__uac_i__4F7CD00D] DEFAULT (newsequentialid()),
[tmc_id] [uniqueidentifier] NOT NULL,
[uac_contato] [varchar] (200) NOT NULL,
[uac_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaContato_uac_situacao] DEFAULT ((1)),
[uac_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaContato_uac_dataCriacao] DEFAULT (getdate()),
[uac_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaContato_uac_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UnidadeAdministrativaContato] on [dbo].[SYS_UnidadeAdministrativaContato]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaContato] ADD CONSTRAINT [PK_SYS_UnidadeAdministrativaContato] PRIMARY KEY CLUSTERED  ([ent_id], [uad_id], [uac_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaContato_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 05/02/2010 11:10
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Entidade e Unidade Administrativa. Filtrada por: 
--					ent_id, uad_id, enc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaContato_Update_Situacao]	
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uac_id uniqueidentifier
		,@uac_situacao TINYINT
		,@uac_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaContato 
	SET 
		uac_situacao = @uac_situacao
		,uac_dataAlteracao = @uac_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uac_id = @uac_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_TemaPaleta]'
GO
CREATE TABLE [dbo].[CFG_TemaPaleta]
(
[tep_id] [int] NOT NULL,
[tpl_id] [int] NOT NULL,
[tpl_nome] [varchar] (100) NOT NULL,
[tpl_caminhoCSS] [varchar] (1000) NOT NULL,
[tpl_imagemExemploTema] [varchar] (2000) NULL,
[tpl_situacao] [tinyint] NOT NULL,
[tpl_dataCriacao] [datetime] NOT NULL,
[tpl_dataAlteracao] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_TemaPaleta] on [dbo].[CFG_TemaPaleta]'
GO
ALTER TABLE [dbo].[CFG_TemaPaleta] ADD CONSTRAINT [PK_CFG_TemaPaleta] PRIMARY KEY CLUSTERED  ([tep_id], [tpl_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating trigger [dbo].[TRG_CFG_TemaPaleta_Identity] on [dbo].[CFG_TemaPaleta]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 20/01/2015
-- Description:	Realiza o autoincremento do 
--				campo tlp_id garantindo que
--				sempre será reiniciado em 1
--				qdo um tep_id for inserido
-- =============================================
CREATE TRIGGER [dbo].[TRG_CFG_TemaPaleta_Identity] 
  ON  [dbo].[CFG_TemaPaleta] INSTEAD OF INSERT
AS 
BEGIN
	DECLARE @ID INT;
	
	SELECT
		@ID = ISNULL(MAX(CFG_TemaPaleta.tpl_id), 0) + 1
	FROM
		CFG_TemaPaleta WITH(XLOCK,TABLOCK)
		INNER JOIN inserted
			ON CFG_TemaPaleta.tep_id = inserted.tep_id 		
			
	INSERT INTO CFG_TemaPaleta 
	(
		tep_id,
		tpl_id,
		tpl_nome,
		tpl_caminhoCSS,
		tpl_imagemExemploTema,
		tpl_situacao,
		tpl_dataCriacao,
		tpl_dataAlteracao
	)
	SELECT
		tep_id,
		@ID,
		tpl_nome,
		tpl_caminhoCSS,
		tpl_imagemExemploTema,
		tpl_situacao,
		tpl_dataCriacao,
		tpl_dataAlteracao
	FROM
		inserted
		
	SELECT ISNULL(@ID, -1) 	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[END_UnidadeFederativa]'
GO
CREATE TABLE [dbo].[END_UnidadeFederativa]
(
[unf_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__END_Unida__unf_i__24927208] DEFAULT (newsequentialid()),
[pai_id] [uniqueidentifier] NOT NULL,
[unf_nome] [varchar] (100) NOT NULL,
[unf_sigla] [varchar] (2) NOT NULL,
[unf_situacao] [tinyint] NOT NULL CONSTRAINT [DF_END_UnidadeFederativa_unf_situacao] DEFAULT ((1)),
[unf_integridade] [int] NOT NULL CONSTRAINT [DF_END_UnidadeFederativa_unf_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_END_UnidadeFederativa] on [dbo].[END_UnidadeFederativa]'
GO
ALTER TABLE [dbo].[END_UnidadeFederativa] ADD CONSTRAINT [PK_END_UnidadeFederativa] PRIMARY KEY CLUSTERED  ([unf_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[END_Pais]'
GO
CREATE TABLE [dbo].[END_Pais]
(
[pai_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__END_Pais__pai_id__54D8E40C] DEFAULT (newsequentialid()),
[pai_nome] [varchar] (100) NOT NULL,
[pai_sigla] [varchar] (10) NULL,
[pai_ddi] [varchar] (3) NULL,
[pai_naturalMasc] [varchar] (100) NULL,
[pai_naturalFem] [varchar] (100) NULL,
[pai_situacao] [tinyint] NOT NULL CONSTRAINT [DF_END_Pais_pai_situacao] DEFAULT ((1)),
[pai_integridade] [int] NOT NULL CONSTRAINT [DF_END_Pais_pai_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_END_Pais] on [dbo].[END_Pais]'
GO
ALTER TABLE [dbo].[END_Pais] ADD CONSTRAINT [PK_END_Pais] PRIMARY KEY CLUSTERED  ([pai_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[END_Cidade]'
GO
CREATE TABLE [dbo].[END_Cidade]
(
[cid_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__END_Cidad__cid_i__4222D4EF] DEFAULT (newsequentialid()),
[pai_id] [uniqueidentifier] NOT NULL,
[unf_id] [uniqueidentifier] NULL,
[cid_nome] [varchar] (200) NOT NULL,
[cid_ddd] [varchar] (3) NULL,
[cid_situacao] [tinyint] NOT NULL CONSTRAINT [DF_END_Cidade_cid_situacao] DEFAULT ((1)),
[cid_integridade] [int] NOT NULL CONSTRAINT [DF_END_Cidade_cid_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_END_Cidade] on [dbo].[END_Cidade]'
GO
ALTER TABLE [dbo].[END_Cidade] ADD CONSTRAINT [PK_END_Cidade] PRIMARY KEY CLUSTERED  ([cid_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_SelectBy_PesquisaIncremental]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 03/01/2011 09:51
-- Description:	utilizado na busca incremental de cidades
--				retorna as cidades que não foram excluídas logicamente
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_SelectBy_PesquisaIncremental]		
	@cid_nome VARCHAR(200)
AS
BEGIN
	SELECT TOP 10 
		cid_id				
		, cid_nome
		, CASE WHEN END_Cidade.unf_id IS NULL THEN cid_nome + ' - ' + pai_nome
			   ELSE cid_nome + '/' + unf_sigla + ' - ' + pai_nome END AS cid_unf_pai_nome
		, END_Cidade.unf_id
	FROM
		END_Cidade WITH (NOLOCK)
	LEFT JOIN END_UnidadeFederativa WITH (NOLOCK) 
		ON  END_Cidade.unf_id = END_UnidadeFederativa.unf_id
			AND unf_situacao <> 3	
	INNER JOIN END_Pais WITH (NOLOCK) 
		ON END_Cidade.pai_id = END_Pais.pai_id
	WHERE						
		cid_situacao <> 3
    	AND pai_situacao <> 3
		AND (@cid_nome is null or cid_nome LIKE '%' + @cid_nome + '%')
	ORDER BY
		cid_nome	  
	
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_Arquivo]'
GO
CREATE TABLE [dbo].[CFG_Arquivo]
(
[arq_id] [bigint] NOT NULL IDENTITY(1, 1),
[arq_nome] [varchar] (256) NOT NULL,
[arq_tamanhoKB] [bigint] NOT NULL,
[arq_typeMime] [varchar] (200) NOT NULL,
[arq_data] [varbinary] (max) NOT NULL,
[arq_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Arquivo_arq_situacao] DEFAULT ((1)),
[arq_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Arquivo_arq_dataCriacao] DEFAULT (getdate()),
[arq_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Arquivo_arq_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_Arquivo] on [dbo].[CFG_Arquivo]'
GO
ALTER TABLE [dbo].[CFG_Arquivo] ADD CONSTRAINT [PK_CFG_Arquivo] PRIMARY KEY CLUSTERED  ([arq_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Arquivo_UPDATE]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_Arquivo_UPDATE]
	@arq_id BIGINT
	, @arq_nome VARCHAR (256)
	, @arq_tamanhoKB BIGINT
	, @arq_typeMime VARCHAR (200)
	, @arq_data VARBINARY(MAX)
	, @arq_situacao TINYINT
	, @arq_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_Arquivo 
	SET 
		arq_nome = @arq_nome 
		, arq_tamanhoKB = @arq_tamanhoKB 
		, arq_typeMime = @arq_typeMime 
		, arq_data = @arq_data 
		, arq_situacao = @arq_situacao 
		, arq_dataAlteracao = @arq_dataAlteracao 

	WHERE 
		arq_id = @arq_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_UsuarioAPI]'
GO
CREATE TABLE [dbo].[CFG_UsuarioAPI]
(
[uap_id] [int] NOT NULL IDENTITY(1, 1),
[uap_username] [varchar] (100) NOT NULL,
[uap_password] [varchar] (256) NOT NULL,
[uap_situacao] [tinyint] NOT NULL,
[uap_dataCriacao] [datetime] NOT NULL,
[uap_dataAlteracao] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_UsuarioAPI] on [dbo].[CFG_UsuarioAPI]'
GO
ALTER TABLE [dbo].[CFG_UsuarioAPI] ADD CONSTRAINT [PK_CFG_UsuarioAPI] PRIMARY KEY CLUSTERED  ([uap_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_UsuarioAPI_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_UsuarioAPI_LOAD]
	@uap_id Int
	
AS
BEGIN
	SELECT	Top 1
		 uap_id  
		, uap_username 
		, uap_password 
		, uap_situacao 
		, uap_dataCriacao 
		, uap_dataAlteracao 

 	FROM
 		CFG_UsuarioAPI
	WHERE 
		uap_id = @uap_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_VisaoModulo]'
GO
CREATE TABLE [dbo].[SYS_VisaoModulo]
(
[vis_id] [int] NOT NULL,
[sis_id] [int] NOT NULL,
[mod_id] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_VisaoModulo] on [dbo].[SYS_VisaoModulo]'
GO
ALTER TABLE [dbo].[SYS_VisaoModulo] ADD CONSTRAINT [PK_SYS_VisaoModulo] PRIMARY KEY CLUSTERED  ([vis_id], [sis_id], [mod_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_GrupoPermissao]'
GO
CREATE TABLE [dbo].[SYS_GrupoPermissao]
(
[gru_id] [uniqueidentifier] NOT NULL,
[sis_id] [int] NOT NULL,
[mod_id] [int] NOT NULL,
[grp_consultar] [bit] NOT NULL CONSTRAINT [DF_SYS_GrupoPermissao_grp_consultar] DEFAULT ((1)),
[grp_inserir] [bit] NOT NULL CONSTRAINT [DF_SYS_GrupoPermissao_grp_inserir] DEFAULT ((1)),
[grp_alterar] [bit] NOT NULL CONSTRAINT [DF_SYS_GrupoPermissao_grp_alterar] DEFAULT ((1)),
[grp_excluir] [bit] NOT NULL CONSTRAINT [DF_SYS_GrupoPermissao_grp_excluir] DEFAULT ((1))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_GrupoPermissao] on [dbo].[SYS_GrupoPermissao]'
GO
ALTER TABLE [dbo].[SYS_GrupoPermissao] ADD CONSTRAINT [PK_SYS_GrupoPermissao] PRIMARY KEY CLUSTERED  ([gru_id], [sis_id], [mod_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_GrupoPermissao_01] on [dbo].[SYS_GrupoPermissao]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_GrupoPermissao_01] ON [dbo].[SYS_GrupoPermissao] ([gru_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Grupo]'
GO
CREATE TABLE [dbo].[SYS_Grupo]
(
[gru_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Grupo__gru_i__33D4B598] DEFAULT (newsequentialid()),
[gru_nome] [varchar] (50) NOT NULL,
[gru_situacao] [tinyint] NOT NULL CONSTRAINT [DF_Grupo_gru_situacao] DEFAULT ((1)),
[gru_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Grupo_gru_dataCriacao] DEFAULT (getdate()),
[gru_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Grupo_gru_dataAlteracao] DEFAULT (getdate()),
[vis_id] [int] NOT NULL,
[sis_id] [int] NOT NULL,
[gru_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_Grupo_gru_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Grupo] on [dbo].[SYS_Grupo]'
GO
ALTER TABLE [dbo].[SYS_Grupo] ADD CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED  ([gru_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Grupo_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Grupo_INSERT]
		@gru_nome VarChar (50)
		,@gru_situacao TinyInt
		,@gru_dataCriacao DateTime
		,@gru_dataAlteracao DateTime
		,@vis_id Int
		,@sis_id Int
		,@gru_integridade Int

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Grupo
		( 
			gru_nome
			,gru_situacao
			,gru_dataCriacao
			,gru_dataAlteracao
			,vis_id
			,sis_id
			,gru_integridade
 
		)
	OUTPUT inserted.gru_id INTO @ID
	VALUES
		( 
			@gru_nome
			,@gru_situacao
			,@gru_dataCriacao
			,@gru_dataAlteracao
			,@vis_id
			,@sis_id
			,@gru_integridade
 
		)

	SELECT ID FROM @ID


	DECLARE 
	@gru_id UNIQUEIDENTIFIER = (SELECT ID FROM @ID)


	--Insere as permissões de acordo com a visão e sistema do grupo
	INSERT SYS_GrupoPermissao
	SELECT 	
		@gru_id
		, @sis_id
		, SYS_VisaoModulo.mod_id
		, 0
		, 0
		, 0
		, 0
	FROM
		SYS_VisaoModulo WITH(NOLOCK)
	WHERE
		SYS_VisaoModulo.vis_id = @vis_id
		AND SYS_VisaoModulo.sis_id = @sis_id

			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Visao]'
GO
CREATE TABLE [dbo].[SYS_Visao]
(
[vis_id] [int] NOT NULL,
[vis_nome] [varchar] (50) NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Visao] on [dbo].[SYS_Visao]'
GO
ALTER TABLE [dbo].[SYS_Visao] ADD CONSTRAINT [PK_SYS_Visao] PRIMARY KEY CLUSTERED  ([vis_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UnidadeAdministrativa]'
GO
CREATE TABLE [dbo].[SYS_UnidadeAdministrativa]
(
[ent_id] [uniqueidentifier] NOT NULL,
[uad_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Unida__uad_i__3B75D760] DEFAULT (newsequentialid()),
[tua_id] [uniqueidentifier] NOT NULL,
[uad_codigo] [varchar] (20) NULL,
[uad_nome] [varchar] (200) NOT NULL,
[uad_sigla] [varchar] (50) NULL,
[uad_idSuperior] [uniqueidentifier] NULL,
[uad_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativa_uad_situacao] DEFAULT ((1)),
[uad_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativa_uad_dataCriacao] DEFAULT (getdate()),
[uad_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativa_uad_dataAlteracao] DEFAULT (getdate()),
[uad_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativa_uad_integridade] DEFAULT ((0)),
[uad_codigoIntegracao] [varchar] (50) NULL,
[uad_codigoInep] [varchar] (30) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UnidadeAdministrativa] on [dbo].[SYS_UnidadeAdministrativa]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativa] ADD CONSTRAINT [PK_SYS_UnidadeAdministrativa] PRIMARY KEY CLUSTERED  ([ent_id], [uad_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UnidadeAdminstrativa1] on [dbo].[SYS_UnidadeAdministrativa]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UnidadeAdminstrativa1] ON [dbo].[SYS_UnidadeAdministrativa] ([ent_id], [uad_situacao]) INCLUDE ([uad_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UnidadeAdministrativa_01] on [dbo].[SYS_UnidadeAdministrativa]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UnidadeAdministrativa_01] ON [dbo].[SYS_UnidadeAdministrativa] ([uad_id]) INCLUDE ([uad_nome]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UnidadeAdminstrativa_uad_idSuperior] on [dbo].[SYS_UnidadeAdministrativa]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UnidadeAdminstrativa_uad_idSuperior] ON [dbo].[SYS_UnidadeAdministrativa] ([uad_idSuperior]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_TipoUnidadeAdministrativa]'
GO
CREATE TABLE [dbo].[SYS_TipoUnidadeAdministrativa]
(
[tua_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_TipoU__tua_i__0425A276] DEFAULT (newsequentialid()),
[tua_nome] [varchar] (100) NOT NULL,
[tua_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_TipoUnidadeAdministrativa_tua_situacao] DEFAULT ((1)),
[tua_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoUnidadeAdministrativa_tua_dataCriacao] DEFAULT (getdate()),
[tua_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoUnidadeAdministrativa_tua_dataAlteracao] DEFAULT (getdate()),
[tua_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_TipoUnidadeAdministrativa_tua_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_TipoUnidadeAdministrativa] on [dbo].[SYS_TipoUnidadeAdministrativa]'
GO
ALTER TABLE [dbo].[SYS_TipoUnidadeAdministrativa] ADD CONSTRAINT [PK_SYS_TipoUnidadeAdministrativa] PRIMARY KEY CLUSTERED  ([tua_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Entidade]'
GO
CREATE TABLE [dbo].[SYS_Entidade]
(
[ent_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Entid__ent_i__276EDEB3] DEFAULT (newsequentialid()),
[ten_id] [uniqueidentifier] NOT NULL,
[ent_codigo] [varchar] (20) NULL,
[ent_nomeFantasia] [varchar] (200) NULL,
[ent_razaoSocial] [varchar] (200) NULL,
[ent_sigla] [varchar] (50) NULL,
[ent_cnpj] [varchar] (14) NULL,
[ent_inscricaoMunicipal] [varchar] (20) NULL,
[ent_inscricaoEstadual] [varchar] (20) NULL,
[ent_idSuperior] [uniqueidentifier] NULL,
[ent_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Entidade_ent_situacao] DEFAULT ((1)),
[ent_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Entidade_ent_dataCriacao] DEFAULT (getdate()),
[ent_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Entidade_ent_dataAlteracao] DEFAULT (getdate()),
[ent_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_Entidade_ent_integridade] DEFAULT ((0)),
[ent_urlAcesso] [varchar] (200) NULL,
[ent_exibeLogoCliente] [bit] NOT NULL CONSTRAINT [DF_SYS_Entidade_ent_exibeLogoCliente] DEFAULT ((0)),
[ent_logoCliente] [varchar] (2000) NULL,
[tep_id] [int] NULL,
[tpl_id] [int] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Entidade] on [dbo].[SYS_Entidade]'
GO
ALTER TABLE [dbo].[SYS_Entidade] ADD CONSTRAINT [PK_SYS_Entidade] PRIMARY KEY CLUSTERED  ([ent_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UsuarioGrupoUA]'
GO
CREATE TABLE [dbo].[SYS_UsuarioGrupoUA]
(
[usu_id] [uniqueidentifier] NOT NULL,
[gru_id] [uniqueidentifier] NOT NULL,
[ugu_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Usuar__ugu_i__38996AB5] DEFAULT (newsequentialid()),
[ent_id] [uniqueidentifier] NOT NULL,
[uad_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UsuarioGrupoUA_1] on [dbo].[SYS_UsuarioGrupoUA]'
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupoUA] ADD CONSTRAINT [PK_SYS_UsuarioGrupoUA_1] PRIMARY KEY CLUSTERED  ([usu_id], [gru_id], [ugu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UsuarioGrupoUA_02] on [dbo].[SYS_UsuarioGrupoUA]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UsuarioGrupoUA_02] ON [dbo].[SYS_UsuarioGrupoUA] ([gru_id]) INCLUDE ([ent_id], [uad_id], [ugu_id], [usu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UsuarioGrupoUA_01] on [dbo].[SYS_UsuarioGrupoUA]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UsuarioGrupoUA_01] ON [dbo].[SYS_UsuarioGrupoUA] ([usu_id], [gru_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Usuario]'
GO
CREATE TABLE [dbo].[SYS_Usuario]
(
[usu_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Usuar__usu_i__5441852A] DEFAULT (newsequentialid()),
[usu_login] [varchar] (500) NULL,
[usu_dominio] [varchar] (100) NULL,
[usu_email] [varchar] (500) NULL,
[usu_senha] [varchar] (256) NULL,
[usu_criptografia] [tinyint] NULL CONSTRAINT [DF_SYS_Usuario_usu_criptografia] DEFAULT ((1)),
[usu_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Usuario_usu_situacao] DEFAULT ((1)),
[usu_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Usuario_usu_dataCriacao] DEFAULT (getdate()),
[usu_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Usuario_usu_dataAlteracao] DEFAULT (getdate()),
[pes_id] [uniqueidentifier] NULL,
[usu_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_Usuario_usu_integridade] DEFAULT ((0)),
[ent_id] [uniqueidentifier] NOT NULL,
[usu_integracaoAD] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Usuario_usu_ integracaoAD] DEFAULT ((0)),
[usu_integracaoExterna] [tinyint] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Usuario] on [dbo].[SYS_Usuario]'
GO
ALTER TABLE [dbo].[SYS_Usuario] ADD CONSTRAINT [PK_SYS_Usuario] PRIMARY KEY CLUSTERED  ([usu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Usuario_03] on [dbo].[SYS_Usuario]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Usuario_03] ON [dbo].[SYS_Usuario] ([pes_id]) INCLUDE ([usu_id], [usu_login], [usu_situacao]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Usuario_2] on [dbo].[SYS_Usuario]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Usuario_2] ON [dbo].[SYS_Usuario] ([usu_situacao]) INCLUDE ([pes_id], [usu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Usuario_02] on [dbo].[SYS_Usuario]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Usuario_02] ON [dbo].[SYS_Usuario] ([usu_login]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Usuario_1] on [dbo].[SYS_Usuario]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Usuario_1] ON [dbo].[SYS_Usuario] ([usu_login], [ent_id], [usu_situacao]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Usuario_01] on [dbo].[SYS_Usuario]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Usuario_01] ON [dbo].[SYS_Usuario] ([usu_email]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[FN_Select_UAs_By_PermissaoUsuario]'
GO
-- =============================================
-- Author:		Carla Frascareli
-- Create date: 22/11/2010
-- Description:	Traz as Unidades Administrativas que o usuário tem acesso, 
--	pelo grupo do usuário e pela	visão.
-- 	Visões:
-- 	Administração
-- 		Vê todas as Uas.
-- 	Gestão
-- 		Vê as Uas que têm acesso pela tabela SYS_UsuarioGrupoUA e as filhas dessas Uas.
-- 	Unidade Administrativa
-- 		Vê somente as Uas que têm acesso pela tabela SYS_UsuarioGrupoUA.
-- =============================================
CREATE FUNCTION [dbo].[FN_Select_UAs_By_PermissaoUsuario]
(	
	@usu_id UNIQUEIDENTIFIER,
	@gru_id UNIQUEIDENTIFIER
)
RETURNS @ret TABLE 
(
	uad_id UNIQUEIDENTIFIER
)
AS
BEGIN
	DECLARE 
		@visao INT,
		@ent_id UNIQUEIDENTIFIER

	-- Visão do usuário.
	SET @visao = (SELECT TOP 1 vis_id FROM SYS_Grupo WITH(NOLOCK) WHERE gru_id = @gru_id);
	SET @ent_id = (SELECT ent_id FROM SYS_Usuario WITH (NOLOCK) WHERE usu_id = @usu_id);
	
	-- Administrador vê todas.
	IF (@visao = 1)
	BEGIN
		INSERT INTO @ret
		SELECT 
			uad_id
		FROM SYS_UnidadeAdministrativa WITH(NOLOCK)  
		WHERE 
			uad_situacao <> 3 
			AND ent_id = @ent_id
	END
	ELSE IF (@visao <> 4)
	BEGIN
		WITH UA_hierarquia AS  (
			-- UAs Pai que o usuário tem permissão.
			SELECT 
				uad_id
			FROM SYS_UnidadeAdministrativa WITH(NOLOCK)  
			WHERE 
				uad_situacao <> 3 
				AND ent_id = @ent_id
				AND 
				(
					uad_id IN 
					(
						SELECT 
							uad_id 
						FROM SYS_UsuarioGrupoUA WITH(NOLOCK)
						WHERE 
							usu_id = @usu_id
							AND gru_id = @gru_id
					)
				)
			UNION ALL	   
			 -- UAs da hierarquia.
			 SELECT 
				UA.uad_id
			 FROM SYS_UnidadeAdministrativa AS UA WITH(NOLOCK)
				 INNER JOIN UA_hierarquia UAH
				 ON UA.uad_idSuperior = UAH.uad_id 
			 WHERE 
				@visao <> 3 -- Visão de Unidade Administrativa, não vê as filhas.
				AND UA.uad_situacao <> 3
				AND ent_id = @ent_id
		)
	
		INSERT INTO @ret
		SELECT DISTINCT uad_id FROM UA_hierarquia
	END
	
	RETURN
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_UadSuperior_PermissaoUsuario]'
GO

-- ===================================================================================
-- Author:		João Victor
-- Create date: 17/06/2011 15:55
-- Description:	Retorna as Unidades Administrativas superiores pelos filtros informados,
--				pela entidade e pela permissão do usuário nas UAs e nas UAs Superiores
-- ===================================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_UadSuperior_PermissaoUsuario]
    @tua_id UNIQUEIDENTIFIER ,
    @ent_id UNIQUEIDENTIFIER ,
    @gru_id UNIQUEIDENTIFIER ,
    @usu_id UNIQUEIDENTIFIER ,
    @uad_idSuperior UNIQUEIDENTIFIER
AS 
    BEGIN
        SELECT  UA.tua_id ,
                uad_id ,
                uad_idSuperior ,
                uad_nome
        FROM    SYS_UnidadeAdministrativa UA WITH ( NOLOCK )
                INNER JOIN SYS_Entidade Ent WITH ( NOLOCK ) ON Ent.ent_id = UA.ent_id
                INNER JOIN SYS_TipoUnidadeAdministrativa Tua WITH ( NOLOCK ) ON UA.tua_id = Tua.tua_id
        WHERE   uad_situacao <> 3
                AND ent_situacao <> 3
                AND tua_situacao <> 3
                AND ( @tua_id IS NULL
                      OR UA.tua_id = @tua_id
                    )
                AND ( ( @uad_idSuperior IS NULL
                        AND uad_idSuperior IS NULL
                      )
                      OR @uad_idSuperior = ua.uad_idSuperior
                    )
		-- Somente da Entidade informada.
                AND ( UA.ent_id = @ent_id )
			
		-- Filtra as Unidades Administrativas que o usuário tem permissão.
                AND ( UA.uad_id IN (
                      SELECT    uad_id
                      FROM      FN_Select_UAs_By_PermissaoUsuario(@usu_id,
                                                              @gru_id) )
                      OR ( SELECT   vis_nome
                           FROM     SYS_Grupo g WITH ( NOLOCK )
                                    INNER JOIN SYS_Visao v WITH ( NOLOCK ) ON g.vis_id = v.vis_id
                           WHERE    gru_id = @gru_id
                         ) = 'Individual'
                    )
		
		--AND (Exists (select ua1.uad_id from SYS_UnidadeAdministrativa ua1 with(nolock) where ua.uad_id = ua1.uad_idSuperior))
        ORDER BY uad_nome
		
        SELECT  @@ROWCOUNT				
    END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaContato_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:21
-- Description:	Altera o contato da unidade administrativa
--              preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaContato_UPDATE]
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uac_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@uac_contato VarChar (200)
		,@uac_situacao TinyInt
		,@uac_dataAlteracao DateTime

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaContato
	SET 		
		tmc_id = @tmc_id
		,uac_contato = @uac_contato
		,uac_situacao = @uac_situacao		
		,uac_dataAlteracao = @uac_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND	uac_id = @uac_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_AtualizaEmail]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 22/04/2015
-- Description:	Atualiza o email do usuário.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_AtualizaEmail] 
	@usu_id UNIQUEIDENTIFIER, 
	@usu_email VARCHAR(500),
	@usu_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_Usuario
	SET
	    usu_email = @usu_email, 
	    usu_dataAlteracao = @usu_dataAlteracao
	WHERE
		usu_id = @usu_id

	RETURN ISNULL(@@ROWCOUNT, 0);
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_Pessoa]'
GO
CREATE TABLE [dbo].[PES_Pessoa]
(
[pes_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_Pesso__pes_i__47DBAE45] DEFAULT (newsequentialid()),
[pes_nome] [varchar] (200) NOT NULL,
[pes_nome_abreviado] [varchar] (50) NULL,
[pai_idNacionalidade] [uniqueidentifier] NULL,
[pes_naturalizado] [bit] NULL,
[cid_idNaturalidade] [uniqueidentifier] NULL,
[pes_dataNascimento] [date] NULL,
[pes_estadoCivil] [tinyint] NULL,
[pes_racaCor] [tinyint] NULL,
[pes_sexo] [tinyint] NULL,
[pes_idFiliacaoPai] [uniqueidentifier] NULL,
[pes_idFiliacaoMae] [uniqueidentifier] NULL,
[tes_id] [uniqueidentifier] NULL,
[pes_foto] [varbinary] (max) NULL,
[pes_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_Pessoa_pes_situacao] DEFAULT ((1)),
[pes_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_Pessoa_pes_dataCriacao] DEFAULT (getdate()),
[pes_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_Pessoa_pes_dataAlteracao] DEFAULT (getdate()),
[pes_integridade] [int] NOT NULL CONSTRAINT [DF__PES_Pesso__pes_i__2942188C] DEFAULT ((0)),
[arq_idFoto] [bigint] NULL,
[pes_nomeSocial] [varchar] (200) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_Pessoa] on [dbo].[PES_Pessoa]'
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [PK_PES_Pessoa] PRIMARY KEY CLUSTERED  ([pes_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_Pessoa_04] on [dbo].[PES_Pessoa]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_Pessoa_04] ON [dbo].[PES_Pessoa] ([pes_dataNascimento]) INCLUDE ([pes_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_Pessoa_03] on [dbo].[PES_Pessoa]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_Pessoa_03] ON [dbo].[PES_Pessoa] ([pes_nome]) INCLUDE ([pes_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_1_PES_Pessoa] on [dbo].[PES_Pessoa]'
GO
CREATE NONCLUSTERED INDEX [IX_1_PES_Pessoa] ON [dbo].[PES_Pessoa] ([pes_situacao]) INCLUDE ([pes_dataNascimento], [pes_id], [pes_idFiliacaoMae], [pes_idFiliacaoPai], [pes_nome]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_Pessoa_02] on [dbo].[PES_Pessoa]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_Pessoa_02] ON [dbo].[PES_Pessoa] ([pes_situacao]) INCLUDE ([pes_id], [pes_nome], [pes_nome_abreviado]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_INSERT]'
GO

CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_INSERT]
	  @pes_id				UNIQUEIDENTIFIER = NULL
	, @pes_nome 			VARCHAR (200)
	, @pes_nome_abreviado 	VARCHAR (50)
	, @pai_idNacionalidade	UNIQUEIDENTIFIER
	, @pes_naturalizado 	BIT
	, @cid_idNaturalidade	UNIQUEIDENTIFIER
	, @pes_dataNascimento 	DATETIME
	, @pes_estadoCivil		TINYINT
	, @pes_racaCor			TINYINT
	, @pes_sexo 			TINYINT
	, @pes_idFiliacaoPai 	UNIQUEIDENTIFIER
	, @pes_idFiliacaoMae	UNIQUEIDENTIFIER
	, @tes_id				UNIQUEIDENTIFIER
	, @pes_foto				VARBINARY(MAX)  = NULL
	, @pes_situacao 		TINYINT
	, @pes_dataCriacao		DATETIME
	, @pes_dataAlteracao	DATETIME
	, @pes_integridade		INT
	, @arq_idFoto			BIGINT = NULL
	, @pes_nomeSocial		VARCHAR(200) = NULL

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	
	INSERT INTO PES_Pessoa
	( 
		  pes_id 
		, pes_nome 
		, pes_nome_abreviado 
		, pai_idNacionalidade 
		, pes_naturalizado 
		, cid_idNaturalidade 
		, pes_dataNascimento 
		, pes_estadoCivil 
		, pes_racaCor 
		, pes_sexo 
		, pes_idFiliacaoPai 
		, pes_idFiliacaoMae 
		, tes_id 
		, pes_foto 
		, pes_situacao 
		, pes_dataCriacao 
		, pes_dataAlteracao 
		, pes_integridade 
		, arq_idFoto 
		, pes_nomeSocial
 	)
	
	OUTPUT inserted.pes_id INTO @ID
	
	VALUES
	( 
		  ISNULL(@pes_id, NEWID())
		, @pes_nome 
		, @pes_nome_abreviado 
		, @pai_idNacionalidade 
		, @pes_naturalizado 
		, @cid_idNaturalidade 
		, @pes_dataNascimento 
		, @pes_estadoCivil 
		, @pes_racaCor 
		, @pes_sexo 
		, @pes_idFiliacaoPai 
		, @pes_idFiliacaoMae 
		, @tes_id 
		, @pes_foto 
		, @pes_situacao 
		, @pes_dataCriacao 
		, @pes_dataAlteracao 
		, @pes_integridade 
		, @arq_idFoto
		, @pes_nomeSocial
 	)
		
	SELECT ID FROM @ID
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_UsuarioAPI_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_UsuarioAPI_INSERT]
	@uap_username VarChar (100)
	, @uap_password VarChar (256)
	, @uap_situacao TinyInt
	, @uap_dataCriacao DateTime
	, @uap_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		CFG_UsuarioAPI
		( 
			uap_username 
			, uap_password 
			, uap_situacao 
			, uap_dataCriacao 
			, uap_dataAlteracao 
 
		)
	VALUES
		( 
			@uap_username 
			, @uap_password 
			, @uap_situacao 
			, @uap_dataCriacao 
			, @uap_dataAlteracao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Grupo_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Grupo_DELETE]
	@gru_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_Grupo
	WHERE 
		gru_id = @gru_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_PessoaDocumento]'
GO
CREATE TABLE [dbo].[PES_PessoaDocumento]
(
[pes_id] [uniqueidentifier] NOT NULL,
[tdo_id] [uniqueidentifier] NOT NULL,
[psd_numero] [varchar] (50) NOT NULL,
[psd_dataEmissao] [date] NULL,
[psd_orgaoEmissao] [varchar] (200) NULL,
[unf_idEmissao] [uniqueidentifier] NULL,
[psd_infoComplementares] [varchar] (1000) NULL,
[psd_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_PessoaDocumento_psd_situacao] DEFAULT ((1)),
[psd_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaDocumento_psd_dataCriacao] DEFAULT (getdate()),
[psd_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaDocumento_psd_dataAlteracao] DEFAULT (getdate()),
[psd_categoria] [varchar] (64) NULL,
[psd_classificacao] [varchar] (64) NULL,
[psd_csm] [varchar] (32) NULL,
[psd_dataEntrada] [date] NULL,
[psd_dataValidade] [date] NULL,
[pai_idOrigem] [uniqueidentifier] NULL,
[psd_serie] [varchar] (32) NULL,
[psd_tipoGuarda] [varchar] (128) NULL,
[psd_via] [varchar] (16) NULL,
[psd_secao] [varchar] (32) NULL,
[psd_zona] [varchar] (16) NULL,
[psd_regiaoMilitar] [varchar] (16) NULL,
[psd_numeroRA] [varchar] (64) NULL,
[psd_dataExpedicao] [date] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_PessoaDocumento] on [dbo].[PES_PessoaDocumento]'
GO
ALTER TABLE [dbo].[PES_PessoaDocumento] ADD CONSTRAINT [PK_PES_PessoaDocumento] PRIMARY KEY CLUSTERED  ([pes_id], [tdo_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_PessoaDocumento_03] on [dbo].[PES_PessoaDocumento]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_PessoaDocumento_03] ON [dbo].[PES_PessoaDocumento] ([psd_situacao]) INCLUDE ([pes_id], [psd_numero], [tdo_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_PessoaDocumento_02] on [dbo].[PES_PessoaDocumento]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_PessoaDocumento_02] ON [dbo].[PES_PessoaDocumento] ([tdo_id], [psd_situacao]) INCLUDE ([pes_id], [psd_numero]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_PES_PessoaDocumento_01] on [dbo].[PES_PessoaDocumento]'
GO
CREATE NONCLUSTERED INDEX [IX_PES_PessoaDocumento_01] ON [dbo].[PES_PessoaDocumento] ([psd_numero], [tdo_id], [psd_situacao]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_SelectBy_Documento]'
GO
-- ========================================================================
-- Author:		Carla Frascareli
-- Create date: 07/07/2011
-- Description:	Retorna as pessoas encontradas que tenham o mesmo documento
--				do tipo informado
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_SelectBy_Documento]	
	@psd_numero VARCHAR(50)
	, @tdo_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		Pes.pes_id
		, Pes.cid_idNaturalidade
		, Pes.pai_idNacionalidade
		, Pes.pes_dataAlteracao
		, Pes.pes_dataCriacao
		, Pes.pes_dataNascimento
		, Pes.pes_estadoCivil
		, Pes.pes_foto
		, Pes.pes_idFiliacaoMae
		, Pes.pes_idFiliacaoPai
		, Pes.pes_integridade
		, Pes.pes_naturalizado
		, Pes.pes_nome
		, Pes.pes_nome_abreviado
		, Pes.pes_racaCor
		, Pes.pes_sexo
		, Pes.tes_id
		, Pes.pes_situacao
	FROM
		PES_PessoaDocumento	Psd WITH (NOLOCK)
	INNER JOIN PES_Pessoa Pes WITH (NOLOCK)
		ON (Pes.pes_id = Psd.pes_id)
	WHERE		
		Psd.psd_situacao <> 3
		AND Pes.pes_situacao <> 3
		AND Psd.psd_numero = @psd_numero
		AND Psd.tdo_id = @tdo_id
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_CopiaPermissao]'
GO
-- =============================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 05/01/2010 11:00
-- Description:	Copia permissões de um grupo.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_CopiaPermissao]
	@gru_id uniqueidentifier
	, @sis_id int
	, @gru_idOld uniqueidentifier	
AS
BEGIN
	--Deleta as permissões geradas pela trigger
	DELETE FROM SYS_GrupoPermissao
	WHERE gru_id = @gru_id
		AND sis_id = @sis_id
		
	--Copia as permissões do grupo antigo para o grupo novo.	
	INSERT SYS_GrupoPermissao
	SELECT 	
		@gru_id
		, @sis_id
		, SYS_GrupoPermissao.mod_id
		, SYS_GrupoPermissao.grp_consultar
		, SYS_GrupoPermissao.grp_inserir
		, SYS_GrupoPermissao.grp_alterar
		, SYS_GrupoPermissao.grp_excluir
	FROM
		SYS_GrupoPermissao WITH(NOLOCK)
	WHERE
		SYS_GrupoPermissao.gru_id = @gru_idOld
		AND SYS_GrupoPermissao.sis_id = @sis_id	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_UsuarioAPI_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_UsuarioAPI_UPDATE]
	@uap_id INT
	, @uap_username VARCHAR (100)
	, @uap_password VARCHAR (256)
	, @uap_situacao TINYINT
	, @uap_dataCriacao DATETIME
	, @uap_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_UsuarioAPI 
	SET 
		uap_username = @uap_username 
		, uap_password = @uap_password 
		, uap_situacao = @uap_situacao 
		, uap_dataCriacao = @uap_dataCriacao 
		, uap_dataAlteracao = @uap_dataAlteracao 

	WHERE 
		uap_id = @uap_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UsuarioGrupo]'
GO
CREATE TABLE [dbo].[SYS_UsuarioGrupo]
(
[usu_id] [uniqueidentifier] NOT NULL,
[gru_id] [uniqueidentifier] NOT NULL,
[usg_situacao] [tinyint] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UsuarioGrupo] on [dbo].[SYS_UsuarioGrupo]'
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupo] ADD CONSTRAINT [PK_SYS_UsuarioGrupo] PRIMARY KEY CLUSTERED  ([usu_id], [gru_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UsuarioGrupo_01] on [dbo].[SYS_UsuarioGrupo]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UsuarioGrupo_01] ON [dbo].[SYS_UsuarioGrupo] ([usu_id], [usg_situacao], [gru_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_UPDATEBy_Situacao]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 22/01/2010 20:00
-- Description:	Realização alterações na situação como por exemplo
--				exclusão lógica
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_UPDATEBy_Situacao]
		@gru_id uniqueidentifier
		,@gru_situacao TinyInt
		,@gru_dataAlteracao DateTime
AS
BEGIN	
	--Exclui logicamente os usuários do grupo
	UPDATE SYS_UsuarioGrupo 
	SET 
		usg_situacao = @gru_situacao		
	WHERE 
		gru_id = @gru_id
		
	--Exclui logicamente o grupo
	UPDATE SYS_Grupo 
	SET 
		gru_situacao = @gru_situacao
		,gru_dataAlteracao = @gru_dataAlteracao
	WHERE 
		gru_id = @gru_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUsuario]'
GO
-- =========================================================================
-- Author:		Paula Fiorini
-- Create date: 06/07/2011 
-- Description:	Utilizada na pesquisa de unidades administrativas filtrando 
--				pela permissão do usuário.
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUsuario]
	@gru_id uniqueidentifier
	, @usu_id uniqueidentifier	
	, @tua_id uniqueidentifier
	, @ent_id uniqueidentifier
	, @uad_id uniqueidentifier
	, @uad_nome VARCHAR(200)
	, @uad_codigo VARCHAR(20)
	, @uad_situacao TINYINT
AS
BEGIN

	DECLARE @TbUas TABLE (uad_id UNIQUEIDENTIFIER NOT NULL);

	INSERT INTO @TbUas (uad_id)
	SELECT uad_id FROM FN_Select_UAs_By_PermissaoUsuario(@usu_id, @gru_id)

	SELECT
		SYS_UnidadeAdministrativa.ent_id
		, SYS_Entidade.ent_razaoSocial
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_codigo
		, SYS_UnidadeAdministrativa.uad_nome
		, SYS_TipoUnidadeAdministrativa.tua_id
		, SYS_TipoUnidadeAdministrativa.tua_nome		
		, (SELECT uad_nome FROM SYS_UnidadeAdministrativa A  WITH (NOLOCK) WHERE A.ent_id = SYS_UnidadeAdministrativa.ent_id AND  A.uad_id = SYS_UnidadeAdministrativa.uad_idSuperior) AS uad_nomeSup
		, CASE SYS_UnidadeAdministrativa.uad_situacao
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS uad_bloqueado
		  ,uad_codigoInep
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)
	INNER JOIN SYS_Entidade WITH (NOLOCK)
		ON SYS_Entidade.ent_id = SYS_UnidadeAdministrativa.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UnidadeAdministrativa.tua_id = SYS_TipoUnidadeAdministrativa.tua_id	
	WHERE
		uad_situacao <> 3		
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND ((@tua_id IS NULL) OR (SYS_UnidadeAdministrativa.tua_id = @tua_id))
		AND ((@ent_id IS NULL) OR (SYS_UnidadeAdministrativa.ent_id = @ent_id))
		AND ((@uad_id IS NULL) OR (SYS_UnidadeAdministrativa.uad_id <> @uad_id))
		AND ((@uad_nome IS NULL) OR (SYS_UnidadeAdministrativa.uad_nome LIKE '%' + @uad_nome + '%'))
		AND ((@uad_codigo IS NULL) OR (SYS_UnidadeAdministrativa.uad_codigo LIKE '%' + @uad_codigo + '%'))
		AND ((@uad_situacao IS NULL) OR (SYS_UnidadeAdministrativa.uad_situacao = @uad_situacao))			
		AND (((@gru_id IS NULL) AND (@usu_id IS NULL)) OR (uad_id IN (SELECT uad_id FROM @TbUas)))
	ORDER BY 
		SYS_UnidadeAdministrativa.uad_nome
		
	SELECT @@ROWCOUNT						
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_TipoMeioContato]'
GO
CREATE TABLE [dbo].[SYS_TipoMeioContato]
(
[tmc_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_TipoM__tmc_i__07020F21] DEFAULT (newsequentialid()),
[tmc_nome] [varchar] (100) NOT NULL,
[tmc_validacao] [tinyint] NULL,
[tmc_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_TipoMeioContato_tmc_situacao] DEFAULT ((1)),
[tmc_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoMeioContato_tmc_dataCriacao] DEFAULT (getdate()),
[tmc_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoMeioContato_tmc_dataAlteracao] DEFAULT (getdate()),
[tmc_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_TipoMeioContato_tmc_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_TipoMeioContato] on [dbo].[SYS_TipoMeioContato]'
GO
ALTER TABLE [dbo].[SYS_TipoMeioContato] ADD CONSTRAINT [PK_SYS_TipoMeioContato] PRIMARY KEY CLUSTERED  ([tmc_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaContato_SelectBy_ent_id_uad_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/01/2010 16:45
-- Description:	utilizado na busca de contatos de UA, retorna os contatos
--              da unidade administrativa
--				filtrados por:
--					ent_id, uad_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaContato_SelectBy_ent_id_uad_id]	
	@ent_id uniqueidentifier
	,@uad_id uniqueidentifier
AS
BEGIN
	SELECT		
		uac_id as id
		,SYS_UnidadeAdministrativaContato.tmc_id
		,tmc_nome
		,uac_contato as contato		
	FROM
		SYS_UnidadeAdministrativaContato WITH (NOLOCK)
	INNER JOIN
		SYS_TipoMeioContato WITH (NOLOCK) on SYS_TipoMeioContato.tmc_id = SYS_UnidadeAdministrativaContato.tmc_id
	WHERE		
		uac_situacao <> 3
		AND tmc_situacao <> 3
		AND ent_id = @ent_id
		AND uad_id = @uad_id
	ORDER BY
		tmc_nome, uac_contato
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_CopiaUsuarios]'
GO
-- =============================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 05/01/2010 11:10
-- Description:	Copia permissões de um grupo.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_CopiaUsuarios]
	@gru_id uniqueidentifier	
	, @gru_idOld uniqueidentifier	
AS
BEGIN	
	--Copia os usuários do grupo antigo para o grupo novo.	
	INSERT SYS_UsuarioGrupo
	SELECT 					
		SYS_UsuarioGrupo.usu_id		
		, @gru_id
		, 1
	FROM
		SYS_UsuarioGrupo WITH(NOLOCK)
	WHERE
		SYS_UsuarioGrupo.gru_id = @gru_idOld
		
	--Copia as UA's dos usuários do grupo antigo para o grupo novo.	
	INSERT SYS_UsuarioGrupoUA
	SELECT 					
		SYS_UsuarioGrupoUA.usu_id		
		, @gru_id
		, SYS_UsuarioGrupoUA.ugu_id
		, SYS_UsuarioGrupoUA.ent_id
		, SYS_UsuarioGrupoUA.uad_id
	FROM
		SYS_UsuarioGrupoUA WITH(NOLOCK)
	WHERE
		SYS_UsuarioGrupoUA.gru_id = @gru_idOld				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_UsuarioAPI_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_UsuarioAPI_DELETE]
	@uap_id INT

AS
BEGIN
	DELETE FROM 
		CFG_UsuarioAPI 
	WHERE 
		uap_id = @uap_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_UPDATE]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 21/01/2010 20:00
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_UPDATE]
		@gru_id uniqueidentifier
		,@gru_nome VarChar (50)
		,@gru_situacao TinyInt
		,@gru_dataAlteracao DateTime
		,@vis_id Int
		,@sis_id Int
AS
BEGIN

	-- BUSCA VALORES ANTIGOS PARA COMPARAÇÃO (trigger)
	DECLARE 
		@sis_idOld INT = (SELECT sis_id FROM SYS_Grupo WHERE gru_id = @gru_id)
		, @vis_idOld INT = (SELECT vis_id FROM SYS_Grupo WHERE gru_id = @gru_id)

	-- ATUALIZA SYS_GRUPO
	UPDATE SYS_Grupo 
	SET 
		gru_nome = @gru_nome
		,gru_situacao = @gru_situacao
		,gru_dataAlteracao = @gru_dataAlteracao
		,vis_id = @vis_id
		,sis_id = @sis_id
	WHERE 
		gru_id = @gru_id
		
	-- RETORNA STATUS DO UPDATE
	RETURN ISNULL(@@ROWCOUNT,-1)

	/*Trigger*/
    -- se alterar a sistema do grupo
	IF (@sis_idOld <> @sis_id) BEGIN

		--Apaga todos os módulo do sistema
		DELETE FROM [dbo].SYS_GrupoPermissao 
		WHERE 
			    gru_id = @gru_id
			AND sis_id = @sis_id

		--Insere as permissões de acordo com a visão e sistema do grupo
		INSERT INTO SYS_GrupoPermissao ( gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir )
		SELECT 	
			  @gru_id
			, @sis_id
			, vm.mod_id
			, 0
			, 0
			, 0
			, 0
		FROM
			[dbo].SYS_VisaoModulo vm WITH (NOLOCK)
		WHERE
			    vm.vis_id = @vis_id
			AND vm.sis_id = @sis_id
	END

	-- se alterar a visao do grupo
	IF (@vis_id <> @vis_idOld) BEGIN	

		--Apaga todas as permissões que não pertencem a nova visão			
		DELETE FROM [dbo].SYS_GrupoPermissao 
		WHERE 
			    gru_id = @gru_id
			AND sis_id = @sis_id
			AND mod_id IN ( 
                            SELECT  gp2.mod_id 
					        FROM    [dbo].SYS_GrupoPermissao gp2 WITH (NOLOCK)
					        WHERE 
                                        gp2.gru_id = @gru_id
						            AND gp2.sis_id = @sis_id
						            AND NOT EXISTS (
                                                        SELECT  vm2.mod_id 
							                            FROM    SYS_VisaoModulo vm2 WITH (NOLOCK)
							                            WHERE   
								                                vis_id = @vis_id            
								                            AND vm2.sis_id = gp2.sis_id
								                            AND vm2.mod_id = gp2.mod_id
						                            )
			              )

		-- Insere as permissoes que o grupo não tinha anteriormente
		INSERT INTO [dbo].SYS_GrupoPermissao ( gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir )
		SELECT 	
			  @gru_id
			, @sis_id
			, vm.mod_id
			, 0
			, 0
			, 0
			, 0
		FROM
			SYS_VisaoModulo vm WITH (NOLOCK)
		WHERE 
			    vm.vis_id = @vis_id
			AND vm.sis_id = @sis_id
			AND NOT EXISTS 
                            ( 
                                SELECT  gp.mod_id 
				                FROM    SYS_GrupoPermissao gp WITH (NOLOCK)
				                WHERE 
					                    gp.gru_id = @gru_id 
					                AND gp.sis_id = @sis_id
					                AND gp.mod_id = vm.mod_id
			                )
	END



END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_CertidaoCivil]'
GO
CREATE TABLE [dbo].[PES_CertidaoCivil]
(
[pes_id] [uniqueidentifier] NOT NULL,
[ctc_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_Certi__ctc_i__59FA5E80] DEFAULT (newsequentialid()),
[ctc_tipo] [tinyint] NOT NULL,
[ctc_numeroTermo] [varchar] (50) NULL,
[ctc_folha] [varchar] (20) NULL,
[ctc_livro] [varchar] (20) NULL,
[ctc_dataEmissao] [date] NULL,
[ctc_nomeCartorio] [varchar] (200) NULL,
[cid_idCartorio] [uniqueidentifier] NULL,
[unf_idCartorio] [uniqueidentifier] NULL,
[ctc_distritoCartorio] [varchar] (100) NULL,
[ctc_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_CertidaoCivil_ctc_situacao] DEFAULT ((1)),
[ctc_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_CertidaoCivil_ctc_dataCriacao] DEFAULT (getdate()),
[ctc_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_CertidaoCivil_ctc_dataAlteracao] DEFAULT (getdate()),
[ctc_matricula] [varchar] (32) NULL,
[ctc_gemeo] [bit] NOT NULL CONSTRAINT [DF_PES_CertidaoCivil_ctc_gemeo] DEFAULT ((0)),
[ctc_modeloNovo] [bit] NOT NULL CONSTRAINT [DF_PES_CertidaoCivil_ctc_modelonovo] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_CertidaoCivil] on [dbo].[PES_CertidaoCivil]'
GO
ALTER TABLE [dbo].[PES_CertidaoCivil] ADD CONSTRAINT [PK_PES_CertidaoCivil] PRIMARY KEY CLUSTERED  ([pes_id], [ctc_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_CertidaoCivil_SelectBy_Pessoa]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 15/07/2011 19:00
-- Description:	utilizado na busca de certidoes da pessoa, retorna as certidoes
--              da pessoa filtrados por: pes_id
-- Data de alteraçao: 24/06/2011
--
-- Data de alteraçao: 24/09/2012 - Daniel Jun Suguimoto
-- Description: incluido o campo ctc_matricula nos dados que serao retornados
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_CertidaoCivil_SelectBy_Pessoa]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT
		pes_id
		, ctc_id
		, ctc_tipo
		, ctc_numeroTermo
		, ctc_folha
		, ctc_livro
		, ctc_dataEmissao
		, ctc_nomeCartorio
		, ISNULL(cid_idCartorio,'00000000-0000-0000-0000-000000000000') AS cid_idCartorio		
		, ISNULL(unf_idCartorio,'00000000-0000-0000-0000-000000000000') AS unf_idCartorio	
		, ctc_distritoCartorio	
		, ctc_situacao
		, ctc_dataCriacao
		, ctc_dataAlteracao
		, ctc_matricula
		, ctc_gemeo 
		, ctc_modeloNovo 
	FROM
		PES_CertidaoCivil WITH (NOLOCK)
	LEFT JOIN END_Cidade WITH (NOLOCK)
		ON END_Cidade.cid_id = PES_CertidaoCivil.cid_idCartorio
		AND cid_situacao <> 3
	WHERE		
		ctc_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		ctc_tipo
		
	SELECT @@ROWCOUNT	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_Pessoa_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_Pessoa_UPDATE]
	@pes_id UNIQUEIDENTIFIER
	, @pes_nome VARCHAR (200)
	, @pes_nome_abreviado VARCHAR (50)
	, @pai_idNacionalidade UNIQUEIDENTIFIER
	, @pes_naturalizado BIT
	, @cid_idNaturalidade UNIQUEIDENTIFIER
	, @pes_dataNascimento DATETIME
	, @pes_estadoCivil TINYINT
	, @pes_racaCor TINYINT
	, @pes_sexo TINYINT
	, @pes_idFiliacaoPai UNIQUEIDENTIFIER
	, @pes_idFiliacaoMae UNIQUEIDENTIFIER
	, @tes_id UNIQUEIDENTIFIER
	, @pes_foto varbinary(max)  = NULL
	, @pes_situacao TINYINT
	, @pes_dataCriacao DATETIME
	, @pes_dataAlteracao DATETIME
	, @pes_integridade INT
	, @arq_idFoto BIGINT = NULL

AS
BEGIN
	UPDATE PES_Pessoa 
	SET 
		pes_nome = @pes_nome 
		, pes_nome_abreviado = @pes_nome_abreviado 
		, pai_idNacionalidade = @pai_idNacionalidade 
		, pes_naturalizado = @pes_naturalizado 
		, cid_idNaturalidade = @cid_idNaturalidade 
		, pes_dataNascimento = @pes_dataNascimento 
		, pes_estadoCivil = @pes_estadoCivil 
		, pes_racaCor = @pes_racaCor 
		, pes_sexo = @pes_sexo 
		, pes_idFiliacaoPai = @pes_idFiliacaoPai 
		, pes_idFiliacaoMae = @pes_idFiliacaoMae 
		, tes_id = @tes_id 
		, pes_foto = @pes_foto 
		, pes_situacao = @pes_situacao 
		, pes_dataCriacao = @pes_dataCriacao 
		, pes_dataAlteracao = @pes_dataAlteracao 
		, pes_integridade = @pes_integridade 
		, arq_idFoto = @arq_idFoto 

	WHERE 
		pes_id = @pes_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_TipoEscolaridade]'
GO
CREATE TABLE [dbo].[PES_TipoEscolaridade]
(
[tes_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_TipoE__tes_i__1920BF5C] DEFAULT (newsequentialid()),
[tes_nome] [varchar] (100) NOT NULL,
[tes_ordem] [int] NOT NULL,
[tes_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_TipoEscolaridade_tes_situacao] DEFAULT ((1)),
[tes_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_TipoEscolaridade_tes_dataCriacao] DEFAULT (getdate()),
[tes_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_TipoEscolaridade_tes_dataAlteracao] DEFAULT (getdate()),
[tes_integridade] [int] NOT NULL CONSTRAINT [DF_PES_TipoEscolaridade_tes_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_TipoEscolaridade] on [dbo].[PES_TipoEscolaridade]'
GO
ALTER TABLE [dbo].[PES_TipoEscolaridade] ADD CONSTRAINT [PK_PES_TipoEscolaridade] PRIMARY KEY CLUSTERED  ([tes_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:43
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Escolaridade. Filtrada por: 
--					tes_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_Update_Situacao]	
		@tes_id uniqueidentifier
		,@tes_situacao TINYINT
		,@tes_dataAlteracao DateTime
AS
BEGIN
	UPDATE PES_TipoEscolaridade
	SET 
		tes_situacao = @tes_situacao
		,tes_dataAlteracao = @tes_dataAlteracao
	WHERE 
		tes_id = @tes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_UsuarioAPI_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_UsuarioAPI_SELECT]
	
AS
BEGIN
	SELECT 
		uap_id
		,uap_username
		,uap_password
		,uap_situacao
		,uap_dataCriacao
		,uap_dataAlteracao

	FROM 
		CFG_UsuarioAPI WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SelectBy_gru_nome]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 25/03/2010 12:30
-- Description:	utilizado no cadastro de grupos,
--              para saber se o grupo já está cadastrado
--				filtrados por:
--					grupo (diferente do parametro), 					 
--                  nome, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SelectBy_gru_nome]	
	@gru_id uniqueidentifier
	, @sis_id INT
	, @gru_nome VARCHAR(200)	
	, @gru_situacao TINYINT		
AS
BEGIN
	SELECT 
		gru_id
	FROM
		SYS_Grupo WITH (NOLOCK)		
	WHERE
		gru_situacao <> 3
		AND (@gru_id is null or gru_id <> @gru_id)								
		AND (@sis_id is null or sis_id = @sis_id)	
		AND (@gru_nome is null or gru_nome = @gru_nome)			
		AND (@gru_situacao is null or gru_situacao = @gru_situacao)						
	ORDER BY
		gru_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_SelectBy_tmc_nome]'
GO
-- ===================================================================
-- Author:		Paula Fiorini
-- Create date: 20/07/201
-- Description:	Utilizado na busca do tipo de meio de contato
--				com nome igual ao do parâmetro e que não esteja
--				excluído logicamente.
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_SelectBy_tmc_nome]	
	@tmc_nome VARCHAR(100)	
	
AS
BEGIN
	SELECT TOP 1
		tmc_id
		,tmc_nome
		,tmc_validacao
		,tmc_situacao
		,tmc_dataCriacao
		,tmc_dataAlteracao
		,tmc_integridade
	FROM
		SYS_TipoMeioContato WITH (NOLOCK)
	WHERE
		tmc_situacao <> 3
		AND UPPER(tmc_nome) = UPPER(@tmc_nome)	
	ORDER BY
		tmc_nome
		
	SELECT @@ROWCOUNT		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_VisaoModuloMenu]'
GO
CREATE TABLE [dbo].[SYS_VisaoModuloMenu]
(
[vis_id] [int] NOT NULL,
[sis_id] [int] NOT NULL,
[mod_id] [int] NOT NULL,
[msm_id] [int] NOT NULL,
[vmm_ordem] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_ModuloVisaoMenu] on [dbo].[SYS_VisaoModuloMenu]'
GO
ALTER TABLE [dbo].[SYS_VisaoModuloMenu] ADD CONSTRAINT [PK_SYS_ModuloVisaoMenu] PRIMARY KEY CLUSTERED  ([vis_id], [sis_id], [mod_id], [msm_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Sistema]'
GO
CREATE TABLE [dbo].[SYS_Sistema]
(
[sis_id] [int] NOT NULL,
[sis_nome] [varchar] (100) NOT NULL,
[sis_descricao] [text] NULL,
[sis_caminho] [varchar] (2000) NULL,
[sis_urlImagem] [varchar] (2000) NULL,
[sis_urlLogoCabecalho] [varchar] (2000) NULL,
[sis_tipoAutenticacao] [tinyint] NULL CONSTRAINT [DF_SYS_Sistema_sis_tipoAutenticacao] DEFAULT ((1)),
[sis_urlIntegracao] [varchar] (200) NULL,
[sis_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Sistemas_sis_situacao] DEFAULT ((1)),
[sis_caminhoLogout] [varchar] (2000) NULL,
[sis_ocultarLogo] [bit] NOT NULL CONSTRAINT [DF_SYS_Sistema_sis_ocultarLogo] DEFAULT ((0))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Sistemas] on [dbo].[SYS_Sistema]'
GO
ALTER TABLE [dbo].[SYS_Sistema] ADD CONSTRAINT [PK_SYS_Sistemas] PRIMARY KEY CLUSTERED  ([sis_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_ModuloSiteMap]'
GO
CREATE TABLE [dbo].[SYS_ModuloSiteMap]
(
[sis_id] [int] NOT NULL,
[mod_id] [int] NOT NULL,
[msm_id] [int] NOT NULL,
[msm_nome] [varchar] (50) NOT NULL,
[msm_descricao] [varchar] (1000) NULL,
[msm_url] [varchar] (500) NULL,
[msm_informacoes] [text] NULL,
[msm_urlHelp] [varchar] (500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_ModuloSiteMap] on [dbo].[SYS_ModuloSiteMap]'
GO
ALTER TABLE [dbo].[SYS_ModuloSiteMap] ADD CONSTRAINT [PK_ModuloSiteMap] PRIMARY KEY CLUSTERED  ([sis_id], [mod_id], [msm_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_ModuloSiteMap_01] on [dbo].[SYS_ModuloSiteMap]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_ModuloSiteMap_01] ON [dbo].[SYS_ModuloSiteMap] ([msm_url], [sis_id], [mod_id]) INCLUDE ([msm_id], [msm_urlHelp]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Modulo]'
GO
CREATE TABLE [dbo].[SYS_Modulo]
(
[sis_id] [int] NOT NULL,
[mod_id] [int] NOT NULL,
[mod_nome] [varchar] (50) NOT NULL,
[mod_descricao] [text] NULL,
[mod_idPai] [int] NULL,
[mod_auditoria] [bit] NOT NULL CONSTRAINT [DF_SYS_Modulo_mod_auditori] DEFAULT ((0)),
[mod_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Modulo_mod_situacao] DEFAULT ((1)),
[mod_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Modulo_mod_dataCriacao] DEFAULT (getdate()),
[mod_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Modulo_mod_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Modulo] on [dbo].[SYS_Modulo]'
GO
ALTER TABLE [dbo].[SYS_Modulo] ADD CONSTRAINT [PK_SYS_Modulo] PRIMARY KEY CLUSTERED  ([sis_id], [mod_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Modulo_02] on [dbo].[SYS_Modulo]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Modulo_02] ON [dbo].[SYS_Modulo] ([sis_id], [mod_id], [mod_situacao]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_Modulo_01] on [dbo].[SYS_Modulo]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_Modulo_01] ON [dbo].[SYS_Modulo] ([sis_id], [mod_idPai], [mod_situacao], [mod_id], [mod_nome]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_SiteMapXML2]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_SelectBy_SiteMapXML2]
	@gru_id uniqueidentifier
	, @sis_id int
	, @vis_id int
	, @mod_id int
AS
BEGIN
	WITH Menus(sis_id, mod_id, mod_idPai, msm_id, mod_nome, msm_url, vmm_ordem) 
	AS
	(
		(
		SELECT
			SYS_Modulo.sis_id
			, SYS_Modulo.mod_id
			, ISNULL(SYS_Modulo.mod_idPai,0)
			, SYS_ModuloSiteMap.msm_id
			, SYS_Modulo.mod_nome
			, ISNULL(SYS_ModuloSiteMap.msm_url,('~/Index.aspx?mod_id='+ CAST(SYS_Modulo.mod_id AS varchar)))
			, SYS_VisaoModuloMenu.vmm_ordem
		FROM
		SYS_Modulo WITH (NOLOCK)
			INNER JOIN SYS_ModuloSiteMap WITH (NOLOCK)
				ON SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id 
				AND SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
			INNER JOIN SYS_VisaoModuloMenu WITH (NOLOCK)
				ON SYS_ModuloSiteMap.sis_id = SYS_VisaoModuloMenu.sis_id 
				AND SYS_ModuloSiteMap.mod_id = SYS_VisaoModuloMenu.mod_id
				AND SYS_ModuloSiteMap.msm_id = SYS_VisaoModuloMenu.msm_id	
		WHERE
			SYS_Modulo.mod_situacao <> 3
			AND SYS_Modulo.sis_id = @sis_id
			AND SYS_VisaoModuloMenu.vis_id = @vis_id
			AND EXISTS (
				SELECT 
					SYS_GrupoPermissao.gru_id
					, SYS_GrupoPermissao.sis_id
					, SYS_GrupoPermissao.mod_id 
				FROM
					SYS_GrupoPermissao WITH (NOLOCK)
				WHERE
					SYS_GrupoPermissao.gru_id = @gru_id
					AND SYS_GrupoPermissao.sis_id = SYS_Modulo.sis_id
					AND SYS_GrupoPermissao.mod_id = SYS_Modulo.mod_id
					AND 
					(
						(grp_consultar = 1)
						OR  
						(grp_inserir = 1)
						OR 
						(grp_alterar = 1) 
						OR
						(grp_excluir = 1)
					)
			)
		UNION 
		SELECT  
			--sis_id, mod_id, mod_idPai, msm_id, mod_nome, msm_url, vmm_ordem
			sis_id, 0, NULL, 1, sis_nome, '~/Index.aspx', 1			  			
		FROM SYS_Sistema WITH(NOLOCK) 
			WHERE sis_id = @sis_id	 
			
			)	
		
	)
	
	SELECT --	 1,sistema.*,sistema.*,sistema.*,sistema.* 
		1 AS Tag
		, NULL AS Parent
		, mod_nome	AS	[sistema!1!id]
		, msm_url	AS	[sistema!1!url]
		, vmm_ordem	AS	[sistema!1!ordem]
		, NULL AS [menu!2!id]
		, NULL AS [menu!2!url]
		, NULL AS [menu!2!ordem]
		, NULL AS [item!3!id]
		, NULL AS [item!3!url]
		, NULL AS [item!3!ordem]
		, NULL AS [subitem!4!id]
		, NULL AS [subitem!4!url]
		, NULL AS [subitem!4!ordem]	
	FROM 
		menus AS  sistema
	WHERE
	(((@mod_id IS NULL) AND (mod_idPai IS NULL)) OR mod_id = @mod_id)
	
	UNION ALL  
	
	SELECT --2,menu.*,sistema.*,sistema.*,sistema.* 
		2 AS Tag
		, 1 AS Parent
		, sistema.mod_nome	AS	[sistema!1!id]
		, sistema.msm_url	AS	[sistema!1!url]
		, sistema.vmm_ordem	AS	[sistema!1!ordem]
		, menu.mod_nome AS [menu!2!id]
		, menu.msm_url AS [menu!2!url]
		, menu.vmm_ordem AS [menu!2!ordem]
		, NULL AS [item!3!id]
		, NULL AS [item!3!url]
		, NULL AS [item!3!ordem]
		, NULL AS [subitem!4!id]
		, NULL AS [subitem!4!url]
		, NULL AS [subitem!4!ordem]
	FROM 
		menus AS  sistema
	INNER JOIN menus AS menu ON menu.mod_idPai = sistema.mod_id
	WHERE
	(((@mod_id IS NULL) AND (sistema.mod_idPai IS NULL)) OR  sistema.mod_id = @mod_id)		
	
	UNION ALL 
	
	SELECT -- 3,item.*,sistema.*,sistema.*,menu.* 
		3 AS Tag
		, 2 AS Parent
		, sistema.mod_nome	
		, sistema.msm_url	
		, sistema.vmm_ordem	
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, ISNULL(item.msm_url, '')
		, item.vmm_ordem
		, NULL AS [subitem!3!id]
		, NULL AS [subitem!3!url]
		, NULL AS [subitem!3!ordem]
	FROM 
		menus AS  sistema
	INNER JOIN menus AS menu ON menu.mod_idPai = sistema.mod_id
	INNER JOIN Menus AS item ON item.mod_idPai = menu.mod_id
	WHERE 
	(((@mod_id IS NULL) AND (sistema.mod_idPai IS NULL )) OR sistema.mod_id = @mod_id)
	
	UNION ALL
	
	SELECT --4,subitem.*,sistema.*,menu.*, item.*
		4 AS Tag
		, 3 AS Parent
		, sistema.mod_nome	
		, sistema.msm_url	
		, sistema.vmm_ordem	
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, item.msm_url
		, item.vmm_ordem
		, subitem.mod_nome
		, subitem.msm_url
		, subitem.vmm_ordem
	FROM 
		menus AS  sistema
	INNER JOIN menus AS menu ON menu.mod_idPai = sistema.mod_id
	INNER JOIN Menus AS item ON item.mod_idPai = menu.mod_id
	INNER JOIN Menus AS subitem WITH(NOLOCK) ON subitem.mod_idPai = item.mod_id
	WHERE
	(((@mod_id IS NULL) AND (sistema.mod_idPai IS NULL )) OR sistema.mod_id = @mod_id)
	ORDER BY
	[sistema!1!ordem],[menu!2!ordem], [item!3!ordem], [subitem!4!ordem]
	FOR XML EXPLICIT, ROOT('menus')
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_Pessoa_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_Pessoa_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,pes_nome
		,pes_nome_abreviado
		,pai_idNacionalidade
		,pes_naturalizado
		,cid_idNaturalidade
		,pes_dataNascimento
		,pes_estadoCivil
		,pes_racaCor
		,pes_sexo
		,pes_idFiliacaoPai
		,pes_idFiliacaoMae
		,tes_id
		,pes_foto
		,pes_situacao
		,pes_dataCriacao
		,pes_dataAlteracao
		,pes_integridade
		,arq_idFoto

	FROM 
		PES_Pessoa WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:45
-- Description:	Altera o tipo de escolaridade preservando a ordem, 
--				a data da criação e a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_UPDATE]
	@tes_id uniqueidentifier
	, @tes_nome VARCHAR (100)	
	, @tes_ordem INT 
	, @tes_situacao TINYINT	
	, @tes_dataAlteracao DATETIME	

AS
BEGIN
	UPDATE PES_TipoEscolaridade 
	SET 
		tes_nome = @tes_nome 		
		, tes_ordem = @tes_ordem
		, tes_situacao = @tes_situacao 		
		, tes_dataAlteracao = @tes_dataAlteracao 		

	WHERE 
		tes_id = @tes_id 

	RETURN ISNULL(@@ROWCOUNT,-1)
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Parametro]'
GO
CREATE TABLE [dbo].[SYS_Parametro]
(
[par_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Param__par_i__117F9D94] DEFAULT (newsequentialid()),
[par_chave] [varchar] (100) NOT NULL,
[par_valor] [varchar] (1000) NOT NULL,
[par_descricao] [varchar] (200) NULL,
[par_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_Parametro_par_situacao] DEFAULT ((1)),
[par_vigenciaInicio] [date] NOT NULL,
[par_vigenciaFim] [date] NULL,
[par_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Parametro_par_dataCriacao] DEFAULT (getdate()),
[par_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_Parametro_par_dataAlteracao] DEFAULT (getdate()),
[par_obrigatorio] [bit] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Parametro] on [dbo].[SYS_Parametro]'
GO
ALTER TABLE [dbo].[SYS_Parametro] ADD CONSTRAINT [PK_SYS_Parametro] PRIMARY KEY CLUSTERED  ([par_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_SelectBy_Vigente]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 14/02/2012 14:13
-- Description:	Retorna os parâmetros ativos e vigentes.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_SelectBy_Vigente]	
AS
BEGIN

	SELECT 		
		par_chave
		, par_valor
	FROM
		SYS_Parametro WITH (NOLOCK)		
	WHERE
		par_situacao = 1				
		AND par_vigenciaInicio <= CAST(GETDATE() AS DATE)
		AND (par_vigenciaFim IS NULL OR (par_vigenciaFim IS NOT NULL AND par_vigenciaFim >= CAST(GETDATE() AS DATE)))				
						
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_UPDATE]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_UPDATE]
	@uap_id INT
	, @uap_username VARCHAR (100)
	, @uap_situacao TINYINT
	, @uap_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_UsuarioAPI 
	SET 
		uap_username = @uap_username 
		, uap_situacao = @uap_situacao 
		, uap_dataAlteracao = @uap_dataAlteracao 

	WHERE 
		uap_id = @uap_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:14
-- Description:	Seleciona o valor do campo integridade da tabela de grupo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_Select_Integridade]
		@gru_id uniqueidentifier
AS
BEGIN
	SELECT 			
		gru_integridade
	FROM
		SYS_Grupo WITH (NOLOCK)
	WHERE 
		gru_id = @gru_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_Pessoa_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_Pessoa_LOAD]
	@pes_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 pes_id  
		, pes_nome 
		, pes_nome_abreviado 
		, pai_idNacionalidade 
		, pes_naturalizado 
		, cid_idNaturalidade 
		, pes_dataNascimento 
		, pes_estadoCivil 
		, pes_racaCor 
		, pes_sexo 
		, pes_idFiliacaoPai 
		, pes_idFiliacaoMae 
		, tes_id 
		, pes_foto 
		, pes_situacao 
		, pes_dataCriacao 
		, pes_dataAlteracao 
		, pes_integridade 
		, arq_idFoto 

 	FROM
 		PES_Pessoa
	WHERE 
		pes_id = @pes_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UsuarioSenhaHistorico]'
GO
CREATE TABLE [dbo].[SYS_UsuarioSenhaHistorico]
(
[usu_id] [uniqueidentifier] NOT NULL,
[ush_senha] [varchar] (256) NOT NULL,
[ush_criptografia] [tinyint] NULL,
[ush_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_SYS_UsuarioSenhaHistorico_ush_id] DEFAULT (newsequentialid()),
[ush_data] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UsuarioSenhaHistorico] on [dbo].[SYS_UsuarioSenhaHistorico]'
GO
ALTER TABLE [dbo].[SYS_UsuarioSenhaHistorico] ADD CONSTRAINT [PK_SYS_UsuarioSenhaHistorico] PRIMARY KEY CLUSTERED  ([usu_id], [ush_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioSenhaHistorico_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioSenhaHistorico_LOAD]
	@usu_id UniqueIdentifier
	, @ush_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 usu_id  
		, ush_senha 
		, ush_criptografia 
		, ush_id 
		, ush_data 

 	FROM
 		SYS_UsuarioSenhaHistorico
	WHERE 
		usu_id = @usu_id
		AND ush_id = @ush_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_SelectBy_tes_nome]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 17/05/2010 11:59
-- Description:	utilizado no cadastro de tipos de escolaridade,
--              para saber se o nome já está cadastrado
--				filtrados por:
--					tipo de escolaridade (diferente do parametro), 					 
--                  nome e situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_SelectBy_tes_nome]	
	@tes_id uniqueidentifier
	,@tes_nome VARCHAR(100)	
	,@tes_situacao TINYINT		
AS
BEGIN
	SELECT 
		tes_id
	FROM
		PES_TipoEscolaridade WITH (NOLOCK)		
	WHERE
		tes_situacao <> 3
		AND (@tes_id is null or tes_id <> @tes_id)										
		AND (@tes_nome is null or tes_nome = @tes_nome)					
		AND (@tes_situacao is null or tes_situacao = @tes_situacao)						
	ORDER BY
		tes_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_UpdateSituacao]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_UpdateSituacao]
	@uap_id INT
	, @uap_situacao TINYINT
	, @uap_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_UsuarioAPI 
	SET 
		uap_situacao = @uap_situacao 
		, uap_dataAlteracao = @uap_dataAlteracao 

	WHERE 
		uap_id = @uap_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_EntidadeEndereco]'
GO
CREATE TABLE [dbo].[SYS_EntidadeEndereco]
(
[ent_id] [uniqueidentifier] NOT NULL,
[ene_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Entid__ene_i__5CD6CB2B] DEFAULT (newsequentialid()),
[end_id] [uniqueidentifier] NOT NULL,
[ene_numero] [varchar] (20) NOT NULL,
[ene_complemento] [varchar] (100) NULL,
[ene_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_EntidadeEndereco_ene_situacao] DEFAULT ((1)),
[ene_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_EntidadeEndereco_ene_dataCriacao] DEFAULT (getdate()),
[ene_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_EntidadeEndereco_ene_dataAlteracao] DEFAULT (getdate()),
[ene_enderecoPrincipal] [bit] NULL,
[ene_latitude] [decimal] (15, 10) NULL,
[ene_longitude] [decimal] (15, 10) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_EntidadeEndereco] on [dbo].[SYS_EntidadeEndereco]'
GO
ALTER TABLE [dbo].[SYS_EntidadeEndereco] ADD CONSTRAINT [PK_SYS_EntidadeEndereco] PRIMARY KEY CLUSTERED  ([ent_id], [ene_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_EntidadeEndereco_01] on [dbo].[SYS_EntidadeEndereco]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_EntidadeEndereco_01] ON [dbo].[SYS_EntidadeEndereco] ([ene_situacao], [ent_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[END_Endereco]'
GO
CREATE TABLE [dbo].[END_Endereco]
(
[end_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__END_Ender__end_i__44FF419A] DEFAULT (newsequentialid()),
[end_cep] [varchar] (8) NOT NULL,
[end_logradouro] [varchar] (200) NOT NULL,
[end_bairro] [varchar] (100) NULL,
[end_distrito] [varchar] (100) NULL,
[end_zona] [tinyint] NULL,
[cid_id] [uniqueidentifier] NOT NULL,
[end_situacao] [tinyint] NOT NULL CONSTRAINT [DF_END_Endereco_end_situacao] DEFAULT ((1)),
[end_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_END_Endereco_end_dataCriacao] DEFAULT (getdate()),
[end_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_END_Endereco_end_dataAlteracao] DEFAULT (getdate()),
[end_integridade] [int] NOT NULL CONSTRAINT [DF_END_Endereco_end_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_END_Endereco] on [dbo].[END_Endereco]'
GO
ALTER TABLE [dbo].[END_Endereco] ADD CONSTRAINT [PK_END_Endereco] PRIMARY KEY CLUSTERED  ([end_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_END_Endereco_01] on [dbo].[END_Endereco]'
GO
CREATE NONCLUSTERED INDEX [IX_END_Endereco_01] ON [dbo].[END_Endereco] ([end_situacao], [end_id], [cid_id], [end_cep], [end_logradouro], [end_bairro]) INCLUDE ([end_dataCriacao], [end_distrito], [end_zona]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeEndereco_SelectEnderecosBy_Entidade]'
GO
-- ========================================================================
-- Author:		Carla Frascareli
-- Create date: 07/05/2012
-- Description:	Retorna os endereços cadastrados para a entidade.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeEndereco_SelectEnderecosBy_Entidade]
	@ent_id uniqueidentifier
AS
BEGIN
	SELECT 
		Ene.ene_id
		, Ene.end_id
		, Ene.ene_numero
		, Ene.ene_complemento
		, Ende.cid_id
		, Ende.end_bairro
		, Ende.end_logradouro
		, Ende.end_zona
		, Ende.end_cep
		, Cid.cid_nome
		, Unf.unf_id
		, Unf.unf_nome
		, Unf.unf_sigla
	FROM
		SYS_EntidadeEndereco Ene WITH (NOLOCK)
	INNER JOIN END_Endereco Ende WITH(NOLOCK)
		ON Ende.end_id = Ene.end_id
	INNER JOIN END_Cidade Cid WITH(NOLOCK)
		ON Cid.cid_id = Ende.cid_id
	INNER JOIN END_UnidadeFederativa Unf WITH(NOLOCK)
		ON Unf.unf_id = Cid.unf_id
	WHERE
		ene_situacao <> 3
		AND Ende.end_situacao <> 3
		AND Cid.cid_situacao <> 3
		AND Unf.unf_situacao <> 3
		AND ent_id = @ent_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:15
-- Description:	Incrementa uma unidade no campo integridade da tabela de grupo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_INCREMENTA_INTEGRIDADE]
		@gru_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_Grupo
	SET 
		gru_integridade = gru_integridade + 1
	WHERE 
		gru_id = @gru_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_LOAD]'
GO
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_LOAD]
	@pes_id UNIQUEIDENTIFIER

AS
BEGIN
	SELECT	
		TOP 1
		  pes_id
		, pes_nome
		, pes_nome_abreviado			
		, pai_idNacionalidade
		, pes_naturalizado
		, cid_idNaturalidade			
		, pes_dataNascimento
		, pes_estadoCivil
		, pes_racaCor
		, pes_sexo
		, pes_idFiliacaoPai
		, pes_idFiliacaoMae
		, tes_id
		, pes_foto
		, pes_situacao
		, pes_dataCriacao
		, pes_dataAlteracao
		, pes_integridade
		, arq_idFoto
		, pes_nomeSocial
 	FROM
 		PES_Pessoa WITH (NOLOCK)
	WHERE 
		pes_id = @pes_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_Pessoa_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_Pessoa_INSERT]
	@pes_id UniqueIdentifier = null
	, @pes_nome VarChar (200)
	, @pes_nome_abreviado VarChar (50)
	, @pai_idNacionalidade UniqueIdentifier
	, @pes_naturalizado Bit
	, @cid_idNaturalidade UniqueIdentifier
	, @pes_dataNascimento DateTime
	, @pes_estadoCivil TinyInt
	, @pes_racaCor TinyInt
	, @pes_sexo TinyInt
	, @pes_idFiliacaoPai UniqueIdentifier
	, @pes_idFiliacaoMae UniqueIdentifier
	, @tes_id UniqueIdentifier
	, @pes_foto varbinary(max)  = NULL
	, @pes_situacao TinyInt
	, @pes_dataCriacao DateTime
	, @pes_dataAlteracao DateTime
	, @pes_integridade Int
	, @arq_idFoto BigInt = NULL

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_Pessoa
		( 
			pes_id 
			, pes_nome 
			, pes_nome_abreviado 
			, pai_idNacionalidade 
			, pes_naturalizado 
			, cid_idNaturalidade 
			, pes_dataNascimento 
			, pes_estadoCivil 
			, pes_racaCor 
			, pes_sexo 
			, pes_idFiliacaoPai 
			, pes_idFiliacaoMae 
			, tes_id 
			, pes_foto 
			, pes_situacao 
			, pes_dataCriacao 
			, pes_dataAlteracao 
			, pes_integridade 
			, arq_idFoto 
 
		)
	OUTPUT inserted.pes_id INTO @ID
	VALUES
		( 
			ISNULL(@pes_id, NEWID())
			, @pes_nome 
			, @pes_nome_abreviado 
			, @pai_idNacionalidade 
			, @pes_naturalizado 
			, @cid_idNaturalidade 
			, @pes_dataNascimento 
			, @pes_estadoCivil 
			, @pes_racaCor 
			, @pes_sexo 
			, @pes_idFiliacaoPai 
			, @pes_idFiliacaoMae 
			, @tes_id 
			, @pes_foto 
			, @pes_situacao 
			, @pes_dataCriacao 
			, @pes_dataAlteracao 
			, @pes_integridade 
			, @arq_idFoto 
 
		)
		
	SELECT ID FROM @ID
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioSenhaHistorico_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioSenhaHistorico_INSERT]
	@usu_id UniqueIdentifier
	, @ush_senha VarChar (256)
	, @ush_criptografia TinyInt
	, @ush_id UniqueIdentifier
	, @ush_data DateTime

AS
BEGIN
	INSERT INTO 
		SYS_UsuarioSenhaHistorico
		( 
			usu_id 
			, ush_senha 
			, ush_criptografia 
			, ush_id 
			, ush_data 
 
		)
	VALUES
		( 
			@usu_id 
			, @ush_senha 
			, @ush_criptografia 
			, @ush_id 
			, @ush_data 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:34
-- Description:	utilizado na busca de Tipo de Escolaridade, retorna os 
--              Tipos de Escolaridade que não foram excluídos logicamente,
--				filtrados por:
--					id, nome, sigla, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_SelectBy_All]	
		@tes_id uniqueidentifier
		, @tes_nome char(100)
		, @tes_situacao TinyInt
AS
BEGIN
	SELECT 
		tes_id
		, tes_nome
		, tes_ordem
		, CASE tes_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS tes_situacao
	FROM
		PES_TipoEscolaridade WITH (NOLOCK)
	WHERE
		tes_situacao <> 3
		AND (@tes_id is null or tes_id = @tes_id)		
		AND (@tes_nome is null or tes_nome LIKE '%' + @tes_nome + '%')		
		AND (@tes_situacao is null or tes_situacao = @tes_situacao)				
	ORDER BY 
		tes_nome
		
	SELECT @@ROWCOUNT			
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_SelecionaAtivos]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 26/05/2014
-- Description:	Seleciona os usuários API ativos.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_SelecionaAtivos] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		uap.uap_id,
		uap.uap_username,
		uap.uap_password,
		uap.uap_situacao,
		uap.uap_dataCriacao,
		uap.uap_dataAlteracao,
		CAST(0 AS BIT) AS IsNew
	FROM
		CFG_UsuarioAPI uap WITH(NOLOCK)
	WHERE
		uap.uap_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[VW_PES_Pessoa]'
GO


-- =============================================
-- Author:		Carla Frascareli
-- Create date: 11/05/2012
-- Description:	View que retorna dados da pessoa necessários na visualização
--				do boletim online (gestão acadêmica).
-- =============================================
CREATE VIEW [dbo].[VW_PES_Pessoa]
AS
SELECT
	Pes.pes_id
	, Pes.pes_nome
	, Pes.pes_nomeSocial
	, Pes.pes_dataNascimento
FROM PES_Pessoa Pes WITH(NOLOCK)
WHERE Pes.pes_situacao <> 3



GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:16
-- Description:	Decrementa uma unidade no campo integridade da tabela de grupo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_DECREMENTA_INTEGRIDADE]
		@gru_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_Grupo 
	SET 
		gru_integridade = gru_integridade - 1
	WHERE 
		gru_id = @gru_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_Pessoa_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_Pessoa_DELETE]
	@pes_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		PES_Pessoa 
	WHERE 
		pes_id = @pes_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioSenhaHistorico_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioSenhaHistorico_UPDATE]
	@usu_id UNIQUEIDENTIFIER
	, @ush_senha VARCHAR (256)
	, @ush_criptografia TINYINT
	, @ush_id UNIQUEIDENTIFIER
	, @ush_data DATETIME

AS
BEGIN
	UPDATE SYS_UsuarioSenhaHistorico 
	SET 
		ush_senha = @ush_senha 
		, ush_criptografia = @ush_criptografia 
		, ush_data = @ush_data 

	WHERE 
		usu_id = @usu_id 
		AND ush_id = @ush_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:47
-- Description:	Seleciona o valor do campo integridade da tabela tipo de
--				escolaridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_Select_Integridade]
		@tes_id uniqueidentifier
AS
BEGIN
	SELECT 			
		tes_integridade
	FROM
		PES_TipoEscolaridade WITH (NOLOCK)
	WHERE 		
		tes_id = @tes_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_VerificaUsernameExistente]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 27/05/2014
-- Description:	Verifica se já existe um usuário API com o mesmo nome.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_VerificaUsernameExistente] 
	@uap_username VARCHAR(100),
	@uap_id INT
AS
BEGIN
	SET @uap_username = LTRIM(RTRIM(@uap_username));

	SELECT
		uap.uap_id
	FROM
		CFG_UsuarioAPI uap WITH(NOLOCK)
	WHERE
		LTRIM(RTRIM(uap.uap_username)) = @uap_username
		AND uap.uap_id <> ISNULL(@uap_id, -1)
		AND uap.uap_situacao <> 3
		
	RETURN ISNULL(@@ROWCOUNT, -1);
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupoUA_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupoUA_UPDATE]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@ugu_id uniqueidentifier
		,@ent_id uniqueidentifier
		,@uad_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_UsuarioGrupoUA
	SET 		
		ent_id = @ent_id
		,uad_id = @uad_id
	WHERE 
		usu_id = @usu_id
	and gru_id = @gru_id
	and ugu_id = @ugu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupo_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupo_LOAD]
	@usu_id uniqueidentifier
	, @gru_id uniqueidentifier
AS
BEGIN
	SELECT	gru_id
			, usu_id
			, usg_situacao
 	FROM
 		SYS_UsuarioGrupo
	WHERE 
		usu_id = @usu_id
		AND gru_id = @gru_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaSuperior_SelectBy_UASuperior]'
GO
-- ========================================================================
-- Author:		Moisés J. Carmo
-- Create date: 21/10/2013.
-- Description:	Retorna a unidade adminstrativa superior
--				filtrando por: entidade e unidade administrativa superior.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaSuperior_SelectBy_UASuperior]
	  @ent_id UNIQUEIDENTIFIER
	, @uad_idSupeior UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		uad_id
      , uad_nome
      
	FROM
      SYS_UnidadeAdministrativa WITH(NOLOCK)
	WHERE
	      uad_situacao <> 3
      AND ent_id = @ent_id
      AND uad_id = @uad_idSupeior

	SELECT @@ROWCOUNT	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_TipoDocumentacaoAtributo]'
GO
CREATE TABLE [dbo].[SYS_TipoDocumentacaoAtributo]
(
[tda_id] [tinyint] NOT NULL,
[tda_descricao] [varchar] (64) NULL,
[tda_nomeObjeto] [varchar] (256) NULL,
[tda_default] [bit] NULL CONSTRAINT [DF_SYS_TipoDocumentacaoAtributo_tda_default] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_TipoDocumentacaoAtributo] on [dbo].[SYS_TipoDocumentacaoAtributo]'
GO
ALTER TABLE [dbo].[SYS_TipoDocumentacaoAtributo] ADD CONSTRAINT [PK_SYS_TipoDocumentacaoAtributo] PRIMARY KEY CLUSTERED  ([tda_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault]'
GO

-- [dbo].[NEW_SYS_TipoDocumentacaoAtributo_SelecionaStringDefault]
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault]
	@ExibirRetorno			BIT = 1,
	@AtributosDefault		VARCHAR(512)  OUTPUT,
	@NomeObjetosDefault		VARCHAR(1024) OUTPUT
AS
BEGIN

	DECLARE 
		  @stringDefault	VARCHAR(512)  = ''
		, @objetosDefault	VARCHAR(1024) = ''


	-- Monta a string com os atributos default
	SELECT 
		@stringDefault += CASE WHEN ISNULL(tda.tda_default, 0) = 1 THEN '1' ELSE '0' END
	FROM 
		SYS_TipoDocumentacaoAtributo tda WITH (NOLOCK) 
	ORDER BY 
		tda.tda_id


	-- Monta a string com os nomes dos objetos default
	SELECT 
		@objetosDefault += tda.tda_nomeObjeto + ';'
	FROM 
		SYS_TipoDocumentacaoAtributo tda WITH (NOLOCK) 
	WHERE 
		ISNULL(tda.tda_default, 0) = 1
	ORDER BY 
		tda.tda_id

	
	SET @AtributosDefault = @stringDefault
	SET @NomeObjetosDefault = @objetosDefault

	IF (@ExibirRetorno = 1) BEGIN
		-- Retorna as informações default
		SELECT 
			  @AtributosDefault		AS AtributosDefault
			, @NomeObjetosDefault	AS ObjetosDefault
	END

END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_DiaNaoUtil]'
GO
CREATE TABLE [dbo].[SYS_DiaNaoUtil]
(
[dnu_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_DiaNa__dnu_i__4AB81AF0] DEFAULT (newsequentialid()),
[dnu_nome] [varchar] (100) NOT NULL,
[dnu_abrangencia] [tinyint] NOT NULL,
[dnu_descricao] [varchar] (400) NULL,
[dnu_data] [date] NOT NULL,
[dnu_recorrencia] [bit] NULL,
[dnu_vigenciaInicio] [date] NOT NULL,
[dnu_vigenciaFim] [date] NULL,
[cid_id] [uniqueidentifier] NULL,
[unf_id] [uniqueidentifier] NULL,
[dnu_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_DiaNaoUtil_dnu_situacao] DEFAULT ((1)),
[dnu_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_DiaNaoUtil_dnu_dataCriacao] DEFAULT (getdate()),
[dnu_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_DiaNaoUtil_dnu_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_DiaNaoUtil] on [dbo].[SYS_DiaNaoUtil]'
GO
ALTER TABLE [dbo].[SYS_DiaNaoUtil] ADD CONSTRAINT [PK_SYS_DiaNaoUtil] PRIMARY KEY CLUSTERED  ([dnu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_DiaNaoUtil_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_DiaNaoUtil_UPDATE]
		@dnu_id uniqueidentifier
		, @dnu_nome varchar(100)
		, @dnu_abrangencia TinyInt
		, @dnu_descricao varchar(400)
		, @dnu_data date
		, @dnu_recorrencia TinyInt
		, @dnu_vigenciaInicio date
		, @dnu_vigenciaFim date
		, @cid_id uniqueidentifier
		, @unf_id uniqueidentifier
		, @dnu_situacao TinyInt
		, @dnu_dataCriacao DateTime
		, @dnu_dataAlteracao DateTime

AS
BEGIN
	UPDATE SYS_DiaNaoUtil 
	SET 
		dnu_nome = @dnu_nome
		, dnu_abrangencia = @dnu_abrangencia
		, dnu_descricao = @dnu_descricao
		, dnu_data = @dnu_data
		, dnu_recorrencia = @dnu_recorrencia
		, dnu_vigenciaInicio = @dnu_vigenciaInicio
		, dnu_vigenciaFim = @dnu_vigenciaFim
		, cid_id = @cid_id
		, unf_id = @unf_id
		, dnu_situacao = @dnu_situacao
		, dnu_dataCriacao = @dnu_dataCriacao
		, dnu_dataAlteracao = @dnu_dataAlteracao
	WHERE 
		dnu_id = @dnu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioSenhaHistorico_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_UsuarioSenhaHistorico_DELETE]
	@usu_id UNIQUEIDENTIFIER
	, @ush_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		SYS_UsuarioSenhaHistorico 
	WHERE 
		usu_id = @usu_id 
		AND ush_id = @ush_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:46
-- Description:	Incrementa uma unidade no campo integridade do tipo de escolaridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_INCREMENTA_INTEGRIDADE]
		@tes_id uniqueidentifier
AS
BEGIN
	UPDATE PES_TipoEscolaridade
	SET 		
		tes_integridade = tes_integridade + 1		
	WHERE 
		tes_id = @tes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupoUA_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupoUA_SELECT]
	
AS
BEGIN
	SELECT 
		usu_id
		,gru_id
		,ugu_id
		,ent_id
		,uad_id
	FROM 
		SYS_UsuarioGrupoUA WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_ent_id_HierarquiaXML]'
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_ent_id_HierarquiaXML]
	@ent_id uniqueidentifier
AS
BEGIN
	WITH UAs
		(
		uad_id,tua_id, uad_codigo, uad_nome, uad_sigla, uad_idSuperior, uad_situacao, 
		uad_dataCriacao, uad_dataAlteracao, uad_integirdade 
		) 
	AS
	(
		SELECT
			SYS_UnidadeAdministrativa.uad_id,
			SYS_UnidadeAdministrativa.tua_id,
			SYS_UnidadeAdministrativa.uad_codigo,
			SYS_UnidadeAdministrativa.uad_nome,
			SYS_UnidadeAdministrativa.uad_sigla,
			SYS_UnidadeAdministrativa.uad_idSuperior,
			SYS_UnidadeAdministrativa.uad_situacao,
			SYS_UnidadeAdministrativa.uad_dataCriacao,
			SYS_UnidadeAdministrativa.uad_dataAlteracao,
			SYS_UnidadeAdministrativa.uad_integridade
		FROM
			SYS_UnidadeAdministrativa WITH (NOLOCK)
		INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
			ON SYS_UnidadeAdministrativa.tua_id = SYS_TipoUnidadeAdministrativa.tua_id 

		WHERE
			SYS_UnidadeAdministrativa.uad_situacao <> 3
			AND SYS_UnidadeAdministrativa.ent_id = @ent_id
		)
		
	SELECT 1 AS Tag
		, NULL AS Parent
		, uad_id AS [UnidadeAdministrativa_nivel_1!1!id]
		, uad_nome AS [UnidadeAdministrativa_nivel_1!1!nome]
		, NULL AS [UnidadeAdministrativa_nivel_2!2!id]
		, NULL AS [UnidadeAdministrativa_nivel_2!2!nome]
		, NULL AS [UnidadeAdministrativa_nivel_3!3!id]
		, NULL AS [UnidadeAdministrativa_nivel_3!3!nome]
		, NULL AS [UnidadeAdministrativa_nivel_4!4!id]
		, NULL AS [UnidadeAdministrativa_nivel_4!4!nome]
	FROM 
		UAs WITH(NOLOCK)
	WHERE
		uad_idSuperior is null
	UNION ALL
	SELECT 2 AS Tag
		, 1 AS Parent
		, UnidadeAdministrativa.uad_id
		, UnidadeAdministrativa.uad_nome
		, item.uad_id
		, item.uad_nome
		, NULL AS [UnidadeAdministrativa_nivel_3!3!id]
		, NULL AS [UnidadeAdministrativa_nivel_3!3!nome]
		, NULL AS [UnidadeAdministrativa_nivel_4!4!id]
		, NULL AS [UnidadeAdministrativa_nivel_4!4!nome]
	FROM 
		UAs AS UnidadeAdministrativa WITH(NOLOCK)
	INNER JOIN UAs AS item WITH(NOLOCK)
		ON item.uad_idSuperior = UnidadeAdministrativa.uad_id
	WHERE
		UnidadeAdministrativa.uad_idSuperior is null
	UNION ALL
	SELECT 3 AS Tag
		, 2 AS Parent
		, UnidadeAdministrativa.uad_id
		, UnidadeAdministrativa.uad_nome
		, item.uad_id
		, item.uad_nome
		, subitem.uad_id
		, subitem.uad_nome
		, NULL AS [UnidadeAdministrativa_nivel_4!4!id]
		, NULL AS [UnidadeAdministrativa_nivel_4!4!nome]
	FROM 
		UAs AS UnidadeAdministrativa WITH(NOLOCK)
	INNER JOIN UAs AS item WITH(NOLOCK)
		ON item.uad_idSuperior = UnidadeAdministrativa.uad_id
	INNER JOIN UAs AS subitem WITH(NOLOCK)
		ON subitem.uad_idSuperior = item.uad_id
	WHERE
		UnidadeAdministrativa.uad_idSuperior is null
		AND item.uad_idSuperior is not null
	UNION ALL
	SELECT 4 AS Tag
		, 3 AS Parent
		, UnidadeAdministrativa.uad_id
		, UnidadeAdministrativa.uad_nome
		, item.uad_id
		, item.uad_nome
		, subitem.uad_id
		, subitem.uad_nome
		, subitem2.uad_id
		, subitem2.uad_nome
	FROM 
		UAs AS UnidadeAdministrativa WITH(NOLOCK)
	INNER JOIN UAs AS item WITH(NOLOCK)
		ON item.uad_idSuperior = UnidadeAdministrativa.uad_id
	INNER JOIN UAs AS subitem WITH(NOLOCK)
		ON subitem.uad_idSuperior = item.uad_id
	INNER JOIN UAs AS subitem2 WITH(NOLOCK)
		ON subitem2.uad_idSuperior = subitem.uad_id
	WHERE
		UnidadeAdministrativa.uad_idSuperior is null
		AND item.uad_idSuperior is not null
	ORDER BY
		[UnidadeAdministrativa_nivel_1!1!nome], [UnidadeAdministrativa_nivel_2!2!nome], [UnidadeAdministrativa_nivel_3!3!nome], [UnidadeAdministrativa_nivel_4!4!nome]

	FOR XML EXPLICIT, ROOT('Entidade')
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_DiaNaoUtil_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_DiaNaoUtil_SELECT]
	
AS
BEGIN
	SELECT 
		dnu_id
			, dnu_nome
			, dnu_abrangencia
			, dnu_descricao
			, dnu_data
			, dnu_recorrencia
			, dnu_vigenciaInicio
			, dnu_vigenciaFim
			, cid_id
			, unf_id
			, dnu_situacao
			, dnu_dataCriacao
			, dnu_dataAlteracao
	FROM 
		SYS_DiaNaoUtil WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioSenhaHistorico_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioSenhaHistorico_SELECT]
	
AS
BEGIN
	SELECT 
		usu_id
		,ush_senha
		,ush_criptografia
		,ush_id
		,ush_data

	FROM 
		SYS_UsuarioSenhaHistorico WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 08:46
-- Description:	Decrementa uma unidade no campo integridade do tipo de escolaridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_DECREMENTA_INTEGRIDADE]
		@tes_id uniqueidentifier
AS
BEGIN
	UPDATE PES_TipoEscolaridade
	SET 		
		tes_integridade = tes_integridade - 1		
	WHERE 
		tes_id = @tes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupoUA_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupoUA_LOAD]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@ugu_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		usu_id
		,gru_id
		,ugu_id
		,ent_id
		,uad_id
 	FROM
 		SYS_UsuarioGrupoUA
	WHERE 
		usu_id = @usu_id
	and gru_id = @gru_id
	and ugu_id = @ugu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoTotal]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 16/03/2011 10:16
-- Description:	Retorna as Unidades Administrativas pelos filtros informados,
--				pela entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoTotal]
	@tua_id UNIQUEIDENTIFIER	
	, @ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 	
		UA.tua_id
		, uad_id
		, uad_idSuperior
		, uad_nome
	FROM
		SYS_UnidadeAdministrativa UA WITH (NOLOCK)		
	INNER JOIN SYS_Entidade Ent WITH (NOLOCK)
		ON Ent.ent_id = UA.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa Tua WITH (NOLOCK)
		ON UA.tua_id = Tua.tua_id	
	WHERE
		uad_situacao <> 3
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND (@tua_id IS NULL OR UA.tua_id = @tua_id)	
		AND (UA.ent_id = @ent_id)
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_DiaNaoUtil_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_DiaNaoUtil_LOAD]
	@dnu_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		dnu_id
			, dnu_nome
			, dnu_abrangencia
			, dnu_descricao
			, dnu_data
			, dnu_recorrencia
			, dnu_vigenciaInicio
			, dnu_vigenciaFim
			, cid_id
			, unf_id
			, dnu_situacao
			, dnu_dataCriacao
			, dnu_dataAlteracao

 	FROM
 		SYS_DiaNaoUtil
	WHERE 
		dnu_id = @dnu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_TipoDeficiencia]'
GO
CREATE TABLE [dbo].[PES_TipoDeficiencia]
(
[tde_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_TipoD__tde_i__1BFD2C07] DEFAULT (newsequentialid()),
[tde_nome] [varchar] (100) NOT NULL,
[tde_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_TipoDeficiencia_tde_situacao] DEFAULT ((1)),
[tde_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_TipoDeficiencia_tde_dataCriacao] DEFAULT (getdate()),
[tde_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_TipoDeficiencia_tde_dataAlteracao] DEFAULT (getdate()),
[tde_integridade] [int] NOT NULL CONSTRAINT [DF_PES_TipoDeficiencia_tde_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_TipoDeficiencia] on [dbo].[PES_TipoDeficiencia]'
GO
ALTER TABLE [dbo].[PES_TipoDeficiencia] ADD CONSTRAINT [PK_PES_TipoDeficiencia] PRIMARY KEY CLUSTERED  ([tde_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 01/22/2010 13:10
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Deficiencia. Filtrada por: 
--					tdo_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_Update_Situacao]	
		@tde_id uniqueidentifier
		,@tde_situacao TINYINT
		,@tde_dataAlteracao DateTime
AS
BEGIN
	UPDATE PES_TipoDeficiencia 
	SET 
		tde_situacao = @tde_situacao
		,tde_dataAlteracao = @tde_dataAlteracao
	WHERE 
		tde_id = @tde_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_Update_mod_situacao]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 22/07/2010
-- Description:	Exclusão lógica de módulos
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_Update_mod_situacao]
	@sis_id int
	, @mod_id int
AS
BEGIN						
    --Exclui logicamente os módulos filhos
	UPDATE SYS_Modulo
	SET mod_situacao = 3, mod_dataAlteracao = GETDATE()
	WHERE sis_id = @sis_id
		AND mod_idPai = @mod_id

    --Exclui logicamente o módulo pai
	UPDATE SYS_Modulo
	SET mod_situacao = 3, mod_dataAlteracao = GETDATE()
	WHERE sis_id = @sis_id
		AND mod_id = @mod_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_RemovePaginaMenu]'
GO
-- =============================================
-- Author:		Haila Pelloso
-- Create date: 10/10/2011 as 09:57
-- Description:	Remove um módulo do sistema
-- =============================================
/* Parâmetros:
		@nomeSistema : Obrigatório
			Nome do sistema do módulo que será removido do menu (passar nome exato)
		@nomeModulo : Obrigatório
			Nome do módulo que será removido do menu
		@nomeModuloPai : Obrigatório (Passar nulo ou em branco caso o módulo não tenha módulo pai)
			Nome do módulo Pai que será incluído o menu (passar nome exato)
*/

CREATE PROCEDURE [dbo].[MS_RemovePaginaMenu]
	 @nomeSistema VARCHAR(100)
	 , @nomeModulo VARCHAR(50)
	 , @nomeModuloPai VARCHAR(50)

AS
BEGIN

	-- ID do sistema
	DECLARE @sis_id INT = 0
	SELECT TOP 1
		@sis_id = sis_id
	FROM
		SYS_Sistema WITH(NOLOCK)
	WHERE
		sis_nome = @nomeSistema
		AND sis_situacao <> 3

	-- ID do módulo pai
	DECLARE @mod_idPai INT = 0
	SELECT TOP 1
		@mod_idPai = mod_id
	FROM
		SYS_Modulo WITH(NOLOCK)
	WHERE
		sis_id = @sis_id
		AND mod_nome = @nomeModuloPai
		AND mod_situacao <> 3
	
	IF (ISNULL(@nomeModuloPai, '') = '')
	BEGIN
		SET @mod_idPai = NULL
	END
			
	-- ID do módulo 
	DECLARE @mod_id INT = 0
	SELECT TOP 1
		@mod_id = mod_id
	FROM
		SYS_Modulo WITH(NOLOCK)
	WHERE
		sis_id = @sis_id
		AND mod_nome = @nomeModulo
		AND (
				(@mod_idPai IS NULL AND mod_idPai IS NULL)
				OR
				(mod_idPai = @mod_idPai)
		)	
		AND mod_situacao <> 3

	-- Quantidade de módulos com o mesmo nome
	DECLARE @qtdModuloPorNome INT = 0
	SELECT TOP 1
		@qtdModuloPorNome = COUNT(*)
	FROM
		SYS_Modulo WITH(NOLOCK)
	WHERE
		sis_id = @sis_id
		AND mod_nome = @nomeModulo
		AND (
				(@mod_idPai IS NULL AND mod_idPai IS NULL)
				OR
				(mod_idPai = @mod_idPai)
		)	
		AND mod_situacao <> 3
		
	--Guarda as ocorrências ao criar o menu
	DECLARE @Mensagem VARCHAR(MAX) = 'Ínicio do processo de exclusão de módulo do menu.'
	
    if(@sis_id > 0 AND @mod_id > 0 AND @qtdModuloPorNome <= 1)
    BEGIN   
		EXEC NEW_SYS_Modulo_Update_mod_situacao
				@sis_id = @sis_id
				, @mod_id = @mod_id
				
		SET @Mensagem = @Mensagem + CHAR(13) + '-> Módulo "' + @nomeModulo + '" (ID: ' + CAST(@mod_id AS VARCHAR(MAX)) + ') excluído do sistema "' + @nomeSistema + '".'
		
	END   
	ELSE
	BEGIN
		IF (@sis_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + '-> O sistema não foi encontrado.'

		IF (@mod_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + '-> O módulo não foi encontrado.'
		
		IF (@qtdModuloPorNome > 1)
			SET @Mensagem = @Mensagem + CHAR(13) + '-> Existe mais de um módulo com o mesmo nome no sistema (não será deletado pela procedure).'
	END
    
	SET @Mensagem = @Mensagem + CHAR(13) + 'Fim do processo de exclusão do módulo do menu.' + CHAR(13)

	-- Retorna mensagem	
	PRINT @Mensagem
END
   	

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_Servico]'
GO
CREATE TABLE [dbo].[SYS_Servico]
(
[ser_id] [smallint] NOT NULL,
[ser_nome] [nvarchar] (100) NOT NULL,
[ser_nomeProcedimento] [nvarchar] (100) NOT NULL,
[ser_ativo] [bit] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_Servico] on [dbo].[SYS_Servico]'
GO
ALTER TABLE [dbo].[SYS_Servico] ADD CONSTRAINT [PK_SYS_Servico] PRIMARY KEY CLUSTERED  ([ser_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Servico_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Servico_LOAD]
	@ser_id SmallInt
	
AS
BEGIN
	SELECT	Top 1
		 ser_id  
		, ser_nome 
		, ser_nomeProcedimento 
		, ser_ativo 

 	FROM
 		SYS_Servico
	WHERE 
		ser_id = @ser_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupoUA_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupoUA_INSERT]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@ent_id uniqueidentifier
		,@uad_id uniqueidentifier


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_UsuarioGrupoUA
		( 
			usu_id
			,gru_id
			,ent_id
			,uad_id
		)
	OUTPUT inserted.ugu_id INTO @ID
	VALUES
		( 
			@usu_id
			,@gru_id
			,@ent_id
			,@uad_id
		)
	SELECT ID FROM @ID
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUASuperior]'
GO
-- ===================================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 17/03/2011 12:00
-- Description:	Retorna as Unidades Administrativas pelos filtros informados,
--				pela entidade e pela permissão do usuário nas UAs e nas UAs Superiores
-- ===================================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_PermissaoUASuperior]
	@tua_id UNIQUEIDENTIFIER	
	, @ent_id UNIQUEIDENTIFIER
	, @gru_id uniqueidentifier
	, @usu_id uniqueidentifier	
AS
BEGIN

	DECLARE @TbUas TABLE (uad_id UNIQUEIDENTIFIER NOT NULL);

	INSERT INTO @TbUas (uad_id)
	SELECT uad_id FROM FN_Select_UAs_By_PermissaoUsuario(@usu_id, @gru_id)

	SELECT 	
		UA.tua_id
		, uad_id
		, uad_idSuperior
		, uad_nome
	FROM
		SYS_UnidadeAdministrativa UA WITH (NOLOCK)		
	INNER JOIN SYS_Entidade Ent WITH (NOLOCK)
		ON Ent.ent_id = UA.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa Tua WITH (NOLOCK)
		ON UA.tua_id = Tua.tua_id	
	WHERE
		uad_situacao <> 3
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND (@tua_id IS NULL OR UA.tua_id = @tua_id)
		
		-- Somente da Entidade informada.
		AND (UA.ent_id = @ent_id)
			
		-- Filtra as Unidades Administrativas que o usuário tem permissão.
		AND (UA.uad_id IN (SELECT uad_id FROM @TbUas)
			OR UA.uad_id IN (
					SELECT UASup.uad_idSuperior 
					FROM @TbUas Per 
					INNER JOIN SYS_UnidadeAdministrativa UASup WITH (NOLOCK) 
						ON UASup.uad_id = Per.uad_id
						))
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_PessoaEndereco]'
GO
CREATE TABLE [dbo].[PES_PessoaEndereco]
(
[pes_id] [uniqueidentifier] NOT NULL,
[pse_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_Pesso__pse_i__619B8048] DEFAULT (newsequentialid()),
[end_id] [uniqueidentifier] NOT NULL,
[pse_numero] [varchar] (20) NOT NULL,
[pse_complemento] [varchar] (100) NULL,
[pse_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_PessoaEndereco_pse_situacao] DEFAULT ((1)),
[pse_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaEndereco_pse_dataCriacao] DEFAULT (getdate()),
[pse_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaEndereco_pse_dataAlteracao] DEFAULT (getdate()),
[pse_enderecoPrincipal] [bit] NULL,
[pse_latitude] [decimal] (15, 10) NULL,
[pse_longitude] [decimal] (15, 10) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_PessoaEndereco] on [dbo].[PES_PessoaEndereco]'
GO
ALTER TABLE [dbo].[PES_PessoaEndereco] ADD CONSTRAINT [PK_PES_PessoaEndereco] PRIMARY KEY CLUSTERED  ([pes_id], [pse_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaEndereco_CarregaEnderecos_SelectBy_pes_id]'
GO
-- ========================================================================
-- Author:		Gabriel Alves Scavassa
-- Create date: 11/02/2016 10:00
-- Description:	Retorna todos os endereços do usuário e campos cidade
--				filtrados por:
--					pes_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaEndereco_CarregaEnderecos_SelectBy_pes_id]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT
		pse_id as endRel_id
		, E.end_id
		, PE.pse_numero as numero
		, PE.pse_complemento as complemento
		, PE.pse_situacao as situacao
		, PE.pse_dataCriacao as datacriacao
		, PE.pse_dataAlteracao as dataalteracao
		, PE.pse_enderecoPrincipal as enderecoprincipal
		, PE.pse_latitude as latitude
		, PE.pse_longitude as longitude
		, E.end_cep
		, E.end_logradouro
		, E.end_bairro
		, E.end_distrito
		, E.end_zona
		, E.cid_id
		, E.end_situacao
		, E.end_dataCriacao
		, E.end_dataAlteracao
		, E.end_integridade
	FROM
		PES_PessoaEndereco PE WITH (NOLOCK) 
	INNER JOIN
		END_Endereco as E WITH (NOLOCK)
			ON PE.end_id = E.end_id
	WHERE		
		PE.pse_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		end_cep, end_logradouro
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_DiaNaoUtil_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_DiaNaoUtil_INSERT]
		@dnu_nome varchar(100)
		, @dnu_abrangencia TinyInt
		, @dnu_descricao varchar(400)
		, @dnu_data date
		, @dnu_recorrencia TinyInt
		, @dnu_vigenciaInicio date
		, @dnu_vigenciaFim date
		, @cid_id uniqueidentifier
		, @unf_id uniqueidentifier
		, @dnu_situacao TinyInt
		, @dnu_dataCriacao DateTime
		, @dnu_dataAlteracao DateTime


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_DiaNaoUtil
		( 
			dnu_nome
			, dnu_abrangencia
			, dnu_descricao
			, dnu_data
			, dnu_recorrencia
			, dnu_vigenciaInicio
			, dnu_vigenciaFim
			, cid_id
			, unf_id
			, dnu_situacao
			, dnu_dataCriacao
			, dnu_dataAlteracao
		)
	OUTPUT inserted.dnu_id INTO @ID
	VALUES
		( 
			@dnu_nome
			, @dnu_abrangencia
			, @dnu_descricao
			, @dnu_data
			, @dnu_recorrencia
			, @dnu_vigenciaInicio
			, @dnu_vigenciaFim
			, @cid_id
			, @unf_id
			, @dnu_situacao
			, @dnu_dataCriacao
			, @dnu_dataAlteracao
		)
	SELECT ID FROM @ID	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioSenhaHistorico_INSERT]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioSenhaHistorico_INSERT]
	@usu_id UniqueIdentifier
	, @ush_senha VarChar (256)
	, @ush_criptografia TinyInt
	, @ush_data DateTime

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_UsuarioSenhaHistorico
		( 
			usu_id 
			, ush_senha 
			, ush_criptografia 
			, ush_data 
 
		)
	OUTPUT inserted.usu_id INTO @ID
	VALUES
		( 
			@usu_id 
			, @ush_senha 
			, @ush_criptografia 
			, @ush_data 
 
		)
		
	SELECT ID FROM @ID
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_UPDATE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 25/01/2010 20:00
-- Description:	Altera o Tipo de Deficiencia preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_UPDATE]
		@tde_id uniqueidentifier		
		, @tde_nome varchar(100)
		, @tde_situacao TinyInt
		, @tde_dataAlteracao DateTime

AS
BEGIN
	UPDATE PES_TipoDeficiencia 
	SET 
		tde_nome = @tde_nome		
		, tde_situacao = @tde_situacao
		, tde_dataAlteracao = @tde_dataAlteracao
	WHERE 
		tde_id = @tde_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Servico_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Servico_INSERT]
	@ser_id SmallInt
	, @ser_nome NVarChar (200)
	, @ser_nomeProcedimento NVarChar (200)
	, @ser_ativo Bit

AS
BEGIN
	INSERT INTO 
		SYS_Servico
		( 
			ser_id 
			, ser_nome 
			, ser_nomeProcedimento 
			, ser_ativo 
 
		)
	VALUES
		( 
			@ser_id 
			, @ser_nome 
			, @ser_nomeProcedimento 
			, @ser_ativo 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupoUA_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupoUA_DELETE]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@ugu_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_UsuarioGrupoUA	
	WHERE 
		usu_id = @usu_id
	and gru_id = @gru_id
	and ugu_id = @ugu_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UnidadeAdministrativaEndereco]'
GO
CREATE TABLE [dbo].[SYS_UnidadeAdministrativaEndereco]
(
[ent_id] [uniqueidentifier] NOT NULL,
[uad_id] [uniqueidentifier] NOT NULL,
[uae_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Unida__uae_i__571DF1D5] DEFAULT (newsequentialid()),
[end_id] [uniqueidentifier] NOT NULL,
[uae_numero] [varchar] (20) NOT NULL,
[uae_complemento] [varchar] (100) NULL,
[uae_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaEndereco_uae_situacao] DEFAULT ((1)),
[uae_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaEndereco_uae_dataCriacao] DEFAULT (getdate()),
[uae_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_UnidadeAdministrativaEndereco_uae_dataAlteracao] DEFAULT (getdate()),
[uae_enderecoPrincipal] [bit] NULL,
[uae_latitude] [decimal] (15, 10) NULL,
[uae_longitude] [decimal] (15, 10) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UnidadeAdministrativaEndereco] on [dbo].[SYS_UnidadeAdministrativaEndereco]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaEndereco] ADD CONSTRAINT [PK_SYS_UnidadeAdministrativaEndereco] PRIMARY KEY CLUSTERED  ([ent_id], [uad_id], [uae_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_SYS_UnidadeAdministrativaEndereco_uad_id_end_id] on [dbo].[SYS_UnidadeAdministrativaEndereco]'
GO
CREATE NONCLUSTERED INDEX [IX_SYS_UnidadeAdministrativaEndereco_uad_id_end_id] ON [dbo].[SYS_UnidadeAdministrativaEndereco] ([uad_id]) INCLUDE ([end_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_CarregaEndereco]'
GO
-- =============================================
-- Author:		Gabriel Alves Scavassa
-- Create date: 11/02/2016 10:00
-- Description:	Seleciona o endereço de uma unidade administrativa
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_CarregaEndereco] 
	@ent_id UNIQUEIDENTIFIER,
	@uad_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		uae.ent_id,
		uae.uad_id,
		uae.uae_id as endRel_id,
		uae.end_id,
		uae.uae_numero as numero,
		uae.uae_complemento as complemento,
		uae.uae_situacao as situacao,
		uae.uae_dataCriacao as datacriacao,
		uae.uae_dataAlteracao as dataalteracao,
		uae.uae_enderecoPrincipal as enderecoprincipal,
		uae.uae_latitude as latitude,
		uae.uae_longitude as longitude,
		ende.end_cep,
		ende.end_logradouro,
		ende.end_bairro,
		ende.end_distrito,
		ende.end_zona,
		ende.cid_id,
		ende.end_situacao,
		ende.end_dataCriacao,
		ende.end_dataAlteracao,
		ende.end_integridade
	FROM
		SYS_UnidadeAdministrativaEndereco uae WITH(NOLOCK)
		INNER JOIN END_Endereco ende WITH(NOLOCK)
			ON uae.end_id = ende.end_id
			AND ende.end_situacao = 1 and uae.uae_situacao = 1
	WHERE
		uae.ent_id = @ent_id
		AND uae.uad_id = @uad_id
		AND uae.uae_situacao = 1
	ORDER BY
		uae.uae_dataAlteracao
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_DiaNaoUtil_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_DiaNaoUtil_DELETE]
	@dnu_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_DiaNaoUtil	
	WHERE 
		dnu_id = @dnu_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimasSenhas]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 23/04/2015
-- Description:	Seleciona as últimas senhas do usuário.
--				Retornas @qtdeSenhas senhas.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimasSenhas] 
	@usu_id UNIQUEIDENTIFIER,
	@qtdeSenhas INT
AS
BEGIN
	;WITH Dados AS
	(
		SELECT
			ush.usu_id, 
			ush.ush_senha, 
			ush.ush_criptografia, 
			ush.ush_id, 
			ush.ush_data,
			ROW_NUMBER() OVER (PARTITION BY ush.usu_id ORDER BY ush.ush_data DESC) AS linha
		FROM
			SYS_UsuarioSenhaHistorico ush WITH(NOLOCK)
		WHERE 
			ush.usu_id = @usu_id
	)

	SELECT
		usu_id, 
		ush_senha, 
		ush_criptografia, 
		ush_id, 
		ush_data,
		linha
	FROM
		Dados
	WHERE
		linha <= @qtdeSenhas
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_SelectBy_Nome]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 26/01/2010 09:02
-- Description:	utilizado na busca de nome de tipos de deficiencia, retorna quantidade
--				dos tipos de deficiencia que não foram excluídos logicamente,
--				filtrados por:
--					nome, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_SelectBy_Nome]	
	@tde_nome VARCHAR(100)	
	, @tde_id uniqueidentifier
	
AS
BEGIN
	SELECT 
		tde_id
		, tde_nome
	FROM
		PES_TipoDeficiencia	WITH (NOLOCK)	
	WHERE
		tde_situacao <> 3
		AND UPPER(tde_nome) = UPPER(@tde_nome) 
		AND	(@tde_id is null or tde_id <> @tde_id)
	ORDER BY
		tde_nome
	
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Servico_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Servico_UPDATE]
	@ser_id SMALLINT
	, @ser_nome NVARCHAR (200)
	, @ser_nomeProcedimento NVARCHAR (200)
	, @ser_ativo BIT

AS
BEGIN
	UPDATE SYS_Servico 
	SET 
		ser_nome = @ser_nome 
		, ser_nomeProcedimento = @ser_nomeProcedimento 
		, ser_ativo = @ser_ativo 

	WHERE 
		ser_id = @ser_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupo_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupo_SELECT]
	
AS
BEGIN
	SELECT 
		usu_id
		,gru_id
		,usg_situacao
	FROM 
		SYS_UsuarioGrupo WITH(NOLOCK) 
	
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_Configuracao]'
GO
CREATE TABLE [dbo].[CFG_Configuracao]
(
[cfg_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__CFG_Confi__cfg_i__14A88507] DEFAULT (newsequentialid()),
[cfg_chave] [varchar] (100) NOT NULL,
[cfg_valor] [varchar] (300) NOT NULL,
[cfg_descricao] [varchar] (200) NULL,
[cfg_situacao] [tinyint] NOT NULL CONSTRAINT [DF_CFG_Configuracao_cfg_situacao] DEFAULT ((1)),
[cfg_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_CFG_Configuracao_cfg_dataCriacao] DEFAULT (getdate()),
[cfg_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_CFG_Configuracao_cfg_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_Configuracao] on [dbo].[CFG_Configuracao]'
GO
ALTER TABLE [dbo].[CFG_Configuracao] ADD CONSTRAINT [PK_CFG_Configuracao] PRIMARY KEY CLUSTERED  ([cfg_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Configuracao_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Configuracao_INSERT]
	@cfg_chave VarChar (100)
	, @cfg_valor VarChar (300)
	, @cfg_descricao VarChar (200)
	, @cfg_situacao TinyInt
	, @cfg_dataCriacao DateTime
	, @cfg_dataAlteracao DateTime

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		CFG_Configuracao
		( 
			cfg_chave 
			, cfg_valor 
			, cfg_descricao 
			, cfg_situacao 
			, cfg_dataCriacao 
			, cfg_dataAlteracao 
 
		)
	OUTPUT inserted.cfg_id INTO @ID
	VALUES
		( 
			@cfg_chave 
			, @cfg_valor 
			, @cfg_descricao 
			, @cfg_situacao 
			, @cfg_dataCriacao 
			, @cfg_dataAlteracao 
 
		)
		
		SELECT ID FROM @ID
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_it_top_one]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 31/05/2010 14:23
-- Description:	utilizado para verificar o codigo do primeiro registro
--              cadastrado para a entidade e unidade administrativa
--				filtrados por:
--					entidade, unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_it_top_one]	
	@ent_id uniqueidentifier
	,@uad_id uniqueidentifier
AS
BEGIN
	SELECT 	
		TOP 1 uae_id
	FROM
		SYS_UnidadeAdministrativaEndereco WITH (NOLOCK)		
	WHERE
		    ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_situacao <> 3
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_TipoDocumentacao]'
GO
CREATE TABLE [dbo].[SYS_TipoDocumentacao]
(
[tdo_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_TipoD__tdo_i__0CBAE877] DEFAULT (newsequentialid()),
[tdo_nome] [varchar] (100) NOT NULL,
[tdo_sigla] [varchar] (10) NULL,
[tdo_validacao] [tinyint] NULL,
[tdo_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_TipoDocumentacao_tdo_situacao] DEFAULT ((1)),
[tdo_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoDocumentacao_tdo_dataCriacao] DEFAULT (getdate()),
[tdo_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoDocumentacao_tdo_dataAlteracao] DEFAULT (getdate()),
[tdo_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_TipoDocumentacao_tdo_integridade] DEFAULT ((0)),
[tdo_classificacao] [tinyint] NULL CONSTRAINT [DF_SYS_TipoDocumentacao_tdo_classificacao] DEFAULT ((99)),
[tdo_atributos] [varchar] (1024) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_TipoDocumentacao] on [dbo].[SYS_TipoDocumentacao]'
GO
ALTER TABLE [dbo].[SYS_TipoDocumentacao] ADD CONSTRAINT [PK_SYS_TipoDocumentacao] PRIMARY KEY CLUSTERED  ([tdo_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_INSERT]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_INSERT]
	  @tdo_nome VARCHAR (100)
	, @tdo_sigla VARCHAR (10)
	, @tdo_validacao TINYINT
	, @tdo_situacao TINYINT
	, @tdo_dataCriacao DATETIME
	, @tdo_dataAlteracao DATETIME
	, @tdo_integridade INT
	, @tdo_classificacao TINYINT = NULL
	, @tdo_atributos VARCHAR(1024) = NULL

AS
BEGIN
	
	DECLARE @tdo_classificacaoTmp TINYINT = 99

	-- Verifica se foi informado o parâmetro de classificação, caso contrário, assume o valor default do campo (99)
	IF (@tdo_classificacao IS NOT NULL) BEGIN
		SET @tdo_classificacaoTmp = @tdo_classificacao
	END 

	DECLARE @ID TABLE (ID UNIQUEIDENTIFIER)

	INSERT INTO SYS_TipoDocumentacao
		( 
			  tdo_nome 
			, tdo_sigla 
			, tdo_validacao 
			, tdo_situacao 
			, tdo_dataCriacao 
			, tdo_dataAlteracao 
			, tdo_integridade 
			, tdo_classificacao 
			, tdo_atributos
 		)
	
	OUTPUT 
		INSERTED.tdo_id INTO @ID

	VALUES
		( 
			  @tdo_nome 
			, @tdo_sigla 
			, @tdo_validacao 
			, @tdo_situacao 
			, @tdo_dataCriacao 
			, @tdo_dataAlteracao 
			, @tdo_integridade 
			, @tdo_classificacaoTmp
			, @tdo_atributos
		)
		
	SELECT ID FROM @ID
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModuloMenu_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModuloMenu_UPDATE]
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	, @msm_id INT
	, @vmm_ordem INT
	
AS
BEGIN
	UPDATE SYS_VisaoModuloMenu
	SET
		vmm_ordem = @vmm_ordem
	WHERE
		vis_id = @vis_id
		AND sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
		
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_AtualizaSituacao]'
GO
-- ===================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 29/04/2015
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído; 5 - Senha expirada) referente a
--				Usuarios. Filtrada por: 
--					usu_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_AtualizaSituacao]	
		@usu_id uniqueidentifier
		,@usu_situacao TINYINT
		,@usu_dataAlteracao DateTime
AS
BEGIN
	
	UPDATE SYS_Usuario
	SET 
		usu_situacao = @usu_situacao
		,usu_dataAlteracao = @usu_dataAlteracao
	WHERE 
		usu_id = @usu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 21/01/2010 11:37
-- Description:	utilizado na busca de Tipo Documentacao, retorna as entidades
--              que não foram excluídas logicamente,
--				filtrados por:
--					id, nome, sigla, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_SelectBy_All]	
		@tde_id uniqueidentifier
		, @tde_nome char(100)
		, @tde_situacao TinyInt
AS
BEGIN
	SELECT 
		tde_id
		,tde_nome
		, CASE tde_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS tde_situacao
	FROM
		PES_TipoDeficiencia	WITH (NOLOCK)	
	WHERE
		tde_situacao <> 3
		AND (@tde_id is null or tde_id = @tde_id)		
		AND (@tde_nome is null or tde_nome LIKE '%' + @tde_nome + '%')		
		AND (@tde_situacao is null or tde_situacao = @tde_situacao)				
	ORDER BY 
		tde_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Servico_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Servico_DELETE]
	@ser_id SMALLINT

AS
BEGIN
	DELETE FROM 
		SYS_Servico 
	WHERE 
		ser_id = @ser_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupo_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupo_INSERT]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@usg_situacao tinyint
AS
BEGIN
	INSERT INTO 
		SYS_UsuarioGrupo
		( 
			usu_id
			,gru_id
			,usg_situacao
		)
	VALUES
		( 
			@usu_id
			,@gru_id
			,@usg_situacao
		)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Configuracao_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Configuracao_SELECT]
	
AS
BEGIN
	SELECT 
		cfg_id
		,cfg_chave
		,cfg_valor
		,cfg_descricao
		,cfg_situacao
		,cfg_dataCriacao
		,cfg_dataAlteracao

	FROM 
		CFG_Configuracao WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeEndereco_SelectBy_ent_id_top_one]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 31/05/2010 14:23
-- Description:	utilizado para verificar o codigo do primeiro registro
--              cadastrado para a entidade
--				filtrados por:
--					entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeEndereco_SelectBy_ent_id_top_one]	
	@ent_id uniqueidentifier
AS
BEGIN
	SELECT 	
		TOP 1 ene_id
	FROM
		SYS_EntidadeEndereco WITH (NOLOCK)		
	WHERE
		ene_situacao <> 3
		AND ent_id = @ent_id
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacaoAtributo_SELECT]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacaoAtributo_SELECT]
	--@tdo_id UNIQUEIDENTIFIER
AS
BEGIN
	
	--DECLARE @tdo_atributosTmp VARCHAR(1024) = NULL

	--SET @tdo_atributosTmp = ( SELECT td.tdo_atributos FROM SYS_TipoDocumentacao td WITH (NOLOCK) WHERE td.tdo_id = @tdo_id )

	--
	SELECT 
		  tda.tda_id
		, tda.tda_descricao
		, tda.tda_nomeObjeto
		, ISNULL(tda.tda_default, 0) AS tda_default
		--, CASE 
		--	WHEN ISNULL(@tdo_atributosTmp, '') <> '' THEN 
		--		CAST(SUBSTRING(@tdo_atributosTmp, tda.tda_id, 1) AS BIT)
		--	ELSE
		--		CAST(0 AS BIT)
		--END
		--AS selecionado

	FROM 
		SYS_TipoDocumentacaoAtributo tda WITH (NOLOCK) 

	ORDER BY 
		tda.tda_descricao
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModuloMenu_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModuloMenu_SELECT]
	
AS
BEGIN
	SELECT 
		vis_id
		, sis_id
		, mod_id
		, msm_id
		, vmm_ordem
		
	FROM 
		SYS_VisaoModuloMenu WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelecionaPorEmailEntidade]'
GO
-- ===========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 05/05/2015
-- Description:	Consulta de usuário filtrado por email e entidade.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelecionaPorEmailEntidade]
	@ent_id UNIQUEIDENTIFIER
	, @usu_email VARCHAR(500)
AS
BEGIN
	SELECT
		usu_id,
		usu_integracaoAD,
		usu_situacao,
		pes_nome
	FROM SYS_Usuario usu WITH(NOLOCK)	
	LEFT JOIN PES_Pessoa pes WITH(NOLOCK)
		ON usu.pes_id = pes.pes_id
		AND pes.pes_situacao <> 3
	WHERE
		usu.ent_id = @ent_id
		AND usu.usu_email = @usu_email
		AND usu_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 09:53
-- Description:	Seleciona o valor do campo integridade da tabela de tipo de deficiencia
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_Select_Integridade]
		@tde_id uniqueidentifier
AS
BEGIN
	SELECT 			
		tde_integridade
	FROM
		PES_TipoDeficiencia WITH (NOLOCK)
	WHERE 
		tde_id = @tde_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Servico_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Servico_SELECT]
	
AS
BEGIN
	SELECT 
		ser_id
		,ser_nome
		,ser_nomeProcedimento
		,ser_ativo

	FROM 
		SYS_Servico WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupo_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupo_DELETE]
	@usu_id uniqueidentifier
	,@gru_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_UsuarioGrupo
	WHERE 
		usu_id = @usu_id 
	and gru_id = @gru_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoEscolaridade_MAX_tes_ordem]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 02/06/2010 12:50
-- Description:	Retorna valor máximo de tes_ordem da tabela
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoEscolaridade_MAX_tes_ordem]	
AS
BEGIN
	SELECT 
		MAX(tes_ordem) as tes_ordem
	FROM
		PES_TipoEscolaridade WITH (NOLOCK)		
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeEndereco_CarregaEnderecos_SelectBy_ent_id]'
GO
-- ========================================================================
-- Author:		Gabriel Alves Scavassa
-- Create date: 12/02/2016 14:00
-- Description:	Retorna todos os endereços da entidade e campos cidade
--				filtrados por:  @ent_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeEndereco_CarregaEnderecos_SelectBy_ent_id]	
	@ent_id uniqueidentifier
AS
BEGIN
	SELECT
		SEE.ene_id as endRel_id
		, E.end_id
		, SEE.ene_numero as numero
		, SEE.ene_complemento as complemento
		, SEE.ene_situacao as situacao
		, SEE.ene_dataCriacao as datacriacao
		, SEE.ene_dataAlteracao as dataalteracao
		, SEE.ene_enderecoPrincipal as enderecoprincipal
		, SEE.ene_latitude as latitude
		, SEE.ene_longitude as longitude
		, E.end_cep
		, E.end_logradouro
		, E.end_bairro
		, E.end_distrito
		, E.end_zona
		, E.cid_id
		, E.end_situacao
		, E.end_dataCriacao
		, E.end_dataAlteracao
		, E.end_integridade
	FROM
		SYS_EntidadeEndereco SEE WITH (NOLOCK) 
		INNER JOIN END_Endereco as E WITH (NOLOCK) ON SEE.end_id = E.end_id
	WHERE		
		SEE.ent_id = @ent_id
		and SEE.ene_situacao <> 3
	ORDER BY
		E.end_cep, E.end_logradouro
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModuloMenu_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModuloMenu_LOAD] 
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	, @msm_id INT
	
AS
BEGIN
	SELECT Top 1 
		vis_id
		, sis_id
		, mod_id
		, msm_id
		, vmm_ordem
 	FROM
 		 SYS_VisaoModuloMenu
	WHERE 
		vis_id = @vis_id
		AND sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
		
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 8:50
-- Description:	Incrementa uma unidade no campo integridade do tipo de deficiencia.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_INCREMENTA_INTEGRIDADE]
		@tde_id uniqueidentifier

AS
BEGIN
	UPDATE PES_TipoDeficiencia 
	SET 
		tde_integridade = tde_integridade + 1 
	WHERE 
		tde_id = @tde_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InsereServico]'
GO
-- ==========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 28/05/2014
-- Description:	Insere serviços com valores passados. 
--				Verifica se o nome do procedimento do serviço ou o ID do serviço
--				já foi inserido, e não permite duplicar.
--				@ser_id - Obrigatório - ID do serviço
--				@ser_nome - Obrigatório - Nome do serviço
--				@ser_nomeProcedimento - Obrigatório - Nome da StoredProcedure
--				@ser_ativo - Obrigatório - Indica se o serviço estará ativo
-- ==========================================================================
CREATE PROCEDURE [dbo].[MS_InsereServico]
	@ser_id SMALLINT
	, @ser_nome NVARCHAR (100)
	, @ser_nomeProcedimento NVARCHAR (100)
	, @ser_ativo BIT
AS
BEGIN
	-- Verifica se o id do serviço já foi inserido.
	DECLARE @qtID INT = 
	(
		SELECT COUNT(*)
		FROM SYS_Servico WITH(NOLOCK)
		WHERE
			ser_id= @ser_id
	)
	
	IF (@qtID > 0)
	BEGIN
		PRINT 'O id do serviço ' + CAST(@ser_id AS VARCHAR(10)) + ' já existe no sistema (' + CAST(@qtID AS VARCHAR) + ').'; 
	END
	ELSE
	BEGIN	
		-- Busca se o serviço já existe.
		DECLARE @qt INT = 
		(
			SELECT COUNT(*)
			FROM SYS_Servico WITH(NOLOCK)
			WHERE
				LOWER(RTRIM(LTRIM(ser_nomeProcedimento))) = LOWER(RTRIM(LTRIM(@ser_nomeProcedimento)))
		)
		
		IF (@qt IS NOT NULL AND @qt > 0)
		BEGIN
			PRINT 'O serviço ' + @ser_nomeProcedimento + ' já existe no sistema (' + CAST(@qt AS VARCHAR) + ').'; 
		END
		ELSE
		BEGIN
			INSERT INTO SYS_Servico
				   (
						ser_id,					
						ser_nome,
						ser_nomeProcedimento,
						ser_ativo
				   )
			 VALUES
				   (
						@ser_id,
						@ser_nome,
						@ser_nomeProcedimento,
						@ser_ativo
					)
			
			IF (@@ROWCOUNT = 0)
				PRINT 'O serviço não foi incluído.';
			ELSE
				PRINT 'Serviço incluído com sucesso.';

		END
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SelectBy_sis_id_usu_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 19/05/2010 18:00
-- Description:	Carrega os grupos do usuário para um determinado sistema
--				ordenado pelo nome do grupo.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SelectBy_sis_id_usu_id]
	@sis_id int
	, @usu_id uniqueidentifier
AS
BEGIN
	SELECT 
		SYS_Grupo.gru_id
		, SYS_Grupo.gru_nome
		, SYS_Grupo.gru_situacao
		, SYS_Grupo.gru_dataCriacao
		, SYS_Grupo.gru_dataAlteracao
		, SYS_Grupo.vis_id
		, SYS_Grupo.sis_id		
	FROM
		SYS_Grupo WITH (NOLOCK)
	INNER JOIN SYS_UsuarioGrupo WITH (NOLOCK)
		ON SYS_Grupo.gru_id = SYS_UsuarioGrupo.gru_id
	WHERE
		SYS_Grupo.gru_situacao IN (1, 4)
		AND usg_situacao = 1
		AND SYS_Grupo.sis_id = @sis_id
		AND SYS_UsuarioGrupo.usu_id = @usu_id
	ORDER BY
		SYS_Grupo.gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Configuracao_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Configuracao_LOAD]
	@cfg_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 cfg_id  
		, cfg_chave 
		, cfg_valor 
		, cfg_descricao 
		, cfg_situacao 
		, cfg_dataCriacao 
		, cfg_dataAlteracao 

 	FROM
 		CFG_Configuracao
	WHERE 
		cfg_id = @cfg_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacaoAtributo_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacaoAtributo_LOAD]
	@tda_id TINYINT
	
AS
BEGIN
	SELECT
		TOP 1
		  tda_id  
		, tda_descricao 
		, tda_nomeObjeto 
		, tda_default 
 	FROM
 		SYS_TipoDocumentacaoAtributo
	WHERE 
		tda_id = @tda_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModuloMenu_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModuloMenu_INSERT]
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	, @msm_id INT
	, @vmm_ordem INT
	
AS
BEGIN
	INSERT INTO
		SYS_VisaoModuloMenu
		(
			vis_id
			, sis_id
			, mod_id
			, msm_id
			, vmm_ordem
		)
		VALUES
		(
			@vis_id
			, @sis_id
			, @mod_id
			, @msm_id
			, @vmm_ordem
		)
		
	SELECT ISNULL(@@ROWCOUNT, -1)
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_TipoDeficiencia_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 8:50
-- Description:	Decrementa uma unidade no campo integridade do tipo de deficiencia.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_TipoDeficiencia_DECREMENTA_INTEGRIDADE]
		@tde_id uniqueidentifier

AS
BEGIN
	UPDATE PES_TipoDeficiencia 
	SET 
		tde_integridade = tde_integridade - 1
	WHERE 
		tde_id = @tde_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Servico_SelecionaServicos]'
GO
-- ========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 28/05/2014
-- Description:	Retorna todos os serviços cadastrados
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Servico_SelecionaServicos]
AS
BEGIN
	SELECT 
		ser_id,
		ser_nome,
		ser_nomeProcedimento,
		ser_ativo,
		CASE ser_ativo WHEN 0 THEN 'Não'
					   WHEN 1 THEN 'Sim'
					   ELSE NULL 
		END AS ser_ativoDescricao
	FROM
		SYS_Servico WITH (NOLOCK)	
	ORDER BY
		ser_nome, 
		ser_nomeProcedimento
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_ParametroGrupoPerfil]'
GO
CREATE TABLE [dbo].[SYS_ParametroGrupoPerfil]
(
[pgs_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Param__pgs_i__7F60ED59] DEFAULT (newsequentialid()),
[pgs_chave] [varchar] (100) NOT NULL,
[gru_id] [uniqueidentifier] NOT NULL,
[pgs_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_ParametroGrupoPerfil_pgs_situacao] DEFAULT ((1)),
[pgs_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_ParametroGrupoPerfil_pgs_dataCriacao] DEFAULT (getdate()),
[pgs_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_ParametroGrupoPerfil_pgs_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_ParametroGrupoPerfil] on [dbo].[SYS_ParametroGrupoPerfil]'
GO
ALTER TABLE [dbo].[SYS_ParametroGrupoPerfil] ADD CONSTRAINT [PK_SYS_ParametroGrupoPerfil] PRIMARY KEY CLUSTERED  ([pgs_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_UPDATE]
	@pgs_id uniqueidentifier
	, @pgs_chave VARCHAR (100)
	, @gru_id uniqueidentifier
	, @pgs_situacao TINYINT
	, @pgs_dataCriacao DATETIME
	, @pgs_dataAlteracao DATETIME

AS
BEGIN
	UPDATE SYS_ParametroGrupoPerfil 
	SET 
		pgs_chave = @pgs_chave 
		, gru_id = @gru_id 
		, pgs_situacao = @pgs_situacao 
		, pgs_dataCriacao = @pgs_dataCriacao 
		, pgs_dataAlteracao = @pgs_dataAlteracao 

	WHERE 
		pgs_id = @pgs_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating trigger [dbo].[TRG_SYS_ModuloSiteMap_Identity] on [dbo].[SYS_ModuloSiteMap]'
GO
-- =============================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 04/06/2010 10:15
-- Description:	Realiza o autoincremento do 
--				campo msm_id garantindo que
--				sempre será reiniciado em 1
--				qdo um sis_id e mod_id for inserido
-- =============================================
CREATE TRIGGER [dbo].[TRG_SYS_ModuloSiteMap_Identity]
ON [dbo].[SYS_ModuloSiteMap] INSTEAD OF INSERT
AS
BEGIN
	DECLARE @ID INT
	SELECT 
		@ID = CASE WHEN MAX(SYS_ModuloSiteMap.msm_id) IS NULL THEN 1 ELSE MAX(SYS_ModuloSiteMap.msm_id)+1 END 
	FROM 
		SYS_ModuloSiteMap WITH(XLOCK,TABLOCK) 
		INNER JOIN inserted
			ON SYS_ModuloSiteMap.sis_id = inserted.sis_id
			AND SYS_ModuloSiteMap.mod_id = inserted.mod_id
	/* INSERE O ID AUTOINCREMENTO */
	INSERT INTO SYS_ModuloSiteMap (sis_id, mod_id, msm_id, msm_nome, msm_descricao, msm_url, msm_informacoes)
    SELECT sis_id, mod_id, @ID, msm_nome, msm_descricao, msm_url, msm_informacoes FROM inserted
    /* RETORNA INSERT */
    SELECT ISNULL(@ID, -1)     
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacaoAtributo_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacaoAtributo_INSERT]
	  @tda_id TINYINT
	, @tda_descricao VARCHAR(64)
	, @tda_nomeObjeto VARCHAR(256) = NULL
	, @tda_default BIT = 0

AS
BEGIN
	INSERT INTO 
		SYS_TipoDocumentacaoAtributo
		( 
			  tda_id 
			, tda_descricao 
			, tda_nomeObjeto 
			, tda_default 
 		)
	VALUES
		( 
			  @tda_id 
			, @tda_descricao 
			, @tda_nomeObjeto 
			, @tda_default 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModuloMenu_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModuloMenu_DELETE]
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	, @msm_id INT
	
AS
BEGIN
	DELETE FROM 
		SYS_VisaoModuloMenu
	WHERE
		vis_id = @vis_id
		AND sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_SELECTBY_gru_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_SELECTBY_gru_id]
	@gru_id uniqueidentifier
AS
BEGIN
	SELECT
		pgs_id
		,pgs_chave
		,gru_id
		,pgs_situacao
		,pgs_dataCriacao
		,pgs_dataAlteracao

	FROM
		SYS_ParametroGrupoPerfil WITH(NOLOCK)
	WHERE 
		gru_id = @gru_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Todos]'
GO
-- ========================================================================
-- Author:		Paula Fiorini
-- Create date: 02/08/2011
-- Description:	Retorna as Unidades Administrativas pelos filtros informados,
--				pela entidade..
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Todos]
	@tua_id UNIQUEIDENTIFIER	
	, @ent_id UNIQUEIDENTIFIER	
AS
BEGIN
	SELECT	
		UA.tua_id
		, uad_id
		, uad_idSuperior
		, uad_nome
	FROM
		SYS_UnidadeAdministrativa UA WITH (NOLOCK)		
	INNER JOIN SYS_Entidade Ent WITH (NOLOCK)
		ON Ent.ent_id = UA.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa Tua WITH (NOLOCK)
		ON UA.tua_id = Tua.tua_id	
	WHERE
		uad_situacao <> 3
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND(@tua_id IS NULL OR UA.tua_id = @tua_id)
		
		-- Somente da Entidade informada.
		AND (UA.ent_id = @ent_id)
		
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Configuracao_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Configuracao_UPDATE]
	@cfg_id UNIQUEIDENTIFIER
	, @cfg_chave VARCHAR (100)
	, @cfg_valor VARCHAR (300)
	, @cfg_descricao VARCHAR (200)
	, @cfg_situacao TINYINT
	, @cfg_dataCriacao DATETIME
	, @cfg_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_Configuracao 
	SET 
		cfg_chave = @cfg_chave 
		, cfg_valor = @cfg_valor 
		, cfg_descricao = @cfg_descricao 
		, cfg_situacao = @cfg_situacao 
		, cfg_dataCriacao = @cfg_dataCriacao 
		, cfg_dataAlteracao = @cfg_dataAlteracao 

	WHERE 
		cfg_id = @cfg_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_SELECTBY_Sistema_Situacao]'
GO
CREATE PROCEDURE  [dbo].[NEW_SYS_Sistema_SELECTBY_Sistema_Situacao]
     @sis_id int
	,@sis_nome VARCHAR(100)	
	,@sis_situacao TINYINT
AS
BEGIN
	SELECT 
		 sis_id
		,sis_nome
		, CASE sis_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS sis_situacao
	FROM
		SYS_Sistema WITH (NOLOCK)
	WHERE
		sis_situacao <> 3
					
	ORDER BY
		sis_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacaoAtributo_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacaoAtributo_UPDATE]
	  @tda_id TINYINT
	, @tda_descricao VARCHAR(64)
	, @tda_nomeObjeto VARCHAR(256)
	, @tda_default BIT

AS
BEGIN
	UPDATE 
		SYS_TipoDocumentacaoAtributo 
	SET 
		  tda_descricao = @tda_descricao 
		, tda_nomeObjeto = @tda_nomeObjeto 
		, tda_default = @tda_default 
	WHERE 
		tda_id = @tda_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaContato_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaContato_UPDATE]
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uac_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@uac_contato VarChar (200)
		,@uac_situacao TinyInt
		,@uac_dataCriacao DateTime
		,@uac_dataAlteracao DateTime

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaContato
	SET 		
		tmc_id = @tmc_id
		,uac_contato = @uac_contato
		,uac_situacao = @uac_situacao
		,uac_dataCriacao = @uac_dataCriacao
		,uac_dataAlteracao = @uac_dataAlteracao
	WHERE 
		ent_id = @ent_id
	and uad_id = @uad_id
	and	uac_id = @uac_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_SELECT]
	
AS
BEGIN
	SELECT 
		pgs_id
		,pgs_chave
		,gru_id
		,pgs_situacao
		,pgs_dataCriacao
		,pgs_dataAlteracao

	FROM 
		SYS_ParametroGrupoPerfil WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_URL]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 08/08/2011 13:45
-- Description:	Verifica se já existe uma mesma URL no sistema
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_URL]	
	@sis_id INT
	, @mod_id INT
	, @msm_id INT
	, @msm_url VARCHAR(500)
AS
BEGIN
	SELECT 
		msm.msm_id
		, msm.msm_url
	FROM
		SYS_ModuloSiteMap msm WITH (NOLOCK)
	INNER JOIN SYS_Modulo modu WITH (NOLOCK)
		ON modu.sis_id = msm.sis_id
			AND modu.mod_id = msm.mod_id
	WHERE
		mod_situacao <> 3
		AND modu.sis_id = @sis_id
		AND LTRIM(RTRIM(UPPER(msm.msm_url COLLATE Latin1_General_CI_AI))) = LTRIM(RTRIM(UPPER(@msm_url COLLATE Latin1_General_CI_AI)))		
		AND (@msm_id IS NULL OR (
									(msm.mod_id = @mod_id AND msm.msm_id <> @msm_id)
									OR
									(msm.mod_id <> @mod_id)
								)
			)
	ORDER BY
		msm.msm_url	
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Configuracao_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Configuracao_DELETE]
	@cfg_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		CFG_Configuracao 
	WHERE 
		cfg_id = @cfg_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacaoAtributo_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacaoAtributo_DELETE]
	@tda_id TINYINT

AS
BEGIN
	DELETE FROM 
		SYS_TipoDocumentacaoAtributo 
	WHERE 
		tda_id = @tda_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaContato_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaContato_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,uad_id
		,uac_id
		,tmc_id
		,uac_contato
		,uac_situacao
		,uac_dataCriacao
		,uac_dataAlteracao
	FROM 
		SYS_UnidadeAdministrativaContato WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Cron_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Cron_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[CRON_EXPRESSION] [nvarchar] (120) NOT NULL,
[TIME_ZONE_ID] [nvarchar] (80) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_CRON_TRIGGERS] on [dbo].[QTZ_Cron_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Cron_Triggers] ADD CONSTRAINT [PK_QTZ_CRON_TRIGGERS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_QTZ_Cron_Triggers_SelecionaExpressaoPorTrigger]'
GO
-- ========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 28/05/2014
-- Description: Seleciona a expressão Cron por trigger
-- ========================================================================
CREATE PROCEDURE [dbo].[MS_QTZ_Cron_Triggers_SelecionaExpressaoPorTrigger] 
	@trigger VARCHAR(150)
AS 
BEGIN
    SELECT
		CRON_EXPRESSION
	FROM
		QTZ_Cron_Triggers WITH(NOLOCK)
	WHERE
		TRIGGER_NAME = @trigger
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_LOAD]
	@pgs_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		 pgs_id  
		, pgs_chave 
		, gru_id 
		, pgs_situacao 
		, pgs_dataCriacao 
		, pgs_dataAlteracao 

 	FROM
 		SYS_ParametroGrupoPerfil
	WHERE 
		pgs_id = @pgs_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_SistemaEntidade]'
GO
CREATE TABLE [dbo].[SYS_SistemaEntidade]
(
[sis_id] [int] NOT NULL,
[ent_id] [uniqueidentifier] NOT NULL,
[sen_chaveK1] [varchar] (100) NULL,
[sen_urlAcesso] [varchar] (200) NULL,
[sen_logoCliente] [varchar] (2000) NULL,
[sen_urlCliente] [varchar] (1000) NULL,
[sen_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_SistemaEntidade_sen_situacao] DEFAULT ((1))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_SistemaEntidade] on [dbo].[SYS_SistemaEntidade]'
GO
ALTER TABLE [dbo].[SYS_SistemaEntidade] ADD CONSTRAINT [PK_SYS_SistemaEntidade] PRIMARY KEY CLUSTERED  ([sis_id], [ent_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_ServidorRelatorio]'
GO
CREATE TABLE [dbo].[CFG_ServidorRelatorio]
(
[sis_id] [int] NOT NULL,
[ent_id] [uniqueidentifier] NOT NULL,
[srr_id] [int] NOT NULL,
[srr_nome] [varchar] (100) NOT NULL,
[srr_descricao] [varchar] (1000) NULL,
[srr_remoteServer] [bit] NOT NULL CONSTRAINT [DF_CFG_ServidorRelatorio_srr_remoteServer] DEFAULT ((1)),
[srr_usuario] [varchar] (512) NULL,
[srr_senha] [varchar] (512) NULL,
[srr_dominio] [varchar] (512) NULL,
[srr_diretorioRelatorios] [varchar] (1000) NULL,
[srr_pastaRelatorios] [varchar] (1000) NOT NULL,
[srr_situacao] [tinyint] NOT NULL CONSTRAINT [DF_CFG_ServidorRelatorio_srr_situacao] DEFAULT ((1)),
[srr_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_CFG_ServidorRelatorio_srr_dataCriacao] DEFAULT (getdate()),
[srr_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_CFG_ServidorRelatorio_srr_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_ServidorRelatorio] on [dbo].[CFG_ServidorRelatorio]'
GO
ALTER TABLE [dbo].[CFG_ServidorRelatorio] ADD CONSTRAINT [PK_CFG_ServidorRelatorio] PRIMARY KEY CLUSTERED  ([sis_id], [ent_id], [srr_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_RelatorioServidorRelatorio]'
GO
CREATE TABLE [dbo].[CFG_RelatorioServidorRelatorio]
(
[sis_id] [int] NOT NULL,
[ent_id] [uniqueidentifier] NOT NULL,
[srr_id] [int] NOT NULL,
[rlt_id] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_RelatorioServidorRelatorio] on [dbo].[CFG_RelatorioServidorRelatorio]'
GO
ALTER TABLE [dbo].[CFG_RelatorioServidorRelatorio] ADD CONSTRAINT [PK_CFG_RelatorioServidorRelatorio] PRIMARY KEY CLUSTERED  ([sis_id], [ent_id], [srr_id], [rlt_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_Relatorio]'
GO
CREATE TABLE [dbo].[CFG_Relatorio]
(
[rlt_id] [int] NOT NULL,
[rlt_nome] [varchar] (100) NOT NULL,
[rlt_situacao] [tinyint] NOT NULL CONSTRAINT [DF_CFG_Relatorio_rlt_situacao] DEFAULT ((1)),
[rlt_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_CFG_Relatorio_rlt_dataCriacao] DEFAULT (getdate()),
[rlt_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_CFG_Relatorio_rlt_dataAlteracao] DEFAULT (getdate()),
[rlt_integridade] [int] NOT NULL CONSTRAINT [DF_CFG_Relatorio_rlt_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_Relatorio] on [dbo].[CFG_Relatorio]'
GO
ALTER TABLE [dbo].[CFG_Relatorio] ADD CONSTRAINT [PK_CFG_Relatorio] PRIMARY KEY CLUSTERED  ([rlt_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InsereRelatorio]'
GO
-- =============================================
-- Author:		Carla
-- Create date: 06/12/2011
-- Description:	Insere um relatório nas tabelas de configuração de relatórios
--				CFG_Relatorio, CFG_RelatorioServidorRelatorio
-- =============================================
--Parâmetros: 
--		@nomeSistema : Obrigatório
--			Nome do sistema que será incluído o menu (passar nome exato)
--		@rlt_id : Obrigatório
--			ID do relatório -> igual ao enumerador do sistema
--		@rlt_nome : Obrigatorio
--			Nome do relatorio , como a descricção do enumerador do sistema.
CREATE PROCEDURE [dbo].[MS_InsereRelatorio]
	@nomeSistema VARCHAR(100)
	, @rlt_id INT
	, @rlt_nome VARCHAR(100)
AS
BEGIN
	
	IF (NOT @nomeSistema is NULL) AND (NOT @rlt_id IS NULL) AND (NOT @rlt_nome IS NULL)
	BEGIN
	
		-- ID do sistema
		DECLARE @sis_id INT = 0
		SELECT TOP 1
			@sis_id = sis_id
		FROM
			SYS_Sistema WITH(NOLOCK)
		WHERE
			sis_nome = @nomeSistema
		
		IF (ISNULL(@sis_id, 0) <> 0)
		BEGIN
			IF NOT EXISTS(SELECT rlt_id FROM CFG_Relatorio WITH(NOLOCK) WHERE rlt_id = @rlt_id )
				INSERT INTO CFG_Relatorio(rlt_id,rlt_nome, rlt_situacao) VALUES (@rlt_id, @rlt_nome,1)
			ELSE
				PRINT 'O relatório ' + @rlt_nome + ' já existe no sistema.';
			
			IF ((SELECT COUNT(*) FROM SYS_SistemaEntidade Sen WITH(NOLOCK) WHERE sis_id = @sis_id) > 0)
			BEGIN
			
				INSERT INTO CFG_RelatorioServidorRelatorio
				(sis_id, ent_id, srr_id, rlt_id)
				SELECT 
					Sen.sis_id, Sen.ent_id, Srr.srr_id, @rlt_id
				FROM SYS_SistemaEntidade Sen WITH(NOLOCK)
				INNER JOIN CFG_ServidorRelatorio Srr WITH(NOLOCK)
					ON (Srr.sis_id = Sen.sis_id)
						AND (Srr.ent_id = Sen.ent_id)
				WHERE
					Sen.sis_id = @sis_id
					AND Sen.sen_situacao = 1
					AND Srr.srr_situacao = 1
				EXCEPT
				SELECT 
					Ser.sis_id, Ser.ent_id, Ser.srr_id, Ser.rlt_id
				FROM CFG_RelatorioServidorRelatorio Ser WITH(NOLOCK)
			
				--IF NOT EXISTS 
				--(SELECT rlt_id FROM CFG_RelatorioServidorRelatorio WITH(NOLOCK) WHERE ent_id = @ent_id AND rlt_id = @rlt_id AND sis_id = @sis_id)
				--	INSERT INTO CFG_RelatorioServidorRelatorio (sis_id, ent_id, srr_id, rlt_id)
				--		SELECT TOP 1 
				--			sis_id, ent_id, srr_id, @rlt_id FROM CFG_RelatorioServidorRelatorio 
				--		WHERE ent_id = @ent_id AND sis_id = @sis_id
				
			END
			ELSE
			BEGIN
				PRINT 'O sistema ' + @nomeSistema + ' não possui nenhuma entidade ligada a ele.';
			END
			
			PRINT 'Fim da configuração do relatório.';
		END
		ELSE
		BEGIN
			PRINT 'O sistema não foi encontrado.'
		END
	END
	ELSE
	BEGIN
		PRINT 'Relatório não foi inserido pois nem todos os parâmetros obrigatórios foram informados.'
	END
			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_Sis_id]'
GO
-- =============================================
-- Author:		Nícolas Gavlak
-- Create date: 11/06/2010
-- Description:	Select filtrando pai e filhos, configuracao de Auditoria
-- =============================================
CREATE PROCEDURE  [dbo].[NEW_SYS_Modulo_SelectBy_Sis_id]
     @sis_id INT
		
AS
BEGIN
	SELECT 		
		 folha.mod_id
		,(CASE WHEN paipai.mod_nome IS NULL THEN pai.mod_nome +' \ '+ folha.mod_nome
		ELSE paipai.mod_nome +' \ '+ pai.mod_nome +' \ '+ folha.mod_nome END) AS mod_nome
		,folha.mod_auditoria
		,folha.mod_nome as mod_nome_original
		
	FROM
		SYS_Modulo folha WITH(NOLOCK)
	INNER JOIN SYS_Modulo pai WITH(NOLOCK)
		ON folha.sis_id = pai.sis_id
		AND folha.mod_idPai = pai.mod_id
	LEFT JOIN SYS_Modulo paipai WITH(NOLOCK)
		ON pai.sis_id = paipai.sis_id 
		AND pai.mod_idPai = paipai.mod_id
		AND paipai.mod_situacao <> 3
	WHERE
		folha.mod_situacao <> 3
		AND pai.mod_situacao <> 3
		AND folha.mod_idPai is not null 
		AND (SELECT COUNT(*) FROM SYS_Modulo folhaPai WITH(NOLOCK) WHERE folhaPai.sis_id = folha.sis_id AND folhaPai.mod_idPai = folha.mod_id ) = 0		
		AND (folha.sis_id = @sis_id)
				
	ORDER BY 
		mod_nome
	
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacaoAtributo_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacaoAtributo_SELECT]
	
AS
BEGIN
	SELECT 
		  tda_id
		, tda_descricao
		, tda_nomeObjeto
		, tda_default
	FROM 
		SYS_TipoDocumentacaoAtributo WITH (NOLOCK) 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaContato_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaContato_LOAD]
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uac_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		ent_id
		,uad_id
		,uac_id
		,tmc_id
		,uac_contato
		,uac_situacao
		,uac_dataCriacao
		,uac_dataAlteracao
 	FROM
 		SYS_UnidadeAdministrativaContato
	WHERE 
		ent_id = @ent_id
	and uad_id = @uad_id
	and	uac_id = @uac_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Servico_SelecionaNomeProcedimentoPorServico]'
GO
-- ========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 28/05/2014
-- Description:	Seleciona nome do job pelo ID do serviço
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Servico_SelecionaNomeProcedimentoPorServico]	
	@ser_id SMALLINT
AS
BEGIN
	SELECT
		ser_nomeProcedimento
	FROM
		SYS_Servico WITH (NOLOCK)
	WHERE
		ser_id = @ser_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_INSERT]
	@pgs_chave VarChar (100)
	, @gru_id uniqueidentifier
	, @pgs_situacao TinyInt
	, @pgs_dataCriacao DateTime
	, @pgs_dataAlteracao DateTime

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_ParametroGrupoPerfil
		( 
			pgs_chave 
			, gru_id 
			, pgs_situacao 
			, pgs_dataCriacao 
			, pgs_dataAlteracao 
 
		)
	OUTPUT inserted.pgs_id INTO @ID
	VALUES
		( 
			@pgs_chave 
			, @gru_id 
			, @pgs_situacao 
			, @pgs_dataCriacao 
			, @pgs_dataAlteracao 
 
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InsereSiteMap]'
GO

-- =============================================
-- Author:		Paulo Ricardo Barbosa Menezes
-- Create date: 10/10/2011 as 09:57
-- Description:	Insere um sitemap no sistema
-- =============================================
-- Author: Diego Fadanni
-- Alter date: 16/01/2014 
-- Foram inseridos os parâmetros de pai e avo, pois podem haver dois módulos com o mesmo nome
-- =============================================
/* Parâmetros:
		@nomeSistema : Obrigatório
			Nome do sistema que será incluído o menu (passar nome exato)
		@nomeModulo : Obrigatório
			Nome do novo módulo no menu
		@nomeModuloPai : Opcional
			Nome do módulo Pai que será incluído o menu (passar nome exato)
		@SiteMapNome : Obrigatório
			Nome do primeiro sitemap que será incluído para o módulo
		@SiteMapUrl : Obrigatório
			Url do primeiro sitemap que será incluído para o módulo
		@SiteMapUrlHelp : Opcional
			Url do help do sitemap que será incluído para o módulo
		@SiteMapDescricao : Opcional
			Descrição do sitemap que será incluído para o módulo
*/

CREATE PROCEDURE [dbo].[MS_InsereSiteMap]
	 @nomeSistema VARCHAR(100)
	 , @nomeModulo VARCHAR(50)
	 , @nomeModuloPai VARCHAR(50) = ''
	 , @nomeModuloAvo VARCHAR(50) = ''
	 , @SiteMapNome VARCHAR(50) = ''
	 , @SiteMapUrl VARCHAR(500) = ''
	 , @SiteMapUrlHelp VARCHAR(500) = ''
     , @SiteMapDescricao VARCHAR(1000) = NULL
	
AS
BEGIN
	SET @nomeModuloAvo = ISNULL(@nomeModuloAvo, '');
	SET @nomeModuloPai = ISNULL(@nomeModuloPai, '');
	SET @SiteMapNome = ISNULL(@SiteMapNome, '');
	SET @SiteMapUrl = ISNULL(@SiteMapUrl, '');
	
	-- ID do sistema
	DECLARE @sis_id INT = 0
	SELECT TOP 1
		@sis_id = sis_id
	FROM
		SYS_Sistema WITH(NOLOCK)
	WHERE
		sis_nome = @nomeSistema
		AND sis_situacao <> 3
	
	-- ID do módulo pai
	DECLARE @mod_idPai INT = 0;
	
	IF (ISNULL(@nomeModuloPai, '') = '')
	BEGIN
		-- Se não passou módulo pai, é null (módulo raiz).
		SET @mod_idPai = 0
	END
	ELSE
	BEGIN
		IF (@nomeModuloAvo = '')
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloPai + ';';
			-- Se não passou módulo avô, busca o módulo pai só pelo nome.
			SELECT TOP 1
				@mod_idPai = mod_id
			FROM
				SYS_Modulo WITH(NOLOCK)
			WHERE
				sis_id = @sis_id
				AND mod_nome = @nomeModuloPai
				AND mod_situacao <> 3
		END
		ELSE
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloAvo + ' -> ' + @nomeModuloPai + ';';
			-- Se passou o módulo avô, busca o módulo pai pelo módulo avô e pelo módulo pai.
			SELECT TOP 1
				@mod_idPai = ModPai.mod_id
			FROM
				SYS_Modulo ModPai WITH(NOLOCK)
			WHERE
				ModPai.sis_id = @sis_id
				AND ModPai.mod_nome = @nomeModuloPai
				AND ModPai.mod_situacao <> 3
				AND ModPai.mod_idPai = 
				(
					SELECT TOP 1
						mod_id
					FROM
						SYS_Modulo ModAvo WITH(NOLOCK)
					WHERE
						ModAvo.sis_id = @sis_id
						-- Módulo avô.
						AND ModAvo.mod_nome = @nomeModuloAvo
						AND ModAvo.mod_situacao <> 3
				)
		END
	END
	
	PRINT 'ID do módulo pai: ' + CAST(@mod_idPai as varchar(10));
		
	-- ID do módulo 
	DECLARE @mod_id INT = 0
	SELECT TOP 1 @mod_id = mod_id 
	FROM 
		SYS_Modulo WITH(NOLOCK) 
	WHERE mod_situacao <> 3 AND sis_id = @sis_id 
		AND (@mod_idPai = '' OR mod_idPai = @mod_idPai) AND mod_nome = @nomeModulo

	--Guarda as ocorrências ao criar o menu
	DECLARE @Mensagem VARCHAR(MAX) = 'Ínicio do processo de inclusão de siteMap do módulo do menu.'
	
    if(@sis_id > 0 AND @mod_id > 0)
    BEGIN   
      -- SiteMap 
	IF (@SiteMapUrl <> '' AND @SiteMapNome <> '' AND 
	   (NOT EXISTS(SELECT msm_nome FROM SYS_ModuloSiteMap SiteMap WITH(NOLOCK) INNER JOIN SYS_Modulo Modulo WITH(NOLOCK) ON SiteMap.mod_id = Modulo.mod_id AND SiteMap.sis_id = Modulo.sis_id
				   WHERE mod_situacao <> 3 AND Modulo.sis_id = @sis_id AND msm_url = @SiteMapUrl)))
	BEGIN
	    --Grava o SiteMap
		INSERT INTO SYS_ModuloSiteMap 
		(sis_id, mod_id, msm_nome, msm_descricao, msm_url, msm_informacoes, msm_urlHelp)
		VALUES (@sis_id, @mod_id, @SiteMapNome, @SiteMapDescricao, @SiteMapUrl, NULL, @SiteMapUrlHelp)
	END
	ELSE
	BEGIN
		IF (@SiteMapUrl = '' OR @SiteMapNome = '')
			SET @Mensagem = @Mensagem + CHAR(13) + ' A Url ou o nome do SiteMap está em branco.'
		ELSE
			SET @Mensagem = @Mensagem + CHAR(13) + ' A Url ' + @SiteMapUrl + ' já existe cadastrado no sistema.'
			
	IF (NOT EXISTS (SELECT msm_nome FROM SYS_ModuloSiteMap WITH(NOLOCK) WHERE sis_id = @sis_id AND mod_id = @mod_id))
	BEGIN
		--Grava o SiteMap caso não exista nenhum para o módulo
		INSERT INTO SYS_ModuloSiteMap 
		(sis_id, mod_id, msm_nome, msm_descricao, msm_url, msm_informacoes)
		VALUES (@sis_id, @mod_id, @nomeModulo, @SiteMapDescricao, '~/Index.aspx?mod_id=' + CAST(@mod_id AS VARCHAR(MAX)), NULL)
			END
	    END
	END   
	ELSE
	BEGIN
		IF (@sis_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O sistema não foi encontrado.'

		IF (@mod_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O módulo não foi encontrado.'
	END
    
	SET @Mensagem = @Mensagem + CHAR(13) + ' Fim do processo de inclusão de siteMap do módulo do menu.'

	-- Retorna mensagem	
	PRINT @Mensagem
END
   	


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InserePaginaMenu]'
GO

-- =============================================
-- Author:		Carla e Haila
-- Create date: 16/08/2011
-- Description:	Insere um módulo no menu do sistema, aceitando no máximo 3 sitemaps.
/*
	Parâmetros: 
		@nomeSistema : Obrigatório
			Nome do sistema que será incluído o menu (passar nome exato)
			
		@nomeModuloPai : Obrigatório
			Nome do módulo Pai que será incluído o menu (passar nome exato)
			
		@nomeModulo : Obrigatório
			Nome do novo módulo no menu
		
		@SiteMap1Nome : Obrigatório
			Nome do primeiro sitemap que será incluído para o módulo 
		
		@SiteMap1Url : Obrigatório
			Url do primeiro sitemap que será incluído para o módulo
		
		@SiteMap2Nome : Opcional (passar NULL para não inserir)
			Nome do segundo sitemap que será incluído para o módulo 
		
		@SiteMap2Url : Opcional (passar NULL para não inserir)
			Url do segundo sitemap que será incluído para o módulo
		
		@SiteMap3Nome : Opcional (passar NULL para não inserir)
			Nome do terceiro sitemap que será incluído para o módulo 
		
		@SiteMap3Url : Opcional (passar NULL para não inserir)
			Url do terceiro sitemap que será incluído para o módulo
		
		@possuiVisaoAdm : Obrigatório
			Se será inserido permissão para visão Adm para o módulo (só será dado permissão automaticamente
				para os grupos com situação = 4-Padrão do sistema, para os outros será inserido o registro
				sem permissão para nada).

		@possuiVisaoGestao : Obrigatório
			Se será inserido permissão para visão Adm para o módulo (só será dado permissão automaticamente
				para os grupos com situação = 4-Padrão do sistema, para os outros será inserido o registro
				sem permissão para nada).

		@possuiVisaoUA : Obrigatório
			Se será inserido permissão para visão Adm para o módulo (só será dado permissão automaticamente
				para os grupos com situação = 4-Padrão do sistema, para os outros será inserido o registro
				sem permissão para nada).
		
		@possuiVisaoIndividual : Obrigatório
			Se será inserido permissão para visão Adm para o módulo (só será dado permissão automaticamente
				para os grupos com situação = 4-Padrão do sistema, para os outros será inserido o registro
				sem permissão para nada).

		@SiteMap1UrlHelp : Opcional (passar NULL para não inserir)
			Url do help do primeiro sitemap que será incluído para o módulo

		@SiteMap2UrlHelp : Opcional (passar NULL para não inserir)
			Url do help do segundo sitemap que será incluído para o módulo

		@SiteMap3UrlHelp : Opcional (passar NULL para não inserir)
			Url do help do terceiro sitemap que será incluído para o módulo

		@SiteMap1Descricao : Opcional (passar NULL para não inserir)
			Descrição do primeiro sitemap que será incluído para o módulo

		@SiteMap2Descricao : Opcional (passar NULL para não inserir)
			Descrição do segundo sitemap que será incluído para o módulo

		@SiteMap3Descricao : Opcional (passar NULL para não inserir)
			Descrição do terceiro sitemap que será incluído para o módulo

		@DescricaoModulo : Opcional
			Descrição do módulo que será incluído

		@nomeModuloAvo : Opcional
			Nome do módulo pai do módulo pai (se passado, será utilizado para buscar o módulo pai - 
			para o caso de módulos com o mesmo nome).
*/
-- =============================================
CREATE PROCEDURE [dbo].[MS_InserePaginaMenu]
	@nomeSistema VARCHAR(100)
	, @nomeModuloPai VARCHAR(50)
	, @nomeModulo VARCHAR(300)
	, @SiteMap1Nome VARCHAR(50) = NULL
	, @SiteMap1Url VARCHAR(500) = NULL
	, @SiteMap2Nome VARCHAR(50) = NULL
	, @SiteMap2Url VARCHAR(500) = NULL
	, @SiteMap3Nome VARCHAR(50) = NULL
	, @SiteMap3Url VARCHAR(500) = NULL
	, @possuiVisaoAdm BIT = 1
	, @possuiVisaoGestao BIT = 0
	, @possuiVisaoUA BIT = 0
	, @possuiVisaoIndividual BIT = 0
	, @SiteMap1UrlHelp VARCHAR(500) = NULL
	, @SiteMap2UrlHelp VARCHAR(500) = NULL
	, @SiteMap3UrlHelp VARCHAR(500) = NULL

	, @SiteMap1Descricao VARCHAR(1000) = NULL
	, @SiteMap2Descricao VARCHAR(1000) = NULL
	, @SiteMap3Descricao VARCHAR(1000) = NULL
	, @DescricaoModulo Text = NULL

	, @nomeModuloAvo VARCHAR(50) = ''
AS
BEGIN
	SET @SiteMap1Nome = ISNULL(@SiteMap1Nome, '');
	SET @SiteMap1Url = ISNULL(@SiteMap1Url, '');
	SET @SiteMap1UrlHelp = ISNULL(@SiteMap1UrlHelp, '');
	SET @SiteMap2Nome = ISNULL(@SiteMap2Nome, '');
	SET @SiteMap2Url = ISNULL(@SiteMap2Url, '');
	SET @SiteMap2UrlHelp = ISNULL(@SiteMap2UrlHelp, '');
	SET @SiteMap3Nome = ISNULL(@SiteMap3Nome, '');
	SET @SiteMap3Url = ISNULL(@SiteMap3Url, '');
	SET @SiteMap3UrlHelp = ISNULL(@SiteMap3UrlHelp, '');
	
	SET @nomeModuloAvo = ISNULL(@nomeModuloAvo, '');

	--Guarda as ocorrências ao criar o menu
	DECLARE @Mensagem VARCHAR(MAX) = 'Ínicio do processo de inclusão de módulo do menu.'

	-- Tabela de visões que terão acesso à tela.
	DECLARE @tbVisoesInserir TABLE
	(
		vis_id INT NOT NULL
	)

	-- ID do sistema
	DECLARE @sis_id INT = 0
	SELECT TOP 1
		@sis_id = sis_id
	FROM
		SYS_Sistema WITH(NOLOCK)
	WHERE
		sis_nome = @nomeSistema
		AND sis_situacao <> 3
	
	PRINT 'Sistema: ' + CAST(@sis_id AS VARCHAR(10));

	-- ID do módulo pai
	DECLARE @mod_idPai INT = 0;
	
	IF (ISNULL(@nomeModuloPai, '') = '')
	BEGIN
		-- Se não passou módulo pai, é null (módulo raiz).
		SET @mod_idPai = NULL
	END
	ELSE
	BEGIN
		IF (@nomeModuloAvo = '')
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloPai + ';';
			-- Se não passou módulo avô, busca o módulo pai só pelo nome.
			SELECT TOP 1
				@mod_idPai = mod_id
			FROM
				SYS_Modulo WITH(NOLOCK)
			WHERE
				sis_id = @sis_id
				AND mod_nome = @nomeModuloPai
				AND mod_situacao <> 3
		END
		ELSE
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloAvo + ' -> ' + @nomeModuloPai + ';';
			-- Se passou o módulo avô, busca o módulo pai pelo módulo avô e pelo módulo pai.
			SELECT TOP 1
				@mod_idPai = ModPai.mod_id
			FROM
				SYS_Modulo ModPai WITH(NOLOCK)
			WHERE
				ModPai.sis_id = @sis_id
				AND ModPai.mod_nome = @nomeModuloPai
				AND ModPai.mod_situacao <> 3
				AND ModPai.mod_idPai = 
				(
					SELECT TOP 1
						mod_id
					FROM
						SYS_Modulo ModAvo WITH(NOLOCK)
					WHERE
						ModAvo.sis_id = @sis_id
						-- Módulo avô.
						AND ModAvo.mod_nome = @nomeModuloAvo
						AND ModAvo.mod_situacao <> 3
				)
		END
	END
	
	PRINT 'ID do módulo pai: ' + CAST(@mod_idPai as varchar(10));
	
	-- Configura as visões que verão o módulo (com permissão total).
	IF (@possuiVisaoAdm = 1)
	BEGIN
		INSERT INTO @tbVisoesInserir (vis_id) SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Administração'
	END

	IF (@possuiVisaoGestao = 1)
	BEGIN
		INSERT INTO @tbVisoesInserir (vis_id) SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Gestão'
	END
	
	IF (@possuiVisaoUA = 1)
	BEGIN
		INSERT INTO @tbVisoesInserir (vis_id) SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Unidade Administrativa'
	END
	
	IF (@possuiVisaoIndividual = 1)
	BEGIN
		INSERT INTO @tbVisoesInserir (vis_id) SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Individual'
	END

	IF (@sis_id > 0 AND ISNULL(@mod_idPai, 1) > 0 AND (SELECT COUNT(*) FROM @tbVisoesInserir) > 0)
	BEGIN
		IF NOT EXISTS(SELECT mod_id FROM SYS_Modulo WITH(NOLOCK) WHERE mod_situacao <> 3 AND sis_id = @sis_id AND 
										(@mod_idPai IS NULL OR mod_idPai = @mod_idPai) AND mod_nome = @nomeModulo)
		BEGIN	
			INSERT INTO SYS_MODULO (sis_id, mod_nome, mod_descricao, mod_idPai, mod_auditoria, mod_situacao)
			VALUES (@sis_id, @nomeModulo, @DescricaoModulo, @mod_idPai, 0, 1)
			
			
			DECLARE @mod_id INT = (SELECT ISNULL(MAX(mod_id),0) FROM SYS_Modulo WITH(NOLOCK) WHERE sis_id = @sis_id)
		
			-- SiteMap 1
			EXEC MS_InsereSiteMap @nomeSistema, @nomeModulo, @nomeModuloPai, @nomeModuloAvo, @SiteMap1Nome, @SiteMap1Url, @SiteMap1UrlHelp, @SiteMap1Descricao

			-- SiteMap 2
			EXEC MS_InsereSiteMap @nomeSistema, @nomeModulo, @nomeModuloPai, @nomeModuloAvo, @SiteMap2Nome, @SiteMap2Url, @SiteMap2UrlHelp, @SiteMap2Descricao

			-- SiteMap 3
			EXEC MS_InsereSiteMap @nomeSistema, @nomeModulo, @nomeModuloPai, @nomeModuloAvo, @SiteMap3Nome, @SiteMap3Url, @SiteMap3UrlHelp, @SiteMap3Descricao

			-- Visões.
			INSERT INTO SYS_VisaoModulo (vis_id, sis_id, mod_id)
				SELECT 
					vis_id, @sis_id, @mod_id
				FROM -- Visões que terão acesso a tela. 
					@tbVisoesInserir

			-- Descobrir último valor de ordem cadastrado no menu.
			DECLARE @ordem INT
			SET @ordem = 
			(
				SELECT MAX(ISNULL(vmm_ordem, 0)) + 1
				FROM 
					SYS_VisaoModuloMenu Visao WITH(NOLOCK)
					INNER JOIN SYS_Modulo Modulo WITH(NOLOCK)
						ON (Modulo.mod_id = Visao.mod_id)
						AND (Modulo.sis_id = Visao.sis_id)
				WHERE 
					@mod_idPai IS NULL OR Modulo.mod_idPai = @mod_idPai	
					AND Visao.sis_id = @sis_id
			)
			SET @ordem = ISNULL(@ordem, 1)
			
			DECLARE @msm_id INT = 0
			SET @msm_id = (SELECT TOP 1 msm_id FROM SYS_ModuloSiteMap WITH(NOLOCK) WHERE sis_id = @sis_id AND mod_id = @mod_id ORDER BY msm_id)
			
			IF (@msm_id > 0)
				INSERT INTO SYS_VisaoModuloMenu (vmm_ordem, vis_id, sis_id, mod_id, msm_id)
					SELECT @ordem, vis_id, sis_id, mod_id, @msm_id FROM SYS_VisaoModulo WITH(NOLOCK) WHERE sis_id = @sis_id AND mod_id = @mod_id
			
			INSERT INTO SYS_GrupoPermissao 
				SELECT
					gru_id,
					@sis_id,
					@mod_id,
					CASE WHEN (gru_situacao = 4) THEN 1 ELSE 0 END,
					CASE WHEN (gru_situacao = 4) THEN 1 ELSE 0 END,
					CASE WHEN (gru_situacao = 4) THEN 1 ELSE 0 END,
					CASE WHEN (gru_situacao = 4) THEN 1 ELSE 0 END
				FROM
					SYS_Grupo WITH(NOLOCK)
				WHERE 
					sis_id = @sis_id	
					AND vis_id IN (SELECT vis_id FROM @tbVisoesInserir)

		END
		ELSE
		BEGIN
			
			SET @Mensagem = @Mensagem + CHAR(13) + ' O módulo já existe no sistema.'
			
		END
	END
	ELSE
	BEGIN
		IF (@sis_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O sistema não foi encontrado.'
		
		IF (@mod_idPai <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O módulo pai não foi encontrado.'
		
		IF ((SELECT COUNT(*) FROM @tbVisoesInserir) = 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' As visões não foram encontrados.'
	END	
	
	SET @Mensagem = @Mensagem + CHAR(13) + ' Fim da inclusão do módulo do menu.'

	-- Retorna mensagem	
	PRINT @Mensagem

END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_SELECTBy_Classificacao]'
GO
-- ========================================================================
-- Author:		Gabriel Augusto Moreli
-- Create date: 16/02/2016 12:12
-- Description:	utilizado na busca de nome de tipos de documentacao, retorna quantidade
--				dos tipos de documentacao que não foram excluídos logicamente,
--				filtrados por:
--					classifição, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_SELECTBy_Classificacao]
	  @tdo_classificacao TINYINT
	, @tdo_id UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		  tdo_id
		, tdo_classificacao
 	FROM
 		SYS_TipoDocumentacao WITH (NOLOCK)
 	WHERE
 		tdo_situacao <> 3
		AND tdo_classificacao = @tdo_classificacao
		AND (@tdo_id is null or tdo_id <> @tdo_id)
	ORDER BY
		tdo_nome
	
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaContato_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaContato_INSERT]
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@uac_contato VarChar (200)
		,@uac_situacao TinyInt
		,@uac_dataCriacao DateTime
		,@uac_dataAlteracao DateTime


AS
BEGIN
DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_UnidadeAdministrativaContato
		( 
			ent_id
			,uad_id
			,tmc_id
			,uac_contato
			,uac_situacao
			,uac_dataCriacao
			,uac_dataAlteracao
		)
	OUTPUT inserted.uac_id INTO @ID
	VALUES
		( 
			@ent_id
			,@uad_id
			,@tmc_id
			,@uac_contato
			,@uac_situacao
			,@uac_dataCriacao
			,@uac_dataAlteracao
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelecionaEndereco]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 03/06/2014
-- Description:	Seleciona o endereço de uma unidade administrativa
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelecionaEndereco] 
	@ent_id UNIQUEIDENTIFIER,
	@uad_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		uae.ent_id,
		uae.uad_id,
		uae.uae_id,
		uae.end_id,
		uae.uae_numero,
		uae.uae_complemento,
		uae.uae_situacao,
		uae.uae_dataCriacao,
		uae.uae_dataAlteracao,
		uae.uae_enderecoPrincipal,
		uae.uae_latitude,
		uae.uae_longitude,
		ende.end_cep,
		ende.end_logradouro,
		ende.end_bairro,
		ende.end_distrito,
		ende.end_zona,
		ende.cid_id,
		ende.end_situacao,
		ende.end_dataCriacao,
		ende.end_dataAlteracao,
		ende.end_integridade
	FROM
		SYS_UnidadeAdministrativaEndereco uae WITH(NOLOCK)
		INNER JOIN END_Endereco ende WITH(NOLOCK)
			ON uae.end_id = ende.end_id
			AND ende.end_situacao = 1 and uae.uae_situacao = 1
	WHERE
		uae.ent_id = @ent_id
		AND uae.uad_id = @uad_id
		AND uae.uae_situacao = 1
	ORDER BY
		uae.uae_dataAlteracao
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ParametroGrupoPerfil_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ParametroGrupoPerfil_DELETE]
	@pgs_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_ParametroGrupoPerfil 
	WHERE 
		pgs_id = @pgs_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_SelectBy_Nome]'
GO
-- ========================================================================
-- Author:		Heitor Henrique Martins
-- Create date: 22/08/2011 11:30
-- Description:	Seleciona o id da pessoa de acordo com seu nome.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_SelectBy_Nome]
		@pes_nome varchar(200)
AS
BEGIN
	SELECT 			
		pes_id
		, pes_nome
	FROM
		PES_Pessoa WITH (NOLOCK)
	WHERE 
		pes_nome  = @pes_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaContato_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaContato_DELETE]
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uac_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_UnidadeAdministrativaContato	
	WHERE 
		ent_id = @ent_id
	and uad_id = @uad_id
	and	uac_id = @uac_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_UsuarioAD]'
GO
CREATE TABLE [dbo].[LOG_UsuarioAD]
(
[usa_id] [bigint] NOT NULL IDENTITY(1, 1),
[usu_id] [uniqueidentifier] NOT NULL,
[usa_acao] [tinyint] NOT NULL,
[usa_status] [tinyint] NOT NULL,
[usa_dataAcao] [datetime] NOT NULL,
[usa_origemAcao] [tinyint] NOT NULL,
[usa_dataProcessado] [datetime] NULL,
[usa_dados] [varchar] (max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_LOG_UsuarioAD] on [dbo].[LOG_UsuarioAD]'
GO
ALTER TABLE [dbo].[LOG_UsuarioAD] ADD CONSTRAINT [PK_LOG_UsuarioAD] PRIMARY KEY CLUSTERED  ([usa_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_UsuarioAD_SelecionaNaoProcessados]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 04/06/2014
-- Description:	Seleciona todos os históricos de alteração de senha não processados.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_LOG_UsuarioAD_SelecionaNaoProcessados] 
AS
BEGIN
	;WITH tbUsuarioSenhaAD AS
	(
		SELECT
			usa.usa_id,
			usa.usu_id,
			usa.usa_acao,
			usa.usa_status,
			usa.usa_dataAcao,
			usa.usa_origemAcao,
			usa.usa_dataProcessado,
			usa.usa_dados 
		FROM
			LOG_UsuarioAD usa WITH(NOLOCK)
		WHERE
			usa.usa_status = 1
	)
	
	SELECT
		usa.usa_id,
		usa.usu_id,
		usa.usa_acao,
		usa.usa_status,
		usa.usa_dataAcao,
		usa.usa_origemAcao,
		usa.usa_dataProcessado,
		usa.usa_dados,
		usu.usu_login,
		usu.usu_dominio,
		usu.usu_email,
		usu.usu_senha,
		usu.usu_criptografia,
		usu.usu_situacao,
		usu.usu_dataCriacao,
		usu.usu_dataAlteracao,
		usu.pes_id,
		usu.usu_integridade,
		usu.ent_id,
		usu.usu_integracaoAD,
		pes.pes_nome,
		pes.pes_nome_abreviado,
		pes.pai_idNacionalidade,
		pes.pes_naturalizado,
		pes.cid_idNaturalidade,
		pes.pes_dataNascimento,
		pes.pes_estadoCivil,
		pes.pes_racaCor,
		pes.pes_sexo,
		pes.pes_idFiliacaoPai,
		pes.pes_idFiliacaoMae,
		pes.tes_id,
		pes.pes_foto,
		pes.pes_situacao,
		pes.pes_dataCriacao,
		pes.pes_dataAlteracao,
		pes.pes_integridade,
		pes.arq_idFoto
	FROM
		tbUsuarioSenhaAD usa
		LEFT JOIN SYS_Usuario usu WITH(NOLOCK)
			ON usa.usu_id = usu.usu_id
		LEFT JOIN PES_Pessoa pes WITH(NOLOCK)
			ON usu.pes_id = pes.pes_id
	ORDER BY 
		usa.usa_dataAcao
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_Updateby_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/02/2010 13:50
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Entidade. Filtrada por: 
--					ent_id, enc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_Updateby_Situacao]	
		@pgs_id uniqueidentifier
		, @pgs_dataAlteracao DATETIME
		, @pgs_situacao tinyint
AS
BEGIN
	UPDATE SYS_ParametroGrupoPerfil
	SET 
		pgs_situacao = @pgs_situacao
		, pgs_dataAlteracao = @pgs_dataAlteracao
	WHERE 
		pgs_id = @pgs_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_UPDATE]
		  @ent_id uniqueidentifier
		, @usu_id uniqueidentifier
		, @usu_login VarChar (500)
		, @usu_email VarChar (500)
		, @usu_senha VarChar (256)
		, @usu_criptografia TinyInt
		, @usu_situacao TinyInt
		, @usu_dataCriacao DateTime
		, @usu_dataAlteracao DateTime
		, @pes_id uniqueidentifier
		, @usu_integridade Int
		, @usu_dominio varchar(100)
		--,@usu_integracaoAD TINYINT removido para funcionar com o sistema em versão anterior a 1.22.0.0
		, @usu_integracaoExterna TINYINT = NULL
AS
BEGIN
	
	--	
	DECLARE 
		@usu_integracaoExternaTmp TINYINT

	-- Valida se o parâmetro @@usu_integracaoExterna é nulo, para colocar o valor default do campo
	IF (@usu_integracaoExterna IS NULL) BEGIN
		
		SET @usu_integracaoExternaTmp = (	SELECT	u.usu_integracaoExterna
											FROM	SYS_Usuario u WITH (NOLOCK) 
											WHERE	u.usu_id = @usu_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @usu_integracaoExternaTmp = @usu_integracaoExterna
	END 


	-- Atualiza o registro do usuário
	UPDATE
		SYS_Usuario
	SET 
		  ent_id = @ent_id
		, usu_login = @usu_login
		, usu_email = @usu_email
		, usu_senha = @usu_senha
		, usu_criptografia = @usu_criptografia 
		, usu_situacao = @usu_situacao
		, usu_dataCriacao = @usu_dataCriacao
		, usu_dataAlteracao = @usu_dataAlteracao
		, pes_id = @pes_id
		, usu_integridade = @usu_integridade
		, usu_dominio = @usu_dominio
		, usu_integracaoExterna = @usu_integracaoExterna
		--,usu_integracaoAD = @usu_integracaoAD
	WHERE 
		usu_id = @usu_id
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Configuracao_SelecionaValorPorChave]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 05/06/2014
-- Description:	Seleciona valor de configuração do sistema pela chave.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_Configuracao_SelecionaValorPorChave] 
	@cfg_chave varchar(100)
AS
BEGIN
	SELECT TOP 1
		cfg.cfg_valor
	FROM
		CFG_Configuracao cfg WITH(NOLOCK)
	WHERE
		cfg.cfg_chave = @cfg_chave
		AND cfg.cfg_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_UPDATE]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/02/2010 13:50
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Entidade. Filtrada por: 
--					ent_id, enc_id
-- ====================================================================
create PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_UPDATE]	
		@pgs_id uniqueidentifier
		, @pgs_chave VARCHAR(100)
		, @gru_id uniqueidentifier
		, @pgs_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_ParametroGrupoPerfil
	SET 
		pgs_chave = @pgs_chave
		, gru_id = @gru_id
		, pgs_dataAlteracao = @pgs_dataAlteracao
	WHERE 
		pgs_id = @pgs_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,usu_id
		,usu_login
		,usu_email
		,usu_senha
		,usu_criptografia
		,usu_situacao
		,usu_dataCriacao
		,usu_dataAlteracao
		,pes_id
		,usu_integridade
		,usu_dominio
		,usu_integracaoAD
		,usu_integracaoExterna
	FROM 
		SYS_Usuario WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Pais_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 20/01/2010 12:05
-- Description:	utilizado na busca de países, retorna os países
--              que não foram excluídos logicamente,
--				filtrados por:
--					id, nome, sigla, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Pais_SelectBy_All]	
	@pai_id uniqueidentifier
	,@pai_nome VARCHAR(100)
	,@pai_sigla VARCHAR(10)		
	,@pai_situacao TINYINT
AS
BEGIN
	SELECT 
		pai_id
		,pai_nome
		,pai_sigla
		,pai_naturalMasc
		,pai_naturalFem		
		, CASE pai_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS pai_situacao
	FROM
		END_Pais WITH (NOLOCK)		
	WHERE
		pai_situacao <> 3
		AND (@pai_id is null or pai_id = @pai_id)		
		AND (@pai_nome is null or pai_nome LIKE '%' + @pai_nome + '%')
		AND (@pai_sigla is null or pai_sigla = @pai_sigla)				
		AND (@pai_situacao is null or pai_situacao = @pai_situacao)				
	ORDER BY
		pai_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelecionaPorLogin]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 17/06/2014
-- Description:	Seleciona um usuário pelo login.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelecionaPorLogin] 
	@usu_login VARCHAR(500)
AS
BEGIN
	SELECT
		usu.usu_id,
		usu.usu_login,
		usu.usu_dominio,
		usu.usu_email,
		usu.usu_senha,
		usu.usu_criptografia,
		usu.usu_situacao,
		usu.usu_dataCriacao,
		usu.usu_dataAlteracao,
		usu.pes_id,
		usu.usu_integridade,
		usu.ent_id,
		usu.usu_integracaoAD,
		usu_integracaoExterna
	FROM
		SYS_Usuario usu WITH(NOLOCK)
	WHERE
		usu.usu_login = @usu_login
		AND usu.usu_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_pgs_chave]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 19/03/2010 15:15
-- Description:	utilizado no cadastro de usuarios
--              para verificar os grupos a serem cadastrados
--              para o usuários
--				filtrados por:
--					pgs_chave
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_pgs_chave]	
	@pgs_chave VARCHAR(100)
AS 
BEGIN
	SELECT 	
		gru_id
	FROM
		SYS_ParametroGrupoPerfil WITH (NOLOCK)		
	WHERE
		pgs_situacao <> 3
		AND pgs_chave = @pgs_chave
	ORDER BY
		gru_id DESC
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Configuracao_SELECT]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 18/03/2011 10:24
-- Description:	Retorna todas as configurações ativas
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Configuracao_SELECT]

AS
BEGIN
	SELECT 
		cfg_id
		, cfg_chave
		, cfg_valor
		, cfg_descricao
		, cfg_situacao
		, cfg_dataCriacao
		, cfg_dataAlteracao
	FROM
		CFG_Configuracao WITH (NOLOCK)
	WHERE
		cfg_situacao <> 3			
	ORDER BY
		cfg_situacao desc, cfg_dataCriacao
		
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_UPDATE_Auditoria]'
GO

-- ========================================================================
-- Author:		Nícolas Gavlak
-- Create date: 17/06/2010 11:59
-- Description:	Altera a opção de auditoria de um modulo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_UPDATE_Auditoria]
		@sis_id int
		, @mod_id int
		, @mod_auditoria bit
		, @mod_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_Modulo 
	SET 
		
		mod_auditoria = @mod_auditoria
		, mod_dataAlteracao = @mod_dataAlteracao
		
	WHERE 
		sis_id = @sis_id
		AND mod_id = @mod_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_IntegracaoExternaTipo]'
GO
CREATE TABLE [dbo].[SYS_IntegracaoExternaTipo]
(
[iet_id] [tinyint] NOT NULL,
[iet_descricao] [varchar] (128) NOT NULL,
[iet_qtdeDiasAutenticacao] [int] NOT NULL CONSTRAINT [DF_SYS_IntegracaoExternaTipo_iet_QtdeDiasAutenticacao] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_IntegracaoExternaTipo] on [dbo].[SYS_IntegracaoExternaTipo]'
GO
ALTER TABLE [dbo].[SYS_IntegracaoExternaTipo] ADD CONSTRAINT [PK_SYS_IntegracaoExternaTipo] PRIMARY KEY CLUSTERED  ([iet_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExternaTipo_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExternaTipo_LOAD]
	@iet_id TinyInt
	
AS
BEGIN
	SELECT	Top 1
		 iet_id  
		, iet_descricao 
		, iet_qtdeDiasAutenticacao 

 	FROM
 		SYS_IntegracaoExternaTipo
	WHERE 
		iet_id = @iet_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_LOAD]
	@usu_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		 usu_id
		,usu_login
		,usu_email
		,usu_senha
		,usu_criptografia 
		,usu_situacao
		,usu_dataCriacao
		,usu_dataAlteracao
		,pes_id
		,usu_integridade
		,ent_id
		,usu_dominio
		,usu_integracaoAD
		,usu_integracaoExterna
 	FROM
 		SYS_Usuario
	WHERE 
		usu_id = @usu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Pais_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:35
-- Description:	Seleciona o valor do campo integridade da tabela de pais
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Pais_Select_Integridade]
		@pai_id uniqueidentifier
AS
BEGIN
	SELECT 			
		pai_integridade
	FROM
		END_Pais WITH (NOLOCK)
	WHERE 
		pai_id = @pai_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_Nome]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 17/03/2010 10:22
-- Description:	Verifica se já existe um parâmetro grupo perfil
--				com o mesmo nome e grupo
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_Nome]	
	@pgs_chave VARCHAR(100)	
	, @gru_id UNIQUEIDENTIFIER
	, @pgs_id UNIQUEIDENTIFIER	
AS
BEGIN
	SELECT 
		pgs_id
		, pgs_chave
	FROM
		SYS_ParametroGrupoPerfil WITH (NOLOCK)
	WHERE
		pgs_situacao <> 3
		AND UPPER(pgs_chave) = UPPER(@pgs_chave)
		AND gru_id = @gru_id
		AND (@pgs_id is null or pgs_id <> @pgs_id)		
	ORDER BY
		pgs_chave
		
	SELECT @@ROWCOUNT		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Configuracao_UPDATE]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 21/03/2011 09:31
-- Description:	Altera a configuração preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Configuracao_UPDATE]
	@cfg_id UNIQUEIDENTIFIER
	, @cfg_chave VARCHAR (100)
	, @cfg_valor VARCHAR (300)
	, @cfg_descricao VARCHAR (200)
	, @cfg_situacao TINYINT
	, @cfg_dataAlteracao DATETIME
AS
BEGIN
	UPDATE CFG_Configuracao 
	SET 
		cfg_chave = @cfg_chave 
		, cfg_valor = @cfg_valor 
		, cfg_descricao = @cfg_descricao 
		, cfg_situacao = @cfg_situacao 
		, cfg_dataAlteracao = @cfg_dataAlteracao 

	WHERE 
		cfg_id = @cfg_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_UPDATE_UrlIntegracao]'
GO
-- ========================================================================
-- Author:		Nícolas Gavlak
-- Create date: 23/06/2010 12:19
-- Description:	Altera o caminho e a Url integração
CREATE PROCEDURE [dbo].[NEW_SYS_Sistema_UPDATE_UrlIntegracao]
		@sis_id int
		, @sis_caminho Varchar (2000)
		, @sis_urlIntegracao Varchar (200)
AS
BEGIN
	UPDATE SYS_Sistema
	SET 		
		sis_caminho = @sis_caminho
		, sis_urlIntegracao = @sis_urlIntegracao
		
	WHERE 
		sis_id = @sis_id
		
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExternaTipo_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExternaTipo_INSERT]
	@iet_id TinyInt
	, @iet_descricao VarChar (128)
	, @iet_qtdeDiasAutenticacao Int

AS
BEGIN
	INSERT INTO 
		SYS_IntegracaoExternaTipo
		( 
			iet_id 
			, iet_descricao 
			, iet_qtdeDiasAutenticacao 
 
		)
	VALUES
		( 
			@iet_id 
			, @iet_descricao 
			, @iet_qtdeDiasAutenticacao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_INSERT]
		  @ent_id uniqueidentifier
		, @usu_login VarChar (500)
		, @usu_email VarChar (500)
		, @usu_senha VarChar (256)
		, @usu_criptografia TinyInt
		, @usu_situacao TinyInt
		, @usu_dataCriacao DateTime
		, @usu_dataAlteracao DateTime
		, @pes_id uniqueidentifier
		, @usu_integridade Int
		, @usu_dominio varchar(100)
		, @usu_integracaoExterna TINYINT = NULL
		--,@usu_integracaoAD TINYINT removido para funcionar com o sistema em versão anterior a 1.22.0.0
AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Usuario
		( 
			  ent_id
			, usu_login
			, usu_email
			, usu_senha
			, usu_criptografia 
			, usu_situacao
			, usu_dataCriacao
			, usu_dataAlteracao
			, pes_id 
			, usu_integridade
			, usu_dominio
			, usu_integracaoExterna
			--,usu_integracaoAD
		)
	OUTPUT inserted.usu_id INTO @ID
	VALUES
		( 
			  @ent_id
			, @usu_login
			, @usu_email
			, @usu_senha
			, @usu_criptografia 
			, @usu_situacao
			, @usu_dataCriacao
			, @usu_dataAlteracao
			, @pes_id
			, @usu_integridade
			, @usu_dominio
			, @usu_integracaoExterna
			--,@usu_integracaoAD
		)
	SELECT ID FROM @ID
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Pais_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:40
-- Description:	Incrementa uma unidade no campo integridade da tabela de pais
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Pais_INCREMENTA_INTEGRIDADE]
		@pai_id uniqueidentifier

AS
BEGIN
	UPDATE END_Pais
	SET 
		pai_integridade = pai_integridade + 1
	WHERE 
		pai_id = @pai_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_Select]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 01/22/2010 13:50
-- Description:	utilizado na busca de parametro grupo perfil, retorna as os parametros
--				grupo perfil que não foram excluídas logicamente,
--				filtrados por:
--					id, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_Select]	
		@pgs_id uniqueidentifier
AS
BEGIN
	SELECT distinct
		 pgs_chave
		 , pgs_chave as pgs_chave_id
	FROM
		SYS_ParametroGrupoPerfil WITH (NOLOCK)
	WHERE
		pgs_situacao <> 3
		AND (@pgs_id is null or pgs_id = @pgs_id)			
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Configuracao_Update_Situacao]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 21/03/2011 09:35
-- Description:	Deleta a configuração logicamente utilizando o 
--				campo situação (3 – Excluído)
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Configuracao_Update_Situacao]
	@cfg_id UNIQUEIDENTIFIER

AS
BEGIN

	UPDATE 
		CFG_Configuracao
	SET 
		cfg_situacao = 3
		, cfg_dataAlteracao = GETDATE()
	WHERE 
		cfg_id = @cfg_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,sis.ent_id
		,sen_chaveK1
		,sen_urlAcesso
		,sen_logoCliente
		,sen_urlCliente
		,sen_situacao
	FROM 
		SYS_SistemaEntidade sis WITH(NOLOCK) 
	INNER JOIN SYS_Entidade ent WITH (NOLOCK)
		ON sis.ent_id = ent.ent_id		
	ORDER BY
		ent_razaoSocial	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_SELECTBY_pes_id]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_SELECTBY_pes_id]
	@pes_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		pes_id
		,pse_id
		,end_id
		,pse_numero
		,pse_complemento
		,pse_situacao
		,pse_dataCriacao
		,pse_dataAlteracao

		,pse_enderecoPrincipal
		,pse_latitude
		,pse_longitude

	FROM
		PES_PessoaEndereco WITH(NOLOCK)
	WHERE 
		pes_id = @pes_id 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExternaTipo_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExternaTipo_UPDATE]
	@iet_id TINYINT
	, @iet_descricao VARCHAR (128)
	, @iet_qtdeDiasAutenticacao INT

AS
BEGIN
	UPDATE SYS_IntegracaoExternaTipo 
	SET 
		iet_descricao = @iet_descricao 
		, iet_qtdeDiasAutenticacao = @iet_qtdeDiasAutenticacao 

	WHERE 
		iet_id = @iet_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_DELETE]
	@usu_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_Usuario 
	WHERE 
		usu_id = @usu_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Pais_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:41
-- Description:	Decrementa uma unidade no campo integridade da tabela de pais
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Pais_DECREMENTA_INTEGRIDADE]
		@pai_id uniqueidentifier
AS
BEGIN
	UPDATE END_Pais
	SET 
		pai_integridade = pai_integridade - 1
	WHERE 
		pai_id = @pai_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_SELECTBY_usu_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 19/05/2010 17:45
-- Description:	Carrega todos os sistema que o usuário tem acesso ordenado
--				pelo nome do sistema.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Sistema_SELECTBY_usu_id]
	@usu_id uniqueidentifier
AS
BEGIN
	SELECT
		Sis.sis_id
		, Sis.sis_nome
		, Sis.sis_descricao
		, Sis.sis_caminho
		, Sis.sis_caminhoLogout
		, Sis.sis_situacao
		, Sis.sis_urlImagem
		, Sis.sis_urlLogoCabecalho
		, Sis.sis_tipoAutenticacao
	FROM
		SYS_Sistema Sis WITH(NOLOCK)
	WHERE
		Sis.sis_situacao = 1
		AND Sis.sis_ocultarLogo = 0
		AND EXISTS (
			SELECT TOP (1)
				Gru.sis_id 
			FROM 
				SYS_Grupo Gru WITH(NOLOCK)			
			INNER JOIN
				SYS_UsuarioGrupo Usg WITH(NOLOCK)
			ON
				Usg.gru_id = Gru.gru_id		
				AND Usg.usg_situacao = 1	
				AND Usg.usu_id = @usu_id
			WHERE
				Gru.gru_situacao <> 3		
				AND Gru.sis_id = Sis.sis_id		
		)
	ORDER BY 
		Sis.sis_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_SELECTBY_sis_id]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_SELECTBY_sis_id]
	@sis_id INT
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,sen_chaveK1
		,sen_urlAcesso
		,sen_logoCliente
		,sen_urlCliente
		,sen_situacao

	FROM
		SYS_SistemaEntidade WITH(NOLOCK)
	WHERE 
		sis_id = @sis_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_SELECTBY_end_id]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_SELECTBY_end_id]
	@end_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		pes_id
		,pse_id
		,end_id
		,pse_numero
		,pse_complemento
		,pse_situacao
		,pse_dataCriacao
		,pse_dataAlteracao

		,pse_enderecoPrincipal
		,pse_latitude
		,pse_longitude

	FROM
		PES_PessoaEndereco WITH(NOLOCK)
	WHERE 
		end_id = @end_id  
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExternaTipo_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExternaTipo_DELETE]
	@iet_id TINYINT

AS
BEGIN
	DELETE FROM 
		SYS_IntegracaoExternaTipo 
	WHERE 
		iet_id = @iet_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_UPDATE]
	@ent_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
	, @uae_id UNIQUEIDENTIFIER
	, @end_id UNIQUEIDENTIFIER
	, @uae_numero VARCHAR (20)
	, @uae_complemento VARCHAR (100)
	, @uae_situacao TINYINT
	, @uae_dataCriacao DATETIME
	, @uae_dataAlteracao DATETIME
	, @uae_enderecoPrincipal BIT = NULL
	, @uae_latitude DECIMAL (15,10) = NULL
	, @uae_longitude DECIMAL (15,10) = NULL

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaEndereco 
	SET 
		end_id = @end_id 
		, uae_numero = @uae_numero 
		, uae_complemento = @uae_complemento 
		, uae_situacao = @uae_situacao 
		, uae_dataCriacao = @uae_dataCriacao 
		, uae_dataAlteracao = @uae_dataAlteracao 
		, uae_enderecoPrincipal = @uae_enderecoPrincipal 
		, uae_latitude = @uae_latitude 
		, uae_longitude = @uae_longitude 

	WHERE 
		ent_id = @ent_id 
		AND uad_id = @uad_id 
		AND uae_id = @uae_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_BancoRelacionado]'
GO
CREATE TABLE [dbo].[SYS_BancoRelacionado]
(
[bdr_id] [int] NOT NULL IDENTITY(1, 1),
[bdr_nome] [varchar] (100) NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_BancoRelacionado] on [dbo].[SYS_BancoRelacionado]'
GO
ALTER TABLE [dbo].[SYS_BancoRelacionado] ADD CONSTRAINT [PK_SYS_BancoRelacionado] PRIMARY KEY CLUSTERED  ([bdr_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_Update_ColunaValor_BD]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 30/04/2010 12:34
-- Description:	Faz verificação do uso de uma valor para uma coluna no banco 
--				GestaoCore e em todos os bancos dependentes listados na tabela
--				SYS_BancoRelacionado.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_Update_ColunaValor_BD]		
	@valor uniqueidentifier
	,@xml XML	
	,@coluna VARCHAR(100)
	,@tabela_raiz VARCHAR(100)
AS
BEGIN

	-- Declarar variaveis usadas na procedure
	DECLARE @COLUNA_NOME NVARCHAR(100)
	DECLARE @COLUNA_VALOR NVARCHAR(100)
	DECLARE @COLUNA_SITUACAO NVARCHAR(100)
	DECLARE @BANCO NVARCHAR(100)
	DECLARE @TABELA NVARCHAR(100)
	DECLARE @sql NVARCHAR(4000)
	DECLARE @coluna_valores_antigos NVARCHAR(4000)
	DECLARE @OLD_IDS NVARCHAR(4000)

	

	-- Criar Tabela temporária para Listar bancos, tabelas, colunas de situação, nomes das colunas e o valor. 
	-- Essas informações são usadas no BD GestaoCore e todos os outros BDs dependendes do GestaoCore.
	CREATE TABLE #TMP_BD ( 
							coluna_nome nvarchar(100)
							,coluna_valor nvarchar(100)
							,coluna_situacao nvarchar(100)
							,banco nvarchar(100)
							,tabela nvarchar(100)
							,coluna_valores_antigos nvarchar(4000)
						)

	-- Criar tabela temporária para registro do numero de vezes na qual é encontrada o valor da coluna para o banco,tabela,
	-- e situacao cadastradas no #TMP_BD
	DECLARE @hxml INT			
	EXEC sp_xml_preparedocument @hxml OUTPUT, @xml
	
	SELECT @coluna_valores_antigos = ''
	SELECT @coluna_valores_antigos = @coluna_valores_antigos + CONVERT(NVARCHAR,valor) + ',' 
	FROM OPENXML (@hxml, 'Coluna/ColunaValorAntigo', 0) 
	WITH ( valor NVARCHAR(100) 'valor') xcood
	
	EXEC sp_xml_removedocument @hxml
	SELECT @coluna_valores_antigos = LEFT(@coluna_valores_antigos,LEN(@coluna_valores_antigos)-1)
	
	
	-- Inserir registros na #TMP_BD sobre o BANCO GestaoCore
	INSERT INTO #TMP_BD 
	select  COLUMN_NAME
			,@valor AS VALOR
			,(select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS B WHERE B.TABLE_NAME = A.TABLE_NAME AND B.COLUMN_NAME LIKE '%situacao%')
			,TABLE_CATALOG AS BANCO
			,TABLE_NAME
			,@coluna_valores_antigos
	from INFORMATION_SCHEMA.COLUMNS A
	where A.COLUMN_NAME LIKE @coluna AND A.TABLE_NAME NOT LIKE @tabela_raiz


	-- Declara cursor
	DECLARE db_cursor CURSOR 
	FOR
		--Select dos bancos relacionados ao GestaoCore
		SELECT bdr_nome FROM SYS_BancoRelacionado
		
		OPEN db_cursor
		FETCH NEXT FROM db_cursor 
		INTO @BANCO
		--Faz inserção de registros na #TMP_BD sobre os BANCOS relacionados ao GestaoCore.
		WHILE @@FETCH_STATUS = 0 
			BEGIN 
			SET @sql = 'INSERT INTO #TMP_BD '
			SET @sql = @sql + ' SELECT COLUMN_NAME '
			SET @sql = @sql + '		,'+CONVERT(NVARCHAR,@valor)+' AS VALOR '
			SET @sql = @sql + '		,(select COLUMN_NAME from ['+@BANCO+'].INFORMATION_SCHEMA.COLUMNS B WHERE B.TABLE_NAME = A.TABLE_NAME AND B.COLUMN_NAME LIKE ''%situacao%'')'
			SET @sql = @sql + '		,TABLE_CATALOG '
			SET @sql = @sql + '		,TABLE_NAME '
			SET @sql = @sql + '		,'''+@coluna_valores_antigos+''''
			SET @sql = @sql + '	FROM ['+@BANCO+'].INFORMATION_SCHEMA.COLUMNS A '
			SET @sql = @sql + '	WHERE A.COLUMN_NAME LIKE '''+@coluna+''' AND A.TABLE_NAME NOT LIKE '''+@tabela_raiz+''''			
			EXEC(@sql)
			FETCH NEXT FROM db_cursor 
			INTO @BANCO
		END
		CLOSE db_cursor 
	DEALLOCATE db_cursor

	SET @sql = ''
	-- Declara cursor
	DECLARE db_cursor CURSOR
	FOR
		--Select dos registros cadastrados na #TMP_BD
		SELECT  coluna_nome
				,coluna_valor
				,coluna_situacao
				,banco
				,tabela
				,coluna_valores_antigos
		FROM #TMP_BD
		
		OPEN db_cursor
		FETCH NEXT FROM db_cursor 
		INTO @COLUNA_NOME, @COLUNA_VALOR, @COLUNA_SITUACAO, @BANCO, @TABELA, @OLD_IDS
		--Faz inserção na # baseado no COUNT dos registros do #TMP_BD para verificar se valor da coluna (#TMP_BD.COLUNA_VALOR) é usada (cadastrada no BD)
		--para a tabela (#TMP_BD.TABELA), no banco (#TMP_BD.BANCO), com situação (#TMP_BD.COLUNA_SITUACAO) diferente de 3 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 			
			IF (@tabela_raiz = 'PES_PESSOA')
			BEGIN				
				set @sql = 'DELETE FROM PES_CertidaoCivil WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)		
				
				set @sql = 'DELETE FROM PES_PessoaAltaHabilidade WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)					
			
				set @sql = 'DELETE FROM PES_PessoaContato WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)
				
				set @sql = 'DELETE FROM PES_PessoaDeficiencia WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)
				
				set @sql = 'DELETE FROM PES_PessoaDocumento WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)
				
				set @sql = 'DELETE FROM PES_PessoaEndereco WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '				
				EXEC(@sql)
				
				set @sql = 'DELETE FROM PES_PessoaTranstornoDesenvolvimento WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)																	
				
				set @sql = 'DELETE FROM SYS_Usuario WHERE pes_id IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
				EXEC(@sql)					
			END
			
			SET @sql = 'UPDATE ['+ @BANCO +']..[' + @TABELA + '] '
			SET @sql = @sql + ' SET [' + RTRIM(LTRIM(@COLUNA_NOME)) + '] = ' + RTRIM(LTRIM(@COLUNA_VALOR))  + ' ' 
			SET @sql = @sql + ' WHERE [' + RTRIM(LTRIM(@COLUNA_NOME)) + '] IN (' + RTRIM(LTRIM(@OLD_IDS)) + ') '
			EXEC(@sql)
			FETCH NEXT FROM db_cursor 
			INTO @COLUNA_NOME, @COLUNA_VALOR, @COLUNA_SITUACAO, @BANCO, @TABELA, @OLD_IDS
		END
		CLOSE db_cursor 
	DEALLOCATE db_cursor

	SELECT @sql = 'DELETE '+@tabela_raiz+' WHERE '+ LEFT(@coluna,LEN(@coluna)-1) +' IN ('+@coluna_valores_antigos+')' 
	EXEC(@sql)
	
	DROP TABLE #TMP_BD
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_UsuarioADErro]'
GO
CREATE TABLE [dbo].[LOG_UsuarioADErro]
(
[usa_id] [bigint] NOT NULL,
[use_descricaoErro] [varchar] (max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_LOG_UsuarioADErro] on [dbo].[LOG_UsuarioADErro]'
GO
ALTER TABLE [dbo].[LOG_UsuarioADErro] ADD CONSTRAINT [PK_LOG_UsuarioADErro] PRIMARY KEY CLUSTERED  ([usa_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioADErro_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioADErro_LOAD]
	@usa_id BigInt
	
AS
BEGIN
	SELECT	Top 1
		 usa_id  
		, use_descricaoErro 

 	FROM
 		LOG_UsuarioADErro
	WHERE 
		usa_id = @usa_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 01/22/2010 13:50
-- Description:	utilizado na busca de parametro grupo perfil, retorna as os parametros
--				grupo perfil que não foram excluídas logicamente,
--				filtrados por:
--					id, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ParametroGrupoPerfil_SelectBy_All]	
		@pgs_id uniqueidentifier
		, @pgs_situacao tinyint
AS
BEGIN
	SELECT 
		 P.pgs_id
		, P.pgs_chave
		, P.gru_id
		, CASE  P.pgs_situacao 
			WHEN 1 THEN 'Não'
			WHEN 3 THEN 'Excluído'			
		  END AS pgs_situacao
		, CASE WHEN S.sis_nome is null THEN G.gru_nome
			   ELSE S.sis_nome + ' - ' + G.gru_nome END AS sis_gru_Nome
		, CONVERT(VARCHAR, G.sis_id) + ';' + CONVERT(VARCHAR(255),G.gru_id) AS sis_gru_id
		  
	FROM
		SYS_ParametroGrupoPerfil P WITH (NOLOCK)
	INNER JOIN SYS_Grupo G WITH (NOLOCK)	ON
		P.gru_id = G.gru_id
	INNER JOIN SYS_Sistema S WITH (NOLOCK) ON
		G.sis_id = S.sis_id
	WHERE
		pgs_situacao <> 3
		AND gru_situacao <> 3
		AND sis_situacao <> 3
		AND (@pgs_id is null or pgs_id = @pgs_id)			
		
	ORDER BY pgs_chave
	
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_SELECTBY_ent_id]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,sen_chaveK1
		,sen_urlAcesso
		,sen_logoCliente
		,sen_urlCliente
		,sen_situacao

	FROM
		SYS_SistemaEntidade WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExternaTipo_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExternaTipo_SELECT]
	
AS
BEGIN
	SELECT 
		iet_id
		,iet_descricao
		,iet_qtdeDiasAutenticacao

	FROM 
		SYS_IntegracaoExternaTipo WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,uad_id
		,uae_id
		,end_id
		,uae_numero
		,uae_complemento
		,uae_situacao
		,uae_dataCriacao
		,uae_dataAlteracao
		,uae_enderecoPrincipal
		,uae_latitude
		,uae_longitude

	FROM 
		SYS_UnidadeAdministrativaEndereco WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_LoadBy_url]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 11/05/2010 16:56
-- Description:	Retorna um objeto SYS_GrupoPermissao filtrado por grupo url
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_LoadBy_url]
	@gru_id uniqueidentifier
	, @msm_url VARCHAR(500)
AS
BEGIN
	SELECT TOP 1
		gru_id
		, sis_id
		, mod_id
		, grp_consultar
		, grp_inserir
		, grp_alterar
		, grp_excluir
	FROM
		SYS_GrupoPermissao WITH(NOLOCK)
	WHERE
		gru_id = @gru_id
		AND EXISTS (
			SELECT TOP (1)
				SYS_Modulo.sis_id
				, SYS_Modulo.mod_id
			FROM
				SYS_Modulo WITH(NOLOCK)
			INNER JOIN SYS_ModuloSiteMap WITH(NOLOCK)
				ON SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
				AND SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id
				AND SYS_ModuloSiteMap.msm_url = @msm_url	
			WHERE
				mod_situacao <> 3				
				AND SYS_Modulo.sis_id = SYS_GrupoPermissao.sis_id
				AND SYS_Modulo.mod_id = SYS_GrupoPermissao.mod_id
		)
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioADErro_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioADErro_INSERT]
	@usa_id BigInt
	, @use_descricaoErro VARCHAR(MAX)
AS
BEGIN
	INSERT INTO 
		LOG_UsuarioADErro
		( 
			usa_id 
			, use_descricaoErro 
 
		)
	VALUES
		( 
			@usa_id 
			, @use_descricaoErro 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SELECTBy_usu_id]'
GO
-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 03/02/2010 12:15
-- Description:	Consulta de grupos filtrado pelo ID do usuário e ordenado pelo
--				nome do grupo.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SELECTBy_usu_id]
	@usu_id uniqueidentifier
AS
BEGIN
	SELECT 
		SYS_Grupo.gru_id
		,SYS_Grupo.gru_nome
		,SYS_Grupo.gru_situacao
		,SYS_Grupo.gru_dataCriacao
		,SYS_Grupo.gru_dataAlteracao
		,SYS_Grupo.vis_id
		,SYS_Grupo.sis_id
		,SYS_Sistema.sis_nome
		,SYS_UsuarioGrupo.usg_situacao
	FROM 
		SYS_Grupo WITH(NOLOCK) 
	INNER JOIN SYS_Sistema WITH(NOLOCK) 
		ON SYS_Grupo.sis_id = SYS_Sistema.sis_id
	INNER JOIN SYS_UsuarioGrupo WITH(NOLOCK) 
		ON SYS_Grupo.gru_id = SYS_UsuarioGrupo.gru_id
	WHERE
		gru_situacao <> 3
		AND sis_situacao <> 3
		AND SYS_UsuarioGrupo.usg_situacao <> 3
		AND SYS_UsuarioGrupo.usu_id = @usu_id		
	ORDER BY
		SYS_Grupo.gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Configuracao_SelectBy_cfg_chave]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 22/03/2011 09:00
-- Description: Retorna as configurações ativas cadastradas que atendem 
--				ao filtro: chave
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Configuracao_SelectBy_cfg_chave]
	@cfg_id UNIQUEIDENTIFIER
	, @cfg_chave VARCHAR(100)
	
AS
BEGIN
	SELECT 
		cfg_id
		, cfg_chave
		, cfg_valor
		, cfg_descricao
		, cfg_situacao
		, cfg_dataCriacao
		, cfg_dataAlteracao
	FROM
		CFG_Configuracao WITH (NOLOCK)		
	WHERE
		cfg_situacao <> 3
		AND cfg_chave = @cfg_chave
		AND (@cfg_id is null OR cfg_id <> @cfg_id)		
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_LOAD]
	@sis_id Int
	, @ent_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 sis_id  
		, ent_id 
		, sen_chaveK1 
		, sen_urlAcesso 
		, sen_logoCliente 
		, sen_urlCliente 
		, sen_situacao 

 	FROM
 		SYS_SistemaEntidade
	WHERE 
		sis_id = @sis_id
		AND ent_id = @ent_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaSenhas]'
GO
-- =============================================
-- Author:		Gabriel Scavassa
-- Create date: 24/02/2015
-- Description:	Seleciona as todas as senhas do usuário.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaSenhas] 
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN

		SELECT
			ush.usu_id, 
			ush.ush_senha, 
			ush.ush_criptografia, 
			ush.ush_id, 
			ush.ush_data
		FROM
			SYS_UsuarioSenhaHistorico ush WITH(NOLOCK)
		WHERE 
			ush.usu_id = @usu_id

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_LOAD]
	@ent_id UniqueIdentifier
	, @uad_id UniqueIdentifier
	, @uae_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 ent_id  
		, uad_id 
		, uae_id 
		, end_id 
		, uae_numero 
		, uae_complemento 
		, uae_situacao 
		, uae_dataCriacao 
		, uae_dataAlteracao 
		, uae_enderecoPrincipal 
		, uae_latitude 
		, uae_longitude 

 	FROM
 		SYS_UnidadeAdministrativaEndereco
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_id = @uae_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupoUA_SelectBy_UsuarioGrupo]'
GO

-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 03/02/2010 12:15
-- Description:	Consulta de unidade administrativa/entidades filtrado pelo ID 
--				do usuário e ordenado pelo nome do unidade 
--				administrativa/entidades.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupoUA_SelectBy_UsuarioGrupo]
	@usu_id uniqueidentifier,
	@gru_id uniqueidentifier
AS
BEGIN
   IF @usu_id IS NULL
      IF @gru_id IS NOT NULL
         SELECT UGUA.usu_id, UGUA.gru_id, UGUA.ugu_id, UGUA.ent_id, UGUA.uad_id,
                CASE WHEN UGUA.uad_id IS NULL THEN ENT.ent_razaoSocial collate database_default
                     ELSE UAD.uad_nome collate database_default
                END AS ugu_nome			
           FROM SYS_UsuarioGrupoUA UGUA WITH(NOLOCK)
                INNER JOIN SYS_Entidade ENT WITH(NOLOCK)
                ON UGUA.ent_id = ENT.ent_id
                LEFT JOIN SYS_UnidadeAdministrativa UAD WITH(NOLOCK)
                ON UGUA.uad_id = UAD.uad_id						
          WHERE UGUA.gru_id = @gru_id
          ORDER BY ugu_nome
      ELSE
         SELECT UGUA.usu_id, UGUA.gru_id, UGUA.ugu_id, UGUA.ent_id, UGUA.uad_id,
                CASE WHEN UGUA.uad_id IS NULL THEN ENT.ent_razaoSocial collate database_default
                     ELSE UAD.uad_nome collate database_default
                END AS ugu_nome			
           FROM SYS_UsuarioGrupoUA UGUA WITH(NOLOCK)
                INNER JOIN SYS_Entidade ENT WITH(NOLOCK)
                ON UGUA.ent_id = ENT.ent_id
                LEFT JOIN SYS_UnidadeAdministrativa UAD WITH(NOLOCK)
                ON UGUA.uad_id = UAD.uad_id						
          WHERE UGUA.usu_id = @usu_id
            AND UGUA.gru_id = @gru_id
          ORDER BY ugu_nome
   ELSE
      IF @gru_id IS NULL
         SELECT UGUA.usu_id, UGUA.gru_id, UGUA.ugu_id, UGUA.ent_id, UGUA.uad_id,
                CASE WHEN UGUA.uad_id IS NULL THEN ENT.ent_razaoSocial collate database_default
                     ELSE UAD.uad_nome collate database_default
                END AS ugu_nome			
           FROM SYS_UsuarioGrupoUA UGUA WITH(NOLOCK)
                INNER JOIN SYS_Entidade ENT WITH(NOLOCK)
                ON UGUA.ent_id = ENT.ent_id
                LEFT JOIN SYS_UnidadeAdministrativa UAD WITH(NOLOCK)
                ON UGUA.uad_id = UAD.uad_id						
          WHERE UGUA.usu_id = @usu_id
          ORDER BY ugu_nome
      ELSE
         SELECT UGUA.usu_id, UGUA.gru_id, UGUA.ugu_id, UGUA.ent_id, UGUA.uad_id,
                CASE WHEN UGUA.uad_id IS NULL THEN ENT.ent_razaoSocial collate database_default
                     ELSE UAD.uad_nome collate database_default
                END AS ugu_nome			
           FROM SYS_UsuarioGrupoUA UGUA WITH(NOLOCK)
                INNER JOIN SYS_Entidade ENT WITH(NOLOCK)
                ON UGUA.ent_id = ENT.ent_id
                LEFT JOIN SYS_UnidadeAdministrativa UAD WITH(NOLOCK)
                ON UGUA.uad_id = UAD.uad_id						
          WHERE UGUA.usu_id = @usu_id
            AND UGUA.gru_id = @gru_id
          ORDER BY ugu_nome
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioADErro_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioADErro_UPDATE]
	@usa_id BIGINT
	, @use_descricaoErro VARCHAR(MAX)

AS
BEGIN
	UPDATE LOG_UsuarioADErro 
	SET 
		use_descricaoErro = @use_descricaoErro 

	WHERE 
		usa_id = @usa_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SELECTBy_ALL_In_Usuario]'
GO
-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/01/2010 15:40
-- Description:	Consulta de grupos concatenando o nome do sistema ao grupo.
--				Retorna todos os grupos que estiverem ativos ou forem padrão
--				do sistema. Usado para listar os grupos na associoação do 
--				usuário a um grupo conforme documento de especificação 
--				tópico 4.4.5 Gerenciar Usuários.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SELECTBy_ALL_In_Usuario]
AS
BEGIN
	SELECT 
			gru_id
			,sis_nome + ' - ' + gru_nome AS  gru_nome
			,gru_situacao
			,gru_dataCriacao
			,gru_dataAlteracao
			,vis_id
			,SYS_Grupo.sis_id
	FROM 
		SYS_Grupo WITH(NOLOCK) 
	INNER JOIN SYS_Sistema WITH(NOLOCK)
		ON SYS_Grupo.sis_id = SYS_Sistema.sis_id
	WHERE
		gru_situacao in (1,4) --Apenas os grupos ativo e de sistema
		AND sis_situacao <> 3
	ORDER BY 
		sis_nome
		, gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_INSERT]
	@sis_id Int
	, @ent_id UniqueIdentifier
	, @sen_chaveK1 VarChar (100)
	, @sen_urlAcesso VarChar (200)
	, @sen_logoCliente VarChar (2000)
	, @sen_urlCliente VarChar (1000)
	, @sen_situacao TinyInt

AS
BEGIN
	INSERT INTO 
		SYS_SistemaEntidade
		( 
			sis_id 
			, ent_id 
			, sen_chaveK1 
			, sen_urlAcesso 
			, sen_logoCliente 
			, sen_urlCliente 
			, sen_situacao 
 
		)
	VALUES
		( 
			@sis_id 
			, @ent_id 
			, @sen_chaveK1 
			, @sen_urlAcesso 
			, @sen_logoCliente 
			, @sen_urlCliente 
			, @sen_situacao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimaSenhas]'
GO
-- =============================================
-- Author:		Gabriel Scavassa
-- Create date: 24/02/2015
-- Description:	Seleciona a ultima senha salva.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioSenhaHistorico_SelecionaUltimaSenhas] 
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN

		SELECT
			ush.usu_id, 
			ush.ush_senha, 
			ush.ush_criptografia,  
			max_data = max(ush.ush_data)
		FROM
			SYS_UsuarioSenhaHistorico ush WITH(NOLOCK)
		WHERE 
			ush.usu_id = @usu_id
		GROUP BY
			ush.usu_id,
			ush.ush_senha, 
			ush.ush_criptografia
 

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_INSERT]
	  @ent_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
	, @uae_id UNIQUEIDENTIFIER = NULL
	, @end_id UNIQUEIDENTIFIER
	, @uae_numero VARCHAR (20)
	, @uae_complemento VARCHAR (100)
	, @uae_situacao TINYINT
	, @uae_dataCriacao DATETIME
	, @uae_dataAlteracao DATETIME
	, @uae_enderecoPrincipal BIT = NULL
	, @uae_latitude DECIMAL (15,10) = NULL
	, @uae_longitude DECIMAL (15,10) = NULL

AS
BEGIN

	/*
		Alterado o parametro @uae_id com defult igual a nulo, para evitar erros nos sistemas que estão 'informando' esse parâmetro
		Removido o campo uae_id do insert, pois ele tem o valor default newsequentialid()
	*/

	DECLARE @ID TABLE ( ID Uniqueidentifier )

	INSERT INTO 
		SYS_UnidadeAdministrativaEndereco
		( 
			  ent_id 
			, uad_id 
			--, uae_id 
			, end_id 
			, uae_numero 
			, uae_complemento 
			, uae_situacao 
			, uae_dataCriacao 
			, uae_dataAlteracao 
			, uae_enderecoPrincipal 
			, uae_latitude 
			, uae_longitude 
 		)
	OUTPUT inserted.uae_id INTO @ID
	VALUES
		( 
			  @ent_id 
			, @uad_id 
			--, @uae_id 
			, @end_id 
			, @uae_numero 
			, @uae_complemento 
			, @uae_situacao 
			, @uae_dataCriacao 
			, @uae_dataAlteracao 
			, @uae_enderecoPrincipal 
			, @uae_latitude 
			, @uae_longitude 
 
		)
		
	--SELECT ISNULL(SCOPE_IDENTITY(),-1)
	SELECT ID FROM @ID

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa]'
GO
-- =========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 01/02/2010 12:50
-- Description:	Select para retorna os dados da unidade administrativa na 
--				buscar de ua conforme documento de especificação tópico 4.4.6
--				e no tópico 4.4.22, gerenciar unidade administrativa.
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa]
	@gru_id uniqueidentifier
	, @usu_id uniqueidentifier	
	, @tua_id uniqueidentifier
	, @ent_id uniqueidentifier
	, @uad_id uniqueidentifier
	, @uad_nome VARCHAR(200)
	, @uad_codigo VARCHAR(20)
	, @uad_situacao TINYINT
AS
BEGIN
	SELECT
		SYS_UnidadeAdministrativa.ent_id
		, SYS_Entidade.ent_razaoSocial
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_codigo
		, SYS_UnidadeAdministrativa.uad_nome
		, SYS_TipoUnidadeAdministrativa.tua_id
		, SYS_TipoUnidadeAdministrativa.tua_nome		
		, (SELECT uad_nome FROM SYS_UnidadeAdministrativa A  WITH (NOLOCK) WHERE A.ent_id = SYS_UnidadeAdministrativa.ent_id AND  A.uad_id = SYS_UnidadeAdministrativa.uad_idSuperior) AS uad_nomeSup
		, CASE SYS_UnidadeAdministrativa.uad_situacao
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS uad_bloqueado
		, uad_codigoIntegracao
		, uad_codigoInep
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)
	INNER JOIN SYS_Entidade WITH (NOLOCK)
		ON SYS_Entidade.ent_id = SYS_UnidadeAdministrativa.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UnidadeAdministrativa.tua_id = SYS_TipoUnidadeAdministrativa.tua_id	
	WHERE
		uad_situacao <> 3	
		AND ent_situacao <> 3	
		AND tua_situacao <> 3
		AND ((@tua_id IS NULL) OR (SYS_UnidadeAdministrativa.tua_id = @tua_id))
		AND ((@ent_id IS NULL) OR (SYS_UnidadeAdministrativa.ent_id = @ent_id))
		AND ((@uad_id IS NULL) OR (SYS_UnidadeAdministrativa.uad_id <> @uad_id))
		AND ((@uad_nome IS NULL) OR (SYS_UnidadeAdministrativa.uad_nome LIKE '%' + @uad_nome + '%'))
		AND ((@uad_codigo IS NULL) OR (SYS_UnidadeAdministrativa.uad_codigo LIKE '%' + @uad_codigo + '%'))
		AND ((@uad_situacao IS NULL) OR (SYS_UnidadeAdministrativa.uad_situacao = @uad_situacao))		
		AND ((
				(@gru_id IS NULL) 
				AND 
				(@usu_id IS NULL)
			) 
			OR 
			(
				EXISTS (
					SELECT 
						usu_id
						, gru_id 
					FROM 
						SYS_UsuarioGrupoUA WITH (NOLOCK)
					WHERE
						SYS_UsuarioGrupoUA.gru_id = @gru_id
						AND SYS_UsuarioGrupoUA.usu_id = @usu_id
						AND SYS_UsuarioGrupoUA.uad_id = SYS_UnidadeAdministrativa.uad_id
						AND SYS_UsuarioGrupoUA.ent_id = SYS_UnidadeAdministrativa.ent_id
				)
			))
	ORDER BY 
		SYS_Entidade.ent_razaoSocial, SYS_UnidadeAdministrativa.uad_nome
		
	SELECT @@ROWCOUNT						
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioADErro_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_LOG_UsuarioADErro_DELETE]
	@usa_id BIGINT

AS
BEGIN
	DELETE FROM 
		LOG_UsuarioADErro 
	WHERE 
		usa_id = @usa_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SelectBy_sis_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 19/01/2010 15:50
-- Description:	usado na busca de grupos, retorna os grupos de usuário
--              que não foram excluídos lógicamente filtrados por sistema.
--              se o parâmetro @sis_id for nulo retorna todos independente
--              do sistema.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SelectBy_sis_id]
	@sis_id INT
AS
BEGIN
	SELECT 
		gru_id
		, gru_nome
		, gru_situacao
		, CASE gru_situacao 
			WHEN 1 THEN 'Ativo'
			WHEN 2 THEN 'Bloqueado'
			WHEN 4 THEN 'Padrão do Sistema'
		  END AS gru_situacaoNome
		, vis_nome
		, sis_nome
		, CASE WHEN SYS_Sistema.sis_nome is null THEN SYS_Grupo.gru_nome
			   ELSE SYS_Sistema.sis_nome collate database_default + ' - ' + SYS_Grupo.gru_nome collate database_default END AS sis_gru_Nome
		, CONVERT(VARCHAR, SYS_Grupo.sis_id) collate database_default + ';' + CONVERT(VARCHAR(255),SYS_Grupo.gru_id) collate database_default AS sis_gru_id 
		, SYS_Grupo.sis_id
		, SYS_Grupo.vis_id
	FROM
		SYS_Grupo WITH(NOLOCK)
	INNER JOIN SYS_Sistema WITH(NOLOCK)
		ON SYS_Grupo.sis_id = SYS_Sistema.sis_id
	INNER JOIN SYS_Visao WITH(NOLOCK)
		ON SYS_Grupo.vis_id = SYS_Visao.vis_id
	WHERE
		gru_situacao <> 3
		AND sis_situacao <> 3		
		AND ((@sis_id IS NULL) OR (SYS_Grupo.sis_id = @sis_id))
	ORDER BY
		sis_nome
		, gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_DELETE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		SYS_SistemaEntidade 
	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_DELETE]
	@ent_id uniqueidentifier
	, @uad_id uniqueidentifier
	, @uae_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_UnidadeAdministrativaEndereco	
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_id = @uae_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioADErro_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioADErro_SELECT]
	
AS
BEGIN
	SELECT 
		usa_id
		,use_descricaoErro

	FROM 
		LOG_UsuarioADErro WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupoUA_SalvarBy_XML]'
GO
-- =========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 02/02/2010 14:45
-- Description:	Procedure para salvar os grupos do usuário.
--				Básicamente ele remove os que não existem no XML de entrada
--				e insere os que existem no XML não existem no banco de dados
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupoUA_SalvarBy_XML]
	@entidadeUaXml XML
	, @usu_id uniqueidentifier
AS
BEGIN
	--Apaga os registros do usuário que não estão no xml
	DELETE FROM SYS_UsuarioGrupoUA
	WHERE
		usu_id = @usu_id
		AND NOT EXISTS(
			SELECT
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
				, T.Item.value('(@ent_id)[1]', 'uniqueidentifier') AS ent_id
				, T.Item.value('(@uad_id)[1]', 'uniqueidentifier') AS uad_id
			FROM
				@entidadeUaXml.nodes('/ArrayOfTmpEntidadeUA/TmpEntidadeUA') AS T(Item)
			WHERE
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.gru_id
				AND T.Item.value('(@ent_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.ent_id
				AND T.Item.value('(@uad_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.uad_id
		)
	--Insere os registros que estão no xml e não estão na tabela
	INSERT INTO SYS_UsuarioGrupoUA (usu_id, gru_id, ent_id, uad_id)
	SELECT 
		@usu_id
		, T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
		--, ROW_NUMBER() OVER (ORDER BY T.Item.value('(@gru_id)[1]', 'uniqueidentifier'))
		, T.Item.value('(@ent_id)[1]', 'uniqueidentifier') AS ent_id
		, CASE T.Item.value('(@uad_id)[1]', 'uniqueidentifier')
			WHEN CAST('00000000-0000-0000-0000-000000000000' AS UNIQUEIDENTIFIER) THEN NULL 
			ELSE T.Item.value('(@uad_id)[1]', 'uniqueidentifier')
		  END AS uad_id 
	FROM
		@entidadeUaXml.nodes('/ArrayOfTmpEntidadeUA/TmpEntidadeUA') AS T(Item)
	WHERE
		NOT EXISTS (
			SELECT 
				gru_id 
			FROM 
				SYS_UsuarioGrupoUA
			WHERE
				SYS_UsuarioGrupoUA.usu_id = @usu_id
				AND T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.gru_id
				AND T.Item.value('(@ent_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.ent_id
				AND T.Item.value('(@uad_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupoUA.uad_id
		)
		
	RETURN 1
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_SistemaEntidade_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_SistemaEntidade_UPDATE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @sen_chaveK1 VARCHAR (100)
	, @sen_urlAcesso VARCHAR (200)
	, @sen_logoCliente VARCHAR (2000)
	, @sen_urlCliente VARCHAR (1000)
	, @sen_situacao TINYINT

AS
BEGIN
	UPDATE SYS_SistemaEntidade 
	SET 
		sen_chaveK1 = @sen_chaveK1 
		, sen_urlAcesso = @sen_urlAcesso 
		, sen_logoCliente = @sen_logoCliente 
		, sen_urlCliente = @sen_urlCliente 
		, sen_situacao = @sen_situacao 

	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_UPDATE]
		@ent_id uniqueidentifier
			, @ene_id uniqueidentifier
			, @end_id uniqueidentifier
			, @ene_numero varchar(20)
			, @ene_complemento varchar(100)
			, @ene_situacao tinyInt
			, @ene_dataCriacao dateTime
			, @ene_dataAlteracao dateTime
			, @ene_enderecoPrincipal BIT = NULL
			, @ene_latitude DECIMAL (15,10) = NULL
			, @ene_longitude DECIMAL (15,10) = NULL

AS
BEGIN
	UPDATE SYS_EntidadeEndereco
	SET  
			end_id = @end_id
			, ene_numero = @ene_numero
			, ene_complemento = @ene_complemento
			, ene_situacao = @ene_situacao
			, ene_dataCriacao = @ene_dataCriacao
			, ene_dataAlteracao = @ene_dataAlteracao
			, ene_enderecoPrincipal = @ene_enderecoPrincipal 
			, ene_latitude = @ene_latitude 
			, ene_longitude = @ene_longitude 
	WHERE 
		ent_id = @ent_id
		AND ene_id = @ene_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupo_SalvarBy_XML]'
GO
-- =========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 02/02/2010 14:45
-- Description:	Procedure para salvar os grupos do usuário.
--				Básicamente ele remove os que não existem no XML de entrada
--				e insere os que existem no XML não existem no banco de dados
--
--				02/08/2010
--				- Alterada para exclusão lógica e bloqueio - Juliana Ferrarezi													 
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupo_SalvarBy_XML]
	@grupoXml XML
	, @usu_id uniqueidentifier
AS
BEGIN
	--Inativa os registros do usuário que não estão no xml e estão na tabela.
	UPDATE SYS_UsuarioGrupo
	SET usg_situacao = 3
	WHERE
		usu_id = @usu_id
		AND NOT EXISTS(
			SELECT
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
			FROM
				@grupoXml.nodes('/ArrayOfTmpGrupos/TmpGrupos') AS T(Item)
			WHERE
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupo.gru_id
		)
	
	--Ativa os registros do usuário que estão no xml e também na tabela, mas não estão bloqueados
	UPDATE SYS_UsuarioGrupo
	SET usg_situacao = 1
	WHERE
		usu_id = @usu_id
		AND EXISTS(
			SELECT
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
			FROM
				@grupoXml.nodes('/ArrayOfTmpGrupos/TmpGrupos') AS T(Item)
			WHERE
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupo.gru_id
				AND T.Item.value('(@usg_situacao)[1]', 'tinyint') <> 2
		)
		
	--Bloqueia os registros do usuário que estão no xml e também na tabela.
	UPDATE SYS_UsuarioGrupo
	SET usg_situacao = 2
	WHERE
		usu_id = @usu_id
		AND EXISTS(
			SELECT
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
			FROM
				@grupoXml.nodes('/ArrayOfTmpGrupos/TmpGrupos') AS T(Item)
			WHERE
				T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupo.gru_id
				AND T.Item.value('(@usg_situacao)[1]', 'tinyint') = 2
		)	
		
	--Insere os registros que estão no xml e não estão na tabela
	INSERT INTO SYS_UsuarioGrupo (usu_id, gru_id, usg_situacao)
	SELECT
		@usu_id
		, T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
		, T.Item.value('(@usg_situacao)[1]', 'tinyint') AS usg_situacao
	FROM
		@grupoXml.nodes('/ArrayOfTmpGrupos/TmpGrupos') AS T(Item)
	WHERE
		NOT EXISTS (
			SELECT 
				gru_id 
			FROM 
				SYS_UsuarioGrupo
			WHERE
				SYS_UsuarioGrupo.usu_id = @usu_id
				AND T.Item.value('(@gru_id)[1]', 'uniqueidentifier') = SYS_UsuarioGrupo.gru_id)
	RETURN 1
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_SistemaEntidade_UPDATE_UrlChave]'
GO
-- ========================================================================
-- Author:		Nícolas Gavlak
-- Create date: 29/06/2010 13:45
-- Description:	Altera a Chave K1 e o Url de acesso de SYS_SistemaEntidade
CREATE PROCEDURE [dbo].[NEW_SYS_SistemaEntidade_UPDATE_UrlChave]
		@sis_id int
		, @ent_id uniqueidentifier
		, @sen_chaveK1 Varchar (100)
		, @sen_urlAcesso Varchar (200)
AS
BEGIN
	UPDATE SYS_SistemaEntidade
	SET 		
		sen_chaveK1 = @sen_chaveK1
		, sen_urlAcesso = @sen_urlAcesso
		
	WHERE 
		sis_id = @sis_id
		AND ent_id = @ent_id
		
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
			, ene_id
			, end_id
			, ene_numero
			, ene_complemento
			, ene_situacao
			, ene_dataCriacao
			, ene_dataAlteracao
			, ene_enderecoPrincipal
			, ene_latitude
			, ene_longitude
	FROM 
		SYS_EntidadeEndereco WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_UPDATEBY_XML]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 25/01/2010 19:54
-- Description:	Procedure para atualizar as permissões em lote no formato 
--				XML.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_UPDATEBY_XML]
	@permissoesXml xml
AS
BEGIN
	UPDATE gp SET 
		gp.grp_consultar = xmlGp.grp_consultar
		, gp.grp_inserir = xmlGp.grp_inserir
		, gp.grp_alterar = xmlGp.grp_alterar
		, gp.grp_excluir = xmlGp.grp_excluir
	FROM SYS_GrupoPermissao gp WITH (NOLOCK)
	JOIN (
		SELECT 
			T.Item.value('(@gru_id)[1]', 'uniqueidentifier') AS gru_id
			, T.Item.value('(@sis_id)[1]', 'int') AS sis_id
			, T.Item.value('(@mod_id)[1]', 'int') AS mod_id
			, T.Item.value('(@grp_consultar)[1]', 'bit') AS grp_consultar
			, T.Item.value('(@grp_inserir)[1]', 'bit') AS grp_inserir
			, T.Item.value('(@grp_alterar)[1]', 'bit') AS grp_alterar
			, T.Item.value('(@grp_excluir)[1]', 'bit') AS grp_excluir
		FROM
			@permissoesXml.nodes('/ArrayOfPermissoes/Permissoes') AS T(Item)
		) xmlGp
	ON (gp.gru_id = xmlGp.gru_id and gp.sis_id = xmlGp.sis_id and gp.mod_id = xmlGp.mod_id)
	
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_SistemaEntidade_SelectBy_UrlChave]'
GO
-- ========================================================================
-- Author:		Nicolas Gavlak
-- Create date: 01/07/2010 11:06
-- Description:	Carrega as entidades ligadas.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_SistemaEntidade_SelectBy_UrlChave]
	@sis_id INT
AS
BEGIN
	SELECT
		SYS_Entidade.ent_razaoSocial
		, SYS_SistemaEntidade.sis_id
		, SYS_SistemaEntidade.ent_id
		, SYS_SistemaEntidade.sen_chaveK1
		, SYS_SistemaEntidade.sen_urlAcesso
		, SYS_SistemaEntidade.sen_situacao
		, SYS_SistemaEntidade.sen_logoCliente
		, SYS_SistemaEntidade.sen_urlCliente
	FROM
		SYS_SistemaEntidade WITH(NOLOCK)
	INNER JOIN SYS_Entidade WITH(NOLOCK)
		ON SYS_SistemaEntidade.ent_id = SYS_Entidade.ent_id		
	WHERE
		ent_situacao <> 3	
		AND sen_situacao <> 3	
		AND sis_id = @sis_id	
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_LOAD]
	@ent_id uniqueidentifier
	,@ene_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		ent_id
			, ene_id
			, end_id
			, ene_numero
			, ene_complemento
			, ene_situacao
			, ene_dataCriacao
			, ene_dataAlteracao
			, ene_enderecoPrincipal 
			, ene_latitude 
			, ene_longitude 
 	FROM
 		SYS_EntidadeEndereco
	WHERE 
		ent_id = @ent_id
	and ene_id = @ene_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Visao_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Visao_UPDATE]
	@vis_id INT,
	@vis_nome varchar(50)
	
AS
BEGIN
	UPDATE SYS_Visao
	SET
		vis_nome = @vis_nome
	WHERE
		vis_id = @vis_id
		
		
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_INSERT]
		@ent_id uniqueidentifier
			, @end_id uniqueidentifier
			, @ene_numero varchar(20)
			, @ene_complemento varchar(100)
			, @ene_situacao tinyInt
			, @ene_dataCriacao dateTime
			, @ene_dataAlteracao dateTime
			, @ene_enderecoPrincipal Bit = NULL
			, @ene_latitude Decimal (15,10) = NULL
			, @ene_longitude Decimal (15,10) = NULL


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_EntidadeEndereco
		( 
			ent_id
			, end_id
			, ene_numero
			, ene_complemento
			, ene_situacao
			, ene_dataCriacao
			, ene_dataAlteracao
			, ene_enderecoPrincipal 
			, ene_latitude 
			, ene_longitude 
			
		)
	OUTPUT inserted.ene_id INTO @ID
	VALUES
		( 
			@ent_id
			, @end_id
			, @ene_numero
			, @ene_complemento
			, @ene_situacao
			, @ene_dataCriacao
			, @ene_dataAlteracao
			, @ene_enderecoPrincipal 
			, @ene_latitude 
			, @ene_longitude 
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Visao_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Visao_SELECT]
	
AS
BEGIN
	SELECT 
		vis_id
		, vis_nome
		
	FROM 
		SYS_Visao WITH(NOLOCK) 
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_SELECTBY_ent_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,ene_id
		,end_id
		,ene_numero
		,ene_complemento
		,ene_situacao
		,ene_dataCriacao
		,ene_dataAlteracao

	FROM
		SYS_EntidadeEndereco WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UsuarioLoginProvider]'
GO
CREATE TABLE [dbo].[SYS_UsuarioLoginProvider]
(
[LoginProvider] [varchar] (128) NOT NULL,
[ProviderKey] [varchar] (512) NOT NULL,
[usu_id] [uniqueidentifier] NOT NULL,
[Username] [varchar] (128) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UsuarioLoginProvider] on [dbo].[SYS_UsuarioLoginProvider]'
GO
ALTER TABLE [dbo].[SYS_UsuarioLoginProvider] ADD CONSTRAINT [PK_SYS_UsuarioLoginProvider] PRIMARY KEY CLUSTERED  ([LoginProvider], [ProviderKey], [usu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_LOAD]
	@LoginProvider VarChar (128)
	, @ProviderKey VarChar (512)
	, @usu_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 LoginProvider  
		, ProviderKey 
		, usu_id 
		, Username 

 	FROM
 		SYS_UsuarioLoginProvider
	WHERE 
		LoginProvider = @LoginProvider
		AND ProviderKey = @ProviderKey
		AND usu_id = @usu_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_DELETE]
	@ent_id uniqueidentifier
	,@ene_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_EntidadeEndereco	
	WHERE 
		ent_id = @ent_id
	and ene_id = @ene_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Visao_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Visao_LOAD] 
	@vis_id INT
	
AS
BEGIN
	SELECT Top 1 
		vis_id
		,vis_nome
 	FROM
 		 SYS_Visao
	WHERE 
		vis_id = @vis_id
		
		
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeEndereco_SELECTBY_end_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeEndereco_SELECTBY_end_id]
	@end_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,ene_id
		,end_id
		,ene_numero
		,ene_complemento
		,ene_situacao
		,ene_dataCriacao
		,ene_dataAlteracao

	FROM
		SYS_EntidadeEndereco WITH(NOLOCK)
	WHERE 
		end_id = @end_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_INSERT]
	@LoginProvider VarChar (128)
	, @ProviderKey VarChar (512)
	, @usu_id UniqueIdentifier
	, @Username VarChar (128)

AS
BEGIN
	INSERT INTO 
		SYS_UsuarioLoginProvider
		( 
			LoginProvider 
			, ProviderKey 
			, usu_id 
			, Username 
 
		)
	VALUES
		( 
			@LoginProvider 
			, @ProviderKey 
			, @usu_id 
			, @Username 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_TipoEntidade]'
GO
CREATE TABLE [dbo].[SYS_TipoEntidade]
(
[ten_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_TipoE__ten_i__09DE7BCC] DEFAULT (newsequentialid()),
[ten_nome] [varchar] (100) NOT NULL,
[ten_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_TipoEntidade_ten_situacao] DEFAULT ((1)),
[ten_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoEntidade_ten_dataCriacao] DEFAULT (getdate()),
[ten_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_TipoEntidade_ten_dataAlteracao] DEFAULT (getdate()),
[ten_integridade] [int] NOT NULL CONSTRAINT [DF_SYS_TipoEntidade_ten_integridade] DEFAULT ((0))
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_TipoEntidade] on [dbo].[SYS_TipoEntidade]'
GO
ALTER TABLE [dbo].[SYS_TipoEntidade] ADD CONSTRAINT [PK_SYS_TipoEntidade] PRIMARY KEY CLUSTERED  ([ten_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_Entidade_SelecionarEntidadesFilhas]'
GO

CREATE PROCEDURE [dbo].[MS_Entidade_SelecionarEntidadesFilhas]
	@entidadeId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
	     e.ent_id
	   , e.ten_id
	   , e.ent_codigo
	   , e.ent_nomeFantasia
	   , e.ent_razaoSocial
	   , e.ent_sigla
	   , e.ent_cnpj
	   , e.ent_inscricaoMunicipal
	   , e.ent_inscricaoEstadual
	   , e.ent_idSuperior
	   , e.ent_situacao
	   , e.ent_dataCriacao
	   , e.ent_dataAlteracao
	   , e.ent_integridade
	   , e.ent_urlAcesso
	   , e.ent_exibeLogoCliente
	   , e.ent_logoCliente
	   , e.tep_id
	   , e.tpl_id
	   , te.ten_nome

	FROM
		SYS_Entidade e WITH (NOLOCK)
		INNER JOIN SYS_TipoEntidade te WITH (NOLOCK) ON (te.ten_id = e.ten_id)
	
	WHERE
		    e.ent_idSuperior = @entidadeId
		AND e.ent_situacao <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Visao_INSERT]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_Visao_INSERT]
	@vis_id int
	, @vis_nome varchar(50)	
AS
BEGIN
	INSERT INTO
		SYS_Visao
		(
			vis_id
			, vis_nome
		)
		VALUES
		(
			@vis_id
			, @vis_nome
		)
		

	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_UPDATE]
	@LoginProvider VARCHAR (128)
	, @ProviderKey VARCHAR (512)
	, @usu_id UNIQUEIDENTIFIER
	, @Username VARCHAR (128)

AS
BEGIN
	UPDATE SYS_UsuarioLoginProvider 
	SET 
		Username = @Username 

	WHERE 
		LoginProvider = @LoginProvider 
		AND ProviderKey = @ProviderKey 
		AND usu_id = @usu_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_Entidade_SelecionarPorId]'
GO

CREATE PROCEDURE [dbo].[MS_Entidade_SelecionarPorId]
	@entidadeId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
	     e.ent_id
	   , e.ten_id
	   , e.ent_codigo
	   , e.ent_nomeFantasia
	   , e.ent_razaoSocial
	   , e.ent_sigla
	   , e.ent_cnpj
	   , e.ent_inscricaoMunicipal
	   , e.ent_inscricaoEstadual
	   , e.ent_idSuperior
	   , e.ent_situacao
	   , e.ent_dataCriacao
	   , e.ent_dataAlteracao
	   , e.ent_integridade
	   , e.ent_urlAcesso
	   , e.ent_exibeLogoCliente
	   , e.ent_logoCliente
	   , e.tep_id
	   , e.tpl_id
	   , te.ten_nome

	FROM
		SYS_Entidade e WITH (NOLOCK)
		INNER JOIN SYS_TipoEntidade te WITH (NOLOCK) ON (te.ten_id = e.ten_id)
	
	WHERE
		e.ent_id = @entidadeId
		AND e.ent_situacao	 <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_UPDATE]'
GO

-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:21
-- Description:	Altera a entidade preservando a data da criação e a integridade
--				16/11/2015 - Hélio Lima
--					Incluido validações no campo ent_exibeLogoCliente, pois o campo não aceita valores nulos
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_UPDATE]
		  @ent_id uniqueidentifier
		, @ten_id uniqueidentifier
		, @ent_codigo varchar(20)
		, @ent_nomeFantasia varchar(200)
		, @ent_razaoSocial varchar(200)
		, @ent_sigla  varchar(50)
		, @ent_cnpj varchar(14)
		, @ent_inscricaoMunicipal varchar(20)
		, @ent_inscricaoEstadual varchar(20)
		, @ent_idSuperior uniqueidentifier
		, @ent_situacao TinyInt			
		, @ent_dataAlteracao Datetime	
		, @ent_urlAcesso VARCHAR (200) = NULL
		, @ent_exibeLogoCliente BIT = NULL
		, @ent_logoCliente VARCHAR (2000) = NULL 
		, @tep_id INT = NULL	
		, @tpl_id INT = NULL
AS
BEGIN
	
	DECLARE @exibeLogoCliente BIT = 0

	-- Verifica se foi informado o parametro ent_exibeLogoCliente
	-- Se não foi informado (valor = null), seleciona qual é o valor já inserido para esse campo na tabela de entidades
	-- Foi incluida essa validação, para os sistemas que não atualizaram a dll do CoreSSO
	IF ( @ent_exibeLogoCliente IS NULL ) BEGIN 
		
		SET @exibeLogoCliente = (	SELECT	e.ent_exibeLogoCliente 
									FROM	SYS_Entidade e WITH (NOLOCK) 
									WHERE	e.ent_id = @ent_id )
	END 
	ELSE BEGIN
	
		SET @exibeLogoCliente = @ent_exibeLogoCliente
	
	END 

	--
	UPDATE 
		SYS_Entidade
	SET 		
		  ten_id = @ten_id
		, ent_codigo = @ent_codigo
		, ent_nomeFantasia = @ent_nomeFantasia
		, ent_razaoSocial = @ent_razaoSocial
		, ent_sigla = @ent_sigla
		, ent_cnpj = @ent_cnpj
		, ent_inscricaoMunicipal = @ent_inscricaoMunicipal
		, ent_inscricaoEstadual = @ent_inscricaoEstadual
		, ent_idSuperior = @ent_idSuperior
		, ent_situacao = @ent_situacao			
		, ent_dataAlteracao = @ent_dataAlteracao		
		, ent_urlAcesso = @ent_urlAcesso 
		, ent_exibeLogoCliente = @exibeLogoCliente	-- @ent_exibeLogoCliente 
		, ent_logoCliente = @ent_logoCliente 
		, tep_id = @tep_id 	
		, tpl_id = @tpl_id
	WHERE 
		ent_id = @ent_id
		
	RETURN ISNULL(@@ROWCOUNT, -1)		

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Visao_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Visao_DELETE]
	@vis_id INT
	
AS
BEGIN
	DELETE FROM 
		SYS_Visao
	WHERE
		vis_id = @vis_id
		
		
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_DELETE]
	@LoginProvider VARCHAR(128)
	, @ProviderKey VARCHAR(512)
	, @usu_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		SYS_UsuarioLoginProvider 
	WHERE 
		LoginProvider = @LoginProvider 
		AND ProviderKey = @ProviderKey 
		AND usu_id = @usu_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_Entidade_SelecionarTodas]'
GO

CREATE PROCEDURE [dbo].[MS_Entidade_SelecionarTodas]

AS
BEGIN

	SELECT
	     e.ent_id
	   , e.ten_id
	   , e.ent_codigo
	   , e.ent_nomeFantasia
	   , e.ent_razaoSocial
	   , e.ent_sigla
	   , e.ent_cnpj
	   , e.ent_inscricaoMunicipal
	   , e.ent_inscricaoEstadual
	   , e.ent_idSuperior
	   , e.ent_situacao
	   , e.ent_dataCriacao
	   , e.ent_dataAlteracao
	   , e.ent_integridade
	   , e.ent_urlAcesso
	   , e.ent_exibeLogoCliente
	   , e.ent_logoCliente
	   , e.tep_id
	   , e.tpl_id
	   , te.ten_nome

	FROM
		SYS_Entidade e WITH (NOLOCK)
		INNER JOIN SYS_TipoEntidade te WITH (NOLOCK) ON (te.ten_id = e.ten_id)
	
	WHERE	
		e.ent_situacao	 <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_SelectBy_ent_razaoSocial_ent_CNPJ]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 26/01/2010 13:55
-- Description:	utilizado no cadastro de entidades,
--              para saber se a razão social ou CNPJ já está cadastrada
--				filtrados por:
--					entidade (diferente do parametro), 					 
--                  razão social, cnpj, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_SelectBy_ent_razaoSocial_ent_CNPJ]	
	@ent_id uniqueidentifier
	,@ent_razaoSocial VARCHAR(200)
	,@ent_CNPJ VARCHAR(14)		
	,@ent_situacao TINYINT		
AS
BEGIN
	SELECT 
		ent_id
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_Entidade.ten_id = SYS_TipoEntidade.ten_id		
	WHERE
		ent_situacao <> 3
		AND ten_situacao <> 3
		AND (@ent_id is null or ent_id <> @ent_id)								
		AND (@ent_razaoSocial is null or ent_razaoSocial = @ent_razaoSocial)	
		AND (@ent_CNPJ is null or ent_CNPJ = @ent_CNPJ)					
		AND (@ent_situacao is null or ent_situacao = @ent_situacao)						
	ORDER BY
		ent_razaosocial
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoUnidadeAdministrativa_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoUnidadeAdministrativa_UPDATE]
		@tua_id uniqueidentifier
		,@tua_nome VarChar (100)
		,@tua_situacao TinyInt
		,@tua_dataCriacao DateTime
		,@tua_dataAlteracao DateTime
		,@tua_integridade Int

AS
BEGIN
	UPDATE SYS_TipoUnidadeAdministrativa
	SET 
		tua_nome = @tua_nome
		,tua_situacao = @tua_situacao
		,tua_dataCriacao = @tua_dataCriacao
		,tua_dataAlteracao = @tua_dataAlteracao
		,tua_integridade = @tua_integridade
	WHERE 
		tua_id = @tua_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_SELECT]
	
AS
BEGIN
	SELECT 
		LoginProvider
		,ProviderKey
		,usu_id
		,Username

	FROM 
		SYS_UsuarioLoginProvider WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_EntidadeSelecionarEntidadesFilhas]'
GO

CREATE PROCEDURE [dbo].[MS_EntidadeSelecionarEntidadesFilhas]
	@entidadeId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
	     e.ent_id
	   , e.ten_id
	   , e.ent_codigo
	   , e.ent_nomeFantasia
	   , e.ent_razaoSocial
	   , e.ent_sigla
	   , e.ent_cnpj
	   , e.ent_inscricaoMunicipal
	   , e.ent_inscricaoEstadual
	   , e.ent_idSuperior
	   , e.ent_situacao
	   , e.ent_dataCriacao
	   , e.ent_dataAlteracao
	   , e.ent_integridade
	   , e.ent_urlAcesso
	   , e.ent_exibeLogoCliente
	   , e.ent_logoCliente
	   , e.tep_id
	   , e.tpl_id
	   , te.ten_nome

	FROM
		SYS_Entidade e WITH (NOLOCK)
		INNER JOIN SYS_TipoEntidade te WITH (NOLOCK) ON (te.ten_id = e.ten_id)
	
	WHERE
		    e.ent_idSuperior = @entidadeId
		AND e.ent_situacao <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 20/01/2010 11:35
-- Description:	utilizado na busca de entidades, retorna as entidades
--              que não foram excluídas logicamente,
--				filtrados por:
--					entidade (diferente do parametro), tipo de entidade,					 
--                  razão social, nome fantasia, cnpj, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_SelectBy_All]	
	@ent_id uniqueidentifier
	,@ten_id uniqueidentifier
	,@ent_razaoSocial VARCHAR(200)
	,@ent_nomeFantasia VARCHAR(200)
	,@ent_CNPJ VARCHAR(14)	
	,@ent_codigo VARCHAR(20)
	,@ent_situacao TINYINT	
AS
BEGIN
	SELECT 
		ent_id
		,SYS_Entidade.ten_id
		,ent_codigo
		,ent_razaoSocial
		,ent_nomeFantasia
		,ent_CNPJ
		,ten_nome
		,CASE ent_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS ent_situacao
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_Entidade.ten_id = SYS_TipoEntidade.ten_id		
	WHERE
		ent_situacao <> 3
		AND ten_situacao <> 3
		AND (@ent_id is null or ent_id <> @ent_id)						
		AND (@ten_id is null or SYS_Entidade.ten_id = @ten_id)				
		AND (@ent_razaoSocial is null or ent_razaoSocial LIKE '%' + @ent_razaoSocial + '%')
		AND (@ent_nomeFantasia is null or ent_nomeFantasia LIKE '%' + @ent_nomeFantasia + '%')		
		AND (@ent_CNPJ is null or ent_CNPJ LIKE '%' + @ent_CNPJ + '%')			
		AND (@ent_codigo is null or ent_codigo LIKE '%' + @ent_codigo + '%')
		AND (@ent_situacao is null or ent_situacao = @ent_situacao)						
	ORDER BY
		ent_razaosocial
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoUnidadeAdministrativa_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoUnidadeAdministrativa_SELECT]
	
AS
BEGIN
	SELECT 
		tua_id
		,tua_nome
		,tua_situacao
		,tua_dataCriacao
		,tua_dataAlteracao
		,tua_integridade
	FROM 
		SYS_TipoUnidadeAdministrativa WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_Seleciona_Usuario_By_LoginProvider_ProviderKey]'
GO

CREATE PROCEDURE [dbo].[MS_Seleciona_Usuario_By_LoginProvider_ProviderKey]
	@LoginProvider VarChar (128)
	, @ProviderKey VarChar (128)
	
AS
BEGIN

	SELECT U.usu_id,
			U.ent_id,
			U.pes_id,
			U.usu_login,
			U.usu_dominio,
			U.usu_email,
			U.usu_senha,
			U.usu_criptografia,
			U.usu_situacao,
			U.usu_dataCriacao,
			U.usu_dataAlteracao,
			U.usu_integridade,
			U.usu_integracaoAD,
			U.usu_integracaoExterna
 	FROM
 		SYS_UsuarioLoginProvider ULP
		INNER JOIN SYS_Usuario U ON ULP.usu_id = U.usu_id
	WHERE 
		ULP.LoginProvider = @LoginProvider
		AND ULP.ProviderKey = @ProviderKey						
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_EntidadeSelecionarPorIdGrupo]'
GO
-- ========================================================================
-- Description:	 Seleciona as entidades por grupo		 
-- ========================================================================
CREATE PROCEDURE [dbo].[MS_EntidadeSelecionarPorIdGrupo]	
	@idEntidade uniqueidentifier
	,@idGrupo uniqueidentifier
AS
BEGIN
	SELECT
	    SYS_Entidade.ent_id
	   , SYS_Entidade.ten_id
	   , ent_codigo
	   , ent_nomeFantasia
	   , ent_razaoSocial
	   , ent_sigla
	   , ent_cnpj
	   , ent_inscricaoMunicipal
	   , ent_inscricaoEstadual
	   , ent_idSuperior
	   , ent_situacao
	   , ent_dataCriacao
	   , ent_dataAlteracao
	   , ent_integridade
	   , ent_urlAcesso
	   , ent_exibeLogoCliente
	   , ent_logoCliente
	   , tep_id
	   , tpl_id
	   , SYS_TipoEntidade.ten_nome
		
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_TipoEntidade.ten_id = SYS_Entidade.ten_id		
	WHERE
		ent_situacao <> 3
		AND ten_situacao <> 3		
		AND (@idEntidade is null or SYS_Entidade.ent_id = @idEntidade)
		AND Exists (		
				SELECT 
					ent_id 
				FROM 
					SYS_UsuarioGrupoUA WITH (NOLOCK)
				WHERE 
					SYS_UsuarioGrupoUA.ent_id = SYS_Entidade.ent_id
				AND SYS_UsuarioGrupoUA.gru_id = @idGrupo
		)
		
	ORDER BY
		ent_razaosocial
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 09:43
-- Description:	Seleciona o valor do campo integridade da tabela entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_Select_Integridade]
		@ent_id uniqueidentifier
AS
BEGIN
	SELECT 			
		ent_integridade
	FROM
		SYS_Entidade WITH (NOLOCK)
	WHERE 		
		ent_id = @ent_id
		
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoUnidadeAdministrativa_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoUnidadeAdministrativa_LOAD]
	@tua_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		tua_id
		,tua_nome
		,tua_situacao
		,tua_dataCriacao
		,tua_dataAlteracao
		,tua_integridade

 	FROM
 		SYS_TipoUnidadeAdministrativa
	WHERE 
		tua_id = @tua_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_LOADBy_ent_id_usu_login]'
GO
-- ==========================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 15/03/2010 12:25
-- Description:	Carrega os dados do usuário através do login.
-- ==========================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_LOADBy_ent_id_usu_login]
	@ent_id uniqueidentifier
	, @usu_login VARCHAR(500)
AS
BEGIN
	SELECT TOP 1
		usu_id,
		usu_login,
		usu_dominio,
		usu_email,
		usu_senha,
		usu_criptografia,
		usu_situacao,
		usu_dataCriacao,
		usu_dataAlteracao,
		pes_id,
		usu_integridade,
		ent_id,
		usu_integracaoAD, 
		usu_integracaoExterna
	FROM
		SYS_Usuario WITH (NOLOCK)
	WHERE
		usu_situacao <> 3
		AND ent_id = @ent_id
		AND usu_login = @usu_login		
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_STP_SYS_UsuarioLoginProvider_LOADBy_LoginProvider_ProviderKey]'
GO

CREATE PROCEDURE [dbo].[NEW_STP_SYS_UsuarioLoginProvider_LOADBy_LoginProvider_ProviderKey]
	@LoginProvider VarChar (128)
	, @ProviderKey VarChar (512)
	
AS
BEGIN

	SELECT U.*
 	FROM
 		SYS_UsuarioLoginProvider ULP
		INNER JOIN SYS_Usuario U ON ULP.usu_id = U.usu_id
	WHERE 
		ULP.LoginProvider = @LoginProvider
		AND ULP.ProviderKey = @ProviderKey						
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_MensagemSistema]'
GO
CREATE TABLE [dbo].[SYS_MensagemSistema]
(
[mss_id] [int] NOT NULL IDENTITY(1, 1),
[mss_chave] [varchar] (100) NOT NULL,
[mss_valor] [varchar] (max) NOT NULL,
[mss_descricao] [varchar] (200) NULL,
[mss_situacao] [tinyint] NOT NULL,
[mss_dataCriacao] [datetime] NOT NULL,
[mss_dataAlteracao] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_MensagemSistema] on [dbo].[SYS_MensagemSistema]'
GO
ALTER TABLE [dbo].[SYS_MensagemSistema] ADD CONSTRAINT [PK_SYS_MensagemSistema] PRIMARY KEY CLUSTERED  ([mss_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_MensagemSistema_SELECT]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_MensagemSistema_SELECT]
	
AS
BEGIN
	SELECT 
		mss_id
		,mss_chave
		,mss_valor
		,mss_descricao
		,mss_situacao
		,mss_dataCriacao
		,mss_dataAlteracao
	FROM 
		SYS_MensagemSistema WITH(NOLOCK) 
	WHERE
		mss_situacao <> 3
	ORDER BY
		mss_chave	
	
	SELECT @@ROWCOUNT;
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:49
-- Description:	Incrementa uma unidade no campo integridade da entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_INCREMENTA_INTEGRIDADE]
		@ent_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_Entidade
	SET 		
		ent_integridade = ent_integridade + 1		
	WHERE 
		ent_id = @ent_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoUnidadeAdministrativa_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoUnidadeAdministrativa_INSERT]
		@tua_nome VarChar (100)
		,@tua_situacao TinyInt
		,@tua_dataCriacao DateTime
		,@tua_dataAlteracao DateTime
		,@tua_integridade Int

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_TipoUnidadeAdministrativa
		( 
			tua_nome
			,tua_situacao
			,tua_dataCriacao
			,tua_dataAlteracao
			,tua_integridade
		)
	OUTPUT inserted.tua_id INTO @ID
	VALUES
		( 
			@tua_nome
			,@tua_situacao
			,@tua_dataCriacao
			,@tua_dataAlteracao
			,@tua_integridade
		)	
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id]'
GO
 

CREATE PROCEDURE [dbo].[NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id]
       @usu_id UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
			LoginProvider, ProviderKey, ULP.usu_id, Username
      FROM
            SYS_UsuarioLoginProvider ULP with(nolock)

             INNER JOIN SYS_Usuario U ON ULP.usu_id = U.usu_id
       WHERE
             ULP.usu_id = @usu_id                          
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_MensagemSistema_Update]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_MensagemSistema_Update]
	@mss_id INT
	, @mss_chave VARCHAR (100)
	, @mss_valor VARCHAR (MAX)
	, @mss_descricao VARCHAR (200)
	, @mss_situacao TINYINT
	, @mss_dataAlteracao DATETIME

AS
BEGIN
	UPDATE SYS_MensagemSistema
	SET 
		mss_chave = @mss_chave 
		, mss_valor = @mss_valor 
		, mss_descricao = @mss_descricao 
		, mss_situacao = @mss_situacao 
		, mss_dataAlteracao = @mss_dataAlteracao 

	WHERE 
		mss_id = @mss_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_Modulos_SelecionarModulosPorIdGrupo]'
GO

CREATE PROCEDURE [dbo].[MS_Modulos_SelecionarModulosPorIdGrupo]
	  @sistemaId	INT = NULL
	, @grupoId		UNIQUEIDENTIFIER
	
AS
BEGIN

	SELECT 

		  gp.gru_id
		, g.gru_nome

		, gp.sis_id
		, s.sis_nome

		, gp.mod_id
		, m.mod_nome
		, m.mod_idPai

		, gp.gru_id
		, gp.grp_consultar
		, gp.grp_inserir
		, gp.grp_alterar
		, gp.grp_excluir

	FROM 
		SYS_GrupoPermissao     gp WITH (NOLOCK)
		INNER JOIN SYS_Grupo   g  WITH (NOLOCK) ON ( g.gru_id = gp.gru_id )
		INNER JOIN SYS_Sistema s  WITH (NOLOCK) ON ( s.sis_id = gp.sis_id )
		INNER JOIN SYS_Modulo  m  WITH (NOLOCK) ON ( m.mod_id = gp.mod_id )
	
	WHERE
			gp.gru_id = @grupoId
		AND gp.sis_id = ISNULL(@sistemaId, gp.sis_id) 
		AND m.mod_situacao <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:49
-- Description:	Decrementa uma unidade no campo integridade da entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_DECREMENTA_INTEGRIDADE]
		@ent_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_Entidade
	SET 		
		ent_integridade = ent_integridade - 1		
	WHERE 
		ent_id = @ent_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoUnidadeAdministrativa_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoUnidadeAdministrativa_DELETE]
	@tua_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_TipoUnidadeAdministrativa
	WHERE 
		tua_id = @tua_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_MensagemSistema_UpdateSituacao]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_MensagemSistema_UpdateSituacao]
	@mss_id INT
	, @mss_situacao TINYINT
	, @mss_dataAlteracao DATETIME

AS
BEGIN
	UPDATE SYS_MensagemSistema
	SET 
		mss_situacao = @mss_situacao 
		, mss_dataAlteracao = @mss_dataAlteracao 

	WHERE 
		mss_id = @mss_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_UPDATE]
	@pes_id UNIQUEIDENTIFIER
	, @pse_id UNIQUEIDENTIFIER
	, @end_id UNIQUEIDENTIFIER
	, @pse_numero VARCHAR (20)
	, @pse_complemento VARCHAR (100)
	, @pse_situacao TINYINT
	, @pse_dataCriacao DATETIME
	, @pse_dataAlteracao DATETIME
	, @pse_enderecoPrincipal BIT = NULL
	, @pse_latitude DECIMAL (15,2) = NULL
	, @pse_longitude DECIMAL (15,2) = NULL

AS
BEGIN
	UPDATE PES_PessoaEndereco 
	SET 
		end_id = @end_id 
		, pse_numero = @pse_numero 
		, pse_complemento = @pse_complemento 
		, pse_situacao = @pse_situacao 
		, pse_dataCriacao = @pse_dataCriacao 
		, pse_dataAlteracao = @pse_dataAlteracao 
		, pse_enderecoPrincipal = @pse_enderecoPrincipal 
		, pse_latitude = @pse_latitude 
		, pse_longitude = @pse_longitude 

	WHERE 
		pes_id = @pes_id 
		AND pse_id = @pse_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarGrupoPorId]'
GO

CREATE PROCEDURE [dbo].[MS_SelecionarGrupoPorId]
	@idGrupo uniqueidentifier
AS
BEGIN
	SELECT 
		  g.gru_id
		, g.gru_nome
		, g.gru_situacao
		, g.gru_dataCriacao
		, g.gru_dataAlteracao
		, g.vis_id
		, g.sis_id		
	FROM
		SYS_Grupo g WITH (NOLOCK)
	WHERE
			g.gru_id = @idGrupo
		AND g.gru_situacao IN (1, 4)
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UnidadeFederativa_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:35
-- Description:	Seleciona o valor do campo integridade da tabela de unidade federativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UnidadeFederativa_Select_Integridade]
		@unf_id uniqueidentifier
AS
BEGIN
	SELECT 			
		unf_integridade
	FROM
		END_UnidadeFederativa WITH (NOLOCK)
	WHERE 
		unf_id = @unf_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoMeioContato_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoMeioContato_UPDATE]
		@tmc_id uniqueidentifier
		,@tmc_nome VarChar (100)
		,@tmc_validacao TinyInt
		,@tmc_situacao TinyInt
		,@tmc_dataCriacao DateTime
		,@tmc_dataAlteracao DateTime
		,@tmc_integridade Int

AS
BEGIN
	UPDATE SYS_TipoMeioContato 
	SET 
		tmc_nome = @tmc_nome
		,tmc_validacao = @tmc_validacao
		,tmc_situacao = @tmc_situacao		
		,tmc_dataCriacao = @tmc_dataCriacao
		,tmc_dataAlteracao = @tmc_dataAlteracao
		,tmc_integridade = @tmc_integridade
	WHERE 
		tmc_id = @tmc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_MensagemSistema_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_MensagemSistema_UPDATE]
	@mss_id INT
	, @mss_chave VARCHAR (100)
	, @mss_valor VARCHAR(MAX)
	, @mss_descricao VARCHAR (200)
	, @mss_situacao TINYINT
	, @mss_dataCriacao DATETIME
	, @mss_dataAlteracao DATETIME

AS
BEGIN
	UPDATE SYS_MensagemSistema 
	SET 
		mss_chave = @mss_chave 
		, mss_valor = @mss_valor 
		, mss_descricao = @mss_descricao 
		, mss_situacao = @mss_situacao 
		, mss_dataCriacao = @mss_dataCriacao 
		, mss_dataAlteracao = @mss_dataAlteracao 

	WHERE 
		mss_id = @mss_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,pse_id
		,end_id
		,pse_numero
		,pse_complemento
		,pse_situacao
		,pse_dataCriacao
		,pse_dataAlteracao
		,pse_enderecoPrincipal
		,pse_latitude
		,pse_longitude

	FROM 
		PES_PessoaEndereco WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarGruposPermissaoPorIdGrupo]'
GO

CREATE PROCEDURE [dbo].[MS_SelecionarGruposPermissaoPorIdGrupo]
	@grupoId UNIQUEIDENTIFIER
AS
BEGIN

    SELECT 
          gp.gru_id
	    , g.gru_nome
        , gp.sis_id
	    , s.sis_nome
	    , gp.mod_id
	    , m.mod_nome
	    , m.mod_idPai
	    , gp.grp_alterar
	    , gp.grp_consultar
	    , gp.grp_excluir
	    , gp.grp_inserir
	    , sm.msm_url

    FROM 
        SYS_Grupo g WITH (NOLOCK)

        INNER JOIN SYS_GrupoPermissao gp WITH (NOLOCK) 
            ON ( gp.gru_id = g.gru_id )

        INNER JOIN SYS_Sistema s WITH (NOLOCK) 
            ON ( s.sis_id = gp.sis_id )

        INNER JOIN SYS_Modulo m WITH (NOLOCK) 
            ON (    m.sis_id = gp.sis_id 
                AND m.mod_id = gp.mod_id )

        LEFT JOIN SYS_ModuloSiteMap sm WITH (NOLOCK) 
            ON (    sm.sis_id = m.sis_id 
                AND sm.mod_id = m.mod_id )

    WHERE 
		    m.mod_situacao <> 3
	    AND s.sis_situacao <> 3
        AND g.gru_id = @grupoId

    ORDER BY 
	    m.mod_nome
		
	SELECT @@ROWCOUNT
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UnidadeFederativa_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:40
-- Description:	Incrementa uma unidade no campo integridade da tabela de unidade federativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UnidadeFederativa_INCREMENTA_INTEGRIDADE]
		@unf_id uniqueidentifier

AS
BEGIN
	UPDATE END_UnidadeFederativa
	SET 
		unf_integridade = unf_integridade + 1
	WHERE 
		unf_id = @unf_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoMeioContato_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoMeioContato_SELECT]
	
AS
BEGIN
	SELECT 
		tmc_id
		,tmc_nome
		,tmc_validacao
		,tmc_situacao
		,tmc_dataCriacao
		,tmc_dataAlteracao
		,tmc_integridade
	FROM 
		SYS_TipoMeioContato WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_ent_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,uae_id
		,end_id
		,uae_numero
		,uae_complemento
		,uae_situacao
		,uae_dataCriacao
		,uae_dataAlteracao

	FROM
		SYS_UnidadeAdministrativaEndereco WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END
	

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_LoginProviderDominioPermitido]'
GO
CREATE TABLE [dbo].[SYS_LoginProviderDominioPermitido]
(
[ent_id] [uniqueidentifier] NOT NULL,
[LoginProvider] [varchar] (128) NOT NULL,
[Dominios] [varchar] (1024) NULL,
[Tenant] [varchar] (1024) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_LoginProviderDominioPermitido] on [dbo].[SYS_LoginProviderDominioPermitido]'
GO
ALTER TABLE [dbo].[SYS_LoginProviderDominioPermitido] ADD CONSTRAINT [PK_SYS_LoginProviderDominioPermitido] PRIMARY KEY CLUSTERED  ([ent_id], [LoginProvider]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProviderDominioPermitido_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProviderDominioPermitido_LOAD]
	@ent_id UniqueIdentifier
	, @LoginProvider VarChar (128)
	
AS
BEGIN
	SELECT	Top 1
		 ent_id  
		, LoginProvider 
		, Dominios 
		, Tenant
 	FROM
 		SYS_LoginProviderDominioPermitido
	WHERE 
		ent_id = @ent_id
		AND LoginProvider = @LoginProvider
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_MensagemSistema_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_MensagemSistema_SELECT]
	
AS
BEGIN
	SELECT 
		mss_id
		,mss_chave
		,mss_valor
		,mss_descricao
		,mss_situacao
		,mss_dataCriacao
		,mss_dataAlteracao

	FROM 
		SYS_MensagemSistema WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_LOAD]
	@pes_id UniqueIdentifier
	, @pse_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 pes_id  
		, pse_id 
		, end_id 
		, pse_numero 
		, pse_complemento 
		, pse_situacao 
		, pse_dataCriacao 
		, pse_dataAlteracao 
		, pse_enderecoPrincipal 
		, pse_latitude 
		, pse_longitude 

 	FROM
 		PES_PessoaEndereco
	WHERE 
		pes_id = @pes_id
		AND pse_id = @pse_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarGruposPorIdSistema]'
GO

CREATE PROCEDURE [dbo].[MS_SelecionarGruposPorIdSistema]
	@idSistema INT
AS
BEGIN
	SELECT 
		  g.gru_id
		, g.gru_nome
		, g.gru_situacao
		, g.gru_dataCriacao
		, g.gru_dataAlteracao
		, g.vis_id
		, g.sis_id		
	FROM
		SYS_Grupo g WITH (NOLOCK)
	WHERE
			g.sis_id = @idSistema
		AND g.gru_situacao IN (1, 4)
	ORDER BY
		g.gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UnidadeFederativa_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:41
-- Description:	Decrementa uma unidade no campo integridade da tabela de unidade federativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UnidadeFederativa_DECREMENTA_INTEGRIDADE]
		@unf_id uniqueidentifier
AS
BEGIN
	UPDATE END_UnidadeFederativa
	SET 
		unf_integridade = unf_integridade - 1
	WHERE 
		unf_id = @unf_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoMeioContato_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoMeioContato_LOAD]
	@tmc_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		tmc_id
		,tmc_nome
		,tmc_validacao
		,tmc_situacao
		,tmc_dataCriacao
		,tmc_dataAlteracao
		,tmc_integridade

 	FROM
 		SYS_TipoMeioContato
	WHERE 
		tmc_id = @tmc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SelectBy_sis_id_vis_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 19/01/2010 15:50
-- Description:	usado na busca de grupos, retorna os grupos de que
--				estao no mesmo sistema e possuem a mesma visao.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SelectBy_sis_id_vis_id]
	@sis_id INT
	,@vis_id INT
AS
BEGIN
	SELECT 
		gru_id
		, vis_id
		, sis_id
		, gru_nome
	FROM
		SYS_Grupo WITH(NOLOCK)
	WHERE
		gru_situacao <> 3
		AND ((@sis_id IS NULL) OR (SYS_Grupo.sis_id = @sis_id))
		AND ((@vis_id IS NULL) OR (SYS_Grupo.vis_id = @vis_id))
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_uad_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_uad_id]
	@uad_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,uae_id
		,end_id
		,uae_numero
		,uae_complemento
		,uae_situacao
		,uae_dataCriacao
		,uae_dataAlteracao

	FROM
		SYS_UnidadeAdministrativaEndereco WITH(NOLOCK)
	WHERE 
		uad_id = @uad_id 
END

	

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProviderDominioPermitido_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProviderDominioPermitido_INSERT]
	@ent_id UniqueIdentifier
	, @LoginProvider VarChar (128)
	, @Dominios VarChar (1024)
	, @Tenant VarChar (1024)

AS
BEGIN
	INSERT INTO 
		SYS_LoginProviderDominioPermitido
		( 
			ent_id 
			, LoginProvider 
			, Dominios
			, Tenant 
 
		)
	VALUES
		( 
			@ent_id 
			, @LoginProvider 
			, @Dominios 
			, @Tenant
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_MensagemSistema_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_MensagemSistema_LOAD]
	@mss_id Int
	
AS
BEGIN
	SELECT	Top 1
		 mss_id  
		, mss_chave 
		, mss_valor 
		, mss_descricao 
		, mss_situacao 
		, mss_dataCriacao 
		, mss_dataAlteracao 

 	FROM
 		SYS_MensagemSistema
	WHERE 
		mss_id = @mss_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_INSERT]
		@pes_id uniqueidentifier
			, @end_id uniqueidentifier
			, @pse_numero VarChar (20)
			, @pse_complemento varchar(100)
			, @pse_situacao tinyint
			, @pse_dataCriacao datetime
			, @pse_dataAlteracao datetime
			, @pse_enderecoPrincipal Bit = NULL
	        , @pse_latitude Decimal(15,10) = NULL
	        , @pse_longitude Decimal(15,10) = NULL


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_PessoaEndereco
		(	
			pes_id
			, end_id
			, pse_numero
			, pse_complemento
			, pse_situacao
			, pse_dataCriacao
			, pse_dataAlteracao
			, pse_enderecoPrincipal 
			, pse_latitude 
			, pse_longitude 
		)
	OUTPUT inserted.pse_id INTO @ID
	VALUES
		( 
			@pes_id
			, @end_id
			, @pse_numero
			, @pse_complemento
			, @pse_situacao
			, @pse_dataCriacao
			, @pse_dataAlteracao
			, @pse_enderecoPrincipal 
			, @pse_latitude 
			, @pse_longitude 
		)
	SELECT ID FROM @ID
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarUnidadesAdministrativasFilhas]'
GO
-- =============================================
-- Create date: 26/08/2015
-- Description:	Retorna as unidades administrativas conforme a unidade superior
-- =============================================
CREATE PROCEDURE [dbo].[MS_SelecionarUnidadesAdministrativasFilhas]
	@ent_id uniqueidentifier,
	@uad_idSuperior uniqueidentifier
AS
BEGIN


	SELECT
		UA.ent_id,
		UA.uad_id,
		UA.tua_id,
		UA.uad_codigo,
		UA.uad_nome,
		UA.uad_sigla,
		UA.uad_idSuperior,
		UA.uad_situacao,
		UA.uad_dataCriacao,
		UA.uad_dataAlteracao,
		UA.uad_integridade,
		SYS_TipoUnidadeAdministrativa.tua_nome
	FROM
		SYS_UnidadeAdministrativa as UA WITH (NOLOCK)
	INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
		ON UA.tua_id = SYS_TipoUnidadeAdministrativa.tua_id 
	WHERE						 
		(UA.ent_id = ISNULL(@ent_id, UA.ent_id) )
		AND (UA.uad_idSuperior = ISNULL(@uad_idSuperior, UA.uad_idSuperior )) 	
		AND UA.uad_situacao <> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UF_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 20/01/2010 14:00
-- Description:	utilizado na busca de unidade federativa, retorna as unidades
--              federativas que não foram excluídas logicamente,
--				filtrados por:
--					id, pais, nome, sigla, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UF_SelectBy_All]	
	@unf_id uniqueidentifier
	,@pai_id uniqueidentifier
	,@unf_nome VARCHAR(100)
	,@unf_sigla VARCHAR(2)		
	,@unf_situacao TINYINT
AS
BEGIN
	SELECT 
	    unf_id
		,pai_id
		,unf_nome
		,unf_sigla
		, CASE unf_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS unf_situacao
	FROM
		END_UnidadeFederativa WITH (NOLOCK)		
	WHERE
		unf_situacao <> 3
		AND (@unf_id is null or unf_id = @unf_id)		
		AND (@pai_id is null or pai_id = @pai_id)			
		AND (@unf_nome is null or unf_nome LIKE '%' + @unf_nome + '%')
		AND (@unf_sigla is null or unf_sigla = @unf_sigla)				
		AND (@unf_situacao is null or unf_situacao = @unf_situacao)				
	ORDER BY
		unf_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoMeioContato_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoMeioContato_INSERT]
		@tmc_nome VarChar (100)
		,@tmc_validacao TinyInt		
		,@tmc_situacao TinyInt
		,@tmc_dataCriacao DateTime
		,@tmc_dataAlteracao DateTime		
		,@tmc_integridade Int		

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_TipoMeioContato
		( 
			tmc_nome
			,tmc_validacao	
			,tmc_situacao		
			,tmc_dataCriacao
			,tmc_dataAlteracao
			,tmc_integridade			
 
		)
	OUTPUT inserted.tmc_id INTO @ID
	VALUES
		( 
			@tmc_nome			
			,@tmc_validacao
			,@tmc_situacao
			,@tmc_dataCriacao
			,@tmc_dataAlteracao
			,@tmc_integridade					
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_ent_id_usu_login_usu_email_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 24/03/2010 17:36
-- Description:	utilizado no cadastro de usuarios,
--              para saber se o login, email ou pessoa está cadastrada
--				filtrados por:
--					usuario (diferente do parametro), 					 
--                  login, email, pessoa, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_ent_id_usu_login_usu_email_pes_id]	
 	@ent_id uniqueidentifier
 	, @usu_id uniqueidentifier
	, @usu_login VARCHAR(500)
	, @usu_dominio VARCHAR(100)
	, @usu_email VARCHAR(500)
	, @pes_id uniqueidentifier
	, @usu_situacao TINYINT		
WITH RECOMPILE
AS
BEGIN
	IF (@usu_id IS NULL)
	BEGIN
		SET @usu_id = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);
	END

	SELECT TOP 1
		usu_id
	FROM
		SYS_Usuario WITH (NOLOCK)		
	WHERE
		usu_situacao <> 3
		AND (ent_id = ISNULL(@ent_id, ent_id))
		AND (usu_id <> @usu_id)						
		AND (@usu_login is null or usu_login = @usu_login)
		AND (@usu_dominio is null or @usu_dominio = usu_dominio)
		AND (@usu_email is null or usu_email = @usu_email)					
		AND (@pes_id is null or pes_id = @pes_id)					
		AND (@usu_situacao is null or usu_situacao = @usu_situacao)
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_end_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativaEndereco_SELECTBY_end_id]
	@end_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,uae_id
		,end_id
		,uae_numero
		,uae_complemento
		,uae_situacao
		,uae_dataCriacao
		,uae_dataAlteracao

	FROM
		SYS_UnidadeAdministrativaEndereco WITH(NOLOCK)
	WHERE 
		end_id = @end_id 

END

	

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProviderDominioPermitido_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProviderDominioPermitido_UPDATE]
	@ent_id UNIQUEIDENTIFIER
	, @LoginProvider VARCHAR (128)
	, @Dominios VARCHAR (1024)
	, @Tenant VARCHAR (1024)


AS
BEGIN
	UPDATE SYS_LoginProviderDominioPermitido 
	SET 
		Dominios = @Dominios,
		Tenant= @Tenant
	WHERE 
		ent_id = @ent_id 
		AND LoginProvider = @LoginProvider 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_MensagemSistema_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_MensagemSistema_INSERT]
	@mss_chave VarChar (100)
	, @mss_valor VARCHAR(MAX)
	, @mss_descricao VarChar (200)
	, @mss_situacao TinyInt
	, @mss_dataCriacao DateTime
	, @mss_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		SYS_MensagemSistema
		( 
			mss_chave 
			, mss_valor 
			, mss_descricao 
			, mss_situacao 
			, mss_dataCriacao 
			, mss_dataAlteracao 
 
		)
	VALUES
		( 
			@mss_chave 
			, @mss_valor 
			, @mss_descricao 
			, @mss_situacao 
			, @mss_dataCriacao 
			, @mss_dataAlteracao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaEndereco_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaEndereco_DELETE]
	@pes_id UNIQUEIDENTIFIER
	, @pse_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		PES_PessoaEndereco 
	WHERE 
		pes_id = @pes_id 
		AND pse_id = @pse_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarUsuarioPorIdUsuario]'
GO

CREATE PROCEDURE [dbo].[MS_SelecionarUsuarioPorIdUsuario]
	@idUsuario uniqueidentifier
AS
BEGIN
	
	SELECT 
		  u.usu_id
		, u.usu_login
		, u.usu_dominio
		, u.usu_email
		, u.usu_senha
		, u.usu_criptografia
		, u.usu_situacao
		, u.usu_dataCriacao
		, u.usu_dataAlteracao
		, u.pes_id
		, u.usu_integridade
		, u.ent_id
		, u.usu_integracaoAD
		, u.usu_integracaoExterna

	FROM 
		SYS_Usuario	u  WITH (NOLOCK)
		--INNER JOIN SYS_UsuarioGrupo ug WITH (NOLOCK) ON ( ug.usu_id = u.usu_id )

	WHERE
			u. usu_situacao <> 3		
		AND u.usu_id		=  @idUsuario
		--AND ug.usg_situacao <> 3
		
	ORDER BY
		u.usu_login
		
	SELECT @@ROWCOUNT
END

--EXEC msdb..spGrantExectoAllRoutines N'user_coresso', N'CoreSSO_DEV'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoMeioContato_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoMeioContato_DELETE]
	@tmc_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_TipoMeioContato
	WHERE 
		tmc_id = @tmc_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_SistemaEntidade_Update_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 06/08/2010 11:34
-- Description:	utilizado para realizar alteração do campo de situação 
--				referente ao Sistema Entidade. Filtrada por: 
--					sis_id, ent_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_SistemaEntidade_Update_Situacao]	
		@sis_id INT
		,@ent_id uniqueidentifier
		,@sen_situacao TINYINT
AS
BEGIN
	UPDATE SYS_SistemaEntidade
	SET 
		sen_situacao = @sen_situacao
	WHERE 
		sis_id = @sis_id
	AND ent_id = @ent_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProviderDominioPermitido_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_LoginProviderDominioPermitido_DELETE]
	@ent_id UNIQUEIDENTIFIER
	, @LoginProvider VARCHAR(128)

AS
BEGIN
	DELETE FROM 
		SYS_LoginProviderDominioPermitido 
	WHERE 
		ent_id = @ent_id 
		AND LoginProvider = @LoginProvider 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_MensagemSistema_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_MensagemSistema_DELETE]
	@mss_id INT

AS
BEGIN
	DELETE FROM 
		SYS_MensagemSistema 
	WHERE 
		mss_id = @mss_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDocumento_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaDocumento_UPDATE]
      @pes_id				UNIQUEIDENTIFIER
	, @tdo_id				UNIQUEIDENTIFIER
	, @psd_numero			VARCHAR (50)
	, @psd_dataEmissao		DATE
	, @psd_orgaoEmissao		VARCHAR (200)
	, @unf_idEmissao		UNIQUEIDENTIFIER
	, @psd_infoComplementares VARCHAR (1000)
	, @psd_situacao			TINYINT
	, @psd_dataCriacao		DATETIME
	, @psd_dataAlteracao	DATETIME
	, @psd_categoria		VARCHAR (64) = NULL
	, @psd_classificacao	VARCHAR (64) = NULL
	, @psd_csm				VARCHAR (32) = NULL
	, @psd_dataEntrada		DATETIME = NULL
	, @psd_dataValidade		DATETIME = NULL
	, @pai_idOrigem			UNIQUEIDENTIFIER = NULL
	, @psd_serie			VARCHAR (32) = NULL
	, @psd_tipoGuarda		VARCHAR (128) = NULL
	, @psd_via				VARCHAR (16) = NULL
	, @psd_secao			VARCHAR (32) = NULL
	, @psd_zona				VARCHAR (16) = NULL
	, @psd_regiaoMilitar	VARCHAR (16) = NULL
	, @psd_numeroRA			VARCHAR (64) = NULL
	, @psd_dataExpedicao	DATE = NULL
AS
BEGIN

	UPDATE 
		PES_PessoaDocumento

	SET 		
		  psd_numero = @psd_numero
		, psd_dataEmissao = @psd_dataEmissao
		, psd_orgaoEmissao = @psd_orgaoEmissao
		, unf_idEmissao = @unf_idEmissao
		, psd_infoComplementares = @psd_infoComplementares
		, psd_situacao = @psd_situacao
		, psd_dataCriacao = @psd_dataCriacao
		, psd_dataAlteracao = @psd_dataAlteracao
		, psd_categoria = @psd_categoria 
		, psd_classificacao = @psd_classificacao 
		, psd_csm = @psd_csm 
		, psd_dataEntrada = @psd_dataEntrada 
		, psd_dataValidade = @psd_dataValidade 
		, pai_idOrigem = @pai_idOrigem 
		, psd_serie = @psd_serie 
		, psd_tipoGuarda = @psd_tipoGuarda 
		, psd_via = @psd_via 
		, psd_secao = @psd_secao 
		, psd_zona = @psd_zona 
		, psd_regiaoMilitar = @psd_regiaoMilitar 
		, psd_numeroRA = @psd_numeroRA
		, psd_dataExpedicao = @psd_dataExpedicao

	WHERE 
			pes_id = @pes_id
		AND	tdo_id = @tdo_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarUsuariosDaUnidadeAdministrativa]'
GO
-- ===========================================================================
-- Create date: 26/08/2015
-- Description:	Retorna todos os usuários que não foram excluídos logicamente
--				filtrando por: entidade e unidade administrativa.
-- ===========================================================================
create PROCEDURE [dbo].[MS_SelecionarUsuariosDaUnidadeAdministrativa]
	@ent_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		Usu.usu_id 
		, Usu.usu_login 
		, Usu.usu_email
		, Usu.usu_senha
		, Usu.usu_criptografia
		, Usu.usu_situacao
		, Usu.pes_id
		, Pes.pes_nome 		
		, Pes.pes_sexo
		, Pes.pes_dataNascimento
		, Usu.usu_dataCriacao
		, Usu.usu_dataAlteracao
	FROM
		SYS_Usuario AS Usu WITH(NOLOCK)
		INNER JOIN SYS_UsuarioGrupo AS Usg WITH(NOLOCK)
			ON Usu.usu_id = Usg.usu_id
			AND Usg.usg_situacao <> 3
		INNER JOIN SYS_UsuarioGrupoUA AS Ugu WITH(NOLOCK)
			ON Usg.usu_id = Ugu.usu_id 
			AND Usg.gru_id = Ugu.gru_id
		INNER JOIN SYS_UnidadeAdministrativa AS Uad WITH(NOLOCK)
			ON Ugu.ent_id = Uad.ent_id 
			AND Ugu.uad_id = Uad.uad_id	
			AND Uad.uad_situacao <> 3
		LEFT JOIN PES_Pessoa AS Pes WITH(NOLOCK)
			ON Usu.pes_id = Pes.pes_id
			AND Pes.pes_situacao <> 3	
	WHERE
		(Usu.ent_id = ISNULL(@ent_id, Ugu.ent_id) )
		AND (Ugu.uad_id = ISNULL(@uad_id, Ugu.uad_id)) 	
		AND usu_situacao <> 3
	ORDER BY
		usu_login

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoEntidade_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoEntidade_UPDATE]
		@ten_id uniqueidentifier
		,@ten_nome VarChar (100)
		,@ten_situacao TinyInt
		,@ten_dataCriacao DateTime
		,@ten_dataAlteracao DateTime
		,@ten_integridade Int

AS
BEGIN
	UPDATE SYS_TipoEntidade 
	SET 
		ten_nome = @ten_nome
		,ten_situacao = @ten_situacao
		,ten_dataCriacao = @ten_dataCriacao
		,ten_dataAlteracao = @ten_dataAlteracao
		,ten_integridade = @ten_integridade
	WHERE 
		ten_id = @ten_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProviderDominioPermitido_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProviderDominioPermitido_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,LoginProvider
		,Dominios
		,Tenant

	FROM 
		SYS_LoginProviderDominioPermitido WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_GeraScriptCompletoMenuSistema]'
GO
-- =============================================
-- Author:		Haila Pelloso
-- Create date: 12/11/2012
-- Description:	Gera o script completo do menu de determinado sistema
-- =============================================
CREATE PROCEDURE [dbo].[MS_GeraScriptCompletoMenuSistema]
	@nomeSistema VARCHAR(MAX) 
	
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @tbDados TABLE(Dados VARCHAR(MAX) NOT NULL)
	
	DECLARE 
		@sis_id INT = 0

	SET @sis_id = (SELECT TOP 1 sis_id FROM SYS_Sistema AS SIS WITH(NOLOCK) 
					WHERE SIS.sis_situacao <> 3 AND SIS.sis_nome = @nomeSistema)

	IF (@sis_id IS NOT NULL)
	BEGIN

		--Dados do cabeçalho
		INSERT INTO @tbDados (Dados) VALUES ('BEGIN TRANSACTION' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES ('SET XACT_ABORT ON' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES (CHAR(13))
	
		INSERT INTO @tbDados (Dados) VALUES ('DECLARE @nomeSistema VARCHAR(MAX) = ''' + @nomeSistema + '''' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES ('DECLARE @nomeModuloAvo VARCHAR(MAX)' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES ('DECLARE @nomeModuloPai VARCHAR(MAX)' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES ('DECLARE @nomeModulo VARCHAR(MAX)' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES (CHAR(13))
	
		
		DECLARE @vis_idAdm INT = 0
		SET @vis_idAdm = (SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Administração')

		DECLARE @vis_idGestao INT = 0
		SET @vis_idGestao = (SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Gestão')

		DECLARE @vis_idUA INT = 0
		SET @vis_idUA = (SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Unidade Administrativa')

		DECLARE @vis_idIndividual INT = 0
		SET @vis_idIndividual = (SELECT vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = 'Individual')
	
		DECLARE 
			@ModId INT,
			@NomeModulo VARCHAR(MAX),
			@ModIdPai INT,
			@NomeModuloPai VARCHAR(MAX),
			@ModIdAvo INT,
			@NomeModuloAvo VARCHAR(MAX),
			@DescricaoModulo VARCHAR(MAX)

		-- Cursor para percorrer os módulos
		DECLARE cursorModulos CURSOR FOR
		SELECT 			
			Modulo.mod_id AS ModId,
			Modulo.mod_nome AS NomeModulo, 
			Modulo.mod_idPai AS ModIdPai,
			ModPai.mod_nome AS NomeModuloPai,
			ModPai.mod_idPai AS ModIdAvo,
			ModAvo.mod_nome AS NomeModuloAvo,
			CONVERT(NVARCHAR(MAX),Modulo.mod_descricao) AS DescricaoModulo
		FROM 
			SYS_Modulo Modulo WITH(NOLOCK)
			INNER JOIN SYS_VisaoModulo AS VM WITH(NOLOCK)
				ON Modulo.sis_id = VM.sis_id
				AND Modulo.mod_id = VM.mod_id
			INNER JOIN SYS_VisaoModuloMenu AS VMM WITH(NOLOCK)
				ON VM.vis_id = VMM.vis_id 
				AND VM.sis_id = VMM.sis_id 
				AND VM.mod_id = VMM.mod_id	
			LEFT JOIN SYS_Modulo ModPai WITH(NOLOCK)
				ON Modulo.sis_id = ModPai.sis_id
				AND Modulo.mod_idPai = ModPai.mod_id
				AND ModPai.mod_situacao <> 3
			LEFT JOIN SYS_Modulo ModAvo WITH(NOLOCK)
				ON ModPai.sis_id = ModAvo.sis_id
				AND ModPai.mod_idPai = ModAvo.mod_id
				AND ModAvo.mod_situacao <> 3
		WHERE 
			Modulo.sis_id = @sis_id
			AND Modulo.mod_situacao <> 3
			
		GROUP BY
			Modulo.mod_id,
			Modulo.mod_nome, 
			Modulo.mod_idPai,
			ModPai.mod_nome,
			ModPai.mod_idPai,
			ModAvo.mod_nome,
			CONVERT(NVARCHAR(MAX),Modulo.mod_descricao)
		ORDER BY
			ModIdPai, MAX(VMM.vmm_ordem)

		-- Abrindo cursor para leitura
		OPEN cursorModulos

		-- Lendo a próxima linha
		FETCH NEXT FROM cursorModulos INTO @ModId, @NomeModulo, @ModIdPai, @NomeModuloPai, @ModIdAvo, @NomeModuloAvo, @DescricaoModulo

		-- Percorrendo linhas do cursor (enquanto houverem)
		WHILE @@FETCH_STATUS = 0
		BEGIN
		
			DECLARE @SiteMap1Nome VARCHAR(MAX) = NULL
			DECLARE @SiteMap2Nome VARCHAR(MAX) = NULL
			DECLARE @SiteMap3Nome VARCHAR(MAX) = NULL

			DECLARE @SiteMap1Url VARCHAR(MAX) = NULL
			DECLARE @SiteMap2Url VARCHAR(MAX) = NULL
			DECLARE @SiteMap3Url VARCHAR(MAX) = NULL
			
			DECLARE @SiteMap1UrlHelp VARCHAR(MAX) = NULL
			DECLARE @SiteMap2UrlHelp VARCHAR(MAX) = NULL
			DECLARE @SiteMap3UrlHelp VARCHAR(MAX) = NULL
			
			DECLARE @SiteMap1Descricao VARCHAR(MAX) = NULL
			DECLARE @SiteMap2Descricao VARCHAR(MAX) = NULL
			DECLARE @SiteMap3Descricao VARCHAR(MAX) = NULL
					
			DECLARE @possuiVisaoAdm BIT = 0
			DECLARE @possuiVisaoGestao BIT = 0
			DECLARE @possuiVisaoUA BIT = 0
			DECLARE @possuiVisaoIndividual BIT = 0

			IF (EXISTS (SELECT vis_id, sis_id, mod_id FROM  SYS_VisaoModulo AS Visao WITH(NOLOCK) 
						WHERE Visao.sis_id = @sis_id AND Visao.mod_id = @modId AND vis_id = @vis_idAdm))
			BEGIN
				SET @possuiVisaoAdm = 1
			END			

			IF (EXISTS (SELECT vis_id, sis_id, mod_id FROM  SYS_VisaoModulo AS Visao WITH(NOLOCK) 
						WHERE Visao.sis_id = @sis_id AND Visao.mod_id = @modId AND vis_id = @vis_idGestao))
			BEGIN
				SET @possuiVisaoGestao = 1
			END			

			IF (EXISTS (SELECT vis_id, sis_id, mod_id FROM  SYS_VisaoModulo AS Visao WITH(NOLOCK) 
						WHERE Visao.sis_id = @sis_id AND Visao.mod_id = @modId AND vis_id = @vis_idUA))
			BEGIN
				SET @possuiVisaoUA = 1
			END			

			IF (EXISTS (SELECT vis_id, sis_id, mod_id FROM  SYS_VisaoModulo AS Visao WITH(NOLOCK) 
						WHERE Visao.sis_id = @sis_id AND Visao.mod_id = @modId AND vis_id = @vis_idIndividual))
			BEGIN
				SET @possuiVisaoIndividual = 1
			END
								
			DECLARE @tbSiteMap AS TABLE (
				numLinha INT NOT NULL,
				msm_nome VARCHAR(MAX) NOT NULL, 
				msm_url VARCHAR(MAX) NULL, 
				msm_urlHelp VARCHAR(MAX) NULL, 
				msm_descricao VARCHAR(MAX) NULL
			)

			DELETE FROM @tbSiteMap
			
			INSERT INTO @tbSiteMap (numLinha, msm_nome, msm_url, msm_urlHelp, msm_descricao)
				SELECT 
					ROW_NUMBER() OVER(ORDER BY CASE WHEN VMM.vmm_ordem IS NULL THEN 0 ELSE 1 END DESC) AS numLinha
					, msm_nome
					, CASE WHEN (msm_url LIKE '~/Index.aspx?mod_id=%') THEN NULL ELSE REPLACE(msm_url, '''', '''''') END
					, msm_urlHelp
					, msm_descricao 				
				FROM 
					SYS_ModuloSiteMap AS MSM WITH(NOLOCK)
					LEFT JOIN SYS_VisaoModuloMenu AS VMM WITH(NOLOCK)
						ON MSM.sis_id = VMM.sis_id 
						AND MSM.mod_id = VMM.mod_id 
						AND MSM.msm_id = VMM.msm_id
						AND VMM.vis_id = @vis_idAdm
				WHERE 
					MSM.sis_id = @sis_id
					AND MSM.mod_id = @ModId
			
			--Dados do siteMap 1
			SELECT 
				@SiteMap1Nome = msm_nome,
				@SiteMap1Url = msm_url,
				@SiteMap1UrlHelp = msm_urlHelp,
				@SiteMap1Descricao = msm_descricao
			FROM 
				@tbSiteMap SM 
			WHERE 
				SM.numLinha = 1
			
			--Dados do siteMap 2	
			SELECT 
				@SiteMap2Nome = msm_nome,
				@SiteMap2Url = msm_url,
				@SiteMap2UrlHelp = msm_urlHelp,
				@SiteMap2Descricao = msm_descricao
			FROM 
				@tbSiteMap SM 
			WHERE 
				SM.numLinha = 2
			
			--Dados do siteMap 3
			SELECT 
				@SiteMap3Nome = msm_nome,
				@SiteMap3Url = msm_url,
				@SiteMap3UrlHelp = msm_urlHelp,
				@SiteMap3Descricao = msm_descricao
			FROM 
				@tbSiteMap SM 
			WHERE 
				SM.numLinha = 3
					

			INSERT INTO @tbDados (Dados) VALUES (
						CASE WHEN (@nomeModuloAvo IS NULL)
							 THEN 'SET @nomeModuloAvo = NULL'
							 ELSE 'SET @nomeModuloAvo = ''' + @nomeModuloAvo + '''' END)

			INSERT INTO @tbDados (Dados) VALUES (
						CASE WHEN (@nomeModuloPai IS NULL)
							 THEN 'SET @nomeModuloPai = NULL'
							 ELSE 'SET @nomeModuloPai = ''' + @nomeModuloPai + '''' END)

			INSERT INTO @tbDados (Dados) VALUES (
						CASE WHEN (@nomeModulo IS NULL)
							 THEN 'SET @nomeModulo = NULL'
							 ELSE 'SET @nomeModulo = ''' + @nomeModulo + '''' END)
			
			
			INSERT INTO @tbDados (Dados) VALUES (CHAR(13))
			
			INSERT INTO @tbDados (Dados) VALUES ('EXEC MS_InserePaginaMenu' + CHAR(13))
			INSERT INTO @tbDados (Dados) VALUES (		'		@nomeSistema = @nomeSistema, ' + CHAR(13))
			INSERT INTO @tbDados (Dados) VALUES (		'		@nomeModuloPai = @nomeModuloPai, ' + CHAR(13))
			INSERT INTO @tbDados (Dados) VALUES (		'		@nomeModulo = @nomeModulo, ' + CHAR(13))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap1Nome = ''' + @SiteMap1Nome + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap1Url = ''' + @SiteMap1Url + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap2Nome = ''' + @SiteMap2Nome + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap2Url = ''' + @SiteMap2Url + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap3Nome = ''' + @SiteMap3Nome + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap3Url = ''' + @SiteMap3Url + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@possuiVisaoAdm = ' + CAST(@possuiVisaoAdm AS VARCHAR) + ', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@possuiVisaoGestao = ' + CAST(@possuiVisaoGestao AS VARCHAR) + ', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@possuiVisaoUA = ' + CAST(@possuiVisaoUA AS VARCHAR) + ', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@possuiVisaoIndividual = ' + CAST(@possuiVisaoIndividual AS VARCHAR) + ', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap1UrlHelp = ''' + @SiteMap1UrlHelp + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap2UrlHelp = ''' + @SiteMap2UrlHelp + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap3UrlHelp = ''' + @SiteMap3UrlHelp + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap1Descricao = ''' + @SiteMap1Descricao + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap2Descricao = ''' + @SiteMap2Descricao + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@SiteMap3Descricao = ''' + @SiteMap3Descricao + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@DescricaoModulo = ''' + @DescricaoModulo + ''', ' + CHAR(13), ''))
			INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@nomeModuloAvo = @nomeModuloAvo' + CHAR(13), ''))
			
			
			IF ((SELECT COUNT(*) FROM @tbSiteMap) > 3)
			BEGIN
				
				DECLARE
					@SiteMapNome VARCHAR(MAX) = NULL
					, @SiteMapUrl VARCHAR(MAX) = NULL
					, @SiteMapUrlHelp VARCHAR(MAX) = NULL
					, @SiteMapDescricao  VARCHAR(MAX) = NULL

				-- Cursor para percorrer os nomes dos siteMaps 
				DECLARE cursorSiteMaps CURSOR FOR
					SELECT 
						msm_nome
						, msm_url
						, msm_urlHelp
						, msm_descricao 				
					FROM 
						@tbSiteMap SM
					WHERE
						numLinha > 3					

				-- Abrindo cursor para leitura
				OPEN cursorSiteMaps

				-- Lendo a próxima linha
				FETCH NEXT FROM cursorSiteMaps INTO @SiteMapNome, @SiteMapUrl, @SiteMapUrlHelp, @SiteMapDescricao

				-- Percorrendo linhas do cursor (enquanto houverem)
				WHILE @@FETCH_STATUS = 0
				BEGIN

					INSERT INTO @tbDados (Dados) VALUES (CHAR(13))
					
					INSERT INTO @tbDados (Dados) VALUES ('EXEC MS_InsereSiteMap' + CHAR(13))
					INSERT INTO @tbDados (Dados) VALUES (		'		@nomeSistema = @nomeSistema ' + CHAR(13))
					INSERT INTO @tbDados (Dados) VALUES (		'		, @nomeModulo = @nomeModulo ' + CHAR(13))
					INSERT INTO @tbDados (Dados) VALUES (		'		, @nomeModuloPai = @nomeModuloPai' + CHAR(13))
					INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @nomeModuloAvo = @nomeModuloAvo' + CHAR(13), ''))
					INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @SiteMapNome = ''' + @SiteMapNome + '''' + CHAR(13), ''))
					INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @SiteMapUrl = ''' + @SiteMapUrl + '''' + CHAR(13), ''))
					INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @SiteMapUrlHelp = ''' + @SiteMapUrlHelp + '''' + CHAR(13), ''))
					INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @SiteMapDescricao = ''' + @SiteMapDescricao + '''' + CHAR(13), ''))

					-- Lendo a próxima linha
					FETCH NEXT FROM cursorSiteMaps INTO @SiteMapNome, @SiteMapUrl, @SiteMapUrlHelp, @SiteMapDescricao
				END

				-- Fechando cursor para leitura
				CLOSE cursorSiteMaps

				-- Desalocando o cursor
				DEALLOCATE cursorSiteMaps 				
				
			END
			
			INSERT INTO @tbDados (Dados) VALUES (CHAR(13))

			-- Lendo a próxima linha
			FETCH NEXT FROM cursorModulos INTO @ModId, @NomeModulo, @ModIdPai, @NomeModuloPai, @ModIdAvo, @NomeModuloAvo, @DescricaoModulo
		END

		-- Fechando cursor para leitura
		CLOSE cursorModulos

		-- Desalocando o cursor
		DEALLOCATE cursorModulos 
		
		INSERT INTO @tbDados (Dados) VALUES ('SET XACT_ABORT OFF')
		INSERT INTO @tbDados (Dados) VALUES ('COMMIT TRANSACTION')
		
	END
	ELSE
	BEGIN
		INSERT INTO @tbDados (Dados) VALUES (' O sistema não foi encontrado.')
	END
	
	SELECT 
		Dados 
	FROM 
		@tbDados 
	WHERE 
		Dados <> ''

END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDocumento_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaDocumento_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,tdo_id
		,psd_numero
		,psd_dataEmissao
		,psd_orgaoEmissao
		,unf_idEmissao
		,psd_infoComplementares
		,psd_situacao
		,psd_dataCriacao
		,psd_dataAlteracao
		,psd_categoria
		,psd_classificacao
		,psd_csm
		,psd_dataEntrada
		,psd_dataValidade
		,pai_idOrigem
		,psd_serie
		,psd_tipoGuarda
		,psd_via
		,psd_secao
		,psd_zona
		,psd_regiaoMilitar
		,psd_numeroRA
		,psd_dataExpedicao
	FROM 
		PES_PessoaDocumento WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_SelecionarUsuariosPorIdGrupo]'
GO

CREATE PROCEDURE [dbo].[MS_SelecionarUsuariosPorIdGrupo]
	@idGrupo uniqueidentifier
AS
BEGIN
	
	SELECT 
		--  u.usu_id
		--, u.pes_id
		--, u.usu_dominio
		--, u.usu_login	
		--, u.usu_email
		--, u.usu_integracaoAD
		--, u.usu_dataCriacao
		--, u.usu_dataAlteracao

		  u.usu_id
		, u.usu_login
		, u.usu_dominio
		, u.usu_email
		, u.usu_senha
		, u.usu_criptografia
		, u.usu_situacao
		, u.usu_dataCriacao
		, u.usu_dataAlteracao
		, u.pes_id
		, u.usu_integridade
		, u.ent_id
		, u.usu_integracaoAD
		, u.usu_integracaoExterna

	FROM 
		SYS_Usuario					u  WITH (NOLOCK)
		INNER JOIN SYS_UsuarioGrupo ug WITH (NOLOCK) ON ( ug.usu_id = u.usu_id )

	WHERE
			u. usu_situacao <> 3		
		AND ug.usg_situacao <> 3
		AND ug.gru_id = @idGrupo

	ORDER BY
		u.usu_login
		
	SELECT @@ROWCOUNT
END


--EXEC msdb..spGrantExectoAllRoutines N'user_coresso', N'CoreSSO_DEV'
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoEntidade_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoEntidade_SELECT]
	
AS
BEGIN
	SELECT 
		ten_id
		,ten_nome
		,ten_situacao
		,ten_dataCriacao
		,ten_dataAlteracao
		,ten_integridade
		
	FROM 
		SYS_TipoEntidade WITH(NOLOCK) 
	
	select @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_GeraScriptCompletoParametro]'
GO
-- =============================================
-- Author:		Haila Pelloso
-- Create date: 12/11/2012
-- Description:	Gera o script completo dos parâmetros.
-- =============================================
CREATE PROCEDURE [dbo].[MS_GeraScriptCompletoParametro]

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SEggLECT statements.
	SET NOCOUNT ON;
	
	DECLARE @tbDados TABLE(Dados VARCHAR(MAX) NOT NULL)
	
		--Dados do cabeçalho
	INSERT INTO @tbDados (Dados) VALUES ('BEGIN TRANSACTION' + CHAR(13))
	INSERT INTO @tbDados (Dados) VALUES ('SET XACT_ABORT ON' + CHAR(13))
	INSERT INTO @tbDados (Dados) VALUES (CHAR(13))
	
	
	DECLARE 
		@par_chave VARCHAR(MAX), 
		@par_valor VARCHAR(MAX), 
		@par_descricao VARCHAR(MAX), 
		@par_obrigatorio BIT

	-- Cursor para percorrer os nomes dos parâmetros 
	DECLARE cursorParametros CURSOR FOR
		SELECT 
			par_chave,
			par_valor,
			par_descricao,
			par_obrigatorio
		FROM 
			SYS_Parametro AS PAR WITH(NOLOCK)
		WHERE 
			PAR.par_situacao <> 3

	-- Abrindo Cursor para leitura
	OPEN cursorParametros

	-- Lendo a próxima linha
	FETCH NEXT FROM cursorParametros INTO @par_chave, @par_valor, @par_descricao, @par_obrigatorio

	-- Percorrendo linhas do cursor (enquanto houverem)
	WHILE @@FETCH_STATUS = 0
	BEGIN

		DECLARE @valor VARCHAR(MAX) = ''
		IF (@par_valor = 'True' OR @par_valor = 'False')
		BEGIN
			SET @valor = @par_valor
		END

		INSERT INTO @tbDados (Dados) VALUES ('EXEC MS_InsereParametro' + CHAR(13))
		INSERT INTO @tbDados (Dados) VALUES (ISNULL('		@par_chave = ''' + @par_chave + '''' + CHAR(13), ''))
		INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @par_valor = ''' + @valor + '''' + CHAR(13), ''))
		INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @par_descricao = ''' + @par_descricao + '''' + CHAR(13), ''))
		INSERT INTO @tbDados (Dados) VALUES (ISNULL('		, @par_obrigatorio = ''' + CAST(@par_obrigatorio AS VARCHAR) + '''' + CHAR(13), ''))		

		INSERT INTO @tbDados (Dados) VALUES (CHAR(13))

		-- Lendo a próxima linha
		FETCH NEXT FROM cursorParametros INTO @par_chave, @par_valor, @par_descricao, @par_obrigatorio
	END

	-- Fechando cursor para leitura
	CLOSE cursorParametros

	-- Desalocando o cursor
	DEALLOCATE cursorParametros 
	
	INSERT INTO @tbDados (Dados) VALUES ('SET XACT_ABORT OFF')
	INSERT INTO @tbDados (Dados) VALUES ('COMMIT TRANSACTION')	
	
	
	SELECT 
		Dados 
	FROM 
		@tbDados 
	WHERE 
		Dados <> ''		

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDocumento_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaDocumento_LOAD]
		@pes_id uniqueidentifier
		,@tdo_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		pes_id
		,tdo_id
		,psd_numero
		,psd_dataEmissao
		,psd_orgaoEmissao
		,unf_idEmissao
		,psd_infoComplementares
		,psd_situacao
		,psd_dataCriacao
		,psd_dataAlteracao
		,psd_categoria 
		,psd_classificacao 
		,psd_csm 
		,psd_dataEntrada 
		,psd_dataValidade 
		,pai_idOrigem 
		,psd_serie 
		,psd_tipoGuarda 
		,psd_via 
		,psd_secao 
		,psd_zona 
		,psd_regiaoMilitar 
		,psd_numeroRA
		,psd_dataExpedicao 
 	FROM
 		PES_PessoaDocumento
	WHERE 
		pes_id = @pes_id
	and tdo_id = @tdo_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativa_SelecionarPorId]'
GO

CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativa_SelecionarPorId]
	  @entidadeId				UNIQUEIDENTIFIER
	, @unidadeAdministrativaId	UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		  ua.ent_id
		, ua.uad_id
		, ua.tua_id
		, tua.tua_nome
		, ua.uad_codigo
		, ua.uad_nome
		, ua.uad_sigla
		, ua.uad_idSuperior
		, ua.uad_situacao
		, ua.uad_dataCriacao
		, ua.uad_dataAlteracao
		, ua.uad_integridade

	FROM
		SYS_UnidadeAdministrativa AS ua WITH (NOLOCK)
		INNER JOIN SYS_TipoUnidadeAdministrativa AS tua WITH (NOLOCK) ON ( ua.tua_id = tua.tua_id )

	WHERE						 
			ua.ent_id = ISNULL(@entidadeId, ua.ent_id)
		AND ua.uad_id = ISNULL(@unidadeAdministrativaId, ua.uad_id) 	
		AND ua.uad_situacao	<> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoEntidade_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoEntidade_LOAD]
	@ten_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		ten_id
		,ten_nome
		,ten_situacao
		,ten_dataCriacao
		,ten_dataAlteracao
		,ten_integridade
 	FROM
 		SYS_TipoEntidade
	WHERE 
		ten_id = @ten_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_tua_id]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 27/05/2010 11:32
-- Description:	filtra unidades administrativas por uad_idSuperior
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_tua_id]	
	@tua_id UNIQUEIDENTIFIER	
	, @ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 	
		tua_id
		, uad_id
		, uad_idSuperior
		, uad_nome
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)		
	WHERE
		uad_situacao <> 3
		AND(@tua_id IS NULL OR tua_id = @tua_id)
		AND (@ent_id IS NULL OR ent_id = @ent_id)
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDocumento_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaDocumento_INSERT]
	  @pes_id uniqueidentifier
	, @tdo_id uniqueidentifier
	, @psd_numero VarChar (50)
	, @psd_dataEmissao Date
	, @psd_orgaoEmissao VarChar (200)
	, @unf_idEmissao uniqueidentifier
	, @psd_infoComplementares VarChar (1000)
	, @psd_situacao TinyInt
	, @psd_dataCriacao DateTime
	, @psd_dataAlteracao DateTime
	, @psd_categoria VarChar (64) = NULL
	, @psd_classificacao VarChar (64) = NULL
	, @psd_csm VarChar (32) = NULL
	, @psd_dataEntrada DateTime = NULL
	, @psd_dataValidade DateTime = NULL
	, @pai_idOrigem UniqueIdentifier = NULL
	, @psd_serie VarChar (32) = NULL
	, @psd_tipoGuarda VarChar (128) = NULL
	, @psd_via VarChar (16) = NULL
	, @psd_secao VarChar (32) = NULL
	, @psd_zona VarChar (16) = NULL
	, @psd_regiaoMilitar VarChar (16) = NULL
	, @psd_numeroRA VarChar (64) = NULL
	, @psd_dataExpedicao Date = NULL

AS
BEGIN
	INSERT INTO 
		PES_PessoaDocumento
		( 
			pes_id
			,tdo_id
			,psd_numero
			,psd_dataEmissao
			,psd_orgaoEmissao
			,unf_idEmissao
			,psd_infoComplementares
			,psd_situacao
			,psd_dataCriacao
			,psd_dataAlteracao
			,psd_categoria 
			,psd_classificacao 
			,psd_csm 
			,psd_dataEntrada 
			,psd_dataValidade 
			,pai_idOrigem 
			,psd_serie 
			,psd_tipoGuarda 
			,psd_via 
			,psd_secao 
			,psd_zona 
			,psd_regiaoMilitar 
			,psd_numeroRA
			,psd_dataExpedicao 
		)
	VALUES
		( 
			@pes_id
			,@tdo_id
			,@psd_numero
			,@psd_dataEmissao
			,@psd_orgaoEmissao
			,@unf_idEmissao
			,@psd_infoComplementares
			,@psd_situacao
			,@psd_dataCriacao
			,@psd_dataAlteracao
			,@psd_categoria 
			,@psd_classificacao 
			,@psd_csm 
			,@psd_dataEntrada 
			,@psd_dataValidade 
			,@pai_idOrigem 
			,@psd_serie 
			,@psd_tipoGuarda 
			,@psd_via 
			,@psd_secao 
			,@psd_zona 
			,@psd_regiaoMilitar 
			,@psd_numeroRA
			,@psd_dataExpedicao 
		)
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativa_SelecionarTodas]'
GO

CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativa_SelecionarTodas]
	  @entidadeId UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		  ua.ent_id
		, ua.uad_id
		, ua.tua_id
		, tua.tua_nome
		, ua.uad_codigo
		, ua.uad_nome
		, ua.uad_sigla
		, ua.uad_idSuperior
		, ua.uad_situacao
		, ua.uad_dataCriacao
		, ua.uad_dataAlteracao
		, ua.uad_integridade

	FROM
		SYS_UnidadeAdministrativa AS ua WITH (NOLOCK)
		INNER JOIN SYS_TipoUnidadeAdministrativa AS tua WITH (NOLOCK) ON ( ua.tua_id = tua.tua_id )

	WHERE						 
			ua.ent_id = @entidadeId
		AND ua.uad_situacao	<> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoEntidade_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoEntidade_INSERT]
		@ten_nome VarChar (100)
		,@ten_situacao TinyInt
		,@ten_dataCriacao DateTime
		,@ten_dataAlteracao DateTime
		,@ten_integridade INT		

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_TipoEntidade
		( 
			ten_nome
			,ten_situacao
			,ten_dataCriacao
			,ten_dataAlteracao
			,ten_integridade			
		)
	OUTPUT inserted.ten_id INTO @ID
	VALUES
		( 
			@ten_nome
			,@ten_situacao
			,@ten_dataCriacao
			,@ten_dataAlteracao
			,@ten_integridade
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Codigo]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 05/04/2011 10:20
-- Description:	Utilizado para verificar se existe determinada 
--				unidade administrativa
--				filtrando por: Entidade e código
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Codigo]
	@ent_id UNIQUEIDENTIFIER	
	, @uad_codigo VARCHAR(20)	
	
AS
BEGIN
	SELECT
		ent_id
		, uad_id
		, tua_id
		, uad_codigo
		, uad_nome
		, uad_sigla
		, uad_idSuperior
		, uad_situacao
		, uad_dataCriacao
		, uad_dataAlteracao
		, uad_integridade
		, uad_codigoIntegracao
		,uad_codigoInep
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)		
	WHERE
		uad_situacao <> 3
		AND ent_id = @ent_id
		AND UPPER(uad_codigo) = UPPER(@uad_codigo)
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_LoginProvider_Config]'
GO
CREATE TABLE [dbo].[SYS_LoginProvider_Config]
(
[ent_id] [uniqueidentifier] NOT NULL,
[LoginProvider] [varchar] (128) NOT NULL,
[ClientId] [varchar] (1024) NOT NULL,
[ClientSecret] [nvarchar] (512) NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_LoginProvider_Config] on [dbo].[SYS_LoginProvider_Config]'
GO
ALTER TABLE [dbo].[SYS_LoginProvider_Config] ADD CONSTRAINT [PK_SYS_LoginProvider_Config] PRIMARY KEY CLUSTERED  ([ent_id], [LoginProvider]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProvider_Config_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProvider_Config_LOAD]
	@ent_id UniqueIdentifier
	, @LoginProvider VarChar (128)
	
AS
BEGIN
	SELECT	Top 1
		 ent_id  
		, LoginProvider 
		, ClientId 
		, ClientSecret 

 	FROM
 		SYS_LoginProvider_Config
	WHERE 
		ent_id = @ent_id
		AND LoginProvider = @LoginProvider
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDocumento_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaDocumento_DELETE]
		@pes_id uniqueidentifier
		,@tdo_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		PES_PessoaDocumento
	WHERE 
		pes_id = @pes_id
	and tdo_id = @tdo_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativa_SelecionarUnidadesFilhas]'
GO

CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativa_SelecionarUnidadesFilhas]
	  @entidadeId				UNIQUEIDENTIFIER
	, @unidadeAdministrativaId	UNIQUEIDENTIFIER
AS
BEGIN

	SELECT
		  ua.ent_id
		, ua.uad_id
		, ua.tua_id
		, tua.tua_nome
		, ua.uad_codigo
		, ua.uad_nome
		, ua.uad_sigla
		, ua.uad_idSuperior
		, ua.uad_situacao
		, ua.uad_dataCriacao
		, ua.uad_dataAlteracao
		, ua.uad_integridade

	FROM
		SYS_UnidadeAdministrativa AS ua WITH (NOLOCK)
		INNER JOIN SYS_TipoUnidadeAdministrativa AS tua WITH (NOLOCK) ON ( ua.tua_id = tua.tua_id )

	WHERE						 
			ua.ent_id = ISNULL(@entidadeId, ua.ent_id)
		AND ua.uad_idSuperior = ISNULL(@unidadeAdministrativaId, ua.uad_idSuperior) 	
		AND ua.uad_situacao	<> 3

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_UnidadeFederativa_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_END_UnidadeFederativa_UPDATE]
		@unf_id uniqueidentifier
		,@pai_id uniqueidentifier
		,@unf_nome VarChar (100)
		,@unf_sigla VarChar (2)
		,@unf_situacao TinyInt
		,@unf_integridade Int

AS
BEGIN
	UPDATE END_UnidadeFederativa 
	SET 
		pai_id = @pai_id
		,unf_nome = @unf_nome
		,unf_sigla = @unf_sigla
		,unf_situacao = @unf_situacao
		,unf_integridade = @unf_integridade
	WHERE 
		unf_id = @unf_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoEntidade_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoEntidade_DELETE]
	@ten_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_TipoEntidade
	WHERE 
		ten_id = @ten_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProvider_Config_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProvider_Config_INSERT]
	@ent_id UniqueIdentifier
	, @LoginProvider VarChar (128)
	, @ClientId VarChar (1024)
	, @ClientSecret VarChar (1024)

AS
BEGIN
	INSERT INTO 
		SYS_LoginProvider_Config
		( 
			ent_id 
			, LoginProvider 
			, ClientId 
			, ClientSecret 
 
		)
	VALUES
		( 
			@ent_id 
			, @LoginProvider 
			, @ClientId 
			, @ClientSecret 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativaSelecionarPorIdGrupo]'
GO
-- ========================================================================
-- Description:	 Seleciona as unidades administrativas por grupo		 
-- ========================================================================
CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativaSelecionarPorIdGrupo]	
	@idEntidade uniqueidentifier
	,@idGrupo uniqueidentifier
AS
BEGIN
	SELECT
		UA.ent_id
		, UA.uad_id
		, UA.tua_id
		, UA.uad_codigo
		, UA.uad_nome
		, UA.uad_sigla
		, UA.uad_idSuperior
		, UA.uad_situacao
		, UA.uad_dataCriacao
		, UA.uad_dataAlteracao
		, UA.uad_integridade
		, UA.uad_codigoIntegracao
		, TIPO.tua_nome
	FROM
		[SYS_UnidadeAdministrativa] AS UA WITH (NOLOCK)
	INNER JOIN [SYS_TipoUnidadeAdministrativa] AS TIPO WITH (NOLOCK)
		ON TIPO.tua_id = UA.tua_id		
	WHERE
		UA.uad_situacao <> 3
		AND TIPO.tua_situacao <> 3		
		AND (@idEntidade is null or  UA.ent_id = @idEntidade)

		AND EXISTS (		
				SELECT 
					ent_id,uad_id 
				FROM 
					SYS_UsuarioGrupoUA WITH (NOLOCK)
				WHERE 
					SYS_UsuarioGrupoUA.ent_id = UA.ent_id
				AND SYS_UsuarioGrupoUA.uad_id = UA.uad_id
				AND SYS_UsuarioGrupoUA.gru_id = @idGrupo
		)
		
	ORDER BY
		UA.uad_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_UnidadeFederativa_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_END_UnidadeFederativa_SELECT]
	
AS
BEGIN
	SELECT 
		unf_id
		,pai_id
		,unf_nome
		,unf_sigla
		,unf_situacao
		,unf_integridade
		
	FROM 
		END_UnidadeFederativa WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacao_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacao_UPDATE]
	  @tdo_id UNIQUEIDENTIFIER
	, @tdo_nome VARCHAR (100)
	, @tdo_sigla VARCHAR (10)
	, @tdo_validacao TINYINT
	, @tdo_situacao TINYINT
	, @tdo_dataCriacao DATETIME
	, @tdo_dataAlteracao DATETIME
	, @tdo_integridade INT
	, @tdo_classificacao	TINYINT = NULL
	, @tdo_atributos		VARCHAR(1024) = NULL

AS
BEGIN
	DECLARE 
		  @tdo_classificacaoTmp TINYINT = 99
		, @tdo_atributosTmp		VARCHAR(1024)
		, @stringDefault		VARCHAR(512)  = NULL 
		, @objetosDefault		VARCHAR(1024) = ''

	-- Valida se o parâmetro @tdo_classificacao é nulo, para colocar o valor default do campo
	IF (@tdo_classificacao IS NULL) BEGIN
		
		SET @tdo_classificacaoTmp = (	SELECT	ISNULL(td.tdo_classificacao, 99) 
										FROM	SYS_TipoDocumentacao td WITH (NOLOCK) 
										WHERE	td.tdo_id = @tdo_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @tdo_classificacaoTmp = @tdo_classificacao
	END 



	-- Valida se o parâmetro @tdo_atributos é nulo
	IF (@tdo_atributos IS NULL) BEGIN
		
		-- Executa as procedures para selecionar as informações sobre os atributos default
		EXEC NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault 
			  @ExibirRetorno      = 0
	 		, @AtributosDefault   = @stringDefault OUTPUT
			, @NomeObjetosDefault = @objetosDefault OUTPUT

		-- 
		SET @tdo_atributosTmp = (	SELECT	ISNULL(td.tdo_atributos, @stringDefault)
									FROM	SYS_TipoDocumentacao td WITH (NOLOCK) 
									WHERE	td.tdo_id = @tdo_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @tdo_atributosTmp = @tdo_atributos
	END 
	
	-- Atualiza o registro do tipo de documentação
	UPDATE 
		SYS_TipoDocumentacao 
	SET 
		  tdo_nome			= @tdo_nome 
		, tdo_sigla			= @tdo_sigla 
		, tdo_validacao		= @tdo_validacao 
		, tdo_situacao		= @tdo_situacao 
		, tdo_dataCriacao	= @tdo_dataCriacao 
		, tdo_dataAlteracao = @tdo_dataAlteracao 
		, tdo_integridade	= @tdo_integridade 
		, tdo_classificacao = @tdo_classificacaoTmp 
		, tdo_atributos		= @tdo_atributosTmp 
	WHERE 
		tdo_id = @tdo_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProvider_Config_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProvider_Config_UPDATE]
	@ent_id UNIQUEIDENTIFIER
	, @LoginProvider VARCHAR (128)
	, @ClientId VARCHAR (1024)
	, @ClientSecret NVARCHAR (1024)

AS
BEGIN
	UPDATE SYS_LoginProvider_Config 
	SET 
		ClientId = @ClientId 
		, ClientSecret = @ClientSecret 

	WHERE 
		ent_id = @ent_id 
		AND LoginProvider = @LoginProvider 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_id_uae_id]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 31/05/2010 14:23
-- Description:	utilizado para verificar o codigo do primeiro registro
--              cadastrado para a entidade e unidade administrativa
--				filtrados por:
--					entidade, unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_SelectBy_ent_id_uad_id_uae_id]	
	@ent_id uniqueidentifier
	,@uad_id uniqueidentifier
	,@uae_id uniqueidentifier
AS
BEGIN
	SELECT 	
		TOP 1 uae_id
	FROM
		SYS_UnidadeAdministrativaEndereco WITH (NOLOCK)		
	WHERE
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_id = @uae_id
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_UnidadeFederativa_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_END_UnidadeFederativa_LOAD]
	@unf_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		unf_id
		,pai_id
		,unf_nome
		,unf_sigla
		,unf_situacao
		,unf_integridade

 	FROM
 		END_UnidadeFederativa
	WHERE 
		unf_id = @unf_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacao_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacao_SELECT]
	
AS
BEGIN
	SELECT 
		  tdo_id
		, tdo_nome
		, tdo_sigla
		, tdo_validacao
		, tdo_situacao
		, tdo_dataCriacao
		, tdo_dataAlteracao
		, tdo_integridade
		, tdo_classificacao
		, tdo_atributos
	FROM 
		SYS_TipoDocumentacao WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProvider_Config_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_SYS_LoginProvider_Config_DELETE]
	@ent_id UNIQUEIDENTIFIER
	, @LoginProvider VARCHAR(128)

AS
BEGIN
	DELETE FROM 
		SYS_LoginProvider_Config 
	WHERE 
		ent_id = @ent_id 
		AND LoginProvider = @LoginProvider 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_UnidadeFederativa_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_END_UnidadeFederativa_INSERT]
		@pai_id uniqueidentifier
		,@unf_nome VarChar (100)
		,@unf_sigla VarChar (2)
		,@unf_situacao TinyInt
		,@unf_integridade Int

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		END_UnidadeFederativa
		( 
			pai_id
			,unf_nome
			,unf_sigla
			,unf_situacao
			,unf_integridade
		)
	OUTPUT inserted.unf_id INTO @ID
	VALUES
		( 
			@pai_id
			,@unf_nome
			,@unf_sigla
			,@unf_situacao
			,@unf_integridade
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacao_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacao_LOAD]
	@tdo_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	
		TOP 1
		  tdo_id  
		, tdo_nome 
		, tdo_sigla 
		, tdo_validacao 
		, tdo_situacao 
		, tdo_dataCriacao 
		, tdo_dataAlteracao 
		, tdo_integridade 
		, tdo_classificacao 
		, tdo_atributos
 	FROM
 		SYS_TipoDocumentacao
	WHERE 
		tdo_id = @tdo_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_LoginProvider_Config_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_LoginProvider_Config_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,LoginProvider
		,ClientId
		,ClientSecret

	FROM 
		SYS_LoginProvider_Config WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_EntidadeSelecionarTodasLigadasPorSistema]'
GO
-- ========================================================================
-- Description:	Retorna todas as entidades ligadas a sistema
-- ========================================================================
CREATE PROCEDURE [dbo].[MS_EntidadeSelecionarTodasLigadasPorSistema]	
AS
BEGIN
	SELECT	
		 SYS_Entidade.ent_id
		, SYS_Entidade.ten_id
		, SYS_Entidade.ent_codigo
		, SYS_Entidade.ent_nomeFantasia
		, SYS_Entidade.ent_razaoSocial
		, SYS_Entidade.ent_sigla
		, SYS_Entidade.ent_cnpj
		, SYS_Entidade.ent_inscricaoMunicipal
		, SYS_Entidade.ent_inscricaoEstadual
		, SYS_Entidade.ent_idSuperior
		, SYS_Entidade.ent_situacao
		, SYS_Entidade.ent_dataCriacao
		, SYS_Entidade.ent_dataAlteracao
		, SYS_Entidade.ent_integridade
		, SYS_Entidade.ent_urlAcesso
		, SYS_Entidade.ent_exibeLogoCliente
		, SYS_Entidade.ent_logoCliente
		, SYS_Entidade.tep_id
		, SYS_Entidade.tpl_id
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_Entidade.ten_id = SYS_TipoEntidade.ten_id		
	WHERE
		ent_situacao <> 3	
		AND ten_situacao <> 3	
		AND (ent_id in (SELECT ent_id FROM SYS_SistemaEntidade WITH (NOLOCK) WHERE sen_situacao <> 3))		
	ORDER BY
		ent_razaosocial
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_UnidadeFederativa_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_END_UnidadeFederativa_DELETE]
	@unf_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		END_UnidadeFederativa
	WHERE 
		unf_id = @unf_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacao_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacao_INSERT]
	  @tdo_nome VARCHAR (100)
	, @tdo_sigla VARCHAR (10)
	, @tdo_validacao TINYINT
	, @tdo_situacao TINYINT
	, @tdo_dataCriacao DATETIME
	, @tdo_dataAlteracao DATETIME
	, @tdo_integridade INT
	, @tdo_classificacao TINYINT = 99
	, @tdo_atributos	 VARCHAR(1024) = NULL

AS
BEGIN
	
	DECLARE @ID TABLE (ID UNIQUEIDENTIFIER)

	INSERT INTO SYS_TipoDocumentacao
		( 
			  tdo_nome 
			, tdo_sigla 
			, tdo_validacao 
			, tdo_situacao 
			, tdo_dataCriacao 
			, tdo_dataAlteracao 
			, tdo_integridade 
			, tdo_classificacao 
			, tdo_atributos
		)
	
	OUTPUT 
		INSERTED.tdo_id INTO @ID

	VALUES
		( 
			  @tdo_nome 
			, @tdo_sigla 
			, @tdo_validacao 
			, @tdo_situacao 
			, @tdo_dataCriacao 
			, @tdo_dataAlteracao 
			, @tdo_integridade 
			, @tdo_classificacao 
			, @tdo_atributos
		)
		
	SELECT ID FROM @ID

END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_LoginProviderConfig_Dominios_SelectBY_ent_id_LoginProvider]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_LoginProviderConfig_Dominios_SelectBY_ent_id_LoginProvider]
	@ent_id UniqueIdentifier
	, @LoginProvider VarChar (128)
AS
BEGIN
	SELECT
		 LP.ent_id  
		, LP.LoginProvider 
		, ClientId 
		, ClientSecret 
		, Dominios
		, Tenant
 	FROM
 		SYS_LoginProvider_Config as LP
		INNER JOIN SYS_LoginProviderDominioPermitido AS LPDP 
			ON (LP.ent_id = LPDP.ent_id AND LP.LoginProvider = LPDP.LoginProvider)
	WHERE
		LP.ent_id = @ent_id
		AND LP.LoginProvider like '%'+ @LoginProvider + '%'

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_AtualizaSenha]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 30/06/2014
-- Description:	Atualiza a senha do usuário.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_AtualizaSenha] 
	@uap_id INT,
	@uap_password VARCHAR(256)
AS
BEGIN
	UPDATE CFG_UsuarioAPI
	SET uap_password = @uap_password
	WHERE uap_id = @uap_id
	
	RETURN ISNULL(@@ROWCOUNT, -1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_TipoDocumentacao_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_TipoDocumentacao_DELETE] 
	@tdo_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_TipoDocumentacao
	WHERE 
		tdo_id = @tdo_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_Select_SYS_LoginProviderConfig_Dominios]'
GO

CREATE PROCEDURE [dbo].[NEW_Select_SYS_LoginProviderConfig_Dominios]

AS
BEGIN
	SELECT
		 LP.ent_id  
		, LP.LoginProvider 
		, ClientId 
		, ClientSecret 
		, Dominios
		, Tenant
 	FROM
 		SYS_LoginProvider_Config as LP
		INNER JOIN SYS_LoginProviderDominioPermitido AS LPDP 
			ON (LP.ent_id = LPDP.ent_id AND LP.LoginProvider = LPDP.LoginProvider)

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_PessoaContato]'
GO
CREATE TABLE [dbo].[PES_PessoaContato]
(
[pes_id] [uniqueidentifier] NOT NULL,
[psc_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PES_Pesso__psc_i__68487DD7] DEFAULT (newsequentialid()),
[tmc_id] [uniqueidentifier] NOT NULL,
[psc_contato] [varchar] (200) NOT NULL,
[psc_situacao] [tinyint] NOT NULL CONSTRAINT [DF_PES_PessoaContato_psc_situacao] DEFAULT ((1)),
[psc_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaContato_psc_dataCriacao] DEFAULT (getdate()),
[psc_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_PES_PessoaContato_psc_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_PessoaContato] on [dbo].[PES_PessoaContato]'
GO
ALTER TABLE [dbo].[PES_PessoaContato] ADD CONSTRAINT [PK_PES_PessoaContato] PRIMARY KEY CLUSTERED  ([pes_id], [psc_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaContato_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaContato_UPDATE]
		@pes_id uniqueidentifier
		,@psc_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@psc_contato VarChar (200)
		,@psc_situacao TinyInt
		,@psc_dataCriacao DateTime
		,@psc_dataAlteracao DateTime

AS
BEGIN
	UPDATE PES_PessoaContato
	SET
		tmc_id = @tmc_id
		,psc_contato = @psc_contato
		,psc_situacao = @psc_situacao
		,psc_dataCriacao = @psc_dataCriacao
		,psc_dataAlteracao = @psc_dataAlteracao
	WHERE 
		pes_id = @pes_id
	and	psc_id = @psc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Entidade_UPDATE]
	@ent_id UNIQUEIDENTIFIER
	, @ten_id UNIQUEIDENTIFIER
	, @ent_codigo VARCHAR (20)
	, @ent_nomeFantasia VARCHAR (200)
	, @ent_razaoSocial VARCHAR (200)
	, @ent_sigla VARCHAR (50)
	, @ent_cnpj VARCHAR (14)
	, @ent_inscricaoMunicipal VARCHAR (20)
	, @ent_inscricaoEstadual VARCHAR (20)
	, @ent_idSuperior UNIQUEIDENTIFIER
	, @ent_situacao TINYINT
	, @ent_dataCriacao DATETIME
	, @ent_dataAlteracao DATETIME
	, @ent_integridade INT
	-- Comentado para funcionar com versões anteriores do coresso
	--, @ent_urlAcesso VARCHAR (200)
	--, @ent_exibeLogoCliente BIT
	--, @ent_logoCliente VARCHAR (2000)
	--, @tep_id INT
	--, @tpl_id INT

AS
BEGIN
	UPDATE SYS_Entidade 
	SET 
		ten_id = @ten_id 
		, ent_codigo = @ent_codigo 
		, ent_nomeFantasia = @ent_nomeFantasia 
		, ent_razaoSocial = @ent_razaoSocial 
		, ent_sigla = @ent_sigla 
		, ent_cnpj = @ent_cnpj 
		, ent_inscricaoMunicipal = @ent_inscricaoMunicipal 
		, ent_inscricaoEstadual = @ent_inscricaoEstadual 
		, ent_idSuperior = @ent_idSuperior 
		, ent_situacao = @ent_situacao 
		, ent_dataCriacao = @ent_dataCriacao 
		, ent_dataAlteracao = @ent_dataAlteracao 
		, ent_integridade = @ent_integridade 
		--, ent_urlAcesso = @ent_urlAcesso 
		--, ent_exibeLogoCliente = @ent_exibeLogoCliente 
		--, ent_logoCliente = @ent_logoCliente 
		--, tep_id = @tep_id 
		--, tpl_id = @tpl_id

	WHERE 
		ent_id = @ent_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Sistema_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Sistema_UPDATE]
	@sis_id INT
	, @sis_nome VARCHAR (100)
	, @sis_descricao TEXT
	, @sis_caminho VARCHAR (2000)
	, @sis_caminhoLogout VARCHAR (2000)
	, @sis_urlImagem VARCHAR (2000)
	, @sis_urlLogoCabecalho VARCHAR (2000)
	, @sis_tipoAutenticacao TINYINT
	, @sis_urlIntegracao VARCHAR (200)
	, @sis_situacao TINYINT
	, @sis_ocultarLogo BIT

AS
BEGIN
	UPDATE SYS_Sistema 
	SET 
		sis_nome = @sis_nome 
		, sis_descricao = @sis_descricao 
		, sis_caminho = @sis_caminho 
		, sis_caminhoLogout = @sis_caminhoLogout 
		, sis_urlImagem = @sis_urlImagem 
		, sis_urlLogoCabecalho = @sis_urlLogoCabecalho 
		, sis_tipoAutenticacao = @sis_tipoAutenticacao 
		, sis_urlIntegracao = @sis_urlIntegracao 
		, sis_situacao = @sis_situacao 
		, sis_ocultarLogo = @sis_ocultarLogo 

	WHERE 
		sis_id = @sis_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_Pesquisa_uad_id]'
GO
-- ===========================================================================
-- Author:		Aline Dornelas
-- Create date: 11/08/2010 11:25
-- Description:	Consulta de usuários filtrando por 
--				UA do usuário logado (para filtrar os usuários 
--				apenas da hierarquia da UA do usuário logado),
--			    login, email, situacao e nome da Pessoa.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_Pesquisa_uad_id]
	@ent_id UNIQUEIDENTIFIER,
	@uad_id UNIQUEIDENTIFIER,
	@usu_login VARCHAR(500),
	@usu_email VARCHAR(500),
	@usu_bloqueado TINYINT,
	@pes_nome VARCHAR(200)
AS
BEGIN

	WITH UA_hierarquia AS  (
    -- UA
    SELECT uad_id, uad_idSuperior, uad_nome
    FROM SYS_UnidadeAdministrativa  WITH (NOLOCK)
	WHERE uad_situacao <> 3 AND uad_id = @uad_id
	
	UNION ALL
	
	 -- UAs da hierarquia
	 SELECT UA.uad_id,  UA.uad_idSuperior, UA.uad_nome
	 FROM SYS_UnidadeAdministrativa AS UA  WITH (NOLOCK)
	 INNER JOIN UA_hierarquia UAH
	 ON UA.uad_situacao <> 3 AND UA.uad_idSuperior = UAH.uad_id 
	)

	SELECT
		SYS_Usuario.usu_id,
		usu_login,
		usu_email,
		pes_nome,
		usu_situacao,
		CASE usu_situacao 
			WHEN 1 THEN 'Ativo'
			WHEN 2 THEN 'Bloqueado'
			WHEN 4 THEN 'Padrão do Sistema'
			WHEN 5 THEN 'Senha Expirada'
		  END AS usu_situacaoNome
	FROM
		SYS_Usuario WITH(NOLOCK)
	LEFT JOIN PES_Pessoa WITH(NOLOCK)
		ON SYS_Usuario.pes_id = PES_Pessoa.pes_id
		AND pes_situacao <> 3
	LEFT JOIN SYS_UsuarioGrupoUA WITH(NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupoUA.usu_id		
	WHERE
		usu_situacao <> 3
		AND ((@usu_login IS NULL) OR (usu_login LIKE '%' + @usu_login + '%'))
		AND ((@usu_email IS NULL) OR (usu_email LIKE '%' + @usu_email + '%'))
		AND ((@usu_bloqueado IS NULL) 
			OR ((@usu_bloqueado = 1) AND (usu_situacao IN (1,4)))
			OR ((@usu_bloqueado = 2) AND (usu_situacao IN (2,5)))
			)
		AND ((@pes_nome IS NULL) 
			OR ((pes_nome LIKE '%' + @pes_nome + '%') 
			OR (pes_nome_abreviado LIKE '%' + @pes_nome + '%'))
			)			
		AND	((@ent_id IS NULL) OR (SYS_Usuario.ent_id = @ent_id))
		AND ((@uad_id IS NULL) OR (SYS_UsuarioGrupoUA.uad_id IN(SELECT uad_id FROM UA_hierarquia)))
	ORDER BY	
		pes_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_LoginProviderDominioPermitido_DELETE]'
GO


CREATE PROCEDURE [dbo].[NEW_SYS_LoginProviderDominioPermitido_DELETE]
	@ent_id UNIQUEIDENTIFIER
	, @LoginProvider VARCHAR(128)

AS
BEGIN
	DELETE FROM 
		SYS_LoginProviderDominioPermitido 
	WHERE 
		ent_id = @ent_id 
		AND LoginProvider = @LoginProvider 

	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaContato_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaContato_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,psc_id
		,tmc_id
		,psc_contato
		,psc_situacao
		,psc_dataCriacao
		,psc_dataAlteracao
	FROM 
		PES_PessoaContato WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Entidade_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,ten_id
		,ent_codigo
		,ent_nomeFantasia
		,ent_razaoSocial
		,ent_sigla
		,ent_cnpj
		,ent_inscricaoMunicipal
		,ent_inscricaoEstadual
		,ent_idSuperior
		,ent_situacao
		,ent_dataCriacao
		,ent_dataAlteracao
		,ent_integridade
		,ent_urlAcesso
		,ent_exibeLogoCliente
		,ent_logoCliente
		,tep_id
		,tpl_id

	FROM 
		SYS_Entidade WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Sistema_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Sistema_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,sis_nome
		,sis_descricao
		,sis_caminho
		,sis_caminhoLogout
		,sis_urlImagem
		,sis_urlLogoCabecalho
		,sis_tipoAutenticacao
		,sis_urlIntegracao
		,sis_situacao
		,sis_ocultarLogo

	FROM 
		SYS_Sistema WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_uad_id]'
GO
-- =========================================================================
-- Author:		Aline Dornelas
-- Create date: 12/08/2010 10:00
-- Description:	Consulta de unidades adminstrativas filtrando por 
--				entidade, tipo de UA, UA do usuário logado (para filtrar 
--				as UAs apenas da hierarquia da UA do usuário logado), 
--				nome da UA e código da UA
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_uad_id]
	@tua_id uniqueidentifier,
	@ent_id uniqueidentifier,
	@uad_id uniqueidentifier,
	@uad_nome VARCHAR(200),
	@uad_codigo VARCHAR(20)
AS
BEGIN

	WITH UA_hierarquia AS  (
    -- UA
    SELECT uad_id, uad_idSuperior, uad_nome
    FROM SYS_UnidadeAdministrativa WITH (NOLOCK)  
	WHERE uad_situacao <> 3 AND uad_id = @uad_id
	
	UNION ALL
	
	 -- UAs da hierarquia
	 SELECT UA.uad_id,  UA.uad_idSuperior, UA.uad_nome
	 FROM SYS_UnidadeAdministrativa AS UA  WITH (NOLOCK)
	 INNER JOIN UA_hierarquia UAH
	 ON UA.uad_idSuperior = UAH.uad_id 
	 WHERE UA.uad_situacao <> 3
	)

	SELECT
		SYS_UnidadeAdministrativa.ent_id
		, SYS_Entidade.ent_razaoSocial
		, SYS_UnidadeAdministrativa.uad_id		
		, SYS_UnidadeAdministrativa.uad_codigo
		, SYS_UnidadeAdministrativa.uad_nome
		, SYS_TipoUnidadeAdministrativa.tua_id
		, SYS_TipoUnidadeAdministrativa.tua_nome		
		, (SELECT uad_nome FROM SYS_UnidadeAdministrativa A  WITH (NOLOCK) WHERE A.ent_id = SYS_UnidadeAdministrativa.ent_id AND  A.uad_id = SYS_UnidadeAdministrativa.uad_idSuperior) AS uad_nomeSup
		, CASE SYS_UnidadeAdministrativa.uad_situacao
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS uad_bloqueado
		
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)
	INNER JOIN SYS_Entidade WITH (NOLOCK)
		ON SYS_Entidade.ent_id = SYS_UnidadeAdministrativa.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UnidadeAdministrativa.tua_id = SYS_TipoUnidadeAdministrativa.tua_id	
	WHERE
		uad_situacao <> 3		
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND ((@tua_id IS NULL) OR (SYS_UnidadeAdministrativa.tua_id = @tua_id))
		AND ((@ent_id IS NULL) OR (SYS_UnidadeAdministrativa.ent_id = @ent_id))
		AND ((@uad_nome IS NULL) OR (SYS_UnidadeAdministrativa.uad_nome LIKE '%' + @uad_nome + '%'))
		AND ((@uad_codigo IS NULL) OR (SYS_UnidadeAdministrativa.uad_codigo LIKE '%' + @uad_codigo + '%'))
		AND ((@uad_id IS NULL) OR (SYS_UnidadeAdministrativa.uad_id IN(SELECT uad_id FROM UA_hierarquia)))
	ORDER BY 
		SYS_UnidadeAdministrativa.uad_nome
		
	SELECT @@ROWCOUNT						
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioLoginProvider_DELETE_By_usu_id]'
GO
-- =============================================
-- Author:		Gabriel Augusto Moreli
-- Create date: 09/03/2016
-- Description:	Deleta um registro da tabela
--				Sys_UsuarioLoginProvider
--				pelo id do usuário
-- =============================================
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioLoginProvider_DELETE_By_usu_id]
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM 
		SYS_UsuarioLoginProvider 
	WHERE 
		usu_id = @usu_id 
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaContato_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaContato_LOAD]
		@pes_id uniqueidentifier
		,@psc_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		pes_id
		,psc_id
		,tmc_id
		,psc_contato
		,psc_situacao
		,psc_dataCriacao
		,psc_dataAlteracao
 	FROM
 		PES_PessoaContato
	WHERE
		pes_id = @pes_id
	and	psc_id = @psc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Entidade_LOAD]
	@ent_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 ent_id  
		, ten_id 
		, ent_codigo 
		, ent_nomeFantasia 
		, ent_razaoSocial 
		, ent_sigla 
		, ent_cnpj 
		, ent_inscricaoMunicipal 
		, ent_inscricaoEstadual 
		, ent_idSuperior 
		, ent_situacao 
		, ent_dataCriacao 
		, ent_dataAlteracao 
		, ent_integridade 
		, ent_urlAcesso 
		, ent_exibeLogoCliente 
		, ent_logoCliente 
		, tep_id 
		, tpl_id

 	FROM
 		SYS_Entidade WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Sistema_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Sistema_LOAD]
	@sis_id Int
	
AS
BEGIN
	SELECT	Top 1
		 sis_id  
		, sis_nome 
		, sis_descricao 
		, sis_caminho 
		, sis_caminhoLogout 
		, sis_urlImagem 
		, sis_urlLogoCabecalho 
		, sis_tipoAutenticacao 
		, sis_urlIntegracao 
		, sis_situacao 
		, sis_ocultarLogo

 	FROM
 		SYS_Sistema
	WHERE 
		sis_id = @sis_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,ent_id
		,srr_id
		,rlt_id

	FROM 
		CFG_RelatorioServidorRelatorio WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaContato_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaContato_INSERT]
		@pes_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@psc_contato VarChar (200)
		,@psc_situacao TinyInt
		,@psc_dataCriacao DateTime
		,@psc_dataAlteracao DateTime


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_PessoaContato
		( 
			pes_id
			,tmc_id
			,psc_contato
			,psc_situacao
			,psc_dataCriacao
			,psc_dataAlteracao
		)
	OUTPUT inserted.psc_id INTO @ID
	VALUES
		( 
			@pes_id
			,@tmc_id
			,@psc_contato
			,@psc_situacao
			,@psc_dataCriacao
			,@psc_dataAlteracao
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Entidade_INSERT]
	@ten_id UniqueIdentifier
	, @ent_codigo VarChar (20)
	, @ent_nomeFantasia VarChar (200)
	, @ent_razaoSocial VarChar (200)
	, @ent_sigla VarChar (50)
	, @ent_cnpj VarChar (14)
	, @ent_inscricaoMunicipal VarChar (20)
	, @ent_inscricaoEstadual VarChar (20)
	, @ent_idSuperior UniqueIdentifier
	, @ent_situacao TinyInt
	, @ent_dataCriacao DateTime
	, @ent_dataAlteracao DateTime
	, @ent_integridade INT
	-- Comentado para funcionar com versões anteriores do coresso
	--, @ent_urlAcesso VarChar (200)
	--, @ent_exibeLogoCliente Bit
	--, @ent_logoCliente VarChar (2000)
	--, @tep_id INT
	--, @tpl_id INT

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Entidade
		( 
			ten_id 
			, ent_codigo 
			, ent_nomeFantasia 
			, ent_razaoSocial 
			, ent_sigla 
			, ent_cnpj 
			, ent_inscricaoMunicipal 
			, ent_inscricaoEstadual 
			, ent_idSuperior 
			, ent_situacao 
			, ent_dataCriacao 
			, ent_dataAlteracao 
			, ent_integridade 
			--, ent_urlAcesso 
			--, ent_exibeLogoCliente 
			--, ent_logoCliente 
			--, tep_id 
			--, tpl_id
		)
	OUTPUT inserted.ent_id INTO @ID
	VALUES
		( 
			@ten_id 
			, @ent_codigo 
			, @ent_nomeFantasia 
			, @ent_razaoSocial 
			, @ent_sigla 
			, @ent_cnpj 
			, @ent_inscricaoMunicipal 
			, @ent_inscricaoEstadual 
			, @ent_idSuperior 
			, @ent_situacao 
			, @ent_dataCriacao 
			, @ent_dataAlteracao 
			, @ent_integridade 
			--, @ent_urlAcesso 
			--, @ent_exibeLogoCliente 
			--, @ent_logoCliente 
			--, @tep_id 
			--, @tpl_id
		)
	SELECT ID FROM @ID
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Sistema_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Sistema_INSERT]
	@sis_id Int
	, @sis_nome VarChar (100)
	, @sis_descricao Text
	, @sis_caminho VarChar (2000)
	, @sis_caminhoLogout VarChar (2000)
	, @sis_urlImagem VarChar (2000)
	, @sis_urlLogoCabecalho VarChar (2000)
	, @sis_tipoAutenticacao TinyInt
	, @sis_urlIntegracao VarChar (200)
	, @sis_situacao TinyInt
	, @sis_ocultarLogo Bit

AS
BEGIN
	INSERT INTO 
		SYS_Sistema
		( 
			sis_id
			, sis_nome 
			, sis_descricao 
			, sis_caminho 
			, sis_caminhoLogout 
			, sis_urlImagem 
			, sis_urlLogoCabecalho 
			, sis_tipoAutenticacao 
			, sis_urlIntegracao 
			, sis_situacao 
			, sis_ocultarLogo 
 
		)
	VALUES
		( 
			@sis_id
			, @sis_nome
			, @sis_descricao 
			, @sis_caminho 
			, @sis_caminhoLogout 
			, @sis_urlImagem 
			, @sis_urlLogoCabecalho 
			, @sis_tipoAutenticacao 
			, @sis_urlIntegracao 
			, @sis_situacao 
			, @sis_ocultarLogo 
 
		)
		

	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_Select_gru_sis]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 08/09/2010 11:22
-- Description:	seleciona todas os grupos de um usuario em seus sistemas.
--				filtrados por usu_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_Select_gru_sis]
		@usu_id uniqueidentifier
AS
BEGIN	
	SELECT
		 SYS_Grupo.gru_id
		, SYS_Grupo.gru_nome
		, SYS_Sistema.sis_id
		, SYS_Sistema.sis_nome
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_nome
	FROM
		SYS_Usuario WITH (NOLOCK)
	INNER JOIN SYS_UsuarioGrupo WITH (NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupo.usu_id
	INNER JOIN SYS_Grupo WITH (NOLOCK)
		ON SYS_UsuarioGrupo.gru_id = SYS_Grupo.gru_id
	INNER JOIN SYS_Sistema WITH (NOLOCK)
		ON SYS_Grupo.sis_id = SYS_Sistema.sis_id
	LEFT JOIN SYS_UsuarioGrupoUA WITH (NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupoUA.usu_id
		AND SYS_Grupo.gru_id =  SYS_UsuarioGrupoUA.gru_id
	LEFT JOIN SYS_UnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UsuarioGrupoUA.uad_id = SYS_UnidadeAdministrativa.uad_id
		AND SYS_UsuarioGrupoUA.ent_id = SYS_UnidadeAdministrativa.ent_id
		AND uad_situacao <> 3
	WHERE 
		usu_situacao <> 3
		AND usg_situacao <> 3	
		AND gru_situacao <> 3
		AND sis_situacao <> 3	
		AND(SYS_Usuario.usu_id IS NULL OR SYS_Usuario.usu_id = @usu_id)
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_sis_id]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_sis_id]
	@sis_id INT
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,srr_id
		,rlt_id

	FROM
		CFG_RelatorioServidorRelatorio WITH(NOLOCK)
	WHERE 
		sis_id = @sis_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Select_Modulos_By_Sistema_Visao_Modulo]'
GO
-- ========================================================================
-- Author:		Gabriel Scavassa
-- Create date: 04/05/2016
-- Description: Retorna Modulos que tenha uma ligação com visão de modulo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Select_Modulos_By_Sistema_Visao_Modulo]
	@sis_id INT
	,@vis_id INT
	,@mod_id INT
AS
BEGIN
	SELECT
		M.mod_id
		,M.sis_id
		,M.mod_id
		,M.mod_idPai
		,M.mod_nome
		,M.mod_situacao
	FROM
		SYS_Modulo M
		INNER JOIN SYS_VisaoModulo VM ON M.mod_id = VM.mod_id 
	WHERE
		vm.vis_id = @vis_id
		AND vm.sis_id = @sis_id
		AND VM.mod_id = @mod_id		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaContato_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_PessoaContato_DELETE]
		@pes_id uniqueidentifier
		,@psc_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		PES_PessoaContato
	WHERE 
		pes_id = @pes_id
	and	psc_id = @psc_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Entidade_DELETE]
	@ent_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_Entidade	
	WHERE 
		ent_id = @ent_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Sistema_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_Sistema_DELETE]
	@sis_id INT

AS
BEGIN
	DELETE FROM 
		SYS_Sistema 
	WHERE 
		sis_id = @sis_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_ent_id]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,srr_id
		,rlt_id

	FROM
		CFG_RelatorioServidorRelatorio WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Modulo_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Modulo_UPDATE]
	@sis_id INT
	, @mod_id INT
	, @mod_nome varchar(50)
	, @mod_descricao text
	, @mod_idPai INT
	, @mod_auditoria BIT
	, @mod_situacao TINYINT
	, @mod_dataCriacao DATETIME
	, @mod_dataAlteracao DATETIME
	
AS
BEGIN
	UPDATE SYS_Modulo
	SET
		mod_nome = @mod_nome
		, mod_descricao = @mod_descricao
		, mod_idPai = @mod_idPai
		, mod_auditoria = @mod_auditoria 
		, mod_situacao = @mod_situacao 
		, mod_dataCriacao = @mod_dataCriacao 
		, mod_dataAlteracao = @mod_dataAlteracao 
	WHERE
		sis_id = @sis_id
		AND mod_id = @mod_id
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Parametro_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Parametro_UPDATE]
		@par_id uniqueidentifier
		,@par_chave VarChar (100)
		,@par_valor VarChar (1000)
		,@par_descricao VarChar (200)
		,@par_situacao TinyInt
		,@par_vigenciaInicio Date
		,@par_vigenciaFim Date
		,@par_dataCriacao DateTime
		,@par_dataAlteracao DateTime
		,@par_obrigatorio Bit

AS
BEGIN
	UPDATE SYS_Parametro
	SET 
		par_chave = @par_chave
		,par_valor = @par_valor
		,par_descricao = @par_descricao
		,par_situacao = @par_situacao
		,par_vigenciaInicio = @par_vigenciaInicio
		,par_vigenciaFim = @par_vigenciaFim
		,par_dataCriacao = @par_dataCriacao
		,par_dataAlteracao = @par_dataAlteracao
		,par_obrigatorio = @par_obrigatorio
	WHERE 
		par_id = @par_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_Select_gru_sis_id]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 08/09/2010 11:22
-- Description:	seleciona todas os grupos de um usuario em seus sistemas.
--				filtrados por usu_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_Select_gru_sis_id]
		@usu_id uniqueidentifier
AS
BEGIN	
	SELECT
		 SYS_Grupo.gru_id
		, SYS_Grupo.gru_nome
		, SYS_Sistema.sis_id
		, SYS_Sistema.sis_nome
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_nome
		, SYS_Grupo.gru_situacao
		, SYS_UsuarioGrupo.usg_situacao
	FROM
		SYS_Usuario WITH (NOLOCK)
	INNER JOIN SYS_UsuarioGrupo WITH (NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupo.usu_id
	INNER JOIN SYS_Grupo WITH (NOLOCK)
		ON SYS_UsuarioGrupo.gru_id = SYS_Grupo.gru_id
	INNER JOIN SYS_Sistema WITH (NOLOCK)
		ON SYS_Grupo.sis_id = SYS_Sistema.sis_id
	LEFT JOIN SYS_UsuarioGrupoUA WITH (NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupoUA.usu_id
		AND SYS_Grupo.gru_id =  SYS_UsuarioGrupoUA.gru_id
	LEFT JOIN SYS_UnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UsuarioGrupoUA.uad_id = SYS_UnidadeAdministrativa.uad_id
		AND SYS_UsuarioGrupoUA.ent_id = SYS_UnidadeAdministrativa.ent_id
		AND uad_situacao <> 3
	WHERE 
		SYS_Usuario.usu_situacao <> 3
		AND(SYS_UsuarioGrupo.usg_situacao = 1)
		AND (SYS_Grupo.gru_situacao NOT IN (2, 3))
		AND(SYS_Sistema.sis_situacao <> 3)
		AND(SYS_Usuario.usu_id IS NULL OR SYS_Usuario.usu_id = @usu_id)
	ORDER BY 
		SYS_Sistema.sis_nome,
		SYS_Grupo.gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_srr_id]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_srr_id]
	@srr_id INT
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,srr_id
		,rlt_id

	FROM
		CFG_RelatorioServidorRelatorio WITH(NOLOCK)
	WHERE 
		srr_id = @srr_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Modulo_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Modulo_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,mod_id
		, mod_nome
		, mod_descricao
		, mod_idPai
		, mod_auditoria 
		, mod_situacao 
		, mod_dataCriacao 
		, mod_dataAlteracao 
		
	FROM 
		SYS_Modulo WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Parametro_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Parametro_SELECT]
	
AS
BEGIN
	SELECT 
		par_id
		,par_chave
		,par_valor
		,par_descricao
		,par_situacao
		,par_vigenciaInicio
		,par_vigenciaFim
		,par_dataCriacao
		,par_dataAlteracao
		,par_obrigatorio
		
	FROM 
		SYS_Parametro WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InsereParametro]'
GO
-- =============================================
-- Author:		Haila Pelloso
-- Create date: 11/10/2011
-- Description:	Insere o parâmetro com valores passados. Verifica se a chave
--				do parâmetro já foi inserida, e não permite duplicar.
--				@par_chave - Obrigatório - Chave do parâmetro.
--				@par_valor - Obrigatório - Valor do parâmetro.
--				@par_descricao - Obrigatório - Descrição do parâmetro.
--				@par_obrigatorio - Obrigatório - indica se o parâmetro é obrigatório no sistema.
-- =============================================
CREATE PROCEDURE [dbo].[MS_InsereParametro]
	@par_chave VARCHAR (100)
	, @par_valor VARCHAR (1000)
	, @par_descricao VARCHAR (200)
	, @par_obrigatorio BIT
AS
BEGIN
	-- Busca se configuração já existe.
	DECLARE @qt INT = 
	(
		SELECT COUNT(*)
		FROM SYS_Parametro WITH(NOLOCK)
		WHERE
			LOWER(RTRIM(LTRIM(par_chave))) = LOWER(RTRIM(LTRIM(@par_chave)))
			AND par_situacao <> 3
	)
	
	IF (@qt IS NOT NULL AND @qt > 0)
	BEGIN
		PRINT 'O parâmetro ' + @par_chave + ' já existe no sistema (' + CAST(@qt AS VARCHAR) + ').'; 
	END
	ELSE
	BEGIN
		DECLARE @DataAtual DATETIME = GETDATE();
		
		INSERT INTO SYS_Parametro
			   (
					par_chave,
					par_valor,
					par_descricao,
					par_obrigatorio,
					par_situacao,
					par_vigenciaInicio,
					par_vigenciaFim,
					par_dataCriacao,
					par_dataAlteracao
			   )
		 VALUES
			   (
					@par_chave,
					@par_valor,
					@par_descricao,
					@par_obrigatorio,
					1, -- Situacao
					@DataAtual, -- Vigência
					NULL,
					@DataAtual, -- Inclusão
					@DataAtual -- Alteração
				)
		
		IF (@@ROWCOUNT = 0)
			PRINT 'O parâmetro não foi incluído.';
		ELSE
			PRINT 'Parâmetro incluído com sucesso.';

	END

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_rlt_id]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_SELECTBY_rlt_id]
	@rlt_id INT
AS
BEGIN
	SELECT
		sis_id
		,ent_id
		,srr_id
		,rlt_id

	FROM
		CFG_RelatorioServidorRelatorio WITH(NOLOCK)
	WHERE 
		rlt_id = @rlt_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Modulo_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Modulo_LOAD]
	@sis_id INT
	, @mod_id INT
	
AS
BEGIN
	SELECT	Top 1
		sis_id
		, mod_id
		, mod_nome
		, mod_descricao
		, mod_idPai
		, mod_auditoria 
		, mod_situacao 
		, mod_dataCriacao 
		, mod_dataAlteracao 

 	FROM
 		SYS_Modulo
	WHERE 
		sis_id = @sis_id
		AND mod_id = @mod_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Parametro_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Parametro_LOAD]
	@par_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		par_id
		,par_chave
		,par_valor
		,par_descricao
		,par_situacao
		,par_vigenciaInicio
		,par_vigenciaFim
		,par_dataCriacao
		,par_dataAlteracao
		,par_obrigatorio
 	FROM
 		SYS_Parametro
	WHERE 
		par_id = @par_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupoUA_DeletarPorUsuarioGrupoUA]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 13/10/2011 16:16
-- Description:	utilizado para deletar uma ua do grupo do usuário
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupoUA_DeletarPorUsuarioGrupoUA]
	@usu_id UNIQUEIDENTIFIER
	, @gru_id UNIQUEIDENTIFIER
	, @ent_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM 
		SYS_UsuarioGrupoUA	
	WHERE 
		usu_id = @usu_id
		AND gru_id = @gru_id
		AND ent_id = @ent_id 
		AND uad_id = @uad_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_LOADBy_ent_id_usu_dominio_usu_login]'
GO
-- ==========================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 10/09/2010 08:40
-- Description:	Carrega os dados do usuário através do login.
-- ==========================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_LOADBy_ent_id_usu_dominio_usu_login]
	@ent_id uniqueidentifier
	, @usu_dominio VARCHAR(100)
	, @usu_login VARCHAR(500)	
AS
BEGIN
	SELECT TOP 1
		usu_id
		, usu_login
		, usu_dominio		
		, usu_email
		, usu_criptografia		
		, usu_situacao
		, usu_dataCriacao
		, usu_dataAlteracao
		, pes_id
		, ent_id
		, usu_integracaoAD
		, usu_integracaoExterna
	FROM
		SYS_Usuario WITH (NOLOCK)
	WHERE
		usu_situacao <> 3	
		AND ent_id = @ent_id
		AND usu_dominio = @usu_dominio
		AND usu_login = @usu_login		
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_INSERT]
	@sis_id Int
	, @ent_id UniqueIdentifier
	, @srr_id Int
	, @rlt_id Int

AS
BEGIN
	INSERT INTO 
		CFG_RelatorioServidorRelatorio
		( 
			sis_id 
			, ent_id 
			, srr_id 
			, rlt_id 
 
		)
	VALUES
		( 
			@sis_id 
			, @ent_id 
			, @srr_id 
			, @rlt_id 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Modulo_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Modulo_INSERT]
	@sis_id INT
	, @mod_id INT
	, @mod_nome varchar(50)
	, @mod_descricao text
	, @mod_idPai INT
	, @mod_auditoria BIT
	, @mod_situacao TINYINT
	, @mod_dataCriacao DATETIME
	, @mod_dataAlteracao DATETIME
	
AS
BEGIN
	INSERT INTO
		SYS_Modulo
		(
			sis_id
			, mod_id
			, mod_nome
			, mod_descricao
			, mod_idPai
			, mod_auditoria
			, mod_situacao
			, mod_dataCriacao
			, mod_dataAlteracao
		)
		VALUES
		(
			@sis_id
			, @mod_id
			, @mod_nome
			, @mod_descricao
			, @mod_idPai
			, @mod_auditoria
			, @mod_situacao
			, @mod_dataCriacao
			, @mod_dataAlteracao
		)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Parametro_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Parametro_INSERT]
		@par_chave VarChar (100)
		,@par_valor VarChar (1000)
		,@par_descricao VarChar (200)
		,@par_situacao TinyInt
		,@par_vigenciaInicio Date
		,@par_vigenciaFim Date
		,@par_dataCriacao DateTime
		,@par_dataAlteracao DateTime
		,@par_obrigatorio Bit

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Parametro
		( 
			par_chave
			,par_valor
			,par_descricao
			,par_situacao
			,par_vigenciaInicio
			,par_vigenciaFim
			,par_dataCriacao
			,par_dataAlteracao
			,par_obrigatorio
 
		)
	OUTPUT inserted.par_id INTO @ID
	VALUES
		( 
			@par_chave
			,@par_valor
			,@par_descricao
			,@par_situacao
			,@par_vigenciaInicio
			,@par_vigenciaFim
			,@par_dataCriacao
			,@par_dataAlteracao
			,@par_obrigatorio
		)
	SELECT ID FROM @ID		
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupo_DeletarPorUsuario]'
GO
-- ======================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 13/10/2011 18:00
-- Description:	utilizado para deletar a ligação dos grupos com o usuário
-- ======================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupo_DeletarPorUsuario]
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN
	--Exclui as UA's dos grupos do usuário
	DELETE FROM 
		SYS_UsuarioGrupoUA	
	WHERE 
		usu_id = @usu_id
		
	--Exclui logicamente os grupo do usuário
	UPDATE SYS_UsuarioGrupo
	SET 
		usg_situacao = 3
	WHERE 
		usu_id = @usu_id				
		
	RETURN ISNULL(@@ROWCOUNT,-1)	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_SelectBy_SistemaEntidade]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/09/2010 08:41
-- Description:	utilizado na busca de entidades, retorna as entidades
--              que não foram excluídas logicamente e que estão ligadas
--              a pelo menos um sistema
--				filtrados por:
--					entidade (diferente do parametro), tipo de entidade,					 
--                  razão social, nome fantasia, cnpj, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_SelectBy_SistemaEntidade]	
	@ent_id uniqueidentifier
	,@ten_id uniqueidentifier
	,@ent_razaoSocial VARCHAR(200)
	,@ent_nomeFantasia VARCHAR(200)
	,@ent_CNPJ VARCHAR(14)	
	,@ent_situacao TINYINT	
AS
BEGIN
	SELECT
		ent_id
		,SYS_Entidade.ten_id
		,ent_razaoSocial
		,ent_nomeFantasia
		,ent_CNPJ
		,ten_nome
		,CASE ent_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS ent_situacao
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_Entidade.ten_id = SYS_TipoEntidade.ten_id		
	WHERE
		ent_situacao <> 3	
		AND ten_situacao <> 3	
		AND (@ent_id is null or ent_id <> @ent_id)						
		AND (@ten_id is null or SYS_Entidade.ten_id = @ten_id)				
		AND (@ent_razaoSocial is null or ent_razaoSocial LIKE '%' + @ent_razaoSocial + '%')
		AND (@ent_nomeFantasia is null or ent_nomeFantasia LIKE '%' + @ent_nomeFantasia + '%')		
		AND (@ent_CNPJ is null or ent_CNPJ LIKE '%' + @ent_CNPJ + '%')			
		AND (@ent_situacao is null or ent_situacao = @ent_situacao)								
		AND (ent_id in (SELECT ent_id FROM SYS_SistemaEntidade WITH (NOLOCK) WHERE sen_situacao <> 3))		
	ORDER BY
		ent_razaosocial
		
	SELECT @@ROWCOUNT		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_RelatorioServidorRelatorio_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_RelatorioServidorRelatorio_DELETE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT
	, @rlt_id INT

AS
BEGIN
	DELETE FROM 
		CFG_RelatorioServidorRelatorio 
	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 
		AND rlt_id = @rlt_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Modulo_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Modulo_DELETE]
	@sis_id INT
	, @mod_id INT
	
AS
BEGIN
	DELETE FROM 
		SYS_Modulo
	WHERE
		sis_id = @sis_id
		AND mod_id = @mod_id
		
	
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Parametro_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Parametro_DELETE]
	@par_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_Parametro
	WHERE 
		par_id = @par_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UnidadeFederativa_SelectBy_Sigla]'
GO
-- ========================================================================
-- Author:		Paula Fiorini
-- Create date: 18/10/2011 11:42
-- Description:	Retorna a unidade federativa filtrando através da sigla.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UnidadeFederativa_SelectBy_Sigla]
	@unf_sigla VARCHAR(2)	
AS
BEGIN
	SELECT TOP 1
		unf_id
		, pai_id
		, unf_nome
		, unf_sigla
		, unf_situacao
		, unf_integridade		
	FROM
		END_UnidadeFederativa Unf WITH (NOLOCK)		
	WHERE
		UPPER(Unf.unf_sigla) = UPPER(@unf_sigla)			
	ORDER BY
		unf_nome
		
	SELECT @@ROWCOUNT		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Relatorio_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Relatorio_UPDATE]
	@rlt_id INT
	, @rlt_nome VARCHAR (100)
	, @rlt_situacao TINYINT
	, @rlt_dataCriacao DATETIME
	, @rlt_dataAlteracao DATETIME
	, @rlt_integridade INT

AS
BEGIN
	UPDATE CFG_Relatorio 
	SET 
		rlt_nome = @rlt_nome 
		, rlt_situacao = @rlt_situacao 
		, rlt_dataCriacao = @rlt_dataCriacao 
		, rlt_dataAlteracao = @rlt_dataAlteracao 
		, rlt_integridade = @rlt_integridade 

	WHERE 
		rlt_id = @rlt_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ModuloSiteMap_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ModuloSiteMap_UPDATE]
	@sis_id INT
	, @mod_id INT
	, @msm_id INT
	, @msm_nome varchar(50)
	, @msm_descricao varchar(5000)
	, @msm_url varchar(500)
	, @msm_informacoes text
	, @msm_urlHelp varchar(500) = NULL
	
AS
BEGIN
	UPDATE SYS_ModuloSiteMap
	SET
		msm_nome = @msm_nome
		, msm_descricao = @msm_descricao
		, msm_url = @msm_url
		, msm_informacoes = @msm_informacoes
		, msm_urlHelp = @msm_urlHelp
	WHERE
		sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_INSERT]'
GO
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_INSERT]
		@ent_id uniqueidentifier
		,@usu_login VarChar (500)
		,@usu_email VarChar (500)
		,@usu_senha VarChar (256)
		,@usu_criptografia TinyInt
		,@usu_situacao TinyInt
		,@usu_dataCriacao DateTime
		,@usu_dataAlteracao DateTime
		,@pes_id uniqueidentifier
		,@usu_integridade Int
		,@usu_dominio varchar(100)
		,@usu_integracaoAD TINYINT = 0  -- atribuído valor default para funcionar com o sistema em uma versão anterior a 1.22.0.0
		,@usu_integracaoExterna TinyInt = 0
AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Usuario
		( 
			ent_id
			,usu_login
			,usu_email
			,usu_senha
			,usu_criptografia 
			,usu_situacao
			,usu_dataCriacao
			,usu_dataAlteracao
			,pes_id 
			,usu_integridade
			,usu_dominio
			,usu_integracaoAD
			,usu_integracaoExterna 
		)
	OUTPUT inserted.usu_id INTO @ID
	VALUES
		( 
			@ent_id
			,@usu_login
			,@usu_email
			,@usu_senha
			,@usu_criptografia 
			,@usu_situacao
			,@usu_dataCriacao
			,@usu_dataAlteracao
			,@pes_id
			,@usu_integridade
			,@usu_dominio
			,@usu_integracaoAD
			,@usu_integracaoExterna 
		)
	SELECT ID FROM @ID
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_gru_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 22/01/2010 11:50
-- Description:	Procedure para retorno dos módulos pai de um determinado
--				grupo.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_SelectBy_gru_id]
	@gru_id uniqueidentifier
AS
BEGIN
	SELECT
		SYS_Modulo.sis_id
		, SYS_Modulo.mod_id
		, SYS_Modulo.mod_nome
		, SYS_Modulo.mod_descricao
		, SYS_Modulo.mod_idPai
		, SYS_Modulo.mod_auditoria
		, SYS_Modulo.mod_situacao
		, SYS_Modulo.mod_dataCriacao
		, SYS_Modulo.mod_dataAlteracao
	--FROM
	--	SYS_Modulo WITH(NOLOCK)
	--	INNER JOIN SYS_GrupoPermissao WITH(NOLOCK)
	--		ON SYS_Modulo.sis_id = SYS_GrupoPermissao.sis_id
	--		   AND SYS_Modulo.mod_id = SYS_GrupoPermissao.mod_id
	FROM
		SYS_GrupoPermissao WITH(NOLOCK)
	INNER JOIN SYS_Modulo WITH(NOLOCK)
		ON SYS_Modulo.sis_id = SYS_GrupoPermissao.sis_id
		AND SYS_Modulo.mod_id = SYS_GrupoPermissao.mod_id
	INNER JOIN SYS_VisaoModulo WITH(NOLOCK)
		ON SYS_VisaoModulo.sis_id = SYS_GrupoPermissao.sis_id
		AND SYS_VisaoModulo.mod_id = SYS_GrupoPermissao.mod_id
	INNER JOIN SYS_Grupo WITH(NOLOCK)
		ON SYS_Grupo.gru_id = SYS_GrupoPermissao.gru_id
		AND SYS_Grupo.vis_id = SYS_VisaoModulo.vis_id	
	WHERE
		mod_situacao <> 3
		AND gru_situacao <> 3
		AND SYS_GrupoPermissao.gru_id = @gru_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_SelectBy_UsuarioGrupoUA]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 21/09/2010 11:25
-- Description:	utilizado na busca de entidades, retorna as entidades
--              que não foram excluídas logicamente,
--				filtrados por:
--					entidade, grupo, usuario e situacao					 
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_SelectBy_UsuarioGrupoUA]	
	@ent_id uniqueidentifier
	,@gru_id uniqueidentifier
	,@usu_id uniqueidentifier
	,@ent_situacao TINYINT	
AS
BEGIN
	SELECT 
		SYS_Entidade.ent_id
		,SYS_Entidade.ten_id
		,ent_razaoSocial
		,ent_nomeFantasia
		,ent_CNPJ
		,ten_nome
		,CASE ent_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS ent_situacao
	FROM
		SYS_Entidade WITH (NOLOCK)
	INNER JOIN SYS_TipoEntidade WITH (NOLOCK)
		ON SYS_TipoEntidade.ten_id = SYS_Entidade.ten_id		
	INNER JOIN SYS_UsuarioGrupoUA WITH (NOLOCK)
		ON SYS_UsuarioGrupoUA.ent_id = SYS_Entidade.ent_id
	WHERE
		ent_situacao <> 3
		AND ten_situacao <> 3		
		AND (@ent_id is null or SYS_Entidade.ent_id = @ent_id)
		AND (@gru_id is null or SYS_UsuarioGrupoUA.gru_id = @gru_id)
		AND (@usu_id is null or SYS_UsuarioGrupoUA.usu_id = @usu_id)
		AND (@ent_situacao is null or ent_situacao = @ent_situacao)						
	ORDER BY
		ent_razaosocial
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Relatorio_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Relatorio_SELECT]
	
AS
BEGIN
	SELECT 
		rlt_id
		,rlt_nome
		,rlt_situacao
		,rlt_dataCriacao
		,rlt_dataAlteracao
		,rlt_integridade

	FROM 
		CFG_Relatorio WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ModuloSiteMap_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ModuloSiteMap_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,mod_id
		, msm_id
		, msm_nome
		, msm_descricao
		, msm_url
		, msm_informacoes
		, msm_urlHelp
		
	FROM 
		SYS_ModuloSiteMap WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_SelectBy_mod_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 22/01/2010 13:30
-- Description:	Retorna um objeto SYS_GrupoPermissao filtrado por módulo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_SelectBy_mod_id]
	@mod_id int
	, @gru_id uniqueidentifier
AS
BEGIN
	SELECT
		SYS_GrupoPermissao.gru_id
		, SYS_GrupoPermissao.sis_id
		, SYS_GrupoPermissao.mod_id
		, SYS_Modulo.mod_nome
		, SYS_GrupoPermissao.grp_consultar
		, SYS_GrupoPermissao.grp_inserir
		, SYS_GrupoPermissao.grp_alterar
		, SYS_GrupoPermissao.grp_excluir
	FROM
		SYS_GrupoPermissao WITH(NOLOCK)
	INNER JOIN SYS_Modulo WITH(NOLOCK)
		ON SYS_GrupoPermissao.sis_id = SYS_Modulo.sis_id
			AND SYS_GrupoPermissao.mod_id = SYS_Modulo.mod_id
	WHERE
		mod_situacao <> 3
		AND SYS_Modulo.mod_idPai = @mod_id
		AND SYS_GrupoPermissao.gru_id = @gru_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[FN_Select_uad_idBy_UsuarioGrupoUA]'
GO
/****** Object:  UserDefinedFunction [dbo].[FN_Select_uad_idBy_UsuarioGrupoUA]    ******/
CREATE FUNCTION [dbo].[FN_Select_uad_idBy_UsuarioGrupoUA](@gru_id uniqueidentifier, @usu_id uniqueidentifier)
RETURNS @Table_uad_id  TABLE (
	uad_id uniqueidentifier NOT NULL
)
AS 
BEGIN
	
	DECLARE @vis_id INT
	SELECT
		@vis_id = vis_id
	FROM
		SYS_GRUPO WITH(NOLOCK)
	WHERE
		gru_id = @gru_id	

	--INICIAR BUSCA DE UA'S A PARTIR DAS DEFINIDAS
	DECLARE @tblUApai TABLE (uad_id uniqueidentifier NOT NULL)
	DECLARE @tblTmp TABLE (uad_id uniqueidentifier NOT NULL)
	
	INSERT INTO @tblUApai
		SELECT
			UGUA.uad_id
		FROM
			SYS_UsuarioGrupoUA UGUA WITH(NOLOCK)
		WHERE UGUA.usu_id = @usu_id
		  AND UGUA.gru_id = @gru_id
		  
	INSERT INTO @Table_uad_id
		SELECT uad_id FROM @tblUApai
	
	IF @vis_id = 2
		BEGIN
			WHILE (SELECT COUNT(uad_id) FROM @tblUApai) > 0
			BEGIN
				INSERT INTO @tblTmp
					SELECT uad_id FROM @tblUApai
				DELETE FROM @tblUApai
				
				INSERT INTO @tblUApai
					SELECT
						uad_id
					FROM
						SYS_UnidadeAdministrativa WITH(NOLOCK)
					WHERE
						uad_idSuperior IN (SELECT uad_id FROM @tblTmp)
					
				INSERT INTO @Table_uad_id
					SELECT uad_id FROM @tblUApai
					
				DELETE FROM @tblTmp
			END 
		END
	RETURN 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Relatorio_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Relatorio_LOAD]
	@rlt_id Int
	
AS
BEGIN
	SELECT	Top 1
		 rlt_id  
		, rlt_nome 
		, rlt_situacao 
		, rlt_dataCriacao 
		, rlt_dataAlteracao 
		, rlt_integridade 

 	FROM
 		CFG_Relatorio
	WHERE 
		rlt_id = @rlt_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ModuloSiteMap_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ModuloSiteMap_LOAD] 
	@sis_id INT
	, @mod_id INT
	, @msm_id INT
	
AS
BEGIN
	SELECT Top 1 
		sis_id
		, mod_id
		, msm_id
		, msm_nome
		, msm_descricao
		, msm_url
		, msm_informacoes
		, msm_urlHelp
 	FROM
 		 SYS_ModuloSiteMap
	WHERE 
		sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_UsuarioGrupoUA]'
GO
-- =========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 21/09/2010 14:37
-- Description:	Select para retorna os dados da unidade administrativa na 
--				buscar de ua conforme documento de especificação tópico 4.4.6
--				e no tópico 4.4.22, gerenciar unidade administrativa.
-- =========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_UsuarioGrupoUA]
	@gru_id uniqueidentifier
	, @usu_id uniqueidentifier	
	, @tua_id uniqueidentifier
	, @ent_id uniqueidentifier
	, @uad_id uniqueidentifier
	, @uad_nome VARCHAR(200)
	, @uad_codigo VARCHAR(20)
	, @uad_situacao TINYINT
AS
BEGIN
	SELECT
		SYS_UnidadeAdministrativa.ent_id
		, SYS_Entidade.ent_razaoSocial
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_codigo
		, SYS_UnidadeAdministrativa.uad_nome
		, SYS_TipoUnidadeAdministrativa.tua_id
		, SYS_TipoUnidadeAdministrativa.tua_nome		
		, (SELECT uad_nome FROM SYS_UnidadeAdministrativa A WITH (NOLOCK) WHERE A.ent_id = SYS_UnidadeAdministrativa.ent_id AND A.uad_id = SYS_UnidadeAdministrativa.uad_idSuperior) AS uad_nomeSup
		, CASE SYS_UnidadeAdministrativa.uad_situacao
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS uad_bloqueado
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)
	INNER JOIN SYS_Entidade WITH (NOLOCK)
		ON SYS_Entidade.ent_id = SYS_UnidadeAdministrativa.ent_id
	INNER JOIN SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
		ON SYS_UnidadeAdministrativa.tua_id = SYS_TipoUnidadeAdministrativa.tua_id	
	WHERE
		uad_situacao <> 3		
		AND ent_situacao <> 3
		AND tua_situacao <> 3
		AND ((@tua_id IS NULL) OR (SYS_UnidadeAdministrativa.tua_id = @tua_id))
		AND ((@ent_id IS NULL) OR (SYS_UnidadeAdministrativa.ent_id = @ent_id))
		AND ((@uad_id IS NULL) OR (SYS_UnidadeAdministrativa.uad_id <> @uad_id))
		AND ((@uad_nome IS NULL) OR (SYS_UnidadeAdministrativa.uad_nome LIKE '%' + @uad_nome + '%'))
		AND ((@uad_codigo IS NULL) OR (SYS_UnidadeAdministrativa.uad_codigo LIKE '%' + @uad_codigo + '%'))
		AND ((@uad_situacao IS NULL) OR (SYS_UnidadeAdministrativa.uad_situacao = @uad_situacao))		
		AND (((@gru_id IS NULL) AND (@usu_id IS NULL)) OR (uad_id IN (SELECT uad_id FROM FN_Select_uad_idBy_UsuarioGrupoUA(@gru_id, @usu_id))))
	ORDER BY 
		SYS_UnidadeAdministrativa.uad_nome
		
	SELECT @@ROWCOUNT						
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Relatorio_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Relatorio_INSERT]
	@rlt_id Int
	, @rlt_nome VarChar (100)
	, @rlt_situacao TinyInt
	, @rlt_dataCriacao DateTime
	, @rlt_dataAlteracao DateTime
	, @rlt_integridade Int

AS
BEGIN
	INSERT INTO 
		CFG_Relatorio
		( 
			rlt_id 
			, rlt_nome 
			, rlt_situacao 
			, rlt_dataCriacao 
			, rlt_dataAlteracao 
			, rlt_integridade 
 
		)
	VALUES
		( 
			@rlt_id 
			, @rlt_nome 
			, @rlt_situacao 
			, @rlt_dataCriacao 
			, @rlt_dataAlteracao 
			, @rlt_integridade 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ModuloSiteMap_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ModuloSiteMap_INSERT]
	@sis_id INT
	, @mod_id INT
	, @msm_id INT
	, @msm_nome varchar(50)
	, @msm_descricao varchar(5000)
	, @msm_url varchar(500)
	, @msm_informacoes text
	, @msm_urlHelp varchar(500) = NULL
AS
BEGIN
	INSERT INTO
		SYS_ModuloSiteMap
		(
			sis_id
			, mod_id
			, msm_id
			, msm_nome
			, msm_descricao
			, msm_url
			, msm_informacoes
			, msm_urlHelp
		)
		VALUES
		(
			@sis_id
			, @mod_id
			, @msm_id
			, @msm_nome
			, @msm_descricao
			, @msm_url
			, @msm_informacoes
			, @msm_urlHelp
		)
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Relatorio_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Relatorio_DELETE]
	@rlt_id INT

AS
BEGIN
	DELETE FROM 
		CFG_Relatorio 
	WHERE 
		rlt_id = @rlt_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_CertidaoCivil_SelecionaTipoCertidao]'
GO
-- ========================================================================
-- Author:		Renata Prado
-- Create date: 18/01/2013 
-- Description:	utilizado na busca de certidoes da pessoa, retorna as certidoes
--              da pessoa filtrados por: pes_id e tipo
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_CertidaoCivil_SelecionaTipoCertidao]	
	@pes_id uniqueidentifier,
	@ctc_tipo TINYINT
AS
BEGIN
	SELECT
		TOP 1
		pes_id
		, ctc_id
		, ctc_tipo
		, ctc_numeroTermo
		, ctc_folha
		, ctc_livro
		, ctc_dataEmissao
		, ctc_nomeCartorio
		, cid_idCartorio	
		, unf_idCartorio
		, ctc_distritoCartorio	
		, ctc_situacao
		, ctc_dataCriacao
		, ctc_dataAlteracao
		, ctc_matricula
		, ctc_gemeo 
		, ctc_modeloNovo
	FROM
		PES_CertidaoCivil WITH (NOLOCK)
	WHERE		
		ctc_situacao <> 3
		AND pes_id = @pes_id
		AND ctc_tipo = @ctc_tipo
	ORDER BY
		ctc_tipo
	SELECT @@ROWCOUNT	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_ModuloSiteMap_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_ModuloSiteMap_DELETE]
	@sis_id INT
	, @mod_id INT
	, @msm_id INT
	
AS
BEGIN
	DELETE FROM 
		SYS_ModuloSiteMap
	WHERE
		sis_id = @sis_id
		AND mod_id = @mod_id
		AND msm_id = @msm_id
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_gru_id]'
GO
-- ===========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 06/01/2011 08:46
-- Description:	Consulta de usuários filtrado pelo ID do grupo
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_gru_id]
	@gru_id uniqueidentifier
AS
BEGIN
	SELECT 
		SYS_Usuario.usu_id
		, CASE 
			WHEN usu_dominio is NULL THEN usu_login
			ELSE usu_dominio+'\'+usu_login
		  END AS usu_login	
		, pes_nome	
		, SYS_Usuario.usu_situacao
		, usg_situacao
		, CASE usg_situacao
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'
			ELSE ''
		  END AS usg_situacaoDescricao		
	FROM 
		SYS_Usuario WITH (NOLOCK)
	INNER JOIN SYS_UsuarioGrupo WITH(NOLOCK) 
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupo.usu_id	
	LEFT JOIN PES_Pessoa WITH(NOLOCK)
		ON SYS_Usuario.pes_id = PES_Pessoa.pes_id
		AND pes_situacao <> 3		
	WHERE
		usu_situacao <> 3		
		AND usg_situacao <> 3
		AND gru_id = @gru_id		
	ORDER BY
		usu_login
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_SELECTBY_ModuloVinculado]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 23/09/2010 10:14
-- Description:	Carrega todos os sistema que tenha algum módulo vinculado
-- ========================================================================
CREATE PROCEDURE  [dbo].[NEW_SYS_Sistema_SELECTBY_ModuloVinculado]
AS
BEGIN
	SELECT DISTINCT
		 SYS_Sistema.sis_id
		,SYS_Sistema.sis_nome
		, CASE SYS_Sistema.sis_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS sis_situacao
	FROM
		SYS_Sistema WITH (NOLOCK)
	INNER JOIN SYS_Modulo WITH (NOLOCK)
		ON SYS_Modulo.sis_id = SYS_Sistema.sis_id		
	WHERE
		sis_situacao <> 3
		AND mod_situacao <> 3
					
	ORDER BY
		sis_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_UPDATE]
		@ent_id uniqueidentifier
			, @uad_id uniqueidentifier 
			, @tua_id uniqueidentifier
			, @uad_codigo varchar(20)
			, @uad_nome varchar(200)
			, @uad_sigla varchar(50)
			, @uad_idSuperior uniqueidentifier
			, @uad_situacao tinyInt
			, @uad_dataCriacao dateTime
			, @uad_dataAlteracao dateTime
			, @uad_integridade Int
			, @uad_codigoIntegracao varchar(50) = NULL
			, @uad_codigoInep varchar(30) = NULL

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativa
	SET 		
		tua_id = @tua_id 
			, uad_codigo = @uad_codigo
			, uad_nome = @uad_nome
			, uad_sigla = @uad_sigla
			, uad_idSuperior = @uad_idSuperior
			, uad_situacao = @uad_situacao
			, uad_dataCriacao = @uad_situacao
			, uad_dataAlteracao = @uad_dataAlteracao
			, uad_integridade = @uad_integridade
			, uad_codigoIntegracao = @uad_codigoIntegracao
			, uad_codigoInep = @uad_codigoInep
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_SelectBy_unf_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 23/09/2010 14:34
-- Description:	utilizado na busca de cidades, retorna as cidades
--              que não foram excluídas logicamente de um determinado estado
--				filtradas por:
--					unf_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_SelectBy_unf_id]		
	@unf_id uniqueidentifier
AS
BEGIN
	SELECT 
		END_Cidade.cid_id
		,END_Cidade.unf_id
		,END_Cidade.pai_id
		,cid_nome
		,cid_ddd
		,unf_nome				    		
		,unf_sigla		
		,pai_nome	
		, CASE cid_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS cid_situacao
	FROM
		END_Cidade WITH (NOLOCK)
	LEFT JOIN
		END_UnidadeFederativa WITH (NOLOCK) on END_Cidade.unf_id = END_UnidadeFederativa.unf_id	AND unf_situacao <> 3	
	INNER JOIN
		END_Pais WITH (NOLOCK) on END_Cidade.pai_id = END_Pais.pai_id
	WHERE
		cid_situacao <> 3							
		AND pai_situacao <> 3
		AND END_Cidade.unf_id = @unf_id				
	ORDER BY
		pai_nome, isnull(unf_nome,''), cid_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
			, uad_id
			, tua_id
			, uad_codigo
			, uad_nome
			, uad_sigla
			, uad_idSuperior
			, uad_situacao
			, uad_dataCriacao
			, uad_dataAlteracao
			, uad_integridade
			, uad_codigoIntegracao
	FROM 
		SYS_UnidadeAdministrativa WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_SistemaEntidade_SelectBy_sis_id_ent_id_excluido]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 23/09/2010 15:46
-- Description:	utilizado na busca de entidades do sistema, retorna as
--              entidades do sistena mesmo as excluídas logiocamente
--				filtrados por:
--					sis_id, ent_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_SistemaEntidade_SelectBy_sis_id_ent_id_excluido]	
	@sis_id int
	, @ent_id uniqueidentifier
AS
BEGIN
	SELECT		
		ent_id
	FROM
		SYS_SistemaEntidade	WITH (NOLOCK)
	WHERE				
		sis_id = @sis_id
		AND ent_id = @ent_id
	ORDER BY
		ent_id
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_LOAD]
	@ent_id uniqueidentifier
	,@uad_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		ent_id
			, uad_id
			, tua_id
			, uad_codigo
			, uad_codigoInep
			, uad_nome
			, uad_sigla
			, uad_idSuperior
			, uad_situacao
			, uad_dataCriacao
			, uad_dataAlteracao
			, uad_integridade
			, uad_codigoIntegracao
 	FROM
 		SYS_UnidadeAdministrativa
	WHERE 
		ent_id = @ent_id
	and uad_id = @uad_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_UsuarioFalhaAutenticacao]'
GO
CREATE TABLE [dbo].[SYS_UsuarioFalhaAutenticacao]
(
[usu_id] [uniqueidentifier] NOT NULL,
[ufl_qtdeFalhas] [int] NOT NULL,
[ufl_dataUltimaTentativa] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_UsuarioFalhaAutenticação] on [dbo].[SYS_UsuarioFalhaAutenticacao]'
GO
ALTER TABLE [dbo].[SYS_UsuarioFalhaAutenticacao] ADD CONSTRAINT [PK_SYS_UsuarioFalhaAutenticação] PRIMARY KEY CLUSTERED  ([usu_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_InsereFalhaUsuario]'
GO
-- =============================================
-- Author:		Carla Frascareli
-- Create date: 16/04/2013
-- Description:	Insere um registro de falha de autenticação para o usuário, ou incrementa 1
--				no contador de erros caso o usuário já tenha errado, no intervalo de minutos informado.
--				Caso o último erro do usuário tenha sido depois desse intervalo, reinicia o contador pra 1.
--				Retorna o registro do usuário com a quantidade erros efetuada.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_InsereFalhaUsuario]
	@usu_id UNIQUEIDENTIFIER
	, @minutosIntervalo INT -- Quantidade de minutos que deve considerar como intervalo.
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE
		@ufl_qtdeFalhas INT
		, @ufl_dataUltimaTentativa DATETIME
		, @dataAgora DATETIME = GETDATE();
	
	SELECT
		@ufl_qtdeFalhas = Ufl.ufl_qtdeFalhas
		, @ufl_dataUltimaTentativa = Ufl.ufl_dataUltimaTentativa
	FROM SYS_UsuarioFalhaAutenticacao Ufl WITH(NOLOCK)
	WHERE
		Ufl.usu_id = @usu_id
	
	IF (@ufl_qtdeFalhas IS NULL)
	BEGIN
		-- Registro não existe, insere o primeiro.
		INSERT INTO SYS_UsuarioFalhaAutenticacao
		(usu_id, ufl_qtdeFalhas, ufl_dataUltimaTentativa)
		VALUES
		(@usu_id, 1, @dataAgora)
	END
	ELSE
	BEGIN
		IF (DATEDIFF(MINUTE, @ufl_dataUltimaTentativa, @dataAgora) < @minutosIntervalo)
		BEGIN
			-- O último erro foi há menos do intervalo de minutos,
			-- incrementa um no erro do usuário, e atualiza a data.
			UPDATE SYS_UsuarioFalhaAutenticacao
			SET
				ufl_qtdeFalhas = ISNULL(ufl_qtdeFalhas, 0) + 1
				, ufl_dataUltimaTentativa = @dataAgora
			WHERE
				usu_id = @usu_id
		END
		ELSE
		BEGIN
			-- Passou do intervalo de minutos do último erro do usuário, reinicia contador.
			UPDATE SYS_UsuarioFalhaAutenticacao
			SET
				ufl_qtdeFalhas = 1
				, ufl_dataUltimaTentativa = @dataAgora
			WHERE
				usu_id = @usu_id
		END
	END
	
	SELECT
		usu_id, ufl_qtdeFalhas, ufl_dataUltimaTentativa
	FROM SYS_UsuarioFalhaAutenticacao Ufl WITH(NOLOCK)
	WHERE
		Ufl.usu_id = @usu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 03/02/2010 13:15
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Unidade Administrativa. Filtrada por: 
--					ent_id, uad_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_Update_Situacao]	
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uad_situacao TINYINT
		,@uad_dataAlteracao DateTime
AS
BEGIN
	--Exclui logicamente os endereços da unidade administrativa	
	UPDATE SYS_UnidadeAdministrativaEndereco 
	SET 
		uae_situacao = @uad_situacao
		,uae_dataAlteracao = @uad_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id	
			
	--Exclui logicamente os contatos da unidade administrativa	
	UPDATE SYS_UnidadeAdministrativaContato 
	SET 
		uac_situacao = @uad_situacao
		,uac_dataAlteracao = @uad_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id				
		
	--Exclui logicamente a unidade administrativa
	UPDATE SYS_UnidadeAdministrativa 
	SET 
		uad_situacao = @uad_situacao
		,uad_dataAlteracao = @uad_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
				
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativaEndereco_Incluir]'
GO

CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativaEndereco_Incluir]
      @ent_id					UNIQUEIDENTIFIER
	, @uad_id					UNIQUEIDENTIFIER
	, @uae_id					UNIQUEIDENTIFIER = NULL
	, @end_id					UNIQUEIDENTIFIER
	, @uae_numero				VARCHAR (20)
	, @uae_complemento			VARCHAR (100)
	, @uae_situacao				TINYINT
	, @uae_dataCriacao			DATETIME
	, @uae_dataAlteracao		DATETIME
	, @uae_enderecoPrincipal	BIT = NULL
	, @uae_latitude				DECIMAL (15,10) = NULL
	, @uae_longitude			DECIMAL (15,10) = NULL
	, @desmarcarDemaisEnderecosPrincipais BIT = 0

AS
BEGIN
	
	-- 
	DECLARE @tabEnderecosUA	TABLE ( uae_id UNIQUEIDENTIFIER, uae_situacao INT, uae_enderecoPrincipal BIT )
	DECLARE @tabRetornoUAE	TABLE ( uae_id UNIQUEIDENTIFIER )
	
	-- 
	DECLARE @enderecoPrincipal BIT = 0

	
	-- Seleciona os endereços da unidade administrativa para validações
	INSERT INTO @tabEnderecosUA ( uae_id, uae_situacao, uae_enderecoPrincipal )
	SELECT 
		uae.uae_id, 
		uae.uae_situacao, 
		COALESCE(uae.uae_enderecoPrincipal, 0) AS uae_enderecoPrincipal
	FROM
		SYS_UnidadeAdministrativaEndereco uae WITH (NOLOCK) 
	WHERE
			uae.ent_id = @ent_id
		AND uae.uad_id = @uad_id



	-- Verifica se o parâmetro indicando como endereço principal foi informado
	IF (@uae_enderecoPrincipal IS NULL) BEGIN

		-- Verifica se a unidade administrativa já possui algum endereco ativo cadastrado como principal
		IF EXISTS (	SELECT e.uae_id FROM @tabEnderecosUA e WHERE e.uae_situacao = 1 AND e.uae_enderecoPrincipal = 1 ) BEGIN
			SET @enderecoPrincipal = 0
		
		-- Verifica se a unidade administrativa possui algum endereço ativo
		END IF EXISTS( SELECT e.uae_id FROM @tabEnderecosUA e WHERE e.uae_situacao = 1 ) BEGIN 
			SET @enderecoPrincipal = 0

		-- Se não existir endereço ativo, ou endereço ativo principal, indica que o endereço que está sendo incluido será o principal para a unidade
		END ELSE BEGIN
			SET @enderecoPrincipal = 1		
		
		END

	END ELSE BEGIN

		-- Se o parâmetro foi informado (True ou False), assume o valor do parâmetro para a inclusão do endereço
		SET @enderecoPrincipal = @uae_enderecoPrincipal
	END 


	-- Validações para setar os demais endereços da unidade administrativa como NÃO principal, verificando 
	-- se o endereço para inclusão está indicado como principal, e também se irá desmarcar os demais endereços
	IF (	COALESCE(@uae_enderecoPrincipal, 0) = 1
		AND COALESCE(@desmarcarDemaisEnderecosPrincipais, 0) = 1 ) BEGIN

		-- Confirma se existe algum endereço cadastrado para a unidade administrativa
		IF EXISTS ( SELECT	e.uae_id 
					FROM	@tabEnderecosUA e ) BEGIN
			
			-- Atualiza os registros, removendo a informação de endereço principal
			UPDATE 
				uae 
			SET 
				uae.uae_enderecoPrincipal = 0 
			FROM 
				SYS_UnidadeAdministrativaEndereco uae 
				INNER JOIN @tabEnderecosUA e ON ( e.uae_id = uae.uae_id )
		END 

	END
	ELSE BEGIN

		-- Confirma se existe algum endereço diferente de ativo cadastrado para a unidade administrativa
		IF EXISTS ( select e.uae_id from @tabEnderecosUA e WHERE e.uae_situacao <> 1) BEGIN

			-- Atualiza os registros, removendo a informação de endereço principal (somente dos endereços que não estão ativos)
			UPDATE 
				uae 
			SET 
				uae.uae_enderecoPrincipal = 0 
			FROM 
				SYS_UnidadeAdministrativaEndereco uae 
				INNER JOIN @tabEnderecosUA e ON ( e.uae_id = uae.uae_id ) 
			WHERE 
				e.uae_situacao <> 1
		END 

	END 


	-- Faz a inclusão do endereço
	INSERT INTO 
		SYS_UnidadeAdministrativaEndereco
		( 
			  ent_id 
			, uad_id 
			--, uae_id
			, end_id 
			, uae_numero 
			, uae_complemento 
			, uae_situacao 
			, uae_dataCriacao 
			, uae_dataAlteracao 
			, uae_enderecoPrincipal 
			, uae_latitude 
			, uae_longitude 
 		)
	OUTPUT inserted.uae_id INTO @tabRetornoUAE	-- Armazena os Id's que estão sendo cadastrados, para retornar no final da procedure
	VALUES
		( 
			  @ent_id 
			, @uad_id 
			--, @uae_id 
			, @end_id 
			, @uae_numero 
			, @uae_complemento 
			, @uae_situacao 
			, @uae_dataCriacao 
			, @uae_dataAlteracao 
			, @enderecoPrincipal	-- Validado na procedure se será o principal
			, @uae_latitude 
			, @uae_longitude 
 
		)


	-- Retorna os Id's do(s) endereço(s) cadastrado(s)
	SELECT 
		r.uae_id AS ID 
	FROM 
		@tabRetornoUAE r

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_INSERT]
		@ent_id uniqueidentifier
			, @tua_id uniqueidentifier
			, @uad_codigo varchar(20)
			, @uad_nome varchar(200)
			, @uad_sigla varchar(50)
			, @uad_idSuperior uniqueidentifier
			, @uad_situacao tinyInt
			, @uad_dataCriacao dateTime
			, @uad_dataAlteracao dateTime
			, @uad_integridade Int
			, @uad_codigoIntegracao varchar(50) = NULL
			, @uad_codigoInep varchar(30) = NULL

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_UnidadeAdministrativa
		( 
			ent_id
			, tua_id
			, uad_codigo
			, uad_nome
			, uad_sigla
			, uad_idSuperior
			, uad_situacao
			, uad_dataCriacao
			, uad_dataAlteracao
			, uad_integridade
			, uad_codigoIntegracao
			, uad_codigoInep
		)
	OUTPUT inserted.uad_id INTO @ID
	VALUES
		( 
			@ent_id
			, @tua_id
			, @uad_codigo
			, @uad_nome
			, @uad_sigla
			, @uad_idSuperior
			, @uad_situacao
			, @uad_dataCriacao
			, @uad_dataAlteracao
			, @uad_integridade
			, @uad_codigoIntegracao
			, @uad_codigoInep
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_ZeraFalhaUsuario]'
GO
-- =============================================
-- Author:		Carla Frascareli
-- Create date: 16/04/2013
-- Description:	Zera a quantidade de falhas que o usuário teve, para não exibir mais o captcha.
--				Utilizada após o usuário efetuar um login com sucesso.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_ZeraFalhaUsuario]
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE SYS_UsuarioFalhaAutenticacao
	SET
		ufl_qtdeFalhas = 0
	WHERE
		usu_id = @usu_id
	
	SELECT @@ROWCOUNT;
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 25/03/2010 10:03
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Usuarios. Filtrada por: 
--					usu_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_Update_Situacao]	
		@usu_id uniqueidentifier
		,@usu_situacao TINYINT
		,@usu_dataAlteracao DateTime
AS
BEGIN
	--Exclui logicamente os grupo do usuário
	UPDATE SYS_UsuarioGrupo 
	SET 
		usg_situacao = @usu_situacao		
	WHERE 
		usu_id = @usu_id
		
	--Exclui logicamente os usuários
	UPDATE SYS_Usuario
	SET 
		usu_situacao = @usu_situacao
		,usu_dataAlteracao = @usu_dataAlteracao
	WHERE 
		usu_id = @usu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_UnidadeAdministrativaEndereco_SelecionarEnderecos]'
GO

CREATE PROCEDURE [dbo].[MS_UnidadeAdministrativaEndereco_SelecionarEnderecos]	
	  @ent_id		UNIQUEIDENTIFIER
	, @uad_id		UNIQUEIDENTIFIER
	, @uae_id		UNIQUEIDENTIFIER = NULL
	, @consultaTop1 BIT = 0

AS
BEGIN
	
	DECLARE @qtdeRegistros INT = 99999

	-- Verifica se a consulta é top 1
	IF ( @consultaTop1 = 1 ) BEGIN
		SET @qtdeRegistros = 1
	END

	-- Faz a consulta dos endereços da unidade administrativa
	; WITH tmpUAE AS 
	(
		SELECT 
			  uae.ent_id
			, uae.uad_id
			, uae.uae_id
			, uae.end_id
			, uae.uae_numero
			, uae.uae_complemento
			, uae.uae_situacao
			, uae.uae_dataCriacao
			, uae.uae_dataAlteracao
			, COALESCE(uae.uae_enderecoPrincipal, 0) AS uae_enderecoPrincipal
			, uae.uae_latitude
			, uae.uae_longitude

		FROM
			SYS_UnidadeAdministrativaEndereco uae WITH (NOLOCK)		

		WHERE
				uae.ent_id = @ent_id
			AND uae.uad_id = @uad_id
			AND uae.uae_id = COALESCE(@uae_id, uae.uae_id)
			AND uae.uae_situacao <> 3
	)
	
	-- Retorna os registros ordenados, primeiramente pela data de alteração (mais recentes primeiro) e depois o(s) endereço(s) principal(is)
	SELECT 
		TOP (@qtdeRegistros)
		  tmp.ent_id
		, tmp.uad_id
		, tmp.uae_id
		, tmp.end_id
		, tmp.uae_numero
		, tmp.uae_complemento
		, tmp.uae_situacao
		, tmp.uae_dataCriacao
		, tmp.uae_dataAlteracao
		, tmp.uae_enderecoPrincipal
		, tmp.uae_latitude
		, tmp.uae_longitude
	FROM 
		tmpUAE tmp
	ORDER BY 
		  tmp.uae_enderecoPrincipal DESC
		, tmp.uae_dataAlteracao DESC
		

	--
	SELECT @@ROWCOUNT			

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_DELETE]
	@ent_id uniqueidentifier
	, @uad_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_UnidadeAdministrativa	
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_Load]'
GO
-- =============================================
-- Author:		Carla Frascareli
-- Create date: 16/04/2013
-- Description:	Retorna o registro de falha de autenticação do usuário.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioFalhaAutenticacao_Load]
	@usu_id UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		usu_id, ufl_qtdeFalhas, ufl_dataUltimaTentativa
	FROM SYS_UsuarioFalhaAutenticacao WITH(NOLOCK)
	WHERE
		usu_id = @usu_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_Update_Senha]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 21/05/2010 12:14
-- Description:	utilizado para realizar alteração da senha 
--				do usuário. Filtrada por: 
--					usu_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_Update_Senha]	
		@usu_id uniqueidentifier
		,@usu_senha varchar(256)
		,@usu_criptografia Tinyint
		,@usu_dataAlteracao DateTime
		,@usu_dataAlteracaoSenha DateTime = NULL
AS
BEGIN
	SET @usu_dataAlteracaoSenha = CASE WHEN @usu_dataAlteracaoSenha IS NULL THEN GETDATE() ELSE @usu_dataAlteracaoSenha END
	
	UPDATE SYS_Usuario
	SET 
		usu_senha = @usu_senha
		, usu_criptografia = @usu_criptografia
		, usu_dataAlteracao = @usu_dataAlteracao
		, usu_dataAlteracaoSenha = @usu_dataAlteracaoSenha
	WHERE 
		usu_id = @usu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END

IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_EntidadeContato]'
GO
CREATE TABLE [dbo].[SYS_EntidadeContato]
(
[ent_id] [uniqueidentifier] NOT NULL,
[enc_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Entid__enc_i__30F848ED] DEFAULT (newsequentialid()),
[tmc_id] [uniqueidentifier] NOT NULL,
[enc_contato] [varchar] (200) NOT NULL,
[enc_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_EntidadeContato_enc_situacao] DEFAULT ((1)),
[enc_dataCriacao] [datetime] NOT NULL CONSTRAINT [DF_SYS_EntidadeContato_enc_dataCriacao] DEFAULT (getdate()),
[enc_dataAlteracao] [datetime] NOT NULL CONSTRAINT [DF_SYS_EntidadeContato_enc_dataAlteracao] DEFAULT (getdate())
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_EntidadeContato] on [dbo].[SYS_EntidadeContato]'
GO
ALTER TABLE [dbo].[SYS_EntidadeContato] ADD CONSTRAINT [PK_SYS_EntidadeContato] PRIMARY KEY CLUSTERED  ([ent_id], [enc_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeContato_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeContato_UPDATE]
		@ent_id uniqueidentifier
		,@enc_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@enc_contato VarChar (200)
		,@enc_situacao TinyInt
		,@enc_dataCriacao DateTime
		,@enc_dataAlteracao DateTime

AS
BEGIN
	UPDATE SYS_EntidadeContato
	SET 
		tmc_id = @tmc_id
		,enc_contato = @enc_contato
		,enc_situacao = @enc_situacao
		,enc_dataCriacao = @enc_dataCriacao
		,enc_dataAlteracao = @enc_dataAlteracao
	WHERE 
		ent_id = @ent_id
	and enc_id = @enc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_UPDATE]'
GO
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_UPDATE]
		@usu_id uniqueidentifier
		,@usu_login VarChar (500)
		,@usu_email VarChar (500)
		,@usu_criptografia TinyInt		
		,@usu_situacao TinyInt
		,@usu_dataAlteracao DateTime
		,@pes_id uniqueidentifier
		,@usu_dominio varchar(100)
		,@ent_id UNIQUEIDENTIFIER
		,@usu_integracaoAD TINYINT = 0 -- atribuído valor default para funcionar com o sistema em uma versão anterior a 1.22.0.0
		, @usu_integracaoExterna TinyInt = NULL
AS
BEGIN
	--	
	DECLARE 
		@usu_integracaoExternaTmp TINYINT

	-- Valida se o parâmetro @usu_integracaoExterna é nulo, para colocar o valor default do campo
	IF (@usu_integracaoExterna IS NULL) BEGIN
		
		SET @usu_integracaoExternaTmp = (	SELECT	u.usu_integracaoExterna
											FROM	SYS_Usuario u WITH (NOLOCK) 
											WHERE	u.usu_id = @usu_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @usu_integracaoExternaTmp = @usu_integracaoExterna
	END 

	IF (@usu_integracaoAD = 1) BEGIN

		UPDATE 
			SYS_Usuario
		SET 
			usu_login = @usu_login
			,usu_email = @usu_email	
			,usu_criptografia = @usu_criptografia 	
			,usu_situacao = @usu_situacao
			,usu_dataAlteracao = @usu_dataAlteracao
			,pes_id = @pes_id
			,usu_dominio = @usu_dominio
			,ent_id = @ent_id
			,usu_integracaoAD = @usu_integracaoAD
			,usu_senha = NULL
			,usu_integracaoExterna = @usu_integracaoExternaTmp
		WHERE 
			usu_id = @usu_id
		
		RETURN ISNULL(@@ROWCOUNT,-1)
	
	END ELSE BEGIN

		UPDATE 
			SYS_Usuario
		SET 
			usu_login = @usu_login
			,usu_email = @usu_email	
			,usu_criptografia = @usu_criptografia 	
			,usu_situacao = @usu_situacao
			,usu_dataAlteracao = @usu_dataAlteracao
			,pes_id = @pes_id
			,usu_dominio = @usu_dominio
			,ent_id = @ent_id
			,usu_integracaoAD = @usu_integracaoAD
			,usu_integracaoExterna = @usu_integracaoExternaTmp 
		WHERE 
			usu_id = @usu_id	
			
		RETURN ISNULL(@@ROWCOUNT,-1)
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_LOAD]
	@pes_id UniqueIdentifier
	, @ctc_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 pes_id  
		, ctc_id 
		, ctc_tipo 
		, ctc_numeroTermo 
		, ctc_folha 
		, ctc_livro 
		, ctc_dataEmissao 
		, ctc_nomeCartorio 
		, cid_idCartorio 
		, unf_idCartorio 
		, ctc_distritoCartorio 
		, ctc_situacao 
		, ctc_dataCriacao 
		, ctc_dataAlteracao 
		, ctc_matricula
		, ctc_gemeo 
		, ctc_modeloNovo 

 	FROM
 		PES_CertidaoCivil
	WHERE 
		pes_id = @pes_id
		AND ctc_id = @ctc_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeContato_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeContato_SELECT]
	
AS
BEGIN
	SELECT 
		ent_id
		,enc_id
		,tmc_id
		,enc_contato
		,enc_situacao
		,enc_dataCriacao
		,enc_dataAlteracao
		
	FROM 
		SYS_EntidadeContato WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_UsuarioAPI]'
GO
CREATE TABLE [dbo].[LOG_UsuarioAPI]
(
[lua_id] [bigint] NOT NULL IDENTITY(1, 1),
[usu_id] [uniqueidentifier] NOT NULL,
[uap_id] [int] NOT NULL,
[lua_acao] [tinyint] NOT NULL,
[lua_dataHora] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_LOG_UsuarioAPI] on [dbo].[LOG_UsuarioAPI]'
GO
ALTER TABLE [dbo].[LOG_UsuarioAPI] ADD CONSTRAINT [PK_LOG_UsuarioAPI] PRIMARY KEY CLUSTERED  ([lua_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAPI_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAPI_LOAD]
	@lua_id BigInt
	
AS
BEGIN
	SELECT	Top 1
		 lua_id  
		, usu_id 
		, uap_id 
		, lua_acao 
		, lua_dataHora 

 	FROM
 		LOG_UsuarioAPI
	WHERE 
		lua_id = @lua_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_SelectBy_end_cep_end_logradouro]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 19/10/2010 10:49
-- Description:	utilizado na busca incremental de endereços
--				retorna os endereços
--              que não foram excluídos logicamente,
--				filtrados por:
--				end_cep, end_logradouro
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_SelectBy_end_cep_end_logradouro]	
	@end_cep VARCHAR(8)
	,@end_logradouro VARCHAR(200)
AS
BEGIN
	IF (@end_cep IS NULL)
	BEGIN
		SELECT TOP 10  	
			end_id				
			, end_cep
			, end_logradouro
			, end_logradouro + ' - ' + ISNULL(end_bairro,'') + ' - ' + cid_nome + '/' + unf_sigla AS end_endereco
			, ISNULL(end_distrito,'') AS end_distrito
			, ISNULL(end_zona,'0') AS end_zona
			, ISNULL(end_bairro,'') AS end_bairro 
			, cid_nome
			, END_Endereco.cid_id
		FROM
			END_Endereco WITH (NOLOCK)
		INNER JOIN
			END_Cidade WITH (NOLOCK) on END_endereco.cid_id = END_Cidade.cid_id
		LEFT JOIN
			END_UnidadeFederativa WITH (NOLOCK) on END_Cidade.unf_id = END_UnidadeFederativa.unf_id	AND unf_situacao <> 3	
		INNER JOIN
			END_Pais WITH (NOLOCK) on END_Cidade.pai_id = END_Pais.pai_id
		WHERE
			end_situacao <> 3							
			AND cid_situacao <> 3
    		AND pai_situacao <> 3
			AND (@end_logradouro is null or end_logradouro LIKE '%' + @end_logradouro + '%')									
		ORDER BY
			end_logradouro	  
	END
	ELSE
	BEGIN
		SELECT	
			end_id				
			, end_cep
			, end_logradouro
			, end_logradouro + ' - ' + ISNULL(end_bairro,'') + ' - ' + cid_nome + '/' + unf_sigla AS end_endereco
			, ISNULL(end_distrito,'') AS end_distrito
			, ISNULL(end_zona,'0') AS end_zona
			, ISNULL(end_bairro,'') AS end_bairro 
			, cid_nome
			, END_Endereco.cid_id
		FROM
			END_Endereco WITH (NOLOCK)
		INNER JOIN
			END_Cidade WITH (NOLOCK) on END_endereco.cid_id = END_Cidade.cid_id
		LEFT JOIN
			END_UnidadeFederativa WITH (NOLOCK) on END_Cidade.unf_id = END_UnidadeFederativa.unf_id	AND unf_situacao <> 3	
		INNER JOIN
			END_Pais WITH (NOLOCK) on END_Cidade.pai_id = END_Pais.pai_id
		WHERE
			end_situacao <> 3
			AND cid_situacao <> 3
			AND pai_situacao <> 3
			AND (@end_cep is null or end_cep = @end_cep)				
			AND (@end_logradouro is null or end_logradouro LIKE '%' + @end_logradouro + '%')									
		ORDER BY
			end_logradouro	 	
	END
		
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_Load_PermissaoModulo_PorUrl]'
GO
-- ========================================================================
-- Author:		Carla Frascareli
-- Create date: 06/06/2013
-- Description:	Retorna os dados da permissão (consultar, inserir, alterar e excluir)
--				para o grupo informado, no módulo do sistema ligado à url informada.
--				Traz também os campos da tabela SYS_Modulo.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_Load_PermissaoModulo_PorUrl]
	@sis_id INT
	, @gru_id UNIQUEIDENTIFIER
	, @msm_url VARCHAR(500) 
AS
BEGIN
	SELECT
		Per.gru_id
		, Per.sis_id
		, Per.mod_id
		, Per.grp_consultar
		, Per.grp_inserir
		, Per.grp_alterar
		, Per.grp_excluir
		, Modulo.mod_nome
		, Modulo.mod_descricao
		, Modulo.mod_idPai
		, Modulo.mod_auditoria
		, Modulo.mod_situacao
		, Modulo.mod_dataCriacao
		, Modulo.mod_dataAlteracao
		, Msm.msm_url
	FROM SYS_Modulo Modulo WITH(NOLOCK)
	INNER JOIN SYS_GrupoPermissao Per WITH(NOLOCK)
		ON Modulo.sis_id = Per.sis_id
		AND Modulo.mod_id = Per.mod_id
	INNER JOIN SYS_ModuloSiteMap Msm WITH(NOLOCK)
		ON Msm.sis_id = Modulo.sis_id
		AND Msm.mod_id = Modulo.mod_id
	WHERE
		Modulo.mod_situacao <> 3
		AND Per.gru_id = @gru_id
		AND Per.sis_id = @sis_id
		AND Msm.msm_url = @msm_url
		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_usu_login_email_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 24/03/2010 17:36
-- Description:	utilizado no cadastro de usuarios,
--              para saber se o login, email ou pessoa está cadastrada
--				filtrados por:
--					usuario (diferente do parametro), 					 
--                  login, email, pessoa, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_usu_login_email_pes_id]	
	@usu_id uniqueidentifier
	, @usu_login VARCHAR(500)
	, @usu_dominio VARCHAR(100)
	, @usu_email VARCHAR(500)
	, @pes_id uniqueidentifier
	, @usu_situacao TINYINT		
AS
BEGIN
	SELECT 
		usu_id
	FROM
		SYS_Usuario WITH (NOLOCK)		
	WHERE
		usu_situacao <> 3
		AND (@usu_id is null or usu_id <> @usu_id)				
		
		--AND (((@usu_dominio is null) AND (usu_dominio is null) AND (@usu_login is null or usu_login = @usu_login))
		--	OR ((@usu_dominio is null) AND (usu_dominio is not null) AND (@usu_login is null or @usu_login = usu_dominio+'\'+usu_login))
		--	OR ((@usu_dominio is not null) AND (usu_dominio is not null) AND (@usu_login is null or @usu_dominio+'\'+@usu_login = usu_dominio+'\'+usu_login)))
		AND (@usu_login is null or usu_login = @usu_login)
		AND ((@usu_dominio is null AND usu_dominio is null) or (@usu_dominio = usu_dominio))
		AND (@usu_email is null or usu_email = @usu_email)					
		AND (@pes_id is null or pes_id = @pes_id)					
		AND (@usu_situacao is null or usu_situacao = @usu_situacao)						
	ORDER BY
		usu_login
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_INSERT]
	@pes_id UniqueIdentifier
	, @ctc_tipo TinyInt
	, @ctc_numeroTermo VarChar (50)
	, @ctc_folha VarChar (20)
	, @ctc_livro VarChar (20)
	, @ctc_dataEmissao DateTime
	, @ctc_nomeCartorio VarChar (200)
	, @cid_idCartorio UniqueIdentifier
	, @unf_idCartorio UniqueIdentifier
	, @ctc_distritoCartorio VarChar (100)
	, @ctc_situacao TinyInt
	, @ctc_dataCriacao DateTime
	, @ctc_dataAlteracao DateTime
	, @ctc_matricula VarChar (32)
	, @ctc_gemeo Bit = 0
	, @ctc_modeloNovo Bit = 0

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_CertidaoCivil
		( 
			pes_id 
			, ctc_tipo 
			, ctc_numeroTermo 
			, ctc_folha 
			, ctc_livro 
			, ctc_dataEmissao 
			, ctc_nomeCartorio 
			, cid_idCartorio 
			, unf_idCartorio 
			, ctc_distritoCartorio 
			, ctc_situacao 
			, ctc_dataCriacao 
			, ctc_dataAlteracao 
			, ctc_matricula
			, ctc_gemeo 
			, ctc_modeloNovo 
 
		)
	OUTPUT inserted.ctc_id INTO @ID
	VALUES
		( 
			@pes_id 
			, @ctc_tipo 
			, @ctc_numeroTermo 
			, @ctc_folha 
			, @ctc_livro 
			, @ctc_dataEmissao 
			, @ctc_nomeCartorio 
			, @cid_idCartorio 
			, @unf_idCartorio 
			, @ctc_distritoCartorio 
			, @ctc_situacao 
			, @ctc_dataCriacao 
			, @ctc_dataAlteracao 
			, @ctc_matricula 
			, @ctc_gemeo 
			, @ctc_modeloNovo
		)
		
		SELECT ID FROM @ID

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeContato_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeContato_LOAD]
		@ent_id uniqueidentifier
		,@enc_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		ent_id
		,enc_id
		,tmc_id
		,enc_contato
		,enc_situacao
		,enc_dataCriacao
		,enc_dataAlteracao
 	FROM
 		SYS_EntidadeContato
	WHERE 
		ent_id = @ent_id
	and enc_id = @enc_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAPI_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAPI_INSERT]
	@usu_id UniqueIdentifier
	, @uap_id Int
	, @lua_acao TinyInt
	, @lua_dataHora DateTime

AS
BEGIN
	INSERT INTO 
		LOG_UsuarioAPI
		( 
			usu_id 
			, uap_id 
			, lua_acao 
			, lua_dataHora 
 
		)
	VALUES
		( 
			@usu_id 
			, @uap_id 
			, @lua_acao 
			, @lua_dataHora 
 
		)
		
		SELECT cast(ISNULL(SCOPE_IDENTITY(),-1) as bigint )

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoEscolaridade_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoEscolaridade_UPDATE]
	@tes_id uniqueidentifier
	, @tes_nome VARCHAR (100)
	, @tes_ordem INT
	, @tes_situacao TINYINT
	, @tes_dataCriacao DATETIME
	, @tes_dataAlteracao DATETIME
	, @tes_integridade INT

AS
BEGIN
	UPDATE PES_TipoEscolaridade 
	SET 
		tes_nome = @tes_nome 
		, tes_ordem = @tes_ordem 
		, tes_situacao = @tes_situacao 
		, tes_dataCriacao = @tes_dataCriacao 
		, tes_dataAlteracao = @tes_dataAlteracao 
		, tes_integridade = @tes_integridade 

	WHERE 
		tes_id = @tes_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_Select_Modulos_Urls_Help]'
GO
-- ========================================================================
-- Author:		Carla Frascareli
-- Create date: 07/06/2013
-- Description:	Retorna as urls do help, as urls do sitemap e o nome dos módulos
--				cadastrados para o sistema informado.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_Select_Modulos_Urls_Help]
	@sis_id INT
AS
BEGIN
	SELECT
		Modulo.sis_id
		, Modulo.mod_id
		, Modulo.mod_nome
		, Modulo.mod_idPai
		, Modulo.mod_situacao
		, Modulo.mod_dataCriacao
		, Modulo.mod_dataAlteracao
		, Msm.msm_id
		, Msm.msm_nome
		, Msm.msm_descricao
		, Msm.msm_url
		, Msm.msm_urlHelp
	FROM SYS_Modulo Modulo WITH(NOLOCK)
	INNER JOIN SYS_ModuloSiteMap Msm WITH(NOLOCK)
		ON Msm.sis_id = Modulo.sis_id
		AND Msm.mod_id = Modulo.mod_id
	WHERE
		Modulo.mod_situacao <> 3
		AND Modulo.sis_id = @sis_id
		
	SELECT @@ROWCOUNT
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_Pesquisa]'
GO
-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/01/2010 11:20
-- Description:	Consulta de usuário filtrado por login(LIKE%), E-mail(LIKE%)
--				Bloqueado (IS NULL-Todos,1-Ativo e Padrão Sistema,2-Bloqueado e 
--				Senha expirada) e Nome da Pessoa conforme fluxo principal
--				do documento de especificação tópico 4.4.5 Gerenciar Usuários.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_Pesquisa]
	@ent_id UNIQUEIDENTIFIER
	, @usu_login VARCHAR(500)
	, @usu_email VARCHAR(500)
	, @usu_bloqueado TINYINT
	, @pes_nome VARCHAR(200)
WITH RECOMPILE
AS
BEGIN
	SET @usu_login = '%' + @usu_login + '%';
	SET @usu_email = '%' + @usu_email + '%';
	SET @pes_nome = '%' + @pes_nome + '%';

	SELECT
		usu_id
		, CASE 
			WHEN usu_dominio is NULL THEN usu_login
			ELSE usu_dominio+'\'+usu_login
		  END AS usu_login
		, usu_email
		, pes_nome
		, usu_situacao
		, CASE usu_situacao 
			WHEN 1 THEN 'Ativo'
			WHEN 2 THEN 'Bloqueado'
			WHEN 4 THEN 'Padrão do Sistema'
			WHEN 5 THEN 'Senha Expirada'
		  END AS usu_situacaoNome
		, ent_nomeFantasia
		, ent_razaoSocial
		, SYS_Usuario.ent_id
		, usu_dominio
		, usu_integracaoAD
		, usu_integracaoExterna
	FROM SYS_Usuario WITH(NOLOCK)
	INNER JOIN SYS_Entidade WITH(NOLOCK)
		ON SYS_Usuario.ent_id = SYS_Entidade.ent_id		
	LEFT JOIN PES_Pessoa WITH(NOLOCK)
		ON SYS_Usuario.pes_id = PES_Pessoa.pes_id
		AND pes_situacao <> 3
	WHERE
		(ISNULL(usu_login, '') LIKE COALESCE(@usu_login, usu_login, '%'))
		AND	(SYS_Usuario.ent_id = ISNULL(@ent_id, SYS_Usuario.ent_id))
		AND (ISNULL(usu_email, '') LIKE COALESCE(@usu_email, usu_email, '%'))
		AND (usu_situacao = ISNULL(@usu_bloqueado, usu_situacao))
		AND usu_situacao <> 3
		AND ent_situacao <> 3
		AND ((@pes_nome IS NULL) 
		      OR ((pes_nome LIKE @pes_nome) 
		          OR (pes_nome_abreviado LIKE @pes_nome))
		    )
	ORDER BY	
		ent_razaoSocial, pes_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PES_PessoaDeficiencia]'
GO
CREATE TABLE [dbo].[PES_PessoaDeficiencia]
(
[pes_id] [uniqueidentifier] NOT NULL,
[tde_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PES_PessoaDeficiencia] on [dbo].[PES_PessoaDeficiencia]'
GO
ALTER TABLE [dbo].[PES_PessoaDeficiencia] ADD CONSTRAINT [PK_PES_PessoaDeficiencia] PRIMARY KEY CLUSTERED  ([pes_id], [tde_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDeficiencia_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaDeficiencia_INSERT]
	@pes_id UniqueIdentifier
	, @tde_id UniqueIdentifier

AS
BEGIN
	INSERT INTO 
		PES_PessoaDeficiencia
		( 
			pes_id 
			, tde_id 
 
		)
	VALUES
		( 
			@pes_id 
			, @tde_id 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeContato_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeContato_INSERT]
		@ent_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@enc_contato VarChar (200)
		,@enc_situacao TinyInt
		,@enc_dataCriacao DateTime
		,@enc_dataAlteracao DateTime

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_EntidadeContato
		( 
			ent_id
			,tmc_id
			,enc_contato
			,enc_situacao
			,enc_dataCriacao
			,enc_dataAlteracao
		)
	OUTPUT inserted.enc_id INTO @ID
	VALUES
		( 
			@ent_id
			,@tmc_id
			,@enc_contato
			,@enc_situacao
			,@enc_dataCriacao
			,@enc_dataAlteracao
		)
	SELECT ID FROM @ID		
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/01/2011 09:30
-- Description:	Altera o módulo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_UPDATE]
	@sis_id INT
	, @mod_id INT
	, @mod_nome varchar(50)
	, @mod_descricao text
	, @mod_idPai INT
	, @mod_auditoria BIT
	, @mod_situacao TINYINT	
	, @mod_dataAlteracao DATETIME	
AS
BEGIN
	UPDATE SYS_Modulo
	SET
		mod_nome = @mod_nome
		, mod_descricao = @mod_descricao
		, mod_idPai = @mod_idPai
		, mod_auditoria = @mod_auditoria 
		, mod_situacao = @mod_situacao 		
		, mod_dataAlteracao = @mod_dataAlteracao 
	WHERE
		sis_id = @sis_id
		AND mod_id = @mod_id
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAPI_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAPI_UPDATE]
	@lua_id BIGINT
	, @usu_id UNIQUEIDENTIFIER
	, @uap_id INT
	, @lua_acao TINYINT
	, @lua_dataHora DATETIME

AS
BEGIN
	UPDATE LOG_UsuarioAPI 
	SET 
		usu_id = @usu_id 
		, uap_id = @uap_id 
		, lua_acao = @lua_acao 
		, lua_dataHora = @lua_dataHora 

	WHERE 
		lua_id = @lua_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoEscolaridade_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoEscolaridade_SELECT]
	
AS
BEGIN
	SELECT 
		tes_id
		,tes_nome
		,tes_ordem
		,tes_situacao
		,tes_dataCriacao
		,tes_dataAlteracao
		,tes_integridade

	FROM 
		PES_TipoEscolaridade WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_pes_id]'
GO
-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/01/2010 11:20
-- Description:	Consulta de usuário filtrado por pes_id
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_pes_id]
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT
		usu_id
	FROM
		SYS_Usuario WITH(NOLOCK)		
	WHERE
		usu_situacao <> 3
		AND ((@pes_id IS NULL) OR (pes_id = @pes_id))		
		
	SELECT @@ROWCOUNT 		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_UPDATE]
	  @pes_id UNIQUEIDENTIFIER
	, @ctc_id UNIQUEIDENTIFIER
	, @ctc_tipo TINYINT
	, @ctc_numeroTermo VARCHAR (50)
	, @ctc_folha VARCHAR (20)
	, @ctc_livro VARCHAR (20)
	, @ctc_dataEmissao DATETIME
	, @ctc_nomeCartorio VARCHAR (200)
	, @cid_idCartorio UNIQUEIDENTIFIER
	, @unf_idCartorio UNIQUEIDENTIFIER
	, @ctc_distritoCartorio VARCHAR (100)
	, @ctc_situacao TINYINT
	, @ctc_dataCriacao DATETIME
	, @ctc_dataAlteracao DATETIME
	, @ctc_matricula VARCHAR (32)
	, @ctc_gemeo BIT = NULL
	, @ctc_modeloNovo BIT = NULL

AS
BEGIN

	DECLARE 
		  @ctc_gemeoTmp BIT = 0
		, @ctc_modeloNovoTmp BIT = 0

	-- 
	IF(@ctc_gemeo IS NULL) BEGIN
		SET @ctc_gemeoTmp = (	SELECT	ISNULL(c.ctc_gemeo, 0)
								FROM	PES_CertidaoCivil c WITH (NOLOCK) 
								WHERE	pes_id = @pes_id AND ctc_id = @ctc_id )
	END ELSE BEGIN
		SET @ctc_gemeoTmp = @ctc_gemeo
	END

	-- 
	IF(@ctc_modeloNovo IS NULL ) BEGIN
		SET @ctc_modeloNovoTmp = (	SELECT	ISNULL(c.ctc_modeloNovo, 0)
									FROM	PES_CertidaoCivil c WITH (NOLOCK) 
									WHERE	pes_id = @pes_id AND ctc_id = @ctc_id )
	END BEGIN
		SET @ctc_modeloNovoTmp = @ctc_modeloNovo
	END


	UPDATE 
		PES_CertidaoCivil 
	
	SET 
		  ctc_tipo = @ctc_tipo 
		, ctc_numeroTermo = @ctc_numeroTermo 
		, ctc_folha = @ctc_folha 
		, ctc_livro = @ctc_livro 
		, ctc_dataEmissao = @ctc_dataEmissao 
		, ctc_nomeCartorio = @ctc_nomeCartorio 
		, cid_idCartorio = @cid_idCartorio 
		, unf_idCartorio = @unf_idCartorio 
		, ctc_distritoCartorio = @ctc_distritoCartorio 
		, ctc_situacao = @ctc_situacao 
		, ctc_dataCriacao = @ctc_dataCriacao 
		, ctc_dataAlteracao = @ctc_dataAlteracao 
		, ctc_matricula = @ctc_matricula
		, ctc_gemeo = @ctc_gemeoTmp
		, ctc_modeloNovo = @ctc_modeloNovoTmp

	WHERE 
		    pes_id = @pes_id 
		AND ctc_id = @ctc_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_EntidadeContato_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_EntidadeContato_DELETE]
		@ent_id uniqueidentifier
		,@enc_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		SYS_EntidadeContato
	WHERE 
		ent_id = @ent_id
	and enc_id = @enc_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAPI_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAPI_DELETE]
	@lua_id BIGINT

AS
BEGIN
	DELETE FROM 
		LOG_UsuarioAPI 
	WHERE 
		lua_id = @lua_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_Update_Cidade]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 29/04/2010 11:36
-- Description:	utilizado para realizar alteração do campo de cidade 
--				(1 – Ativo ; 3 – Excluído) referente a endereço. 
--				Quando for alterado a cidade de algum endereço com final "000"
--              Filtrada por: end_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_Update_Cidade]	
	@end_cep VARCHAR(8)
	,@cid_id uniqueidentifier	
	,@end_dataAlteracao DateTime	
AS
BEGIN
	UPDATE END_Endereco
	SET 
		end_dataAlteracao = @end_dataAlteracao
		,cid_id = @cid_id
	WHERE 
		end_cep = @end_cep
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoEscolaridade_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoEscolaridade_LOAD]
	@tes_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		 tes_id  
		, tes_nome 
		, tes_ordem 
		, tes_situacao 
		, tes_dataCriacao 
		, tes_dataAlteracao 
		, tes_integridade 

 	FROM
 		PES_TipoEscolaridade
	WHERE 
		tes_id = @tes_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id_loginProvider]'
GO
 

Create PROCEDURE [dbo].[NEW_STP_SYS_UsuarioLoginProvider_SelectBy_usu_id_loginProvider]
       @usu_id UNIQUEIDENTIFIER,
	   @LoginProvider VARCHAR(128)
AS
BEGIN
      SELECT 
			LoginProvider, ProviderKey, ULP.usu_id, Username
      FROM

            SYS_UsuarioLoginProvider ULP with(nolock)

             INNER JOIN SYS_Usuario U ON ULP.usu_id = U.usu_id

       WHERE
             ULP.usu_id = @usu_id AND LoginProvider = @LoginProvider
                                 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:14
-- Description:	Seleciona o valor do campo integridade da tabela de usuário
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_Select_Integridade]
		@usu_id uniqueidentifier
AS
BEGIN
	SELECT 			
		usu_integridade
	FROM
		SYS_Usuario WITH (NOLOCK)
	WHERE 
		usu_id = @usu_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_DELETE]
	@pes_id UNIQUEIDENTIFIER
	, @ctc_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		PES_CertidaoCivil 
	WHERE 
		pes_id = @pes_id 
		AND ctc_id = @ctc_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Cidade_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Cidade_UPDATE]
		@cid_id uniqueidentifier
		,@pai_id uniqueidentifier
		,@unf_id uniqueidentifier
		,@cid_nome VarChar (200)
		,@cid_ddd VarChar(3)
		,@cid_situacao TinyInt
		,@cid_integridade int

AS
BEGIN
	UPDATE END_Cidade
	SET 		
		pai_id = @pai_id
		,unf_id = @unf_id
		,cid_nome = @cid_nome
		,cid_ddd = @cid_ddd
		,cid_situacao = @cid_situacao
		,cid_integridade = @cid_integridade
	WHERE 
		cid_id = @cid_id
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_SELECTBY_sis_caminhoLogout]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 12/01/2011 11:11
-- Description:	Carrega os dados do sistema através do caminho de logout
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Sistema_SELECTBY_sis_caminhoLogout]
	@sis_caminhoLogout VARCHAR(2000)
AS
BEGIN
	SELECT TOP 1
		*
	FROM
		SYS_Sistema WITH (NOLOCK)		
	WHERE
		sis_situacao <> 3
		AND sis_caminhoLogout = @sis_caminhoLogout
						
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAPI_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAPI_SELECT]
	
AS
BEGIN
	SELECT 
		lua_id
		,usu_id
		,uap_id
		,lua_acao
		,lua_dataHora

	FROM 
		LOG_UsuarioAPI WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoEscolaridade_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoEscolaridade_INSERT]
	@tes_nome VarChar (100)
	, @tes_ordem Int
	, @tes_situacao TinyInt
	, @tes_dataCriacao DateTime
	, @tes_dataAlteracao DateTime
	, @tes_integridade Int

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_TipoEscolaridade
		( 
			tes_nome 
			, tes_ordem 
			, tes_situacao 
			, tes_dataCriacao 
			, tes_dataAlteracao 
			, tes_integridade 
 
		)
	OUTPUT inserted.tes_id INTO @ID
	VALUES
		( 
			@tes_nome 
			, @tes_ordem 
			, @tes_situacao 
			, @tes_dataCriacao 
			, @tes_dataAlteracao 
			, @tes_integridade 
 
		)
	SELECT ID FROM @ID		
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDeficiencia_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaDeficiencia_DELETE]
	@pes_id UNIQUEIDENTIFIER
	, @tde_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		PES_PessoaDeficiencia 
	WHERE 
		pes_id = @pes_id 
		AND tde_id = @tde_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Cidade_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Cidade_SELECT]
	
AS
BEGIN
	SELECT 
		cid_id
		,pai_id
		,unf_id
		,cid_nome
		,cid_ddd
		,cid_situacao
		,cid_integridade
	FROM 
		END_Cidade WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Sistema_SELECTBY_sis_caminho]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 12/01/2011 11:18
-- Description:	Carrega os dados do sistema através do caminho de login
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Sistema_SELECTBY_sis_caminho]
	@sis_caminho VARCHAR(2000)
AS
BEGIN
	SELECT TOP 1
		*
	FROM
		SYS_Sistema WITH (NOLOCK)		
	WHERE
		sis_situacao <> 3
		AND sis_caminho = @sis_caminho
						
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_UsuarioAPI_SelecionaPorUsername]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 30/06/2014
-- Description:	Seleciona um usuário da API pelo nome.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_UsuarioAPI_SelecionaPorUsername] 
	@uap_username VARCHAR(100)
AS
BEGIN
	SELECT TOP 1
		uap.uap_id,
		uap.uap_username,
		uap.uap_password,
		uap.uap_situacao,
		uap.uap_dataCriacao,
		uap.uap_dataAlteracao
	FROM
		CFG_UsuarioAPI uap WITH(NOLOCK)
	WHERE
		LTRIM(RTRIM(uap.uap_username)) = LTRIM(RTRIM(@uap_username))
		AND uap.uap_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoEscolaridade_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoEscolaridade_DELETE]
	@tes_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		PES_TipoEscolaridade 
	WHERE 
		tes_id = @tes_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:17
-- Description:	Incrementa uma unidade no campo integridade da tabela de usuario
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_INCREMENTA_INTEGRIDADE]
		@usu_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_Usuario
	SET 
		usu_integridade = usu_integridade + 1
	WHERE 
		usu_id = @usu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,ctc_id
		,ctc_tipo
		,ctc_numeroTermo
		,ctc_folha
		,ctc_livro
		,ctc_dataEmissao
		,ctc_nomeCartorio
		,cid_idCartorio
		,unf_idCartorio
		,ctc_distritoCartorio
		,ctc_situacao
		,ctc_dataCriacao
		,ctc_dataAlteracao
		,ctc_matricula
		,ctc_gemeo
		,ctc_modeloNovo

	FROM 
		PES_CertidaoCivil WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Cidade_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_END_Cidade_LOAD]
	@cid_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		cid_id
		,pai_id
		,unf_id
		,cid_nome
		,cid_ddd
		,cid_situacao
		,cid_integridade
 	FROM
 		END_Cidade
	WHERE 
		cid_id = @cid_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_mod_idPai]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/01/2011 11:40
-- Description:	Select para retorna a busca de homepage dos módulos
--				filtrado por sistema e módulo pai
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_mod_idPai]
	@sis_id INT
	, @mod_idPai INT
	, @gru_id UNIQUEIDENTIFIER
	, @vis_id INT
AS
BEGIN				
	WITH Modulo_hierarquia AS  (
    -- Modulo
    SELECT sis_id, mod_id, mod_idPai, mod_nome
    FROM SYS_Modulo WITH (NOLOCK)  
	WHERE SYS_Modulo.mod_situacao = 1
		AND SYS_Modulo.sis_id = @sis_id
		AND SYS_Modulo.mod_idPai = @mod_idPai		
	
	UNION ALL
	
	 -- Módulos da hierarquia
	 SELECT modu.sis_id, modu.mod_id,  modu.mod_idPai, modu.mod_nome
	 FROM SYS_modulo AS modu WITH (NOLOCK)
	 INNER JOIN Modulo_hierarquia moduh
	 ON modu.sis_id = moduh.sis_id 
		AND modu.mod_idPai = moduh.mod_id 
	 WHERE modu.mod_situacao <> 3
		AND modu.sis_id = @sis_id
	)

	SELECT 
		SYS_ModuloSiteMap.mod_id
		, SYS_ModuloSiteMap.sis_id
		, SYS_ModuloSiteMap.msm_id
		, SYS_ModuloSiteMap.msm_nome
		, SYS_ModuloSiteMap.msm_descricao
		, SYS_ModuloSiteMap.msm_url
		, SYS_ModuloSiteMap.msm_informacoes
		, SYS_Modulo.mod_idPai
		, SYS_Modulo.mod_nome
	FROM
		SYS_Modulo WITH(NOLOCK)
	INNER JOIN SYS_ModuloSiteMap WITH(NOLOCK)
		ON SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
		AND SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id
	INNER JOIN SYS_VisaoModulo WITH (NOLOCK)
		ON SYS_VisaoModulo.sis_id = SYS_Modulo.sis_id
			AND SYS_VisaoModulo.mod_id = SYS_Modulo.mod_id			
	INNER JOIN SYS_VisaoModuloMenu WITH (NOLOCK)
		ON SYS_VisaoModuloMenu.sis_id = SYS_Modulo.sis_id
			AND SYS_VisaoModuloMenu.mod_id = SYS_Modulo.mod_id
			AND SYS_VisaoModuloMenu.msm_id = SYS_ModuloSiteMap.msm_id
	WHERE
		SYS_Modulo.mod_situacao = 1
		AND SYS_ModuloSiteMap.sis_id = @sis_id
		AND SYS_Modulo.mod_idPai = @mod_idPai
		AND SYS_VisaoModulo.vis_id = @vis_id
		AND SYS_VisaoModuloMenu.vis_id = @vis_id
		AND ((@mod_idPai IS NULL) OR (SYS_Modulo.mod_id IN(SELECT mod_id FROM Modulo_hierarquia)))
		AND EXISTS (
			SELECT 
				SYS_GrupoPermissao.gru_id
				, SYS_GrupoPermissao.sis_id
				, SYS_GrupoPermissao.mod_id 
			FROM
				SYS_GrupoPermissao WITH (NOLOCK)
			WHERE
				SYS_GrupoPermissao.gru_id = @gru_id
				AND SYS_GrupoPermissao.sis_id = SYS_Modulo.sis_id
				AND SYS_GrupoPermissao.mod_id = SYS_Modulo.mod_id
				AND 
				(
					(grp_consultar = 1)
					OR  
					(grp_inserir = 1)
					OR 
					(grp_alterar = 1) 
					OR
					(grp_excluir = 1)
				)
		)
	ORDER BY
		SYS_VisaoModuloMenu.vmm_ordem
	
	SELECT @@ROWCOUNT					
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupo_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 11/11/2010 10:39
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente a
--				UsuariosGrupo. Filtrada por: 
--					usu_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupo_Update_Situacao]	
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@usg_situacao tinyint		
AS
BEGIN
	--Exclui logicamente os grupo do usuário
	UPDATE SYS_UsuarioGrupo 
	SET 
		usg_situacao = @usg_situacao		
	WHERE 
		usu_id = @usu_id
		AND gru_id = @gru_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoDeficiencia_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoDeficiencia_UPDATE]
		@tde_id uniqueidentifier
		, @tde_nome varchar(100)
		, @tde_situacao TinyInt
		, @tde_dataAlteracao DateTime
		, @tde_integridade Int

AS
BEGIN
	UPDATE PES_TipoDeficiencia 
	SET 
		tde_nome = @tde_nome
		, tde_situacao = @tde_situacao
		, tde_dataAlteracao = @tde_dataAlteracao
		, tde_integridade = @tde_integridade
	WHERE 
		tde_id = @tde_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:41
-- Description:	Decrementa uma unidade no campo integridade da tabela de usuario
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_DECREMENTA_INTEGRIDADE]
		@usu_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_Usuario
	SET 
		usu_integridade = usu_integridade - 1
	WHERE 
		usu_id = @usu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_CertidaoCivil_SELECTBY_pes_id]'
GO

CREATE PROCEDURE [dbo].[STP_PES_CertidaoCivil_SELECTBY_pes_id]
	@pes_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		pes_id
		,ctc_id
		,ctc_tipo
		,ctc_numeroTermo
		,ctc_folha
		,ctc_livro
		,ctc_dataEmissao
		,ctc_nomeCartorio
		,cid_idCartorio
		,unf_idCartorio
		,ctc_distritoCartorio
		,ctc_situacao
		,ctc_dataCriacao
		,ctc_dataAlteracao
		,ctc_matricula
		,ctc_gemeo 
		,ctc_modeloNovo

	FROM
		PES_CertidaoCivil WITH(NOLOCK)
	WHERE 
		pes_id = @pes_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Cidade_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Cidade_INSERT]
		@pai_id uniqueidentifier
		,@unf_id uniqueidentifier
		,@cid_nome VarChar (200)
		,@cid_ddd VarChar (3)
		,@cid_situacao TinyInt
		,@cid_integridade int

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		END_Cidade
		( 
			pai_id
			,unf_id
			,cid_nome
			,cid_ddd
			,cid_situacao
			,cid_integridade
		)
	OUTPUT inserted.cid_id INTO @ID
	VALUES
		( 
			@pai_id
			,@unf_id
			,@cid_nome
			,@cid_ddd
			,@cid_situacao
			,@cid_integridade
		)
	SELECT ID FROM @ID
		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UsuarioGrupo_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UsuarioGrupo_UPDATE]
		@usu_id uniqueidentifier
		,@gru_id uniqueidentifier
		,@usg_situacao tinyint
AS
BEGIN
	UPDATE SYS_UsuarioGrupo
	SET 
		usg_situacao = @usg_situacao
	WHERE 
		usu_id = @usu_id
		AND gru_id = @gru_id	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoDeficiencia_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoDeficiencia_SELECT]
	
AS
BEGIN
	SELECT 
		tde_id
			, tde_nome
			, tde_situacao
			, tde_dataCriacao
			, tde_dataAlteracao
			, tde_integridade
		
	FROM 
		PES_TipoDeficiencia WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MS_InsereVisaoMenu]'
GO

-- =============================================
-- Author:		Diego Fadanni
-- Create date: 15/01/2014
-- Description:	Insere uma visão em um módulo no menu do sistema.
/*
	Parâmetros: 
		@nomeSistema : Obrigatório
			Nome do sistema que será incluído o menu (passar nome exato)
		
		@nomeModuloAvo : Opcional
			Nome do módulo pai do módulo pai (se passado, será utilizado para buscar o módulo pai - 
			para o caso de módulos com o mesmo nome).
				
		@nomeModuloPai : Obrigatório
			Nome do módulo Pai que será incluído o menu (passar nome exato)
			
		@nomeModulo : Obrigatório
			Nome do novo módulo no menu

		@nomeVisao : Obrigatório
			Nome da visao que será adicionada no módulo
*/
-- =============================================
CREATE PROCEDURE [dbo].[MS_InsereVisaoMenu]
	@nomeSistema VARCHAR(100)
	, @nomeModuloPai VARCHAR(50)
	, @nomeModulo VARCHAR(300)
	, @nomeVisao VARCHAR(50)
	, @nomeModuloAvo VARCHAR(50)
AS
BEGIN
	DECLARE @vis_id INT
	SET @nomeModuloAvo = ISNULL(@nomeModuloAvo, '');
	
	--Guarda as ocorrências ao criar o menu
	DECLARE @Mensagem VARCHAR(MAX) = 'Ínicio do processo de inclusão da visão do módulo do menu.'

	-- ID do sistema
	DECLARE @sis_id INT = 0
	SELECT TOP 1
		@sis_id = sis_id
	FROM
		SYS_Sistema WITH(NOLOCK)
	WHERE
		sis_nome = @nomeSistema
		AND sis_situacao <> 3
	
	PRINT 'Sistema: ' + CAST(@sis_id AS VARCHAR(10));

	-- ID do módulo pai
	DECLARE @mod_idPai INT = 0;
	
	IF (ISNULL(@nomeModuloPai, '') = '')
	BEGIN
		-- Se não passou módulo pai, é null (módulo raiz).
		SET @mod_idPai = 0
	END
	ELSE
	BEGIN
		IF (@nomeModuloAvo = '')
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloPai + ';';
			-- Se não passou módulo avô, busca o módulo pai só pelo nome.
			SELECT TOP 1
				@mod_idPai = mod_id
			FROM
				SYS_Modulo WITH(NOLOCK)
			WHERE
				sis_id = @sis_id
				AND mod_nome = @nomeModuloPai
				AND mod_situacao <> 3
		END
		ELSE
		BEGIN
			PRINT 'Buscando módulo pai dentro do módulo: ' + @nomeModuloAvo + ' -> ' + @nomeModuloPai + ';';
			-- Se passou o módulo avô, busca o módulo pai pelo módulo avô e pelo módulo pai.
			SELECT TOP 1
				@mod_idPai = ModPai.mod_id
			FROM
				SYS_Modulo ModPai WITH(NOLOCK)
			WHERE
				ModPai.sis_id = @sis_id
				AND ModPai.mod_nome = @nomeModuloPai
				AND ModPai.mod_situacao <> 3
				AND ModPai.mod_idPai = 
				(
					SELECT TOP 1
						mod_id
					FROM
						SYS_Modulo ModAvo WITH(NOLOCK)
					WHERE
						ModAvo.sis_id = @sis_id
						-- Módulo avô.
						AND ModAvo.mod_nome = @nomeModuloAvo
						AND ModAvo.mod_situacao <> 3
				)
		END
	END
	
	PRINT 'ID do módulo pai: ' + CAST(@mod_idPai as varchar(10));
	
	-- Configura as visões que verão o módulo (com permissão total).
	IF (ISNULL(@nomeVisao, '') = '')
	BEGIN
		SET @vis_id = NULL
	END
	ELSE
	BEGIN
		SET @vis_id = (SELECT TOP 1 vis_id FROM SYS_Visao WITH(NOLOCK) WHERE vis_nome = @nomeVisao)
	END

	IF (@sis_id > 0 AND ISNULL(@mod_idPai, 1) > 0 AND @vis_id IS NOT NULL)
	BEGIN
	
		DECLARE @mod_id INT = (SELECT TOP 1 mod_id FROM SYS_Modulo WITH(NOLOCK) WHERE mod_situacao <> 3 AND sis_id = @sis_id AND 
																	(@mod_idPai IS NULL OR mod_idPai = @mod_idPai) AND mod_nome = @nomeModulo)
		IF (@mod_id IS NOT NULL)
		BEGIN	

			IF NOT EXISTS (SELECT vis_id FROM SYS_VisaoModulo WHERE vis_id = @vis_id AND sis_id = @sis_id AND mod_id = @mod_id)
			BEGIN
			
			-- Visões.
			INSERT INTO SYS_VisaoModulo (vis_id, sis_id, mod_id)
			VALUES (@vis_id, @sis_id, @mod_id)

			-- Descobrir último valor de ordem cadastrado no menu.
			DECLARE @ordem INT
			SET @ordem = 
			(
				SELECT MAX(ISNULL(vmm_ordem, 0)) + 1
				FROM 
					SYS_VisaoModuloMenu Visao WITH(NOLOCK)
					INNER JOIN SYS_Modulo Modulo WITH(NOLOCK)
						ON (Modulo.mod_id = Visao.mod_id)
				WHERE 
					@mod_idPai IS NULL OR Modulo.mod_idPai = @mod_idPai	
			)
			SET @ordem = ISNULL(@ordem, 1)
			
			DECLARE @msm_id INT = 0
			SET @msm_id = (SELECT TOP 1 msm_id FROM SYS_ModuloSiteMap WITH(NOLOCK) WHERE sis_id = @sis_id AND mod_id = @mod_id ORDER BY msm_id)
			
			IF (@msm_id > 0)
			BEGIN
				INSERT INTO SYS_VisaoModuloMenu (vmm_ordem, vis_id, sis_id, mod_id, msm_id)
				VALUES (@ordem, @vis_id, @sis_id, @mod_id, @msm_id)
			END
			SET @Mensagem = @Mensagem + CHAR(13) + ' Foi incluída a visão "'+@nomeVisao+'" para o módulo.'
			END
			ELSE
			BEGIN
				SET @Mensagem = @Mensagem + CHAR(13) + ' A visão já está cadastrada para o módulo.'
			END
		END
		ELSE
		BEGIN
			
			SET @Mensagem = @Mensagem + CHAR(13) + ' O módulo não existe no sistema.'
			
		END
	END
	ELSE
	BEGIN
		IF (@sis_id <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O sistema não foi encontrado.'
		
		IF (@mod_idPai <= 0)
			SET @Mensagem = @Mensagem + CHAR(13) + ' O módulo pai não foi encontrado.'
		
		IF (@vis_id IS NULL)
			SET @Mensagem = @Mensagem + CHAR(13) + ' A visão não foi encontrada.'
	END	
	
	SET @Mensagem = @Mensagem + CHAR(13) + ' Fim da inclusão da visão no módulo do menu.'

	-- Retorna mensagem	
	PRINT @Mensagem

END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 13:44
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Endereço da Entidade. Filtrada por: 
--					ent_id, enc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_Update_Situacao]	
		@ent_id uniqueidentifier
		,@uad_id uniqueidentifier
		,@uae_id uniqueidentifier
		,@uae_situacao TINYINT
		,@uae_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaEndereco
	SET 
		uae_situacao = @uae_situacao
		,uae_dataAlteracao = @uae_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_id = @uae_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDeficiencia_SELECT]'
GO



CREATE PROCEDURE [dbo].[STP_PES_PessoaDeficiencia_SELECT]
	
AS
BEGIN
	SELECT 
		pes_id
		,tde_id

	FROM 
		PES_PessoaDeficiencia WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Cidade_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Cidade_DELETE]
	@cid_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		END_Cidade	
	WHERE 
		cid_id = @cid_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoDeficiencia_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoDeficiencia_LOAD]
	@tde_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
			tde_id
			, tde_nome
			, tde_situacao
			, tde_dataCriacao
			, tde_dataAlteracao
			, tde_integridade

 	FROM
 		PES_TipoDeficiencia
	WHERE 
		tde_id = @tde_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:21
-- Description:	Altera o endereço da unidade administrativa 
--              preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativaEndereco_UPDATE]
		@ent_id uniqueidentifier
			, @uad_id uniqueidentifier
			, @uae_id uniqueidentifier 
			, @end_id uniqueidentifier
			, @uae_numero varchar(20)
			, @uae_complemento varchar(100)
			, @uae_situacao tinyInt			
			, @uae_dataAlteracao DateTime
			, @uae_latitude DECIMAL(15,10) = NULL
			, @uae_longitude DECIMAL(15,10) = NULL
			, @uae_enderecoPrincipal bit = NULL

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativaEndereco
	SET 
			end_id = @end_id
			, uae_numero = @uae_numero
			, uae_complemento = @uae_complemento
			, uae_situacao = @uae_situacao			
			, uae_dataAlteracao = @uae_dataAlteracao
			, uae_latitude = @uae_latitude
			, uae_longitude = @uae_longitude
			, uae_enderecoPrincipal = @uae_enderecoPrincipal
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		AND uae_id = @uae_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDeficiencia_SELECTBY_pes_id]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaDeficiencia_SELECTBY_pes_id]
	@pes_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		pes_id
		,tde_id

	FROM
		PES_PessoaDeficiencia WITH(NOLOCK)
	WHERE 
		pes_id = @pes_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:21
-- Description:	Altera a unidade administrativa
--              preservando a data da criação e a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_UPDATE]
		@ent_id uniqueidentifier
			, @uad_id uniqueidentifier 
			, @tua_id uniqueidentifier
			, @uad_codigo varchar(20)
			, @uad_nome varchar(200)
			, @uad_sigla varchar(50)
			, @uad_idSuperior uniqueidentifier
			, @uad_situacao tinyInt			
			, @uad_dataAlteracao dateTime			
			, @uad_codigoIntegracao varchar(50) = NULL
			, @uad_codigoInep varchar(30) = NULL

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativa
	SET 		
		tua_id = @tua_id 
			, uad_codigo = @uad_codigo
			, uad_nome = @uad_nome
			, uad_sigla = @uad_sigla
			, uad_idSuperior = @uad_idSuperior
			, uad_situacao = @uad_situacao			
			, uad_dataAlteracao = @uad_dataAlteracao
			-- Se o campo for nulo (não informado), não altera o valor.
			, uad_codigoIntegracao = ISNULL(@uad_codigoIntegracao, uad_codigoIntegracao)
			, uad_codigoInep = @uad_codigoInep
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoDeficiencia_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoDeficiencia_INSERT]
		@tde_nome varchar(100)
		, @tde_situacao TinyInt
		, @tde_dataCriacao DateTime
		, @tde_dataAlteracao DateTime
		, @tde_integridade Int		

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		PES_TipoDeficiencia
		( 
			tde_nome
			, tde_situacao
			, tde_dataCriacao
			, tde_dataAlteracao
			, tde_integridade
			 
		)
	OUTPUT inserted.tde_id INTO @ID
	VALUES
		( 
			@tde_nome
			, @tde_situacao
			, @tde_dataCriacao
			, @tde_dataAlteracao
			, @tde_integridade
		)
	SELECT ID FROM @ID	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/01/2010 15:49
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Entidade. Filtrada por: 
--					ent_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_Update_Situacao]	
		@ent_id uniqueidentifier
		,@ent_situacao TINYINT
		,@ent_dataAlteracao DateTime
AS
BEGIN
	--Exclui logicamente os endereços da entidade	
	UPDATE SYS_EntidadeEndereco
	SET 
		ene_situacao = @ent_situacao
		,ene_dataAlteracao = @ent_dataAlteracao
	WHERE 
		ent_id = @ent_id					
			
	--Exclui logicamente os contatos da entidade	
	UPDATE SYS_EntidadeContato 
	SET 
		enc_situacao = @ent_situacao
		,enc_dataAlteracao = @ent_dataAlteracao
	WHERE 
		ent_id = @ent_id		
	
	--Exclui logicamente a entidade
	UPDATE SYS_Entidade 
	SET 
		ent_situacao = @ent_situacao
		,ent_dataAlteracao = @ent_dataAlteracao
	WHERE 
		ent_id = @ent_id
			
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_PessoaDeficiencia_SELECTBY_tde_id]'
GO

CREATE PROCEDURE [dbo].[STP_PES_PessoaDeficiencia_SELECTBY_tde_id]
	@tde_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		pes_id
		,tde_id

	FROM
		PES_PessoaDeficiencia WITH(NOLOCK)
	WHERE 
		tde_id = @tde_id 
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_uad_Nome]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 03/02/2010 16:50
-- Description:	utilizado no cadastro de unidade administrativas,
--              para saber se o nome já está cadastrado na mesma entidade
--				filtrados por:
--					entidade
--					unidade administrativa (diferente do parametro), 					 
--                  nome, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_uad_Nome]	
	@ent_id uniqueidentifier
	, @uad_id uniqueidentifier
	, @uad_nome VARCHAR(200)			
	, @uad_situacao TINYINT		
AS
BEGIN
	SELECT 	
		ent_id		
		,uad_id
		,uad_nome
		,tua_id
		,uad_codigo
		,uad_sigla
		,uad_idSuperior
		,uad_situacao
		,uad_dataCriacao
		,uad_dataAlteracao
		,uad_integridade 
		,uad_codigoInep		
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)		
	WHERE
		uad_situacao <> 3
		AND (@ent_id is null or ent_id = @ent_id)								
		AND (@uad_id is null or uad_id <> @uad_id)								
		AND (@uad_nome  is null or uad_nome = @uad_nome)			
		AND (@uad_situacao is null or uad_situacao = @uad_situacao)						
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_SelectBy_Nome_Nascimento_CPF]'
GO
-- ========================================================================
-- Author:		
-- Create date:  19/08/2014
-- Description:	
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_SelectBy_Nome_Nascimento_CPF]
    @pes_nome			VARCHAR(200),
    @pes_dataNascimento DATE,
    @tdo_id				UNIQUEIDENTIFIER = NULL,
    @psd_numero			VARCHAR(50) = NULL
AS 
BEGIN

	SELECT 
		  pes.pes_id
		, pes.pes_nome
		, pes.pes_nome_abreviado
		, pes.pai_idNacionalidade
		, pes.pes_naturalizado
		, pes.cid_idNaturalidade
		, pes.pes_dataNascimento
		, pes.pes_estadoCivil
		, pes.pes_racaCor
		, pes.pes_sexo
		, pes.pes_idFiliacaoPai
		, pes.pes_idFiliacaoMae
		, pes.tes_id
		, pes.tes_id
		, pes.pes_foto
		, pes.pes_situacao
		, pes.pes_dataCriacao
		, pes.pes_dataAlteracao
		, pes.pes_integridade
		, pes.arq_idFoto
		, pes.pes_nomeSocial
	
	FROM 
		PES_Pessoa pes WITH (NOLOCK)
		LEFT JOIN PES_PessoaDocumento doc WITH (NOLOCK)
			ON		doc.pes_id = pes.pes_id
				AND doc.tdo_id = @tdo_id
				AND doc.psd_situacao <> 3
	WHERE
		pes.pes_situacao <> 3
		AND pes.pes_nome = @pes_nome
		AND pes.pes_dataNascimento = @pes_dataNascimento
		AND (@tdo_id IS NULL OR doc.psd_numero = @psd_numero)
		 
    ORDER BY 
		pes.pes_dataCriacao
	
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Permissao]'
GO
-- ========================================================================
-- Author:		Carla Frascareli
-- Create date: 22/11/2010
-- Description:	Retorna as Unidades Administrativas pelos filtros informados,
--				pela entidade e pela permissão do usuário nas UAs.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_Pesquisa_Permissao]
	@tua_id UNIQUEIDENTIFIER	
	, @ent_id UNIQUEIDENTIFIER
	, @gru_id uniqueidentifier
	, @usu_id uniqueidentifier	
	, @uad_situacao TINYINT
	, @uad_id uniqueidentifier
AS
BEGIN

	DECLARE @TbUas TABLE (uad_id UNIQUEIDENTIFIER NOT NULL);

	INSERT INTO @TbUas (uad_id)
	SELECT uad_id FROM FN_Select_UAs_By_PermissaoUsuario(@usu_id, @gru_id)
	
	SELECT	
		UA.tua_id
		, uad_id
		, uad_idSuperior
		, uad_nome
	FROM
		SYS_UnidadeAdministrativa UA WITH (NOLOCK)		
	INNER JOIN SYS_Entidade Ent WITH (NOLOCK)
		ON Ent.ent_id = UA.ent_id
		AND Ent.ent_situacao <> 3
	INNER JOIN SYS_TipoUnidadeAdministrativa Tua WITH (NOLOCK)
		ON Tua.tua_id = UA.tua_id	
		AND Tua.tua_situacao <> 3
	WHERE
		uad_situacao <> 3
		AND((@tua_id IS NULL) OR (UA.tua_id = @tua_id))
		AND ((@uad_id IS NULL) OR (UA.uad_id <> @uad_id))
		AND ((@uad_situacao IS NULL) OR (UA.uad_situacao = @uad_situacao))
		
		-- Somente da Entidade informada.
		AND (UA.ent_id = @ent_id)
		
		-- Filtra as Unidades Administrativas que o usuário tem permissão.
		AND EXISTS(SELECT uad_id FROM @TbUas T WHERE UA.uad_id = T.uad_id)
		
	ORDER BY
		uad_nome
		
	SELECT @@ROWCOUNT				
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Pais_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Pais_UPDATE]
		@pai_id uniqueidentifier
		,@pai_nome VarChar (100)
		,@pai_sigla VarChar (10)
		,@pai_naturalMasc VarChar (100)
		,@pai_naturalFem VarChar (100)
		,@pai_situacao TinyInt
		,@pai_integridade Int

AS
BEGIN
	UPDATE END_Pais 
	SET 		
		pai_nome = @pai_nome
		,pai_sigla = @pai_sigla
		,pai_naturalMasc = @pai_naturalMasc
		,pai_naturalFem = @pai_naturalFem
		,pai_situacao = @pai_situacao
		,pai_integridade = @pai_integridade
	WHERE 
		pai_id = @pai_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_SelectBy_Busca]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 29/01/2010 18:30
-- Description:	Select para retorna a busca de pessoas conforme o documento
--				de especificação tópico 4.4.7.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_SelectBy_Busca]
	@pes_nome VARCHAR(200)
	,@pes_dataNascimento DATE
	, @TIPO_DOCUMENTACAO_CPF VARCHAR(50)
	, @TIPO_DOCUMENTACAO_RG VARCHAR(50)
	
AS
BEGIN

	DECLARE @pes_nomeFiltro VARCHAR(200) = ISNULL('%' + @pes_nome + '%', '%')
	
	DECLARE @tdo_idCPF UNIQUEIDENTIFIER
	DECLARE @tdo_idRG UNIQUEIDENTIFIER
	
	DECLARE @Pessoas AS TABLE(
		pes_id UNIQUEIDENTIFIER NOT NULL,
		pes_nome VARCHAR(200) NOT NULL,
		pes_dataNascimento VARCHAR(10) NULL,
		TIPO_DOCUMENTACAO_CPF VARCHAR(50) NULL,
		TIPO_DOCUMENTACAO_RG VARCHAR(50) NULL,
		PRIMARY KEY (pes_id)
	)
	
	SET @tdo_idCPF = (	SELECT par.par_valor
						FROM  SYS_Parametro par WITH(NOLOCK)
						WHERE 
							par_situacao = 1
							AND par.par_chave = 'TIPO_DOCUMENTACAO_CPF'
							AND par_vigenciaInicio <= CAST(GETDATE() AS DATE)
							AND ((par_vigenciaFim IS NULL) OR (par_vigenciaFim >= CAST(GETDATE() AS DATE))))

	SET @tdo_idRG = (	SELECT par.par_valor
						FROM  SYS_Parametro par WITH(NOLOCK)
						WHERE 
							par_situacao = 1
							AND par.par_chave = 'TIPO_DOCUMENTACAO_RG'
							AND par_vigenciaInicio <= CAST(GETDATE() AS DATE)
							AND ((par_vigenciaFim IS NULL) OR (par_vigenciaFim >= CAST(GETDATE() AS DATE))))

	IF (@TIPO_DOCUMENTACAO_CPF IS NOT NULL)
	BEGIN
		INSERT INTO @Pessoas(pes_id, pes_nome, pes_dataNascimento, TIPO_DOCUMENTACAO_CPF, TIPO_DOCUMENTACAO_RG)	
		SELECT 
			Pes.pes_id 
			, Pes.pes_nome
			, CONVERT(VARCHAR, Pes.pes_dataNascimento, 103) AS pes_dataNascimento
			, PsdCPF.psd_numero
			, PsdRG.psd_numero
		FROM 
			PES_Pessoa AS Pes WITH(NOLOCK)
			INNER JOIN PES_PessoaDocumento AS PsdCPF WITH(NOLOCK)
				ON Pes.pes_id = PsdCPF.pes_id
				AND PsdCPF.tdo_id = @tdo_idCPF
				AND PsdCPF.psd_situacao <> 3
			LEFT JOIN PES_PessoaDocumento AS PsdRG WITH(NOLOCK)
				ON Pes.pes_id = PsdRG.pes_id
				AND PsdRG.tdo_id = @tdo_idRG
				AND PsdRG.psd_situacao <> 3
		WHERE
			((PsdCPF.psd_numero LIKE '%' + @TIPO_DOCUMENTACAO_CPF + '%'))
			AND ((@TIPO_DOCUMENTACAO_RG IS NULL) OR (PsdRG.psd_numero LIKE '%' + @TIPO_DOCUMENTACAO_RG + '%'))		
			AND pes_situacao <> 3
			AND ((pes_nome LIKE @pes_nomeFiltro) 
					OR (pes_nome_abreviado LIKE @pes_nomeFiltro))
			AND (@pes_dataNascimento IS NULL OR pes_dataNascimento = @pes_dataNascimento)
	END
	ELSE IF (@TIPO_DOCUMENTACAO_RG IS NOT NULL)
	BEGIN
		INSERT INTO @Pessoas(pes_id, pes_nome, pes_dataNascimento, TIPO_DOCUMENTACAO_CPF, TIPO_DOCUMENTACAO_RG)	
		SELECT 
			Pes.pes_id 
			, Pes.pes_nome
			, CONVERT(VARCHAR, Pes.pes_dataNascimento, 103) AS pes_dataNascimento
			, PsdCPF.psd_numero
			, PsdRG.psd_numero
		FROM 
			PES_Pessoa AS Pes WITH(NOLOCK)
			INNER JOIN PES_PessoaDocumento AS PsdRG WITH(NOLOCK)
				ON Pes.pes_id = PsdRG.pes_id
				AND PsdRG.tdo_id = @tdo_idRG
				AND PsdRG.psd_situacao <> 3
			LEFT JOIN PES_PessoaDocumento AS PsdCPF WITH(NOLOCK)
				ON Pes.pes_id = PsdCPF.pes_id
				AND PsdCPF.tdo_id = @tdo_idCPF
				AND PsdCPF.psd_situacao <> 3
		WHERE
			((PsdRG.psd_numero LIKE '%' + @TIPO_DOCUMENTACAO_RG + '%'))		
			AND ((@TIPO_DOCUMENTACAO_CPF IS NULL) OR (PsdCPF.psd_numero LIKE '%' + @TIPO_DOCUMENTACAO_CPF + '%'))
			AND pes_situacao <> 3
			AND ((pes_nome LIKE @pes_nomeFiltro) 
					OR (pes_nome_abreviado LIKE @pes_nomeFiltro))
			AND (@pes_dataNascimento IS NULL OR pes_dataNascimento = @pes_dataNascimento)
	END
	ELSE 
	BEGIN
		; WITH Pessoas AS (
			SELECT 
				Pes.pes_id 
				, Pes.pes_nome
				, CONVERT(VARCHAR, Pes.pes_dataNascimento, 103) AS pes_dataNascimento
			FROM 
				PES_Pessoa AS Pes WITH(NOLOCK)
			WHERE
				pes_situacao <> 3
				AND ((pes_nome LIKE @pes_nomeFiltro) 
						OR (pes_nome_abreviado LIKE @pes_nomeFiltro))
				AND (@pes_dataNascimento IS NULL OR pes_dataNascimento = @pes_dataNascimento)
		)
		INSERT INTO @Pessoas(pes_id, pes_nome, pes_dataNascimento, TIPO_DOCUMENTACAO_CPF, TIPO_DOCUMENTACAO_RG)
			SELECT 
				Pes.pes_id 
				, Pes.pes_nome
				, CONVERT(VARCHAR, Pes.pes_dataNascimento, 103) AS pes_dataNascimento
				, PsdCPF.psd_numero
				, PsdRG.psd_numero
			FROM 
				Pessoas AS Pes WITH(NOLOCK)
				LEFT JOIN PES_PessoaDocumento AS PsdCPF WITH(NOLOCK)
					ON Pes.pes_id = PsdCPF.pes_id
					AND PsdCPF.tdo_id = @tdo_idCPF
					AND PsdCPF.psd_situacao <> 3
				LEFT JOIN PES_PessoaDocumento AS PsdRG WITH(NOLOCK)
					ON Pes.pes_id = PsdRG.pes_id
					AND PsdRG.tdo_id = @tdo_idRG
					AND PsdRG.psd_situacao <> 3
	END
	
	SELECT
		Pes.pes_id 
		, Pes.pes_nome
		, [TIPO_DOCUMENTACAO_CPF]
		, [TIPO_DOCUMENTACAO_RG]
		, pes_dataNascimento
	FROM 
		@Pessoas AS Pes
	ORDER BY
		Pes.pes_nome	
		
	SELECT @@ROWCOUNT

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_SiteMapXML]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 22/11/2010 10:09
-- Description:	Retorno o mapa do site apartir de modulo do sistema.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_SelectBy_SiteMapXML]
	@gru_id uniqueidentifier
	, @sis_id int
	, @vis_id int
	, @mod_id int
AS
BEGIN
	WITH Menus(sis_id, mod_id, mod_idPai, msm_id, mod_nome, msm_url, vmm_ordem) 
	AS
	(
		SELECT
			SYS_Modulo.sis_id
			, SYS_Modulo.mod_id
			, SYS_Modulo.mod_idPai
			, SYS_ModuloSiteMap.msm_id
			, SYS_Modulo.mod_nome
			, SYS_ModuloSiteMap.msm_url
			, SYS_VisaoModuloMenu.vmm_ordem
		FROM
			SYS_Modulo WITH (NOLOCK)
			INNER JOIN SYS_ModuloSiteMap WITH (NOLOCK)
				ON SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id 
				AND SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
			INNER JOIN SYS_VisaoModuloMenu WITH (NOLOCK)
				ON SYS_ModuloSiteMap.sis_id = SYS_VisaoModuloMenu.sis_id 
				AND SYS_ModuloSiteMap.mod_id = SYS_VisaoModuloMenu.mod_id
				AND SYS_ModuloSiteMap.msm_id = SYS_VisaoModuloMenu.msm_id	
		WHERE
			SYS_Modulo.mod_situacao <> 3
			AND SYS_Modulo.sis_id = @sis_id
			AND SYS_VisaoModuloMenu.vis_id = @vis_id
			AND EXISTS (
				SELECT 
					SYS_GrupoPermissao.gru_id
					, SYS_GrupoPermissao.sis_id
					, SYS_GrupoPermissao.mod_id 
				FROM
					SYS_GrupoPermissao WITH (NOLOCK)
				WHERE
					SYS_GrupoPermissao.gru_id = @gru_id
					AND SYS_GrupoPermissao.sis_id = SYS_Modulo.sis_id
					AND SYS_GrupoPermissao.mod_id = SYS_Modulo.mod_id
					AND 
					(
						(grp_consultar = 1)
						OR  
						(grp_inserir = 1)
						OR 
						(grp_alterar = 1) 
						OR
						(grp_excluir = 1)
					)
			)
	)
	
	SELECT 1 AS Tag
		, NULL AS Parent
		, mod_nome AS [menu!1!id]
		, msm_url AS [menu!1!url]
		, vmm_ordem AS [menu!1!ordem]
		, NULL AS [item!2!id]
		, NULL AS [item!2!url]
		, NULL AS [item!2!ordem]
		, NULL AS [subitem!3!id]
		, NULL AS [subitem!3!url]
		, NULL AS [subitem!3!ordem]
	FROM 
		Menus WITH(NOLOCK)
	WHERE
		(((@mod_id IS NULL) AND (mod_idPai is null)) or  mod_id = @mod_id)
	UNION ALL
	SELECT 2 AS Tag
		, 1 AS Parent
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, ISNULL(item.msm_url, '')
		, item.vmm_ordem
		, NULL AS [subitem!3!id]
		, NULL AS [subitem!3!url]
		, NULL AS [subitem!3!ordem]
	FROM 
		Menus AS menu WITH(NOLOCK)
	INNER JOIN Menus AS item WITH(NOLOCK)
		ON item.mod_idPai = menu.mod_id
	WHERE
		(((@mod_id IS NULL) AND (menu.mod_idPai is null)) or  menu.mod_id = @mod_id)
	UNION ALL
	SELECT 3 AS Tag
		, 2 AS Parent
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, item.msm_url
		, item.vmm_ordem
		, subitem.mod_nome
		, subitem.msm_url
		, subitem.vmm_ordem
	FROM 
		Menus AS menu WITH(NOLOCK)
	INNER JOIN Menus AS item WITH(NOLOCK)
		ON item.mod_idPai = menu.mod_id
	INNER JOIN Menus AS subitem WITH(NOLOCK)
		ON subitem.mod_idPai = item.mod_id
	WHERE
		(((@mod_id IS NULL) AND (not item.mod_idPai is null)) or  menu.mod_id = @mod_id)
	ORDER BY
		[menu!1!ordem], [item!2!ordem], [subitem!3!ordem]
	FOR XML EXPLICIT, ROOT('menus')
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Pais_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Pais_SELECT]
	
AS
BEGIN
	SELECT 
		pai_id
		,pai_nome
		,pai_sigla
		,pai_naturalMasc
		,pai_naturalFem
		,pai_situacao
		,pai_integridade
	FROM 
		END_Pais WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_ServidorRelatorio_DELETE]'
GO
-- ===================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/04/2011 17:35
-- Description:	Altera o campo srr_situacao para 3 efetuando assim a 
--				exclusão lógica.
-- ===================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_ServidorRelatorio_DELETE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT
AS
BEGIN
	UPDATE CFG_ServidorRelatorio SET 
		srr_situacao = 3
		, srr_dataAlteracao = GETDATE()
	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeEndereco_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 13:44
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Endereço da Entidade. Filtrada por: 
--					ent_id, enc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeEndereco_Update_Situacao]	
		@ent_id uniqueidentifier
		,@ene_id uniqueidentifier
		,@ene_situacao TINYINT
		,@ene_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_EntidadeEndereco
	SET 
		ene_situacao = @ene_situacao
		,ene_dataAlteracao = @ene_dataAlteracao
	WHERE 
		ent_id = @ent_id
		and ene_id = @ene_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_Select]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 08/02/2010 15:15
-- Description:	utilizado na busca parametros, retorna os parametros
--              que não foram excluídas logicamente.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_Select]	
	
AS
BEGIN
	SELECT
		P1.par_id
		,P1.par_chave
		,P1.par_obrigatorio
		,CASE P1.par_chave
			WHEN 'PAIS_PADRAO_BRASIL' THEN (SELECT pai_nome FROM END_Pais  WITH (NOLOCK) WHERE pai_id = P1.par_valor)
			WHEN 'ESTADO_PADRAO_SP' THEN (SELECT unf_nome FROM END_UnidadeFederativa  WITH (NOLOCK) WHERE unf_id = P1.par_valor)
			WHEN 'TIPO_DOCUMENTACAO_CPF' THEN (SELECT tdo_nome FROM SYS_TipoDocumentacao WITH (NOLOCK) WHERE tdo_id = P1.par_valor)
			WHEN 'TIPO_DOCUMENTACAO_RG' THEN (SELECT tdo_nome FROM SYS_TipoDocumentacao WITH (NOLOCK) WHERE tdo_id = P1.par_valor)			
			WHEN 'TIPO_MEIOCONTATO_EMAIL' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WITH (NOLOCK) WHERE tmc_id = P1.par_valor) 
			WHEN 'TIPO_MEIOCONTATO_TELEFONE' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WITH (NOLOCK) WHERE tmc_id = P1.par_valor)
			WHEN 'TIPO_MEIOCONTATO_SITE' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WHERE tmc_id = par_valor) 
			WHEN 'TAMANHO_MAX_FOTO_PESSOA' THEN P1.par_valor + ' - Kbytes'
			WHEN 'URL_ADMINISTRATIVO' THEN P1.par_valor
			WHEN 'TITULO_GERAL' THEN P1.par_valor
			WHEN 'MENSAGEM_COPYRIGHT' THEN P1.par_valor
			WHEN 'LOGO_CLIENTE' THEN ''
			WHEN 'URL_CLIENTE' THEN P1.par_valor						
			WHEN 'EXIBIR_LOGO_CLIENTE' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOGO_GERAL_SISTEMA' THEN ''			
			WHEN 'QT_ITENS_PAGINACAO' THEN P1.par_valor						
			WHEN 'SALVAR_SEMPRE_MAIUSCULO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'FORMATO_SENHA_USUARIO' THEN P1.par_valor
			WHEN 'TAMANHO_SENHA_USUARIO' THEN P1.par_valor			
			WHEN 'LOG_ERROS_GRAVAR_QUERYSTRING' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOG_ERROS_GRAVAR_SERVERVARIABLES' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOG_ERROS_GRAVAR_PARAMS' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOG_ERROS_CHAVES_NAO_GRAVAR' THEN P1.par_valor
			WHEN 'HELP_DESK_CONTATO' THEN P1.par_valor
			WHEN 'MENSAGEM_ICONE_HELP' THEN P1.par_valor
			WHEN 'ID_GOOGLE_ANALYTICS' THEN P1.par_valor
			WHEN 'SUPORTE_TECNICO_EMAILS' THEN P1.par_valor
			WHEN 'REMOVER_OPCAO_ESQUECISENHA' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'MENSAGEM_ALERTA_PRELOGIN' THEN P1.par_valor
			WHEN 'UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO' THEN P1.par_valor
			WHEN 'QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA' THEN P1.par_valor
			WHEN 'PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'VERSAO_WEBAPI_CORESSO' THEN P1.par_valor
			WHEN 'URL_WEBAPI_CORESSO' THEN P1.par_valor
			WHEN 'PERMITIR_ALTERAR_EMAIL_MEUSDADOS' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'SALVAR_HISTORICO_SENHA_USUARIO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO' THEN P1.par_valor
			WHEN 'PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'GERAR_SENHA_FORMATO_PARAMETRIZADO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'VALIDAR_UNICIDADE_EMAIL_USUARIO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'PERMITIR_MULTIPLOS_ENDERECOS_UA' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'ENDERECO_OBRIGATORIO_CADASTRO_UA' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'PERMITIR_TIPO_CONTATOS_DUPLICADOS' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'TIPO_MEIOCONTATO_TELEFONE_CELULAR' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WITH (NOLOCK) WHERE tmc_id = P1.par_valor)
			WHEN 'TIPO_DOCUMENTACAO_IDENTIFICACAO_FUNCIONAL' THEN (SELECT tdo_nome FROM SYS_TipoDocumentacao WITH (NOLOCK) WHERE tdo_id = P1.par_valor)
			WHEN 'PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'PERMITIR_MULTIPLOS_ENDERECOS_PESSOA' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
			WHEN 'PERMITIR_LOGIN_COM_PROVIDER_EXTERNO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END)
		 END 
		 AS par_valor_nome

		,P1.par_vigenciaFim
		,P1.par_vigenciaInicio
		,CONVERT(VARCHAR,P1.par_vigenciaInicio,103) + ' - ' + ISNULL(CONVERT(VARCHAR,P1.par_vigenciaFim,103),'*') AS par_vigencia
		,P1.par_descricao
	FROM
		SYS_Parametro P1 WITH (NOLOCK)
	where
		par_situacao <> 3
		AND CAST(P1.par_vigenciaInicio AS DATE)<= CAST(GETDATE() AS DATE)
		AND (P1.par_vigenciaFim IS NULL OR CAST(P1.par_vigenciaFim AS DATE) >= CAST(GETDATE() AS DATE))

	ORDER BY 
		par_chave
	
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Visao_SelectBy_vis_nome]'
GO
-- =================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 06/12/2010 16:15
-- Description:	Retorna o id da visão pela nome da visão
-- =================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Visao_SelectBy_vis_nome]
	@vis_nome varchar(50)
AS
BEGIN
	SELECT 
		vis_id
	FROM
		SYS_Visao  WITH (NOLOCK)
	WHERE
		UPPER(vis_nome) = UPPER(@vis_nome)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Pais_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_END_Pais_LOAD]
	@pai_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		pai_id
		,pai_nome
		,pai_sigla
		,pai_naturalMasc
		,pai_naturalFem
		,pai_situacao
		,pai_integridade

 	FROM
 		END_Pais
	WHERE 
		pai_id = @pai_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeEndereco_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:27
-- Description:	Altera o endereço da entidade preservando a data da criação e a integridade
--
-- Author :      Gabriel Scavassa
-- Alter date :  12/02/2016 15:34
-- Description : Alteração do endereço, adicionando endprincipal, latitude e longitude
--
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeEndereco_UPDATE]
		@ent_id uniqueidentifier
			, @ene_id uniqueidentifier
			, @end_id uniqueidentifier
			, @ene_numero varchar(20)
			, @ene_complemento varchar(100)
			, @ene_situacao tinyInt			
			, @ene_dataAlteracao dateTime
			, @ene_enderecoPrincipal BIT = NULL
			, @ene_latitude DECIMAL (15,10) = NULL
			, @ene_longitude DECIMAL (15,10) = NULL

AS
BEGIN
	UPDATE SYS_EntidadeEndereco
	SET  
			end_id = @end_id
			, ene_numero = @ene_numero
			, ene_complemento = @ene_complemento
			, ene_situacao = @ene_situacao			
			, ene_dataAlteracao = @ene_dataAlteracao
			, ene_enderecoPrincipal = @ene_enderecoPrincipal 
			, ene_latitude = @ene_latitude 
			, ene_longitude = @ene_longitude 
	WHERE 
		ent_id = @ent_id
		AND ene_id = @ene_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_sis_id]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 29/01/2010 18:30
-- Description:	Select para retorna a busca de homepage dos módulos
--				filtrado por sistema.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_sis_id]
	@sis_id INT
AS
BEGIN
	SELECT 
		SYS_ModuloSiteMap.mod_id
		, SYS_ModuloSiteMap.sis_id
		, SYS_ModuloSiteMap.msm_id
		, SYS_ModuloSiteMap.msm_nome
		, SYS_ModuloSiteMap.msm_descricao
		, SYS_ModuloSiteMap.msm_url
		, SYS_ModuloSiteMap.msm_informacoes
		, SYS_Modulo.mod_idPai
		, SYS_ModuloSiteMap.msm_urlHelp
	FROM
		SYS_Modulo WITH(NOLOCK)
	INNER JOIN SYS_ModuloSiteMap WITH(NOLOCK)
		ON SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
		AND SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id
	WHERE
		SYS_Modulo.mod_situacao = 1
		AND SYS_ModuloSiteMap.sis_id = @sis_id
		
	SELECT @@ROWCOUNT
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Pais_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Pais_INSERT]
		@pai_nome VarChar (100)
		,@pai_sigla VarChar (10)
		,@pai_naturalMasc VarChar (100)
		,@pai_naturalFem VarChar (100)
		,@pai_situacao TinyInt
		,@pai_integridade Int


AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		END_Pais
		( 
			pai_nome
			,pai_sigla
			,pai_naturalMasc
			,pai_naturalFem
			,pai_situacao
			,pai_integridade
		)
	OUTPUT inserted.pai_id INTO @ID
	VALUES
		( 
			@pai_nome
			,@pai_sigla
			,@pai_naturalMasc
			,@pai_naturalFem
			,@pai_situacao
			,@pai_integridade
		)
	SELECT ID FROM @ID
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_SELECTBY_ent_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Entidade_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,ten_id
		,ent_codigo
		,ent_nomeFantasia
		,ent_razaoSocial
		,ent_sigla
		,ent_cnpj
		,ent_inscricaoMunicipal
		,ent_inscricaoEstadual
		,ent_idSuperior
		,ent_situacao
		,ent_dataCriacao
		,ent_dataAlteracao
		,ent_integridade

	FROM
		SYS_Entidade WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_CertidaoCivil_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 10:26
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Certidão da Pessoa. Filtrada por: 
--					pes_id, ctc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_CertidaoCivil_Update_Situacao]	
		@pes_id uniqueidentifier
		,@ctc_id uniqueidentifier
		,@ctc_situacao TINYINT
		,@ctc_dataAlteracao DATETIME
AS
BEGIN
	UPDATE PES_CertidaoCivil
	SET 
		ctc_situacao = @ctc_situacao
		,ctc_dataAlteracao = @ctc_dataAlteracao
	WHERE 
		pes_id = @pes_id
		and ctc_id = @ctc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:20
-- Description:	Seleciona o valor do campo integridade da tabela de unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_Select_Integridade]
		@uad_id uniqueidentifier
		, @ent_id uniqueidentifier
AS
BEGIN
	SELECT 			
		uad_integridade
	FROM
		SYS_UnidadeAdministrativa WITH (NOLOCK)
	WHERE 
		ent_id = @ent_id
		AND uad_id = @uad_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Pais_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Pais_DELETE]
	@pai_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		END_Pais	
	WHERE 
		pai_id = @pai_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Entidade_SELECTBY_ten_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Entidade_SELECTBY_ten_id]
	@ten_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,ten_id
		,ent_codigo
		,ent_nomeFantasia
		,ent_razaoSocial
		,ent_sigla
		,ent_cnpj
		,ent_inscricaoMunicipal
		,ent_inscricaoEstadual
		,ent_idSuperior
		,ent_situacao
		,ent_dataCriacao
		,ent_dataAlteracao
		,ent_integridade

	FROM
		SYS_Entidade WITH(NOLOCK)
	WHERE 
		ten_id = @ten_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_CertidaoCivil_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 10:28
-- Description:	Altera a certidão da pessoa preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_CertidaoCivil_UPDATE]
	@pes_id UNIQUEIDENTIFIER
	, @ctc_id UNIQUEIDENTIFIER
	, @ctc_tipo TINYINT
	, @ctc_numeroTermo VARCHAR (50)
	, @ctc_folha VARCHAR (20)
	, @ctc_livro VARCHAR (20)
	, @ctc_dataEmissao DATETIME
	, @ctc_nomeCartorio VARCHAR (200)
	, @cid_idCartorio UNIQUEIDENTIFIER
	, @unf_idCartorio UNIQUEIDENTIFIER
	, @ctc_distritoCartorio VARCHAR(100)
	, @ctc_situacao TINYINT	
	, @ctc_dataAlteracao DATETIME
	, @ctc_matricula VARCHAR (32)
	, @ctc_gemeo BIT = NULL
	, @ctc_modeloNovo BIT = NULL

AS
BEGIN

	DECLARE 
		  @ctc_gemeoTmp BIT = 0
		, @ctc_modeloNovoTmp BIT = 0

	-- 
	IF(@ctc_gemeo IS NULL) BEGIN
		SET @ctc_gemeoTmp = (	SELECT	ISNULL(c.ctc_gemeo, 0)
								FROM	PES_CertidaoCivil c WITH (NOLOCK) 
								WHERE	pes_id = @pes_id AND ctc_id = @ctc_id )
	END ELSE BEGIN
		SET @ctc_gemeoTmp = @ctc_gemeo
	END

	-- 
	IF(@ctc_modeloNovo IS NULL ) BEGIN
		SET @ctc_modeloNovoTmp = (	SELECT	ISNULL(c.ctc_modeloNovo, 0)
									FROM	PES_CertidaoCivil c WITH (NOLOCK) 
									WHERE	pes_id = @pes_id AND ctc_id = @ctc_id )
	END BEGIN
		SET @ctc_modeloNovoTmp = @ctc_modeloNovo
	END


	UPDATE 
		PES_CertidaoCivil 
	SET 
		  ctc_tipo = @ctc_tipo 
		, ctc_numeroTermo = @ctc_numeroTermo 
		, ctc_folha = @ctc_folha 
		, ctc_livro = @ctc_livro 
		, ctc_dataEmissao = @ctc_dataEmissao 
		, ctc_nomeCartorio = @ctc_nomeCartorio 
		, cid_idCartorio = @cid_idCartorio 	
		, unf_idCartorio = @unf_idCartorio 
		, ctc_distritoCartorio = @ctc_distritoCartorio
		, ctc_situacao = @ctc_situacao 		
		, ctc_dataAlteracao = @ctc_dataAlteracao 
		, ctc_matricula = @ctc_matricula
		, ctc_gemeo = @ctc_gemeoTmp
		, ctc_modeloNovo = @ctc_modeloNovoTmp

	WHERE 
		pes_id = @pes_id 
		AND ctc_id = @ctc_id 
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:22
-- Description:	Incrementa uma unidade no campo integridade da tabela de unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_INCREMENTA_INTEGRIDADE]
		@uad_id uniqueidentifier
		, @ent_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_UnidadeAdministrativa
	SET 
		uad_integridade = uad_integridade + 1
	WHERE 
		ent_id= @ent_id
		AND uad_id = @uad_id	
		
	RETURN ISNULL(@@ROWCOUNT,-1)			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[VW_SYS_Usuario]'
GO


-- ================================================================
-- Author:		Jean Michel Marque da Silva
-- Create date: 18/06/2014
-- Description:	View utilizada na integração de usuário do CoreSSO 
--				com o Active Directory (AD) utilizando o FIM
--				Apenas usuário do tipo 2-Usuário integrado com AD/Replicação de senha
-- ================================================================
CREATE VIEW [dbo].[VW_SYS_Usuario]
AS
	SELECT
		usu.usu_login
	FROM
		SYS_Usuario usu WITH ( NOLOCK )
	WHERE
		usu.usu_situacao <> 3
		AND usu.usu_integracaoAD = 2
	GROUP BY
		usu.usu_login

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_UPDATE_VigenciaFim]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 12/02/2010 13:12
-- Description:	Altera a data de vigencia final do ultimo registro de determinada 
--				pelo tipo de chave do parametro
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_UPDATE_VigenciaFim]
		@par_chave VARCHAR(1000)
		,@par_vigenciaFim DATE
		,@par_dataAlteracao DateTime
AS
BEGIN
	DECLARE @par_id_alteracao uniqueidentifier
	
	SELECT @par_id_alteracao = (SELECT par_id
								FROM SYS_Parametro
								WHERE par_chave = @par_chave
								AND par_dataCriacao = (SELECT MAX(B.par_dataCriacao)
														FROM SYS_Parametro B
														WHERE B.par_chave = @par_chave
														AND B.par_situacao = 1))

	UPDATE SYS_Parametro
	SET 
		par_vigenciaFim = @par_vigenciaFim
		,par_dataAlteracao = @par_dataAlteracao
	WHERE 
		par_situacao = 1
	AND par_obrigatorio = 1
	AND	par_id = @par_id_alteracao
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:16
-- Description:	Decrementa uma unidade no campo integridade da tabela de unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_DECREMENTA_INTEGRIDADE]
		@uad_id uniqueidentifier
		, @ent_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_UnidadeAdministrativa 
	SET 
		uad_integridade = uad_integridade - 1
	WHERE 
		ent_id= @ent_id
		AND uad_id = @uad_id	
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelecionaPorUnidadeAdministrativa]'
GO
-- ===========================================================================
-- Author:		Haila Pelloso
-- Create date: 17/12/2014
-- Description:	Retorna todos os usuários que não foram excluídos logicamente
--				filtrando por: entidade e unidade administrativa.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelecionaPorUnidadeAdministrativa]
	@ent_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
	, @uad_codigo VARCHAR(20)
	, @trazerFoto BIT 
	, @usu_id UNIQUEIDENTIFIER = NULL
	, @dataAlteracao DATETIME = NULL
AS
BEGIN

	SELECT
		Usu.usu_id 
		, Usu.usu_login 
		, Usu.usu_email
		, Usu.usu_senha
		, Usu.usu_criptografia
		, Usu.usu_situacao
		, Usu.pes_id
		, Pes.pes_nome 
		, CASE WHEN (@trazerFoto = 1) THEN Arq.arq_data ELSE NULL END AS foto 
		, Pes.pes_sexo
		, Pes.pes_dataNascimento
		, Usu.usu_dataCriacao
		, Usu.usu_dataAlteracao
	FROM
		SYS_Usuario AS Usu WITH(NOLOCK)
		INNER JOIN SYS_UsuarioGrupo AS Usg WITH(NOLOCK)
			ON Usu.usu_id = Usg.usu_id
			AND Usg.usg_situacao <> 3
		INNER JOIN SYS_UsuarioGrupoUA AS Ugu WITH(NOLOCK)
			ON Usg.usu_id = Ugu.usu_id 
			AND Usg.gru_id = Ugu.gru_id
		INNER JOIN SYS_UnidadeAdministrativa AS Uad WITH(NOLOCK)
			ON Ugu.ent_id = Uad.ent_id 
			AND Ugu.uad_id = Uad.uad_id	
			AND Uad.uad_situacao <> 3
		LEFT JOIN PES_Pessoa AS Pes WITH(NOLOCK)
			ON Usu.pes_id = Pes.pes_id
			AND Pes.pes_situacao <> 3	
		LEFT JOIN CFG_Arquivo AS Arq WITH(NOLOCK)
			ON Pes.arq_idFoto = Arq.arq_id
			AND Arq.arq_situacao <> 3	
	WHERE
		Usu.ent_id = @ent_id
		AND (Ugu.uad_id = ISNULL(@uad_id, Ugu.uad_id))
		AND Usu.usu_id = ISNULL(@usu_id, usu.usu_id)
		AND (@uad_codigo IS NULL OR uad_codigo = @uad_codigo)
		AND usu_situacao <> 3
		AND (@dataAlteracao IS NULL OR Usu.usu_dataAlteracao >= @dataAlteracao)
	ORDER BY
		usu_login

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_UPDATE_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 10/02/2010 13:10
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente ao Parametro.
--				Filtrada por: 
--					par_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_UPDATE_Situacao]	
		@par_id uniqueidentifier
		,@par_situacao TINYINT
		,@par_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_Parametro
	SET 
		par_situacao = @par_situacao
		,par_dataAlteracao = @par_dataAlteracao
	WHERE 
	par_id = @par_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_CertidaoCivil_SelectBy_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 10:21
-- Description:	utilizado na busca de certidoes da pessoa, retorna as certidoes
--              da pessoa
--				filtrados por:
--					pes_id
-- Data de alteraçao: 24/06/2011
-- Description: incluido o campo ctc_situacao nos dados que serao retornados
-- Data de alteraçao: 24/09/2012 - Daniel Jun Suguimoto
-- Description: incluido o campo ctc_matricula nos dados que serao retornados
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_CertidaoCivil_SelectBy_pes_id]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT
		pes_id
		, ctc_id
		, ctc_tipo
		, CASE ctc_tipo WHEN 1 THEN 'Certidão de nascimento'
					    WHEN 2 THEN 'Certidão de casamento'
					    ELSE '' END as ctc_tipoDescricao
		, ctc_numeroTermo
		, ctc_folha
		, ctc_livro
		, CONVERT (CHAR,ctc_dataEmissao,103) AS ctc_dataEmissao
		, ctc_nomeCartorio
		, ctc_distritoCartorio
		, ISNULL(cid_idCartorio,'00000000-0000-0000-0000-000000000000') AS cid_idCartorio		
		, cid_nome AS cid_nomeCartorio
		, ISNULL(cid_idCartorio,'00000000-0000-0000-0000-000000000000') AS cid_idAntigo
		, ISNULL(unf_idCartorio,'00000000-0000-0000-0000-000000000000') AS unf_idCartorio		
		, (SELECT unf_nome FROM END_UnidadeFederativa WHERE unf_id = unf_idCartorio) AS unf_nome
		, ISNULL(unf_idCartorio,'00000000-0000-0000-0000-000000000000') AS unf_idAntigo
		, ctc_situacao
		, ctc_matricula
		, ctc_gemeo 
		, ctc_modeloNovo
	FROM
		PES_CertidaoCivil WITH (NOLOCK)
	LEFT JOIN END_Cidade WITH (NOLOCK)
		ON END_Cidade.cid_id = PES_CertidaoCivil.cid_idCartorio
		AND cid_situacao <> 3
	WHERE		
		ctc_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		ctc_tipo
		
	SELECT @@ROWCOUNT	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_SelectBy_par_chave2]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 10/02/2010 09:26
-- Description:	utilizado na busca de parametros
--				filtrados por:
--					par_chave, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_SelectBy_par_chave2]	
	@par_chave VARCHAR(100)
AS
BEGIN
	SELECT 		
		par_id
		,par_chave
		,par_valor
		,CASE par_chave
			WHEN 'PAIS_PADRAO_BRASIL' THEN (SELECT pai_nome FROM END_Pais WHERE pai_id = par_valor)
			WHEN 'ESTADO_PADRAO_SP' THEN (SELECT unf_nome FROM END_UnidadeFederativa  WITH (NOLOCK) WHERE unf_id = par_valor)
			WHEN 'TIPO_DOCUMENTACAO_RG' THEN (SELECT tdo_nome FROM SYS_TipoDocumentacao WHERE tdo_id = par_valor)
			WHEN 'TIPO_DOCUMENTACAO_CPF' THEN (SELECT tdo_nome FROM SYS_TipoDocumentacao WHERE tdo_id = par_valor)
			WHEN 'TIPO_MEIOCONTATO_EMAIL' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WHERE tmc_id = par_valor)
			WHEN 'TIPO_MEIOCONTATO_TELEFONE' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WHERE tmc_id = par_valor)
			WHEN 'TIPO_MEIOCONTATO_SITE' THEN (SELECT tmc_nome FROM SYS_TipoMeioContato WHERE tmc_id = par_valor)
			WHEN 'TAMANHO_MAX_FOTO_PESSOA' THEN par_valor + ' - Kbytes' 
			WHEN 'EXIBIR_LOGO_CLIENTE' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'SALVAR_SEMPRE_MAIUSCULO' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )			
			WHEN 'LOG_ERROS_GRAVAR_SERVERVARIABLES' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOG_ERROS_GRAVAR_PARAMS' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
			WHEN 'LOG_ERROS_CHAVES_NAO_GRAVAR' THEN (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
		 END AS par_valor_nome
		,par_obrigatorio
		,par_vigenciaFim
		,par_vigenciaInicio
		,CONVERT(VARCHAR,par_vigenciaInicio,103) + ' - ' + ISNULL(CONVERT(VARCHAR,par_vigenciaFim,103),'*') AS par_vigencia
		,par_descricao
		
	FROM
		SYS_Parametro WITH (NOLOCK)		
	WHERE
		par_situacao = 1				
		AND (@par_chave IS NULL OR par_chave = @par_chave)
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_UPDATE]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 10/02/2010 13:12
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_UPDATE]
		@par_id uniqueidentifier
		,@par_valor VARCHAR(1000)
		,@par_vigenciaInicio DATE
		,@par_vigenciaFim DATE
		,@par_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_Parametro
	SET 
		par_valor = @par_valor
		,par_vigenciaInicio = @par_vigenciaInicio
		,par_vigenciaFim = @par_vigenciaFim
		,par_dataAlteracao = @par_dataAlteracao
	WHERE 
		par_id = @par_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SYS_IntegracaoExterna]'
GO
CREATE TABLE [dbo].[SYS_IntegracaoExterna]
(
[ine_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__SYS_Integ__ine_i__5AB0ED9C] DEFAULT (newsequentialid()),
[ine_descricao] [varchar] (200) NULL,
[ine_urlInterna] [varchar] (200) NOT NULL,
[ine_urlExterna] [varchar] (200) NOT NULL,
[ine_dominio] [varchar] (100) NOT NULL,
[ine_tipo] [tinyint] NULL,
[ine_chave] [varchar] (10) NOT NULL,
[ine_tokenInterno] [varchar] (50) NULL,
[ine_tokenExterno] [varchar] (50) NULL,
[ine_proxy] [bit] NOT NULL,
[ine_proxyIP] [varchar] (15) NULL,
[ine_proxyPorta] [varchar] (10) NULL,
[ine_proxyAutenticacao] [bit] NOT NULL,
[ine_proxyAutenticacaoUsuario] [varchar] (100) NULL,
[ine_proxyAutenticacaoSenha] [varchar] (100) NULL,
[ine_situacao] [tinyint] NOT NULL CONSTRAINT [DF_SYS_IntegracaoExterna_ine_situacao] DEFAULT ((1)),
[iet_id] [tinyint] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SYS_IntegracaoExterna] on [dbo].[SYS_IntegracaoExterna]'
GO
ALTER TABLE [dbo].[SYS_IntegracaoExterna] ADD CONSTRAINT [PK_SYS_IntegracaoExterna] PRIMARY KEY CLUSTERED  ([ine_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExterna_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExterna_LOAD]
	@ine_id UniqueIdentifier
	
AS
BEGIN
	SELECT	Top 1
		 ine_id  
		, ine_descricao 
		, ine_urlInterna 
		, ine_urlExterna 
		, ine_dominio 
		, ine_tipo 
		, ine_chave 
		, ine_tokenInterno 
		, ine_tokenExterno 
		, ine_proxy 
		, ine_proxyIP 
		, ine_proxyPorta 
		, ine_proxyAutenticacao 
		, ine_proxyAutenticacaoUsuario 
		, ine_proxyAutenticacaoSenha 
		, ine_situacao 
		, iet_id 

 	FROM
 		SYS_IntegracaoExterna
	WHERE 
		ine_id = @ine_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeContato_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 05/02/2010 09:40
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Entidade. Filtrada por: 
--					ent_id, enc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeContato_Update_Situacao]	
		@ent_id uniqueidentifier
		,@enc_id uniqueidentifier
		,@enc_situacao TINYINT
		,@enc_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_EntidadeContato 
	SET 
		enc_situacao = @enc_situacao
		,enc_dataAlteracao = @enc_dataAlteracao
	WHERE 
		ent_id = @ent_id
		and enc_id = @enc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_SelectBy_Vigencia]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 10/02/2010 09:26
-- Description:	utilizado na busca de parametros
--				filtrados por:
--					par_chave, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_SelectBy_Vigencia]
	@par_chave VARCHAR(100)
	,@par_vigenciaInicio DATE
	,@par_vigenciaFim DATE
	,@par_obrigatorio BIT
AS
BEGIN
	IF @par_obrigatorio = 1
	BEGIN
		SELECT	*
		FROM SYS_Parametro WITH (NOLOCK)
		WHERE par_situacao = 1
		AND par_chave = @par_chave
		AND @par_vigenciaInicio <= par_vigenciaInicio
		AND par_id = (SELECT par_id
					  FROM SYS_Parametro WITH (NOLOCK)
				 	  WHERE par_situacao = 1
				 	  AND par_chave = @par_chave
				 	  AND par_dataCriacao = (SELECT MAX(B.par_dataCriacao)
												FROM SYS_Parametro B
												WHERE B.par_chave = @par_chave
												AND B.par_situacao = 1))
	END
	ELSE
	BEGIN
		SELECT *
		FROM
			SYS_Parametro WITH (NOLOCK)		
		WHERE
			par_situacao = 1
			AND (@par_chave IS NULL OR par_chave = @par_chave)
			 AND (
					(@par_vigenciaInicio >= par_vigenciaInicio and (@par_vigenciaInicio <= par_vigenciaFim or par_vigenciaFim is null))
					OR	((@par_vigenciaFim >= par_vigenciaInicio or @par_vigenciaFim is null) and (@par_vigenciaFim <= par_vigenciaFim or @par_vigenciaFim is null or par_vigenciaFim is null)) and (@par_vigenciaFim < par_vigenciaFim)
					OR	(par_vigenciaInicio >= @par_vigenciaInicio and (par_vigenciaInicio <= @par_vigenciaFim or @par_vigenciaFim is null))
					OR	((par_vigenciaFim >= @par_vigenciaInicio or par_vigenciaFim is null) and (par_vigenciaFim <= @par_vigenciaFim or par_vigenciaFim is null or @par_vigenciaFim is null))
				 )
	END
	
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating trigger [dbo].[TRG_CFG_ServidorRelatorio_Identity] on [dbo].[CFG_ServidorRelatorio]'
GO
-- =============================================
-- Author:		Rafael Guilherme Amado
-- Create date: 04/04/2011 17:45
-- Description:	Realiza o autoincremento do 
--				campo srr_id garantindo que
--				sempre será reiniciado em 1
--				qdo um sis_id e ent_id for inserido
-- =============================================
CREATE TRIGGER [dbo].[TRG_CFG_ServidorRelatorio_Identity]
ON [dbo].[CFG_ServidorRelatorio] INSTEAD OF INSERT
AS
BEGIN
	DECLARE @ID INT
	SELECT 
		@ID = CASE WHEN MAX(CFG_ServidorRelatorio.srr_id) IS NULL THEN 1 ELSE MAX(CFG_ServidorRelatorio.srr_id)+1 END 
	FROM 
		CFG_ServidorRelatorio WITH(XLOCK,TABLOCK) 
		INNER JOIN inserted
			ON CFG_ServidorRelatorio.sis_id = inserted.sis_id
			AND CFG_ServidorRelatorio.ent_id = inserted.ent_id
	/* INSERE O ID AUTOINCREMENTO */
	INSERT INTO CFG_ServidorRelatorio (sis_id, ent_id, srr_id, srr_nome, srr_descricao, srr_remoteServer, srr_usuario, srr_senha, srr_dominio, srr_diretorioRelatorios, srr_pastaRelatorios, srr_situacao, srr_dataCriacao, srr_dataAlteracao)
    SELECT sis_id, ent_id, @ID, srr_nome, srr_descricao, srr_remoteServer, srr_usuario, srr_senha, srr_dominio, srr_diretorioRelatorios, srr_pastaRelatorios, srr_situacao, srr_dataCriacao, srr_dataAlteracao FROM inserted
    /* RETORNA INSERT */
    SELECT ISNULL(@ID, -1)     
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExterna_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExterna_INSERT]
	@ine_id UniqueIdentifier
	, @ine_descricao VarChar (200)
	, @ine_urlInterna VarChar (200)
	, @ine_urlExterna VarChar (200)
	, @ine_dominio VarChar (100)
	, @ine_tipo TinyInt
	, @ine_chave VarChar (10)
	, @ine_tokenInterno VarChar (50)
	, @ine_tokenExterno VarChar (50)
	, @ine_proxy Bit
	, @ine_proxyIP VarChar (15)
	, @ine_proxyPorta VarChar (10)
	, @ine_proxyAutenticacao Bit
	, @ine_proxyAutenticacaoUsuario VarChar (100)
	, @ine_proxyAutenticacaoSenha VarChar (100)
	, @ine_situacao TinyInt
	, @iet_id TinyInt

AS
BEGIN
	INSERT INTO 
		SYS_IntegracaoExterna
		( 
			ine_id 
			, ine_descricao 
			, ine_urlInterna 
			, ine_urlExterna 
			, ine_dominio 
			, ine_tipo 
			, ine_chave 
			, ine_tokenInterno 
			, ine_tokenExterno 
			, ine_proxy 
			, ine_proxyIP 
			, ine_proxyPorta 
			, ine_proxyAutenticacao 
			, ine_proxyAutenticacaoUsuario 
			, ine_proxyAutenticacaoSenha 
			, ine_situacao 
			, iet_id 
 
		)
	VALUES
		( 
			@ine_id 
			, @ine_descricao 
			, @ine_urlInterna 
			, @ine_urlExterna 
			, @ine_dominio 
			, @ine_tipo 
			, @ine_chave 
			, @ine_tokenInterno 
			, @ine_tokenExterno 
			, @ine_proxy 
			, @ine_proxyIP 
			, @ine_proxyPorta 
			, @ine_proxyAutenticacao 
			, @ine_proxyAutenticacaoUsuario 
			, @ine_proxyAutenticacaoSenha 
			, @ine_situacao 
			, @iet_id 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeContato_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 08:30
-- Description:	Altera o contato da entidade preservando a data da criação e a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeContato_UPDATE]
		@ent_id uniqueidentifier
		,@enc_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@enc_contato VarChar (200)
		,@enc_situacao TinyInt		
		,@enc_dataAlteracao DateTime

AS
BEGIN
	UPDATE SYS_EntidadeContato
	SET 
		tmc_id = @tmc_id
		,enc_contato = @enc_contato
		,enc_situacao = @enc_situacao		
		,enc_dataAlteracao = @enc_dataAlteracao
	WHERE 
		ent_id = @ent_id
		AND enc_id = @enc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_UPDATE_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 25/01/2010 10:32
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Unidade Administrativa. Filtrada por: 
--					@tua_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_UPDATE_Situacao]	
		@tua_id uniqueidentifier
		,@tua_situacao TINYINT
		,@tua_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoUnidadeAdministrativa
	SET 
		tua_situacao = @tua_situacao
		,tua_dataAlteracao = @tua_dataAlteracao
	WHERE 
		tua_id = @tua_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExterna_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExterna_UPDATE]
	@ine_id UNIQUEIDENTIFIER
	, @ine_descricao VARCHAR (200)
	, @ine_urlInterna VARCHAR (200)
	, @ine_urlExterna VARCHAR (200)
	, @ine_dominio VARCHAR (100)
	, @ine_tipo TINYINT
	, @ine_chave VARCHAR (10)
	, @ine_tokenInterno VARCHAR (50)
	, @ine_tokenExterno VARCHAR (50)
	, @ine_proxy BIT
	, @ine_proxyIP VARCHAR (15)
	, @ine_proxyPorta VARCHAR (10)
	, @ine_proxyAutenticacao BIT
	, @ine_proxyAutenticacaoUsuario VARCHAR (100)
	, @ine_proxyAutenticacaoSenha VARCHAR (100)
	, @ine_situacao TINYINT
	, @iet_id TINYINT

AS
BEGIN
	UPDATE SYS_IntegracaoExterna 
	SET 
		ine_descricao = @ine_descricao 
		, ine_urlInterna = @ine_urlInterna 
		, ine_urlExterna = @ine_urlExterna 
		, ine_dominio = @ine_dominio 
		, ine_tipo = @ine_tipo 
		, ine_chave = @ine_chave 
		, ine_tokenInterno = @ine_tokenInterno 
		, ine_tokenExterno = @ine_tokenExterno 
		, ine_proxy = @ine_proxy 
		, ine_proxyIP = @ine_proxyIP 
		, ine_proxyPorta = @ine_proxyPorta 
		, ine_proxyAutenticacao = @ine_proxyAutenticacao 
		, ine_proxyAutenticacaoUsuario = @ine_proxyAutenticacaoUsuario 
		, ine_proxyAutenticacaoSenha = @ine_proxyAutenticacaoSenha 
		, ine_situacao = @ine_situacao 
		, iet_id = @iet_id 

	WHERE 
		ine_id = @ine_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_UPDATE]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 25/01/2010 10:33
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_UPDATE]
		@tua_id uniqueidentifier
		,@tua_nome VarChar (100)
		,@tua_situacao TINYINT
		,@tua_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoUnidadeAdministrativa 
	SET 
		tua_nome = @tua_nome
		,tua_situacao = @tua_situacao
		,tua_dataAlteracao = @tua_dataAlteracao
	WHERE 
		tua_id = @tua_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_ServidorRelatorio_SelectBy_BuscaConfigRelatorio]'
GO
-- ===================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/04/2011 16:10
-- Description:	Lista os servidor de relatório filtrados por sistema,
--				entidade e nome do servidor de relatório, onde,
--				sistema e nome podem ser nulos. o id da entidade é
--				obrigatório. Procedure especifica para a página de 
--				busca do módulo de configurações de servidor de 
--				relatório do sistem CoreSSO.
-- ===================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_ServidorRelatorio_SelectBy_BuscaConfigRelatorio]
	@ent_id UNIQUEIDENTIFIER
	, @sis_id INT
	, @srr_nome VARCHAR(100)
AS
BEGIN
	SELECT
		R.ent_id
		, R.sis_id
		, R.srr_id
		, R.srr_nome
		, CASE R.srr_situacao WHEN 1 THEN 'Ativo' ELSE 'Bloqueado' END AS srr_situacao
		, S.sis_nome
	FROM
		CFG_ServidorRelatorio R WITH(NOLOCK)
		INNER JOIN SYS_Sistema S WITH(NOLOCK)
			ON R.sis_id = S.sis_id AND S.sis_situacao = 1
	WHERE
		R.srr_situacao <> 3
		AND R.ent_id = @ent_id
		AND ((@sis_id = -1) OR (R.sis_id = @sis_id))
		AND ((@srr_nome = '') OR (R.srr_nome LIKE '%' + @srr_nome + '%'))
	ORDER BY
		R.srr_nome		
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_mod_id]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi	
-- Create date: 20/07/2010
-- Description:	Retorna todos os itens do ModuloSiteMap de determinado módulo e sistema.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_SelectBy_mod_id]
	@sis_id int
	, @mod_id int
AS
BEGIN
	SELECT	DISTINCT 
			msm.sis_id
			, msm.mod_id
			, msm.msm_id
			, msm.msm_nome
			, msm.msm_descricao
			, msm.msm_url
			, Cast(msm.msm_informacoes as Varchar(8000)) msm_informacoes	
			, msm_urlHelp		
	FROM
		SYS_ModuloSiteMap msm WITH (NOLOCK)
	LEFT JOIN SYS_VisaoModuloMenu vmm WITH (NOLOCK)
		ON msm.sis_id = vmm.sis_id
		AND msm.mod_id = vmm.mod_id
		AND msm.msm_id = vmm.msm_id	
	WHERE
		msm.sis_id = @sis_id
		AND msm.mod_id = @mod_id 
	GROUP BY
		msm.sis_id
		, msm.mod_id
		, msm.msm_id
		, msm.msm_nome
		, msm.msm_descricao
		, msm.msm_url
		, Cast(msm.msm_informacoes as Varchar(8000))
		, ISNULL(vmm.vmm_ordem, 0)
		, msm_urlHelp
	ORDER BY
		msm.msm_nome
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExterna_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExterna_DELETE]
	@ine_id UNIQUEIDENTIFIER

AS
BEGIN
	DELETE FROM 
		SYS_IntegracaoExterna 
	WHERE 
		ine_id = @ine_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_EntidadeContato_SelectBy_ent_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/01/2010 16:45
-- Description:	utilizado na busca de contatos da entidade, retorna os contatos
--              da entidade
--				filtrados por:
--					ent_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_EntidadeContato_SelectBy_ent_id]	
	@ent_id uniqueidentifier
AS
BEGIN
	SELECT		
		enc_id as id
		,SYS_EntidadeContato.tmc_id
		,tmc_nome
		,enc_contato as contato		
	FROM
		SYS_EntidadeContato WITH (NOLOCK)
	INNER JOIN
		SYS_TipoMeioContato WITH (NOLOCK) on SYS_TipoMeioContato.tmc_id = SYS_EntidadeContato.tmc_id
	WHERE		
		enc_situacao <> 3
		AND tmc_situacao <> 3
		AND ent_id = @ent_id
	ORDER BY
		tmc_nome, enc_contato
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_SelectBy_Nome]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 27/01/2010 14:00
-- Description: utilizado na busca de nome de tipos de unidade administrativa,
--				retorna quantidade dos tipos de unidade administrativa que não
--				foram excluídos logicamente, filtrados por:
--					nome, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_SelectBy_Nome]	
	@tua_nome VARCHAR(100)	
	,@tua_id_alteracao uniqueidentifier
AS
BEGIN
	SELECT 
		tua_id
		, tua_nome
		, tua_situacao
		, tua_dataCriacao
		, tua_dataAlteracao
		, tua_integridade
	FROM
		SYS_TipoUnidadeAdministrativa WITH (NOLOCK)		
	WHERE
		tua_situacao <> 3
		AND UPPER(tua_nome) = UPPER(@tua_nome)
		AND (@tua_id_alteracao is null or tua_id <> @tua_id_alteracao)		
	ORDER BY
		tua_nome
		
	SELECT @@ROWCOUNT					
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_ServidorRelatorio_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_ServidorRelatorio_LOAD]
	@sis_id Int
	, @ent_id UniqueIdentifier
	, @srr_id Int
	
AS
BEGIN
	SELECT	Top 1
		 sis_id  
		, ent_id 
		, srr_id 
		, srr_nome 
		, srr_descricao 
		, srr_remoteServer 
		, srr_usuario 
		, srr_senha 
		, srr_dominio 
		, srr_diretorioRelatorios 
		, srr_pastaRelatorios 
		, srr_situacao 
		, srr_dataCriacao 
		, srr_dataAlteracao 

 	FROM
 		CFG_ServidorRelatorio
	WHERE 
		sis_id = @sis_id
		AND ent_id = @ent_id
		AND srr_id = @srr_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaEndereco_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 10/02/2010 15:10
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Endereço da Pessoa. Filtrada por: 
--					pes_id, pse_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaEndereco_Update_Situacao]	
		@pes_id uniqueidentifier
		,@pse_id uniqueidentifier
		,@pse_situacao TINYINT
		,@pse_dataAlteracao DATETIME
AS
BEGIN
	UPDATE PES_PessoaEndereco 
	SET 
		pse_situacao = @pse_situacao
		,pse_dataAlteracao = @pse_dataAlteracao
	WHERE 
		pes_id = @pes_id
		AND pse_id = @pse_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_IntegracaoExterna_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_SYS_IntegracaoExterna_SELECT]
	
AS
BEGIN
	SELECT 
		ine_id
		,ine_descricao
		,ine_urlInterna
		,ine_urlExterna
		,ine_dominio
		,ine_tipo
		,ine_chave
		,ine_tokenInterno
		,ine_tokenExterno
		,ine_proxy
		,ine_proxyIP
		,ine_proxyPorta
		,ine_proxyAutenticacao
		,ine_proxyAutenticacaoUsuario
		,ine_proxyAutenticacaoSenha
		,ine_situacao
		,iet_id

	FROM 
		SYS_IntegracaoExterna WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_Update_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 28/04/2010 11:46
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente a cidade. Filtrada por:
--					cid_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_Update_Situacao]	
		@cid_id uniqueidentifier
		,@cid_situacao TINYINT
AS
BEGIN
	UPDATE END_Cidade 
	SET 
		cid_situacao = @cid_situacao
	WHERE 
		cid_id = @cid_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_TemaPadrao]'
GO
CREATE TABLE [dbo].[CFG_TemaPadrao]
(
[tep_id] [int] NOT NULL IDENTITY(1, 1),
[tep_nome] [varchar] (100) NOT NULL,
[tep_descricao] [varchar] (200) NULL,
[tep_tipoMenu] [tinyint] NOT NULL CONSTRAINT [DF_CFG_TemaPadrao_tep_tipoMenu] DEFAULT ((1)),
[tep_exibeLinkLogin] [bit] NOT NULL,
[tep_tipoLogin] [tinyint] NOT NULL CONSTRAINT [DF_CFG_TemaPadrao_tep_tipoLogin] DEFAULT ((1)),
[tep_exibeLogoCliente] [bit] NOT NULL CONSTRAINT [DF_CFG_TemaPadrao_tep_exibeLogoCliente] DEFAULT ((0)),
[tep_situacao] [tinyint] NOT NULL,
[tep_dataCriacao] [datetime] NOT NULL,
[tep_dataAlteracao] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_TemaPadrao] on [dbo].[CFG_TemaPadrao]'
GO
ALTER TABLE [dbo].[CFG_TemaPadrao] ADD CONSTRAINT [PK_CFG_TemaPadrao] PRIMARY KEY CLUSTERED  ([tep_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPadrao_SelecionaAtivos]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 15/01/2015
-- Description:	Seleciona todos os temas ativos.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPadrao_SelecionaAtivos] 
AS
BEGIN
	SELECT 
		tep.tep_id,
		tep.tep_nome,
		tep.tep_descricao,
		tep.tep_tipoMenu,
		tep.tep_exibeLinkLogin,
		tep.tep_tipoLogin,
		tep.tep_exibeLogoCliente,
		tep.tep_situacao,
		tep.tep_dataCriacao,
		tep.tep_dataAlteracao 
	FROM
		CFG_TemaPadrao tep WITH(NOLOCK)
	WHERE
		tep.tep_situacao = 1
	ORDER BY
		tep.tep_nome
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_SelectBy_All]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 25/01/2010 10:42
-- Description:	utilizado na busca de tipos de entidade, retorna os tipos
--              de entidade que não foram excluídos logicamente,
--				filtrados por:
--					id, nome, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_SelectBy_All]	
	@tua_id uniqueidentifier
	,@tua_nome VARCHAR(100)	
	,@tua_situacao TINYINT
AS
BEGIN
	SELECT 
		tua_id
		,tua_nome
		, CASE tua_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS tua_situacao
	FROM
		SYS_TipoUnidadeAdministrativa WITH (NOLOCK)	
	WHERE
		tua_situacao <> 3
		AND (@tua_id is null or tua_id = @tua_id)		
		AND (@tua_nome is null or tua_nome LIKE '%' + @tua_nome + '%')		
		AND (@tua_situacao is null or tua_situacao = @tua_situacao)				
	ORDER BY
		tua_nome
		
	SELECT @@ROWCOUNT					
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_ServidorRelatorio_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_ServidorRelatorio_INSERT]
	@sis_id Int
	, @ent_id UniqueIdentifier
	, @srr_id Int
	, @srr_nome VarChar (100)
	, @srr_descricao VarChar (1000)
	, @srr_remoteServer Bit
	, @srr_usuario VarChar (512)
	, @srr_senha VarChar (512)
	, @srr_dominio VarChar (512)
	, @srr_diretorioRelatorios VarChar (1000)
	, @srr_pastaRelatorios VarChar (1000)
	, @srr_situacao TinyInt
	, @srr_dataCriacao DateTime
	, @srr_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		CFG_ServidorRelatorio
		( 
			sis_id 
			, ent_id 
			, srr_id 
			, srr_nome 
			, srr_descricao 
			, srr_remoteServer 
			, srr_usuario 
			, srr_senha 
			, srr_dominio 
			, srr_diretorioRelatorios 
			, srr_pastaRelatorios 
			, srr_situacao 
			, srr_dataCriacao 
			, srr_dataAlteracao 
 
		)
	VALUES
		( 
			@sis_id 
			, @ent_id 
			, @srr_id 
			, @srr_nome 
			, @srr_descricao 
			, @srr_remoteServer 
			, @srr_usuario 
			, @srr_senha 
			, @srr_dominio 
			, @srr_diretorioRelatorios 
			, @srr_pastaRelatorios 
			, @srr_situacao 
			, @srr_dataCriacao 
			, @srr_dataAlteracao 
 
		)
		
	--SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_UnidadeFederativa_SelectBy_unf_nome]'
GO
-- ========================================================================
-- Author:		Jéssica Sartori
-- Create date: 11/08/2016
-- Description:	utilizado no cadastro de unidades federativas,
--              para saber se a unidade federativa já está cadastrada
--				filtrados por:
--					unf_id (diferente do parametro), 					 
--                  pai_id, unf_nome, unf_situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_UnidadeFederativa_SelectBy_unf_nome]
	  @unf_id       UNIQUEIDENTIFIER
	, @pai_id       UNIQUEIDENTIFIER
	, @unf_nome     VARCHAR(200)
	, @unf_situacao TINYINT		
AS
BEGIN
	SELECT 
		  unf_id	
		, unf_nome	
	FROM
		END_UnidadeFederativa WITH (NOLOCK)		
	WHERE
		unf_situacao <> 3
		AND (@unf_id is null or unf_id <> @unf_id)								
		AND (@pai_id is null or pai_id = @pai_id)	
		AND (@unf_nome is null or unf_nome = @unf_nome)					
		AND (@unf_situacao is null or unf_situacao = @unf_situacao)						
	ORDER BY
		unf_nome
		
	SELECT @@ROWCOUNT		
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaEndereco_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 16:47
-- Description:	Altera o endereço da pessoa preservando
--				a data da criação
--
--
-- Alteração
-- Author : Gabriel Scavassa
-- Updated date: 11/02/2016 14:00
-- Description: Adição de endereço principal, latitude e longitude
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaEndereco_UPDATE]
		@pes_id uniqueidentifier
			, @pse_id uniqueidentifier
			, @end_id uniqueidentifier
			, @pse_numero varchar(20)
			, @pse_complemento varchar(100)
			, @pse_situacao tinyint			
			, @pse_dataAlteracao datetime
			, @pse_enderecoPrincipal Bit = NULL
		    , @pse_latitude Decimal(15,10) = NULL
	        , @pse_longitude Decimal(15,10) = NULL

AS
BEGIN
	UPDATE PES_PessoaEndereco
	SET 		
		end_id = @end_id
			, pse_numero = @pse_numero
			, pse_complemento = @pse_complemento
			, pse_situacao = @pse_situacao			
			, pse_dataAlteracao = @pse_dataAlteracao
			, pse_enderecoPrincipal = @pse_enderecoPrincipal 
		    , pse_latitude = @pse_latitude 
		    , pse_longitude = @pse_longitude 
	WHERE 
		pes_id = @pes_id
		AND pse_id = @pse_id

	RETURN ISNULL(@@ROWCOUNT,-1)
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_IntegracaoExterna_SelectBy_All]'
GO
-- ============================================================
-- Author:		Flavio
-- Create date: 01/02/11
-- Description:	Utilizado na busca de Integração
--				externa
-- ============================================================
CREATE PROCEDURE [dbo].[NEW_SYS_IntegracaoExterna_SelectBy_All]
	@ine_tipo TINYINT 
AS
BEGIN
	SELECT
	 ine_id,
	 ine_descricao,
	 ine_urlInterna,
	 ine_tokenInterno,
	 ine_urlExterna,
	 ine_tokenExterno
	 ine_dominio,
	 ine_tipo,
	 ine_chave,
	 ine_proxy,
	 CASE ine_proxy 
	 	WHEN 0 THEN 'Não'
	 	WHEN 1 THEN 'Sim'			
	 END AS ine_proxyDesc,
	 ine_proxyIP,
	 ine_proxyPorta,
	 ine_proxyAutenticacao,
	 CASE ine_proxyAutenticacao 
	 	WHEN 0 THEN 'Não'
	 	WHEN 1 THEN 'Sim'			
	 END AS ine_proxyAutenticacaoDesc,
	 ine_proxyAutenticacaoUsuario,
	 ine_proxyAutenticacaoSenha,
	 ine_situacao,
	 CASE ine_situacao 
	 	WHEN 1 THEN 'Ativo'	
	 	WHEN 2 THEN 'Inativo'
	 END AS ine_situacaoDesc,
	 iet_id
	 FROM 
		SYS_IntegracaoExterna WITH(NOLOCK) 
	 WHERE 
		ine_situacao <> 3
		AND
		(@ine_tipo is null or ine_tipo = @ine_tipo)
		
	 SELECT @@ROWCOUNT
			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 15:57
-- Description:	Altera a cidade preservando a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_UPDATE]
		@cid_id uniqueidentifier
		,@pai_id uniqueidentifier
		,@unf_id uniqueidentifier
		,@cid_nome VarChar (200)
		,@cid_ddd VarChar(3)
		,@cid_situacao TinyInt		
AS
BEGIN
	UPDATE END_Cidade
	SET 		
		pai_id = @pai_id
		,unf_id = @unf_id
		,cid_nome = @cid_nome
		,cid_ddd = @cid_ddd
		,cid_situacao = @cid_situacao		
	WHERE 
		cid_id = @cid_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:20
-- Description:	Seleciona o valor do campo integridade da tabela de tipo de unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_Select_Integridade]
		@tua_id uniqueidentifier
AS
BEGIN
	SELECT 			
		tua_integridade
	FROM
		SYS_TipoUnidadeAdministrativa WITH (NOLOCK)
	WHERE 
		tua_id = @tua_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_ServidorRelatorio_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_ServidorRelatorio_UPDATE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT
	, @srr_nome VARCHAR (100)
	, @srr_descricao VARCHAR (1000)
	, @srr_remoteServer BIT
	, @srr_usuario VARCHAR (512)
	, @srr_senha VARCHAR (512)
	, @srr_dominio VARCHAR (512)
	, @srr_diretorioRelatorios VARCHAR (1000)
	, @srr_pastaRelatorios VARCHAR (1000)
	, @srr_situacao TINYINT
	, @srr_dataCriacao DATETIME
	, @srr_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_ServidorRelatorio 
	SET 
		srr_nome = @srr_nome 
		, srr_descricao = @srr_descricao 
		, srr_remoteServer = @srr_remoteServer 
		, srr_usuario = @srr_usuario 
		, srr_senha = @srr_senha 
		, srr_dominio = @srr_dominio 
		, srr_diretorioRelatorios = @srr_diretorioRelatorios 
		, srr_pastaRelatorios = @srr_pastaRelatorios 
		, srr_situacao = @srr_situacao 
		, srr_dataCriacao = @srr_dataCriacao 
		, srr_dataAlteracao = @srr_dataAlteracao 

	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_IntegracaoExterna_SelectBy_ine_tipo]'
GO
-- ==================================================================
-- Author:		Aline Dornelas
-- Create date: 03/02/2011 11:04:00
-- Description:	Utilizado na busca de Integração Externa, filtrando
--				pelo tipo de integração externa
-- ==================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_IntegracaoExterna_SelectBy_ine_tipo]
	@ine_tipo TINYINT 
AS
BEGIN
	SELECT
		*
	 FROM 
		SYS_IntegracaoExterna WITH(NOLOCK) 
	 WHERE 
		ine_situacao = 1
		AND ine_tipo = @ine_tipo
		
	 SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_SelectBy_cid_nome]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 02/02/2010 09:00
-- Description:	utilizado no cadastro de cidades,
--              para saber se a cidade já está cadastrada
--				filtrados por:
--					cid_id (diferente do parametro), 					 
--                  pai_id, unf_id, cid_nome, cid_situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_SelectBy_cid_nome]
	@cid_id uniqueidentifier
	,@pai_id uniqueidentifier
	,@unf_id uniqueidentifier
	,@cid_nome VARCHAR(200)
	,@cid_situacao TINYINT		
AS
BEGIN
	SELECT 
		cid_id	
		,cid_nome	
	FROM
		END_Cidade WITH (NOLOCK)		
	WHERE
		cid_situacao <> 3
		AND (@cid_id is null or cid_id <> @cid_id)								
		AND (@pai_id is null or pai_id = @pai_id)	
		AND (@unf_id is null or unf_id = @unf_id)	
		AND (@cid_nome is null or cid_nome = @cid_nome)					
		AND (@cid_situacao is null or cid_situacao = @cid_situacao)						
	ORDER BY
		cid_nome
		
	SELECT @@ROWCOUNT		
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPadrao_VerificaExistePorNome]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 20/01/2015
-- Description:	Verifica se já existe um tema com o mesmo nome.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPadrao_VerificaExistePorNome] 
	@tep_id INT,
	@tep_nome VARCHAR(100)
AS
BEGIN
	IF (EXISTS
		(
			SELECT 
				tep_id
			FROM
				CFG_TemaPadrao tep WITH(NOLOCK)
			WHERE
				tep.tep_id <> ISNULL(@tep_id, -1)
				AND LTRIM(RTRIM(tep.tep_nome)) = LTRIM(RTRIM(@tep_nome))
				AND tep.tep_situacao <> 3
		))
	BEGIN
		RETURN 1;
	END
	ELSE
	BEGIN
		RETURN 0;
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:22
-- Description:	Incrementa uma unidade no campo integridade da tabela de tipo unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_INCREMENTA_INTEGRIDADE]
		@tua_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_TipoUnidadeAdministrativa
	SET 
		tua_integridade = tua_integridade + 1
	WHERE 
		tua_id = @tua_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_ServidorRelatorio_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_ServidorRelatorio_DELETE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT

AS
BEGIN
	DELETE FROM 
		CFG_ServidorRelatorio 
	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaEndereco_SelectBy_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 09/02/2010 17:45
-- Description:	utilizado na busca de endereços da pessoa, retorna os 
--              endereços da pessoa
--				filtrados por:
--					pes_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaEndereco_SelectBy_pes_id]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT
		pse_id as id
		, END_Endereco.end_id
		, END_Endereco.cid_id
		, end_cep
		, end_logradouro
		, pse_numero as numero
		, pse_complemento as complemento
		, pse_enderecoPrincipal
		, pse_latitude
		, pse_longitude
		, end_distrito
		, end_zona
		, end_bairro
		, cid_nome	
		, unf_sigla
		, pai_nome
	FROM
		PES_PessoaEndereco WITH (NOLOCK)
	INNER JOIN
		END_Endereco WITH (NOLOCK)
			ON PES_PessoaEndereco.end_id = END_Endereco.end_id
	INNER JOIN
		END_Cidade WITH (NOLOCK)
			ON END_Endereco.cid_id = END_Cidade.cid_id
	INNER JOIN
		END_UnidadeFederativa WITH (NOLOCK)
			ON END_UnidadeFederativa.unf_id = END_Cidade.unf_id
	INNER JOIN
		END_Pais WITH (NOLOCK)
			ON END_Pais.pai_id = END_Cidade.pai_id
	WHERE		
		pse_situacao <> 3
		AND end_situacao <> 3
		AND cid_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		end_cep, end_logradouro
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/01/2010 13:10
-- Description:	utilizado na busca de cidades, retorna as cidades
--              que não foram excluídas logicamente,
--				filtradas por:
--					cid_id, unf_id, pai_id, cidade, estado, sigla do estado, 
--					pais e situação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_SelectBy_All]		
	@cid_id uniqueidentifier
	,@unf_id uniqueidentifier
	,@pai_id uniqueidentifier
	,@cid_nome VARCHAR(200)
	,@unf_nome VARCHAR(100)
	,@unf_sigla VARCHAR(2)
	,@pai_nome VARCHAR(100)
	,@cid_situacao TINYINT	
AS
BEGIN
	SELECT 
		END_Cidade.cid_id
		,END_Cidade.unf_id
		,END_Cidade.pai_id
		,cid_nome
		,cid_ddd
		,unf_nome				    		
		,unf_sigla		
		,pai_nome	
		, CASE cid_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS cid_situacao
	FROM
		END_Cidade WITH (NOLOCK)
	LEFT JOIN
		END_UnidadeFederativa WITH (NOLOCK) on END_Cidade.unf_id = END_UnidadeFederativa.unf_id AND unf_situacao <> 3
	INNER JOIN
		END_Pais WITH (NOLOCK) on END_Cidade.pai_id = END_Pais.pai_id
	WHERE
		cid_situacao <> 3		
		AND pai_situacao <> 3
		AND (@cid_id is null or END_Cidade.cid_id = @cid_id)						
		AND (@unf_id is null or END_Cidade.unf_id = @unf_id)		
		AND (@pai_id is null or END_Cidade.pai_id = @pai_id)
		AND (@cid_nome is null or cid_nome LIKE '%' + @cid_nome + '%')
		AND (@unf_nome is null or unf_nome LIKE '%' + @unf_nome + '%')					
		AND (@unf_sigla is null or unf_sigla = @unf_sigla)						
		AND (@pai_nome is null or pai_nome LIKE '%' + @pai_nome + '%')		
		AND (@cid_situacao is null or cid_situacao = @cid_situacao)				
	ORDER BY
		pai_nome, isnull(unf_nome,''), cid_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoUnidadeAdministrativa_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:16
-- Description:	Decrementa uma unidade no campo integridade da tabela de tipo unidade administrativa
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoUnidadeAdministrativa_DECREMENTA_INTEGRIDADE]
		@tua_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_TipoUnidadeAdministrativa
	SET 
		tua_integridade = tua_integridade - 1
	WHERE 
		tua_id = @tua_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_ServidorRelatorio_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_ServidorRelatorio_SELECT]
	
AS
BEGIN
	SELECT 
		sis_id
		,ent_id
		,srr_id
		,srr_nome
		,srr_descricao
		,srr_remoteServer
		,srr_usuario
		,srr_senha
		,srr_dominio
		,srr_diretorioRelatorios
		,srr_pastaRelatorios
		,srr_situacao
		,srr_dataCriacao
		,srr_dataAlteracao

	FROM 
		CFG_ServidorRelatorio WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_mod_id_Filhos]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 20/07/2010
-- Description:	Select filtrando módulos pais
-- =============================================
CREATE PROCEDURE  [dbo].[NEW_SYS_Modulo_SelectBy_mod_id_Filhos]
     @sis_id INT
	, @mod_id INT	
AS
BEGIN
	SELECT
		modu.mod_id
		, modu.mod_nome
		, vmm.vmm_ordem
	FROM
		SYS_Modulo modu WITH (NOLOCK)
	INNER JOIN SYS_ModuloSiteMap msm WITH (NOLOCK)
		ON modu.mod_id = msm.mod_id
		AND modu.sis_id = msm.sis_id
	INNER JOIN SYS_VisaoModuloMenu vmm WITH (NOLOCK)
		ON msm.msm_id = vmm.msm_id
		AND msm.mod_id = vmm.mod_id
		AND msm.sis_id = vmm.sis_id
	WHERE
		modu.mod_situacao <> 3
		AND (COALESCE(modu.mod_idpai, 0) = COALESCE(@mod_id, 0))
		AND (modu.sis_id = @sis_id)	
		AND vmm.vis_id = (SELECT vis_id FROM SYS_Visao WITH (NOLOCK) WHERE vis_nome = 'Administração')
	GROUP BY
		modu.mod_id, modu.mod_nome , vmm.vmm_ordem	
	UNION 
	SELECT
		modu.mod_id
		, mod_nome
		, 1000 AS vmm_ordem
	FROM
		SYS_Modulo modu WITH (NOLOCK)
	LEFT JOIN SYS_ModuloSiteMap msm WITH (NOLOCK)
		ON modu.mod_id = msm.mod_id 
		AND modu.sis_id = msm.sis_id
	WHERE
		modu.mod_situacao <> 3
		AND (COALESCE(modu.mod_idpai, 0) = COALESCE(@mod_id, 0))
		AND (modu.sis_id = @sis_id)	
		AND msm_id IS NULL
	UNION 
	SELECT
		modu.mod_id
		, mod_nome
		, 1000 AS vmm_ordem
	FROM
		SYS_Modulo modu WITH (NOLOCK)
	LEFT JOIN SYS_VisaoModuloMenu vmm WITH (NOLOCK)
		ON modu.mod_id = vmm.mod_id
		AND modu.sis_id = vmm.sis_id
		AND vmm.vis_id = (SELECT vis_id FROM SYS_Visao WITH (NOLOCK) WHERE vis_nome = 'Administração')
	WHERE
		modu.mod_situacao <> 3
		AND (COALESCE(modu.mod_idpai, 0) = COALESCE(@mod_id, 0))
		AND (modu.sis_id = @sis_id)	
		AND vmm.vmm_ordem IS NULL		
	GROUP BY
		modu.mod_id, modu.mod_nome , modu.mod_id
	ORDER BY 
		vmm_ordem
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 11/02/2010 10:25
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Pessoa. Filtrada por: 
--					pes_id, tdo_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_Update_Situacao]	
		@pes_id uniqueidentifier
		,@tdo_id uniqueidentifier
		,@psd_situacao TINYINT
		,@psd_dataAlteracao DATETIME
AS
BEGIN
	UPDATE PES_PessoaDocumento 
	SET 
		psd_situacao = @psd_situacao
		,psd_dataAlteracao = @psd_dataAlteracao
	WHERE 
		pes_id = @pes_id
		and tdo_id = @tdo_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:35
-- Description:	Seleciona o valor do campo integridade da tabela de cidades
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_Select_Integridade]
		@cid_id uniqueidentifier
AS
BEGIN
	SELECT 			
		cid_integridade
	FROM
		END_Cidade WITH (NOLOCK)
	WHERE 
		cid_id = @cid_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_UPDATE_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 26/01/2010 09:32
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Meio Contato. Filtrada por: 
--					tmc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_UPDATE_Situacao]	
		@tmc_id uniqueidentifier
		,@tmc_situacao TINYINT
		,@tmc_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoMeioContato
	SET 
		tmc_situacao = @tmc_situacao
		,tmc_dataAlteracao = @tmc_dataAlteracao
	WHERE 
		tmc_id = @tmc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_ServidorRelatorio_UPDATE]'
GO
-- =======================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/04/2011 17:35
-- Description:	Altera os dados do servidor sem alterar a 
--				data da criação do registro.
-- =======================================================
CREATE PROCEDURE [dbo].[NEW_CFG_ServidorRelatorio_UPDATE]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT
	, @srr_nome VARCHAR (100)
	, @srr_descricao VARCHAR (1000)
	, @srr_remoteServer BIT
	, @srr_usuario VARCHAR (512)
	, @srr_senha VARCHAR (512)
	, @srr_dominio VARCHAR (512)
	, @srr_diretorioRelatorios VARCHAR (1000)
	, @srr_pastaRelatorios VARCHAR (1000)
	, @srr_situacao TINYINT
	, @srr_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_ServidorRelatorio 
	SET 
		srr_nome = @srr_nome 
		, srr_descricao = @srr_descricao 
		, srr_remoteServer = @srr_remoteServer 
		, srr_usuario = @srr_usuario 
		, srr_senha = @srr_senha 
		, srr_dominio = @srr_dominio 
		, srr_diretorioRelatorios = @srr_diretorioRelatorios 
		, srr_pastaRelatorios = @srr_pastaRelatorios 
		, srr_situacao = @srr_situacao 
		, srr_dataAlteracao = @srr_dataAlteracao 

	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 16:45
-- Description:	Altera o documento da pessoa preservando
--				a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_UPDATE]
		@pes_id uniqueidentifier
		,@tdo_id uniqueidentifier
		,@psd_numero VarChar (50)
		,@psd_dataEmissao Date
		,@psd_orgaoEmissao VarChar (200)
		,@unf_idEmissao uniqueidentifier
		,@psd_infoComplementares VarChar (1000)
		,@psd_situacao TinyInt		
		,@psd_dataAlteracao DateTime
		,@psd_categoria VARCHAR (64) = NULL
		,@psd_classificacao VARCHAR (64) = NULL
		,@psd_csm VARCHAR (32) = NULL
		,@psd_dataEntrada DATETIME = NULL
		,@psd_dataValidade DATETIME = NULL
		,@pai_idOrigem UNIQUEIDENTIFIER = NULL
		,@psd_serie VARCHAR (32) = NULL
		,@psd_tipoGuarda VARCHAR (128) = NULL
		,@psd_via VARCHAR (16) = NULL
		,@psd_secao VARCHAR (32) = NULL
		,@psd_zona VARCHAR (16) = NULL
		,@psd_regiaoMilitar VARCHAR (16) = NULL
		,@psd_numeroRA VARCHAR (64) = NULL
		,@psd_dataExpedicao Date = NULL
AS
BEGIN
	UPDATE PES_PessoaDocumento
	SET 		
		psd_numero = @psd_numero
		,psd_dataEmissao = @psd_dataEmissao
		,psd_orgaoEmissao = @psd_orgaoEmissao
		,unf_idEmissao = @unf_idEmissao
		,psd_infoComplementares = @psd_infoComplementares
		,psd_situacao = @psd_situacao		
		,psd_dataAlteracao = @psd_dataAlteracao
		,psd_categoria = @psd_categoria 
		,psd_classificacao = @psd_classificacao 
		,psd_csm = @psd_csm 
		,psd_dataEntrada = @psd_dataEntrada 
		,psd_dataValidade = @psd_dataValidade 
		,pai_idOrigem = @pai_idOrigem 
		,psd_serie = @psd_serie 
		,psd_tipoGuarda = @psd_tipoGuarda 
		,psd_via = @psd_via 
		,psd_secao = @psd_secao 
		,psd_zona = @psd_zona 
		,psd_regiaoMilitar = @psd_regiaoMilitar 
		,psd_numeroRA = @psd_numeroRA
		,psd_dataExpedicao = @psd_dataExpedicao 
	WHERE 
		pes_id = @pes_id
		AND	tdo_id = @tdo_id

	RETURN ISNULL(@@ROWCOUNT,-1)
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:40
-- Description:	Incrementa uma unidade no campo integridade da tabela de cidades
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_INCREMENTA_INTEGRIDADE]
		@cid_id uniqueidentifier

AS
BEGIN
	UPDATE END_Cidade
	SET 
		cid_integridade = cid_integridade + 1
	WHERE 
		cid_id = @cid_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPaleta_UPDATE]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_TemaPaleta_UPDATE]
	@tep_id INT
	, @tpl_id INT
	, @tpl_nome VARCHAR (100)
	, @tpl_caminhoCSS VARCHAR(1000)
	, @tpl_imagemExemploTema VARCHAR (2000)
	, @tpl_situacao TINYINT
	, @tpl_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPaleta 
	SET 
		tpl_nome = @tpl_nome 
		, tpl_caminhoCSS = @tpl_caminhoCSS 
		, tpl_imagemExemploTema = @tpl_imagemExemploTema 
		, tpl_situacao = @tpl_situacao 
		, tpl_dataAlteracao = @tpl_dataAlteracao 

	WHERE 
		tep_id = @tep_id 
		AND tpl_id = @tpl_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_UPDATE]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 26/01/2010 09:35
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_UPDATE]
		@tmc_id uniqueidentifier
		,@tmc_nome VARCHAR (100)
		,@tmc_situacao TINYINT
		,@tmc_validacao TINYINT
		,@tmc_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_TipoMeioContato 
	SET 
		tmc_nome = @tmc_nome
		,tmc_situacao = @tmc_situacao
		,tmc_validacao = @tmc_validacao
		,tmc_dataAlteracao = @tmc_dataAlteracao
	WHERE 
		tmc_id = @tmc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_SelectBy_psd_numero]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/02/2010 12:15
-- Description:	utilizado no cadastro de documentos da pessoa,
--              para saber se o numero do documento cadastrado
--				já existe, de acordo com o TIPO_DOCUMENTACAO_CPF
--				filtrados por:
--					psd_numero, pes_id (diferente do informado)
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_SelectBy_psd_numero]	
	@psd_numero VARCHAR(50)
	, @pes_id uniqueidentifier
AS
BEGIN
	SELECT
		pes_id
		, tdo_id
	FROM
		PES_PessoaDocumento	WITH (NOLOCK)
	WHERE		
		psd_situacao <> 3
		AND psd_numero = @psd_numero
		AND (@pes_id is null or pes_id <> @pes_id)				
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:41
-- Description:	Decrementa uma unidade no campo integridade da tabela de cidades
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_DECREMENTA_INTEGRIDADE]
		@cid_id uniqueidentifier
AS
BEGIN
	UPDATE END_Cidade
	SET 
		cid_integridade = cid_integridade - 1
	WHERE 
		cid_id = @cid_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPaleta_UpdateSituacao]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_TemaPaleta_UpdateSituacao]
	@tep_id INT
	, @tpl_id INT
	, @tpl_situacao TINYINT
	, @tpl_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPaleta 
	SET 
		tpl_situacao = @tpl_situacao 
		, tpl_dataAlteracao = @tpl_dataAlteracao 

	WHERE 
		tep_id = @tep_id 
		AND tpl_id = @tpl_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_SelectBy_Nome]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 21/01/2010 13:25
-- Description:	utilizado na busca de nome de tipos de meio contato,
--				retorna quantidade dos tipos de meio contato que não
--				foram excluídos logicamente, filtrados por:
--					nome, id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_SelectBy_Nome]	
	@tmc_nome VARCHAR(100)	
	,@tmc_id_alteracao uniqueidentifier
	
AS
BEGIN
	SELECT 
		tmc_id
		,tmc_nome
	FROM
		SYS_TipoMeioContato WITH (NOLOCK)
	WHERE
		tmc_situacao <> 3
		AND UPPER(tmc_nome) = UPPER(@tmc_nome)
		AND (@tmc_id_alteracao is null or tmc_id <> @tmc_id_alteracao)		
	ORDER BY
		tmc_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_ServidorRelatorio_ContaUrlPorEntidadeDoSistema]'
GO
-- ===========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 28/04/2011 13:00
-- Description:	Conta quantos servidores de relatórios ativos e bloqueados 
--				estão cadastrado para o sistema da entidade com a mesma url e 
--				diretório.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_ServidorRelatorio_ContaUrlPorEntidadeDoSistema]
	@ent_id UNIQUEIDENTIFIER
	, @sis_id INT
	, @srr_id INT
	, @srr_diretorioRelatorios VARCHAR(1000)
	, @srr_pastaRelatorios VARCHAR(1000)
AS
BEGIN
	SELECT 
		COUNT(srr_id)
	FROM 
		CFG_ServidorRelatorio WITH(NOLOCK)
	WHERE
		ent_id = @ent_id
		AND sis_id = @sis_id
		AND srr_id <> @srr_id
		AND srr_diretorioRelatorios = @srr_diretorioRelatorios
		AND srr_pastaRelatorios = @srr_pastaRelatorios
		AND srr_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_ent_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,tua_id
		,uad_codigo
		,uad_nome
		,uad_sigla
		,uad_idSuperior
		,uad_situacao
		,uad_dataCriacao
		,uad_dataAlteracao
		,uad_integridade

	FROM
		SYS_UnidadeAdministrativa WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_SelectBy_pes_id_tdo_id_excluido]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 17/02/2010 16:48
-- Description:	utilizado na busca de documentos da pessoa, retorna os
--              documentos da pessoa mesmo os excluídos logiocamente
--				filtrados por:
--					pes_id, tdo_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_SelectBy_pes_id_tdo_id_excluido]	
	@pes_id uniqueidentifier
	, @tdo_id uniqueidentifier
AS
BEGIN
	SELECT		
		tdo_id
	FROM
		PES_PessoaDocumento	WITH (NOLOCK)
	WHERE				
		pes_id = @pes_id
		AND tdo_id = @tdo_id
	ORDER BY
		tdo_id
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModulo_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModulo_SELECT]
	
AS
BEGIN
	SELECT 
		vis_id
		, sis_id
		, mod_id
		
	FROM 
		SYS_VisaoModulo WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPaleta_SelecionaAtivos]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 20/01/2015
-- Description:	Seleciona os temas de cores ativos no sistema.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPaleta_SelecionaAtivos] 
AS
BEGIN
	SELECT 
		tpl.tep_id,
		tpl.tpl_id,
		tpl.tpl_nome,
		tpl.tpl_caminhoCSS,
		tpl.tpl_imagemExemploTema,
		tpl.tpl_situacao,
		tpl.tpl_dataCriacao,
		tpl.tpl_dataAlteracao,
		tep.tep_nome 
	FROM
		CFG_TemaPaleta tpl WITH(NOLOCK)
		INNER JOIN CFG_TemaPadrao tep WITH(NOLOCK)
			ON tpl.tep_id = tep.tep_id
			AND tep.tep_situacao <> 3
	WHERE
		tpl.tpl_situacao = 1
	ORDER BY
		tpl.tpl_nome
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 21/01/2010 13:25
-- Description:	utilizado na busca de tipos de meio de contato,
--				retorna os tipos
--              de meio de contato que não foram excluídos logicamente,
--				filtrados por:
--					id, nome, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_SelectBy_All]	
	@tmc_id uniqueidentifier
	,@tmc_nome VARCHAR(100)	
	,@tmc_situacao TINYINT
AS
BEGIN
	SELECT 
		tmc_id
		,tmc_nome
		, CASE tmc_validacao
			WHEN 1 THEN 'E-mail'
			WHEN 2 THEN 'Web site'
			WHEN 3 THEN 'Telefone'
			ELSE NULL
		END AS tmc_validacao
		, CASE tmc_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS tmc_situacao
	FROM
		SYS_TipoMeioContato WITH (NOLOCK)
	WHERE
		tmc_situacao <> 3
		AND (@tmc_id is null or tmc_id = @tmc_id)		
		AND (@tmc_nome is null or tmc_nome LIKE '%' + @tmc_nome + '%')		
		AND (@tmc_situacao is null or tmc_situacao = @tmc_situacao)				
	ORDER BY
		tmc_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_uad_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_uad_id]
	@uad_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,tua_id
		,uad_codigo
		,uad_nome
		,uad_sigla
		,uad_idSuperior
		,uad_situacao
		,uad_dataCriacao
		,uad_dataAlteracao
		,uad_integridade

	FROM
		SYS_UnidadeAdministrativa WITH(NOLOCK)
	WHERE 
		uad_id = @uad_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDocumento_SelectBy_pes_id]'
GO

-- ================================================================================================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 11/02/2010 10:15
-- Description:	utilizado na busca de documentos da pessoa, retorna os documentos da pessoa filtrados por pes_id
-- ================================================================================================================================================

CREATE PROCEDURE [dbo].[NEW_PES_PessoaDocumento_SelectBy_pes_id]	
	@pes_id UNIQUEIDENTIFIER
AS
BEGIN 
	
	DECLARE
		  @stringDefault	VARCHAR(512)  = NULL 
		, @objetosDefault	VARCHAR(1024) = ''
		, @Cont INT
		, @Max	INT
		, @atributosTmp		VARCHAR(512)
		, @nomeObjetosTmp   VARCHAR(1024) = ''

	-- Executa as procedures para selecionar as informações sobre os atributos default
	EXEC NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault 
	      @ExibirRetorno      = 0
	 	, @AtributosDefault   = @stringDefault OUTPUT
		, @NomeObjetosDefault = @objetosDefault OUTPUT

	-- 
	DECLARE @tabTipoDocumentacao TABLE 
	( 
		Seq				INT IDENTITY(1,1),
		tdo_id			UNIQUEIDENTIFIER, 
		tdo_atributos	VARCHAR(512),
		nomeObjetos		VARCHAR(1024) 
	)

	-- 
	INSERT INTO @tabTipoDocumentacao ( tdo_id, tdo_atributos )
	SELECT td.tdo_id, tdo_atributos FROM SYS_TipoDocumentacao td WITH (NOLOCK)

	--
	SET @Cont = 1
	SET @Max = ( SELECT MAX(tmp.Seq) FROM @tabTipoDocumentacao tmp )

	WHILE (@Cont <= @Max ) BEGIN
		
		SET @nomeObjetosTmp = ''

		-- 
		SET @atributosTmp = (	SELECT	ISNULL(tmp.tdo_atributos, @stringDefault) 
								FROM	@tabTipoDocumentacao tmp 
								WHERE	tmp.Seq = @Cont )

		IF (@atributosTmp = @stringDefault) BEGIN
			
			SET @nomeObjetosTmp = @objetosDefault

		END ELSE BEGIN

			SELECT	@nomeObjetosTmp += tda.tda_nomeObjeto + ';'
			FROM	SYS_TipoDocumentacaoAtributo tda WITH (NOLOCK) 
			WHERE	SUBSTRING(@atributosTmp, tda.tda_id, 1)  = 1

		END 

		UPDATE 
			tmp
		SET 
			tmp.nomeObjetos = @nomeObjetosTmp
		FROM 
			@tabTipoDocumentacao tmp
		WHERE 
			tmp.Seq = @Cont



		SET @Cont += 1
	END




	--SELECT * FROM @tabTipoDocumentacao tmp
	





	SELECT		
		  pd.tdo_id
		, tdo_nome
		, ISNULL(unf_idEmissao,'00000000-0000-0000-0000-000000000000') AS unf_idEmissao
		, ISNULL(unf_idEmissao,'00000000-0000-0000-0000-000000000000') AS unf_idAntigo
		, psd_numero				AS numero
		, CONVERT(CHAR,psd_dataEmissao,103) AS dataEmissao
		, psd_orgaoEmissao			AS orgaoEmissao		
		, unf_nome
		, ISNULL(pai_idOrigem,'00000000-0000-0000-0000-000000000000') AS pai_idOrigem
		, ISNULL(pai_idOrigem,'00000000-0000-0000-0000-000000000000') AS pai_idAntigo
		, pai_nome
		, psd_infoComplementares	AS info

		, psd_categoria				AS categoria
		, psd_classificacao			AS classificacao
		, psd_csm					AS csm
		, psd_dataEntrada			AS dataEntrada
		, psd_dataValidade			AS dataValidade
		, psd_serie					AS serie
		, psd_tipoGuarda			AS tipoGuarda
		, psd_via					AS via
		, psd_secao					AS secao
		, psd_zona					AS zona
		, psd_regiaoMilitar			AS regiaoMilitar 
		, psd_numeroRA				AS numeroRA
		, CONVERT(CHAR,psd_dataExpedicao,103) AS dataexpedicao
		, tdo.tdo_atributos			
		, tab.nomeObjetos			
		 		
	FROM
		PES_PessoaDocumento	pd WITH (NOLOCK)
		INNER JOIN SYS_TipoDocumentacao tdo WITH (NOLOCK)	ON tdo.tdo_id = pd.tdo_id
		LEFT  JOIN END_UnidadeFederativa WITH (NOLOCK)	ON END_UnidadeFederativa.unf_id = pd.unf_idEmissao AND unf_situacao <> 3
		LEFT  JOIN END_Pais WITH (NOLOCK)				ON END_Pais.pai_id = pd.pai_idOrigem AND pai_situacao <> 3

		LEFT JOIN @tabTipoDocumentacao tab				on ( tab.tdo_id = pd.tdo_id )

	WHERE		
		    psd_situacao <> 3
		AND tdo_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		tdo_nome
		
	SELECT @@ROWCOUNT

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModulo_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModulo_INSERT]
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	
AS
BEGIN
	INSERT INTO
		SYS_VisaoModulo
		(
			vis_id
			, sis_id
			, mod_id
		)
		VALUES
		(
			@vis_id
			, @sis_id
			, @mod_id
		)
		
	SELECT ISNULL(@@ROWCOUNT, -1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPaleta_VerificaExistePorNomeTemaPadrao]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 21/01/2015
-- Description:	Verifica se já existe um tema com o mesmo nome e tema padrão.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPaleta_VerificaExistePorNomeTemaPadrao] 
	@tep_id INT, 
	@tpl_id INT,
	@tpl_nome VARCHAR(100)
AS
BEGIN
	IF (EXISTS
		(
			SELECT 
				tpl.tpl_id
			FROM
				CFG_TemaPaleta tpl WITH(NOLOCK)
			WHERE
				tpl.tep_id  = @tep_id
				AND tpl.tpl_id <> ISNULL(@tpl_id, -1)
				AND LTRIM(RTRIM(tpl.tpl_nome)) = LTRIM(RTRIM(@tpl_nome))
				AND tpl.tpl_situacao <> 3
		))
	BEGIN
		RETURN 1;
	END
	ELSE
	BEGIN
		RETURN 0;
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:20
-- Description:	Seleciona o valor do campo integridade da tabela de tipo meio de contato
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_Select_Integridade]
		@tmc_id uniqueidentifier
AS
BEGIN
	SELECT 			
		tmc_integridade
	FROM
		SYS_TipoMeioContato WITH (NOLOCK)
	WHERE 
		tmc_id = @tmc_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_RelatorioServidorRelatorio_INSERTBY_XML]'
GO
-- ===================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 29/04/2011 18:30
-- Description:	Envia um xml com os ids dos relatorio selecionados
--				para um determinado servidor. Esses ids são inseridos
--				na tabela caso ainda não estejam lá e são excluídos
--				o registros que foram deselecionados no cadastro.
-- ===================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_RelatorioServidorRelatorio_INSERTBY_XML]
	@xmlRelatorios XML
AS
BEGIN
	DECLARE	@ent_id UNIQUEIDENTIFIER
			, @sis_id INT
			, @srr_id INT;
	
	CREATE TABLE #Relatorios(sis_id INT NOT NULL, ent_id UNIQUEIDENTIFIER NOT NULL, srr_id INT NULL, rlt_id INT NULL);
			
	WITH Relatorios(sis_id, ent_id, srr_id, rlt_id)	AS
	(
		SELECT 
			T.item.value('sis_id[1]', 'int')  AS sis_id
			, T.Item.value('ent_id[1]', 'uniqueidentifier') AS ent_id
			, T.Item.value('srr_id[1]', 'int') AS srr_id
			, T.Item.value('rlt_id[1]', 'int') AS rlt_id		
		FROM
			@xmlRelatorios.nodes('/ArrayOfCFG_RelatorioServidorRelatorio/CFG_RelatorioServidorRelatorio') AS T(Item)	
	)
	/* Insere os dados na tabela temporária */
	INSERT INTO #Relatorios (sis_id, ent_id, srr_id, rlt_id) 
	SELECT sis_id, ent_id, srr_id, rlt_id FROM Relatorios
	/* Apagar a tuplas não selecionadas caso tenha sido desmarcadas */
	SELECT TOP 1
		@sis_id = sis_id
		, @ent_id = ent_id
		, @srr_id = srr_id
	FROM
		#Relatorios;
		
	DELETE FROM CFG_RelatorioServidorRelatorio
	WHERE
		sis_id = @sis_id
		AND ent_id = @ent_id
		AND srr_id = @srr_id
		AND NOT rlt_id IN (SELECT rlt_id FROM #Relatorios)
	/* Insere as novas tuplas caso ainda não estejam inseridas */
	INSERT INTO CFG_RelatorioServidorRelatorio(sis_id, ent_id, srr_id, rlt_id)
	SELECT sis_id, ent_id, srr_id, rlt_id FROM #Relatorios R
	WHERE
		NOT EXISTS(
			SELECT 
				rlt_id 
			FROM 
				CFG_RelatorioServidorRelatorio RSR WITH(NOLOCK)
			WHERE
				R.ent_id = RSR.ent_id
				AND R.sis_id = RSR.sis_id
				AND R.srr_id = RSR.srr_id
				AND R.rlt_id = RSR.rlt_id
		);
		
	DROP TABLE #Relatorios
	
	RETURN ISNULL(@@ROWCOUNT,-1)	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_tua_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_UnidadeAdministrativa_SELECTBY_tua_id]
	@tua_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		ent_id
		,uad_id
		,tua_id
		,uad_codigo
		,uad_nome
		,uad_sigla
		,uad_idSuperior
		,uad_situacao
		,uad_dataCriacao
		,uad_dataAlteracao
		,uad_integridade

	FROM
		SYS_UnidadeAdministrativa WITH(NOLOCK)
	WHERE 
		tua_id = @tua_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_VisaoModulo_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_VisaoModulo_DELETE]
	@vis_id INT
	, @sis_id INT
	, @mod_id INT
	
AS
BEGIN
	DELETE FROM 
		SYS_VisaoModulo
	WHERE
		vis_id = @vis_id
		AND sis_id = @sis_id
		AND mod_id = @mod_id
		
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
	
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPaleta_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPaleta_LOAD]
	@tep_id Int
	, @tpl_id Int
	
AS
BEGIN
	SELECT	Top 1
		 tep_id  
		, tpl_id 
		, tpl_nome 
		, tpl_caminhoCSS 
		, tpl_imagemExemploTema 
		, tpl_situacao 
		, tpl_dataCriacao 
		, tpl_dataAlteracao 

 	FROM
 		CFG_TemaPaleta WITH(NOLOCK)
	WHERE 
		tep_id = @tep_id
		AND tpl_id = @tpl_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Incrementa uma unidade no campo integridade da tabela de tipo meio contato
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_INCREMENTA_INTEGRIDADE]
		@tmc_id uniqueidentifier

AS
BEGIN
	UPDATE SYS_TipoMeioContato
	SET 
		tmc_integridade = tmc_integridade + 1
	WHERE 
		tmc_id = @tmc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Relatorio_SELECTBY_RelatorioAtivo]'
GO
-- ================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 02/05/2011 18:10
-- Description:	Retorna todos os relatórios com a situação igual a
--				ativo(rlt_situacao = 1).
-- ================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Relatorio_SELECTBY_RelatorioAtivo]
AS
BEGIN
	SELECT 
		rlt_id
		,rlt_nome
		,rlt_situacao
		,rlt_dataCriacao
		,rlt_dataAlteracao
		,rlt_integridade
	FROM 
		CFG_Relatorio WITH(NOLOCK) 
	WHERE
		rlt_situacao = 1
	ORDER BY 
		rlt_nome
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Modulo_SelectBy_MenuXML]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 21/01/2010 18:56
-- Description:	Monta o menu de acesso de acordo com a permissão do grupo
--				do usuário.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Modulo_SelectBy_MenuXML]
	@gru_id uniqueidentifier
	, @sis_id int
	, @vis_id int
AS
BEGIN
	WITH Menus(sis_id, mod_id, mod_idPai, msm_id, mod_nome, msm_url, vmm_ordem) 
	AS
	(
		SELECT
			SYS_Modulo.sis_id
			, SYS_Modulo.mod_id
			, SYS_Modulo.mod_idPai
			, SYS_ModuloSiteMap.msm_id
			, SYS_Modulo.mod_nome
			, SYS_ModuloSiteMap.msm_url
			, SYS_VisaoModuloMenu.vmm_ordem
		FROM
			SYS_Modulo WITH (NOLOCK)
		INNER JOIN SYS_ModuloSiteMap WITH (NOLOCK)
			ON SYS_Modulo.sis_id = SYS_ModuloSiteMap.sis_id 
			AND SYS_Modulo.mod_id = SYS_ModuloSiteMap.mod_id
		INNER JOIN SYS_VisaoModuloMenu WITH (NOLOCK)
			ON SYS_ModuloSiteMap.sis_id = SYS_VisaoModuloMenu.sis_id 
			AND SYS_ModuloSiteMap.mod_id = SYS_VisaoModuloMenu.mod_id
			AND SYS_ModuloSiteMap.msm_id = SYS_VisaoModuloMenu.msm_id	
		WHERE
			SYS_Modulo.mod_situacao <> 3
			AND SYS_Modulo.sis_id = @sis_id
			AND SYS_VisaoModuloMenu.vis_id = @vis_id
			AND EXISTS (
				SELECT 
					SYS_GrupoPermissao.gru_id
					, SYS_GrupoPermissao.sis_id
					, SYS_GrupoPermissao.mod_id 
				FROM
					SYS_GrupoPermissao WITH (NOLOCK)
				WHERE
					SYS_GrupoPermissao.gru_id = @gru_id
					AND SYS_GrupoPermissao.sis_id = SYS_Modulo.sis_id
					AND SYS_GrupoPermissao.mod_id = SYS_Modulo.mod_id
					AND 
					(
						(grp_consultar = 1)
						OR  
						(grp_inserir = 1)
						OR 
						(grp_alterar = 1) 
						OR
						(grp_excluir = 1)
					)
			)
	)

	SELECT 1 AS Tag
		, NULL AS Parent
		, mod_nome AS [menu!1!id]
		, msm_url AS [menu!1!url]
		, vmm_ordem AS [menu!1!ordem]
		, NULL AS [item!2!id]
		, NULL AS [item!2!url]
		, NULL AS [item!2!ordem]
		, NULL AS [subitem!3!id]
		, NULL AS [subitem!3!url]
		, NULL AS [subitem!3!ordem]
		, NULL AS [subitem2!4!id]
		, NULL AS [subitem2!4!url]
		, NULL AS [subitem2!4!ordem]
	FROM 
		Menus WITH(NOLOCK)
	WHERE
		mod_idPai is null
	UNION ALL
	SELECT 2 AS Tag
		, 1 AS Parent
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, ISNULL(item.msm_url, '')
		, item.vmm_ordem
		, NULL AS [subitem!3!id]
		, NULL AS [subitem!3!url]
		, NULL AS [subitem!3!ordem]
		, NULL AS [subitem2!4!id]
		, NULL AS [subitem2!4!url]
		, NULL AS [subitem2!4!ordem]
	FROM 
		Menus AS menu WITH(NOLOCK)
	INNER JOIN Menus AS item WITH(NOLOCK)
		ON item.mod_idPai = menu.mod_id
	WHERE
		menu.mod_idPai is null
	UNION ALL
	SELECT 3 AS Tag
		, 2 AS Parent
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, item.msm_url
		, item.vmm_ordem
		, subitem.mod_nome
		, subitem.msm_url
		, subitem.vmm_ordem
		, NULL AS [subitem2!4!id]
		, NULL AS [subitem2!4!url]
		, NULL AS [subitem2!4!ordem]
	FROM 
		Menus AS menu WITH(NOLOCK)
	INNER JOIN Menus AS item WITH(NOLOCK)
		ON item.mod_idPai = menu.mod_id
	INNER JOIN Menus AS subitem WITH(NOLOCK)
		ON subitem.mod_idPai = item.mod_id
	WHERE
		menu.mod_idPai is null
		AND not item.mod_idPai is null
	UNION ALL
	SELECT 4 AS Tag
		, 3 AS Parent
		, menu.mod_nome
		, menu.msm_url
		, menu.vmm_ordem
		, item.mod_nome
		, item.msm_url
		, item.vmm_ordem
		, subitem.mod_nome
		, subitem.msm_url
		, subitem.vmm_ordem
		, subitem2.mod_nome
		, subitem2.msm_url
		, subitem2.vmm_ordem
	FROM
		Menus AS menu WITH(NOLOCK)
	INNER JOIN Menus AS item WITH(NOLOCK)
		ON item.mod_idPai = menu.mod_id
	INNER JOIN Menus AS subitem WITH(NOLOCK)
		ON subitem.mod_idPai = item.mod_id
	INNER JOIN Menus AS subitem2 WITH(NOLOCK)
		ON subitem2.mod_idPai = subitem.mod_id
	WHERE
		menu.mod_idPai is null
		AND not item.mod_idPai is null
	ORDER BY
		[menu!1!ordem], [item!2!ordem], [subitem!3!ordem], [subitem2!4!ordem]
		
	FOR XML EXPLICIT, ROOT('menus')
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPaleta_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPaleta_INSERT]
	@tep_id Int
	, @tpl_id Int
	, @tpl_nome VarChar (100)
	, @tpl_caminhoCSS VarChar (1000)
	, @tpl_imagemExemploTema VarChar (2000)
	, @tpl_situacao TinyInt
	, @tpl_dataCriacao DateTime
	, @tpl_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		CFG_TemaPaleta
		( 
			tep_id 
			, tpl_id 
			, tpl_nome 
			, tpl_caminhoCSS 
			, tpl_imagemExemploTema 
			, tpl_situacao 
			, tpl_dataCriacao 
			, tpl_dataAlteracao 
 
		)
	VALUES
		( 
			@tep_id 
			, @tpl_id 
			, @tpl_nome 
			, @tpl_caminhoCSS 
			, @tpl_imagemExemploTema 
			, @tpl_situacao 
			, @tpl_dataCriacao 
			, @tpl_dataAlteracao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Decrementa uma unidade no campo integridade da tabela de tipo meio contato
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_DECREMENTA_INTEGRIDADE]
		@tmc_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_TipoMeioContato
	SET 
		tmc_integridade = tmc_integridade - 1
	WHERE 
		tmc_id = @tmc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_RelatorioServidorRelatorio_SelectByServidorRelatorio]'
GO
-- ================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 26/04/2011 12:05
-- Description:	Lista todos os relatórios associados ao servidor 
--				de relatórios definidos nos parâmetros de entrada.
-- ================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_RelatorioServidorRelatorio_SelectByServidorRelatorio]
	@ent_id UNIQUEIDENTIFIER
	, @sis_id INT
	, @srr_id INT
AS
BEGIN
	SELECT		
		sis_id
		, ent_id
		, srr_id
		, rlt_id	
	FROM
		CFG_RelatorioServidorRelatorio WITH(NOLOCK)
	WHERE
		ent_id = @ent_id
		AND sis_id = @sis_id
		AND srr_id = @srr_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 29/04/2010 10:07
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente a endereço. Filtrada por:
--				end_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_Update_Situacao]	
	@end_id uniqueidentifier
	,@end_dataAlteracao DateTime
	,@end_situacao TINYINT	
AS
BEGIN
	UPDATE END_Endereco
	SET 
		end_dataAlteracao = @end_dataAlteracao
		,end_situacao = @end_situacao
	WHERE 
		end_id = @end_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPaleta_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPaleta_UPDATE]
	@tep_id INT
	, @tpl_id INT
	, @tpl_nome VARCHAR (100)
	, @tpl_caminhoCSS VARCHAR (1000)
	, @tpl_imagemExemploTema VARCHAR (2000)
	, @tpl_situacao TINYINT
	, @tpl_dataCriacao DATETIME
	, @tpl_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPaleta 
	SET 
		tpl_nome = @tpl_nome 
		, tpl_caminhoCSS = @tpl_caminhoCSS 
		, tpl_imagemExemploTema = @tpl_imagemExemploTema 
		, tpl_situacao = @tpl_situacao 
		, tpl_dataCriacao = @tpl_dataCriacao 
		, tpl_dataAlteracao = @tpl_dataAlteracao 

	WHERE 
		tep_id = @tep_id 
		AND tpl_id = @tpl_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_Update_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 21/01/2010 13:04
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Entidade. Filtrada por: 
--					ten_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_Update_Situacao]	
		@ten_id uniqueidentifier
		,@ten_situacao TINYINT
		,@ten_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoEntidade 
	SET 
		ten_situacao = @ten_situacao
		,ten_dataAlteracao = @ten_dataAlteracao
	WHERE 
		ten_id = @ten_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Grupo_SelectBy_Sistema]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 04/05/2011 14:41
-- Description:	Retorna todas as configurações ativas
--				filtrando por: sistema
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Grupo_SelectBy_Sistema]
	@sis_id INT
AS
BEGIN
	SELECT 
		gru_id
		, gru_nome
		, vis_id
		, CONVERT(VARCHAR(36), gru_id) + ';' + CONVERT(VARCHAR, vis_id) AS gru_vis_id 
	FROM
		SYS_Grupo WITH(NOLOCK)
	WHERE
		gru_situacao <> 3
		AND sis_id = @sis_id
	ORDER BY
		gru_nome
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating trigger [dbo].[TRG_SYS_Modulo_Identity] on [dbo].[SYS_Modulo]'
GO
-- =============================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 04/06/2010 10:00
-- Description:	Realiza o autoincremento do 
--				campo mod_id garantindo que
--				sempre será reiniciado em 1
--				qdo um sis_id for inserido
-- =============================================
CREATE TRIGGER [dbo].[TRG_SYS_Modulo_Identity]
ON [dbo].[SYS_Modulo] INSTEAD OF INSERT
AS
BEGIN
	DECLARE @ID INT
	SELECT 
		@ID = CASE WHEN MAX(SYS_Modulo.mod_id) IS NULL THEN 1 ELSE MAX(SYS_Modulo.mod_id)+1 END 
	FROM 
		SYS_Modulo WITH(XLOCK,TABLOCK) 
		INNER JOIN inserted
			ON SYS_Modulo.sis_id = inserted.sis_id
	/* INSERE O ID AUTOINCREMENTO */
	INSERT INTO SYS_Modulo (sis_id, mod_id, mod_nome, mod_descricao, mod_idPai, mod_auditoria, mod_situacao, mod_dataCriacao, mod_dataAlteracao)
    SELECT sis_id, @ID, mod_nome, mod_descricao, mod_idPai, mod_auditoria, mod_situacao, mod_dataCriacao, mod_dataAlteracao FROM inserted
    /* RETORNA INSERT */
    SELECT ISNULL(@ID, -1)
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaDeficiencia_SelectBy_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 03/05/2010 08:26
-- Description:	utilizado na busca de deficiencias da pessoa, retorna as
--              deficiencias da pessoa
--				filtrados por:
--					pes_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaDeficiencia_SelectBy_pes_id]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT		
		pes_id
		, PES_PessoaDeficiencia.tde_id	
		, tde_nome
	FROM
		PES_PessoaDeficiencia	WITH (NOLOCK)
	INNER JOIN
		PES_TipoDeficiencia WITH (NOLOCK) on  PES_TipoDeficiencia.tde_id = PES_PessoaDeficiencia.tde_id		
	WHERE				
		tde_situacao <> 3		
		AND pes_id = @pes_id
	ORDER BY
		tde_nome
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPaleta_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_CFG_TemaPaleta_DELETE]
	@tep_id INT
	, @tpl_id INT

AS
BEGIN
	DELETE FROM 
		CFG_TemaPaleta 
	WHERE 
		tep_id = @tep_id 
		AND tpl_id = @tpl_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_Update]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 22/01/2010 13:33
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_Update]
		@ten_id uniqueidentifier
		,@ten_nome VarChar (100)
		,@ten_situacao TINYINT
		,@ten_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoEntidade 
	SET 
		ten_nome = @ten_nome
		,ten_situacao = @ten_situacao
		,ten_dataAlteracao = @ten_dataAlteracao
	WHERE 
		ten_id = @ten_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelectBy_GrupoUA]'
GO
-- ===========================================================================
-- Author:		Aline Dornelas
-- Create date: 05/05/2011 11:31
-- Description:	Retorna todos os usuários que não foram excluídos logicamente
--				filtrando por: entidade, grupo e unidade administrativa.
-- ===========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelectBy_GrupoUA]
	@ent_id UNIQUEIDENTIFIER
	, @usu_idPermissao UNIQUEIDENTIFIER
	, @gru_idPermissao UNIQUEIDENTIFIER
	, @gru_id UNIQUEIDENTIFIER
	, @uad_id UNIQUEIDENTIFIER
	, @uad_idSuperior UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		SYS_Usuario.usu_id 
		, usu_login
		, usu_email
		, SYS_UsuarioGrupo.gru_id
		, SYS_UsuarioGrupoUA.ugu_id
		, SYS_UnidadeAdministrativa.uad_id
		, SYS_UnidadeAdministrativa.uad_nome
		, usu_situacao
		, CASE usu_situacao 
			WHEN 1 THEN 'Ativo'
			WHEN 2 THEN 'Bloqueado'
			WHEN 4 THEN 'Padrão do Sistema'
			WHEN 5 THEN 'Senha Expirada'
		  END AS usu_situacaoDescricao
	FROM
		SYS_Usuario WITH(NOLOCK)
	INNER JOIN SYS_UsuarioGrupo WITH(NOLOCK)
		ON SYS_Usuario.usu_id = SYS_UsuarioGrupo.usu_id
		AND SYS_UsuarioGrupo.usg_situacao <> 3
	LEFT JOIN SYS_UsuarioGrupoUA WITH(NOLOCK)
		ON SYS_UsuarioGrupo.usu_id = SYS_UsuarioGrupoUA.usu_id
		AND SYS_UsuarioGrupo.gru_id = SYS_UsuarioGrupoUA.gru_id
	LEFT JOIN SYS_UnidadeAdministrativa WITH(NOLOCK)
		ON SYS_UsuarioGrupoUA.ent_id = SYS_UnidadeAdministrativa.ent_id
		AND SYS_UsuarioGrupoUA.uad_id = SYS_UnidadeAdministrativa.uad_id
		AND SYS_UnidadeAdministrativa.uad_situacao <> 3
	WHERE
		usu_situacao <> 3
		AND SYS_Usuario.ent_id = @ent_id
		AND SYS_UsuarioGrupo.gru_id = @gru_id
		AND (((@uad_id IS NULL) AND ((SYS_UsuarioGrupoUA.uad_id IS NULL) OR (SYS_UsuarioGrupoUA.uad_id IN(SELECT uad_id FROM FN_Select_UAs_By_PermissaoUsuario(@usu_idPermissao, @gru_idPermissao))))) 
			OR (SYS_UsuarioGrupoUA.uad_id = @uad_id))	
		AND ((@uad_idSuperior IS NULL) OR (SYS_UnidadeAdministrativa.uad_idSuperior = @uad_idSuperior)) 
	ORDER BY
		usu_login
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaContato_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 10/02/2010 15:40
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente a
--				Contato da Pessoa. Filtrada por: 
--					pes_id, psc_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaContato_Update_Situacao]	
		@pes_id uniqueidentifier
		,@psc_id uniqueidentifier
		,@psc_situacao TINYINT
		,@psc_dataAlteracao DATETIME
AS
BEGIN
	UPDATE PES_PessoaContato 
	SET 
		psc_situacao = @psc_situacao
		,psc_dataAlteracao = @psc_dataAlteracao
	WHERE 
		pes_id = @pes_id
		and psc_id = @psc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_UPDATE]'
GO
/****** Object:  StoredProcedure [dbo].[NEW_END_Endereco_UPDATE]    Script Date: 05/14/2010 15:56:55 ******/
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 14/05/2010 13:45
-- Description:	Altera o endereço preservando a data da criação e a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_UPDATE]
		@end_id uniqueidentifier
		,@end_cep VARCHAR (8)
		,@end_logradouro VARCHAR (200)
		,@end_distrito VARCHAR (100)
		,@end_zona TINYINT
		,@end_bairro VARCHAR (100)
		,@cid_id uniqueidentifier
		,@end_integridade INT
		,@end_situacao TINYINT		
		,@end_dataAlteracao DATETIME		

AS
BEGIN
	UPDATE END_Endereco
	SET 		
		end_cep = @end_cep
		,end_logradouro = @end_logradouro
		,end_distrito = @end_distrito
		,end_zona = @end_zona
		,end_bairro = @end_bairro		
		,cid_id = @cid_id
		,end_integridade = @end_integridade
		,end_situacao = @end_situacao		
		,end_dataAlteracao = @end_dataAlteracao		
	WHERE 
		end_id = @end_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPaleta_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPaleta_SELECT]
	
AS
BEGIN
	SELECT 
		tep_id
		,tpl_id
		,tpl_nome
		,tpl_caminhoCSS
		,tpl_imagemExemploTema
		,tpl_situacao
		,tpl_dataCriacao
		,tpl_dataAlteracao

	FROM 
		CFG_TemaPaleta WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_SelectBy_Nome]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 26/01/2010 14:10
-- Description:	utilizado na busca de nome de tipos de entidade, retorna quantidade
--				dos tipos de entidade que não foram excluídos logicamente,
--				filtrados por:
--					nome, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_SelectBy_Nome]	
	@ten_nome VARCHAR(100)	
	,@ten_id_alteracao uniqueidentifier
	
AS
BEGIN
	SELECT 
		ten_id
		,ten_nome
	FROM
		SYS_TipoEntidade WITH (NOLOCK)		
	WHERE
		ten_situacao <> 3
		AND UPPER(ten_nome) = UPPER(@ten_nome)	
		AND (@ten_id_alteracao is null or ten_id <> @ten_id_alteracao)
	ORDER BY
		ten_nome
	
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaContato_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 18/05/2010 10:40
-- Description:	Altera o contato da pessoa preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaContato_UPDATE]
		@pes_id uniqueidentifier
		,@psc_id uniqueidentifier
		,@tmc_id uniqueidentifier
		,@psc_contato VARCHAR (200)
		,@psc_situacao TINYINT		
		,@psc_dataAlteracao DATETIME

AS
BEGIN
	UPDATE PES_PessoaContato
	SET
		tmc_id = @tmc_id
		,psc_contato = @psc_contato
		,psc_situacao = @psc_situacao		
		,psc_dataAlteracao = @psc_dataAlteracao
	WHERE 
		pes_id = @pes_id
		AND	psc_id = @psc_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 22/01/2010 11:40
-- Description:	utilizado na busca de endereços, retorna os endereços
--              que não foram excluídos logicamente,
--				filtrados por:
--					end_id, cid_id, unf_id, pai_id, cep, logradouro, bairro,
--					cidade, estado, sigla do estado, pais e situação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_SelectBy_All]	
	@end_id uniqueidentifier
	,@cid_id uniqueidentifier
	,@unf_id uniqueidentifier
	,@pai_id uniqueidentifier
	,@end_cep VARCHAR(8)
	,@end_logradouro VARCHAR(200)
	,@end_bairro VARCHAR(100)
	,@cid_nome VARCHAR(200)
	,@unf_nome VARCHAR(100)
	,@unf_sigla VARCHAR(2)
	,@pai_nome VARCHAR(100)
	,@end_situacao TINYINT	
AS
BEGIN
	SELECT 
	    end_id		
		,END_Endereco.cid_id		
		,END_Cidade.unf_id
		,END_Cidade.pai_id
		,end_cep
		,end_logradouro
		,end_distrito
		,end_zona
		,end_bairro
		,cid_nome
		,unf_nome				    		
		,unf_sigla		
		,pai_nome	
		, end_dataCriacao AS data
		,cid_nome + ' - ' + unf_sigla AS cidadeuf
		, CASE end_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS unf_situacao
	FROM
		END_Endereco WITH (NOLOCK)
	INNER JOIN
		END_Cidade WITH (NOLOCK) on END_endereco.cid_id = END_Cidade.cid_id
	LEFT JOIN
		END_UnidadeFederativa WITH (NOLOCK) on END_Cidade.unf_id = END_UnidadeFederativa.unf_id	and unf_situacao <> 3	
	INNER JOIN
		END_Pais WITH (NOLOCK) on END_Cidade.pai_id = END_Pais.pai_id
	WHERE
		end_situacao <> 3
		AND cid_situacao <> 3
		AND pai_situacao <> 3
		AND (@end_id is null or end_id = @end_id)				
		AND (@cid_id is null or END_Endereco.cid_id = @cid_id)						
		AND (@unf_id is null or END_Cidade.unf_id = @unf_id)		
		AND (@pai_id is null or END_Cidade.pai_id = @pai_id)
		AND (@end_cep is null or end_cep = @end_cep)				
		AND (@end_logradouro is null or end_logradouro LIKE  '%' + @end_logradouro + '%')							
		AND (@end_bairro is null or end_bairro LIKE '%' + @end_bairro + '%')																				
		AND (@cid_nome is null or cid_nome LIKE '%' + @cid_nome + '%')
		AND (@unf_nome is null or unf_nome LIKE '%' + @unf_nome + '%')					
		AND (@unf_sigla is null or unf_sigla = @unf_sigla)						
		AND (@pai_nome is null or pai_nome LIKE '%' + @pai_nome + '%')		
		AND (@end_situacao is null or end_situacao = @end_situacao)				
	ORDER BY
		pai_nome, unf_nome, cid_nome, end_logradouro
		
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPaleta_SelecionaPorTemaPadrao]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 21/01/2015
-- Description:	Seleciona temas de core por tema padrão
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPaleta_SelecionaPorTemaPadrao] 
	@tep_id INT
AS
BEGIN
	SELECT 
		tpl.tep_id,
		tpl.tpl_id,
		tpl.tpl_nome,
		tpl.tpl_caminhoCSS,
		tpl.tpl_imagemExemploTema,
		tpl.tpl_situacao,
		tpl.tpl_dataCriacao,
		tpl.tpl_dataAlteracao,
		tep.tep_nome
	FROM
		CFG_TemaPaleta tpl WITH(NOLOCK)
		INNER JOIN CFG_TemaPadrao tep WITH(NOLOCK)
			ON tpl.tep_id = tep.tep_id
			AND tep.tep_situacao <> 3
	WHERE
		tpl.tep_id = @tep_id
		AND tpl.tpl_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 20/01/2010 13:35
-- Description:	utilizado na busca de tipos de entidade, retorna os tipos
--              de entidade que não foram excluídos logicamente,
--				filtrados por:
--					id, nome, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_SelectBy_All]	
	@ten_id uniqueidentifier
	,@ten_nome VARCHAR(100)	
	,@ten_situacao TINYINT
AS
BEGIN
	SELECT 
		ten_id
		,ten_nome
		, CASE ten_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		  END AS ten_situacao
	FROM
		SYS_TipoEntidade WITH (NOLOCK)	
	WHERE
		ten_situacao <> 3
		AND (@ten_id is null or ten_id = @ten_id)		
		AND (@ten_nome is null or ten_nome LIKE '%' + @ten_nome + '%')		
		AND (@ten_situacao is null or ten_situacao = @ten_situacao)				
	ORDER BY
		ten_nome
		
	SELECT @@ROWCOUNT				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_SelectBy_Busca_OLD]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 29/01/2010 18:30
-- Description:	Select para retorna a busca de pessoas conforme o documento
--				de especificação tópico 4.4.7.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_SelectBy_Busca_OLD]
	@pes_nome VARCHAR(200)
	,@pes_dataNascimento DATE
	, @TIPO_DOCUMENTACAO_CPF VARCHAR(50)
	, @TIPO_DOCUMENTACAO_RG VARCHAR(50)
AS
BEGIN
	SELECT 
		PES_Pessoa.pes_id 
		, pes_nome
		, [TIPO_DOCUMENTACAO_CPF]
		, [TIPO_DOCUMENTACAO_RG]
		, CONVERT(VARCHAR, pes_dataNascimento, 103) AS pes_dataNascimento
	FROM PES_Pessoa WITH (NOLOCK)
	LEFT JOIN
		(
			SELECT
				pes_id
				, psd_numero
				, (SELECT TOP 1 par.par_chave FROM SYS_Parametro par WITH (NOLOCK) WHERE par.par_chave in ('TIPO_DOCUMENTACAO_CPF','TIPO_DOCUMENTACAO_RG') AND par.par_valor = CONVERT( varchar(36), psd.tdo_id))
				AS par_chave
			FROM
				PES_PessoaDocumento psd WITH (NOLOCK)
			WHERE
				psd.psd_situacao <> 3
				AND psd.tdo_id IN (
								SELECT 
									par.par_valor
								FROM 
									SYS_Parametro par WITH (NOLOCK)
								WHERE 
									par_situacao = 1
									AND par.par_chave in ('TIPO_DOCUMENTACAO_CPF','TIPO_DOCUMENTACAO_RG')
									AND par_vigenciaInicio <= CAST(GETDATE() AS DATE)
									AND ((par_vigenciaFim IS NULL) OR (par_vigenciaFim >= CAST(GETDATE() AS DATE)))									
				) -- parenteses IN
		) AS T1
		PIVOT (
			MAX(psd_numero) FOR par_chave in ([TIPO_DOCUMENTACAO_RG], [TIPO_DOCUMENTACAO_CPF])
		) as pvt
			ON pvt.pes_id = PES_Pessoa.pes_id		
	WHERE
		pes_situacao <> 3
		AND (
			(@pes_nome IS NULL) 
			OR (
					(pes_nome LIKE '%' + @pes_nome + '%') 
					OR 
					(pes_nome_abreviado LIKE '%' + @pes_nome + '%')
				)
			)
		AND (@pes_dataNascimento IS NULL OR pes_dataNascimento = @pes_dataNascimento)
		AND ((@TIPO_DOCUMENTACAO_CPF IS NULL) OR (pvt.TIPO_DOCUMENTACAO_CPF LIKE '%' + @TIPO_DOCUMENTACAO_CPF + '%'))
		AND ((@TIPO_DOCUMENTACAO_RG IS NULL) OR (pvt.TIPO_DOCUMENTACAO_RG LIKE '%' + @TIPO_DOCUMENTACAO_RG + '%'))
	ORDER BY
		pes_nome
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:35
-- Description:	Seleciona o valor do campo integridade da tabela de endereço
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_Select_Integridade]
		@end_id uniqueidentifier
AS
BEGIN
	SELECT 			
		end_integridade
	FROM
		END_Endereco WITH (NOLOCK)
	WHERE 
		end_id = @end_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Entidade_INSERT]'
GO

CREATE PROCEDURE [dbo].[NEW_SYS_Entidade_INSERT]
	@ten_id UniqueIdentifier
	, @ent_codigo VarChar (20)
	, @ent_nomeFantasia VarChar (200)
	, @ent_razaoSocial VarChar (200)
	, @ent_sigla VarChar (50)
	, @ent_cnpj VarChar (14)
	, @ent_inscricaoMunicipal VarChar (20)
	, @ent_inscricaoEstadual VarChar (20)
	, @ent_idSuperior UniqueIdentifier
	, @ent_situacao TinyInt
	, @ent_dataCriacao DateTime
	, @ent_dataAlteracao DateTime
	, @ent_integridade INT
	, @ent_urlAcesso VarChar (200) = NULL
	, @ent_exibeLogoCliente BIT = NULL
	, @ent_logoCliente VarChar (2000) = NULL
	, @tep_id INT = NULL
	, @tpl_id INT = NULL

AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		SYS_Entidade
		( 
			ten_id 
			, ent_codigo 
			, ent_nomeFantasia 
			, ent_razaoSocial 
			, ent_sigla 
			, ent_cnpj 
			, ent_inscricaoMunicipal 
			, ent_inscricaoEstadual 
			, ent_idSuperior 
			, ent_situacao 
			, ent_dataCriacao 
			, ent_dataAlteracao 
			, ent_integridade 
			, ent_urlAcesso 
			, ent_exibeLogoCliente 
			, ent_logoCliente 
			, tep_id 
			, tpl_id
		)
	OUTPUT inserted.ent_id INTO @ID
	VALUES
		( 
			@ten_id 
			, @ent_codigo 
			, @ent_nomeFantasia 
			, @ent_razaoSocial 
			, @ent_sigla 
			, @ent_cnpj 
			, @ent_inscricaoMunicipal 
			, @ent_inscricaoEstadual 
			, @ent_idSuperior 
			, @ent_situacao 
			, @ent_dataCriacao 
			, @ent_dataAlteracao 
			, @ent_integridade 
			, @ent_urlAcesso 
			, @ent_exibeLogoCliente 
			, @ent_logoCliente 
			, @tep_id 
			, @tpl_id
		)
	SELECT ID FROM @ID
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 09:43
-- Description:	Seleciona o valor do campo integridade da tabela tipo de entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_Select_Integridade]
		@ten_id uniqueidentifier
AS
BEGIN
	SELECT 			
		ten_integridade
	FROM
		SYS_TipoEntidade WITH (NOLOCK)
	WHERE 		
		ten_id = @ten_id
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_Insert_Visoes]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 21/07/2010
-- Description:	Insere permissões (com 0) para os módulos.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_Insert_Visoes]
	@sis_id int
	, @vis_id int
	, @mod_id int
AS
BEGIN
	INSERT INTO SYS_GrupoPermissao (gru_id, sis_id, mod_id, grp_consultar, grp_inserir, grp_alterar, grp_excluir)
	SELECT gru.gru_id
			, @sis_id
			, @mod_id
			, CASE WHEN (gru.gru_situacao = 4) THEN 1 ELSE 0 END
			, CASE WHEN (gru.gru_situacao = 4) THEN 1 ELSE 0 END
			, CASE WHEN (gru.gru_situacao = 4) THEN 1 ELSE 0 END
			, CASE WHEN (gru.gru_situacao = 4) THEN 1 ELSE 0 END
	FROM SYS_Visao vis WITH(NOLOCK)
	INNER JOIN SYS_Grupo gru WITH(NOLOCK)
		ON vis.vis_id = gru.vis_id
	WHERE vis.vis_id = @vis_id
		  AND sis_id = @sis_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Incrementa uma unidade no campo integridade da tabela de endereço
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_INCREMENTA_INTEGRIDADE]
		@end_id uniqueidentifier

AS
BEGIN
	UPDATE END_Endereco
	SET 
		end_integridade = end_integridade + 1
	WHERE 
		end_id = @end_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UsuarioGrupoUA_SelecionaPorLogin]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 12/01/2015
-- Description:	Seleciona as entidades e unidades administrativas por login do usuário.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_UsuarioGrupoUA_SelecionaPorLogin] 
	@usu_login VARCHAR(500)
AS
BEGIN
	;WITH tbUsuario AS 
	(
		SELECT
			usu.usu_id,
			usu.ent_id,
			usu.pes_id
		FROM
			SYS_Usuario usu WITH(NOLOCK)
		WHERE
			usu.usu_login = @usu_login
			AND usu.usu_situacao <> 3 
	)
	
	SELECT
		usu.usu_id,
		usu.ent_id,
		usu.pes_id,
		ent.ent_razaoSocial,
		ugu.gru_id,
		ugu.ugu_id,
		ugu.uad_id
	FROM
		tbUsuario usu
		INNER JOIN SYS_Entidade ent WITH(NOLOCK)
			ON ent.ent_id = usu.ent_id
			AND ent.ent_situacao <> 3
		LEFT JOIN SYS_UsuarioGrupoUA ugu WITH(NOLOCK)
			ON ugu.usu_id = usu.usu_id
			AND ugu.ent_id = usu.ent_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 09:01
-- Description:	Incrementa uma unidade no campo integridade do tipo de entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_INCREMENTA_INTEGRIDADE]
		@ten_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_TipoEntidade
	SET 		
		ten_integridade = ten_integridade + 1		
	WHERE 
		ten_id = @ten_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_SelectUrlHelpByUrl]'
GO
-- ========================================================================
-- Author:		Renata Tiepo Fonseca
-- Create date: 16/01/2012 10:20
-- Description:	Retorna a url do help filtrando pela url do sitemap.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_SelectUrlHelpByUrl]
	@gru_id uniqueidentifier
	, @msm_url VARCHAR(500)
AS
BEGIN
	SELECT TOP 1
		msm.msm_id
		, msm.msm_urlHelp
	FROM
		SYS_GrupoPermissao grp WITH(NOLOCK) 
	INNER JOIN SYS_Modulo mol WITH(NOLOCK)
		ON  grp.sis_id = mol.sis_id
		AND grp.mod_id = mol.mod_id
		AND	mol.mod_situacao <> 3	
	INNER JOIN SYS_ModuloSiteMap msm WITH(NOLOCK)
		ON  mol.mod_id = msm.mod_id
		AND mol.sis_id = msm.sis_id
		AND msm.msm_url = @msm_url
	WHERE
		grp.gru_id = @gru_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_DiaNaoUtil_SelecionaTodosPorCidade]'
GO
-- ========================================================================
-- Author:		Heitor Henrique Martins
-- Create date: 09/05/2011 11:00
-- Description:	utilizado na busca de dias não util, retorna os dias não util
--              que não foram excluídas logicamente,
--				filtrados por:
--					cidade do usuário
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_DiaNaoUtil_SelecionaTodosPorCidade]	
	@cid_id uniqueidentifier
AS
BEGIN
	SELECT 
		dnu_id
		, dnu_nome
		, dnu_abrangencia
		, dnu_descricao
		, dnu_data
		, dnu_recorrencia
		, dnu_vigenciaInicio
		, dnu_vigenciaFim
		, cid_id
		, unf_id
		, dnu_situacao
		, dnu_dataCriacao
		, dnu_dataAlteracao
	FROM 
		SYS_DiaNaoUtil WITH(NOLOCK)
	WHERE
		dnu_situacao <> 3	
		AND ((cid_id is NULL) OR (cid_id = @cid_id))
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaContato_SelectBy_pes_id_tmc_id]'
GO
-- ========================================================================
-- Author:		Jean Augusto da Costa
-- Create date: 05/05/2010 17:30
-- Description:	busca o contato da pessoa (pes_id) de acordo com o tipo meio contato(tmc_id)
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaContato_SelectBy_pes_id_tmc_id]	
	@pes_id uniqueidentifier
	, @tmc_id uniqueidentifier
AS
BEGIN
	SELECT
		pes_id
		,psc_id
		,tmc_id
		,psc_contato
		,psc_situacao
		,psc_dataCriacao
		,psc_dataAlteracao
	FROM
		PES_PessoaContato WITH (NOLOCK)	
	WHERE		
		psc_situacao <> 3 
		AND pes_id = @pes_id
		AND tmc_id = @tmc_id
	ORDER BY psc_dataAlteracao DESC 
					
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Endereco_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Decrementa uma unidade no campo integridade da tabela de endereços
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Endereco_DECREMENTA_INTEGRIDADE]
		@end_id uniqueidentifier
AS
BEGIN
	UPDATE END_Endereco
	SET 
		end_integridade = end_integridade - 1
	WHERE 
		end_id = @end_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelecionaPorEntidade]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 13/01/2015
-- Description:	Retorna todos os usuários que não foram excluídos logicamente
--				filtrando por: entidade e pessoa
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelecionaPorEntidade] 
	@ent_id UNIQUEIDENTIFIER,
	@pes_id UNIQUEIDENTIFIER,
	@trazerFoto BIT,
	@dataAlteracao DATETIME = NULL
AS
BEGIN
	-- =============================================================
	-- Cria um guid vazio
	-- =============================================================	
	DECLARE @guidVazio UNIQUEIDENTIFIER = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER);

	SELECT
		usu.usu_id 
		, usu.usu_login 
		, usu.usu_email
		, usu.usu_senha
		, usu.usu_criptografia
		, usu.usu_situacao
		, usu.pes_id
		, pes.pes_nome 
		, CASE WHEN (@trazerFoto = 1) THEN arq.arq_data ELSE NULL END AS foto 
		, pes.pes_sexo
		, pes.pes_dataNascimento
		, usu.usu_dataCriacao
		, usu.usu_dataAlteracao
	FROM
		SYS_Usuario usu WITH(NOLOCK)
		LEFT JOIN PES_Pessoa pes WITH(NOLOCK)
			ON usu.pes_id = pes.pes_id
			AND pes.pes_situacao <> 3
		LEFT JOIN CFG_Arquivo AS arq WITH(NOLOCK)
			ON pes.arq_idFoto = arq.arq_id
			AND arq.arq_situacao <> 3
	WHERE
		usu.ent_id = @ent_id
		AND ISNULL(usu.pes_id, @guidVazio) = COALESCE(@pes_id, usu.pes_id, @guidVazio)
		AND usu.usu_situacao <> 3
		AND (@dataAlteracao IS NULL OR Usu.usu_dataAlteracao >= @dataAlteracao)
	ORDER BY 
		usu.usu_login
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoEntidade_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 12/05/2010 09:03
-- Description:	Decrementa uma unidade no campo integridade do tipo de entidade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoEntidade_DECREMENTA_INTEGRIDADE]
		@ten_id uniqueidentifier
AS
BEGIN
	UPDATE SYS_TipoEntidade
	SET 		
		ten_integridade = ten_integridade - 1		
	WHERE 
		ten_id = @ten_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_Delete_Visoes]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 21/07/2010
-- Description:	Deleta permissões (com 0) para os módulos.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_Delete_Visoes]
	@sis_id int
	, @vis_id int
	, @mod_id int
AS
BEGIN
	DELETE FROM SYS_GrupoPermissao
	WHERE sis_id = @sis_id
		AND mod_id = @mod_id
		AND gru_id IN ( SELECT gru_id
						FROM SYS_GRUPO
						WHERE sis_id = @sis_id
							AND vis_id = @vis_id)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_PessoaContato_SelectBy_pes_id]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 10/02/2010 15:30
-- Description:	utilizado na busca de contatos da pessoa, retorna os contatos
--              da pessoa
--				filtrados por:
--					pes_id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_PessoaContato_SelectBy_pes_id]	
	@pes_id uniqueidentifier
AS
BEGIN
	SELECT		
		psc_id as id
		,PES_PessoaContato.tmc_id
		,tmc_nome
		,psc_contato as contato		
		,psc_contato as enc_contato
	FROM
		PES_PessoaContato WITH (NOLOCK)
	INNER JOIN
		SYS_TipoMeioContato WITH (NOLOCK) on SYS_TipoMeioContato.tmc_id = PES_PessoaContato.tmc_id
	WHERE		
		psc_situacao <> 3
		AND tmc_situacao <> 3
		AND pes_id = @pes_id
	ORDER BY
		tmc_nome, psc_contato
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_DiaNaoUtil_UPDATE_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 05/02/2010 09:27
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente ao
--				Dia Não Util. Filtrada por: 
--					dnu_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_DiaNaoUtil_UPDATE_Situacao]	
		@dnu_id uniqueidentifier
		,@dnu_situacao TINYINT
		,@dnu_dataAlteracao DATETIME
AS
BEGIN
	UPDATE SYS_DiaNaoUtil
	SET 
		dnu_situacao = @dnu_situacao
		,dnu_dataAlteracao = @dnu_dataAlteracao
	WHERE 
		dnu_id = @dnu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPadrao_UPDATE]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_TemaPadrao_UPDATE]
	@tep_id INT
	, @tep_nome VARCHAR (100)
	, @tep_descricao VARCHAR(200)
	, @tep_tipoMenu TINYINT
	, @tep_exibeLinkLogin BIT
	, @tep_tipoLogin TINYINT
	, @tep_exibeLogoCliente BIT
	, @tep_situacao TINYINT
	, @tep_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPadrao 
	SET 
		tep_nome = @tep_nome 
		, tep_tipoMenu = @tep_tipoMenu 
		, tep_descricao = @tep_descricao
		, tep_exibeLinkLogin = @tep_exibeLinkLogin 
		, tep_tipoLogin = @tep_tipoLogin 
		, tep_exibeLogoCliente = @tep_exibeLogoCliente
		, tep_situacao = @tep_situacao 
		, tep_dataAlteracao = @tep_dataAlteracao 

	WHERE 
		tep_id = @tep_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_Update_Situacao]'
GO
-- ===================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 01/22/2010 08:59
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ;2 – Bloqueado ; 3 – Excluído) referente ao
--				Tipo de Documentacao. Filtrada por: 
--					tdo_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_Update_Situacao]	
		@tdo_id uniqueidentifier
		,@tdo_situacao TINYINT
		,@tdo_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_TipoDocumentacao 
	SET 
		tdo_situacao = @tdo_situacao
		,tdo_dataAlteracao = @tdo_dataAlteracao
	WHERE 
		tdo_id = @tdo_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_UASuperior]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 10/05/2011 16:10
-- Description:	Retorna as unidades adminstrativas 
--				filtrando por: entidade e unidade administrativa superior.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_UnidadeAdministrativa_SelectBy_UASuperior]
	@ent_id UNIQUEIDENTIFIER
	, @uad_idSupeior UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
      ent_id
      , uad_id
      , tua_id
      , uad_codigo
      , uad_nome
      , uad_sigla
      , uad_idSuperior
      , uad_situacao
      , uad_dataCriacao
      , uad_dataAlteracao
      , uad_integridade
	FROM
      SYS_UnidadeAdministrativa WITH(NOLOCK)
	WHERE
	  uad_situacao <> 3
      AND ent_id = @ent_id
      AND ((@uad_idSupeior IS NULL) AND (uad_idSuperior IS NULL)) OR (uad_idSuperior = @uad_idSupeior)

	SELECT @@ROWCOUNT	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_GrupoPermissao_Select_Visoes]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 21/07/2010
-- Description:	Seleciona as visões que possuem permissão módulo do sistema
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_GrupoPermissao_Select_Visoes] 
	@sis_id int
	, @mod_id int
AS
BEGIN
	SELECT	vis.vis_id
		, vis.vis_nome
	FROM
		SYS_VisaoModulo vsm WITH (NOLOCK)
	INNER JOIN SYS_Visao vis WITH (NOLOCK)
		ON vsm.vis_id = vis.vis_id
	WHERE sis_id = @sis_id
		AND mod_id = @mod_id
	ORDER BY
		vis.vis_id
	
	SELECT @@ROWCOUNT
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_DiaNaoUtil_UPDATE]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 05/02/2010 09:28
-- Description:	Altera o grupo preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_DiaNaoUtil_UPDATE]
		@dnu_id uniqueidentifier
		,@dnu_descricao VARCHAR(400)
		,@dnu_vigenciaInicio DATE
		,@dnu_vigenciaFim DATE
		,@dnu_dataAlteracao DateTime
AS
BEGIN
	UPDATE SYS_DiaNaoUtil 
	SET 
		dnu_descricao = @dnu_descricao
		,dnu_vigenciaInicio = @dnu_vigenciaInicio
		,dnu_vigenciaFim = @dnu_vigenciaFim
		,dnu_dataAlteracao = @dnu_dataAlteracao
	WHERE 
		dnu_id = @dnu_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPadrao_UpdateSituacao]'
GO

CREATE PROCEDURE [dbo].[NEW_CFG_TemaPadrao_UpdateSituacao]
	@tep_id INT
	, @tep_situacao TINYINT
	, @tep_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPadrao 
	SET 
		tep_situacao = @tep_situacao 
		, tep_dataAlteracao = @tep_dataAlteracao 

	WHERE 
		tep_id = @tep_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_UPDATE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 25/01/2010 15:09
-- Description:	Altera o Tipo de Documento preservando a data da criação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_UPDATE]
		  @tdo_id				UNIQUEIDENTIFIER
		, @tdo_nome				VARCHAR(100)
		, @tdo_sigla			VARCHAR(10)
		, @tdo_validacao		TINYINT
		, @tdo_situacao			TINYINT
		, @tdo_dataAlteracao	DATETIME
		, @tdo_classificacao	TINYINT = NULL
		, @tdo_atributos		VARCHAR(1024) = NULL
AS
BEGIN
	
	DECLARE 
		  @tdo_classificacaoTmp TINYINT = 99
		, @tdo_atributosTmp		VARCHAR(1024)
		, @stringDefault		VARCHAR(512)  = NULL 
		, @objetosDefault		VARCHAR(1024) = ''

	-- Valida se o parâmetro @tdo_classificacao é nulo, para colocar o valor default do campo
	IF (@tdo_classificacao IS NULL) BEGIN
		
		SET @tdo_classificacaoTmp = (	SELECT	ISNULL(td.tdo_classificacao, 99) 
										FROM	SYS_TipoDocumentacao td WITH (NOLOCK) 
										WHERE	td.tdo_id = @tdo_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @tdo_classificacaoTmp = @tdo_classificacao
	END 



	-- Valida se o parâmetro @tdo_atributos é nulo
	IF (@tdo_atributos IS NULL) BEGIN
		
		-- Executa as procedures para selecionar as informações sobre os atributos default
		EXEC NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault 
			  @ExibirRetorno      = 0
	 		, @AtributosDefault   = @stringDefault OUTPUT
			, @NomeObjetosDefault = @objetosDefault OUTPUT

		--
		SET @tdo_atributosTmp = (	SELECT	ISNULL(td.tdo_atributos, @stringDefault)
									FROM	SYS_TipoDocumentacao td WITH (NOLOCK) 
									WHERE	td.tdo_id = @tdo_id )

	END 
	ELSE BEGIN
		
		-- Caso contrário, atualiza o registro com o valor do parâmetro
		SET @tdo_atributosTmp = @tdo_atributos
	END 



	UPDATE 
		SYS_TipoDocumentacao 
	SET 
		  tdo_nome			= @tdo_nome
		, tdo_sigla			= @tdo_sigla
		, tdo_validacao		= @tdo_validacao
		, tdo_situacao		= @tdo_situacao
		, tdo_dataAlteracao = @tdo_dataAlteracao
		, tdo_classificacao = @tdo_classificacaoTmp
		, tdo_atributos		= @tdo_atributosTmp
	WHERE 
		tdo_id = @tdo_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_DiaNaoUtil_Select]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 04/02/2010 15:01
-- Description:	utilizado na busca de dias não util, retorna os dias não util
--              que não foram excluídas logicamente,
--				filtrados por:
--					nome, abrangência e data
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_DiaNaoUtil_Select]	
	@dnu_nome VARCHAR(100)
	,@dnu_abrangencia TINYINT
	,@dnu_data DATE
	,@dnu_recorrencia TINYINT
AS
BEGIN
	SELECT 
		D.dnu_id
		,D.dnu_nome
		,D.cid_id
		,D.unf_id
		,D.dnu_vigenciaInicio
		,D.dnu_vigenciaFim
		,CASE D.dnu_abrangencia
			WHEN 1 THEN 'Federal'
			WHEN 2 THEN 'Estadual (' + UF.unf_sigla + ')'
			WHEN 3 THEN 'Municipal (' + C.cid_nome + ' - ' + UF.unf_sigla + ')'
		 END AS dnu_abrangencia
		,CASE D.dnu_recorrencia
			WHEN 0 THEN CONVERT(VARCHAR,D.dnu_data,103)
			WHEN 1 THEN LEFT(CONVERT(VARCHAR,D.dnu_data,103),5) + ' - Recorrente'
		 END AS dnu_data
		,CONVERT(VARCHAR,D.dnu_vigenciaInicio,103) + ' - ' + ISNULL(CONVERT(VARCHAR,D.dnu_vigenciaFim,103),'*') AS dnu_vigencia
	FROM
		SYS_DiaNaoUtil D WITH (NOLOCK)
	LEFT JOIN END_UnidadeFederativa UF WITH (NOLOCK)
		ON UF.unf_id = D.unf_id
		AND unf_situacao <> 3
	LEFT JOIN END_Cidade C WITH (NOLOCK)
		ON C.cid_id = D.cid_id
		AND cid_situacao <> 3
		
	WHERE
		D.dnu_situacao <> 3
		AND (@dnu_nome is null or dnu_nome LIKE '%' + @dnu_nome + '%')
		AND (@dnu_abrangencia is null or dnu_abrangencia = @dnu_abrangencia)					
		AND ((@dnu_data is null) or (dnu_data = @dnu_data and @dnu_recorrencia = 0) or ((DATEPART(dd, dnu_data) = DATEPART(dd, @dnu_data)) AND (DATEPART(mm, dnu_data) = DATEPART(mm, @dnu_data)) and @dnu_recorrencia = 1))					
		AND ((@dnu_recorrencia = 0 and dnu_recorrencia = 1) or (@dnu_recorrencia = 1 and dnu_recorrencia = 0) or (@dnu_recorrencia = 2))
	
	ORDER BY 
		MONTH(dnu_data), DAY(dnu_data), YEAR(dnu_data)
	
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_TemaPadrao_CarregarPorNome]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 14/01/2015
-- Description:	Carrega tema padrão por nome.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_CFG_TemaPadrao_CarregarPorNome] 
	@tep_nome VARCHAR(100)
AS
BEGIN
	SELECT 
		tep.tep_id,
		tep.tep_nome,
		tep.tep_descricao,
		tep.tep_tipoMenu,
		tep.tep_exibeLinkLogin,
		tep.tep_tipoLogin,
		tep.tep_exibeLogoCliente,
		tep.tep_situacao,
		tep.tep_dataCriacao,
		tep.tep_dataAlteracao
	FROM
		CFG_TemaPadrao tep WITH(NOLOCK)
	WHERE
		LTRIM(RTRIM(tep.tep_nome)) = LTRIM(RTRIM(@tep_nome))
		AND tep.tep_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_SELECTBy_Nome]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 28/01/2010 09:02
-- Description:	utilizado na busca de nome de tipos de documentacao, retorna quantidade
--				dos tipos de documentacao que não foram excluídos logicamente,
--				filtrados por:
--					nome, id
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_SELECTBy_Nome]
	@tdo_nome varchar(100)
	, @tdo_id uniqueidentifier
AS
BEGIN
	SELECT
		tdo_id
		, tdo_nome

 	FROM
 		SYS_TipoDocumentacao WITH (NOLOCK)
 	WHERE
 		tdo_situacao <> 3
		AND UPPER(tdo_nome) = UPPER(@tdo_nome)
		AND (@tdo_id is null or tdo_id <> @tdo_id)
	ORDER BY
		tdo_nome
	
	SELECT @@ROWCOUNT	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Usuario_SelecionaPorDocumentoDataNascimento]'
GO
-- =============================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 21/05/2014
-- Description:	Seleciona usuário por documento e data de nascimento.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_Usuario_SelecionaPorDocumentoDataNascimento] 
	@ent_id UNIQUEIDENTIFIER, 
	@psd_numero VARCHAR(50),
	@tdo_id UNIQUEIDENTIFIER,
	@pes_dataNascimento DATETIME
AS
BEGIN
	;WITH tbPessoa AS
	(
		SELECT
			pes.pes_id,
			pes.pes_nome
		FROM
			PES_Pessoa pes WITH(NOLOCK)
			INNER JOIN PES_PessoaDocumento psd WITH(NOLOCK)
				ON pes.pes_id = psd.pes_id
				AND psd.tdo_id = @tdo_id
				AND psd.psd_numero = @psd_numero
				AND psd.psd_situacao <> 3
		WHERE
			pes.pes_dataNascimento = @pes_dataNascimento
			AND pes.pes_situacao <> 3	
	)
	
	SELECT
		usu.usu_id
		, CASE 
			WHEN usu.usu_integracaoAD = 1 
				THEN usu.usu_dominio + '\' + usu.usu_login
			ELSE
				usu.usu_login
		  END AS usu_login
		, usu.usu_email
		, pes.pes_nome
		, usu.usu_situacao
		, CASE usu.usu_situacao 
			WHEN 1 THEN 'Ativo'
			WHEN 2 THEN 'Bloqueado'
			WHEN 4 THEN 'Padrão do Sistema'
			WHEN 5 THEN 'Senha Expirada'
		  END AS usu_situacaoNome
		, ent.ent_nomeFantasia
		, ent.ent_razaoSocial
		, usu.ent_id
		, usu.usu_dominio
		, usu.usu_integracaoAD
		, usu_integracaoExterna
	FROM
		tbPessoa pes
		INNER JOIN SYS_Usuario usu WITH(NOLOCK)
			ON pes.pes_id = usu.pes_id
			AND usu.ent_id = @ent_id
			AND usu_situacao <> 3
		INNER JOIN SYS_Entidade ent WITH(NOLOCK)
			ON usu.ent_id = ent.ent_id
			AND ent.ent_situacao <> 3
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 10:26
-- Description:	Seleciona o valor do campo integridade da tabela de pessoas
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_Select_Integridade]
		@pes_id uniqueidentifier
AS
BEGIN
	SELECT 			
		pes_integridade
	FROM
		PES_Pessoa WITH (NOLOCK)
	WHERE 
		pes_id = @pes_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPadrao_LOAD]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPadrao_LOAD]
	@tep_id Int
	
AS
BEGIN
	SELECT	Top 1
		 tep_id  
		, tep_nome 
		, tep_descricao 
		, tep_tipoMenu 
		, tep_exibeLinkLogin 
		, tep_tipoLogin 
		, tep_exibeLogoCliente 
		, tep_situacao 
		, tep_dataCriacao 
		, tep_dataAlteracao 

 	FROM
 		CFG_TemaPadrao
	WHERE 
		tep_id = @tep_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_SelectBy_All]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 01/22/2010 13:50
-- Description:	utilizado na busca de Tipo Documentacao, retorna as entidades
--              que não foram excluídas logicamente,
--				filtrados por:
--					id, nome, sigla, situacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_SelectBy_All]	
	  @tdo_id		UNIQUEIDENTIFIER
	, @tdo_nome		VARCHAR(100)
	, @tdo_sigla	VARCHAR(10)
	, @tdo_situacao TINYINT
AS
BEGIN
	SELECT 
		  tdo_id
		, tdo_nome
		
		, CASE tdo_validacao
			WHEN 1 THEN 'CPF'
			WHEN 2 THEN 'Somente números'
		END 
		AS tdo_validacao
		
		, tdo_sigla
		
		, CASE tdo_situacao 
			WHEN 1 THEN 'Não'
			WHEN 2 THEN 'Sim'			
		END 
		AS tdo_situacao

		, tdo_classificacao

		, CASE 
			WHEN tdo_classificacao = 1  THEN 'CPF'
            WHEN tdo_classificacao = 2  THEN 'RG'
            WHEN tdo_classificacao = 3  THEN 'PIS'
            WHEN tdo_classificacao = 4  THEN 'NIS'
            WHEN tdo_classificacao = 5  THEN 'Título de Eleitor'
            WHEN tdo_classificacao = 6  THEN 'CNH'
            WHEN tdo_classificacao = 7  THEN 'Reservista'
            WHEN tdo_classificacao = 8  THEN 'CTPS'
            WHEN tdo_classificacao = 9  THEN 'RNE'
            WHEN tdo_classificacao = 10 THEN 'Guarda'
            WHEN tdo_classificacao = 99 THEN 'Outros'
			ELSE							 'N/D'
		END 
		AS tdo_classificacaoNome

		, tdo_atributos

	FROM
		SYS_TipoDocumentacao WITH (NOLOCK)
	WHERE
		    tdo_situacao <> 3
		AND (@tdo_id is null or tdo_id = @tdo_id)		
		AND (@tdo_nome is null or tdo_nome LIKE '%' + @tdo_nome + '%')	
		AND (@tdo_sigla is null or tdo_sigla LIKE '%' + @tdo_sigla + '%')		
		AND (@tdo_situacao is null or tdo_situacao = @tdo_situacao)				
	ORDER BY
		tdo_nome
		
	SELECT @@ROWCOUNT			
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_ServidorRelatorio_LOADBY_idRelatorioSistema]'
GO
-- =================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 11/05/2011 11:40
-- Description:	Retorna o servidor de relatório filtrado por sistema
--				, entidade e id do relatório.
--				Caso exista mais de um servidor de relatórios para
--				a entidade do sistema ele escolhe um de forma 
--				randômica.
-- =================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_ServidorRelatorio_LOADBY_idRelatorioSistema]
	@ent_id UNIQUEIDENTIFIER
	, @sis_id INT
	, @rlt_id INT
AS
BEGIN	
	SELECT TOP 1
		S.sis_id
		, S.ent_id
		, S.srr_id
		, S.srr_nome
		, S.srr_descricao
		, S.srr_remoteServer
		, S.srr_usuario
		, S.srr_senha
		, S.srr_dominio
		, S.srr_diretorioRelatorios
		, S.srr_pastaRelatorios
		, S.srr_situacao
		, S.srr_dataCriacao
		, S.srr_dataAlteracao
	FROM
		CFG_ServidorRelatorio S WITH(NOLOCK)	
		
	INNER JOIN	CFG_RelatorioServidorRelatorio A WITH(NOLOCK)
		ON A.ent_id = S.ent_id 
		AND A.sis_id = S.sis_id 
		AND A.srr_id = S.srr_id
		
	INNER JOIN CFG_Relatorio R WITH(NOLOCK)
			ON A.rlt_id = R.rlt_id 
			AND R.rlt_situacao = 1
			
	WHERE
		S.srr_situacao = 1
		AND S.ent_id = @ent_id
		AND S.sis_id = @sis_id
		AND A.rlt_id = @rlt_id
	ORDER BY NEWID()	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_SELECTBY_pes_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_SELECTBY_pes_id]
	@pes_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		usu_id
		,usu_login
		,usu_dominio
		,usu_email
		,usu_senha
		,usu_criptografia
		,usu_situacao
		,usu_dataCriacao
		,usu_dataAlteracao
		,pes_id
		,usu_integridade
		,ent_id
		,usu_integracaoAD
		,usu_integracaoExterna
	FROM
		SYS_Usuario WITH(NOLOCK)
	WHERE 
		pes_id = @pes_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 10:26
-- Description:	Incrementa uma unidade no campo integridade da tabela de pessoas
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_INCREMENTA_INTEGRIDADE]
		@pes_id uniqueidentifier

AS
BEGIN
	UPDATE PES_Pessoa
	SET 
		pes_integridade = pes_integridade + 1
	WHERE 
		pes_id = @pes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPadrao_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPadrao_INSERT]
	@tep_nome VarChar (100)
	, @tep_descricao VarChar (200)
	, @tep_tipoMenu TinyInt
	, @tep_exibeLinkLogin Bit
	, @tep_tipoLogin TinyInt
	, @tep_exibeLogoCliente Bit
	, @tep_situacao TinyInt
	, @tep_dataCriacao DateTime
	, @tep_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		CFG_TemaPadrao
		( 
			tep_nome 
			, tep_descricao 
			, tep_tipoMenu 
			, tep_exibeLinkLogin 
			, tep_tipoLogin 
			, tep_exibeLogoCliente 
			, tep_situacao 
			, tep_dataCriacao 
			, tep_dataAlteracao 
 
		)
	VALUES
		( 
			@tep_nome 
			, @tep_descricao 
			, @tep_tipoMenu 
			, @tep_exibeLinkLogin 
			, @tep_tipoLogin 
			, @tep_exibeLogoCliente 
			, @tep_situacao 
			, @tep_dataCriacao 
			, @tep_dataAlteracao 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_Select_Integridade]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:32
-- Description:	Seleciona o valor do campo integridade da tabela de tipo documentação
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_Select_Integridade]
		@tdo_id uniqueidentifier
AS
BEGIN
	SELECT 			
		tdo_integridade
	FROM
		SYS_TipoDocumentacao WITH (NOLOCK)
	WHERE 
		tdo_id = @tdo_id
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_GrupoPermissao_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_GrupoPermissao_UPDATE]
		@gru_id uniqueidentifier
		,@sis_id Int
		,@mod_id Int
		,@grp_consultar Bit
		,@grp_inserir Bit
		,@grp_alterar Bit
		,@grp_excluir Bit

AS
BEGIN
	UPDATE SYS_GrupoPermissao
	SET 
		grp_consultar = @grp_consultar
		,grp_inserir = @grp_inserir
		,grp_alterar = @grp_alterar
		,grp_excluir = @grp_excluir
	WHERE 
		gru_id = @gru_id
	and sis_id = @sis_id
	and mod_id = @mod_id
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_RelatorioServidorRelatorio_DELETE_ALL]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 12/05/2011 17:50
-- Description:	Apaga todos os relatórios do servidor de relatórios.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_RelatorioServidorRelatorio_DELETE_ALL]
	@sis_id INT
	, @ent_id UNIQUEIDENTIFIER
	, @srr_id INT
AS
BEGIN
	DELETE FROM 
		CFG_RelatorioServidorRelatorio 
	WHERE 
		sis_id = @sis_id 
		AND ent_id = @ent_id 
		AND srr_id = @srr_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_ModuloSiteMap_Select_Gerar_msm_id]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 23/07/2010
-- Description:	Gera um código utilizado na página para novos registros do grid.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_ModuloSiteMap_Select_Gerar_msm_id]
	@sis_id int
	, @mod_id int
AS
BEGIN
	SELECT	ISNULL(MAX(msm_id), 0) + 1
	FROM SYS_ModuloSiteMap WITH (NOLOCK)
	WHERE sis_id = @sis_id
		AND mod_id = @mod_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_Update_Situacao]'
GO
-- ===================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 30/04/2010 16:10
-- Description:	utilizado para realizar alteração do campo de situação 
--				(1 – Ativo ; 3 – Excluído) referente a pessoa. Filtrada por:
--				end_id
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_Update_Situacao]	
	@pes_id uniqueidentifier
	,@pes_dataAlteracao DateTime
	,@pes_situacao TINYINT	
AS
BEGIN
	--Exclui logicamente as certidões civis da pessoa
	UPDATE PES_CertidaoCivil
	SET 
		ctc_dataAlteracao = @pes_dataAlteracao
		,ctc_situacao = @pes_situacao
	WHERE 
		pes_id = @pes_id
		
	--Exclui logicamente os contatos da pessoa
	UPDATE PES_PessoaContato
	SET 
		psc_dataAlteracao = @pes_dataAlteracao
		,psc_situacao = @pes_situacao
	WHERE 
		pes_id = @pes_id		
			
	--Exclui logicamente os documentos da pessoa
	UPDATE PES_PessoaDocumento
	SET 
		psd_dataAlteracao = @pes_dataAlteracao
		,psd_situacao = @pes_situacao
	WHERE 
		pes_id = @pes_id
		
	--Exclui logicamente os endreços da pessoa
	UPDATE PES_PessoaEndereco
	SET 
		pse_dataAlteracao = @pes_dataAlteracao
		,pse_situacao = @pes_situacao
	WHERE 
		pes_id = @pes_id							
	
	-- Foto da pessoa.
	DECLARE @arq_idFoto BIGINT = 
	(
		SELECT arq_idFoto 
		FROM PES_Pessoa P WITH(NOLOCK)
		WHERE 
			pes_id = @pes_id
	);
	IF (@arq_idFoto IS NOT NULL AND @arq_idFoto > 0)
	BEGIN
		-- Exclui logicamente a foto, se existir foto para a pessoa.
		UPDATE CFG_Arquivo
		SET arq_situacao = 3
		WHERE
			arq_id = @arq_idFoto
	END
	
	--Exclui logicamente a pessoa
	UPDATE PES_Pessoa
	SET 
		pes_dataAlteracao = @pes_dataAlteracao
		,pes_situacao = @pes_situacao
	WHERE 
		pes_id = @pes_id

	RETURN ISNULL(@@ROWCOUNT,-1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Usuario_SELECTBY_ent_id]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Usuario_SELECTBY_ent_id]
	@ent_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		usu_id
		,usu_login
		,usu_dominio
		,usu_email
		,usu_senha
		,usu_criptografia
		,usu_situacao
		,usu_dataCriacao
		,usu_dataAlteracao
		,pes_id
		,usu_integridade
		,ent_id
		,usu_integracaoAD
		,usu_integracaoExterna

	FROM
		SYS_Usuario WITH(NOLOCK)
	WHERE 
		ent_id = @ent_id 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 10:51
-- Description:	Decrementa uma unidade no campo integridade da tabela de pessoas
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_DECREMENTA_INTEGRIDADE]
		@pes_id uniqueidentifier

AS
BEGIN
	UPDATE PES_Pessoa 
	SET 
		pes_integridade = pes_integridade - 1
	WHERE 
		pes_id = @pes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)				
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPadrao_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPadrao_UPDATE]
	@tep_id INT
	, @tep_nome VARCHAR (100)
	, @tep_descricao VARCHAR (200)
	, @tep_tipoMenu TINYINT
	, @tep_exibeLinkLogin BIT
	, @tep_tipoLogin TINYINT
	, @tep_exibeLogoCliente BIT
	, @tep_situacao TINYINT
	, @tep_dataCriacao DATETIME
	, @tep_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_TemaPadrao 
	SET 
		tep_nome = @tep_nome 
		, tep_descricao = @tep_descricao 
		, tep_tipoMenu = @tep_tipoMenu 
		, tep_exibeLinkLogin = @tep_exibeLinkLogin 
		, tep_tipoLogin = @tep_tipoLogin 
		, tep_exibeLogoCliente = @tep_exibeLogoCliente 
		, tep_situacao = @tep_situacao 
		, tep_dataCriacao = @tep_dataCriacao 
		, tep_dataAlteracao = @tep_dataAlteracao 

	WHERE 
		tep_id = @tep_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_INCREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Incrementa uma unidade no campo integridade da tabela de tipo documentacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_INCREMENTA_INTEGRIDADE]
	@tdo_id UNIQUEIDENTIFIER

AS
BEGIN

	UPDATE 
		SYS_TipoDocumentacao
	SET 
		tdo_integridade = (tdo_integridade + 1)
	WHERE 
		tdo_id = @tdo_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAD_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAD_LOAD]
	@usa_id bigInt
	
AS
BEGIN
	SELECT	Top 1
		 usa_id  
		, usu_id 
		, usa_acao 
		, usa_status 
		, usa_dataAcao 
		, usa_origemAcao 
		, usa_dataProcessado 
		, usa_dados 

 	FROM
 		LOG_UsuarioAD
	WHERE 
		usa_id = @usa_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_GrupoPermissao_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_GrupoPermissao_SELECT]
	
AS
BEGIN
	SELECT 
		gru_id
		,sis_id
		,mod_id
		,grp_consultar
		,grp_inserir
		,grp_alterar
		,grp_excluir
		
	FROM 
		SYS_GrupoPermissao WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_PES_Pessoa_UPDATE]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 13/05/2010 13:48
-- Description:	Altera a pessoa preservando a data da criação e a integridade
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_PES_Pessoa_UPDATE]
	  @pes_id				UNIQUEIDENTIFIER
	, @pes_nome				VARCHAR(200)
	, @pes_nome_abreviado	VARCHAR(50)
	, @pai_idNacionalidade	UNIQUEIDENTIFIER
	, @pes_naturalizado		BIT
	, @cid_idNaturalidade	UNIQUEIDENTIFIER
	, @pes_dataNascimento	DATE
	, @pes_estadoCivil		TINYINT
	, @pes_racaCor			TINYINT
	, @pes_sexo				TINYINT
	, @pes_idFiliacaoPai	UNIQUEIDENTIFIER
	, @pes_idFiliacaoMae	UNIQUEIDENTIFIER
	, @tes_id				UNIQUEIDENTIFIER
	, @pes_foto				VARBINARY(MAX) = NULL
	, @pes_situacao			TINYINT			
	, @pes_dataAlteracao	DATETIME			
	, @arq_idFoto			BIGINT = NULL
	, @pes_nomeSocial		VARCHAR(200) = NULL

AS
BEGIN
	
	UPDATE 
		PES_Pessoa
	SET 	
		  pes_nome = @pes_nome
		, pes_nome_abreviado = @pes_nome_abreviado
		, pai_idNacionalidade = @pai_idNacionalidade
		, pes_naturalizado = @pes_naturalizado
		, cid_idNaturalidade = @cid_idNaturalidade
		, pes_dataNascimento = @pes_dataNascimento
		, pes_estadoCivil = @pes_estadoCivil
		, pes_racaCor = @pes_racaCor
		, pes_sexo = @pes_sexo
		, pes_idFiliacaoPai = @pes_idFiliacaoPai
		, pes_idFiliacaoMae = @pes_idFiliacaoMae
		, tes_id = @tes_id
		, pes_foto = @pes_foto
		, pes_situacao = @pes_situacao		
		, pes_dataAlteracao = @pes_dataAlteracao
		, arq_idFoto = @arq_idFoto
		, pes_nomeSocial = @pes_nomeSocial
	WHERE 
		pes_id = @pes_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPadrao_DELETE]'
GO


CREATE PROCEDURE [dbo].[STP_CFG_TemaPadrao_DELETE]
	@tep_id INT

AS
BEGIN
	DELETE FROM 
		CFG_TemaPadrao 
	WHERE 
		tep_id = @tep_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoDocumentacao_DECREMENTA_INTEGRIDADE]'
GO
-- ========================================================================
-- Author:		Lais Tiemi Mukai
-- Create date: 12/05/2010 11:30
-- Description:	Decrementa uma unidade no campo integridade da tabela de tipo documentacao
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoDocumentacao_DECREMENTA_INTEGRIDADE]
		@tdo_id uniqueidentifier
AS
BEGIN

	UPDATE 
		SYS_TipoDocumentacao
	SET 
		tdo_integridade = (tdo_integridade - 1)
	WHERE 
		tdo_id = @tdo_id
		
	RETURN ISNULL(@@ROWCOUNT,-1)		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAD_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAD_INSERT]
	@usu_id UniqueIdentifier
	, @usa_acao TinyInt
	, @usa_status TinyInt
	, @usa_dataAcao DateTime
	, @usa_origemAcao TinyInt
	, @usa_dataProcessado DateTime
	, @usa_dados Varchar(MAX)

AS
BEGIN
	INSERT INTO 
		LOG_UsuarioAD
		( 
			usu_id 
			, usa_acao 
			, usa_status 
			, usa_dataAcao 
			, usa_origemAcao 
			, usa_dataProcessado 
			, usa_dados 
 
		)
	VALUES
		( 
			@usu_id 
			, @usa_acao 
			, @usa_status 
			, @usa_dataAcao 
			, @usa_origemAcao 
			, @usa_dataProcessado 
			, @usa_dados 
 
		)
		
		SELECT ISNULL(SCOPE_IDENTITY(),-1)

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_GrupoPermissao_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_GrupoPermissao_LOAD]
	@gru_id uniqueidentifier
	,@sis_id Int
	,@mod_id Int
	
AS
BEGIN
	SELECT	Top 1
		gru_id
		,sis_id
		,mod_id
		,grp_consultar
		,grp_inserir
		,grp_alterar
		,grp_excluir

 	FROM
 		SYS_GrupoPermissao
	WHERE 
		gru_id = @gru_id
	and sis_id = @sis_id
	and mod_id = @mod_id
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_VisaoModuloMenu_SelectBy_SiteMapMenu]'
GO
-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: 27/07/2010
-- Description:	Retorna o menu principal de determinados módulo e sistema.
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_VisaoModuloMenu_SelectBy_SiteMapMenu]
	@sis_id int
	, @mod_id int
AS
BEGIN
	SELECT 
		msm_id
	FROM
		SYS_VisaoModuloMenu  WITH (NOLOCK)
	WHERE
		mod_id = @mod_id
		AND	sis_id = @sis_id
	GROUP BY
		mod_id, sis_id, msm_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Endereco_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Endereco_UPDATE]
		@end_id uniqueidentifier
		,@end_cep VarChar (8)
		,@end_logradouro VarChar (200)
		,@end_distrito VarChar (100)
		,@end_zona TinyInt
		,@end_bairro VarChar (100)
		,@cid_id uniqueidentifier
		,@end_situacao TinyInt
		,@end_dataCriacao DateTime
		,@end_dataAlteracao DateTime
		,@end_integridade int

AS
BEGIN
	UPDATE END_Endereco
	SET 		
		end_cep = @end_cep
		,end_logradouro = @end_logradouro
		,end_distrito = @end_distrito
		,end_zona = @end_zona
		,end_bairro = @end_bairro		
		,cid_id = @cid_id
		,end_situacao = @end_situacao
		,end_dataCriacao = @end_dataCriacao
		,end_dataAlteracao = @end_dataAlteracao
		,end_integridade = @end_integridade
	WHERE 
		end_id = @end_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_TemaPadrao_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_TemaPadrao_SELECT]
	
AS
BEGIN
	SELECT 
		tep_id
		,tep_nome
		,tep_descricao
		,tep_tipoMenu
		,tep_exibeLinkLogin
		,tep_tipoLogin
		,tep_exibeLogoCliente
		,tep_situacao
		,tep_dataCriacao
		,tep_dataAlteracao

	FROM 
		CFG_TemaPadrao WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_Parametro_SelectBy_par_chave]'
GO
-- ========================================================================
-- Author:		Jean Michel Marques da Silva
-- Create date: 27/01/2010 16:50
-- Description:	utilizado na busca de parametros
--				filtrados por:
--					par_chave
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_Parametro_SelectBy_par_chave]	
	@par_chave VARCHAR(100)
AS
BEGIN
	SELECT 		
		par_valor
	FROM
		SYS_Parametro WITH (NOLOCK)		
	WHERE
		par_situacao = 1				
		AND par_vigenciaInicio <= CAST(GETDATE() AS DATE)
		AND (par_vigenciaFim IS NULL OR (par_vigenciaFim IS NOT NULL AND par_vigenciaFim >= CAST(GETDATE() AS DATE)))				
		AND (@par_chave IS NULL OR par_chave = @par_chave)						
		
	SELECT @@ROWCOUNT
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Arquivo_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_CFG_Arquivo_LOAD]
	@arq_id BigInt
	
AS
BEGIN
	SELECT	Top 1
		 arq_id  
		, arq_nome 
		, arq_tamanhoKB 
		, arq_typeMime 
		, arq_data 
		, arq_situacao 
		, arq_dataCriacao 
		, arq_dataAlteracao 

 	FROM
 		CFG_Arquivo
	WHERE 
		arq_id = @arq_id
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAD_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAD_UPDATE]
	@usa_id BIGINT
	, @usu_id UNIQUEIDENTIFIER
	, @usa_acao TINYINT
	, @usa_status TINYINT
	, @usa_dataAcao DATETIME
	, @usa_origemAcao TINYINT
	, @usa_dataProcessado DATETIME
	, @usa_dados VARCHAR(MAX)

AS
BEGIN
	UPDATE LOG_UsuarioAD 
	SET 
		usu_id = @usu_id 
		, usa_acao = @usa_acao 
		, usa_status = @usa_status 
		, usa_dataAcao = @usa_dataAcao 
		, usa_origemAcao = @usa_origemAcao 
		, usa_dataProcessado = @usa_dataProcessado 
		, usa_dados = @usa_dados 

	WHERE 
		usa_id = @usa_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_GrupoPermissao_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_GrupoPermissao_INSERT]
		@gru_id uniqueidentifier
		,@sis_id Int
		,@mod_id Int
		,@grp_consultar Bit
		,@grp_inserir Bit
		,@grp_alterar Bit
		,@grp_excluir Bit

AS
BEGIN
	INSERT INTO 
		SYS_GrupoPermissao
		( 
			gru_id
			,sis_id
			,mod_id
			,grp_consultar
			,grp_inserir
			,grp_alterar
			,grp_excluir
 
		)
	VALUES
		( 
			@gru_id
			,@sis_id
			,@mod_id
			,@grp_consultar
			,@grp_inserir
			,@grp_alterar
			,@grp_excluir
 
		)
			SELECT ISNULL(SCOPE_IDENTITY(),-1)
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Endereco_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Endereco_SELECT]
	
AS
BEGIN
	SELECT
		end_id 
		,end_cep
		,end_logradouro
		,end_distrito
		,end_zona		
		,end_bairro
		,cid_id
		,end_situacao
		,end_dataCriacao
		,end_dataAlteracao
		,end_integridade
	FROM 
		END_Endereco WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_Select_ColunaValor_BD]'
GO
-- ========================================================================
-- Author:		William Hiroshi Otani Awaji
-- Create date: 30/04/2010 12:34
-- Description:	Faz verificação do uso de uma valor para uma coluna no banco 
--				GestaoCore e em todos os bancos dependentes listados na tabela
--				SYS_BancoRelacionado.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_Select_ColunaValor_BD]		
	@coluna VARCHAR(100)
	,@valor uniqueidentifier
	,@tabela_raiz VARCHAR(100)
AS
BEGIN

	-- Declarar variaveis usadas na procedure
	DECLARE @COLUNA_NOME NVARCHAR(100)
	DECLARE @COLUNA_VALOR NVARCHAR(100)
	DECLARE @COLUNA_SITUACAO NVARCHAR(100)
	DECLARE @BANCO NVARCHAR(100)
	DECLARE @TABELA NVARCHAR(100)
	DECLARE @sql NVARCHAR(4000)

	-- Criar Tabela temporária para Listar bancos, tabelas, colunas de situação, nomes das colunas e o valor. 
	-- Essas informações são usadas no BD GestaoCore e todos os outros BDs dependendes do GestaoCore.
	CREATE TABLE #TMP_BD ( 
							coluna_nome nvarchar(100)
							,coluna_valor nvarchar(100)
							,coluna_situacao nvarchar(100)
							,banco nvarchar(100)
							,tabela nvarchar(100)
						)

	-- Criar tabela temporária para registro do numero de vezes na qual é encontrada o valor da coluna para o banco,tabela,
	-- e situacao cadastradas no #TMP_BD
	CREATE TABLE #TMP_COUNT ( 
								cont INT
							)

	-- Inserir registros na #TMP_BD sobre o BANCO GestaoCore
	INSERT INTO #TMP_BD 
	select  COLUMN_NAME
			,@valor AS VALOR
			,(select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS B WHERE B.TABLE_NAME = A.TABLE_NAME AND B.COLUMN_NAME LIKE '%situacao%')
			,TABLE_CATALOG AS BANCO
			,TABLE_NAME
	from INFORMATION_SCHEMA.COLUMNS A
	where A.COLUMN_NAME LIKE @coluna AND A.TABLE_NAME NOT LIKE @tabela_raiz


	-- Declara cursor
	DECLARE db_cursor CURSOR 
	FOR
		--Select dos bancos relacionados ao GestaoCore
		SELECT bdr_nome FROM SYS_BancoRelacionado
		
		OPEN db_cursor
		FETCH NEXT FROM db_cursor 
		INTO @BANCO
		--Faz inserção de registros na #TMP_BD sobre os BANCOS relacionados ao GestaoCore.
		WHILE @@FETCH_STATUS = 0 
			BEGIN 
			SET @sql = 'INSERT INTO #TMP_BD '
			SET @sql = @sql + ' SELECT COLUMN_NAME '
			SET @sql = @sql + '		,'+CONVERT(NVARCHAR,@valor)+' AS VALOR '
			SET @sql = @sql + '		,(select COLUMN_NAME from ['+@BANCO+'].INFORMATION_SCHEMA.COLUMNS B WHERE B.TABLE_NAME = A.TABLE_NAME AND B.COLUMN_NAME LIKE ''%situacao%'')'
			SET @sql = @sql + '		,TABLE_CATALOG '
			SET @sql = @sql + '		,TABLE_NAME '
			SET @sql = @sql + '	FROM ['+@BANCO+'].INFORMATION_SCHEMA.COLUMNS A '
			SET @sql = @sql + '	WHERE A.COLUMN_NAME LIKE '''+@coluna+''' AND A.TABLE_NAME NOT LIKE '''+@tabela_raiz+''''
			PRINT(@sql)
			EXEC(@sql)
			FETCH NEXT FROM db_cursor 
			INTO @BANCO
		END
		CLOSE db_cursor 
	DEALLOCATE db_cursor

	SET @sql = ''
	-- Declara cursor
	DECLARE db_cursor CURSOR
	FOR
		--Select dos registros cadastrados na #TMP_BD
		SELECT  coluna_nome
				,coluna_valor
				,coluna_situacao
				,banco
				,tabela
		FROM #TMP_BD
		
		OPEN db_cursor
		FETCH NEXT FROM db_cursor 
		INTO @COLUNA_NOME, @COLUNA_VALOR, @COLUNA_SITUACAO, @BANCO, @TABELA
		--Faz inserção na #TMP_COUNT baseado no COUNT dos registros do #TMP_BD para verificar se valor da coluna (#TMP_BD.COLUNA_VALOR) é usada (cadastrada no BD)
		--para a tabela (#TMP_BD.TABELA), no banco (#TMP_BD.BANCO), com situação (#TMP_BD.COLUNA_SITUACAO) diferente de 3 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			SET @sql = 'DECLARE @count INT SELECT @count = COUNT('+ @COLUNA_NOME +') FROM ['+ @BANCO +']..[' + @TABELA + '] ' 
			SET @sql = @sql + 'WHERE RTRIM(LTRIM([' + @COLUNA_NOME + '])) = ' + @COLUNA_VALOR + ' ' 
			SET @sql = @sql + 'AND RTRIM(LTRIM([' + @COLUNA_SITUACAO + '])) <> 3 '
			SET @sql = @sql + 'INSERT INTO #TMP_COUNT VALUES (@count)'
				PRINT @sql
			EXEC(@sql)
			FETCH NEXT FROM db_cursor 
			INTO @COLUNA_NOME, @COLUNA_VALOR, @COLUNA_SITUACAO, @BANCO, @TABELA
		END
		CLOSE db_cursor 
	DEALLOCATE db_cursor

	--Select da soma dos contadores. Caso maior que 0 @coluna no @valor é usada no BD. Caso igual a 0, não é usada.
	SELECT SUM(cont) AS Contador
	FROM #TMP_COUNT
	DROP TABLE #TMP_COUNT
	--SELECT *
	--FROM #TMP_BD
	DROP TABLE #TMP_BD

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Arquivo_INSERT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Arquivo_INSERT]
	@arq_nome VarChar (256)
	, @arq_tamanhoKB BigInt
	, @arq_typeMime VarChar (200)
	, @arq_data VARBINARY(MAX)
	, @arq_situacao TinyInt
	, @arq_dataCriacao DateTime
	, @arq_dataAlteracao DateTime

AS
BEGIN
	INSERT INTO 
		CFG_Arquivo
		( 
			arq_nome 
			, arq_tamanhoKB 
			, arq_typeMime 
			, arq_data 
			, arq_situacao 
			, arq_dataCriacao 
			, arq_dataAlteracao 
 
		)
	VALUES
		( 
			@arq_nome 
			, @arq_tamanhoKB 
			, @arq_typeMime 
			, @arq_data 
			, @arq_situacao 
			, @arq_dataCriacao 
			, @arq_dataAlteracao 
 
		)
		
		SELECT cast(ISNULL(SCOPE_IDENTITY(),-1) as bigint )

	
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAD_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAD_DELETE]
	@usa_id BIGINT

AS
BEGIN
	DELETE FROM 
		LOG_UsuarioAD 
	WHERE 
		usa_id = @usa_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_GrupoPermissao_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_GrupoPermissao_DELETE]
	@gru_id uniqueidentifier
	,@sis_id Int
	,@mod_id Int

AS
BEGIN
	DELETE FROM 
		SYS_GrupoPermissao
	WHERE 
		gru_id = @gru_id
	and sis_id = @sis_id
	and mod_id = @mod_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Endereco_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_END_Endereco_LOAD]
	@end_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		end_id
		,end_cep
		,end_logradouro
		,end_distrito
	    ,end_zona		
		,end_bairro
		,cid_id
		,end_situacao
		,end_dataCriacao
		,end_dataAlteracao
		,end_integridade
 	FROM
 		END_Endereco
	WHERE 
		end_id = @end_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Arquivo_UPDATE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Arquivo_UPDATE]
	@arq_id BIGINT
	, @arq_nome VARCHAR (256)
	, @arq_tamanhoKB BIGINT
	, @arq_typeMime VARCHAR (200)
	, @arq_data VARBINARY(MAX)
	, @arq_situacao TINYINT
	, @arq_dataCriacao DATETIME
	, @arq_dataAlteracao DATETIME

AS
BEGIN
	UPDATE CFG_Arquivo 
	SET 
		arq_nome = @arq_nome 
		, arq_tamanhoKB = @arq_tamanhoKB 
		, arq_typeMime = @arq_typeMime 
		, arq_data = @arq_data 
		, arq_situacao = @arq_situacao 
		, arq_dataCriacao = @arq_dataCriacao 
		, arq_dataAlteracao = @arq_dataAlteracao 

	WHERE 
		arq_id = @arq_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_UsuarioAD_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_LOG_UsuarioAD_SELECT]
	
AS
BEGIN
	SELECT 
		usa_id
		,usu_id
		,usa_acao
		,usa_status
		,usa_dataAcao
		,usa_origemAcao
		,usa_dataProcessado
		,usa_dados

	FROM 
		LOG_UsuarioAD WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Grupo_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Grupo_UPDATE]
		@gru_id uniqueidentifier
		,@gru_nome VarChar (50)
		,@gru_situacao TinyInt
		,@gru_dataCriacao DateTime
		,@gru_dataAlteracao DateTime
		,@vis_id Int
		,@sis_id Int
		,@gru_integridade Int

AS
BEGIN
	UPDATE SYS_Grupo 
	SET 
		gru_nome = @gru_nome
		,gru_situacao = @gru_situacao
		,gru_dataCriacao = @gru_dataCriacao
		,gru_dataAlteracao = @gru_dataAlteracao
		,vis_id = @vis_id
		,sis_id = @sis_id
		,gru_integridade = @gru_integridade
	WHERE 
		gru_id = @gru_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_TipoMeioContato_SelecionaContatosDaPessoa]'
GO
-- =============================================================================
-- Author:		Heitor Henrique Martins
-- Create date: 20/05/2011 18:40
-- Description:	Seleciona todos contatos da pessoa não excluídos logicamente.
-- =============================================================================
CREATE PROCEDURE [dbo].[NEW_SYS_TipoMeioContato_SelecionaContatosDaPessoa]	
	@pes_id uniqueidentifier
AS
BEGIN

	WITH PessoaContato AS
	(
		SELECT
			tmc_id
			,psc_contato
			,psc_id
		FROM
			PES_PessoaContato WITH (NOLOCK)
		WHERE
			pes_id = @pes_id
			AND psc_situacao <> 3
	)
	
	SELECT 
		Tmc.tmc_id
		,Tmc.tmc_nome
		,Tmc.tmc_validacao
		,Psc.psc_contato as contato
		,Psc.psc_id as id
		, CASE
			WHEN Psc.psc_contato IS NULL THEN 'false'
			ELSE 'true'
		  END AS banco
	FROM
		SYS_TipoMeioContato AS Tmc WITH (NOLOCK)
	LEFT JOIN PessoaContato	AS Psc
		ON Psc.tmc_id = Tmc.tmc_id
	WHERE
		Tmc.tmc_situacao <> 3
	ORDER BY
		Tmc.tmc_nome, Psc.psc_contato
		
	SELECT @@ROWCOUNT			
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_SYS_VisaoModuloMenu_SelectBy_GerarOrdem]'
GO

-- =============================================
-- Author:		Juliana Ferrarezi
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NEW_SYS_VisaoModuloMenu_SelectBy_GerarOrdem]
	@sis_id int
	, @mod_idPai int
	, @vis_id int
AS
BEGIN
	SELECT 
		ISNULL(MAX(vmm.vmm_ordem), 0) + 1
	FROM
		SYS_Modulo modu  WITH (NOLOCK)
	INNER JOIN SYS_VisaoModuloMenu vmm  WITH (NOLOCK)
		ON modu.mod_id = vmm.mod_id
		AND modu.sis_id = vmm.sis_id
		AND modu.mod_id = vmm.mod_id
	WHERE
		mod_situacao <> 3
		AND ((@mod_idPai is null and modu.mod_idPai is null) or (modu.mod_idPai = @mod_idPai))
		AND modu.sis_id = @sis_id
		AND vmm.vis_id = @vis_id
END

		

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Endereco_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_END_Endereco_INSERT]
		@end_cep VarChar (8)
		,@end_logradouro VarChar (200)
		,@end_distrito VarChar (100)
		,@end_zona TinyInt
		,@end_bairro VarChar (100)
		,@cid_id uniqueidentifier
		,@end_situacao TinyInt
		,@end_dataCriacao DateTime
		,@end_dataAlteracao DateTime
		,@end_integridade int
AS
BEGIN
	DECLARE @ID TABLE (ID Uniqueidentifier)
	INSERT INTO 
		END_Endereco
		( 
			end_cep
			,end_logradouro
			,end_distrito
			,end_zona
			,end_bairro
			,cid_id
			,end_situacao
			,end_dataCriacao
			,end_dataAlteracao
			,end_integridade
		)
	OUTPUT inserted.end_id INTO @ID
	VALUES
		( 
			@end_cep
			,@end_logradouro
			,@end_distrito
			,@end_zona			
			,@end_bairro
			,@cid_id
			,@end_situacao
			,@end_dataCriacao
			,@end_dataAlteracao
			,@end_integridade
		)
	SELECT ID FROM @ID

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Arquivo_DELETE]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Arquivo_DELETE]
	@arq_id BIGINT

AS
BEGIN
	DELETE FROM 
		CFG_Arquivo 
	WHERE 
		arq_id = @arq_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Grupo_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Grupo_SELECT]
	
AS
BEGIN
	SELECT 
		gru_id
		,gru_nome
		,gru_situacao
		,gru_dataCriacao
		,gru_dataAlteracao
		,vis_id
		,sis_id
		,gru_integridade
		
	FROM 
		SYS_Grupo WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_END_Endereco_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_END_Endereco_DELETE]
	@end_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		END_Endereco	
	WHERE 
		end_id = @end_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_CFG_Arquivo_SELECT]'
GO

CREATE PROCEDURE [dbo].[STP_CFG_Arquivo_SELECT]
	
AS
BEGIN
	SELECT 
		arq_id
		,arq_nome
		,arq_tamanhoKB
		,arq_typeMime
		,arq_data
		,arq_situacao
		,arq_dataCriacao
		,arq_dataAlteracao

	FROM 
		CFG_Arquivo WITH(NOLOCK) 
	
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_SYS_Grupo_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_SYS_Grupo_LOAD]
	@gru_id uniqueidentifier
	
AS
BEGIN
	SELECT	Top 1
		gru_id
		,gru_nome
		,gru_situacao
		,gru_dataCriacao
		,gru_dataAlteracao
		,vis_id
		,sis_id
		,gru_integridade

 	FROM
 		SYS_Grupo
	WHERE 
		gru_id = @gru_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_END_Cidade_SelectBy_Nome_UF]'
GO
-- ========================================================================
-- Author:		Aline Dornelas
-- Create date: 20/06/2011 12:50
-- Description:	
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_END_Cidade_SelectBy_Nome_UF]
	@cid_nome VARCHAR(200)
	,@unf_sigla VARCHAR(2)	
AS
BEGIN
	SELECT 
		cid_id	
		, cid.pai_id
		, cid.unf_id
		, cid_nome
		, cid_ddd
		, cid_situacao
		, cid_integridade
	FROM
		END_Cidade AS cid WITH (NOLOCK)	
	LEFT JOIN END_UnidadeFederativa AS uf WITH (NOLOCK)
		ON cid.unf_id = uf.unf_id
		AND unf_situacao <> 3	
	WHERE
		cid_situacao <> 3
		AND UPPER(cid_nome) = UPPER(@cid_nome)
		AND UPPER(unf_sigla) = UPPER(@unf_sigla)										
	ORDER BY
		cid_nome
		
	SELECT @@ROWCOUNT		
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[JOB_NAME] [nvarchar] (150) NOT NULL,
[JOB_GROUP] [nvarchar] (150) NOT NULL,
[DESCRIPTION] [nvarchar] (250) NULL,
[NEXT_FIRE_TIME] [bigint] NULL,
[PREV_FIRE_TIME] [bigint] NULL,
[PRIORITY] [int] NULL,
[TRIGGER_STATE] [nvarchar] (16) NOT NULL,
[TRIGGER_TYPE] [nvarchar] (8) NOT NULL,
[START_TIME] [bigint] NOT NULL,
[END_TIME] [bigint] NULL,
[CALENDAR_NAME] [nvarchar] (200) NULL,
[MISFIRE_INSTR] [int] NULL,
[JOB_DATA] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_TRIGGERS] on [dbo].[QTZ_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Triggers] ADD CONSTRAINT [PK_QTZ_TRIGGERS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_C] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_C] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [CALENDAR_NAME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_JG] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_JG] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [JOB_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_J] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_J] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_NFT_MISFIRE] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_NFT_MISFIRE] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [MISFIRE_INSTR], [NEXT_FIRE_TIME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_NFT_ST_MISFIRE_GRP] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_NFT_ST_MISFIRE_GRP] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [MISFIRE_INSTR], [NEXT_FIRE_TIME], [TRIGGER_GROUP], [TRIGGER_STATE]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_NFT_ST_MISFIRE] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_NFT_ST_MISFIRE] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [MISFIRE_INSTR], [NEXT_FIRE_TIME], [TRIGGER_STATE]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_NEXT_FIRE_TIME] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_NEXT_FIRE_TIME] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [NEXT_FIRE_TIME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_G] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_G] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_N_G_STATE] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_N_G_STATE] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_GROUP], [TRIGGER_STATE]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_N_STATE] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_N_STATE] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [TRIGGER_STATE]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_STATE] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_STATE] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_STATE]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_T_NFT_ST] on [dbo].[QTZ_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_T_NFT_ST] ON [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_STATE], [NEXT_FIRE_TIME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Simple_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Simple_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[REPEAT_COUNT] [int] NOT NULL,
[REPEAT_INTERVAL] [bigint] NOT NULL,
[TIMES_TRIGGERED] [int] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_SIMPLE_TRIGGERS] on [dbo].[QTZ_Simple_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Simple_Triggers] ADD CONSTRAINT [PK_QTZ_SIMPLE_TRIGGERS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Simprop_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Simprop_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[STR_PROP_1] [nvarchar] (512) NULL,
[STR_PROP_2] [nvarchar] (512) NULL,
[STR_PROP_3] [nvarchar] (512) NULL,
[INT_PROP_1] [int] NULL,
[INT_PROP_2] [int] NULL,
[LONG_PROP_1] [bigint] NULL,
[LONG_PROP_2] [bigint] NULL,
[DEC_PROP_1] [numeric] (13, 4) NULL,
[DEC_PROP_2] [numeric] (13, 4) NULL,
[BOOL_PROP_1] [bit] NULL,
[BOOL_PROP_2] [bit] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_SIMPROP_TRIGGERS] on [dbo].[QTZ_Simprop_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Simprop_Triggers] ADD CONSTRAINT [PK_QTZ_SIMPROP_TRIGGERS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Job_Details]'
GO
CREATE TABLE [dbo].[QTZ_Job_Details]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[JOB_NAME] [nvarchar] (150) NOT NULL,
[JOB_GROUP] [nvarchar] (150) NOT NULL,
[DESCRIPTION] [nvarchar] (250) NULL,
[JOB_CLASS_NAME] [nvarchar] (250) NOT NULL,
[IS_DURABLE] [bit] NOT NULL,
[IS_NONCONCURRENT] [bit] NOT NULL,
[IS_UPDATE_DATA] [bit] NOT NULL,
[REQUESTS_RECOVERY] [bit] NOT NULL,
[JOB_DATA] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_JOB_DETAILS] on [dbo].[QTZ_Job_Details]'
GO
ALTER TABLE [dbo].[QTZ_Job_Details] ADD CONSTRAINT [PK_QTZ_JOB_DETAILS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CFG_Versao]'
GO
CREATE TABLE [dbo].[CFG_Versao]
(
[ver_id] [int] NOT NULL IDENTITY(1, 1),
[ver_Versao] [varchar] (15) NOT NULL,
[ver_DataCriacao] [datetime] NOT NULL,
[ver_DataAlteracao] [datetime] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CFG_Versao] on [dbo].[CFG_Versao]'
GO
ALTER TABLE [dbo].[CFG_Versao] ADD CONSTRAINT [PK_CFG_Versao] PRIMARY KEY CLUSTERED  ([ver_id]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NewUsers]'
GO
CREATE TABLE [dbo].[NewUsers]
(
[usu_id] [uniqueidentifier] NULL,
[usu_login] [nvarchar] (500) NULL,
[usu_senha] [nvarchar] (256) NULL,
[usu_status_sinc] [int] NULL,
[usu_senha_antiga] [nvarchar] (500) NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Blob_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Blob_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[BLOB_DATA] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Calendars]'
GO
CREATE TABLE [dbo].[QTZ_Calendars]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[CALENDAR_NAME] [nvarchar] (200) NOT NULL,
[CALENDAR] [image] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_CALENDARS] on [dbo].[QTZ_Calendars]'
GO
ALTER TABLE [dbo].[QTZ_Calendars] ADD CONSTRAINT [PK_QTZ_CALENDARS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [CALENDAR_NAME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Fired_Triggers]'
GO
CREATE TABLE [dbo].[QTZ_Fired_Triggers]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[ENTRY_ID] [nvarchar] (95) NOT NULL,
[TRIGGER_NAME] [nvarchar] (150) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL,
[INSTANCE_NAME] [nvarchar] (200) NOT NULL,
[FIRED_TIME] [bigint] NOT NULL,
[PRIORITY] [int] NOT NULL,
[STATE] [nvarchar] (16) NOT NULL,
[JOB_NAME] [nvarchar] (150) NULL,
[JOB_GROUP] [nvarchar] (150) NULL,
[IS_NONCONCURRENT] [bit] NULL,
[REQUESTS_RECOVERY] [bit] NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_FIRED_TRIGGERS] on [dbo].[QTZ_Fired_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Fired_Triggers] ADD CONSTRAINT [PK_QTZ_FIRED_TRIGGERS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [ENTRY_ID]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_TRIG_INST_NAME] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_TRIG_INST_NAME] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [INSTANCE_NAME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_INST_JOB_REQ_RCVRY] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_INST_JOB_REQ_RCVRY] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [INSTANCE_NAME], [REQUESTS_RECOVERY]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_JG] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_JG] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [JOB_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_J_G] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_J_G] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_TG] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_TG] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IDX_QTZ_FT_T_G] on [dbo].[QTZ_Fired_Triggers]'
GO
CREATE NONCLUSTERED INDEX [IDX_QTZ_FT_T_G] ON [dbo].[QTZ_Fired_Triggers] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Locks]'
GO
CREATE TABLE [dbo].[QTZ_Locks]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[LOCK_NAME] [nvarchar] (40) NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_LOCKS] on [dbo].[QTZ_Locks]'
GO
ALTER TABLE [dbo].[QTZ_Locks] ADD CONSTRAINT [PK_QTZ_LOCKS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [LOCK_NAME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Paused_Trigger_Grps]'
GO
CREATE TABLE [dbo].[QTZ_Paused_Trigger_Grps]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[TRIGGER_GROUP] [nvarchar] (150) NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_PAUSED_TRIGGER_GRPS] on [dbo].[QTZ_Paused_Trigger_Grps]'
GO
ALTER TABLE [dbo].[QTZ_Paused_Trigger_Grps] ADD CONSTRAINT [PK_QTZ_PAUSED_TRIGGER_GRPS] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [TRIGGER_GROUP]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[QTZ_Scheduler_State]'
GO
CREATE TABLE [dbo].[QTZ_Scheduler_State]
(
[SCHED_NAME] [nvarchar] (100) NOT NULL,
[INSTANCE_NAME] [nvarchar] (200) NOT NULL,
[LAST_CHECKIN_TIME] [bigint] NOT NULL,
[CHECKIN_INTERVAL] [bigint] NOT NULL
) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_QTZ_SCHEDULER_STATE] on [dbo].[QTZ_Scheduler_State]'
GO
ALTER TABLE [dbo].[QTZ_Scheduler_State] ADD CONSTRAINT [PK_QTZ_SCHEDULER_STATE] PRIMARY KEY CLUSTERED  ([SCHED_NAME], [INSTANCE_NAME]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[_users_ad]'
GO
CREATE TABLE [dbo].[_users_ad]
(
[seq] [int] NOT NULL IDENTITY(1, 1),
[nome] [varchar] (max) NULL,
[logon_user] [varchar] (500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [pk_users_ad] on [dbo].[_users_ad]'
GO
ALTER TABLE [dbo].[_users_ad] ADD CONSTRAINT [pk_users_ad] PRIMARY KEY CLUSTERED  ([seq]) ON [PRIMARY]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Parametro_Load_Ultimo_VigenciaFim_By_Chave]'
GO
CREATE PROCEDURE [dbo].[NEW_CFG_Parametro_Load_Ultimo_VigenciaFim_By_Chave]
	@par_chave VARCHAR(100)

AS
BEGIN
	SELECT
		TOP 1 *
	FROM CFG_Parametro WITH(NOLOCK)
	WHERE 
		par_situacao <> 3
		AND par_chave = @par_chave
		AND par_vigenciaFim IS NOT NULL
	ORDER BY 
		par_vigenciaInicio DESC
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Parametro_SelectBy_Chave_Valor]'
GO

-- ===================================================================
-- Author:		Carla Frascareli
-- Create date: 23/11/2010
-- Description:	Retorna os parâmetros da chave, com o mesmo valor.
-- ====================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Parametro_SelectBy_Chave_Valor]
	@par_chave VARCHAR(100)
	, @par_valor VARCHAR(1000)

AS
BEGIN
	SELECT 
		par_id, par_chave, par_valor, par_descricao, par_obrigatorio, par_situacao, 
		par_vigenciaInicio, par_vigenciaFim, par_dataCriacao, par_dataAlteracao
	FROM CFG_Parametro WITH(NOLOCK)
	WHERE
		par_situacao <> 3
		AND par_chave = @par_chave
		AND par_valor = @par_valor
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Parametro_Select]'
GO
-- ========================================================================
-- Author:		Cesar Henrique Marcusso
-- Create date: 06/01/2014 13:47
-- Description:	utilizado na busca parametros, retorna os parametros
--              que não foram excluídas logicamente.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_CFG_Parametro_Select]	

AS
BEGIN
	SELECT
		par_id
		,par_chave
		,par_obrigatorio
		
		--,CASE par_chave
		--	WHEN 'URL_WEBAPI_GESTAO_ACADEMICA' THEN  (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
		--	WHEN 'VERSAO_WEBAPI_GESTAO_ACADEMICA' THEN  (CASE par_valor WHEN 'True' THEN 'Sim' WHEN 'False' THEN 'Não' END )
		-- END AS par_valor_nome
	
		,par_valor --AS par_valor_nome
		,par_vigenciaFim
		,par_vigenciaInicio
		,CONVERT(VARCHAR,par_vigenciaInicio,103) + ' - ' + ISNULL(CONVERT(VARCHAR,par_vigenciaFim,103),'*') AS par_vigencia
		,par_descricao
		, par_valor

	FROM
		CFG_Parametro WITH (NOLOCK)
	WHERE
		par_situacao <> 3
		AND CAST(par_vigenciaInicio AS DATE) <= CAST(GETDATE() AS DATE)
		AND (par_vigenciaFim IS NULL OR CAST(par_vigenciaFim AS DATE) >= CAST(GETDATE() AS DATE))

	ORDER BY par_chave

	RETURN @@ROWCOUNT		
END


GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_CFG_Parametro_Select_TipoUABy_Chave]'
GO
CREATE PROCEDURE [dbo].[NEW_CFG_Parametro_Select_TipoUABy_Chave]
	@par_chave VARCHAR(100)

AS 
BEGIN

	SELECT 
		Tip.tua_id
		, Tip.tua_nome
	FROM CFG_Parametro Par WITH(NOLOCK)
	INNER JOIN Synonym_SYS_TipoUnidadeAdministrativa Tip WITH(NOLOCK)
		ON (Tip.tua_id = CAST(Par.par_valor AS UNIQUEIDENTIFIER))
	WHERE 
		Par.par_situacao <> 3
		AND Tip.tua_situacao <> 3
		AND par_chave = @par_chave
		AND GETDATE() BETWEEN par_vigenciaInicio AND COALESCE(par_vigenciaFim, GETDATE())
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_VerificarIntegridadeChaveDupla]'
GO
-- ========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 21/01/2015
-- Description:	Busca o valor para as chaves (campo1 e campo2) em outras tabelas do banco,
--				não considerando as tabelas no parâmetro @tabelasNaoVerificar.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_VerificarIntegridadeChaveDupla]
	@tabelasNaoVerificar VARCHAR(MAX)
	, @campo1 VARCHAR(50)
	, @campo2 VARCHAR(50)
	, @valorCampo1 VARCHAR(50)
	, @valorCampo2 VARCHAR(50)
AS
BEGIN
	DECLARE @Scripts Table (script VARCHAR(MAX));
	
	DECLARE @xmlTabelas XML;
	
	SELECT @xmlTabelas = CONVERT(xml,' <Tabelas> <tabela>' + REPLACE(@tabelasNaoVerificar,',','</tabela> <tabela>') + '</tabela>   </Tabelas> ')

	-- filtrando pelos valores informados e pela situação
	INSERT INTO @Scripts (script)
	SELECT 'SELECT TOP 1 1 AS Qt FROM ' + T.name + ' WITH(NOLOCK) ' + 
			' WHERE ' + C1.name + ' = ' + @valorCampo1 + 
			' AND ' + C2.name + ' = ' + @valorCampo2 +
			ISNULL((
				-- Campo Situação da tabela
				SELECT TOP 1 ' AND ' + C2.name + ' <> 3 ' -- Situação <> Excluído
				FROM sys.columns C2 WITH(NOLOCK)
				WHERE
					C2.object_id = T.object_id
					AND C2.name LIKE '%situacao'
			 ), ' ')
			+ CHAR(13)
	FROM sys.tables T WITH(NOLOCK)
	INNER JOIN sys.columns C1 WITH(NOLOCK)
	ON	-- Campo começa por
		C1.name LIKE @campo1 + '%'
		AND C1.object_id = T.object_id
	INNER JOIN sys.columns C2 WITH(NOLOCK)
	ON	-- Campo começa por
		C2.name LIKE @campo2 + '%'
		AND C2.object_id = T.object_id
	WHERE
		T.name NOT IN (SELECT LTRIM(RTRIM(xmlTabelas.noTabela.value('.','varchar(100)'))) FROM @xmlTabelas.nodes('/Tabelas/tabela') xmlTabelas(noTabela))

	DECLARE @script VARCHAR(MAX);
	DECLARE @QT Table (qt INT);
	
	WHILE (ISNULL((SELECT TOP 1 1 FROM @Scripts), 0) > 0) AND (ISNULL((SELECT SUM(ISNULL(qt,0)) FROM @QT), 0) <= 0)
	BEGIN
		
		SET @script = (SELECT TOP 1 script FROM @Scripts);
		PRINT @script;
		INSERT INTO @QT (qt)
		EXEC (@script);
		
		DELETE FROM @Scripts WHERE script = @script;
	END
	
	DECLARE @retorno INT;
	
	SELECT 
		@retorno = ISNULL(SUM(ISNULL(qt,0)),0)
	FROM @QT;
	
	RETURN ISNULL(@retorno, 0)
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_VerificarIntegridade]'
GO
-- ========================================================================
-- Author:		Daniel Jun Suguimoto
-- Create date: 20/01/2015
-- Description:	Busca o valor para a chave (campo) em outras tabelas do banco,
--				não considerando as tabelas no parâmetro @tabelasNaoVerificar.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_VerificarIntegridade]
	@tabelasNaoVerificar VARCHAR(MAX)
	, @campo VARCHAR(50)
	, @valorCampo VARCHAR(50)
AS
BEGIN
	DECLARE @Scripts Table (script VARCHAR(MAX));
	
	DECLARE @xmlTabelas XML;
	
	SELECT @xmlTabelas = CONVERT(xml,' <Tabelas> <tabela>' + REPLACE(@tabelasNaoVerificar,',','</tabela> <tabela>') + '</tabela>   </Tabelas> ')

	-- Traz um script que faz um select nas tabelas que possuem os 2 campos informados,
	-- filtrando pelos valores informados e pela situação
	INSERT INTO @Scripts (script)
	SELECT 'SELECT TOP 1 1 AS Qt FROM ' + T.name + ' WITH(NOLOCK) ' + 
			' WHERE ' + C1.name + ' = ' + @valorCampo +
			ISNULL((
				-- Campo Situação da tabela
				SELECT TOP 1 ' AND ' + C2.name + ' <> 3 ' -- Situação <> Excluído
				FROM sys.columns C2 WITH(NOLOCK)
				WHERE
					C2.object_id = T.object_id
					AND C2.name LIKE '%situacao'
			 ), ' ')
	
	FROM sys.tables T WITH(NOLOCK)
	INNER JOIN sys.columns C1 WITH(NOLOCK)
	ON	-- Campo começa por
		C1.name LIKE @campo + '%'
		AND C1.object_id = T.object_id
	WHERE
		T.name NOT IN (SELECT LTRIM(RTRIM(xmlTabelas.noTabela.value('.','varchar(100)'))) FROM @xmlTabelas.nodes('/Tabelas/tabela') xmlTabelas(noTabela))

	--SELECT * FROM @Scripts
	
	DECLARE @script VARCHAR(MAX);
	DECLARE @QT Table (qt INT);
	
	WHILE (ISNULL((SELECT TOP 1 1 FROM @Scripts), 0) > 0) AND (ISNULL((SELECT SUM(ISNULL(qt,0)) FROM @QT), 0) <= 0)
	BEGIN
		
		SET @script = (SELECT TOP 1 script FROM @Scripts);
		PRINT @script;
		INSERT INTO @QT (qt)
		EXEC (@script);
		
		DELETE FROM @Scripts WHERE script = @script;
	END
	
	DECLARE @retorno INT;
	
	SELECT 
		@retorno = ISNULL(SUM(ISNULL(qt,0)),0) 
	FROM @QT;
	
	RETURN ISNULL(@retorno, 0)
END

GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_PES_TipoDeficiencia_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_PES_TipoDeficiencia_DELETE]
	@tde_id uniqueidentifier

AS
BEGIN
	DELETE FROM 
		PES_NivelDeficiencia
	WHERE 
		tde_id = @tde_id

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[SYS_UnidadeAdministrativa]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativa] ADD CONSTRAINT [SYS_UnidadeAdministrativa_UadIdSuperiorDifUadId] CHECK (([uad_idSuperior]<>[uad_id]))
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_Pessoa]'
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_CFG_Arquivo] FOREIGN KEY ([arq_idFoto]) REFERENCES [dbo].[CFG_Arquivo] ([arq_id])
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_END_Cidade] FOREIGN KEY ([cid_idNaturalidade]) REFERENCES [dbo].[END_Cidade] ([cid_id])
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_END_Pais] FOREIGN KEY ([pai_idNacionalidade]) REFERENCES [dbo].[END_Pais] ([pai_id])
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_PES_Pessoa] FOREIGN KEY ([pes_idFiliacaoMae]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_PES_Pessoa1] FOREIGN KEY ([pes_idFiliacaoPai]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[PES_Pessoa] ADD CONSTRAINT [FK_PES_Pessoa_PES_TipoEscolaridade] FOREIGN KEY ([tes_id]) REFERENCES [dbo].[PES_TipoEscolaridade] ([tes_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[CFG_RelatorioServidorRelatorio]'
GO
ALTER TABLE [dbo].[CFG_RelatorioServidorRelatorio] ADD CONSTRAINT [FK_CFG_RelatorioServidorRelatorio_CFG_ServidorRelatorio] FOREIGN KEY ([sis_id], [ent_id], [srr_id]) REFERENCES [dbo].[CFG_ServidorRelatorio] ([sis_id], [ent_id], [srr_id])
GO
ALTER TABLE [dbo].[CFG_RelatorioServidorRelatorio] ADD CONSTRAINT [FK_CFG_RelatorioServidorRelatorio_CFG_Relatorio] FOREIGN KEY ([rlt_id]) REFERENCES [dbo].[CFG_Relatorio] ([rlt_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[CFG_ServidorRelatorio]'
GO
ALTER TABLE [dbo].[CFG_ServidorRelatorio] ADD CONSTRAINT [FK_CFG_ServidorRelatorio_SYS_Sistema] FOREIGN KEY ([sis_id]) REFERENCES [dbo].[SYS_Sistema] ([sis_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[CFG_TemaPaleta]'
GO
ALTER TABLE [dbo].[CFG_TemaPaleta] ADD CONSTRAINT [FK_CFG_TemaPaleta_CFG_TemaPadrao] FOREIGN KEY ([tep_id]) REFERENCES [dbo].[CFG_TemaPadrao] ([tep_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_Entidade]'
GO
ALTER TABLE [dbo].[SYS_Entidade] ADD CONSTRAINT [FK_SYS_Entidade_CFG_TemaPaleta] FOREIGN KEY ([tep_id], [tpl_id]) REFERENCES [dbo].[CFG_TemaPaleta] ([tep_id], [tpl_id])
GO
ALTER TABLE [dbo].[SYS_Entidade] ADD CONSTRAINT [FK_SYS_Entidade_SYS_Entidade_Superior] FOREIGN KEY ([ent_idSuperior]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_Entidade] ADD CONSTRAINT [FK_SYS_Entidade_SYS_TipoEntidade] FOREIGN KEY ([ten_id]) REFERENCES [dbo].[SYS_TipoEntidade] ([ten_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[LOG_UsuarioAPI]'
GO
ALTER TABLE [dbo].[LOG_UsuarioAPI] ADD CONSTRAINT [FK_LOG_UsuarioAPI_CFG_UsuarioAPI] FOREIGN KEY ([uap_id]) REFERENCES [dbo].[CFG_UsuarioAPI] ([uap_id])
GO
ALTER TABLE [dbo].[LOG_UsuarioAPI] ADD CONSTRAINT [FK_LOG_UsuarioAPI_SYS_Usuario] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[SYS_Usuario] ([usu_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[END_Endereco]'
GO
ALTER TABLE [dbo].[END_Endereco] ADD CONSTRAINT [FK_END_Endereco_END_Cidade] FOREIGN KEY ([cid_id]) REFERENCES [dbo].[END_Cidade] ([cid_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_CertidaoCivil]'
GO
ALTER TABLE [dbo].[PES_CertidaoCivil] ADD CONSTRAINT [FK_PES_CertidaoCivil_END_Cidade] FOREIGN KEY ([cid_idCartorio]) REFERENCES [dbo].[END_Cidade] ([cid_id])
GO
ALTER TABLE [dbo].[PES_CertidaoCivil] ADD CONSTRAINT [FK_PES_CertidaoCivil_END_UnidadeFederativa] FOREIGN KEY ([unf_idCartorio]) REFERENCES [dbo].[END_UnidadeFederativa] ([unf_id])
GO
ALTER TABLE [dbo].[PES_CertidaoCivil] ADD CONSTRAINT [FK_PES_CertidaoCivil_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_DiaNaoUtil]'
GO
ALTER TABLE [dbo].[SYS_DiaNaoUtil] ADD CONSTRAINT [FK_SYS_DiaNaoUtil_END_Cidade] FOREIGN KEY ([cid_id]) REFERENCES [dbo].[END_Cidade] ([cid_id])
GO
ALTER TABLE [dbo].[SYS_DiaNaoUtil] ADD CONSTRAINT [FK_SYS_DiaNaoUtil_END_UnidadeFederativa] FOREIGN KEY ([unf_id]) REFERENCES [dbo].[END_UnidadeFederativa] ([unf_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[END_Cidade]'
GO
ALTER TABLE [dbo].[END_Cidade] ADD CONSTRAINT [FK_END_Cidade_END_Pais] FOREIGN KEY ([pai_id]) REFERENCES [dbo].[END_Pais] ([pai_id])
GO
ALTER TABLE [dbo].[END_Cidade] ADD CONSTRAINT [FK_END_Cidade_END_UnidadeFederativa] FOREIGN KEY ([unf_id]) REFERENCES [dbo].[END_UnidadeFederativa] ([unf_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_PessoaEndereco]'
GO
ALTER TABLE [dbo].[PES_PessoaEndereco] ADD CONSTRAINT [FK_PES_PessoaEndereco_END_Endereco] FOREIGN KEY ([end_id]) REFERENCES [dbo].[END_Endereco] ([end_id])
GO
ALTER TABLE [dbo].[PES_PessoaEndereco] ADD CONSTRAINT [FK_PES_PessoaEndereco_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_EntidadeEndereco]'
GO
ALTER TABLE [dbo].[SYS_EntidadeEndereco] ADD CONSTRAINT [FK_SYS_EntidadeEndereco_END_Endereco] FOREIGN KEY ([end_id]) REFERENCES [dbo].[END_Endereco] ([end_id])
GO
ALTER TABLE [dbo].[SYS_EntidadeEndereco] ADD CONSTRAINT [FK_SYS_EntidadeEndereco_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UnidadeAdministrativaEndereco]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaEndereco] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativaEndereco_END_Endereco] FOREIGN KEY ([end_id]) REFERENCES [dbo].[END_Endereco] ([end_id])
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaEndereco] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativaEndereco_SYS_UnidadeAdministrativa] FOREIGN KEY ([ent_id], [uad_id]) REFERENCES [dbo].[SYS_UnidadeAdministrativa] ([ent_id], [uad_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[END_UnidadeFederativa]'
GO
ALTER TABLE [dbo].[END_UnidadeFederativa] ADD CONSTRAINT [FK_END_UnidadeFederativa_END_Pais] FOREIGN KEY ([pai_id]) REFERENCES [dbo].[END_Pais] ([pai_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_PessoaDocumento]'
GO
ALTER TABLE [dbo].[PES_PessoaDocumento] ADD CONSTRAINT [FK_PES_PessoaDocumento_END_Pais] FOREIGN KEY ([pai_idOrigem]) REFERENCES [dbo].[END_Pais] ([pai_id])
GO
ALTER TABLE [dbo].[PES_PessoaDocumento] ADD CONSTRAINT [FK_PES_PessoaDocumento_END_UnidadeFederativa] FOREIGN KEY ([unf_idEmissao]) REFERENCES [dbo].[END_UnidadeFederativa] ([unf_id])
GO
ALTER TABLE [dbo].[PES_PessoaDocumento] ADD CONSTRAINT [FK_PES_PessoaDocumento_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[PES_PessoaDocumento] ADD CONSTRAINT [FK_PES_PessoaDocumento_SYS_TipoDocumentacao] FOREIGN KEY ([tdo_id]) REFERENCES [dbo].[SYS_TipoDocumentacao] ([tdo_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[LOG_UsuarioADErro]'
GO
ALTER TABLE [dbo].[LOG_UsuarioADErro] ADD CONSTRAINT [FK_LOG_UsuarioADErro_LOG_UsuarioAD] FOREIGN KEY ([usa_id]) REFERENCES [dbo].[LOG_UsuarioAD] ([usa_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[LOG_UsuarioAD]'
GO
ALTER TABLE [dbo].[LOG_UsuarioAD] ADD CONSTRAINT [FK_LOG_UsuarioAD_SYS_Usuario] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[SYS_Usuario] ([usu_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_PessoaContato]'
GO
ALTER TABLE [dbo].[PES_PessoaContato] ADD CONSTRAINT [FK_PES_PessoaContato_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[PES_PessoaContato] ADD CONSTRAINT [FK_PES_PessoaContato_SYS_TipoMeioContato] FOREIGN KEY ([tmc_id]) REFERENCES [dbo].[SYS_TipoMeioContato] ([tmc_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PES_PessoaDeficiencia]'
GO
ALTER TABLE [dbo].[PES_PessoaDeficiencia] ADD CONSTRAINT [FK_PES_PessoaDeficiencia_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[PES_PessoaDeficiencia] ADD CONSTRAINT [FK_PES_PessoaDeficiencia_PES_TipoDeficiencia] FOREIGN KEY ([tde_id]) REFERENCES [dbo].[PES_TipoDeficiencia] ([tde_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_Usuario]'
GO
ALTER TABLE [dbo].[SYS_Usuario] ADD CONSTRAINT [FK_SYS_Usuario_PES_Pessoa] FOREIGN KEY ([pes_id]) REFERENCES [dbo].[PES_Pessoa] ([pes_id])
GO
ALTER TABLE [dbo].[SYS_Usuario] ADD CONSTRAINT [FK_SYS_Usuario_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_Usuario] ADD CONSTRAINT [FK_SYS_Usuario_SYS_IntegracaoExternaTipo] FOREIGN KEY ([usu_integracaoExterna]) REFERENCES [dbo].[SYS_IntegracaoExternaTipo] ([iet_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[QTZ_Cron_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Cron_Triggers] ADD CONSTRAINT [FK_QTZ_CRON_TRIGGERS_QTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[QTZ_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Triggers] ADD CONSTRAINT [FK_QTZ_TRIGGERS_QTZ_JOB_DETAILS] FOREIGN KEY ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) REFERENCES [dbo].[QTZ_Job_Details] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[QTZ_Simple_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Simple_Triggers] ADD CONSTRAINT [FK_QTZ_SIMPLE_TRIGGERS_QTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[QTZ_Simprop_Triggers]'
GO
ALTER TABLE [dbo].[QTZ_Simprop_Triggers] ADD CONSTRAINT [FK_QTZ_SIMPROP_TRIGGERS_QTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [dbo].[QTZ_Triggers] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_EntidadeContato]'
GO
ALTER TABLE [dbo].[SYS_EntidadeContato] ADD CONSTRAINT [FK_SYS_EntidadeContato_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_EntidadeContato] ADD CONSTRAINT [FK_SYS_EntidadeContato_SYS_TipoMeioContato] FOREIGN KEY ([tmc_id]) REFERENCES [dbo].[SYS_TipoMeioContato] ([tmc_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_SistemaEntidade]'
GO
ALTER TABLE [dbo].[SYS_SistemaEntidade] ADD CONSTRAINT [FK_SYS_SistemaEntidade_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_SistemaEntidade] ADD CONSTRAINT [FK_SYS_SistemaEntidade_SYS_Sistema] FOREIGN KEY ([sis_id]) REFERENCES [dbo].[SYS_Sistema] ([sis_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UnidadeAdministrativa]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativa] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativa_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativa] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativa_SYS_TipoUnidadeAdministrativa] FOREIGN KEY ([tua_id]) REFERENCES [dbo].[SYS_TipoUnidadeAdministrativa] ([tua_id])
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativa] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativa_SYS_UnidadeAdministrativa_Superior] FOREIGN KEY ([ent_id], [uad_idSuperior]) REFERENCES [dbo].[SYS_UnidadeAdministrativa] ([ent_id], [uad_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UsuarioGrupoUA]'
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupoUA] ADD CONSTRAINT [FK_SYS_UsuarioGrupoUA_SYS_Entidade] FOREIGN KEY ([ent_id]) REFERENCES [dbo].[SYS_Entidade] ([ent_id])
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupoUA] ADD CONSTRAINT [FK_SYS_UsuarioGrupoUA_SYS_UnidadeAdministrativa] FOREIGN KEY ([ent_id], [uad_id]) REFERENCES [dbo].[SYS_UnidadeAdministrativa] ([ent_id], [uad_id])
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupoUA] ADD CONSTRAINT [FK_SYS_UsuarioGrupoUA_SYS_UsuarioGrupo] FOREIGN KEY ([usu_id], [gru_id]) REFERENCES [dbo].[SYS_UsuarioGrupo] ([usu_id], [gru_id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_GrupoPermissao]'
GO
ALTER TABLE [dbo].[SYS_GrupoPermissao] ADD CONSTRAINT [FK_SYS_GrupoPermissao_SYS_Grupo] FOREIGN KEY ([gru_id]) REFERENCES [dbo].[SYS_Grupo] ([gru_id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[SYS_GrupoPermissao] ADD CONSTRAINT [FK_SYS_GrupoPermissao_SYS_Modulo] FOREIGN KEY ([sis_id], [mod_id]) REFERENCES [dbo].[SYS_Modulo] ([sis_id], [mod_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_ParametroGrupoPerfil]'
GO
ALTER TABLE [dbo].[SYS_ParametroGrupoPerfil] ADD CONSTRAINT [FK_SYS_ParametroGrupoPerfil_SYS_Grupo] FOREIGN KEY ([gru_id]) REFERENCES [dbo].[SYS_Grupo] ([gru_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UsuarioGrupo]'
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupo] ADD CONSTRAINT [FK_SYS_UsuarioGrupo_SYS_Grupo] FOREIGN KEY ([gru_id]) REFERENCES [dbo].[SYS_Grupo] ([gru_id])
GO
ALTER TABLE [dbo].[SYS_UsuarioGrupo] ADD CONSTRAINT [FK_SYS_UsuarioGrupo_SYS_Usuario] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[SYS_Usuario] ([usu_id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_Grupo]'
GO
ALTER TABLE [dbo].[SYS_Grupo] ADD CONSTRAINT [FK_SYS_Grupo_SYS_Visao] FOREIGN KEY ([vis_id]) REFERENCES [dbo].[SYS_Visao] ([vis_id])
GO
ALTER TABLE [dbo].[SYS_Grupo] ADD CONSTRAINT [FK_SYS_Grupo_SYS_Sistema] FOREIGN KEY ([sis_id]) REFERENCES [dbo].[SYS_Sistema] ([sis_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_IntegracaoExterna]'
GO
ALTER TABLE [dbo].[SYS_IntegracaoExterna] ADD CONSTRAINT [FK_SYS_IntegracaoExterna_SYS_IntegracaoExternaTipo] FOREIGN KEY ([iet_id]) REFERENCES [dbo].[SYS_IntegracaoExternaTipo] ([iet_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_ModuloSiteMap]'
GO
ALTER TABLE [dbo].[SYS_ModuloSiteMap] ADD CONSTRAINT [FK_SYS_ModuloSiteMap_SYS_Modulo] FOREIGN KEY ([sis_id], [mod_id]) REFERENCES [dbo].[SYS_Modulo] ([sis_id], [mod_id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_VisaoModuloMenu]'
GO
ALTER TABLE [dbo].[SYS_VisaoModuloMenu] ADD CONSTRAINT [FK_SYS_VisaoModuloMenu_SYS_ModuloSiteMap] FOREIGN KEY ([sis_id], [mod_id], [msm_id]) REFERENCES [dbo].[SYS_ModuloSiteMap] ([sis_id], [mod_id], [msm_id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[SYS_VisaoModuloMenu] ADD CONSTRAINT [FK_SYS_VisaoModuloMenu_SYS_VisaoModulo] FOREIGN KEY ([vis_id], [sis_id], [mod_id]) REFERENCES [dbo].[SYS_VisaoModulo] ([vis_id], [sis_id], [mod_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_Modulo]'
GO
ALTER TABLE [dbo].[SYS_Modulo] ADD CONSTRAINT [FK_SYS_Modulo_SYS_Sistema] FOREIGN KEY ([sis_id]) REFERENCES [dbo].[SYS_Sistema] ([sis_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_VisaoModulo]'
GO
ALTER TABLE [dbo].[SYS_VisaoModulo] ADD CONSTRAINT [FK_SYS_VisaoModulo_SYS_Modulo] FOREIGN KEY ([sis_id], [mod_id]) REFERENCES [dbo].[SYS_Modulo] ([sis_id], [mod_id])
GO
ALTER TABLE [dbo].[SYS_VisaoModulo] ADD CONSTRAINT [FK_SYS_VisaoModulo_SYS_Visao] FOREIGN KEY ([vis_id]) REFERENCES [dbo].[SYS_Visao] ([vis_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UnidadeAdministrativaContato]'
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaContato] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativaContato_SYS_TipoMeioContato] FOREIGN KEY ([tmc_id]) REFERENCES [dbo].[SYS_TipoMeioContato] ([tmc_id])
GO
ALTER TABLE [dbo].[SYS_UnidadeAdministrativaContato] ADD CONSTRAINT [FK_SYS_UnidadeAdministrativaContato_SYS_UnidadeAdministrativa] FOREIGN KEY ([ent_id], [uad_id]) REFERENCES [dbo].[SYS_UnidadeAdministrativa] ([ent_id], [uad_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UsuarioFalhaAutenticacao]'
GO
ALTER TABLE [dbo].[SYS_UsuarioFalhaAutenticacao] ADD CONSTRAINT [FK_SYS_UsuarioFalhaAutenticacao_SYS_Usuario] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[SYS_Usuario] ([usu_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[SYS_UsuarioSenhaHistorico]'
GO
ALTER TABLE [dbo].[SYS_UsuarioSenhaHistorico] ADD CONSTRAINT [FK_SYS_UsuarioSenhaHistorico_SYS_Usuario] FOREIGN KEY ([usu_id]) REFERENCES [dbo].[SYS_Usuario] ([usu_id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
