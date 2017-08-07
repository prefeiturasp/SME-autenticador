DECLARE
	@clientId INT

SELECT @clientId = id FROM IDS_Clients WHERE ClientId = 'smespmvc'

IF(NOT EXISTS(SELECT * FROM IDS_ClientCorsOrigins AS icco WHERE icco.ClientId = @clientId AND icco.Origin = '$UrlMain$'))
BEGIN
	INSERT INTO IDS_ClientCorsOrigins (ClientId, Origin) VALUES (@clientId, '$UrlMain$')
END

IF(NOT EXISTS(SELECT * FROM IDS_ClientRedirectUris AS icru WHERE icru.ClientId = @clientId AND icru.RedirectUri = '$UrlCoreLogin$'))
BEGIN
	INSERT INTO IDS_ClientRedirectUris (ClientId, RedirectUri) VALUES (@clientId, '$UrlCoreLogin$')
END

IF(NOT EXISTS(SELECT * FROM IDS_ClientPostLogoutRedirectUris AS icplru WHERE icplru.ClientId = @clientId AND icplru.PostLogoutRedirectUri = '$UrlCoreLogin$'))
BEGIN
	INSERT INTO IDS_ClientPostLogoutRedirectUris (ClientId, PostLogoutRedirectUri) VALUES (@clientId, '$UrlCoreLogin$')
END