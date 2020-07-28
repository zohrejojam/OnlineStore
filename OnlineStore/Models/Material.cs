using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace OnlineStore.Models
{
    public class Material
    {
        [Key]
        [Required]
        public int MaterialId { get; set; }
        [Required]
        [Display(Name = "کد کالا")]
        [DisplayName("کد کالا")]
        [MaxLength(200)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MaterialCode { get; set; }
        [Required]
        [Display(Name = "عنوان کالا")]
        [DisplayName("عنوان کالا")]
        [MaxLength(200)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MaterialTitle { get; set; }
        [Required]
        [Display(Name = "گروه کالا")]
        [DisplayName("گروه کالا")]
        public int MaterialGroupId { get; set; }
        [ForeignKey("MaterialGroupId")]
        public virtual MaterialGroup MaterialGroup { get; set; }
        [Required]
        [Display(Name = "حداقل موجودی")]
        [DisplayName("حداقل موجودی")]
        public int MinInventory { get; set; }
    }
}