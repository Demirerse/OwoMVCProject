using Project.BLL.DesignPattern.GenericRepository.ConcRep;
using Project.WebUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebUI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _oRep;
        private readonly OrderDetailRepository _odRep;
        public OrderController()
        {
            _oRep = new OrderRepository();
            _odRep = new OrderDetailRepository();
        }

        // GET: Admin/Order
        public ActionResult OrderList()
        {
            OrderVM ovm = new OrderVM
            {
                Orders = _oRep.GetActives(),
                OrderDetails=_odRep.GetActives()
            };
            return View(ovm);
        }
    }
}