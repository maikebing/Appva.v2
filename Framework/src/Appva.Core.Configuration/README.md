# Appva.Core.Configuration
## Examples
```javascript
ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").ToObject();
ConfigurableApplicationContext.Write(new { Foo = "bar" }).To("config.json").Execute();
```