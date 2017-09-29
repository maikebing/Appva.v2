// <copyright file="UploadFileHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Log.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadFileHandler : RequestHandler<Parameterless<UploadFileModel>, UploadFileModel>
    {
        #region Fields.

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileHandler"/> class.
        /// </summary>
        public UploadFileHandler()
        {

        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UploadFileModel Handle(Parameterless<UploadFileModel> message)
        {
            return new UploadFileModel();
        }

        #endregion
    }
}