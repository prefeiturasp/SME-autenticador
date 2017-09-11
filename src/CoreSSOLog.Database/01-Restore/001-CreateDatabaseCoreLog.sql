SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_Sistema]'
GO
CREATE TABLE [dbo].[LOG_Sistema]
(
[log_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_LOG_Sistema_log_id] DEFAULT (newsequentialid()),
[log_dataHora] [datetime] NOT NULL CONSTRAINT [DF_LOG_Sistema_log_dataHora] DEFAULT (getdate()),
[log_ip] [varchar] (15) COLLATE Latin1_General_CI_AS NOT NULL,
[log_machineName] [varchar] (256) COLLATE Latin1_General_CI_AS NOT NULL,
[log_acao] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[log_descricao] [varchar] (max) COLLATE Latin1_General_CI_AS NOT NULL,
[sis_id] [int] NULL,
[sis_nome] [varchar] (50) COLLATE Latin1_General_CI_AS NULL,
[mod_id] [int] NULL,
[mod_nome] [varchar] (50) COLLATE Latin1_General_CI_AS NULL,
[usu_id] [uniqueidentifier] NULL,
[usu_login] [varchar] (100) COLLATE Latin1_General_CI_AS NULL,
[gru_id] [uniqueidentifier] NULL,
[gru_nome] [varchar] (50) COLLATE Latin1_General_CI_AS NULL,
[log_grupoUA] [varchar] (max) COLLATE Latin1_General_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Sistema_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Sistema_UPDATE]
	@log_id UNIQUEIDENTIFIER
	, @log_dataHora DATETIME
	, @log_ip VARCHAR (15)
	, @log_machineName VARCHAR (256)
	, @log_acao VARCHAR (50)
	, @log_descricao VARCHAR(MAX)
	, @sis_id INT
	, @sis_nome VARCHAR (50)
	, @mod_id INT
	, @mod_nome VARCHAR (50)
	, @usu_id uniqueidentifier
	, @usu_login VARCHAR (100)
	, @gru_id uniqueidentifier
	, @gru_nome VARCHAR (50)
	, @log_grupoUA VARCHAR(MAX)

AS
BEGIN
	UPDATE LOG_Sistema 
	SET 
		log_dataHora = @log_dataHora 
		, log_ip = @log_ip 
		, log_machineName = @log_machineName 
		, log_acao = @log_acao 
		, log_descricao = @log_descricao 
		, sis_id = @sis_id 
		, sis_nome = @sis_nome 
		, mod_id = @mod_id 
		, mod_nome = @mod_nome 
		, usu_id = @usu_id 
		, usu_login = @usu_login 
		, gru_id = @gru_id 
		, gru_nome = @gru_nome 
		, log_grupoUA = @log_grupoUA 
	WHERE 
		log_id = @log_id 
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Sistema_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Sistema_SELECT]
	
AS
BEGIN
	SELECT 
		log_id
		,log_dataHora
		,log_ip
		,log_machineName
		,log_acao
		,log_descricao
		,sis_id
		,sis_nome
		,mod_id
		,mod_nome
		,usu_id
		,usu_login
		,gru_id
		,gru_nome
		,log_grupoUA

	FROM 
		LOG_Sistema WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Sistema_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Sistema_LOAD]
		@log_id uniqueidentifier
AS
BEGIN
	SELECT	Top 1
		 log_id  
		, log_dataHora 
		, log_ip 
		, log_machineName 
		, log_acao 
		, log_descricao 
		, sis_id 
		, sis_nome 
		, mod_id 
		, mod_nome 
		, usu_id 
		, usu_login 
		, gru_id 
		, gru_nome 
		, log_grupoUA 

 	FROM
 		LOG_Sistema
	WHERE 
		log_id = @log_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Sistema_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Sistema_INSERT]
	@log_id uniqueidentifier
	, @log_dataHora DateTime
	, @log_ip VarChar (15)
	, @log_machineName VarChar (256)
	, @log_acao VarChar (50)
	, @log_descricao varchar(MAX)
	, @sis_id Int
	, @sis_nome VarChar (50)
	, @mod_id Int
	, @mod_nome VarChar (50)
	, @usu_id uniqueidentifier
	, @usu_login VarChar (100)
	, @gru_id uniqueidentifier
	, @gru_nome VarChar (50)
	, @log_grupoUA varchar(MAX)
AS
BEGIN
	INSERT INTO 
		LOG_Sistema
		( 
			log_id
			, log_dataHora 
			, log_ip 
			, log_machineName 
			, log_acao 
			, log_descricao 
			, sis_id 
			, sis_nome 
			, mod_id 
			, mod_nome 
			, usu_id 
			, usu_login 
			, gru_id 
			, gru_nome 
			, log_grupoUA 
		)
	VALUES
		( 
			@log_id
			, @log_dataHora 
			, @log_ip 
			, @log_machineName 
			, @log_acao 
			, @log_descricao 
			, @sis_id 
			, @sis_nome
			, @mod_id 
			, @mod_nome 
			, @usu_id 
			, @usu_login 
			, @gru_id 
			, @gru_nome 
			, @log_grupoUA 
		)
	SELECT ISNULL(@@ROWCOUNT, -1)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Sistema_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Sistema_DELETE]
	@log_id uniqueidentifier
AS
BEGIN
	DELETE FROM 
		LOG_Sistema 
	WHERE 
		log_id = @log_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_Erros]'
GO
CREATE TABLE [dbo].[LOG_Erros]
(
[err_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_LOG_Erros_err_id] DEFAULT (newsequentialid()),
[err_dataHora] [datetime] NOT NULL CONSTRAINT [DF_LOG_Erros_err_dataHora] DEFAULT (getdate()),
[err_ip] [varchar] (15) COLLATE Latin1_General_CI_AS NOT NULL,
[err_machineName] [varchar] (256) COLLATE Latin1_General_CI_AS NOT NULL,
[err_browser] [varchar] (256) COLLATE Latin1_General_CI_AS NULL,
[err_caminhoArq] [varchar] (2000) COLLATE Latin1_General_CI_AS NULL,
[err_descricao] [varchar] (max) COLLATE Latin1_General_CI_AS NOT NULL,
[err_erroBase] [varchar] (max) COLLATE Latin1_General_CI_AS NULL,
[err_tipoErro] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL,
[sis_id] [int] NULL,
[sis_decricao] [varchar] (100) COLLATE Latin1_General_CI_AS NULL,
[usu_id] [uniqueidentifier] NULL,
[usu_login] [varchar] (100) COLLATE Latin1_General_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Erros_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Erros_UPDATE]
	@err_id UNIQUEIDENTIFIER
	, @err_dataHora DATETIME
	, @err_ip VARCHAR (15)
	, @err_machineName VARCHAR (256)
	, @err_browser VARCHAR (256)
	, @err_caminhoArq VARCHAR (2000)
	, @err_descricao VarChar (MAX)
	, @err_erroBase VarChar (MAX)
	, @err_tipoErro VARCHAR (1000)
	, @sis_id INT
	, @sis_decricao VARCHAR (100)
	, @usu_id UNIQUEIDENTIFIER
	, @usu_login VARCHAR (100)

AS
BEGIN
	UPDATE LOG_Erros SET 
		err_dataHora = @err_dataHora 
		, err_ip = @err_ip 
		, err_machineName = @err_machineName 
		, err_browser = @err_browser 
		, err_caminhoArq = @err_caminhoArq 
		, err_descricao = @err_descricao 
		, err_erroBase = @err_erroBase 
		, err_tipoErro = @err_tipoErro 
		, sis_id = @sis_id 
		, sis_decricao = @sis_decricao 
		, usu_id = @usu_id 
		, usu_login = @usu_login 
	WHERE 
		err_id = @err_id 

		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Erros_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Erros_SELECT]
AS
BEGIN
	SELECT 
		err_id
		,err_dataHora
		,err_ip
		,err_machineName
		,err_browser
		,err_caminhoArq
		,err_descricao
		,err_erroBase
		,err_tipoErro
		,sis_id
		,sis_decricao
		,usu_id
		,usu_login
	FROM 
		LOG_Erros WITH(NOLOCK) 
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Erros_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Erros_LOAD]
	@err_id uniqueidentifier
AS
BEGIN
	SELECT	Top 1
		 err_id  
		, err_dataHora 
		, err_ip 
		, err_machineName 
		, err_browser 
		, err_caminhoArq 
		, err_descricao 
		, err_erroBase 
		, err_tipoErro 
		, sis_id 
		, sis_decricao 
		, usu_id 
		, usu_login 

 	FROM
 		LOG_Erros
	WHERE 
		err_id = @err_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Erros_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Erros_INSERT]
	@err_dataHora DateTime
	, @err_ip VarChar (15)
	, @err_machineName VarChar (256)
	, @err_browser VarChar (256)
	, @err_caminhoArq VarChar (2000)
	, @err_descricao VarChar (MAX)
	, @err_erroBase VarChar (MAX)
	, @err_tipoErro VarChar (1000)
	, @sis_id Int
	, @sis_decricao VarChar (100)
	, @usu_id UniqueIdentifier
	, @usu_login VarChar (100)

AS
BEGIN
	
	DECLARE @ID TABLE( ID UNIQUEIDENTIFIER );
	
	/*
	 [Carla F. 06/02/2013] Verificação adicionada para parar de inserir esse tipo de erro no log
	 da produção do RJ (somente para essas duas páginas), pois existem muitos registros desse erro, 
	 e isso não permite a consulta dos logs. Essa correção ficará até que o erro seja resolvido na produção.
	*/
	IF (@err_tipoErro <> 'MSTech.Data.Common.Exceptions.NullConnectionException'
		OR (@err_caminhoArq <> '~/BLUESUPPORT.aspx' AND @err_caminhoArq <> '~/PAGINACONTADORACESSO.aspx'))
	BEGIN
		INSERT INTO 
			LOG_Erros
			( 
				err_dataHora 
				, err_ip 
				, err_machineName 
				, err_browser 
				, err_caminhoArq 
				, err_descricao 
				, err_erroBase 
				, err_tipoErro 
				, sis_id 
				, sis_decricao 
				, usu_id 
				, usu_login 
	 
			)
		OUTPUT inserted.err_id INTO @ID
		VALUES
			( 
				@err_dataHora 
				, @err_ip 
				, @err_machineName 
				, @err_browser 
				, @err_caminhoArq 
				, @err_descricao 
				, @err_erroBase 
				, @err_tipoErro 
				, @sis_id 
				, @sis_decricao 
				, @usu_id 
				, @usu_login 
	 
			)
	END
	
	SELECT ID FROM @ID
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Erros_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Erros_DELETE]
@err_id uniqueidentifier	
AS
BEGIN
	DELETE FROM 
		LOG_Erros 
	WHERE 
		err_id = @err_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[LOG_Auditoria]'
GO
CREATE TABLE [dbo].[LOG_Auditoria]
(
[log_id] [uniqueidentifier] NOT NULL,
[aud_id] [int] NOT NULL,
[aud_dataHora] [datetime] NOT NULL CONSTRAINT [DF_LOG_Auditoria_aud_dataHora] DEFAULT (getdate()),
[aud_entidade] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL,
[aud_operacao] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[aud_entidadeOriginal] [varchar] (max) COLLATE Latin1_General_CI_AS NULL,
[aud_entidadeNova] [varchar] (max) COLLATE Latin1_General_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Auditoria_UPDATE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Auditoria_UPDATE]
	@log_id UNIQUEIDENTIFIER
	, @aud_id INT
	, @aud_dataHora DATETIME
	, @aud_entidade VARCHAR (100)
	, @aud_operacao VARCHAR (50)
	, @aud_entidadeOriginal VARCHAR(MAX)
	, @aud_entidadeNova VARCHAR(MAX)
AS
BEGIN
	UPDATE LOG_Auditoria 
	SET 
		aud_dataHora = @aud_dataHora 
		, aud_entidade = @aud_entidade 
		, aud_operacao = @aud_operacao 
		, aud_entidadeOriginal = @aud_entidadeOriginal 
		, aud_entidadeNova = @aud_entidadeNova 
	WHERE 
		log_id = @log_id 
		AND aud_id = @aud_id 
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Auditoria_SELECT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Auditoria_SELECT]
	
AS
BEGIN
	SELECT 
		log_id
		,aud_id
		,aud_dataHora
		,aud_entidade
		,aud_operacao
		,aud_entidadeOriginal
		,aud_entidadeNova

	FROM 
		LOG_Auditoria WITH(NOLOCK) 
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Auditoria_LOAD]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Auditoria_LOAD]
	@log_id uniqueidentifier
	, @aud_id int
AS
BEGIN
	SELECT	Top 1
		 log_id  
		, aud_id 
		, aud_dataHora 
		, aud_entidade 
		, aud_operacao 
		, aud_entidadeOriginal 
		, aud_entidadeNova 
 	FROM
 		LOG_Auditoria
	WHERE 
		log_id = @log_id
		AND aud_id = @aud_id
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Auditoria_INSERT]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Auditoria_INSERT]
	@log_id UniqueIdentifier
	, @aud_dataHora DateTime
	, @aud_entidade VarChar (100)
	, @aud_operacao VarChar (50)
	, @aud_entidadeOriginal VARCHAR(MAX)
	, @aud_entidadeNova VARCHAR(MAX)
AS
BEGIN
	INSERT INTO 
		LOG_Auditoria
		( 
			log_id 
			, aud_dataHora 
			, aud_entidade 
			, aud_operacao 
			, aud_entidadeOriginal 
			, aud_entidadeNova 
		)
	VALUES
		( 
			@log_id 
			, @aud_dataHora 
			, @aud_entidade 
			, @aud_operacao 
			, @aud_entidadeOriginal 
			, @aud_entidadeNova 
		)
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[STP_LOG_Auditoria_DELETE]'
GO
CREATE PROCEDURE [dbo].[STP_LOG_Auditoria_DELETE]
	@log_id uniqueidentifier
	, @aud_id int
AS
BEGIN
	DELETE FROM 
		LOG_Auditoria 
	WHERE 
		log_id = @log_id
		AND aud_id = @aud_id
		
	DECLARE @ret INT
	SELECT @ret = ISNULL(@@ROWCOUNT,-1)
	RETURN @ret
	
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Sistema_Selectby_usu_id]'
GO
-- ========================================================================
-- Author:		João Victor Rossetti Vieira	
-- Create date: 10/08/2010 09:50
-- Description:	utilizado na busca de log de sistema, lista os dados do log
--              em ordem decrescente.
--				filtrados por:
--					id do usuario, sistema do gestão core.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_LOG_Sistema_Selectby_usu_id]
	@usu_id UNIQUEIDENTIFIER
	, @sis_id INT
AS
BEGIN
	SELECT 
		log_id
		, log_dataHora
		, log_ip
		, log_machineName
		, log_acao
		, log_descricao
		, sis_id
		, sis_nome
		, mod_id
		, mod_nome
		, usu_id
		, usu_login
		, gru_id
		, gru_nome
		, log_grupoUA
	FROM
		LOG_Sistema WITH(NOLOCK)
	WHERE
		usu_id = @usu_id
		AND (@sis_id IS NULL OR sis_id = @sis_id)
	ORDER BY
		log_datahora DESC
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Sistema_Selectby_Busca]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 13/05/2010 13:40
-- Description:	utilizado na busca de log de sistema, lista os dados do log
--              em ordem decrescente.
--				filtrados por:
--					data inicial e final, sistema do gestão core e tipo de
--					log.
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_LOG_Sistema_Selectby_Busca]
	@DataInicio DATETIME
	, @DataTermino DATETIME
	, @sis_id INT
	, @log_acao VARCHAR(50)
	, @usu_login VARCHAR(100)
AS
BEGIN
	SELECT 
		log_id
		, log_dataHora
		, log_ip
		, log_machineName
		, log_acao
		, log_descricao
		, sis_id
		, sis_nome
		, mod_id
		, mod_nome
		, usu_id
		, usu_login
		, gru_id
		, gru_nome
		, log_grupoUA
	FROM
		LOG_Sistema WITH(NOLOCK)
	WHERE
		log_datahora >= @DataInicio
		AND log_datahora < dateadd(day,1,@DataTermino)
		AND (@sis_id IS NULL OR sis_id = @sis_id)
		AND (@log_acao IS NULL OR log_acao = @log_acao)
		AND (@usu_login IS NULL OR usu_login LIKE  '%'+@usu_login+'%')
	ORDER BY
		log_datahora DESC
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Erros_SelectBY_Dia2]'
GO
CREATE PROCEDURE [dbo].[NEW_LOG_Erros_SelectBY_Dia2]
	@sis_id INT
	, @Data DATETIME
	, @usu_login VARCHAR(100)
AS
BEGIN
	SELECT
		err_id
		, err_dataHora
		, err_ip
		, err_machineName
		, err_browser
		, err_descricao
		, sis_id
		, sis_decricao
		, usu_id
		, usu_login
	FROM
		LOG_Erros WITH(NOLOCK)
	WHERE 
		(@Data IS NULL OR CAST(err_dataHora AS DATE) = @Data)--(@Data IS NULL OR err_dataHora >= @Data AND CAST(err_dataHora AS DATE) <= @Data)   --(err_dataHora,  err_dataHora < DATEADD(day, 1, @Data))		
		AND	((@sis_id IS NOT NULL AND sis_id = @sis_id) OR (@sis_id IS NULL AND sis_id is null)) -- AND	((@sis_id IS NOT NULL AND sis_id = @sis_id) OR (sis_id is null))
		AND ((@usu_login IS NULL) OR (usu_login LIKE '%'+ @usu_login + '%'))
	ORDER BY 
		err_dataHora DESC
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Erros_SelectBY_Dia]'
GO
CREATE PROCEDURE [dbo].[NEW_LOG_Erros_SelectBY_Dia]
	@sis_id INT
	, @Data DATETIME
	, @usu_login VARCHAR(100)
AS
BEGIN
	
	SET @usu_login = '%' + @usu_login + '%';
	
	IF (@sis_id IS NOT NULL AND @usu_login IS NOT NULL)
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
			AND	(sis_id = @sis_id)
			AND (usu_login LIKE @usu_login)
		ORDER BY 
			err_dataHora DESC
	END
	ELSE IF (@sis_id IS NULL AND @usu_login IS NOT NULL)
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
			AND	(sis_id IS NULL)
			AND (usu_login LIKE @usu_login)
		ORDER BY 
			err_dataHora DESC
	END
	ELSE IF (@sis_id IS NOT NULL)
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
			AND	(sis_id = @sis_id)
		ORDER BY 
			err_dataHora DESC
	END
	ELSE IF (@sis_id IS NULL)
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
			AND	(sis_id IS NULL)
		ORDER BY 
			err_dataHora DESC
	END
	ELSE IF (@usu_login IS NOT NULL)
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
			AND (usu_login LIKE @usu_login)
		ORDER BY 
			err_dataHora DESC
	END
	ELSE 
	BEGIN
		SELECT
			err_id
			, err_dataHora
			, err_ip
			, err_machineName
			, err_browser
			, SUBSTRING(err_descricao, 1, 100) AS err_descricao
			, sis_id
			, sis_decricao
			, usu_id
			, usu_login
		FROM
			LOG_Erros WITH(NOLOCK)
		WHERE 
			(CAST(err_dataHora AS DATE) = @Data)
		ORDER BY 
			err_dataHora DESC
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Erros_Selectby_Busca_TipoErros]'
GO
-- ==============================================================
-- Author:		Matheus Felipe Barbosa
-- Create date: 10/10/2011
-- Description:	Retorna:(Id do Erro, Data/hora, Descricao do erro,
--				Nome da Maquina, Ip, Navegador, Usuário
--				e Sistema) conforme quantidade de um deteminado
--				tipo de erro.
--				Filtrado: Data, Tipo Erro, Id Sistema.
-- ==============================================================
CREATE PROCEDURE [dbo].[NEW_LOG_Erros_Selectby_Busca_TipoErros] 
	
	@sis_id INT
	, @Data DATETIME
	, @err_tipoErro NVARCHAR(1000)
	 
AS
BEGIN
	SELECT 
		err_id
		, err_dataHora
		, SUBSTRING(err_descricao, 1, 100) AS  err_descricao
		, err_machineName
		, err_ip
		, err_browser
		, usu_login
		, sis_decricao
	
	FROM 
		LOG_Erros WITH(NOLOCK)
	
	WHERE
		((sis_id = @sis_id) OR (@sis_id IS NULL))
		AND CAST (err_dataHora AS DATE) = @Data    
		AND err_tipoErro = LTRIM(RTRIM(@err_tipoErro))  
	

END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Synonym_SYS_Sistema]'
GO
CREATE SYNONYM [dbo].[Synonym_SYS_Sistema] FOR [$CoreTarget$]..[SYS_Sistema]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Erros_Selectby_Busca_QtdErros]'
GO
-- ====================================================================
-- Author:		Matheus Felipe Barbosa	
-- Create date: 28/09/2011
-- Description:	Retorna a data, o tipo de erro, quantidade de vezes que
--				o erro ocorreu e sistema que erro ocorreu em um
--				deteminado intervalo de datas. Agrupado por data, tipo
--				de erro, ID sistema e descrição do sistema.
--				Filtrado: Data inicial e final
-- =====================================================================
CREATE PROCEDURE [dbo].[NEW_LOG_Erros_Selectby_Busca_QtdErros]
		@DataInicio DATETIME
		, @DataTermino DATETIME
AS
BEGIN
	SELECT
		CAST(err_dataHora AS DATE) AS Data
		, err_tipoErro
		, COUNT(err_tipoErro) AS Quantidade
		, sis_id
		, (SELECT CONVERT(VARCHAR,sis_id)+' - '+sis_nome FROM Synonym_SYS_Sistema Sistema WITH(NOLOCK) WHERE Sistema.sis_id = LOG_Erros.sis_id) AS sis_nome
				
	FROM 
		LOG_Erros WITH(NOLOCK)
	
	WHERE 
		((CAST (err_dataHora AS DATE) >= @DataInicio) AND (CAST(err_dataHora AS DATE) < DATEADD(day,1,@DataTermino)))
					 
 	GROUP BY
		CAST(err_dataHora AS DATE)
		, err_tipoErro
		, sis_id
		, sis_decricao
		
	ORDER BY 
		Data DESC 
		, sis_decricao
	
	SELECT @@ROWCOUNT;
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[NEW_LOG_Erros_SelectBy_Busca]'
GO
-- ========================================================================
-- Author:		Rafael Guilherme Amado
-- Create date: 07/05/2010 18:40
-- Description:	utilizado na busca de log de erros,
--              agrupa todos os erros por data e soma quanto ocorreram no
--				dia.
--				filtrados por:
--					data inicial e final, id do usuário do gestão core
-- ========================================================================
CREATE PROCEDURE [dbo].[NEW_LOG_Erros_SelectBy_Busca]
	@sis_id INT
	, @DataInicio DATETIME
	, @DataTermino DATETIME
	, @usu_login VARCHAR(100)
AS
BEGIN

	IF (@DataInicio IS NULL)
	BEGIN
		SET @DataInicio = '1000-01-01';
	END

	IF (@DataTermino IS NULL)
	BEGIN
		SET @DataTermino = '9999-12-31';
	END
	ELSE
	BEGIN
		SET @DataTermino = dateadd(day,1,@DataTermino);
	END
	
	SET @usu_login = '%' + @usu_login + '%';

	IF (@sis_id IS NOT NULL AND @usu_login IS NOT NULL)
	BEGIN
		SELECT
			LOG_Erros.sis_id
			, CAST(err_dataHora AS DATE) AS Data 
			, COUNT(*) AS num_logs
			, (SELECT CONVERT(VARCHAR,sis_id)+' - '+sis_nome FROM Synonym_SYS_Sistema Sistema WITH(NOLOCK) WHERE Sistema.sis_id = LOG_Erros.sis_id) AS sis_nome
		FROM 
			LOG_Erros WITH(NOLOCK)
		WHERE  
			(LOG_Erros.sis_id = @sis_id)
			AND (err_dataHora >= @DataInicio)
			AND (err_dataHora < @DataTermino)
			AND (usu_login LIKE @usu_login)
		GROUP BY
		LOG_Erros.sis_id
		,CAST(err_dataHora AS DATE)
		ORDER BY 
			Data DESC, sis_id
	END
	ELSE IF (@sis_id IS NOT NULL)
	BEGIN
		SELECT
			LOG_Erros.sis_id
			, CAST(err_dataHora AS DATE) AS Data 
			, COUNT(*) AS num_logs
			, (SELECT CONVERT(VARCHAR,sis_id)+' - '+sis_nome FROM Synonym_SYS_Sistema Sistema WITH(NOLOCK) WHERE Sistema.sis_id = LOG_Erros.sis_id) AS sis_nome
		FROM 
			LOG_Erros WITH(NOLOCK)
		WHERE  
			(LOG_Erros.sis_id = @sis_id)
			AND (err_dataHora >= @DataInicio)
			AND (err_dataHora < @DataTermino)
		GROUP BY
		LOG_Erros.sis_id
		,CAST(err_dataHora AS DATE)
		ORDER BY 
			Data DESC, sis_id
	END
	ELSE IF (@usu_login IS NOT NULL)
	BEGIN
		SELECT
			LOG_Erros.sis_id
			, CAST(err_dataHora AS DATE) AS Data 
			, COUNT(*) AS num_logs
			, (SELECT CONVERT(VARCHAR,sis_id)+' - '+sis_nome FROM Synonym_SYS_Sistema Sistema WITH(NOLOCK) WHERE Sistema.sis_id = LOG_Erros.sis_id) AS sis_nome
		FROM 
			LOG_Erros WITH(NOLOCK)
		WHERE  
			(err_dataHora >= @DataInicio)
			AND (err_dataHora < @DataTermino)
			AND (usu_login LIKE @usu_login)
		GROUP BY
		LOG_Erros.sis_id
		,CAST(err_dataHora AS DATE)
		ORDER BY 
			Data DESC, sis_id
	END
	ELSE
	BEGIN
		SELECT
			LOG_Erros.sis_id
			, CAST(err_dataHora AS DATE) AS Data 
			, COUNT(*) AS num_logs
			, (SELECT CONVERT(VARCHAR,sis_id)+' - '+sis_nome FROM Synonym_SYS_Sistema Sistema WITH(NOLOCK) WHERE Sistema.sis_id = LOG_Erros.sis_id) AS sis_nome
		FROM 
			LOG_Erros WITH(NOLOCK)
		WHERE  
			(err_dataHora >= @DataInicio)
			AND (err_dataHora < @DataTermino)
		GROUP BY
		LOG_Erros.sis_id
		,CAST(err_dataHora AS DATE)
		ORDER BY 
			Data DESC, sis_id
	END
END
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Serilog]'
GO
CREATE TABLE [dbo].[Serilog]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Message] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[MessageTemplate] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[Level] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL,
[TimeStamp] [datetimeoffset] NOT NULL,
[Exception] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL,
[Properties] [xml] NULL,
[LogEvent] [nvarchar] (max) COLLATE Latin1_General_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Logs] on [dbo].[Serilog]'
GO
ALTER TABLE [dbo].[Serilog] ADD CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
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
