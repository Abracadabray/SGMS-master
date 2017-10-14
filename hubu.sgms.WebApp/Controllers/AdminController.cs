using hubu.sgms.BLL;
using hubu.sgms.BLL.Impl;
using hubu.sgms.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hubu.sgms.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private ICourseService courseService = new CourseServiceImpl();

        // Admin后台中心主界面
        public ActionResult Index()
        {
            return View();
        }

        //管理员操纵学生信息界面
        public ActionResult AdminOperateStudent()
        {
            return View();
        }

        //学生改密码
        public ActionResult StudentChangePassword()
        {
            return View();
        }

        //添加学生信息
        public ActionResult AddCourseInfo()
        {
            return View();
        }

        //提交添加课程的信息
        public ActionResult SubmitAddCourseInfo()
        {
            // 获取用户提交的表单内容
            //Request courseRequest = new Request();
            string CourseID="20171010";
            string CourseName = Request["course_name"];
            string courseCredit = Request["course_credit"];  //数字+汉字
            string CourseHour = Request["course_hour"];
            string CourseType = Request["course_type"];     //null
            string CourseDepartement = Request["course_department"];     //null
            string CourseClass = Request["course_class"];     //null
            string CourseTheory = Request["course_theory"];
            string CourseExperiment = Request["course_experiment"];
            string CourseOpentime = Request["course_opentime"];
            string CoursePrior = Request["course_prior"];
            string courseStatus = Request["course_status"];     //null

            //部分转换数据类型
            decimal CourseCredit = 0;
            int CourseStatus = 3;
            if (courseCredit != null)
            {
                CourseCredit = Convert.ToDecimal(courseCredit);
            }
            if (courseStatus != null)
            {
                if (courseStatus == "待开课")
                {
                    courseStatus = "1";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
                else if (courseStatus == "已开课")
                {
                    courseStatus = "2";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
                else
                {
                    courseStatus = "3";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
            }
            string result = courseService.AddCourseBaseInfo(CourseID,CourseName, CourseCredit, CourseHour, CourseType, CourseDepartement, CourseClass, CourseTheory, CourseExperiment, CourseOpentime, CoursePrior, CourseStatus);

            ViewData["courseType"] = CourseType;
            return View();
        }


        //更新课程信息
        public ActionResult UpdateCourseInfo(int courseId)
        { 
            Course course = courseService.SelectCourseById(courseId);
            ViewData["courseID"] = course.course_id;
            ViewData["courseName"] = course.course_name;
            ViewData["courseCredit"] = course.course_credit;
            ViewData["courseHour"] = course.course_hour;
            ViewData["courseType"] = course.course_type;
            ViewData["courseTheory"] = course.course_theory;
            ViewData["collegeID"] = course.college_id;
            ViewData["classID"] = course.class_id;
            ViewData["courseExperiment"] = course.course_experiment;
            ViewData["courseOpentime"] = course.course_opentime;
            ViewData["coursePrior"] = course.course_prior;
            ViewData["status"] = course.status;
            return View();
        }


        //提交更新的课程信息
        public ActionResult SubmitUpdateCourseInfo()
        {
            // 获取用户提交的表单内容
            string CourseID = Request["course_id"];
            string CourseName = Request["course_name"];
            string courseCredit = Request["course_credit"];
            string CourseHour = Request["course_hour"];
            string CourseType = Request["course_type"];
            string CourseDepartement = Request["course_departement"]; 
            string CourseClass = Request["course_class"];
            string CourseTheory = Request["course_theory"];
            Console.Write(CourseTheory);
            string CourseExperiment = Request["course_experiment"];
            string CourseOpentime = Request["course_opentime"];
            string CoursePrior = Request["course_prior"];
            string courseStatus = Request["course_status"];

            decimal CourseCredit = 0;
            int CourseStatus = 3;
            if (courseCredit != null)
            {
                CourseCredit = Convert.ToDecimal(courseCredit);
            }
            if (courseStatus != null)
            {
                if (courseStatus == "待开课")
                {
                    courseStatus = "1";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
                else if (courseStatus == "已开课")
                {
                    courseStatus = "2";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
                else
                {
                    courseStatus = "3";
                    CourseStatus = Convert.ToInt32(courseStatus);
                }
            }

            string result = courseService.UpdateCourseBaseInfo(CourseID,CourseName, CourseCredit, CourseHour, CourseType, CourseDepartement, CourseClass, CourseTheory, CourseExperiment, CourseOpentime, CoursePrior, CourseStatus);
            Response.Write("修改成功");
            return View();
        }


        //查看课程页面
        public ActionResult ViewCourseInfo(int courseId)
             //public ActionResult ViewCourseInfo()
        {
            Course course = courseService.SelectCourseById(courseId);
            ViewData["courseName"] = course.course_name;
            ViewData["courseCredit"] = course.course_credit;
            ViewData["courseHour"] = course.course_hour;
            ViewData["courseType"] = course.course_type;
            ViewData["courseTheory"] = course.course_theory;
            ViewData["collegeID"] = course.college_id;
            ViewData["classID"] = course.class_id;
            ViewData["courseExperiment"] = course.course_experiment;
            ViewData["courseOpentime"] = course.course_opentime;
            ViewData["coursePrior"] = course.course_prior;
            ViewData["status"] = course.status;
         
            //ViewData["coursePhoto"] = course.yuliu1;
            return View();
        }

        //查询课程页面
        public ActionResult AlterCourseInfo()
        {
            return View();
        }

        public ActionResult DeleteCourseInfo(int courseId)
        {
            string result = courseService.DeleteCourseBaseInfo(courseId.ToString());
            return View("AlterCourseInfo");
        }

    }
}