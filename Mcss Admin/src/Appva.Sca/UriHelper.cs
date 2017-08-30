using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Sca
{
    internal static class UriHelper
    {
        private const string residentUrl = "api/resident/";
        internal const string ManualEventUrl = "api/manualevent/";
        internal const string TokenUrl = "api/token/";

        internal static string ResidentUrl(string id)
        {
            return $"{residentUrl}{id}";
        }
    }
}
