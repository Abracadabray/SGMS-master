﻿using hubu.sgms.BLL;
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
                if (Session["prePage"] != null)
                {
                    string prePage = (string)Session["prePage"];//登陆前，访问的页面
                    Session["prePage"] = null;
                    return Json(new { status = "1", successUrl = prePage });
                }
                //return Content("success:登陆成功！");
                //TODO  不同角色的处理
                return Json(new { status = "1", successUrl = "/Admin/Index" });//跳转到后台管理页面
            }
            else
            {
                //return Content("error:登陆失败！");
                return Json(new { status = "0", msg = "登录失败" } );//跳转到后台管理页面
            }
        }
    }
}