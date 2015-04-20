// <copyright file="ITenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Identity
{
    /// <summary>
    /// A tenant identification strategy is used to determine the ID for the current 
    /// tenant based on execution context.
    /// </summary>
    /// <remarks>
    /// Performance is important in tenant identification. Tenant identification happens 
    /// every time you resolve a component, begin a new lifetime scope, etc. As such, it 
    /// is very important to make sure your tenant identification strategy is fast. For 
    /// example, you wouldn’t want to do a service call or a database query during tenant 
    /// identification
    /// </remarks>
    public interface ITenantIdentificationStrategy
    {
        /// <summary>
        /// Attempts to identify the tenant from the current execution context.
        /// </summary>
        /// <param name="identifier">The current tenant identifier</param>
        /// <returns>True if the tenant could be identified; false if not</returns>
        bool TryIdentifyTenant(out ITenantIdentifier identifier);
    }
}