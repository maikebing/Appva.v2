using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using NHibernate;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Persistence;
using System.Runtime.Caching;
using Appva.Caching.Providers;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Models;
using Appva.Core.Extensions;

namespace Appva.Mcss.Web.Mappers {

    public class PatientMapper {

        public static readonly IRuntimeMemoryCache RTC = new RuntimeMemoryCache("test");
    }
}