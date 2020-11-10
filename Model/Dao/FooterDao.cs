using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FooterDao
    {
        OnlineShopDbContext db = null;

        public FooterDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Footer> ListAll()
        {
           return db.Footers.Where(x => x.Status == true);
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
    }
}
