﻿// <copyright file="BristolObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Domain.Unit;
    using Validation;

    #endregion

    public class BristolObservation : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public BristolObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base (patient, name, description, null, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolObservation"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        internal protected BristolObservation()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the type of the scale.
        /// </summary>
        /// <value>
        /// The type of the scale.
        /// </value>
        public virtual Type ScaleType
        {
            get
            {
                //// HACK:       MUST FIX!!!
                //// FIXME:      PRIORITY TO FIX!!!!
                //// UNRESOLVED: PRIORITY TO FIX!!!! 
                const string NameSpacePrefix = "Appva.Mcss.Domain.Unit.";
                var typeName = this.Scale.Replace("\"", "");
                if (typeName.StartsWith(NameSpacePrefix))
                {
                    return Type.GetType(typeName);
                }
                typeName = NameSpacePrefix + typeName + "Unit";
                return Type.GetType(typeName);
            }
            set
            {
                this.Scale = value.FullName;
            }
        }

        #endregion

        #region Members.

        /// <summary>
        /// The Scale name.
        /// </summary>
        /// <returns>
        /// The unit name.
        /// </returns>
        public virtual string ScaleName()
        {
            return (Activator.CreateInstance(this.ScaleType, true) as IUnit).UnitName;
        }

        #endregion
    }
}
