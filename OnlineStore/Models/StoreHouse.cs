using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class StoreHouse
    {
        [Key]
        public int StoreHouseId { get; set; }

        [Required]
        public int MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [Required]
        [Display(Name = "تعداد")]
        [DisplayName("تعداد")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "تاریخ")]
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "شماره فاکتور")]
        [DisplayName("شماره فاکتور")]
        [MaxLength(100)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string InputInvoiceNumber { get; set; }
    }
}