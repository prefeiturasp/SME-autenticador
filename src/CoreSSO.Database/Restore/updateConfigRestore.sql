UPDATE SYS_Parametro
SET par_valor = ''
WHERE par_chave = 'ID_GOOGLE_ANALYTICS'

UPDATE SYS_Sistema
SET sis_caminho = '', sis_caminhoLogout = ''

UPDATE CFG_Configuracao
SET cfg_valor = ''
WHERE cfg_chave = 'AppIPMaquinaAgentesFIM'
