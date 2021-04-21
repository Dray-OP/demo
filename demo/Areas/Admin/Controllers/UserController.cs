using demo.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace demo.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page =1,int pageSize=3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            // load trang nó vẫn nó vẫn còn ở input
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    // thông báo
                    SetAlert("Thêm user thành công", "success");
                    //chuyen ve trang quan ly
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm user thất bại", "error");
                    ModelState.AddModelError("","them user thất bại");
                }
            }
            return RedirectToAction("Create", "User");
        }
        [HttpGet]
        //[HttpPost]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)//user là cái người dùng truyền vào
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }
                //user.CreatedDate = DateTime.Now;
                // update trả về kiểu bool còn insert trả về kiểu int
                var rerult = dao.Update(user);
                if (rerult)
                {
                    SetAlert("Sửa user thành công", "success");
                    //chuyen ve trang quan ly
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "update user không thanh cong");
                }
            }
            return RedirectToAction("Edit", "User");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id) // cùng kiểu với user trong EF
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }
        [HttpPost]
        public JsonResult FindEmpoyee(int id) // cùng kiểu với user trong EF
        {
            var result = new UserDao().ViewDetail(id);
            return Json(result);
            //return Json(result);
        }

        [HttpPost]
        public JsonResult EditEmpoloyee(User user)//user là cái người dùng truyền vào
        {
            
                var dao = new UserDao();
                //if (!string.IsNullOrEmpty(user.Password))
                //{
                //    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //    user.Password = encryptedMd5Pas;
                //}
                //user.CreatedDate = DateTime.Now;
                // update trả về kiểu bool còn insert trả về kiểu int
                var rerult = dao.Update(user);
                if (rerult)
                {
                    //SetAlert("Sửa user thành công", "success");
                }
                else
                {

                }
            return Json(user);
        }
    }
}