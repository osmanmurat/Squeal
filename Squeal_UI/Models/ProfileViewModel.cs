using System.ComponentModel.DataAnnotations;
using Squeal_EL.IdentityModels;

namespace Squeal_UI.Models
{
    public class ProfileViewModel : AppUser
    {
        public IFormFile? SelectedPhoto { get; set; }
    }
}
