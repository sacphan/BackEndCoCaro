using CoCaro.Core;
using CoCaro.Data.Models;
using CoCaro.Service.WorkContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Controllers
{

    public class BaseController : Controller
    {
        protected IWorkContext _WorkContext = EngineContext.Resolve<IWorkContext>();
        public User _User { get => _WorkContext.CurrentUser; set => _WorkContext.CurrentUser = value; }
    }
}
