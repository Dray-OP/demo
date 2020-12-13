using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
    [Serializable]
    public class CartItem
    {
        //public long ProductId { get; set; }
        // để thành một đối tượng xong gọi ra
        public Product Product {  set; get; } 
        public int  Quantity {  set; get; }
    }
}