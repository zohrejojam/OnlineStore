using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreCore.Models
{
    public class AccountingDocument
    {
        [Key]
        public int AccountingDocumentId { get; set; }

        [Required]
        [Display(Name = "تاریخ ثبت سند")]
        [DisplayName("تاریخ ثبت سند")]
        public DateTime DocumentDate { get; set; }

        [Required]
        [Display(Name = "شماره سند")]
        [DisplayName("شماره سند")]
        [MaxLength(100)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DocumentNumber { get; set; }

        //شماره فاکتور فروش
        public int SalesInvoiceId { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }

        [Required]
        [Display(Name = "مبلغ")]
        [DisplayName("مبلغ")]
        public decimal Amount { get; set; }

       
    }

    public class ListOfMaterial
    {
        public int MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialTitle { get; set; }
        public string MaterialGroupName { get; set; }
        public int MinInventory { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}