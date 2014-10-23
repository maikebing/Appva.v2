// <copyright file="ControllerExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System.Web.Mvc;
    using Models;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal static class ControllerExtensions
    {
        /// <summary>
        /// Initialize the wizard with a model.
        /// </summary>
        /// <typeparam name="T">The wizard type</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="model">The model</param>
        /// <returns>A wizard</returns>
        public static Wizard<T> Wizard<T>(this Controller controller, T model)
            where T : class
        {
            return new Wizard<T>(model, controller.Session);
        }

        /// <summary>
        /// Returns the wizard.
        /// </summary>
        /// <typeparam name="T">The wizard type</typeparam>
        /// <param name="controller">The controller</param>
        /// <returns>A wizard</returns>
        public static Wizard<T> Wizard<T>(this Controller controller)
            where T : class
        {
            return new Wizard<T>(controller.Session);
        }
    }
}