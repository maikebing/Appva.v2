// <copyright file="InputExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    #endregion

    public static class OtherExtensions
    {
        public static ILabel Label<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression, string text = null)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata      = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var labelText     = text ?? metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            return new LabelElement(htmlHelper, labelText);
        }

        public static ITextArea TextArea<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var area = new TextAreaElement(htmlHelper, metadata.Model).Name(htmlFieldName);
            return area;
        }

        public static IButtonElement Button(this HtmlHelper htmlHelper, string text = null)
        {
            return new ButtonElement(htmlHelper, text);
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class InputExtensions
    {
        public static IButton InputButton(this HtmlHelper htmlHelper)
        {
            return new Button(htmlHelper);
        }

        public static ICheckbox Checkbox<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression, object value, bool isSelected = false)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var checkbox = SetMetadata(htmlHelper, new Checkbox(htmlHelper, true) as ICheckbox, expression);
            if (value != null && value.Equals(metadata.Model))
            {
                checkbox.Checked();
            }
            if (isSelected)
            {
                checkbox.Checked();
            }
            /*var hidden = new Hidden(htmlHelper).Name(ExpressionHelper.GetExpressionText(expression)).Value("false");
            hidden.On
            ((Checkbox) checkbox).AddElement(Position.Before, 
               );*/
            return checkbox;
        }

        public static IColor Color(this HtmlHelper htmlHelper)
        {
            return new Color(htmlHelper);
        }

        public static IDate Date(this HtmlHelper htmlHelper)
        {
            return new Date(htmlHelper);
        }

        public static IDateTimeLocal DateTimeLocal(this HtmlHelper htmlHelper)
        {
            return new DateTimeLocal(htmlHelper);
        }

        public static IEmail Email(this HtmlHelper htmlHelper)
        {
            return new Email(htmlHelper);
        }

        public static IFile File(this HtmlHelper htmlHelper)
        {
            return new File(htmlHelper);
        }

        public static IHidden Hidden(this HtmlHelper htmlHelper)
        {
            return new Hidden(htmlHelper);
        }

        public static IImage Image(this HtmlHelper htmlHelper, Uri location)
        {
            return new Image(location, htmlHelper);
        }

        public static IMonth Month(this HtmlHelper htmlHelper)
        {
            return new Month(htmlHelper);
        }

        public static INumber Number<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression)
        {
            var meta    = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var element = SetMetadata(htmlHelper, new Number(htmlHelper) as INumber, expression);
            return element;
        }

        public static IPassword Password(this HtmlHelper htmlHelper)
        {
            return new Password(htmlHelper);
        }

        public static IRadio Radio<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression, object value)
        {
            var meta  = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var radio = SetMetadata(htmlHelper, new Radio(htmlHelper) as IRadio, expression);
            if (value != null && value.ToString() == meta.Model.ToString())
            {
                radio.Checked();
            }
            radio.Value(value.ToString());
            return radio;
        }

        public static C SetMetadata<T, C, V>(HtmlHelper<T> htmlHelper, C element, Expression<Func<T, V>> expression) where C : IInputElement<C>
        {
            var metadata      = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            element.Name(htmlFieldName);
            if (metadata.Model != null && (element is INumber || element is IText))
            {
                element.Value(metadata.Model.ToString());
            }
            /*if (metadata.IsRequired)
            {
                element.Required();
            }
            if (metadata.IsReadOnly)
            {
                element.Readonly();
            }*/
            return element;
        }

        public static IRange Range(this HtmlHelper htmlHelper)
        {
            return new Range(htmlHelper);
        }

        public static IReset Reset(this HtmlHelper htmlHelper)
        {
            return new Reset(htmlHelper);
        }

        public static ISearch Search(this HtmlHelper htmlHelper)
        {
            return new Search(htmlHelper);
        }

        public static ISubmit Submit(this HtmlHelper htmlHelper)
        {
            return new Submit(htmlHelper);
        }

        public static ITel Tel(this HtmlHelper htmlHelper)
        {
            return new Tel(htmlHelper);
        }

        public static IText Text<T, V>(this HtmlHelper<T> htmlHelper, Expression<Func<T, V>> expression)
        {
            var meta = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var text = SetMetadata(htmlHelper, new Text(htmlHelper) as IText, expression);
            if (meta.Model != null)
            {
                text.Value(meta.Model.ToString());
            }
            return text;
        }

        public static ITime Time(this HtmlHelper htmlHelper)
        {
            return new Time(htmlHelper);
        }

        public static IUrl Url(this HtmlHelper htmlHelper)
        {
            return new Url(htmlHelper);
        }

        public static IWeek Week(this HtmlHelper htmlHelper)
        {
            return new Week(htmlHelper);
        }
    }
}