# Appva.Core.Configuration

## Synchronous
### Read Configuration
```c#
ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").ToObject();
```
### Write Configuration
```c#
ConfigurableApplicationContext.Write(new { Foo = "bar" }).To("config.json").Execute();
```
## Asynchronous
### Read Configuration Async
```c#
await ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").ToObjectAsync();
```
### Write Configuration Async
```c#
await ConfigurableApplicationContext.Write(new { Foo = "bar" }).To("config.json").ExecuteAsync();
```
## Protect
### Read Protected Configuration
```c#
ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").Unprotect().ToObject();
await ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").Unprotect().ToObjectAsync();
```
### Write Protected Configuration
```c#
ConfigurableApplicationContext.Write(new { Foo = "bar" }).To("config.json").Protect().Execute();
await ConfigurableApplicationContext.Write(new { Foo = "bar" }).To("config.json").Protect().ExecuteAsync();
```
## Store Configuration
### Read Protected Configuration
```c#
var config = ConfigurableApplicationContext.Read<ConfigurableResource>().From("config.json").Unprotect().ToObjectAsync();
ConfigurableApplicationContext.Add(config);
ConfigurableApplicationContext.Get<ConfigurableResource>().Equals(config);
```