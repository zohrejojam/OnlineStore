using System;
using System.Linq;
using OnlineStoreCore.IServices;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using OnlineStoreCore.Resources;

namespace OnlineStoreCore.Services
{
    public class ProductService : IMaterialService
    {
        private readonly DataBaseContext DbContext;

        public ProductService(DataBaseContext context)
        {
            DbContext = context;
        }

        public void Add(Product material)
        {
            try
            {
                #region Uniqe Material Code
                bool isExistCode = this.IsUniqueCode(material.Code);
                if (isExistCode)
                {
                    throw new Exception(Messages.MaterialCodeMustBeUniqe);
                }
                #endregion

                #region Uniqe Material Title In Group
                bool isExistTitleInGroup = this.IsUniqueTitleInGroup(material.Title, material.GroupId);
                if (isExistTitleInGroup)
                {
                    throw new Exception(Messages.MaterialTitleInGroupMustBeUniqe);
                }
                #endregion

                DbContext.Products.Add(material);
                DbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }

        public IQueryable<Product> Get()
        {
            return DbContext.Products;
        }

        #region Unique MaterialCode and TiltleInGroup
        public bool IsUniqueCode(string code)
        {
            //یکتا بودن کد کالا
            return DbContext.Products.Any(p => p.Code == code);
        }

        public bool IsUniqueTitleInGroup(string title, int groupId)
        {
            //یکتا بودن عنوان کالا در هر گروه
            return DbContext.Products.Any(p => p.GroupId == groupId &&
                                                p.Title == title);
        }
        #endregion
    }
}
