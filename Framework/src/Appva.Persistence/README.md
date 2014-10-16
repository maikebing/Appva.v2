# Appva.Persistence

### AutoFac
```javascript
var persistenceContextFactory = ConfigurableApplicationContext
    .Read<SinglePersistenceConfiguration>()
	.From("App_Data\\Persistence.json")
	.ToObject()
	.Build();
builder.Register(x => persistenceContextFactory).As<IPersistenceContextFactory>().SingleInstance();
builder.Register(x => x.Resolve<IPersistenceContextFactory>().Build()).As<IPersistenceContext>().InstancePerRequest();
```

### Action Attribute Filter
```javascript
/// <inheritdoc />
public override void OnActionExecuting(ActionExecutingContext filterContext)
{
    //// The <see cref="IPersistenceContext"/> is injected in constructor or field.
    this.persistenceContext.Open().BeginTransaction(IsolationLevel.ReadCommitted);
}

/// <inheritdoc />
public override void OnActionExecuted(ActionExecutedContext filterContext)
{
    var state = ((filterContext.Exception.IsNull() && ! filterContext.ExceptionHandled)
                && ! filterContext.IsChildAction && filterContext.Controller.ViewData.ModelState.IsValid);
    this.persistenceContext..Commit(state);
}
```