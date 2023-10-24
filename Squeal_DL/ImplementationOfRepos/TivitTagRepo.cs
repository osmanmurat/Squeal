using Squeal_DL.InterfaceofRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squeal_DL.ImplementationOfManagers
{
    public class TivitTagRepo : Repository<TivitTag, long>, ITivitTagRepo
    {
        public TivitTagRepo(MyContext context) : base(context)
        {

        }
    }
}
