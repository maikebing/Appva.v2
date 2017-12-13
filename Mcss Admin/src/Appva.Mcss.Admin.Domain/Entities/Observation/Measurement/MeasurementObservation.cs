//// <copyright file="MeasurementObservation.cs" company="Appva AB">
////     Copyright (c) Appva AB. All rights reserved.
//// </copyright>
//// <author>
////     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
//// </author>
//namespace Appva.Mcss.Admin.Domain.Entities
//{
//    #region Imports.

//    using System;
//    using Appva.Mcss.Domain.Unit;
//    using Validation;

//    #endregion

//    /// <summary>
//    /// An implementation of Observation for Measurement Values
//    /// </summary>
//    /// <seealso cref="Appva.Mcss.Admin.Domain.Entities.Observation" />
//    public class MeasurementObservation : Observation
//    {
//        #region Constructors.

//        public MeasurementObservation(Patient patient, string name, string description, object scale, Taxon delegation = null)
//            :base(patient, name, description, scale, delegation)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="MeasurementObservation"/> class.
//        /// </summary>
//        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
//        internal protected MeasurementObservation()
//        {
//        }

//        #endregion

//        #region Properties.

//        /// <summary>
//        /// Gets or sets the type of the scale.
//        /// </summary>
//        /// <value>
//        /// The type of the scale.
//        /// </value>
//        public virtual Type ScaleType
//        {
//            get
//            {
//                //// HACK:       MUST FIX!!!
//                //// FIXME:      PRIORITY TO FIX!!!!
//                //// UNRESOLVED: PRIORITY TO FIX!!!! 
//                const string NameSpacePrefix = "Appva.Mcss.Domain.Unit.";
//                var typeName = this.Scale.Replace("\"", "");
//                if (typeName.StartsWith(NameSpacePrefix))
//                {
//                    return Type.GetType(typeName);
//                }
//                if (typeName.ToLower() == "common") { typeName = "Feces"; }
//                if (typeName.ToLower() == "tena_identifi") { typeName = "TenaIdentifi"; }
//                typeName = NameSpacePrefix + typeName + "Unit";
//                return Type.GetType(typeName);
//            }
//            set
//            {
//                this.Scale = value.FullName;
//            }
//        }

//        #endregion

//        #region Members.

//        /// <summary>
//        /// The Scale name.
//        /// </summary>
//        /// <returns>
//        /// The unit name.
//        /// </returns>
//        public virtual string ScaleName()
//        {
//            var scaleType = this.ScaleType;
//            var obj = Activator.CreateInstance(ScaleType, true) as IUnit;
//            return obj.UnitName;
//        }

//        #endregion
//    }
//}
