// <copyright file="IAutocompleteHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// This attribute indicates whether the value of the control can be automatically 
    /// completed by the browser.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IAutocompleteHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This attribute indicates whether the value of the control can be automatically 
        /// completed by the browser. Possible values are:
        /// <list type="table">
        ///     <listheader>
        ///         <term>value</term>
        ///         <description>description</description>  
        ///     </listheader>
        ///     <item>  
        ///         <term>off</term>
        ///         <description>
        ///         The user must explicitly enter a value into this field for every use, or the 
        ///         document provides its own auto-completion method. The browser does not 
        ///         automatically complete the entry.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>on</term>
        ///         <description>
        ///         The browser is allowed to automatically complete the value based on values that 
        ///         the user has entered during previous uses, however on does not provide any 
        ///         further information about what kind of data the user might be expected to enter.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>name</term>
        ///         <description>
        ///         Full name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>honorific-prefix</term>
        ///         <description>
        ///         Prefix or title (e.g. "Mr.", "Ms.", "Dr.", "Mlle").
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>given-name</term>
        ///         <description>
        ///         First name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>additional-name</term>
        ///         <description>
        ///         Middle name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>family-name</term>
        ///         <description>
        ///         Last name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>honorific-suffix</term>
        ///         <description>
        ///         Suffix (e.g. "Jr.", "B.Sc.", "MBASW", "II").
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>nickname</term>
        ///         <description>
        ///         Nickname.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>email</term>
        ///         <description>
        ///         E-mailaddress.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>username</term>
        ///         <description>
        ///         User name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>new-password</term>
        ///         <description>
        ///         A new password (e.g. when creating an account or changing a password).
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>current-password</term>
        ///         <description>
        ///         The current password.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>organization-title</term>
        ///         <description>
        ///         Job title (e.g. "Software Engineer", "Senior Vice President", "Deputy Managing 
        ///         Director").
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>organization</term>
        ///         <description>
        ///         Organization.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>street-address</term>
        ///         <description>
        ///         Street address.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>
        ///         address-line1, address-line2, address-line3, address-level4, address-level3, 
        ///         address-level2, address-level1        
        ///         </term>
        ///         <description>
        ///         Address lines.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>country</term>
        ///         <description>
        ///         Country.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>country-name</term>
        ///         <description>
        ///         Country name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>postal-code</term>
        ///         <description>
        ///         Postal code.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-name</term>
        ///         <description>
        ///         Full name as given on the payment instrument.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-given-name</term>
        ///         <description>
        ///         Given name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-additional-name</term>
        ///         <description>
        ///         Additional name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-family-name</term>
        ///         <description>
        ///         Family name.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-number</term>
        ///         <description>
        ///         Code identifying the payment instrument (e.g. the credit card number).
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-exp</term>
        ///         <description>
        ///         Expiration date of the payment instrument.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-exp-month</term>
        ///         <description>
        ///         Expiration month.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-exp-year</term>
        ///         <description>
        ///         Expiration year.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-csc</term>
        ///         <description>
        ///         Security code for the payment instrument.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>cc-type</term>
        ///         <description>
        ///         Type of payment instrument (e.g. Visa).
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>transaction-currency</term>
        ///         <description>
        ///         Transaction currency.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>transaction-amount</term>
        ///         <description>
        ///         Transaction amount.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>language</term>
        ///         <description>
        ///         Preferred language; a valid BCP 47 language tag.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>bday</term>
        ///         <description>
        ///         Birthday.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>bday-day</term>
        ///         <description>
        ///         Birthday day of month.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>bday-month</term>
        ///         <description>
        ///         Birthday month of year.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>bday-year</term>
        ///         <description>
        ///         Birthday year.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>sex</term>
        ///         <description>
        ///         Gender identity (e.g. Female, Fa'afafine), free-form text, no newlines.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>tel</term>
        ///         <description>
        ///         Full telephone number, including country code. Additional tel variants: 
        ///         tel-country-code, tel-national, tel-area-code, tel-local, tel-local-prefix, 
        ///         tel-local-suffix, tel-extension.
        ///         </description>
        ///     </item>
        ///     <item>  
        ///         <term>url</term>
        ///         <description>
        ///         Home page or other Web page corresponding to the company, person, address, or 
        ///         contact information in the other fields associated with this field.
        ///         </description>
        ///     </item>
        ///     <item>  
        ///         <term>photo</term>
        ///         <description>
        ///         Photograph, icon, or other image corresponding to the company, person, address, 
        ///         or contact information in the other fields associated with this field.
        ///         </description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="value">
        /// The type of autocomplete.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("autocomplete")]
        T Autocomplete(string value);

        /// <summary>
        /// Indicates whether input elements can by default have their values automatically 
        /// completed by the browser. This setting can be overridden by an autocomplete 
        /// attribute on an element belonging to the form. Possible values are:
        /// off: The user must explicitly enter a value into each field for every use, or 
        /// the document provides its own auto-completion method; the browser does not 
        /// automatically complete entries.
        /// on: The browser can automatically complete values based on values that the user 
        /// has previously entered in the form.
        /// For most modern browsers (including Firefox 38+, Google Chrome 34+, IE 11+) 
        /// setting the autocomplete attribute will not prevent a browser's password manager 
        /// from asking the user if they want to store login fields (username and password), 
        /// if the user permits the storage the browser will autofill the login the next time 
        /// the user visits the page. See The autocomplete attribute and login fields.
        /// </summary>
        /// <param name="isEnabled">
        /// Whether or not autocomplete is enabled. Defaults to false.
        /// </param>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// <note type="note">
        /// If you set autocomplete to off in a form because the document provides its own 
        /// auto-completion, then you should also set autocomplete to off for each of the 
        /// form's input elements that the document can auto-complete. 
        /// </note>
        /// </remarks>
        [Code("autocomplete", IsBool = true, Format = BoolFormat.OnOff)]
        T Autocomplete(bool isEnabled);
    }
}