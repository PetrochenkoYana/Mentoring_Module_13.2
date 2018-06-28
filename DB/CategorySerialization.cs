using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.DB
{
    public partial class Category
    {
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            var dbcontext = (context.Context as IObjectContextAdapter).ObjectContext;
           
                dbcontext.LoadProperty(this, c => c.Products);
            
        }
    }
}
