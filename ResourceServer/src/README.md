# Appva.Mcss.ResourceServer

#### Using Machine Name Configuration File
##### Application.config
```javascript
{
  "IsProduction": "false",
  "IsSslRequired": "false",
  "SkipTokenAndScopeAuthorization": "true",
  "TenantServerUri": "NotUsedWhenSkipMultiTenantIsTrue",
  "ResourceServerSigningKey": "NotUsedInSkipTokenAndScopeAuthorizationIsTrue",
  "AuthorizationServerSigningKey": "NotUsedInSkipTokenAndScopeAuthorizationIsTrue"
}
```
##### Persistence.config
```javascript
{
  "SkipMultiTenant": "true",
  "ConnectionString": "Server=localhost;Database=GoldFinger;Trusted_Connection=False;User ID=JamesBond;Password=007",
  "Assembly": "Appva.Mcss.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
  "Properties": {
    "cache.use_query_cache": "false",
    "cache.use_second_level_cache" : "false",
    "generate_statistics" : "true"
  }
}
```
#### Things to remember
```c#
this.User.Identity.Id();
```
Currently, the `IIdentity` extension method `Id()` is dependent on `AuthorizeAttraibute.cs` and will trigger an exception when 
used in `"SkipTokenAndScopeAuthorization": "true"`. 