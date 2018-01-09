// <copyright file="InputType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// How an input works varies considerably depending on the value of its type 
    /// attribute.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// A push button with no default behavior.
        /// </summary>
        [Code("button")]
        Button,

        /// <summary>
        /// A check box allowing single values to be selected/deselected.
        /// </summary>
        [Code("checkbox")]
        Checkbox,

        /// <summary>
        /// A control for specifying a color. A color picker's UI has no required features 
        /// other than accepting simple colors as text.
        /// </summary>
        [Code("color")]
        Color,

        /// <summary>
        /// A control for entering a date (year, month, and day, with no time).
        /// </summary>
        [Code("date")]
        Date,

        /// <summary>
        /// A control for entering a date and time, with no time zone.
        /// </summary>
        [Code("datetime-local")]
        DateTimeLocal,

        /// <summary>
        /// A field for editing an e-mail address.
        /// </summary>
        [Code("email")]
        Email,

        /// <summary>
        /// A control that lets the user select a file. Use the accept attribute to define 
        /// the types of files that the control can select.
        /// </summary>
        [Code("file")]
        File,

        /// <summary>
        /// A control that is not displayed but whose value is submitted to the server.
        /// </summary>
        [Code("hidden")]
        Hidden,

        /// <summary>
        /// A graphical submit button. You must use the src attribute to define the source 
        /// of the image and the alt attribute to define alternative text. You can use the 
        /// height and width attributes to define the size of the image in pixels.
        /// </summary>
        [Code("image")]
        Image,

        /// <summary>
        /// A control for entering a month and year, with no time zone.
        /// </summary>
        [Code("month")]
        Month,

        /// <summary>
        /// A control for entering a number.
        /// </summary>
        [Code("number")]
        Number,

        /// <summary>
        /// A single-line text field whose value is obscured. Use the maxlength and 
        /// minlength attributes to specify the maximum length of the value that can be 
        /// entered.
        /// </summary>
        [Code("password")]
        Password,

        /// <summary>
        /// A radio button, allowing a single value to be selected out of multiple choices.
        /// </summary>
        [Code("radio")]
        Radio,

        /// <summary>
        /// A control for entering a number whose exact value is not important.
        /// </summary>
        [Code("range")]
        Range,

        /// <summary>
        /// A button that resets the contents of the form to default values.
        /// </summary>
        [Code("reset")]
        Reset,

        /// <summary>
        /// A single-line text field for entering search strings. Line-breaks are 
        /// automatically removed from the input value.
        /// </summary>
        [Code("search")]
        Search,

        /// <summary>
        /// A button that submits the form.
        /// </summary>
        [Code("submit")]
        Submit,

        /// <summary>
        /// A control for entering a telephone number.
        /// </summary>
        [Code("tel")]
        Tel,

        /// <summary>
        /// A single-line text field. Line-breaks are automatically removed from the input 
        /// value.
        /// </summary>
        [Code("text")]
        Text,

        /// <summary>
        /// A control for entering a time value with no time zone.
        /// </summary>
        [Code("time")]
        Time,

        /// <summary>
        /// A field for entering a URL.
        /// </summary>
        [Code("url")]
        Url,

        /// <summary>
        /// A control for entering a date consisting of a week-year number and a week number 
        /// with no time zone.
        /// </summary>
        [Code("week")]
        Week,
    }
}