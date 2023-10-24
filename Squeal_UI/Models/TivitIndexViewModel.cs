using Squeal_EL.IdentityModels;
using Squeal_EL.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Squeal_UI.Models
{
    public class TivitIndexViewModel
    {
        public long Id { get; set; }

        public DateTime InsertedDate { get; set; }

        [StringLength(500)]
        [MinLength(2)]
        public string Tivit { get; set; }

        public string UserId { get; set; }

        public bool IsDefaultTivit { get; set; } // ekstra

        public bool IsDeleted { get; set; }

        public AppUser? AppUser { get; set; }

        public List<IFormFile>? TivitPictures { get; set; }

        public List<TivitMediaDTO>? TivitPhotos { get; set; }

    }
}
