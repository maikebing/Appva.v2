namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    public class Scale : AggregateRoot
    {
        public Scale()
        {
        }

        public enum Type : byte
        {
            none,
            weight,
            height,
            glukos,
            pulse,
            preassure,
            bristol
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Unit { get; set; }
        public virtual Type ScaleType { get; set; }
        public virtual string Values { get; set; }
    }
}
