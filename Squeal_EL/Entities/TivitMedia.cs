using Squeal_EL.Entities;
using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TivitMedia : BaseNumeric<long>
{
    public string MediaPath { get; set; }

    public long TivitId { get; set; } // FK

    [ForeignKey("TivitId")]
    public virtual UserTivit UserTivit { get; set; }
}
