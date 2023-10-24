using Squeal_DL.InterfaceofRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squeal_DL.ImplementationOfManagers
{
    public class TivitMediaRepo : Repository<TivitMedia, long>, ITivitMediaRepo
    {
        public TivitMediaRepo(MyContext context) : base(context)
        {

        }
    }
}
