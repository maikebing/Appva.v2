// <copyright file="PermissionManager.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using Appva.Core.Contracts.Permissions;
    using Appva.Html.Security;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PermissionManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static volatile PermissionManager instance;
        private static readonly object syncLock = new object();
        private readonly IList<PermissionControllerDescriptor> controllers;

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionManager"/> class.
        /// </summary>
        private PermissionManager()
        {
            this.controllers = new List<PermissionControllerDescriptor>();
        }

        #endregion

        public static PermissionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new PermissionManager();
                        }
                    }
                }
                return instance;
            }
        }

        public Dictionary<string, IEnumerable<IPermission>> RoutePermissions = new Dictionary<string, IEnumerable<IPermission>>();

        public void Build(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var controller in this.Controllers(assembly))
                {
                    var reflected = new PermissionControllerDescriptor(controller);
                    
                    reflected.Actions = ActionInfo.GenerateFromController(controller, reflected).ToList();
                    this.controllers.Add(reflected);
                    foreach (var action in reflected.Actions)
                    {
                        if (!RoutePermissions.ContainsKey(action.CacheKey()))
                        {
                            RoutePermissions.Add(action.CacheKey(), action.Permissions);
                        }
                        else
                        {
                            var h = action.CacheKey();
                        }
                    }
                }
            }
            var xstring = "";
            var x = xstring + "";
        }

        private IEnumerable<Type> Controllers(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof(Controller).IsAssignableFrom(x) && x.IsAbstract == false);
        }
    }

    public sealed class ActionInfo
    {
        public ActionInfo()
        {
            this.Methods     = new List<HttpVerbs>();
            this.Permissions = new List<IPermission>();
            this.Roles       = new List<string>();
        }
        public List<HttpVerbs> Methods { get; private set; }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        public bool AllowAnonymous { get; private set; }
        public List<string> Roles { get; private set; }
        public List<IPermission> Permissions { get; private set; }
        public PermissionControllerDescriptor Controller { get; private set; }
        public static IEnumerable<ActionInfo> GenerateFromController(Type controller, PermissionControllerDescriptor control)
        {
            var actions = new List<ActionInfo>();
            var methods = controller
                    .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .Where(x => x.IsDefined(typeof(NonActionAttribute)) == false);
            foreach (var method in methods)
            {
                var action        = new ActionInfo();
                action.Controller = control;
                action.Name       = method.Name;
                action.ReturnType = method.ReturnType.Name;

                //// Actions c
                method.GetAttribute<AuthorizeAttribute>      (x => { action.Roles.AddRange(x.Roles.Split(new [] {','}, StringSplitOptions.RemoveEmptyEntries)); });
                method.GetAttribute<PermissionsAttribute>    (x => { action.Permissions.Add(Permission.New(x.GetValue())); });
                method.GetAttribute<AllowAnonymousAttribute> (x => { action.AllowAnonymous = true; });

                //// 
                method.GetAttribute<AcceptVerbsAttribute> (x => { action.Methods.AddRange(x.Verbs.Select(y => (HttpVerbs) Enum.Parse(typeof(HttpVerbs), y, true))); });
                method.GetAttribute<HttpDeleteAttribute>  (x => { action.Methods.Add(HttpVerbs.Delete /*.ToString()*/); });
                method.GetAttribute<HttpGetAttribute>     (x => { action.Methods.Add(HttpVerbs.Get    /*.ToString()*/); });
                method.GetAttribute<HttpHeadAttribute>    (x => { action.Methods.Add(HttpVerbs.Head   /*.ToString()*/); });
                method.GetAttribute<HttpOptionsAttribute> (x => { action.Methods.Add(HttpVerbs.Options/*.ToString()*/); });
                method.GetAttribute<HttpPatchAttribute>   (x => { action.Methods.Add(HttpVerbs.Patch  /*.ToString()*/); });
                method.GetAttribute<HttpPostAttribute>    (x => { action.Methods.Add(HttpVerbs.Post   /*.ToString()*/); });
                method.GetAttribute<HttpPutAttribute>     (x => { action.Methods.Add(HttpVerbs.Put    /*.ToString()*/); });
                actions.Add(action);
            }
            return actions;
        }

        public string CacheKey()
        {
            var area       = this.Controller.ControllerAreaName;
            var controller = this.Controller.ControllerName;
            var action     = this.Name;
            var method     = string.Join(".", this.Methods);
            var inner      = string.Join(".", action ?? "*", controller ?? "*", area ?? "*", string.Join(".", this.Methods)).ToLowerInvariant().Replace("controller", "");
            return inner;
        }


    }
    public static class AttributeHelper
    {
        public static void GetAttribute<T>(this MemberInfo element, Action<T> action, bool inherit = true) where T : Attribute
        {
            var attributes = element.GetCustomAttributes<T>(inherit);
            if (attributes.Any())
            {
                action(attributes.First());;
            }
        }
    }

    // controllerinfo?
    // Encapsulates information that describes a controller, such as its name, type, and actions.
    public sealed class PermissionControllerDescriptor
    {
        #region Properties.
        public Type ControllerType
        {
            get;
            set;
        }
        public string ControllerName
        {
            get;
            set;
        }
        public string ControllerAreaName
        {
            get;
            set;
        }
        public string ControllerAreaPrefix
        {
            get;
            set;
        }
        public string ControllerRoutePrefix
        {
            get;
            set;
        }
        public List<string> ControllerPermissions
        {
            get;
            set;
        }
        public bool AllowAnonymous
        {
            get;
            set;
        }
        public List<ActionInfo> Actions
        {
            get;
            set;
        } 
        #endregion

        #region Constructors.
        public PermissionControllerDescriptor(Type controllerType)
        {
            this.ControllerType        = controllerType;
            this.Actions               = new List<ActionInfo>();
            this.ControllerPermissions = new List<string>();
            this.ControllerName        = controllerType.Name;
            this.LoadAttributes();
        }
        #endregion

        private void LoadAttributes()
        {
            //// All controllers are injected with <see cref="AuthorizeAttribute" /> by default so no need to check that attribute.
            this.ControllerType.GetAttribute<AllowAnonymousAttribute>(x => { this.AllowAnonymous = true; });
            this.ControllerType.GetAttribute<RouteAreaAttribute>     (x => { this.ControllerAreaName = x.AreaName; this.ControllerAreaPrefix = x.AreaPrefix; });
            this.ControllerType.GetAttribute<RoutePrefixAttribute>   (x => { this.ControllerRoutePrefix = x.Prefix; });
            this.ControllerType.GetAttribute<PermissionsAttribute>   (x => { this.ControllerPermissions.Add(x.GetValue()); });
        }
    }

    public static class PermissionAttributeExtensions
    {
        public static string GetValue(this PermissionsAttribute attribute)
        {
            var value = typeof(PermissionsAttribute).GetField("permission", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(attribute) as string;
            return value;
        }
    }
}