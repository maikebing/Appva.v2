// <copyright file="ISecurityLabel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.ValueSets
{
    #region Imports.

    using System.Collections.Generic;
    using Complex;

    #endregion

    /// <summary>
    /// A security label is a concept attached to a resource or bundle that provides 
    /// specific security metadata about the information it is fixed to. The Access 
    /// Control decision engine uses the security label together with any provenance 
    /// resources associated with the resource and other metadata (e.g. the resource 
    /// type, resource contents, etc.) to 
    /// <list type="bullet">
    ///     <item>approve read, change, and other operations</item> 
    ///     <item>determine what level of the resource can be returned</item>
    ///     <item>determine what handling caveats must be conveyed with the data</item>
    /// </list>
    /// <para>
    /// Security Labels enable more data to flow as they enable policy fragments to 
    /// accompany the resource data.
    /// </para>
    /// <para>
    /// The intent of a security label is that the recipient of resources or bundles 
    /// with security-tags is obligated to enforce the handling caveats of the tags and 
    /// carry the security labels forward as appropriate.
    /// </para>
    /// <para>
    /// Security labels are only a device to connect specific resources, bundles, or 
    /// operations to a wider security framework; a full set of policy and consent 
    /// statements and their consequent obligations is needed to give the labels meaning.
    /// </para>
    /// <para>
    /// As a consequence of this, security labels are most effective in fully trusted 
    /// environments - that is, where all trading partners have agreed to abide by them in 
    /// a Mutual Trust Framework. Note also that security labels support policy, and 
    /// specific tagging of individual resources is not always required to implement policy 
    /// correctly.
    /// </para>
    /// <para>
    /// In the absence of this kind of pre-agreement, Security Labels may still be used by 
    /// individual parties to assist with security role checking, but they may not all be 
    /// recognized and enforced, which in turn limits what information is allowed to flow.
    /// Local agreements and implementation profiles for the use security labels should 
    /// describe how the security labels connect to the relevant consent and policy 
    /// statements, and in particular:
    /// </para>
    /// <list type="bullet">
    /// <item>Which Security Labels are able to be used</item>
    /// <item>What do if a resource has an unrecognized security label on it</item>
    /// <item>Authoring obligations around security labels</item>
    /// <item>Operational implications of security labels</item>
    /// </list>
    /// <para>
    /// This specification defines a basic set of labels for the most common use cases 
    /// trading partners, and also a wider array of security labels that allow much finer 
    /// grained management of the information.
    /// </para>
    /// <externalLink>
    ///     <linkText>2.13.1 Security Labels</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/security-labels.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public interface ISecurityLabel
    {
        #region Properties.

        /// <summary>
        /// The name of the coding system.
        /// </summary>
        IList<Coding> Coding
        {
            get;
        }

        /// <summary>
        /// A human language representation of the concept as seen/selected/uttered by the 
        /// user who entered the data and/or which represents the intended meaning of the
        /// user.
        /// </summary>
        string Text
        {
            get;
        }

        #endregion
    }
}