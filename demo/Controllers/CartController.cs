using demo.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace demo.Controllers
{
    public class CartController : Controller
    {
        // key để lưu cart session
        // const biến biến thành hằng số không thể thay đổi được
        private const string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        // trả về một đối tượng cartModel trong ajax
        public JsonResult Update(string cartModel)
        {
            // deser convert string -> json  
            // ser json  -> string

            // đang là 1 string json
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {

            }

            return View();
        }

        public ActionResult AddItem(long productID, int quantity)
        {
            var product = new ProductDao().ViewDetail(productID);
            var cart = Session[CartSession];
            if(cart != null)
            {
                // có rồi cộng thêm
                // khởi tạo lại cart trong session với kiểu dữ liệu là list
                var list = (List<CartItem>)cart;
                // nếu mà tồn tại thì cộng quantity
                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                // rồi gán lại vào session
                Session[CartSession] = list;
            }
            else
            {
                // không có giỏ hàng thì thêm mới vào
                // tạo mới đối tượng cart item
                var item = new CartItem();
                // = product tìm trong Dao ViewDetail(productID)
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                // gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}