// <copyright file="HtmlElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The base HTML element <see cref="IHtmlElement{T}"/> 
    /// implementation.
    /// </summary>
    public abstract class HtmlElement<T> : INotifyPropertyChanged, IHtmlElement<T> where T : class, IHtmlElement<T>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="HtmlHelper"/>.
        /// </summary>
        private readonly HtmlHelper htmlHelper;

        /// <summary>
        /// The <see cref="TagBuilder"/>.
        /// </summary>
        private readonly TagBuilder builder;

        /// <summary>
        /// A mapped position to collection of html elements.
        /// </summary>
        private readonly PositionHtmlElementDictionary elements;

        /// <summary>
        /// The unique html element identifier.
        /// </summary>
        private string uniqueId;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement{T}"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="tag">
        /// The <see cref="Builder"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="tag"/> is null.
        /// </exception>
        protected HtmlElement(HtmlHelper htmlHelper, Tag tag)
        {
            Argument.Null(htmlHelper, "htmlHelper");
            Argument.Null(tag,        "tag");
            this.htmlHelper = htmlHelper;
            this.builder    = new TagBuilder(tag.Name);
            this.elements   = new PositionHtmlElementDictionary();
            this.UniqueId   = this.GenerateId();
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The element unique ID.
        /// </summary>
        public string UniqueId
        {
            get
            {
                return this.uniqueId;
            }
            protected internal set
            {
                this.uniqueId = value;
                this.OnPropertyChanged("uniqueId");
            }
        }

        #endregion

        #region IHtmlElement<T> Members. /* global */

        /// <inheritdoc />
        public T Role(string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Role(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Aria(string key, string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Aria(key, value), key, value);
            return this as T;
        }

        /// <inheritdoc />
        public T On(OnEventHandler key, string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.On(key, value), key, value);
            return this as T;
        }

        /// <inheritdoc />
        public T AccessKey(string hint)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.AccessKey(hint), null, hint);
            return this as T;
        }

        /// <inheritdoc />
        public T Class(params string[] classes)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Class(classes), null, classes);
            return this as T;
        }

        /// <inheritdoc />
        public T IsEditable(bool isContentEditable)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.IsEditable(isContentEditable), null, isContentEditable);
            return this as T;
        }

        /// <inheritdoc />
        public T ContextMenu(string id)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.ContextMenu(id), null, id);
            return this as T;
        }

        /// <inheritdoc />
        public T Data(string key, string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Data(key, value), key, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Direction(TextDirection direction = TextDirection.LeftToRight)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Direction(direction), null, direction);
            return this as T;
        }

        /// <inheritdoc />
        public T Draggable(bool isDraggable)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Draggable(isDraggable), null, isDraggable);
            return this as T;
        }

        /// <inheritdoc />
        public T Dropzone(DropzoneType type)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Dropzone(type), null, type);
            return this as T;
        }

        /// <inheritdoc />
        public T Hidden()
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Hidden());
            return this as T;
        }

        /// <inheritdoc />
        /// <remarks>
        /// <note type="note">
        /// Using characters except ASCII letters, digits, '_', '-' and '.' may cause 
        /// compatibility problems, as they weren't allowed in HTML 4. Though this 
        /// restriction has been lifted in HTML 5, an ID should start with a letter for 
        /// compatibility.
        /// </note>
        /// </remarks>
        public T Id(string value)
        {
            this.UniqueId = value;
            this.AddAttribute<HtmlElement<T>>(x => x.Id(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Language(string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Language(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Spellcheck(bool enabled)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Spellcheck(enabled), null, enabled);
            return this as T;
        }

        /// <inheritdoc />
        /// <remarks>
        /// <note type="note">
        /// Note that it is recommended for styles to be defined in a separate file or 
        /// files. This attribute and the style element have mainly the purpose of allowing 
        /// for quick styling, for example for testing purposes.
        /// </note>
        /// </remarks>
        public T Style(string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Style(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T TabIndex(int value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.TabIndex(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Title(string value)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Title(value), null, value);
            return this as T;
        }

        /// <inheritdoc />
        public T Translate(bool translate)
        {
            this.AddAttribute<HtmlElement<T>>(x => x.Translate(translate), null, translate);
            return this as T;
        }

        #endregion

        /// <summary>
        /// This attribute indicates whether the value of the control can be automatically 
        /// completed by the browser.
        /// </summary>
        /// <param name="isEnabled">
        /// Whether or not autocomplete is enabled. Defaults to false.
        /// </param>
        /// <returns>The {T}.</returns>
        /// <seealso cref="IAutocompleteHtmlAttribute{T}"/>
        public T Autocomplete(bool isEnabled)
        {
            this.AddAttribute<IAutocompleteHtmlAttribute<T>>(x => x.Autocomplete(isEnabled), null, isEnabled);
            return this as T;
        }

        /// <summary>
        /// This attribute indicates whether the value of the control can be automatically 
        /// completed by the browser.
        /// </summary>
        /// <param name="value">
        /// The type of autocomplete.
        /// </param>
        /// <returns>The {T}.</returns>
        /// <seealso cref="IAutocompleteHtmlAttribute{T}"/>
        public T Autocomplete(string value)
        {
            this.AddAttribute<IAutocompleteHtmlAttribute<T>>(x => x.Autocomplete(value), null, value);
            return this as T;
        }

        /// <summary>
        /// A  list of character encodings that the server accepts. The browser uses them in 
        /// the order in which they are listed.
        /// </summary>
        /// <param name="charsets">
        /// A space- or comma-delimited list of character encodings.
        /// </param>
        /// <returns>The {T}.</returns>
        public T Charset(params FormCharset[] charsets)
        {
            this.AddAttribute<IAcceptCharsetHtmlAttribute<T>>(x => x.Charset(charsets), null, charsets);
            return this as T;
        }

        /// <summary>
        /// This Boolean attribute indicates that the form is not to be validated when 
        /// submitted.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T EnableValidation()
        {
            this.RemoveAttribute<INoValidateHtmlAttribute<T>>(x => x.EnableValidation());
            return this as T;
        }

        /// <summary>
        /// This Boolean attribute indicates that the form is not to be validated when 
        /// submitted.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T DisableValidation()
        {
            this.AddAttribute<INoValidateHtmlAttribute<T>>(x => x.DisableValidation());
            return this as T;
        }

        /// <summary>
        /// When the value of the method attribute is post, enctype is the MIME type of 
        /// content that is used to submit the form to the server.
        /// </summary>
        /// <param name="encoding">The form encoding.</param>
        /// <returns>The {T}.</returns>
        public T Encoding(FormEncoding encoding)
        {
            this.AddAttribute<IEncTypeHtmlAttribute<T>>(x => x.Encoding(encoding), null, encoding);
            return this as T;
        }

        /// <summary>
        /// The name of the control.
        /// </summary>
        /// <param name="value">The name of the input or form element.</param>
        /// <returns>The {T}.</returns>
        public T Name(string value)
        {
            this.AddAttribute<INameHtmlAttribute<T>>(x => x.Name(value), null, value);
            return this as T;
        }

        /// <summary>
        /// A name or keyword indicating where to display the response that is received 
        /// after submitting the form.
        /// </summary>
        /// <param name="target">The form target.</param>
        /// <returns>The {T}.</returns>
        public T Target(FormTarget target)
        {
            this.AddAttribute<ITargetHtmlAttribute<T>>(x => x.Target(target), null, target);
            return this as T;
        }

        //////////////////////

        /// <summary>
        /// If the value of the type attribute is file, then this attribute will indicate 
        /// the types of files that the server accepts, otherwise it will be ignored.
        /// </summary>
        /// <param name="value">The accept value.</param>
        /// <returns>The {T}.</returns>
        public T Accept(string value)
        {
            this.AddAttribute<IAcceptHtmlAttribute<T>>(x => x.Accept(value), null, value);
            return this as T;
        }

        /// <summary>
        /// The image alternative text.
        /// </summary>
        /// <param name="value">The alternative text.</param>
        /// <returns>The {T}.</returns>
        public T AlternativeText(string value)
        {
            this.AddAttribute<IAltHtmlAttribute<T>>(x => x.AlternativeText(value), null, value);
            return this as T;
        }

        /// <summary>
        /// This Boolean attribute lets you specify that a form control should have input 
        /// focus when the page loads, unless the user overrides it (e.g. by typing in a 
        /// different control). Only one form element in a document can have the autofocus 
        /// attribute, which is a Boolean. It cannot be applied if the type attribute is set 
        /// to hidden (that is, you cannot automatically set focus to a hidden control).
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Autofocus()
        {
            this.AddAttribute<IAutofocusHtmlAttribute<T>>(x => x.Autofocus(), null, null);
            return this as T;
        }

        /// <summary>
        /// When the value of the type attribute is file, the presence of this Boolean 
        /// attribute indicates that capture of media directly from the device's environment 
        /// using a media capture mechanism is preferred.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Capture()
        {
            this.AddAttribute<ICaptureHtmlAttribute<T>>(x => x.Capture(), null, null);
            return this as T;
        }

        /// <summary>
        /// When the value of the type attribute is radio or checkbox, the presence of this 
        /// Boolean attribute indicates that the control is selected by default, otherwise 
        /// it is ignored.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Checked()
        {
            this.AddAttribute<ICheckedHtmlAttribute<T>>(x => x.Checked(), null, null);
            return this as T;
        }

        /// <summary>
        /// The visible width of the text control, in average character widths. If it is 
        /// specified, it must be a positive integer. If it is not specified, the default 
        /// value is 20.
        /// </summary>
        /// <param name="value">The width of the text control</param>
        /// <returns>The {T}.</returns>
        public T Columns(int value)
        {
            this.AddAttribute<IColsHtmlAttribute<T>>(x => x.Columns(value), null, value);
            return this as T;
        }

        /// <summary>
        /// This Boolean attribute indicates that the form control is not available for 
        /// interaction. In particular, the click event will not be dispatched on disabled 
        /// controls. Also, a disabled control's value isn't submitted with the form.
        /// </summary>
        /// <param name="disabled">
        /// If disabled condition.
        /// </param>
        /// <returns>The {T}.</returns>
        public T IsDisabled(bool disabled)
        {
            this.AddAttribute<IDisabledHtmlAttribute<T>>(x => x.IsDisabled(disabled), null, disabled);
            return this as T;
        }

        /// <summary>
        /// The URI of a program that processes the information submitted by the input 
        /// element, if it is a submit button or image. If specified, it overrides the 
        /// action attribute of the element's form owner.
        /// </summary>
        /// <param name="action">The action URI.</param>
        /// <returns>The {T}.</returns>
        public T FormAction(Uri action)
        {
            this.AddAttribute<IFormActionHtmlAttribute<T>>(x => x.FormAction(action), null, action);
            return this as T;
        }

        /// <summary>
        /// The form element that the input element is associated with (its form owner). The 
        /// value of the attribute must be an id of a form element in the same document. If 
        /// this attribute is not specified, this input element must be a descendant of a 
        /// form element. This attribute enables you to place input elements anywhere within 
        /// a document, not just as descendants of their form elements. An input can only be 
        /// associated with one form.
        /// </summary>
        /// <param name="id">The id of a form element in the same document.</param>
        /// <returns>The {T}.</returns>
        public T Form(string id)
        {
            this.AddAttribute<IFormHtmlAttribute<T>>(x => x.Form(id), null, id);
            return this as T;
        }

        /// <summary>
        /// If the input element is a submit button or image, this attribute specifies the 
        /// type of content that is used to submit the form to the server.
        /// </summary>
        /// <param name="value">The form encoding.</param>
        /// <returns>The {T}.</returns>
        public T FormEncoding(FormEncoding value)
        {
            this.AddAttribute<IFormEncTypeHtmlAttribute<T>>(x => x.FormEncoding(value), null, value);
            return this as T;
        }

        /// <summary>
        /// If the input element is a submit button or image, this attribute specifies the 
        /// HTTP method that the browser uses to submit the form. 
        /// </summary>
        /// <param name="method">The form method.</param>
        /// <returns>The {T}.</returns>
        public T FormMethod(FormMethod method)
        {
            this.AddAttribute<IFormMethodHtmlAttribute<T>>(x => x.FormMethod(method), null, method);
            return this as T;
        }

        /// <summary>
        /// If the input element is a submit button or image, this Boolean attribute 
        /// specifies that the form is not to be validated when it is submitted. If this 
        /// attribute is specified, it overrides the novalidate attribute of the element's 
        /// form owner.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T FormDisableValidation()
        {
            this.AddAttribute<IFormNoValidateHtmlAttribute<T>>(x => x.FormDisableValidation(), null, null);
            return this as T;
        }

        /// <summary>
        /// If the input element is a submit button or image, this attribute is a name or 
        /// keyword indicating where to display the response that is received after 
        /// submitting the form.
        /// </summary>
        /// <param name="target">The form target.</param>
        /// <returns>The {T}.</returns>
        public T FormTarget(FormTarget target)
        {
            this.AddAttribute<IFormTargetHtmlAttribute<T>>(x => x.FormTarget(target), null, target);
            return this as T;
        }

        /// <summary>
        /// If the value of the type attribute is image, this attribute defines the height 
        /// of the image displayed for the button.
        /// </summary>
        /// <param name="value">The height.</param>
        /// <returns>The {T}.</returns>
        public T Height(string value)
        {
            this.AddAttribute<IHeightHtmlAttribute<T>>(x => x.Height(value), null, value);
            return this as T;
        }

        /// <summary>
        /// Identifies a list of pre-defined options to suggest to the user. The value must 
        /// be the id of a datalist element in the same document. The browser displays only 
        /// options that are valid values for this input element. This attribute is ignored 
        /// when the type attribute's value is hidden, checkbox, radio, file, or a button 
        /// type.
        /// </summary>
        /// <param name="id">The id of a datalist element.</param>
        /// <returns>The {T}.</returns>
        public T List(string id)
        {
            this.AddAttribute<IListHtmlAttribute<T>>(x => x.List(id), null, id);
            return this as T;
        }

        /// <summary>
        /// The maximum (numeric or date-time) value for this item, which must not be less 
        /// than its minimum (min attribute) value.
        /// </summary>
        /// <param name="value">The maximum value.</param>
        /// <returns>The {T}.</returns>
        public T Max(object value)
        {
            this.AddAttribute<IMaxHtmlAttribute<T>>(x => x.Max(value), null, value);
            return this as T;
        }

        /// <summary>
        /// If the value of the type attribute is text, email, search, password, tel, or 
        /// url, this attribute specifies the maximum number of characters (in UTF-16 code 
        /// units) that the user can enter. For other control types, it is ignored. It can 
        /// exceed the value of the size attribute. If it is not specified, the user can 
        /// enter an unlimited number of characters. Specifying a negative number results in 
        /// the default behavior (i.e. the user can enter an unlimited number of 
        /// characters). The constraint is evaluated only when the value of the attribute 
        /// has been changed.
        /// </summary>
        /// <param name="value">The maximum number of characters the user can enter.</param>
        /// <returns>The {T}.</returns>
        public T MaxLength(int value)
        {
            this.AddAttribute<IMaxLengthHtmlAttribute<T>>(x => x.MaxLength(value), null, value);
            return this as T;
        }

        /// <summary>
        /// The minimum (numeric or date-time) value for this item, which must not be 
        /// greater than its maximum (max attribute) value.
        /// </summary>
        /// <param name="value">The minimum value.</param>
        /// <returns>The {T}.</returns>
        public T Min(object value)
        {
            this.AddAttribute<IMinHtmlAttribute<T>>(x => x.Min(value), null, value);
            return this as T;
        }

        /// <summary>
        /// If the value of the type attribute is text, email, search, password, tel, or 
        /// url, this attribute specifies the minimum number of characters (in Unicode code 
        /// points) that the user can enter. For other control types, it is ignored.
        /// </summary>
        /// <param name="value">The minimum number of characters the user can enter.</param>
        /// <returns>The {T}.</returns>
        public T MinLength(int value)
        {
            this.AddAttribute<IMinLengthHtmlAttribute<T>>(x => x.MinLength(value), null, value);
            return this as T;
        }

        /// <summary>
        /// This Boolean attribute indicates whether the user can enter more than one value. 
        /// This attribute applies when the type attribute is set to email or file, 
        /// otherwise it is ignored.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Multiple()
        {
            this.AddAttribute<IMultipleHtmlAttribute<T>>(x => x.Multiple(), null, null);
            return this as T;
        }

        /// <summary>
        /// A regular expression that the control's value is checked against. The pattern 
        /// must match the entire value, not just some subset. Use the title attribute to 
        /// describe the pattern to help the user. This attribute applies when the value of 
        /// the type attribute is text, search, tel, url, email, or password, otherwise it 
        /// is ignored. The regular expression language is the same as JavaScript RegExp 
        /// algorithm, with the 'u' parameter that makes it treat the pattern as a sequence 
        /// of unicode code points. The pattern is not surrounded by forward slashes.
        /// </summary>
        /// <param name="value">A regular expression.</param>
        /// <returns>The {T}.</returns>
        public T Pattern(string value)
        {
            this.AddAttribute<IPatternHtmlAttribute<T>>(x => x.Pattern(value), null, value);
            return this as T;
        }

        /// <summary>
        /// A hint to the user of what can be entered in the control . The placeholder text 
        /// must not contain carriage returns or line-feeds. 
        /// </summary>
        /// <param name="hint">The hint text.</param>
        /// <returns>The {T}.</returns>
        public T Placeholder(string hint)
        {
            this.AddAttribute<IPlaceholderHtmlAttribute<T>>(x => x.Placeholder(hint), null, hint);
            return this as T;
        }

        /// <summary>
        /// This attribute indicates that the user cannot modify the value of the control. 
        /// The value of the attribute is irrelevant. If you need read-write access to the 
        /// input value, do not add the "readonly" attribute. It is ignored if the value of 
        /// the type attribute is hidden, range, color, checkbox, radio, file, or a button 
        /// type (such as button or submit).
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Readonly()
        {
            this.AddAttribute<IReadonlyHtmlAttribute<T>>(x => x.Readonly(), null, null);
            return this as T;
        }

        /// <summary>
        /// This attribute specifies that the user must fill in a value before submitting a 
        /// form. It cannot be used when the type attribute is hidden, image, or a button 
        /// type (submit, reset, or button). The :optional and :required CSS pseudo-classes 
        /// will be applied to the field as appropriate.
        /// </summary>
        /// <returns>The {T}.</returns>
        public T Required()
        {
            this.AddAttribute<IRequiredHtmlAttribute<T>>(x => x.Required(), null, null);
            return this as T;
        }

        /// <summary>
        /// The number of visible text lines for the control.
        /// </summary>
        /// <param name="value">The number of visible text lines.</param>
        /// <returns>The {T}.</returns>
        public T Rows(int value)
        {
            this.AddAttribute<IRowsHtmlAttribute<T>>(x => x.Rows(value), null, value);
            return this as T;
        }

        /// <summary>
        /// The direction in which selection occurred.
        /// </summary>
        /// <param name="direction">
        /// The direction, either "forward", "backward" or "none".
        /// </param>
        /// <returns>The {T}.</returns>
        public T SelectionDirection(string direction)
        {
            this.AddAttribute<ISelectionDirectionHtmlAttribute<T>>(x => x.SelectionDirection(direction), null, direction);
            return this as T;
        }

        /// <summary>
        /// The offset into the element's text content of the last selected character. If 
        /// there's no selection, this value indicates the offset to the character following 
        /// the current text input cursor position (that is, the position the next character 
        /// typed would occupy).
        /// </summary>
        /// <param name="end">
        /// The end index of the selected text.
        /// </param>
        /// <returns>The {T}.</returns>
        public T SelectionEnd(int end)
        {
            this.AddAttribute<ISelectionEndHtmlAttribute<T>>(x => x.SelectionEnd(end), null, end);
            return this as T;
        }

        /// <summary>
        /// The offset into the element's text content of the first selected character. If 
        /// there's no selection, this value indicates the offset to the character following 
        /// the current text input cursor position (that is, the position the next character 
        /// typed would occupy).
        /// </summary>
        /// <param name="start">
        /// The start index of the selected text.
        /// </param>
        /// <returns>The {T}.</returns>
        public T SelectionStart(int start)
        {
            this.AddAttribute<ISelectionStartHtmlAttribute<T>>(x => x.SelectionStart(start), null, start);
            return this as T;
        }

        /// <summary>
        /// The initial size of the control. This value is in pixels unless the value of the 
        /// type attribute is text or password, in which case it is an integer number of 
        /// characters.
        /// </summary>
        /// <param name="value">
        /// Optional size value; defaults to 20.
        /// </param>
        /// <returns>The {T}.</returns>
        public T Size(int value = 20)
        {
            this.AddAttribute<ISizeHtmlAttribute<T>>(x => x.Size(value), null, value);
            return this as T;
        }

        /// <summary>
        /// If the value of the type attribute is image, this attribute specifies a URI for 
        /// the location of an image to display on the graphical submit button, otherwise it 
        /// is ignored.
        /// </summary>
        /// <param name="location">The URI location.</param>
        /// <returns>The {T}.</returns>
        public T Source(Uri location)
        {
            this.AddAttribute<ISrcHtmlAttribute<T>>(x => x.Source(location), null, location);
            return this as T;
        }

        /// <summary>
        /// Works with the min and max attributes to limit the increments at which a numeric 
        /// or date-time value can be set. It can be the string any or a positive floating 
        /// point number. If this attribute is not set to any, the control accepts only 
        /// values at multiples of the step value greater than the minimum.
        /// </summary>
        /// <param name="value">The step.</param>
        /// <returns>The {T}.</returns>
        public T Step(string value)
        {
            this.AddAttribute<IStepHtmlAttribute<T>>(x => x.Step(value), null, value);
            return this as T;
        }

        /// <summary>
        /// The initial value of the control. This attribute is optional except when the 
        /// value of the type attribute is radio or checkbox.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The {T}.</returns>
        public T Value(string value)
        {
            this.AddAttribute<IValueHtmlAttribute<T>>(x => x.Value(value), null, value);
            return this as T;
        }

        /// <summary>
        /// If the value of the type attribute is image, this attribute defines the width of 
        /// the image displayed for the button.
        /// </summary>
        /// <param name="value">The width of the element.</param>
        /// <returns>The {T}.</returns>
        public T Width(string value)
        {
            this.AddAttribute<IWidthHtmlAttribute<T>>(x => x.Width(value), null, value);
            return this as T;
        }

        /// <summary>
        /// Indicates how the control wraps text.
        /// </summary>
        /// <param name="value">How the control wraps text.</param>
        /// <returns>The {T}.</returns>
        public T Wrap(TextWrapType value)
        {
            this.AddAttribute<IWrapHtmlAttribute<T>>(x => x.Wrap(value), null, value);
            return this as T;
        }

        #region Protected Members.

        /// <summary>
        /// Returns the html helper.
        /// </summary>
        protected HtmlHelper Html
        {
            get
            {
                return this.htmlHelper;
            }
        }

        protected ViewContext ViewContext
        {
            get
            {
                return this.htmlHelper.ViewContext;
            }
        }

        /// <summary>
        /// Returns the tag builder.
        /// </summary>
        protected TagBuilder Builder
        {
            get
            {
                return this.builder;
            }
        }

        /// <summary>
        /// The method to raise the on property change event.
        /// </summary>
        /// <param name="name">The property name that was changed.</param>
        protected virtual void OnPropertyChanged(string name)
        {
            var handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }
            handler(this, new PropertyChangedEventArgs(name));
        }

        protected void AddElement(Position position, IHtmlElement element)
        {
            if (! this.elements.ContainsKey(position))
            {
                this.elements[position] = new List<IHtmlElement>();
            }
            if (! this.elements[position].Contains(element))
            {
                this.elements[position].Add(element);
            }
        }

        protected string GenerateId(string prefix = "id")
        {
            return TagBuilder.CreateSanitizedId(string.Format("{0}_{1}", prefix, Guid.NewGuid().ToString("N")));
        }

        protected string GenerateName(string name)
        {
            return null;
        }

        /// <summary>
        /// Writes the text representation of an object to the text string or stream by 
        /// calling the ToString method on that object.
        /// </summary>
        /// <param name="value">
        /// The object to write.
        /// </param>
        /// <exception cref="System.ObjectDisposedException">
        /// The <see cref="System.IO.TextWriter"/> is closed.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// An I/O error occurs.
        /// </exception>
        protected void Write(object value)
        {
            this.ViewContext.Writer.Write(value);
        }

        /// <summary>
        /// Renders the HTML tag by using the specified render mode.
        /// </summary>
        /// <param name="mode">The render mode.</param>
        /// <returns>The rendered HTML tag.</returns>
        protected string Render(TagRenderMode mode)
        {
            return this.Builder.ToString(mode);
        }

        /// <summary>
        /// Adds a new attribute or optionally replaces an existing attribute in the opening 
        /// tag.
        /// </summary>
        /// <param name="key">
        /// The key for the attribute.
        /// </param>
        /// <param name="value">
        /// The value of the attribute.
        /// </param>
        /// <param name="replace">
        /// <c>true</c> to replace an existing attribute if an attribute exists that has the
        /// specified key value, or <c>false</c> to leave the original attribute unchanged.
        /// </param>
        protected void AddAttribute(string key, string value, bool replace = true)
        {
            this.Builder.MergeAttribute(key, value, replace);
        }

        protected void AddAttribute<V>(Expression<Action<V>> action, object key = null, object value = null)
        {
            Argument.Guard.NotNull("action", action);
            var call = action.Body as MethodCallExpression;
            if (call == null)
            {
                return;
            }
            var attribute = Annotation.Find<CodeAttribute>(typeof(T), call.Method.Name);
            if (attribute == null)
            {
                Debug.Assert(false, typeof(T).Name + " " + call.Method.Name + " has no attribute.");
                return;
            }
            if (attribute.IsNoValue)
            {
                this.AddAttribute(attribute.Value, string.Empty);
                return;
            }
            if (value == null)
            {
                return;
            }
            this.AddAttribute(attribute.GetAttributeName(key), this.GetValue(attribute, value));
        }

        protected void RemoveAttribute<V>(Expression<Action<V>> action)
        {
            Argument.Guard.NotNull("action", action);
            var call = action.Body as MethodCallExpression;
            if (call == null)
            {
                return;
            }
            var attribute = Annotation.Find<CodeAttribute>(typeof(T), call.Method.Name);
            if (attribute == null)
            {
                Debug.Assert(false, typeof(T).Name + " " + call.Method.Name + " has no attribute.");
                return;
            }
            this.Builder.RemoveAttribute(attribute.Value);
        }

        private string GetValue(CodeAttribute attribute, object value)
        {
            var type = value.GetType();
            if (type.IsEnum)
            {
                var annotation = Annotation.Find<CodeAttribute>(type, Enum.GetName(type, value));
                if (annotation != null)
                {
                    return annotation.Value;
                }
            }
            if (type.IsBool())
            {
                var annotation = Annotation.Find<CodeAttribute>(typeof(BoolFormat), Enum.GetName(typeof(BoolFormat), attribute.Format));
                return string.Format(annotation.Value, Convert.ToInt32(value));
            }
            if (type.IsEnumerable())
            {
                var values = ((IEnumerable) value).Cast<object>() ?? Enumerable.Empty<object>();
                return string.Join(attribute.ManySeparator, values.Select(x => this.GetValue(attribute, x)));
            }
            return value.ToString();
        }
        
        #endregion

        #region IHtmlString Members.

        /// <inheritdoc />
        public string ToHtmlString()
        {
            var builder = new StringBuilder();
            this.Build(Position.Before).Each(x => { builder.Append(x.ToHtmlString()); });
            builder.Append(this.Builder.ToString());
            this.Build(Position.After ).Each(x => { builder.Append(x.ToHtmlString()); });
            return builder.ToString();
        }

        private IEnumerable<IHtmlElement> Build(Position position)
        {
            return this.elements
                .Where(x => x.Key == position)
                .Select(x => x.Value.Select(y => y))
                .FirstOrDefault() ?? Enumerable.Empty<IHtmlElement>();
        }

        #endregion

        #region INotifyPropertyChanged Members.

        /// <summary>
        /// Declare the property change event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    internal static class TypeHelperExtensions
    {
        public static bool IsBool(this Type type)
        {
            return type == typeof(bool);
        }
        public static bool IsEnumerable(this Type type)
        {
            return 
                typeof(IEnumerable).IsAssignableFrom(type) && 
                typeof(string) != type;
        }

    }
}