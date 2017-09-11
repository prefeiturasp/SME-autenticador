UPDATE SYS_Parametro
SET par_valor = ''
WHERE par_chave = 'ID_GOOGLE_ANALYTICS'

UPDATE SYS_Sistema
SET sis_caminho = '', sis_caminhoLogout = '', sis_situacao = 3

UPDATE CFG_Configuracao
SET cfg_valor = ''
WHERE cfg_chave = 'AppIPMaquinaAgentesFIM'
