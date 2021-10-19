using Microsoft.EntityFrameworkCore;
using ODATA.MS.CORE.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ODATA.MS.ORDER.API.Models
{
    [Index(nameof(Number), IsUnique = true)]
    public class Order: BaseEntity<Order>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date{ get; set; }

        public ICollection<OrderItem> Items { get; set; }

    }
}
