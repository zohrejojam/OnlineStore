using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
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
}