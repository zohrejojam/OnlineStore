using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class SalesInvoice
    {
        [Key]
        [Required]
        public int SalesInvoiceId { get; set; }

        [Required]
        public int MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [Required]
        [Display(Name = "تعداد")]
        [DisplayName("تعداد")]
        public long Number { get; set; }

        [Required]
        [Display(Name = "تاریخ")]
        [DisplayName("تاریخ")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "شماره فاکتور فروش")]
        [DisplayName("شماره فاکتور فروش")]
        [MaxLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string OutputInvoiceNumber { get; set; }

        [Required]
        [Display(Name = "نام مشتری")]
        [DisplayName("نام مشتری")]
        [MaxLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CustomerName { get; set; }


        [Required]
        [Display(Name = "مبلغ")]
        [DisplayName("مبلغ")]
        public decimal Amount { get; set; }
    }
}