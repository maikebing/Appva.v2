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

        public static IList<TaxonViewModel> CreateItems(Account account, ITaxon selected, IList<ITaxon> organization)
        {
            var retval = new List<TaxonViewModel>();
            var view   = new TaxonViewModel();
            retval.Add(view);
            foreach (var location in account.Locations.OrderByDescending(x => x.Sort))
            {
                var taxon      = organization.Where(x => x.Id == location.Taxon.Id).SingleOrDefault();
                var isSelected = selected == null ? false : (taxon.Id == selected.Id || selected.Path.ToLowerInvariant().Contains(taxon.Id.ToString().ToLowerInvariant()));
                view.Taxons.Add(new SelectListItem
                {
                    Selected = isSelected,
                    Text     = taxon.Name,
                    Value    = taxon.Id.ToString()
                });
                view.Id = "root-" + (taxon.Parent != null ? taxon.Parent.Id.ToString() : taxon.Id.ToString());
                view.Selected = isSelected ? taxon.Id.ToString() : null;
                if (isSelected)
                {
                    GetParents(organization, taxon, selected, retval);
                }
            }
            return retval;
        }

        private static void GetParents(IList<ITaxon> organization, ITaxon taxon, ITaxon selected, List<TaxonViewModel> model)
        {
            if (taxon == null || selected == null)
            {
                return;
            }
            var units = organization.Where(x => x.Parent != null && x.Parent.Id == taxon.Id)
                .OrderBy(x => x.Sort).ToList();
            if (units.Count == 0)
            {
                return;
            }
            var items = new List<SelectListItem>();
            var view = new TaxonViewModel();
            ITaxon newSelected = null;
            foreach (var unit in units)
            {
                var isSelected = unit.Id == selected.Id || selected.Path.ToLowerInvariant().Contains(unit.Id.ToString().ToLowerInvariant());
                if (isSelected)
                {
                    newSelected = unit;
                }
                items.Add(new SelectListItem
                {
                    Selected = isSelected,
                    Text = unit.Name,
                    Value = unit.Id.ToString()
                });
            }
            var sel = items.Where(x => x.Selected).SingleOrDefault();
            model.Add(new TaxonViewModel
            {
                Id = taxon.Id.ToString(),
                Selected = sel == null ? null : sel.Value,
                Label = taxon.Name,
                Taxons = items
            });
            if (sel == null)
            {
                return;
            }
            GetParents(organization, newSelected, selected, model);
        }

        public static IList<TaxonViewModel> SelectList(IList<ITaxon> taxonomy) 
        {
            return SelectList(null, taxonomy);
        }

        public static IList<TaxonViewModel> SelectList(Taxon taxon, IList<ITaxon> taxonomy)
        {
            var retval   = new List<TaxonViewModel>();
            var taxonId  = taxon.IsNull() ? taxonomy.Where(x => x.IsRoot).SingleOrDefault().Id   : taxon.Id;
            var path     = taxon.IsNull() ? taxonomy.Where(x => x.IsRoot).SingleOrDefault().Path : taxon.Path;
            var paths    = path.ToLowerInvariant().Split('.').Reverse().ToList();
            var selected = taxonId;
            foreach (var value in paths) 
            {
                var label  = string.Empty;
                var items  = new List<SelectListItem>();
                var pathId = new Guid(value);
                foreach (var node in taxonomy) 
                {
                    if (node.ParentId.HasValue && node.ParentId == pathId) 
                    {
                        label = node.Type.IsNotEmpty() ? node.Type : string.Empty;
                        items.Add(new SelectListItem { Text = node.Name, Value = node.Id.ToString() });
                    }
                }
                if (items.Count > 0) 
                {
                    retval.Add(new TaxonViewModel 
                    {
                        Id          = pathId.ToString(),
                        Selected    = selected.ToString(),
                        Label       = label,
                        OptionLabel = label.ToLower(),
                        Taxons      = items
                    });
                }
                selected = pathId;
            }
            retval.Reverse();
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