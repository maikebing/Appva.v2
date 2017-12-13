// <copyright file="Post.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Html.Infrastructure;
    using Appva.Html.Infrastructure.Internal;
    //using ExpressionHelper = Microsoft.Web.Mvc.Internal.ExpressionHelper;

    #endregion

    /// PUT etc extra <input type="hidden" name="_method" value="PUT"> 

    // flowcontent borde ha abstract Begin och abstract End
    // Post is a Form,
    // Form has all attributes 
    public abstract class Form<T> : AuthorizedBlock<T>, IForm<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Tag Tag = Tag.New("form");

        private static IReadOnlyDictionary<string, string> Default = new Dictionary<string, string>()
        {
            { "accept-charset", FormCharset.Utf8.ToString() },
            { "role",           "form" }
        };

        private readonly IRoute route;

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Form{T}"/> class.
        /// </summary>
        protected Form(HtmlHelper helper, IRoute route)
            : base(route, helper, Tag)
        {
            Argument.Guard.NotNull("route", route);
            this.route = route;
            this.Initialize();
            /*
            IDictionary<string, object> htmlAttributes;
            this.Builder.MergeAttributes(htmlAttributes);
            string formAction = helper.BuildUrlFromExpression(action);
            this.Builder.MergeAttribute("action", formAction);
            this.Builder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method));
            //this.Html.HttpMethodOverride()
            //TagBuilder.CreateSanitizedId()
            this.Html.BuildUrlFromExpression();
            this.Name(this.CreateName());
            */
        }

        #endregion

        protected override void OnBeforeBegin()
        {
            this.Html.ViewContext.FormContext = new FormContext();
            this.Builder.MergeAttribute("method", "get");
            this.Builder.MergeAttribute("action", route.Url);
            this.Builder.MergeAttribute("accept-charset", "utf-8");
            this.Builder.MergeAttribute("role", "form");
            this.Builder.AddCssClass("form");
        }
        protected override void OnBeforeEnd()
        {
            this.Html.ViewContext.Writer.Write(this.Html.AntiForgeryToken());
        }

        protected override void OnEnd()
        {
            this.Html.ViewContext.OutputClientValidation();
            this.Html.ViewContext.FormContext = null;
        }

        #region IFormAttributes<T> Members.

        public IForm<T> Charset(params FormCharset[] charsets)
        {
            var charset = charsets == null || charsets.Length == 0 ? FormCharset.Unknown.AsString() : charsets.AsString();
            this.Builder.MergeAttribute("accept-charset", charset);
            return this;
        }

        public IForm<T> Autocomplete(bool isEnabled)
        {
            this.Builder.MergeAttribute("autocomplete", isEnabled ? "on" : "off");
            return this;
        }

        public IForm<T> Encoding(FormEncoding encoding = FormEncoding.FormUrlEncoded)
        {
            this.Builder.MergeAttribute("enctype", encoding.AsString());
            return this;
        }

        public IForm<T> Name(string name)
        {
            this.Builder.MergeAttribute("name", name, true);
            return this;
        }

        public IForm<T> DisableValidation()
        {
            this.Builder.MergeAttribute("novalidation", "");
            return this;
        }

        public IForm<T> Target(FormTarget target = FormTarget.Self)
        {
            this.Builder.MergeAttribute("target", target.AsString());
            return this;
        }

        #endregion

        #region Private Methods.

        private string CreateName()
        {
            /*var name = string.Join(
                "-",
                controller.ToLowerInvariant().Replace("controller", string.Empty),
                action.ToLowerInvariant());*/
            return "";
        }

        #endregion
    }
}