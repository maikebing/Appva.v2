﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appva.Core.Debug {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Appva.Core.Debug.Messages", typeof(Messages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;{0}&gt; Initialized with &lt;base uri address&gt;: {1}.
        /// </summary>
        internal static string ClassInitialization {
            get {
                return ResourceManager.GetString("ClassInitialization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;HTTP {0}&gt; Executing: {1}.
        /// </summary>
        internal static string HttpRequestMessage {
            get {
                return ResourceManager.GetString("HttpRequestMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: {1}\n.
        /// </summary>
        internal static string HttpResponseHeaderKeyPair {
            get {
                return ResourceManager.GetString("HttpResponseHeaderKeyPair", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;HTTP {0}&gt; Response: {1}.
        /// </summary>
        internal static string HttpResponseMessage {
            get {
                return ResourceManager.GetString("HttpResponseMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;HTTP {0}&gt; Status: {1} {2}.
        /// </summary>
        internal static string HttpResponseStatusMessage {
            get {
                return ResourceManager.GetString("HttpResponseStatusMessage", resourceCulture);
            }
        }
    }
}
