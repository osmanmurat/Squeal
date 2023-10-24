using AutoMapper;
using Squeal_BL.InterfacesOfManagers;
using Squeal_DL.InterfaceofRepos;
using Squeal_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squeal_BL.ImplementationOfManagers
{
    public class UserTivitManager :
Manager<UserTivitDTO, UserTivit, long>, IUserTivitManager
    {

        public UserTivitManager(IUserTivitRepo repo, IMapper mapper) :
        base(repo, mapper, new string[] { "AppUser"})
        {

        }
    }
}
