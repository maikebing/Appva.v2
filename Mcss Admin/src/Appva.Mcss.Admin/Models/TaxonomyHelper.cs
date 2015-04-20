using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using System.Web.Mvc;
using Appva.Mcss.Admin.Application.Models;
namespace Appva.Mcss.Web {
    
    public class TaxonomyHelper {

        public static List<TaxonViewModel> SelectList(IList<ITaxon> taxonomy) {
            return SelectList(null, taxonomy);
        }

        public static List<TaxonViewModel> SelectList(Taxon taxon, IList<ITaxon> taxonomy)
        {
            var retval = new List<TaxonViewModel>();
            if (taxon.IsNull())
            {
                //taxon = taxonomy.Where(x => x.IsRoot).SingleOrDefault();
            }
            if (taxon.IsNotNull()) {
                var paths = taxon.Path.Split('.').Reverse().ToList();
                var selected = taxon.Id.ToString();
                foreach (var value in paths) {
                    var label = string.Empty;
                    var items = new List<SelectListItem>();
                    foreach (var node in taxonomy) {
                        if (node.ParentId.HasValue && node.ParentId.ToString().Equals(value)) {
                            label = (node.Type.IsNotEmpty()) ? node.Type : string.Empty;
                            items.Add(new SelectListItem() { Text = node.Name, Value = node.Id.ToString() });
                        }
                    }
                    if (items.Count > 0) {
                        retval.Add(new TaxonViewModel() {
                            Id = value,
                            Selected = selected,
                            Label = label,
                            OptionLabel = label.ToLower(),
                            Taxons = items
                        });
                    }
                    selected = value;
                }
                retval.Reverse();
            }
            return retval;
        }

        
        public static Guid FindTaxon(FormCollection collection, IList<Guid> taxons) {
            Guid guid = Guid.Empty;
            Guid retval = Guid.Empty;
            foreach (var key in collection.AllKeys) {
                if (Guid.TryParse(collection[key], out guid)) {
                    if (taxons.Contains(guid)) {
                        retval = guid;
                    }
                }
            }
            return retval;
        }

        public static IList<Guid> GetGuid(FormCollection collection) {
            IList<Guid> retval = new List<Guid>();
            if (collection.AllKeys.Length > 0) {
                Guid guid = Guid.Empty;
                for (var i = collection.AllKeys.Length - 1; i > -1; i--) {
                    var test = collection[i];
                    if (Guid.TryParse(test, out guid)) {
                        retval.Add(guid);
                        break;
                    }
                }
            }
            return retval;
        }

    }
}