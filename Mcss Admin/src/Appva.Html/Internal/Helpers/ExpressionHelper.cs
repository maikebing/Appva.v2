// <copyright file="ExpressionHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure.Internal
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    #region Imports.

    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ExpressionHelpers
    {
        public static RouteValueDictionary GetRouteValuesFromExpression<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            MethodCallExpression call = action.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Expression must be a method call.", "action");
            }

            string controllerName = typeof(TController).Name;
            if (!controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Controller name must end in 'Controller'.", "action");
            }
            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            if (controllerName.Length == 0)
            {
                throw new ArgumentException("Cannot route to class named 'Controller'.", "action");
            }

            var actionName = GetTargetActionName(call.Method);

            var rvd = new RouteValueDictionary();
            rvd.Add("Controller", controllerName);
            rvd.Add("Action", actionName);
            RouteAreaAttribute areaAttr = typeof(TController).GetCustomAttributes(typeof(RouteAreaAttribute), true /* inherit */).FirstOrDefault() as RouteAreaAttribute;
            if (areaAttr != null)
            {
                rvd.Add("Area", areaAttr.AreaName);
            }

            AddParameterValuesFromExpressionToDictionary(rvd, call);
            return rvd;
        }

        public static string GetInputName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Call)
            {
                var methodCallExpression = (MethodCallExpression)expression.Body;
                string name = GetInputName(methodCallExpression);
                return name.Substring(expression.Parameters[0].Name.Length + 1);
            }
            return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }

        private static string GetInputName(MethodCallExpression expression)
        {
            var methodCallExpression = expression.Object as MethodCallExpression;
            if (methodCallExpression != null)
            {
                return GetInputName(methodCallExpression);
            }
            return expression.Object.ToString();
        }

        // This method contains some heuristics that will help determine the correct action name from a given MethodInfo
        // assuming the default sync / async invokers are in use. The logic's not foolproof, but it should be good enough
        // for most uses.
        private static string GetTargetActionName(MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;

            // do we know this not to be an action?
            if (methodInfo.IsDefined(typeof(NonActionAttribute), true /* inherit */))
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                                                                  "The method '{0}' is marked [NonAction] and cannot be called directly.", methodName));
            }

            // has this been renamed?
            ActionNameAttribute nameAttr = methodInfo.GetCustomAttributes(typeof(ActionNameAttribute), true).OfType<ActionNameAttribute>().FirstOrDefault();
            if (nameAttr != null)
            {
                return nameAttr.Name;
            }

            // targeting an async action?
            if (methodInfo.DeclaringType.IsSubclassOf(typeof(AsyncController)))
            {
                if (methodName.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
                {
                    return methodName.Substring(0, methodName.Length - "Async".Length);
                }
                if (methodName.EndsWith("Completed", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "The method '{0}' is an asynchronous completion method and cannot be called directly.", methodName));
                }
            }
            return methodName;
        }

        private static void AddParameterValuesFromExpressionToDictionary(RouteValueDictionary rvd, MethodCallExpression call)
        {
            var parameters = call.Method.GetParameters();
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression arg = call.Arguments[i];
                    object value = null;
                    ConstantExpression ce = arg as ConstantExpression;
                    
                    if (ce != null)
                    {
                        // If argument is a constant expression, just get the value
                        value = ce.Value;
                        rvd.Add(parameters[i].Name, value);
                        continue;
                    }
                    var ne = arg as NewExpression;
                    if (ne != null)
                    {
                        continue;
                    }
                    var mce = arg as MethodCallExpression;
                    if (mce != null)
                    {
                        continue;
                    }
                    Expression<Func<object>> le = Expression.Lambda<Func<object>>(arg);
                    Func<object> compiledExpression = le.Compile();
                    object result = compiledExpression();
                    rvd.Add(parameters[i].Name, result);
                    //value = CachedExpressionCompiler.Evaluate(arg);
                    //rvd.Add(parameters[i].Name, value);
                }
            }
        }
    }
}