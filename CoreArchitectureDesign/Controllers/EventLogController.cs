using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreArchitectureDesign.Business.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CoreArchitectureDesign.Controllers
{
    public class EventLogController : Controller
    {
        private IEventLog eventLog;
        public EventLogController(IEventLog eventLog)
        {
            this.eventLog = eventLog;
        }
        public IActionResult Index()
        {
            var result = this.eventLog.GetList();
            return View(result.ResultObject.OrderByDescending(e => e.CreatedTime).ToList());
        }
    }
}