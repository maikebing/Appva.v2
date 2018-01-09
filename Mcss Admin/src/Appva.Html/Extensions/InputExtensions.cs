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
    using System.Linq.Expressions;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class InputExtensions
    {
        public static IButton Button(this HtmlHelper htmlHelper)
        {
            return new Button(htmlHelper);
        }

        public static ICheckbox Checkbox(this HtmlHelper htmlHelper)
        {
            return new Checkbox(htmlHelper);
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

        public static INumber Number(this HtmlHelper htmlHelper)
        {
            return new Number(htmlHelper);
        }

        public static IPassword Password(this HtmlHelper htmlHelper)
        {
            return new Password(htmlHelper);
        }

        public static IRadio Radio(this HtmlHelper htmlHelper)
        {
            return new Radio(htmlHelper);
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
            //Text(null, x => x.Something).Label("", LabelPosition.Wrap).Required()
            ExpressionHelper.GetExpressionText(expression);
            return new Text(htmlHelper);
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