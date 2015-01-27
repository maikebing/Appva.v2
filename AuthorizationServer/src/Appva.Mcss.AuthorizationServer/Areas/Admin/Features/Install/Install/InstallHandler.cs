// <copyright file="InstallHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Authentication;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    internal sealed class InstallGetHandler : PersistentRequestHandler<NoParameter<Install>, Install>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InstallGetHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Install Handle(NoParameter<Install> message)
        {
            var setting = this.Persistence.QueryOver<Setting>()
                .Where(x => x.Key == "system.installed")
                .SingleOrDefault();
            return new Install()
            {
                IsInstalled = setting.IsNotNull()
            };
        }

        #endregion
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InstallHandler : PersistentRequestHandler<Install, Install>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InstallHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Install Handle(Install message)
        {
            var setting = this.Persistence.QueryOver<Setting>()
                .Where(x => x.Key == "system.installed")
                .SingleOrDefault();
            if (setting.IsNotNull())
            {
                return new Install();
            }
            try
            {
                var tenants = this.CreateTenants();
                var scopes = this.CreateScopes();
                var grants = this.CreateAuthorizationGrants();
                this.CreateClients(tenants, scopes, grants);
                this.CreateResources(scopes);
                var permissions = this.CreatePermissions();
                var roles = this.CreateRoles(permissions);
                this.CreateUsers(roles, tenants);
                this.CreateMenu(permissions);
                this.CreateSettings();
            }
            catch (Exception e)
            {
                return new Install()
                {
                    IsException = true,
                    ExceptionMessage = e.StackTrace
                };
            }
            return new Install();
        }

        private IList<Tenant> CreateTenants()
        {
            var tenant = new Tenant("test-tenant-unique-id", "test-tenant", "Test Tenant", "Just a test tenant", null, "Server=localhost;Database=AuthServer;Trusted_Connection=False;User ID=McssTest;Password=foo1!BAR");
            tenant.Activate();
            this.Persistence.Save(tenant);
            return new List<Tenant>() { tenant };
        }

        private IDictionary<string, Scope> CreateScopes()
        {
            var scopes = new Dictionary<string, Scope>()
            { 
                { "ResourceServerRead",  new Scope("https://appvaapis.se/auth/resource.readonly", "Resource Server (Read-Only)", "Access to resource server in read-only mode") },
                { "ResourceServerWrite", new Scope("https://appvaapis.se/auth/resource", "Resource Server", "Access to resource server") },
                { "ResourceServerAdmin", new Scope("https://appvaapis.se/auth/resource.adminonly", "Resource Server (Admin-Only)", "Access to resource server in admin-only mode") },
            };
            foreach (var scope in scopes)
            {
                this.Persistence.Save(scope.Value);
            }
            return scopes;
        }

        private IDictionary<string, AuthorizationGrant> CreateAuthorizationGrants()
        {
            var grants = new Dictionary<string, AuthorizationGrant>() { 
                { "AuthorizationCode", new AuthorizationGrant("AuthorizationCode", "The authorization code is obtained by using an authorization server as an intermediary between the client and resource owner.  Instead of requesting authorization directly from the resource owner, the client directs the resource owner to an authorization server (via its user-agent as defined in [RFC2616]), which in turn directs the resource owner back to the client with the authorization code.") }, 
                { "Implicit", new AuthorizationGrant("Implicit", "The implicit grant is a simplified authorization code flow optimized for clients implemented in a browser using a scripting language such as JavaScript.  In the implicit flow, instead of issuing the client an authorization code, the client is issued an access token directly (as the result of the resource owner authorization). The grant type is implicit, as no intermediate credentials (such as an authorization code) are issued (and later used to obtain an access token).") },
                { "ResourceOwnerPasswordCredentials", new AuthorizationGrant("ResourceOwnerPasswordCredentials", "The resource owner password credentials (i.e., username and password) can be used directly as an authorization grant to obtain an access token. The credentials should only be used when there is a high degree of trust between the resource owner and the client (e.g., the client is part of the device operating system or a highly privileged application), and when other authorization grant types are not available (such as an authorization code).") },
                { "ClientCredentials", new AuthorizationGrant("ClientCredentials", "The client credentials (or other forms of client authentication) can be used as an authorization grant when the authorization scope is limited to the protected resources under the control of the client, or to protected resources previously arranged with the authorization server.  Client credentials are used as an authorization grant typically when the client is acting on its own behalf (the client is also the resource owner) or is requesting access to protected resources based on an authorization previously arranged with the authorization server.") }
            };
            foreach (var kv in grants)
            {
                this.Persistence.Save(kv.Value);
            }
            return grants;
        }

        private void CreateClients(IList<Tenant> tenants, IDictionary<string, Scope> scopes, IDictionary<string, AuthorizationGrant> grants)
        {
            this.Persistence.Save(new Client("Device Administrator Client", "A client connected to all tenants, for administration of devices only", null, "mEfsXi07yd9gITT8c-6HcQ2", "7JHCSOQ6_7rbqd7Zqjpg4Q2", null, 60, 0, null, ClientType.Public, new List<AuthorizationGrant> { grants["ResourceOwnerPasswordCredentials"] }, tenants, new List<Scope> { scopes["ResourceServerRead"], scopes["ResourceServerAdmin"] }).Activate());
            this.Persistence.Save(new Client("Test Client", "Just a test client", null, "WI7_v6yVJEUSIAl9wBZSRg2", "W63wKmplL7DXF12SLq-USw2", null, 0, 0, null, ClientType.Public, new List<AuthorizationGrant> { grants["ClientCredentials"], grants["ResourceOwnerPasswordCredentials"] }, tenants, new List<Scope> { scopes["ResourceServerRead"], scopes["ResourceServerWrite"] }).Activate());
        }

        private void CreateResources(IDictionary<string, Scope> scopes)
        {
            var keys = new List<Resource> 
            { 
                new Resource("Resource Server", "The mobile device resource server API", 60, "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjVFOEpmc2FScnNZdVpKVHNhTlJIcENsb3NvR0N1OFZtOCtuN0w5YlpGYzkyb3pYS1N0UFRtVWdCT0RUSlYzNzFDMU9hKzBBbyswdHMrQi9JYnR1RUdTeVNKWVh5U2FiKzdHYzRGNDUrSDJKVUFlOXpJcmJmVVFKdVprT25WRk1ISUJMY2RWaFJDNUd0L2JwWVdXeHk4RHcvSEV3WTdHb0Y1RHE0dDFabE9oYz08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+", new List<Scope> { scopes["ResourceServerRead"], scopes["ResourceServerAdmin"], scopes["ResourceServerWrite"] }),
            };
            foreach(var key in keys)
            {
                key.Activate();
                this.Persistence.Save(key);
            }
        }

        private Dictionary<string, Permission> CreatePermissions()
        {
            var context = new PermissionContext("oauth_server", "OAuth Server", "The OAuth Server context");
            this.Persistence.Save(context);
            var permissions = new Dictionary<string, Permission>();
            var resources   = new [] { "user", "userauth", "tenant", "menu", "menulink", "role", "permission", "client" };
            var policies    = new [] { PermissionAction.Read, PermissionAction.Create, PermissionAction.Update, PermissionAction.Delete };
            var mappings    = new Dictionary<PermissionAction, string> { { policies[0], "view" }, { policies[1], "create" }, { policies[2], "update" }, { policies[3], "delete" } };
            foreach (var resource in resources)
            {
                foreach (var policy in policies)
                {
                    var name = string.Format("{0} {1}s", mappings[policy].FirstToUpper(), resource.FirstToUpper());
                    var description = string.Format("Enables the subject to {0} {1}s.", mappings[policy], resource);
                    var permission = new Permission(name, description, resource, policy, context);
                    permissions.Add(permission.Name.Replace(" ", string.Empty), permission);
                    this.Persistence.Save(permission);
                }
            }
            return permissions;
        }

        private Dictionary<string, Role> CreateRoles(Dictionary<string, Permission> permissions)
        {
            var roles = new Dictionary<string, Role>();
            var role = new Role("admin_god", "Admin God", "May do as he pleases", permissions.Select(x => x.Value).ToList());
            roles.Add(role.Key, role);
            this.Persistence.Save(role);
            return roles;
        }

        private void CreateUsers(Dictionary<string, Role> roles, IList<Tenant> tenants)
        {
            var uam = new UserAuthenticationMethod("oauth", "OAuth", "OAuth Server Authentication", "", "");
            this.Persistence.Save(uam);
            var role = roles["admin_god"];
            var user1 = new User("19810602-4915", new FullName("Johan", "Säll Larsson"), new Contact("johansalllarsson@appva.se", "+66876808202"), null, new List<Role>() { role }, tenants);
            user1.Activate();
            this.Persistence.Save(user1);
            this.Persistence.Save(new UserAuthentication(user1, new PersonalIdentityNumberPasswordAuthentication() { Password = new Credentials("abc123ABC").Password, ForcePasswordChange = true, LastLoginAt = DateTime.Now }, uam));
            var user2 = new User("19881225-4855", new FullName("Richard", "Sachade Henriksson"), new Contact("richard.henriksson@appva.se", "+46763363962"), null, new List<Role>() { role }, tenants);
            user2.Activate();
            this.Persistence.Save(user2);
            this.Persistence.Save(new UserAuthentication(user2, new PersonalIdentityNumberPasswordAuthentication() { Password = new Credentials("abc123ABC").Password, ForcePasswordChange = true, LastLoginAt = DateTime.Now }, uam));
            var user3 = new User("19010101-1111", new FullName("Kalle", "Anka"), new Contact("noreply@appva.se", null), null, null, tenants);
            user3.Activate();
            this.Persistence.Save(user3);
            this.Persistence.Save(new UserAuthentication(user3, new PersonalIdentityNumberPasswordAuthentication() { Password = new Credentials("password").Password, ForcePasswordChange = true, LastLoginAt = DateTime.Now }, uam));
        }

        private void CreateMenu(Dictionary<string, Permission> permissions)
        {
            var menu = new Menu("oauth_main_navigation", "Sidebar navigation", "The main side bar navigation menu.");
            this.Persistence.Save(menu);
            //// Dashboard
            this.Persistence.Save(new MenuLink("Dashboard", "/admin", 0, "fa-dashboard", menu));
            //// Clients
            var client = new MenuLink("Applications", "/admin/applications", 1, "fa-cog", menu, null, permissions["ViewClients"]);
            this.Persistence.Save(client);
            this.Persistence.Save(new MenuLink("Register application", "/admin/application/register", 0, null, menu, client, permissions["CreateClients"]));
            //// Tenants
            var tenant = new MenuLink("Tenants", "/admin/tenants/", 2, "fa-cloud-upload", menu, null, permissions["ViewTenants"]);
            this.Persistence.Save(tenant);
            this.Persistence.Save(new MenuLink("Register tenant", "/admin/tenant/register", 0, null, menu, tenant, permissions["CreateTenants"]));
            //// Users
            var user = new MenuLink("Users", "/admin/users/", 3, "fa-user", menu, null, permissions["ViewUsers"]);
            this.Persistence.Save(user);
            this.Persistence.Save(new MenuLink("New user", "/admin/user/new", 0, null, menu, user, permissions["CreateUsers"]));
            //// Roles
            var role = new MenuLink("Roles", "/admin/roles/", 4, "fa-tags", menu, null, permissions["ViewRoles"]);
            this.Persistence.Save(role);
            this.Persistence.Save(new MenuLink("New role", "/admin/role/new", 0, null, menu, role, permissions["CreateRoles"]));
            //// Permissions 
            var permission = new MenuLink("Permissions", "/admin/permissions", 5, "fa-shield", menu, null, permissions["ViewPermissions"]);
            this.Persistence.Save(permission);
            this.Persistence.Save(new MenuLink("New permission", "/admin/permission/new", 0, null, menu, permission, permissions["CreatePermissions"]));
            //// User Authentications
            var userauth = new MenuLink("Authentication methods", "/admin/user/authentications", 6, "fa-lock", menu, null, permissions["ViewUserauths"]);
            this.Persistence.Save(userauth);
            this.Persistence.Save(new MenuLink("New authentication methods", "/admin/user/authentications/new", 0, null, menu, userauth, permissions["CreateUserauths"]));
        }

        private void CreateSettings()
        {
            var settings = new Dictionary<string, object>()
            { 
                { "system.authentication.2fa", false }, 
                { "system.users.perpage", 10 },
                { "system.installed", true }
            };
            foreach (var kv in settings)
            {
                this.Persistence.Save(new Setting(kv.Key, kv.Value));
            }
        }

        #endregion
    }
}