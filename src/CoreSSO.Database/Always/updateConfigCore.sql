UPDATE SYS_Parametro
SET par_valor = '$UrlMain$'
WHERE par_chave = 'URL_ADMINISTRATIVO'

UPDATE SYS_Sistema
SET sis_caminho = '$UrlCoreLogin$', sis_caminhoLogout = 'UrlCoreLogout'
WHERE sis_id = 1

UPDATE CFG_Configuracao
SET cfg_valor = '$HostService$'
WHERE cfg_chave = 'AppSchedulerHost'