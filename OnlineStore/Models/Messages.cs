using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class Messages
    {
        public const string MaterialCodeMustBeUniqe = "کد کالا باید یکتا باشد";
        public const string MaterialTitleInGroupMustBeUniqe = "عنوان کالا در هر گروه کالا باید یکتا باشد";
        public const string MaterialNumberMustBeMoreThanMinInventory = "تعداد کالا باید در محدوده مجاز تعریف شده ی کالا باشد";
        public const string ErrorOccured = "خطا در ذخیره داده";
    }
}