// <copyright file="AdministeredItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Validation;

    #endregion

    public class AdministeredItem : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministeredItem"/> class.
        /// </summary>
        /// <param name="administration">The administration.</param>
        /// <param name="task">The task.</param>
        /// <param name="value">The value.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="comment">The comment.</param>
        protected internal AdministeredItem(Administration administration, Task task, ArbitraryValue value, Signature signature, Comment comment = null)
        {
            Requires.NotNull(administration, "administration");
            Requires.NotNull(task, "task");
            Requires.NotNull(value, "value");
            Requires.NotNull(signature, "signature");
            this.Administration = administration;
            this.Task = task;
            this.Value = value;
            this.Signature = signature;
            this.Comment = comment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministeredItem"/> class.
        /// </summary>
        protected internal AdministeredItem()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the administration.
        /// </summary>
        /// <value>The administration.</value>
        public virtual Administration Administration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>The task.</value>
        public virtual Task Task
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arbitrary value.
        /// </summary>
        /// <value>The arbitrary value.</value>
        public virtual ArbitraryValue Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>The signature.</value>
        public virtual Signature Signature
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public virtual Comment Comment
        {
            get;
            set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// News the specified administration.
        /// </summary>
        /// <param name="administration">The <see cref="Administration"/>.</param>
        /// <param name="task">The task.</param>
        /// <param name="value">The value.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>AdministeredItem.</returns>
        public static AdministeredItem New(Administration administration, Task task, ArbitraryValue value, Signature signature, Comment comment = null)
        {
            return new AdministeredItem(administration, task, value, signature, comment);
        }

        #endregion
    }
}
