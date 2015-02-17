Appva Framework
========

#### Todos
1. Cryptography.Certificate - cache certificates when read
2. Cryptography.Messaging - create SecureEmailMessage which inherits from EmailMessage
3. Persistence.AutoFac - Move TrackablePersistenceContext and create better Utility classes for Request checks.
4. Core.Configuration - Enable fluid storage e.g. ConfigurableApplicationContext.From().Store or .MakePermanent()
5. Apis.TenantServer - Reconnect
6. Persistence.MultiTenant - Resolvers - Add extenders etc to e.g. Appva.Identity
7. Persistence.MultiTenant - Resolvers - Reconnects
8. Cqrs - rename Bus?
9. Mvc - Add razor read e.g. CshtmlResource.Read("x.cshtml", new { Foo = "bar" }) - thus SecureEmailMessage.createNew("subject", CshtmlResource.Read()); 
10. Logging - Add overrides for Log.Debug("string"); Log.Debug(new Exception()); Log.Debug("string", new Exception()); Log.Debug("format {0}", new { Foo = "bar"});