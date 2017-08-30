using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Sca.Models
{
    internal class Token
    {
        internal string Value { get; private set; }
        internal DateTimeOffset Expires { get; private set; }

        internal bool IsValid
        {
            get { return Expires > DateTime.UtcNow; }
        }

        internal Token()
        {

        }

        // vill ta bort denna. Construct.
        internal Token(string value)
        {
            this.Value = value;
            this.Expires = DateTimeOffset.UtcNow;
        }

        internal Token(string value, DateTimeOffset expires)
        {
            this.Value = value;
            this.Expires = expires;
        }

        internal void SetExpires(DateTimeOffset expires)
        {
            this.Expires = expires;
        }

        internal void SetValues(string value, DateTimeOffset expires)
        {
            this.Value = value;
            this.Expires = expires;
        }
    }
}
