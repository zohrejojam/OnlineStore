using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
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