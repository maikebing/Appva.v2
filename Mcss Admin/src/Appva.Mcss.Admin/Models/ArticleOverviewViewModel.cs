using System;
using System.Collections.Generic;
using Appva.Mcss.Application.Models;

namespace Appva.Mcss.Web.ViewModels
{
    public class ArticleOverviewViewModel
    {
        public IList<ArticleModel> OrderedArticles { get; set; }
        public IDictionary<string, string> OrderOptions { get; set; }
        public Guid UserId { get; set; }
    }
}