using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dtos.Dtos.Orders
{
    public enum OStatus {created = 1 , notCreated = 2  ,  updated = 3 , notUpdated = 4 , notExist=5 }
    public class CreatedOrUpdatedOrderDto
    {
        public int OrderId { get; set; }
        public string status { get; set; } = OStatus.created.ToString();
    }
}
