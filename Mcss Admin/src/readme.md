# Local configuration

## Setup

Add a AppSettings.Local.config in directory `src/Appva.Mcss.Admin/`.
Add custom configurations e.g.

```xml
<appSettings>
    <add key="TenantWcfClient:ConnectionString" value="Server=localhost;Database=...;Trusted_Connection=False;User ID=...;Password=..."/>
	<add key="..." value="..." />
	...
</appSettings>
```