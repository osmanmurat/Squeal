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
    public class TivitMediaManager : Manager<TivitMediaDTO, TivitMedia, long>, ITivitMediaManager
    {

        public TivitMediaManager(ITivitMediaRepo repo, IMapper mapper) :
        base(repo, mapper, new string[] { "UserTivit" })
        {

        }
    }
}
