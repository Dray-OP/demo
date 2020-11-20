using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        /// <summary>
        /// list Feature Product by top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot < DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListRelatedProducts(long productId)
        {
            var product = new Product();
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).ToList();
        }

        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<Product> ListByCategoryId(long categoryID,ref int totalRecord ,int pageIndex = 1, int pageSize = 2)
        {

            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            // bỏ qua 2 bản ghi và lấy 2 bản ghi
            var model = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
