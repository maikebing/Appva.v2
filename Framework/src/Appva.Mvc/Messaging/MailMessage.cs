// <copyright file="MailMessage.cs" company="Appva AB">
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
    using Mail = System.Net.Mail;

    #endregion

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly 1. To, 2a. Protect or 2b. Execute. 

    #region Constraints.

    /// <summary>
    /// Constraint which allows the Path() method.
    /// </summary>
    public interface IRazorMailMessagePath
    {
        /// <summary>
        /// Sets the template (cshtml) name.
        /// </summary>
        /// <param name="templateName">The template name</param>
        /// <returns><see cref="IRazorMailMessageModel"/></returns>
        IRazorMailMessageModel Template(string templateName);
    }

    /// <summary>
    /// Constraint which allows the Model() method.
    /// </summary>
    public interface IRazorMailMessageModel
    {
        /// <summary>
        /// Sets the template model to be parsed.
        /// </summary>
        /// <typeparam name="T">The template model type</typeparam>
        /// <param name="templateModel">The template model</param>
        /// <returns><see cref="IRazorMailMessageTo"/></returns>
        IRazorMailMessageTo Model<T>(T templateModel);
    }

    /// <summary>
    /// Constraint which allows the To() method.
    /// </summary>
    public interface IRazorMailMessageTo
    {
        /// <summary>
        /// Sets the recipients.
        /// </summary>
        /// <param name="recipients">The collection of recipients</param>
        /// <returns><see cref="IRazorMailMessageSubject"/></returns>
        IRazorMailMessageSubject To(IList<string> recipients);

        /// <summary>
        /// Sets the recipients.
        /// </summary>
        /// <param name="recipients">The one or more recipients</param>
        /// <returns><see cref="IRazorMailMessageSubject"/></returns>
        IRazorMailMessageSubject To(params string[] recipients);
    }

    /// <summary>
    /// Constraint which allows the Subject() method.
    /// </summary>
    public interface IRazorMailMessageSubject
    {
        /// <summary>
        /// Sets the E-mail subject.
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <returns><see cref="IRazorMailMessageBuild"/></returns>
        IRazorMailMessageBuild Subject(string subject);
    }

    /// <summary>
    /// Constraint which allows the Build() method.
    /// </summary>
    public interface IRazorMailMessageBuild
    {
        /// <summary>
        /// Builds the E-mail message.
        /// </summary>
        /// <returns><see cref="RazorEmailMessage"/></returns>
        MailMessage Build();
    }

    #endregion

    /// <summary>
    /// A razor engine E-mail message.
    /// </summary>
    public sealed class MailMessage : 
        Mail.MailMessage,
        IRazorMailMessagePath,
        IRazorMailMessageModel,
        IRazorMailMessageTo,
        IRazorMailMessageSubject,
        IRazorMailMessageBuild
    {
        #region Constructor.

        /// <summary>
        /// Prevents a default instance of the <see cref="MailMessage" /> class from 
        /// being created.
        /// </summary>
        private MailMessage()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The template name.
        /// </summary>
        public string TemplateName
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

        /// <summary>
        /// The template model type.
        /// </summary>
        public Type TemplateModelType
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
        public static IRazorMailMessagePath CreateNew()
        {
            return new MailMessage();
        }

        #endregion

        #region IRazorMailMessagePath Members.

        /// <inheritdoc />
        IRazorMailMessageModel IRazorMailMessagePath.Template(string templateName)
        {
            this.TemplateName = templateName;
            return this;
        }

        #endregion

        #region IRazorMailMessageModel Members.

        /// <inheritdoc />
        IRazorMailMessageTo IRazorMailMessageModel.Model<T>(T templateModel)
        {
            this.TemplateModel = templateModel;
            this.TemplateModelType = typeof(T);
            return this;
        }

        #endregion

        #region IRazorMailMessageTo Members.

        /// <inheritdoc />
        IRazorMailMessageSubject IRazorMailMessageTo.To(IList<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                this.To.Add(recipient);
            }
            return this;
        }

        /// <inheritdoc />
        IRazorMailMessageSubject IRazorMailMessageTo.To(params string[] recipients)
        {
            foreach (var recipient in recipients)
            {
                this.To.Add(recipient);
            }
            return this;
        }

        #endregion

        #region IRazorMailMessageSubject Members.

        /// <inheritdoc />
        IRazorMailMessageBuild IRazorMailMessageSubject.Subject(string subject)
        {
            this.Subject = subject;
            return this;
        }

        #endregion

        #region IRazorMailMessageBuild Members.

        /// <inheritdoc />
        MailMessage IRazorMailMessageBuild.Build()
        {
            return this;
        }

        #endregion
    }
}