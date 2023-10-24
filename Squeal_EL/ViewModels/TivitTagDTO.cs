using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Squeal_EL.ViewModels
{
    public class TivitTagDTO
    {
        public long Id { get; set; }
        public DateTime InsertedDate { get; set; }

        [StringLength(50)]
        [MinLength(2)]
        public string TagName { get; set; }
        public long TivitId { get; set; }
        public bool IsDeleted { get; set; }
        public UserTivit? UserTivit { get; set; }

    }
}