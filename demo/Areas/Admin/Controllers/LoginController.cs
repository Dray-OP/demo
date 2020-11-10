using demo.Areas.Admin.Models;
using demo.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        // kiểm tra đăng nhập
        public ActionResult Login(LoginModel model)
        {
            // điều kiện của valitation đúng thì xét login
            if (ModelState.IsValid)
            {
                // khởi tạo dao và so sánh dao trong login
                var dao = new UserDao();
                //chuyền dữ liều vào so sánh
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1 )
                {
                    // lấy dữ liệu user thông qua tên
                    var user = dao.GetUserID(model.UserName);

                    //tạo và thêm session cho user
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserId = user.ID;
                    Session.Add(CommonConstrants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }else if (result == 0) 
                {
                    ModelState.AddModelError("", "tài khoản không tồn tại");
                }else if(result == -1)
                {
                    ModelState.AddModelError("", "tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không thành công");
                }
            }
            return View("Index");
        }
    }
}