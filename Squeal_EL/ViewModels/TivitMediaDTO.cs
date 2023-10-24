using Squeal_EL.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace Squeal_EL.ViewModels
{
    public class TivitMediaDTO
    {
        public long Id { get; set; }
        public DateTime InsertedDate { get; set; }

        public long TivitId { get; set; } // FK

        public string MediaPath { get; set; }
        public bool IsDeleted { get; set; }
        public UserTivit? UserTivit { get; set; }

    }
}