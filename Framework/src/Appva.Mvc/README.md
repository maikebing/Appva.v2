# Appva.Mvc
### Controller
### Validation and Hydration Attributes

* HydrateAttribute
* ValidateAttribute

```c#
/// <summary>
/// If validation fails on POST then the <see cref="HydrateAttribute"/>
/// will re-populate the model state.
/// </summary>
/// <returns><see cref="ActionResult"/></returns>
[HttpGet, Hydrate, Route("model/create")]
public ActionResult Create()
{
    return View();
}

/// <summary>
/// If validation fails on POST, then the <see cref="ValidateAttribute"/>
/// will automatically copy the modal state and redirect back to the GET
/// method.
/// </summary>
/// <param name="model">The form model</param>
/// <returns><see cref="ActionResult"/></returns>
[HttpPost, Validate, ValidateAntiForgeryToken, Route("model/create")]
public ActionResult Create(CreateModel model)
{
    return View();
}
```
### Alert Attributes

* AlertSuccessAttribute
* AlertWarningAttribute
* AlertInformationAttribute
* AlertDangerAttribute

```c#
/// <summary>
/// If validation is successful and no exceptions are raised, then the 
/// <see cref="AlertSuccessAttribute"/> message will be stored in the 
/// flash session (TempData).
/// </summary>
/// <param name="model">The form model</param>
/// <returns><see cref="ActionResult"/></returns>
[HttpPost, Validate, ValidateAntiForgeryToken, Route("model/create"), AlertSuccess("Model created successfully!")]
public ActionResult Create(CreateModel model)
{
    return View();
}

//// In the create.cshtml view call
@Html.Alert()
```

### Header Attributes

* HeaderAttribute

```c#
/// <summary>
/// Adds a X-RateLimit header to the request.
/// </summary>
/// <param name="id">The model id</param>
/// <returns><see cref="ActionResult"/></returns>
[HttpGet, Header("X-RateLimit", "5000"), Route("model/details")]
public ActionResult Details(Guid id)
{
    return View();
}
```

### RazorViewEngine
#### Feature View Location 
Feature Folders in ASP.NET MVC which will use a new folder structure:
`Features/{Controller}/{Action}/{Action}.cshtml`
`Features/User/UserController.cs`
`Features/User/Details/Details.cshtml`
`Features/User/Details/DetailsUser.cs`
`Features/User/Details/DetailsUserId.cs`

```c#
ViewEngines.Engines.Clear();
ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
```

### Imaging
#### Wiring it up with AutoFac
```c#
var builder = new ContainerBuilder();
var imageConfiguration = ConfigurableApplicationContext
    .Read<ImageConfiguration>()
	.From("App_Data\\Imaging.json")
	.ToObject()
	.Build();
builder.Register(x => new ImageProcessor(imageConfiguration)).As<IImageProcessor>().SingleInstance();
var container = builder.Build();
DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
```

#### Return Images in Controller
```c#
/// <summary>
/// Returns the image if the user is authenticated and the image
/// exists on disk.
/// </summary>
/// <returns>A <see cref="FileResult"/> if found, otherwise null</returns>
[HttpGet, Route("image/{fileName}/{mimeType}")]
public FileResult Resolve(string fileName, string mimeType)
{
    //// Inject the <see cref="IImageProcessor"/> in contructor or field.
    return (User.Identity.IsAuthenticated) ? this.imageProcessor.Read(fileName, mimeType) : null;
}
```