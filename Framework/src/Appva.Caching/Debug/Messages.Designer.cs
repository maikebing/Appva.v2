﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appva.Caching.Debug {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Appva.Caching.Debug.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to &lt;CacheItem&gt; Initialized with &lt;key&gt;: {0} and &lt;value&gt;: {1}.
        /// </summary>
        internal static string CacheItemConstructorInitialization {
            get {
                return ResourceManager.GetString("CacheItemConstructorInitialization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;CacheItem&gt; Updated hit with &lt;hit&gt;: {0}.
        /// </summary>
        internal static string CacheItemUpdateHit {
            get {
                return ResourceManager.GetString("CacheItemUpdateHit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;CacheItem&gt; Updated with &lt;value&gt;: {0}.
        /// </summary>
        internal static string CacheItemUpdateValue {
            get {
                return ResourceManager.GetString("CacheItemUpdateValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;EvicationPolicy&gt; Found eviction candidate &lt;key&gt;:(0), &lt;value&gt;: {1}, &lt;createdAt&gt;: {2}, &lt;modifiedAt&gt;: {3}, &lt;accessedAt&gt;: {4}, &lt;hits&gt;: {5}.
        /// </summary>
        internal static string EvicationPolicyCandidateFound {
            get {
                return ResourceManager.GetString("EvicationPolicyCandidateFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;EvicationPolicy&gt; Found 0 candidates to evict.
        /// </summary>
        internal static string EvicationPolicyCandidateNotFound {
            get {
                return ResourceManager.GetString("EvicationPolicyCandidateNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;InMemoryCacheProvider&gt; Removed cache item &lt;key&gt;: {0}.
        /// </summary>
        internal static string InMemoryCacheProviderCacheItemRemoved {
            get {
                return ResourceManager.GetString("InMemoryCacheProviderCacheItemRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;InMemoryCacheProvider&gt; Cleared cache.
        /// </summary>
        internal static string InMemoryCacheProviderClearStore {
            get {
                return ResourceManager.GetString("InMemoryCacheProviderClearStore", resourceCulture);
            }
        }
    }
}