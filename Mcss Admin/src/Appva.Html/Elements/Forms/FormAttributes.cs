// <copyright file="FormAttributes.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    public interface IFormAttributes<T>
    {
        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// A  list of character encodings that the server accepts. The browser uses them in 
        /// the order in which they are listed.
        /// <example>
        /// <code language="html" title="Accept-charset Example">
        ///     <form accept-charset="utf-8"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="charset">
        /// A space- or comma-delimited list of character encodings.
        /// </param>
        /// <returns>The {T}.</returns>
        IForm<T> Charset(params FormCharset[] charsets);

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// The URI of a program that processes the form information. This value can be 
        /// overridden by a formaction attribute on a <button> or <input> element.
        /// <example>
        /// <code language="html" title="Action Example">
        ///     <form action="foo/bar/create"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="uri">The action URI.</param>
        /// <returns>The {T}.</returns>
        ///IForm<T> Action(Uri uri);

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
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
        /// <example>
        /// <code language="html" title="Autocomplete Example">
        ///     <form autocomplete="on"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="isEnabled">
        /// Whether or not autocomplete is enabled. Defaults to false.
        /// </param>
        /// <returns>The {T}.</returns>
        IForm<T> Autocomplete(bool isEnabled);

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// When the value of the method attribute is post, enctype is the MIME type of 
        /// content that is used to submit the form to the server.
        /// <example>
        /// <code language="html" title="Encoding Example">
        ///     <form enctype="multipart/form-data"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// This value can be overridden by a formenctype attribute on a <button> or <input> 
        /// element.
        /// </remarks>
        IForm<T> Encoding(FormEncoding encoding = FormEncoding.FormUrlEncoded);

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// The HTTP method that the browser uses to submit the form. Possible values are:
        /// post: Corresponds to the HTTP POST method ; form data are included in the body 
        /// of the form and sent to the server.
        /// get: Corresponds to the HTTP GET method; form data are appended to the action 
        /// attribute URI with a '?' as separator, and the resulting URI is sent to the 
        /// server. Use this method when the form has no side-effects and contains only 
        /// ASCII characters.
        /// This value can be overridden by a formmethod attribute on a <button> or <input> 
        /// element.
        /// <example>
        /// <code language="html" title="Method Example">
        ///     <form method="post"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>The {T}.</returns>
        ///IForm<T> Method();

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// The name of the form. In HTML 4, its use is deprecated (id should be used 
        /// instead). It must be unique among the forms in a document and not just an empty 
        /// string in HTML 5.
        /// <example>
        /// <code language="html" title="Method Example">
        ///     <form name="form-1"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>The {T}.</returns>
        IForm<T> Name(string name);

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// This Boolean attribute indicates that the form is not to be validated when 
        /// submitted. If this attribute is not specified (and therefore the form is 
        /// validated), this default setting can be overridden by a formnovalidate attribute 
        /// on a <button> or <input> element belonging to the form.
        /// <example>
        /// <code language="html" title="NoValidate Example">
        ///     <form novalidate></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>The {T}.</returns>
        IForm<T> DisableValidation();

        /// <summary>
        /// target
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// A name or keyword indicating where to display the response that is received 
        /// after submitting the form. In HTML 4, this is the name/keyword for a frame. 
        /// In HTML5, it is a name/keyword for a browsing context (for example, tab, window, 
        /// or inline frame).
        /// <example>
        /// <code language="html" title="Target Example">
        ///     <form target="_self"></form>
        /// </code>
        /// </example>
        /// </summary>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// HTML5: This value can be overridden by a formtarget attribute on a <button> or 
        /// <input> element.
        /// </remarks>
        IForm<T> Target(FormTarget target = FormTarget.Self);
    }

    public enum FormCharset
    {
        /// <summary>
        /// The reserved string "UNKNOWN", indicates the same encoding as that of the 
        /// document containing the form element.
        /// </summary>
        /// <remarks>
        /// The default value.
        /// </remarks>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        Utf8
    }

    public enum FormEncoding
    {
        /// <summary>
        /// The default value if the attribute is not specified.
        /// </summary>
        /// <remarks>
        /// application/x-www-form-urlencoded
        /// </remarks>
        FormUrlEncoded,
        
        /// <summary>
        /// The value used for an <input> element with the type attribute set to "file".
        /// </summary>
        /// <remarks>
        /// multipart/form-data
        /// </remarks>
        FormData,

        /// <summary>
        /// HTML5 : text/plain.
        /// </summary>
        /// <remarks>
        /// text/plain
        /// </remarks>
        PlainText
    }

    public enum FormTarget
    {
        /// <summary>
        /// Load the response into the same HTML 4 frame (or HTML5 browsing context) 
        /// as the current one. This value is the default if the attribute is not specified.
        /// <example>
        /// <code language="html" title="Target Self Example">
        ///     <form target="_self"></form>
        /// </code>
        /// </example>
        /// </summary>
        Self,

        /// <summary>
        /// Load the response into a new unnamed HTML 4 window or HTML5 browsing 
        /// context.
        /// <example>
        /// <code language="html" title="Target Blank Example">
        ///     <form target="_blank"></form>
        /// </code>
        /// </example>
        /// </summary>
        Blank,

        /// <summary>
        /// Load the response into the HTML 4 frameset parent of the current frame, 
        /// or HTML5 parent browsing context of the current one. If there is no parent, this 
        /// option behaves the same way as _self.
        /// <example>
        /// <code language="html" title="Target Parent Example">
        ///     <form target="_parent"></form>
        /// </code>
        /// </example>
        /// </summary>
        Parent,

        /// <summary>
        /// HTML 4: Load the response into the full original window, and cancel all 
        /// other frames. HTML5: Load the response into the top-level browsing context (i.e., 
        /// the browsing context that is an ancestor of the current one, and has no parent). 
        /// If there is no parent, this option behaves the same way as _self.
        /// <example>
        /// <code language="html" title="Target Top Example">
        ///     <form target="_top"></form>
        /// </code>
        /// </example>
        /// </summary>
        Top,

        /// <summary>
        /// The response is displayed in a named iframe.
        /// <example>
        /// <code language="html" title="Target IFrame Example">
        ///     <form target="iframename"></form>
        /// </code>
        /// </example>
        /// </summary>
        IFrame
    }

    public static class FormCharsetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="charset"></param>
        /// <returns></returns>
        /// <remarks>
        /// In previous versions of HTML, the different character encodings could be 
        /// delimited by spaces or commas. 
        /// In HTML5, only spaces are allowed as delimiters.
        /// </remarks>
        public static string AsString(this FormCharset[] charset)
        {
            return string.Join(" ", charset);
        }

        public static string AsString(this FormCharset charset)
        {
            switch (charset)
            {
                case FormCharset.Unknown:
                    return "UNKNOWN";
                case FormCharset.Utf8:
                    return "utf-8";
            }
            return FormCharset.Unknown.ToString();
        }
    }
    public static class FormEncodingExtensions
    {
        public static string AsString(this FormEncoding encoding)
        {
            switch (encoding)
            {
                case FormEncoding.FormUrlEncoded:
                    return "application/x-www-form-urlencoded";
                case FormEncoding.FormData:
                    return "multipart/form-data";
                case FormEncoding.PlainText:
                    return "text/plain";
                default:
                    return string.Empty;
            }
        }
    }

    public static class FormTargetExtensions
    {
        public static string AsString(this FormTarget target)
        {
            switch (target)
            {
                case FormTarget.Self:
                    return "_self";
                case FormTarget.Blank:
                    return "_blank";
                case FormTarget.Parent:
                    return "_parent";
                case FormTarget.Top:
                    return "_top";
                case FormTarget.IFrame:
                    return "iframename";
                default:
                    return string.Empty;
            }
        }
    }
}