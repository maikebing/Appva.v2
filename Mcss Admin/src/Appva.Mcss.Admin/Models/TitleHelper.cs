using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using System.Web.Mvc;
using Appva.Core.Extensions;
using Appva.Core.Resources;

namespace Appva.Mcss.Web
{
    public class TitleHelper
    {
        public static List<SelectListItem> SelectList(IList<Role> roles)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Role role in roles)
            {
                list.Add(new SelectListItem()
                {
                    Text = role.Name,
                    Value = role.Id.ToString()
                });
            }
            return list;
        }

        //GetNameByMatch
        public static String GetTitle(IList<Role> roles)
        {
            if (roles.Any(r => r.MachineName.Equals(RoleTypes.AdministrativePersonnel)))
            {
                return roles.Single(r => r.MachineName.Equals(RoleTypes.AdministrativePersonnel)).Name;
            }
            else if (roles.Any(r => r.MachineName.StartsWith(RoleTypes.TitlePrefix)))
            {
                return roles.Single(r => r.MachineName.StartsWith(RoleTypes.TitlePrefix)).Name;
            }
            else
            {
                return "Undersköterska";
            }
        }

        public static String GetTitleID(IList<Role> roles, Role stdRole)
        {
            if (roles.Any(x => x.MachineName.StartsWith(RoleTypes.AdministrativePersonnel)))
            {
                return roles.Single(x => x.MachineName.StartsWith(RoleTypes.AdministrativePersonnel)).Id.ToString();
            }
            else if (roles.Any(r => r.MachineName.StartsWith(RoleTypes.TitlePrefix)))
            {
                return roles.Single(r => r.MachineName.StartsWith(RoleTypes.TitlePrefix)).Id.ToString();
            }
            else
            {
                return (stdRole.IsNotNull()) ? stdRole.Id.ToString() : string.Empty;
            }
        }
    }
}