using hubu.sgms.BLL;
using hubu.sgms.BLL.Impl;
using hubu.sgms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hubu.sgms.WebApp.Controllers
{
    public class LoginController : Controller
    {

        ILoginService loginService = new LoginServiceImpl();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin()
        {
            //添加相应的验证码代码

            string username = Request["username"];
            string password = Request["password"];

            Login loginInfo = loginService.GetUser(username, password);

            if (loginInfo != null)
            {
                Session["loginInfo"] = loginInfo;
                return Content("success:登陆成功！");
            }
            else
            {
                return Content("error:登陆失败！");
            }
        }
    }
}