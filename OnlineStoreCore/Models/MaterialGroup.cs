using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreCore.Models
{
    public class MaterialGroup
    {
        [Key]
        public int MaterialGroupId { get; set; }

        [Required]
        [Display(Name ="گروه کالا")]
        [DisplayName("گروه کالا")]
        [MaxLength(200)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MaterialGroupName { get; set; }
    }
}