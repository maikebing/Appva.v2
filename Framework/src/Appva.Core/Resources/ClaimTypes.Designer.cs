﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appva.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ClaimTypes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ClaimTypes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Appva.Core.Resources.ClaimTypes", typeof(ClaimTypes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/acl/claims/enabled.
        /// </summary>
        public static string AclEnabled {
            get {
                return ResourceManager.GetString("AclEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/acl/claims/preview.
        /// </summary>
        public static string AclPreview {
            get {
                return ResourceManager.GetString("AclPreview", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/identity/claims/permission.
        /// </summary>
        public static string Permission {
            get {
                return ResourceManager.GetString("Permission", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/identity/claims/permission/schedulesetting.
        /// </summary>
        public static string SchedulePermission {
            get {
                return ResourceManager.GetString("SchedulePermission", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/security/claims/siths.
        /// </summary>
        public static string SithsEnabled {
            get {
                return ResourceManager.GetString("SithsEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/organization/taxon.
        /// </summary>
        public static string Taxon {
            get {
                return ResourceManager.GetString("Taxon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/security/claims/tenant/hostname.
        /// </summary>
        public static string TenantHostName {
            get {
                return ResourceManager.GetString("TenantHostName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/security/claims/tenant/id.
        /// </summary>
        public static string TenantId {
            get {
                return ResourceManager.GetString("TenantId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://schemas.appva.se/2015/04/security/claims/tenant/name.
        /// </summary>
        public static string TenantName {
            get {
                return ResourceManager.GetString("TenantName", resourceCulture);
            }
        }
    }
}
