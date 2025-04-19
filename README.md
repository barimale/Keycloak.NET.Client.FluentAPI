# Keycloak.NET.Client.FluentAPI, Keycloak.NET.Desktop.Client.FluentAPI 
## Keycloak 6.x
1. Context builder examples:

```
	var publicContext = Context.Create()
		.Credentials(InputData.Username, InputData.Password)
		.Url(InputData.Endpoint)
		.Realm(InputData.Realm)
		.OpenIdConnect()
		.Public(InputData.ClientId);

	var confidentialContext = Context.Create()
		.Credentials(InputData.Username, InputData.Password)
		.Url(InputData.Endpoint)
		.Realm(InputData.Realm)
		.OpenIdConnect()
		.Confidential(InputData.ClientId, InputData.ClientSecret);

	var bearerOnlyContext = Context.Create()
		.Credentials(InputData.Username, InputData.Password)
		.Url(InputData.Endpoint)
		.Realm(InputData.Realm)
		.OpenIdConnect()
		.BearerOnly(InputData.BearerOnlyClientId, InputData.BearerOnlyClientSecret);
```

2. Manager usage examples: 
```
	//given manager
	var service = new AuthorizationManager();

	//and specific context
	var publicContext = ...

	//or
	var confidentialContext = ...

	//or
	var bearerOnlyContext = ...

	//when
	var isAuthorized = await service
		.Authorize(context);

	//then
	ClassicAssert.IsTrue(isAuthorized);
	ClassicAssert.NotNull(service.Token);
```
