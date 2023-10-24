using Squeal_DL.InterfaceofRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squeal_DL.ImplementationOfManagers
{
    public class UserTivitRepo : Repository<UserTivit, long>, IUserTivitRepo
    {
        public UserTivitRepo(MyContext context) : base(context)
        {

        }
    }
}
