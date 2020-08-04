using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OnlineStoreCore.Models
{
    public class SalesInvoice
    {
        [Key]
        public int SalesInvoiceId { get; set; }

        [Required]
        public int StoreHouseId { get; set; }
        [ForeignKey("StoreHouseId")]
        public virtual StoreHouse StoreHouse { get; set; }

        [Required]
        [Display(Name = "تعداد")]
        [DisplayName("تعداد")]
        public int Count { get; set; }

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