using Squeal_EL.Entities;
using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class UserTivit : BaseNumeric<long>
{
    [StringLength(500)]
    [MinLength(2)]
    public string Tivit { get; set; }

    public string UserId { get; set; }

    public bool IsDefaultTivit { get; set; } // ekstra

    public bool IsDeleted { get; set; }


    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }
}
