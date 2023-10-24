using Squeal_EL.Entities;
using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TivitTag : BaseNumeric<long>
{
    [StringLength(50)]
    [MinLength(2)]
    public string TagName { get; set; }
    public long TivitId { get; set; }
    public bool IsDeleted { get; set; }

    [ForeignKey("TivitId")]
    public virtual UserTivit UserTivit { get; set; }
}
