using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations;
namespace Squeal_EL.ViewModels
{
    public class UserTivitDTO
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
    }
}