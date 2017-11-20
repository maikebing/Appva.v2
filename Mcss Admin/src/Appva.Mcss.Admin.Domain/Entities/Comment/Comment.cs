// <copyright file="Comment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Comment : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="createdBy">The comment author.</param>
        /// <param name="content">The comment text content.</param>
        public Comment(Account createdBy, string content)
            : this(null, createdBy, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="parent">The parent comment.</param>
        /// <param name="createdBy">The comment author.</param>
        /// <param name="content">The comment text content.</param>
        public Comment(Comment parent, Account createdBy, string content)
        {
            Requires.NotNull(createdBy, "createdBy");
            Requires.NotNullOrWhiteSpace(content, "content");
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Parent    = parent;
            this.CreatedBy = createdBy;
            this.Content   = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Comment()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The comment text content.
        /// </summary>
        public virtual string Content
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// If the comment is signed.
        /// </summary>
        public virtual Signature Signature
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The author of the comment.
        /// </summary>
        public virtual Account CreatedBy
        {    
            get;
            internal protected set;
        }

        /// <summary>
        /// Whether or not if the comment is a comment on a comment.
        /// </summary>
        public virtual Comment Parent
        {
            get;
            internal protected set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="createdBy">The comment author.</param>
        /// <param name="content">The comment text content.</param>
        /// <returns>A new <see cref="Comment"/> instance.</returns>
        public static Comment New(Account createdBy, string content)
        {
            return new Comment(createdBy, content);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="parent">The parent comment.</param>
        /// <param name="createdBy">The comment author.</param>
        /// <param name="content">The comment text content.</param>
        /// <returns>A new <see cref="Comment"/> instance.</returns>
        public static Comment New(Comment parent, Account createdBy, string content)
        {
            return new Comment(parent, createdBy, content);
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Signs the comment.
        /// </summary>
        /// <param name="signature">The signature.</param>
        public virtual void Sign(Signature signature)
        {
            Requires.NotNull(signature, "signature");
            Requires.ValidState(this.Signature == null, "There is already a signature on this comment.");
            this.Signature = signature;
        }

        #endregion
    }
}