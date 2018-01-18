using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Application.Common
{
    public static class AdministrationDefaults
    {
        public enum AdministrationAmountType
        {
            None,
            Integer,
            Half,
            Quarter,
            Decimal,
            DoubleDecimal
        }

        public static List<AdministrationAmountModel> Units()
        {
            var grek = new List<AdministrationAmountModel>
            {
                new AdministrationAmountModel("Tabletter",  NonUnits.Tablets,        10, 0, 2, 0.25),
                new AdministrationAmountModel("Deciliter",  VolumeUnits.Deciliter,   10, 0, 1, 0.5),
                new AdministrationAmountModel("Centiliter", VolumeUnits.Centiliter,  50, 0, 1, 0.5),
                new AdministrationAmountModel("Milliliter", VolumeUnits.Milliliter,  50, 0, 0, 1),
                new AdministrationAmountModel("Gram",       MassUnits.Gram,          10, 0, 2, 0.25),
                new AdministrationAmountModel("Milligram",  MassUnits.Milligram,    100, 0, 0, 5)
            };
            return grek;
        }

        public static double GetStepFromValueType(AdministrationAmountType customValueType)
        {
            switch (customValueType)
            {
                case AdministrationAmountType.Integer: return 1.00;
                case AdministrationAmountType.Half: return 0.50;
                case AdministrationAmountType.Quarter: return 0.25;
                case AdministrationAmountType.Decimal: return 0.10;
                case AdministrationAmountType.DoubleDecimal: return 0.01;
                default: return 1.00;
            }
        }

        public static int GetFractionsFromValueType(AdministrationAmountType customValueType)
        {
            switch (customValueType)
            {
                case AdministrationAmountType.Integer: return 0;
                case AdministrationAmountType.Half: return 1;
                case AdministrationAmountType.Quarter: return 2;
                case AdministrationAmountType.Decimal: return 1;
                case AdministrationAmountType.DoubleDecimal: return 2;
                default: return 0;
            }
        }
    }
}
