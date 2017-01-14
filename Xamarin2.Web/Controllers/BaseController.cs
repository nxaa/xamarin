using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xamarin2.Data;

namespace Xamarin2.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Model db = new Model();
    }
}