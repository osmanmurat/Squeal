using AutoMapper;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging;
using Squeal_BL.ImplementationOfManagers;
using Squeal_BL.InterfacesOfManagers;
using Squeal_EL.IdentityModels;
using Squeal_EL.ResultModels;
using Squeal_EL.ViewModels;
using Squeal_UI.Models;
using System.Diagnostics;
using System.Linq;

namespace Squeal_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserTivitManager _userTivitManager;
        private readonly ITivitMediaManager _mediaManager;
        private readonly ITivitTagManager _tagManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IUserTivitManager userTivitManager, ITivitMediaManager mediaManager, ITivitTagManager tagManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _userTivitManager = userTivitManager;
            _mediaManager = mediaManager;
            _tagManager = tagManager;
            _mapper = mapper;
        }

        

        public IActionResult Index(string Tagname)
        {
            var tag = _tagManager.GetAll(x => x.TagName == Tagname).Data.ToList();

            List<long> dizi = new List<long>();

            foreach (var itm in tag)
            {
                dizi.Add(itm.TivitId);
            }

            var tivits = _userTivitManager.GetAll(x => !x.IsDeleted).Data.OrderByDescending(x => x.InsertedDate).ToList();


            if (Tagname != null)
            {
                tivits = _userTivitManager.GetAll(x => !x.IsDeleted && dizi.Contains(x.Id)).Data.OrderByDescending(x => x.InsertedDate).ToList();
            }


            var model = _mapper.Map<List<TivitIndexViewModel>>(tivits);

            foreach (var tivit in model)
            {
                var media = _mediaManager.GetAll(x => x.TivitId == tivit.Id).Data;
                if (media.Count == 0)
                {
                    tivit.TivitPhotos = null;
                }
                else
                {
                    tivit.TivitPhotos = new List<TivitMediaDTO>();
                    foreach (var item in media)
                    {
                        tivit.TivitPhotos.Add(item);
                    }
                }
            }

            return View(model);
        }


            public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult TivitIndex()
        {
            //useridyi sayfaya gönderelim böylece adres eklemede useridyi metoda aktarabiliriz
            var username = User.Identity?.Name;
            var user = _userManager.FindByNameAsync(username).Result;
            TivitIndexViewModel model = new TivitIndexViewModel();
            model.TivitPictures = new List<IFormFile>();
            model.UserId = user.Id;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult TivitIndex(TivitIndexViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                    ModelState.AddModelError("", "Lütfen bilgileri eksiksiz giriniz!");
                    return View(model);
                }

                var tivit = _mapper.Map<UserTivitDTO>(model);
                tivit.InsertedDate = DateTime.Now;

                var result2 = _userTivitManager.Add(tivit);
                if (!result2.IsSuccess)
                {
                    _logger.LogError($"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                    ModelState.AddModelError("", "Tivit kaydedilemedi!");
                    return View(model);
                }

                //tivitteki tag arama algoritması

                var tivittext = tivit.Tivit;
                string[] words = tivittext.Split(' '); // Metni boşluklara göre ayır
                List<string> Taglar = new List<string>();

                bool tageklendimi = false;

                foreach (string word in words)
                {
                    if (word.StartsWith("#"))
                    {
                        TivitTagDTO tt = new TivitTagDTO()
                        {
                            InsertedDate = DateTime.Now,
                            TagName = word.Substring(1),
                            IsDeleted = false,
                            TivitId = result2.Data.Id
                        };
                        var result4 = _tagManager.Add(tt);
                        if (result4.IsSuccess) { tageklendimi = true; }
                    }
                }

                if (model.TivitPictures != null)
                {
                    foreach (var item in model.TivitPictures)
                    {
                        if (item.ContentType.Contains("image") && item.Length > 0)
                        {
                            string fileName = $"{item.FileName.Substring(0, item.FileName.IndexOf('.'))}-{Guid.NewGuid().ToString().Replace("-", "")}";

                            string uzanti = Path.GetExtension(item.FileName);

                            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/TivitPictures/{fileName}{uzanti}");

                            string directoryPath =
                               Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/TivitPictures/");

                            if (!Directory.Exists(directoryPath))
                                Directory.CreateDirectory(directoryPath);

                            using var stream = new FileStream(path, FileMode.Create);

                            item.CopyTo(stream);
                            TivitMediaDTO p = new TivitMediaDTO()
                            {
                                InsertedDate = DateTime.Now,
                                MediaPath = $"/TivitPictures/{fileName}{uzanti}",
                                TivitId = result2.Data.Id
                            };

                            var result1 = _mediaManager.Add(p);
                            if (!result1.IsSuccess)
                            {
                                _logger.LogError($"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                                ModelState.AddModelError("", "Tivit kaydedilemedi!");
                                return View(model);
                            }
                        }
                    }
                }

                TempData["TivitIndexSuccessMsg"] = "Tivit attınız!" + (tageklendimi ? " Tag eklendi" : "");

                _logger.LogInformation($"Home/TivitIndex atılan tivit: {JsonConvert.SerializeObject(model)}");
                return RedirectToAction("TivitIndex", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"HATA: Home/TivitIndex post model:{JsonConvert.SerializeObject(model)}");
                ModelState.AddModelError("", "Beklenmedik bir sorun oluştu!");
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}