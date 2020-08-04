using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreCore.IServices;
using OnlineStoreCore.DataLayer;
using Microsoft.EntityFrameworkCore;
using OnlineStoreCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreCore.Services
{
    public class MaterialService : IMaterialService
    {
        private DataBaseContext DbContext;
        public MaterialService(DataBaseContext context)
        {
            DbContext = context;
        }

        /// <summary>
        /// تعریف کالا
        /// </summary>
        /// <param name="material">کالا</param>
        /// <returns></returns>
        public void DefinitionMaterial(Material material)
        {
            try
            {
                //validation
                #region Uniqe Material Code
                bool isExistCode = this.IsUniqueCode(material.MaterialCode);
                if (isExistCode)
                {
                    throw new Exception(Messages.MaterialCodeMustBeUniqe);
                }
                #endregion

                #region Uniqe Material Title In Group
                bool isExistTitleInGroup = this.IsUniqueTitleInGroup(material.MaterialTitle, material.MaterialGroupId);
                if (isExistTitleInGroup)
                {
                    throw new Exception(Messages.MaterialTitleInGroupMustBeUniqe);
                }
                #endregion

                DbContext.Materials.Add(material);
                DbContext.SaveChanges();

            }
            catch (System.Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }


        // GET: api/Materials
        public IQueryable<Material> GetMaterials()
        {
            return DbContext.Materials;
        }

        #region Unique MaterialCode and TiltleInGroup
        public bool IsUniqueCode(string code)
        {
            //یکتا بودن کد کالا
            return DbContext.Materials.Any(p => p.MaterialCode == code);
        }

        public bool IsUniqueTitleInGroup(string title, int groupId)
        {
            //یکتا بودن عنوان کالا در هر گروه
            return DbContext.Materials.Any(p => p.MaterialGroupId == groupId &&
                                          p.MaterialTitle == title);
        }
        #endregion
    }
}
