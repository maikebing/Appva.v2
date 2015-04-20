using System;
using System.Web.Mvc;

namespace Appva.Mvc.Html.DataAnnotations
{
    
    public class PlaceHolderAttribute : Attribute, IMetadataAware {
        
        readonly string _placeholder;
        
        public PlaceHolderAttribute(string placeholder) {
            _placeholder = placeholder;
        }

        public void OnMetadataCreated(ModelMetadata metadata) {
            metadata.AdditionalValues["placeholder"] = _placeholder;
        }

    }

}
