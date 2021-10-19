using Microsoft.Extensions.Logging;
using ODATA.MS.CORE.API.Controllers;
using ODATA.MS.ORDER.API.Models;
using ODATA.MS.ORDER.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODATA.MS.ORDER.API.Controllers
{
    public class OrderController: BaseApiController<Order, OrderController, OrderDbContext>
    {
        public OrderController(ILogger<OrderController> logger, OrderDbContext orderDbContext) : base(logger, orderDbContext) { }
    }
}
