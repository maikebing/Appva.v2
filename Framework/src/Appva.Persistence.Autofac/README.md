# Appva.Persistence.Autofac

Enables application requests, i.e. a IPersistenceContext per request and not per action - in other words; no need for action filters for session management.

#### Using Configuration File
```javascript
{
  "ConnectionString": "Server=localhost;Database=GoldFinger;Trusted_Connection=False;User ID=JamesBond;Password=007",
  "Properties": { "generate_statistics" : "false" },
  "Assembly": "Some.Assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
}
```

#### Wiring it up with AutoFac
```c#
var builder = new ContainerBuilder();
var persistenceContextFactory = ConfigurableApplicationContext
    .Read<SinglePersistenceConfiguration>()
	.From("App_Data\\Persistence.json")
	.ToObject()
	.Build();
builder.RegisterModule(new PersistenceModule(persistenceContextFactory));
var container = builder.Build();
DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
```