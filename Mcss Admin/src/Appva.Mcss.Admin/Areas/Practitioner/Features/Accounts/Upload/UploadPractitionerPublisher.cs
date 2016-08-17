// <copyright file="UploadPractitionerPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Admin.Models;
    using Application.Services;
    using Cqrs;
    using Domain.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadPractitionerPublisher : RequestHandler<UploadPractitionerModel, ListAccount>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService service;

        #endregion

        #region Constructor.

        /// <summary>
        ///  Initializes a new instance of the <see cref="UploadPractitionerPublisher"/> class.
        /// </summary>
        public UploadPractitionerPublisher(IFileService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListAccount Handle(UploadPractitionerModel message)
        {
            byte[] fileData = null;

            using (var binaryReader = new BinaryReader(message.File.InputStream))
            {
                fileData = binaryReader.ReadBytes(message.File.ContentLength);
            }
            
            
            var xls = XLS.CreateNew(message.File.FileName, fileData, message.Description);
            this.service.SaveXLS(xls);

            return new ListAccount();
        }

        #endregion
    }
}