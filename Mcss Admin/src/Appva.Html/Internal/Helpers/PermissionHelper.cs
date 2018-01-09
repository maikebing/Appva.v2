// <copyright file="PermissionHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure.Internal
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Security.Claims;
    using System.Web.Mvc;
    using Appva.Core.Contracts.Permissions;
    using Appva.Html.Security;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class PermissionHelper
    {
        public static bool IsOperationAllowed<T>(this HtmlHelper htmlHelper, Expression<Action<T>> expression)
        {
            return true;
        }

        /// <summary>
        /// Returns the <see cref="IPermission"/> of any controller method decorated
        /// with the <see cref="PermissionsAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="TController">The controller type.</typeparam>
        /// <param name="expression">The action.</param>
        /// <returns>Returns an <see cref="IPermission"/> or null if no permissions are needed for the action.</returns>
        public static IPermission Permissions<T>(this HtmlHelper htmlHelper, Expression<Action<T>> expression)
            where T : Controller
        {
            
            var method = ((MethodCallExpression) expression.Body).Method;
            
            var key = typeof(T).FullName + method.Name;
            /// cache key here and check if we have this.
            var attribute = method.GetCustomAttribute<PermissionsAttribute>(true);
            if (attribute == null)
            {
                return null;
            }
            var field = typeof(PermissionsAttribute).GetField("permission", BindingFlags.NonPublic | BindingFlags.Instance);
            var value = field.GetValue(attribute) as string;
            return Permission.New(value);
        }

        /// <summary>
        /// Returns whether or not the user account ahs the proper permissions.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="permission">The permission to check</param>
        /// <returns>True if the user has the proper permissions; otherwise false</returns>
        public static bool HasPermissionFor(this HtmlHelper htmlHelper, IPermission permission)
        {
            var principal = htmlHelper.ViewContext.HttpContext.User as ClaimsPrincipal;
            if (principal == null || principal.Identity == null || ! principal.Identity.IsAuthenticated)
            {
                return false;
            }
            return principal.HasClaim(permission.Key, permission.Value);
        }
    }
}