using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public ActionResult Category(long cateId, int page = 1, int pageSize = 2)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            // mỗi model chỉ có 1 category truyền vào để lấy tên
            ViewBag.Category = category;
            // list product
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(cateId,ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord; // sẽ nhận giá trị trong Dao
            ViewBag.Page = page;

            int maxPage = 5; // sau có thể cho phần cấu hình
            int totalPage = 0;
            totalPage = (int)Math.Ceiling(((Double)totalRecord / (Double)pageSize)); // tổng số trang

            ViewBag.TotalPage = totalPage;  
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;       // tổng số trang
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);  
        }
        public ActionResult Detail(long cateId)
        {
            var product = new ProductDao().ViewDetail(cateId);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducs = new ProductDao().ListRelatedProducts(cateId);
            return View(product);
        }
        
    }
}