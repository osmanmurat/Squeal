using AutoMapper;
using Squeal_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squeal_EL.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<UserTivit, UserTivitDTO>().ReverseMap();
            CreateMap<TivitTag, TivitTagDTO>().ReverseMap();
            CreateMap<TivitMedia, TivitMediaDTO>().ReverseMap();

        }
    }
}
