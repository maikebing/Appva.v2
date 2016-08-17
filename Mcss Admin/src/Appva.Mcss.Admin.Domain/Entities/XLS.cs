using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Mcss.Admin.Domain.Entities
{
    public class XLS : Resource
    {
        public XLS()
        {

        }

        private XLS(string name, byte[] file, string description)
        {
            this.Name = name;
            this.Data = file;
            this.Description = description;
        }
        
        public static XLS CreateNew(string name, byte[] file, string description)
        {
            return new XLS(name, file, description);
        }
    }
}
