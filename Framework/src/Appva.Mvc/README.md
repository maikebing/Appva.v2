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