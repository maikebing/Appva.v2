// <copyright file="RazorEmailMessage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using RazorEngine;
    using RazorEngine.Templating;

    #endregion

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly 1. To, 2a. Protect or 2b. Execute. 

    #region Constraints.

    /// <summary>
    /// Constraint which allows the Path() method.
    /// </summary>
    public interface IRazorEmailMessagePath
    {
        /// <summary>
        /// Sets the template (cshtml) path.
        /// </summary>
        /// <param name="templatePath">The template path</param>
        /// <returns><see cref="IRazorEmailMessageModel"/></returns>
        IRazorEmailMessageModel Path(string templatePath);
    }

    /// <summary>
    /// Constraint which allows the Model() method.
    /// </summary>
    public interface IRazorEmailMessageModel
    {
        /// <summary>
        /// Sets the template model to be parsed.
        /// </summary>
        /// <param name="templateModel">The template model</param>
        /// <returns><see cref="IRazorEmailMessageTo"/></returns>
        IRazorEmailMessageTo Model(object templateModel);
    }

    /// <summary>
    /// Constraint which allows the To() method.
    /// </summary>
    public interface IRazorEmailMessageTo
    {
        /// <summary>
        /// Sets the recipients.
        /// </summary>
        /// <param name="recipients">The collection of recipients</param>
        /// <returns><see cref="IRazorEmailMessageSubject"/></returns>
        IRazorEmailMessageSubject To(IList<string> recipients);

        /// <summary>
        /// Sets the recipients.
        /// </summary>
        /// <param name="recipients">The one or more recipients</param>
        /// <returns><see cref="IRazorEmailMessageSubject"/></returns>
        IRazorEmailMessageSubject To(params string[] recipients);
    }

    /// <summary>
    /// Constraint which allows the Subject() method.
    /// </summary>
    public interface IRazorEmailMessageSubject
    {
        /// <summary>
        /// Sets the E-mail subject.
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <returns><see cref="IRazorEmailMessageBuild"/></returns>
        IRazorEmailMessageBuild Subject(string subject);
    }

    /// <summary>
    /// Constraint which allows the Build() method.
    /// </summary>
    public interface IRazorEmailMessageBuild
    {
        /// <summary>
        /// Builds the E-mail message.
        /// </summary>
        /// <returns><see cref="RazorEmailMessage"/></returns>
        RazorEmailMessage Build();
    }

    #endregion

    /// <summary>
    /// A razor engine E-mail message.
    /// </summary>
    public sealed class RazorEmailMessage : MailMessage,
        IRazorEmailMessagePath,
        IRazorEmailMessageModel,
        IRazorEmailMessageTo,
        IRazorEmailMessageSubject,
        IRazorEmailMessageBuild
    {
        #region Constructor.

        /// <summary>
        /// Prevents a default instance of the <see cref="RazorEmailMessage" /> class from 
        /// being created.
        /// </summary>
        private RazorEmailMessage()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The template path.
        /// </summary>
        public string TemplatePath
        {
            get;
            private set;
        }

        /// <summary>
        /// The template model.
        /// </summary>
        public object TemplateModel
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="RazorEmailMessage"/> class.
        /// </summary>
        /// <returns>A razor E-mail constraint</returns>
        public static IRazorEmailMessagePath CreateNew()
        {
            return new RazorEmailMessage();
        }

        #endregion

        #region IRazorEmailMessagePath Members.

        /// <inheritdoc />
        public IRazorEmailMessageModel Path(string templatePath)
        {
            this.TemplatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);
            return this;
        }

        #endregion

        #region IRazorEmailMessageModel Members.

        /// <inheritdoc />
        public IRazorEmailMessageTo Model(object templateModel)
        {
            this.TemplateModel = templateModel;
            return this;
        }

        #endregion

        #region IRazorEmailMessageTo Members.

        /// <inheritdoc />
        public new IRazorEmailMessageSubject To(IList<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                base.To.Add(recipient);
            }
            return this;
        }

        /// <inheritdoc />
        IRazorEmailMessageSubject IRazorEmailMessageTo.To(params string[] recipients)
        {
            foreach (var recipient in recipients)
            {
                base.To.Add(recipient);
            }
            return this;
        }

        #endregion

        #region IRazorEmailMessageSubject Members.

        /// <inheritdoc />
        public new IRazorEmailMessageBuild Subject(string subject)
        {
            base.Subject = subject;
            return this;
        }

        #endregion

        #region IRazorEmailMessageBuild Members.

        /// <inheritdoc />
        public RazorEmailMessage Build()
        {
            //// https://antaris.github.io/RazorEngine/Upgrading.html
            //// Engine.Razor.IsTemplateCached()
            //// Inspiration (old): http://mehdi.me/generating-html-emails-with-razorengine-basics-generating-your-first-email/
            //// TODO: Create RazorEmailService and cache!
            this.Body = Engine.Razor.RunCompile(File.ReadAllText(this.TemplatePath), "test", null, this.TemplateModel);
            this.IsBodyHtml = true;
            return this;
        }

        #endregion
    }
}