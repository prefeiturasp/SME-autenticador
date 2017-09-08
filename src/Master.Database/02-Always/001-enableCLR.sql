DECLARE 
	@configure TABLE(name VARCHAR(MAX),	minimum INT, maximum INT, config_value INT,	run_value INT)

INSERT INTO @configure EXEC sp_configure 'clr enabled';
INSERT INTO @configure EXEC sp_configure 'show advanced options';

IF((SELECT TOP 1 config_value FROM @configure WHERE name = 'clr enabled') = 0)
BEGIN
	EXEC sp_configure 'clr enabled', 1;  
END

IF((SELECT TOP 1 config_value FROM @configure WHERE name = 'show advanced options') = 0)
BEGIN
	EXEC sp_configure 'show advanced options', 1;  
END

RECONFIGURE;