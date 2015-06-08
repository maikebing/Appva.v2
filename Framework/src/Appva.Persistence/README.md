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
var persistenceContextFactory = ConfigurableApplicationContext
    .Read<SinglePersistenceConfiguration>()
	.From("App_Data\\Persistence.json")
	.ToObject()
	.Build();
builder.Register(x => persistenceContextFactory).As<IPersistenceContextFactory>().SingleInstance();
builder.Register(x => x.Resolve<IPersistenceContextFactory>().Build()).As<IPersistenceContext>().InstancePerRequest();
var container = builder.Build();
DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
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
    //// Only commit in non-child actions, model state is valid and no exception was raised.
    var state = ((filterContext.Exception.IsNull() && ! filterContext.ExceptionHandled)
                && ! filterContext.IsChildAction && filterContext.Controller.ViewData.ModelState.IsValid);
    this.persistenceContext.Commit(state);
}
```