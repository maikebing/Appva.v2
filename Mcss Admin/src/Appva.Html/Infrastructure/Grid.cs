// <copyright file="Grid.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /*
    /// 
    /// using(Html.Form<Controller>().Post(x => x.action).Classes("class1 class2").IgnoreNativeValidation())
    /// {
    ///     using(var grid = Html.Grid().Align().Top().Flowing().Begin()) 
    ///     {
    ///         using(grid.GridItem().Begin()) 
    ///         {
    ///             Html.FormControl(x => model).Label().Required();
    ///         }
    ///     }
    /// }
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    ///
    ///

    public enum Alignment
    {
        Default,
        Top,
        Middle,
        Bottom,
        Left,
        Right
    }
    public interface IAlignment<T> where T : class
    {
        T Top();


    }
    public class Alignment<T> : IAlignment<T> where T : class
    {
        private readonly T obj;
        public Alignment(T obj)
        {
            this.obj = obj;
        }
        public T Top()
        {

            return obj;
        }
    }
    public interface IGrid : IDisposable
    {
        IAlignment<IGrid> Align();
        IGrid Begin();
        IGrid AlignTop();
        IGrid AlignMiddle();
        IGrid AlignBottom();
        IGrid AlignRight();
        IGrid Vertical();
        IGrid InlineBlock();
        IGrid Flowing();
        IGrid DataColumns(string columns); 
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Grid : FlowContent, IGrid
    {
        private static readonly IList<string> ValidClasses = new List<string>
        {
            "grid--align-top",   "grid--align-middle", "grid--align-bottom", 
            "grid--align-right", "grid--vertical",     "grid--inline-block", 
            "grid--flowing"
        };
        /// grid--align-top
        /// grid--align-middle
        /// grid--align-bottom
        /// grid--align-right
        /// grid--vertical
        /// grid--inline-block
        /// grid--flowing
        /// attribute data-columns m:1, l:3
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid(HtmlHelper htmlHelper, Alignment alignment = Alignment.Default) 
            : base(htmlHelper, "div")
        {
            this.AddClass("grid");
            if (alignment != Alignment.Default)
            {
                this.AddClass("grid--align-" + Enum.GetName(typeof(Alignment), alignment).ToLowerInvariant());
            }
        }

        #endregion

        #region IGrid Members.

        public IAlignment<IGrid> Align()
        {
            return new Alignment<IGrid>(this);
        }

        /// <inheritdoc />
        public IGrid AlignTop()
        {
            this.AddClass("grid--align-top");
            return this;
        }

        /// <inheritdoc />
        public IGrid AlignMiddle()
        {
            this.AddClass("grid--align-middle");
            return this;
        }

        /// <inheritdoc />
        public IGrid AlignBottom()
        {
            this.AddClass("grid--align-bottom");
            return this;
        }

        /// <inheritdoc />
        public IGrid AlignRight()
        {
            this.AddClass("grid--align-right");
            return this;
        }

        /// <inheritdoc />
        public IGrid Vertical()
        {
            this.AddClass("grid--vertical");
            return this;
        }

        /// <inheritdoc />
        public IGrid InlineBlock()
        {
            this.AddClass("grid--inline-block");
            return this;
        }

        /// <inheritdoc />
        public IGrid Flowing()
        {
            this.AddClass("grid--flowing");
            return this;
        }

        /// <inheritdoc />
        public IGrid DataColumns(string columns)
        {
            return this;
        }

        public IGrid Begin()
        {
            base.Begin();
            return this;
        }

        #endregion
    }
     * */
}