

namespace Appva.Mcss.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    //using System.Web.Mvc.Html; /// all extensions

    /// 1. Layout -> grids
    /// 2. Forms -> forms
    /// 3. Elements ->

    /// Terminologi : https://www.w3.org/TR/css-grid-1/#grid-concepts
    ///
    /// using (Html.Forms().Action().Controller().) {
    ///     using (var grid = Html.Grid()) {
    ///         using (grid.Item(xs: 24, s: 12, m:8, l:6, xl:6)) {
    ///             Html.Forms.NumberedHeader().Heading(...).Description(...).Build();
    ///             Html.Forms.TextArea().Required().Build();
    ///         }
    ///     }
    /// }
    /// using (Html.Section().Bubble().AddVerticalSpace().AddHorizontalSpace()) {
    ///     ...
    /// }
    /// 
    /*
    public static class TemporaryHtmlExtensions
    {
        public static IGrid Grid(this HtmlHelper htmlHelper, Alignment alignment = Alignment.Default)
        {
            return new Grid(htmlHelper, alignment);
        }

        public static void Test(this HtmlHelper htmlHelper)
        {
            var Html = htmlHelper.Grid().AlignBottom().Flowing();
            //Html.Elements().
        }
    }

    public static class ElementExtensions
    {
        public static void Elements(this HtmlHelper htmlHelper)
        {
            
        }
    }

    

    public class Element
    {
        /// <summary>
        /// The <see cref="HtmlHelper"/>.
        /// </summary>
        private readonly HtmlHelper htmlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="viewContext">The <see cref="ViewContext"/>.</param>
        public Element(HtmlHelper htmlHelper) 
        {
            this.htmlHelper = htmlHelper;
        }
    }

    public class CompoundElement
    {

    }

    public class Container
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class Section : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> class.
        /// </summary>
        /// <param name="viewContext">The <see cref="ViewContext"/>.</param>
        public Section(ViewContext viewContext) 
            : base(viewContext)
        {
        }
    }

    /// <summary>
    /// Represents an HTML component element in an MVC view.
    /// </summary>
    public abstract class Component : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ViewContext viewContext;

        /// <summary>
        /// 
        /// </summary>
        private bool isDisposed;

        private readonly TagBuilder tagBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        /// <param name="viewContext">The <see cref="ViewContext"/>.</param>
        protected Component(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }
            this.viewContext = viewContext;
            this.tagBuilder = new TagBuilder("div");
        }

        /// <summary>
        /// Ends the form and disposes of all form resources.
        /// </summary>
        public void EndForm()
        {
            this.Dispose(true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and, optionally, managed resources used by the current 
        /// instance of the System.Web.Mvc.Html.MvcForm class.
        /// </summary>
        /// <param name="disposing">
        /// True to release both managed and unmanaged resources; false to release only 
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (! this.isDisposed)
            {
                this.isDisposed = true;
                viewContext.Writer.Write("</form>");
                FormExtensions.EndForm(viewContext);
            }
        }
    }*/
}