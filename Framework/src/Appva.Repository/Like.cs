namespace Appva.Repository
{

    #region Imports.

    using System;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class Like<TEntity> where TEntity : class
    {

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<TEntity, object>> Property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

    }

}
