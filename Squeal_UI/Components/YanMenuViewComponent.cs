using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Squeal_BL.InterfacesOfManagers;
using Squeal_EL.IdentityModels;
using Squeal_EL.ResultModels;
using Squeal_EL.ViewModels;
using Squeal_UI.Models;

namespace Squeal_UI.Components
{
    public class YanMenuViewComponent : ViewComponent
    {
        private readonly ITivitTagManager _tagManager;
        private readonly IMapper _mapper;

        public YanMenuViewComponent(ITivitTagManager tagManager, IMapper mapper)
        {
            _tagManager = tagManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var taglar = _tagManager.GetAll(x => !x.IsDeleted).Data.GroupBy(x => x.TagName).Select(group => group.First()).ToList();
                var model = _mapper.Map<List<TivitTagDTO>>(taglar);

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
