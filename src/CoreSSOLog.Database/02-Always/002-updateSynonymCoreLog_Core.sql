-- =============================================
-- Description:	Atualiza os sin�nimos de determinado banco de dados
--		Par�metros: 
--			@database : Obrigat�rio
--				Nome do banco de dados que ter� os sin�nimos atualizados (passar nome exato)
--			@anterior : Obrigat�rio
--				Nome do banco de dados anterior que o sin�nimo referencia.  (passar nome exato)
--			@novo : Obrigat�rio
--				Nome do banco de dados novo que o sin�nimo ir� referenciar.  (passar nome exato)
-- =============================================

DECLARE
	@database NVARCHAR(128) = '$SystemDatabase$'
	, @anterior NVARCHAR(128) = '$CoreSource$'
	, @novo NVARCHAR(128) = '$CoreTarget$'

	SET NOCOUNT ON;

	DECLARE @exec VARCHAR(MAX) = ''

	CREATE TABLE #tabelaSinonimosBD
	(
		ID INT IDENTITY(1,1),
		name VARCHAR(MAX) NOT NULL,
		base_object_name VARCHAR(MAX) NOT NULL
	)
	--Seleciona os sin�nimos que devem ser atualizados
	EXEC (
		'INSERT INTO #tabelaSinonimosBD (name, base_object_name)
		SELECT name, base_object_name FROM ' + @database + '.sys.synonyms
		WHERE base_object_name LIKE ''[[]' + @anterior + ']%'''
	)

	DECLARE @id INT = (SELECT MAX(ID) FROM #tabelaSinonimosBD)
	WHILE (@id > 0)
	BEGIN
	
		SELECT @exec = 	
			'DROP SYNONYM ' + name + ';' + 
			'CREATE SYNONYM ' + name + ' FOR ' + REPLACE(base_object_name, '[' + @anterior + ']', '[' + @novo + ']') + ';'
		FROM
			#tabelaSinonimosBD
		WHERE
			ID = @id	
		
		--Atualiza o sin�nimo
		EXEC (@exec)
	
		SET @id = @id - 1
	END
	
	--Mostra a lista de sin�nimos atualizados
	EXEC ('
		DECLARE @printSinonimos VARCHAR(MAX) = ''''

		SELECT @printSinonimos += name COLLATE database_default + '' -> '' + base_object_name COLLATE database_default + CHAR(13)
		FROM ' + @database + '.sys.synonyms
		WHERE name COLLATE database_default IN (SELECT name COLLATE database_default FROM #tabelaSinonimosBD)
		ORDER BY name COLLATE database_default
		
		IF (@printSinonimos = '''')
			PRINT ''Nenhum sin�nimo foi atualizado''
		ELSE
			PRINT ''Sin�nimos atualizados: '' + CHAR(13) + CHAR(13) + @printSinonimos
	')

	DROP TABLE #tabelaSinonimosBD	