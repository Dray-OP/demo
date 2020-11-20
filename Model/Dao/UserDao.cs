using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    //public để project khác truy cập được vào nó
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            // trả về id khi khởi tạo
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                // lấy ra user có id cần thay đổi
                var user = db.Users.Find(entity.ID);
                // thay đổi từng thuộc tính của user do entity truyền vào
                user.UserName = entity.UserName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                //rồi lưu lại
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        // sau có thể update hay làm gì đó thông qua phương thức này
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }


        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }        
        }
        //phương thức trả về 1 list
        // thêm searchString vào để xử lý phần tìm kiếm, có thể thêm tham số tìm kiếm vào đó
        public IEnumerable<User> ListAllPaging(string searchString, int page,int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            // thêm else if vào đây nếu có thêm điều kiện
            return model.OrderByDescending(x=>x.CreatedDate).ToPagedList(page,pageSize);
        }

        public User GetUserID(string userName)
        {
            return db.Users.SingleOrDefault(x=>x.UserName == userName);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public int Login(string userName, string passWord)
        {
           
            var result = db.Users.SingleOrDefault(x => x.UserName == userName );
            if (result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}
