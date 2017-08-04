
UPDATE SYS_Parametro
SET par_valor = '$UrlMain$'
WHERE par_chave = 'URL_ADMINISTRATIVO'

UPDATE SYS_Parametro
SET par_valor = ''
WHERE par_chave = 'ID_GOOGLE_ANALYTICS'

UPDATE SYS_Sistema
SET sis_caminho = '', sis_caminhoLogout = ''

UPDATE SYS_Sistema
SET sis_caminho = '$UrlLogin$', sis_caminhoLogout = '$UrlLogout$'
WHERE sis_id = 1

UPDATE CFG_Configuracao
SET cfg_valor = '$HostService$'
WHERE cfg_chave = 'AppSchedulerHost'

UPDATE CFG_Configuracao
SET cfg_valor = ''
WHERE cfg_chave = 'AppIPMaquinaAgentesFIM'
