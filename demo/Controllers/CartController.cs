using demo.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;

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

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;

            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }



        // trả về một đối tượng cartModel trong ajax
        public JsonResult Update(string cartModel)
        {
            // deser convert string -> json  
            // ser json  -> string

            // đang là 1 string json đã thay đổi
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status = true
            });
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
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile,string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            var id = new OrderDao().Insert(order);
            var cart = (List <CartItem>) Session[CartSession];
            var detailDao = new OrderDetailDao();

            decimal total = 0;

            try
            {
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Product.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                // toEmail cho quản trị
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                // gửi back up cho dray
                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop dành cho khách hàng", content);
                // gửi cho người gửi
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop dành cho shop quản trị được setting in webconfig", content);
            }
            catch (Exception)
            {
                //ghi log
                return Redirect("/loi-thanh-toan"); 
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {

            return View();
        }
    }   
}