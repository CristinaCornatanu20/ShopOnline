using ShopOnline.Models.DBObjects;
using static com.sun.tools.@internal.xjc.reader.xmlschema.bindinfo.BIConversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class OrderModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AspNetUser User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
