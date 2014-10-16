// <copyright file="OkPagedResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Results
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    #endregion

    /// <summary>
    /// The pagination info is included in the Link header. 
    /// It is important to follow these Link header values instead of constructing your own URLs.
    /// </summary>
    /// <typeparam name="T">The result type</typeparam>
    public sealed class OkPagedResult<T> : NegotiatedContentResult<T> where T : class
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ApiController"/>.
        /// </summary>
        private readonly ApiController controller;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OkPagedResult{T}"/> class.
        /// </summary>
        /// <param name="controller">The <see cref="ApiController"/></param>
        /// <param name="content">The content</param>
        public OkPagedResult(ApiController controller, T content)
            : base(HttpStatusCode.OK, content, controller)
        {
            this.controller = controller;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The route name.
        /// </summary>
        public string RouteName 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The page number.
        /// </summary>
        public long Page
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The results to show per page.
        /// </summary>
        public long PerPage 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The total count.
        /// </summary>
        public long TotalResult 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Returns the controller.
        /// </summary>
        public ApiController Controller 
        {
            get
            {
                return this.controller;
            } 
        }

        #endregion

        #region NegotiatedContentResult{T} Overrides.

        /// <inheritdoc />
        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await base.ExecuteAsync(cancellationToken);
            this.BuildPagingLinks(response);
            return response;
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Builds the header paging links.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage"/></param>
        private void BuildPagingLinks(HttpResponseMessage response)
        {
            var page         = this.Page;
            var next         = this.Page + 1;
            var prev         = this.Page - 1;
            var totalPages   = (int) Math.Ceiling((double) this.TotalResult / this.PerPage);
            var rules = new List<PagingLinkRule>()
            {
                new PagingLinkRule("first", page > 0, 1),
                new PagingLinkRule("next",  page < totalPages - 1, next),
                new PagingLinkRule("prev",  page > 1, prev),
                new PagingLinkRule("last",  page != totalPages, totalPages)
            };
            var links = (from rule in rules
                where rule.Condition
                select string.Format(
                    this.Controller.Url.Link(
                        this.RouteName, 
                        new
                        {
                            page = rule.Page, per_page = this.PerPage
                        }), 
                    rule.Relation)).ToList();
            if (links.Count > 0)
            {
                response.Headers.Add("Link", string.Join(", ", links.ToArray()));
            }
            response.Headers.Add("X-Total-Count", this.TotalResult.ToString());
        }

        /// <summary>
        /// The paging links rules.
        /// </summary>
        private class PagingLinkRule
        {
            #region Variables.

            /// <summary>
            /// The relation.
            /// </summary>
            private readonly string relation;

            /// <summary>
            /// The condition.
            /// </summary>
            private readonly bool condition;
            
            /// <summary>
            /// The page.
            /// </summary>
            private readonly long page;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="PagingLinkRule"/> class.
            /// </summary>
            /// <param name="relation">The relation</param>
            /// <param name="condition">The condition</param>
            /// <param name="page">The page</param>
            public PagingLinkRule(string relation, bool condition, long page)
            {
                this.condition = condition;
                this.relation = relation;
                this.page = page;
            }

            #endregion

            #region Public Properties.

            /// <summary>
            /// Returns the condition.
            /// </summary>
            public bool Condition
            { 
                get
                {
                    return this.condition;
                }
            }

            /// <summary>
            /// Returns the relation.
            /// </summary>
            public string Relation
            {
                get
                {
                    return this.relation;
                } 
            }

            /// <summary>
            /// Returns the page.
            /// </summary>
            public long Page
            {
                get
                {
                    return this.page;
                }
            }

            #endregion
        }

        #endregion
    }
}