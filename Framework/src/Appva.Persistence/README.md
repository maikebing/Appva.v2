# Appva.Persistence

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
var configuration = ConfigurableApplicationContext.Read<DefaultDatasourceConfiguration>()
	.From("App_Data\\Persistence.json")
	.AsMachineNameSpecific()
	.ToObject();
builder.Register(x => configuration).As<IDefaultDatasourceConfiguration>().SingleInstance();
builder.RegisterType<DefaultDatasource>().As<IDefaultDatasource>().SingleInstance();
builder.RegisterType<DefaultPersistenceExceptionHandler>().As<IPersistenceExceptionHandler>().SingleInstance();
builder.RegisterType<DefaultPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance().AutoActivate();
builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerRequest();
```

#### Usage In ActionFilterAttribute
```c#
/// <inheritdoc />
public override void OnActionExecuting(ActionExecutingContext filterContext)
{
    //// The <see cref="IPersistenceContext"/> is injected in constructor or field.
    this.persistenceContext.Open().BeginTransaction(IsolationLevel.ReadCommitted);
}

/// <inheritdoc />
public override void OnActionExecuted(ActionExecutedContext filterContext)
{
    //// Commit in non-child actions, model state is valid and no exception was raised.
	if (filterContext.Exception.IsNotNull())
	{
		return;
	}
	if (filterContext.IsChildAction)
	{
		return;
	}
	if (filterContext.Controller.ViewData.ModelState.IsValid)
	{
		return;
	}
    this.persistenceContext.Commit(true);
}
```