// <copyright file="CultureHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CultureHandler : DelegatingHandler
    {
        #region Variables.

        /// <summary>
        /// The supported cultures.
        /// </summary>
        private readonly ISet<string> supportedCultures;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureHandler"/> class.
        /// </summary>
        /// <param name="supportedCultures">The supported cultures</param>
        public CultureHandler(ISet<string> supportedCultures = null)
        {
            this.supportedCultures = supportedCultures ?? new HashSet<string> { "en-us", "en", "sv-se", "sv" };
        }

        #endregion

        #region DelegatingHandler Overrides.

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var list = request.Headers.AcceptLanguage;
            if (list.IsNull() || list.Count.Equals(0))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var headerValue = list.OrderByDescending(x => x.Quality ?? 1.0D)
                .Where(x => !x.Quality.HasValue || x.Quality.Value > 0.0D)
                .FirstOrDefault(x => this.supportedCultures.Contains(x.Value, StringComparer.OrdinalIgnoreCase));
            if (headerValue.IsNotNull())
            {
                this.SetCurrentCulture(headerValue.Value);
            }
            else
            {
                if (list.Any(x => x.Value == "*" && (!x.Quality.HasValue || x.Quality.Value > 0.0D)))
                {
                    var culture = this.supportedCultures.FirstOrDefault(sc => !list.Any(x => x.Value.Equals(sc, StringComparison.OrdinalIgnoreCase) &&
                        x.Quality.HasValue &&
                        Math.Abs(x.Quality.Value).Equals(0)));
                    if (culture.IsNotEmpty())
                    {
                        this.SetCurrentCulture(culture);
                    }
                }
            }
            //// TODO: If not satisfied return 406 http://tools.ietf.org/html/rfc7231#section-6.5.6
            return await base.SendAsync(request, cancellationToken);
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Sets the current culture.
        /// </summary>
        /// <param name="culture">The culture to be set</param>
        private void SetCurrentCulture(string culture)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
        }

        #endregion
    }
}