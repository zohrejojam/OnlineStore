using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using OnlineStore.IServices;
using OnlineStore.DataLayer;

namespace OnlineStore.Models
{
    public class Material:IMaterial
    {
        [Key]
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


        #region Unique MaterialCode and TiltleInGroup
        private DataBaseContext db = new DataBaseContext();
        public bool IsUniqueMaterialCode(string materialCode)
        {
            //یکتا بودن کد کالا
          return  db.Materials.Any(p => p.MaterialCode == materialCode);
        }

        public bool IsUniqueMaterialTitleInGroup(string materialtitle,int materialGroupId)
        {
            //یکتا بودن عنوان کالا در هر گروه
           return db.Materials.Any(p => p.MaterialGroupId == materialGroupId &&
                                         p.MaterialTitle == materialtitle);
        }
        #endregion
    }
}