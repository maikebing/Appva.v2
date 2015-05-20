// <copyright file="CshtmlTemplateManager.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using System;
    using System.IO;
    using Appva.Core.IO;
    using RazorEngine.Templating;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CshtmlTemplateManager : ITemplateManager
    {
        #region Variables.

        /// <summary>
        /// The template file suffix.
        /// </summary>
        private const string Suffix = ".cshtml";

        /// <summary>
        /// The base template path.
        /// </summary>
        private readonly string path;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CshtmlTemplateManager"/> class.
        /// </summary>
        /// <param name="path">The template path, e.g. 'Shared/EmailTemplates'</param>
        public CshtmlTemplateManager(string path)
        {
            this.path = PathResolver.ResolveAppRelativePath(path);
        }

        #endregion

        #region ITemplateManager Members

        /// <inheritdoc />
        public void AddDynamic(ITemplateKey key, ITemplateSource source)
        {
            throw new NotImplementedException("Dynamic templates are not supported!");
        }

        /// <inheritdoc />
        public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
        {
            return new NameOnlyTemplateKey(name, resolveType, context);
        }

        /// <inheritdoc />
        public ITemplateSource Resolve(ITemplateKey key)
        {
            var path = Path.Combine(this.path, string.Concat(key.Name, Suffix));
            var content = File.ReadAllText(path);
            return new LoadedTemplateSource(content, path);
        }

        #endregion
    }
}