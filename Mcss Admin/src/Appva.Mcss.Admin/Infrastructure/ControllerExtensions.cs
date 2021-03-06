﻿// <copyright file="ControllerExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Mvc;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class ControllerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DispatchJsonResult JsonPost(this Controller controller)
        {
            return new DispatchJsonResult(JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DispatchJsonResult JsonPost(this Controller controller, object response)
        {
            controller.ViewData.Model = response;
            return controller.JsonPost();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DispatchJsonResult JsonGet(this Controller controller, object response)
        {
            controller.ViewData.Model = JsonConvert.SerializeObject(response);
            return controller.JsonGet();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DispatchJsonResult JsonGet(this Controller controller)
        {
            return new DispatchJsonResult(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DispatchExcelFileContentResult ExcelFile(this Controller controller)
        {
            return new DispatchExcelFileContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static PdfFileResult PdfFile(this Controller controller)
        {
            return new PdfFileResult();
        }
    }
}