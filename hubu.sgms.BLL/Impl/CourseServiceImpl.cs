﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hubu.sgms.DAL;
using hubu.sgms.Model;
using hubu.sgms.DAL.Impl;

namespace hubu.sgms.BLL.Impl
{
    public class CourseServiceImpl : ICourseService
    {
        private ICourseDAL courseDAL = new CourseDALIml();
        private ICollegeDAL collegeDAL = new CollegeDALImpl();

        /// <summary>
        /// 查询详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        public Course SelectCourseById(int id)
        {
            Course course = courseDAL.SelectCourseById(id);
            return course;
        }

        public IList<Course> SelectCoursesBaseInfo(CourseType courseType, int status, int page, int size) => courseDAL.SelectCoursesBaseInfo(courseType, status, page, size);


        public IList<Course> SelectCoursesOrderByDate(CourseType courseType, int size)
        {
            return courseDAL.SelectCoursesOrderByDate(courseType, size);
        }


        public IList<Course> SelectCoursesOrderBySelectCount(CourseType courseType, int size)
        {
            return courseDAL.SelectCoursesOrderBySelectCount(courseType, size);
        }

        public int SelectCountByType(CourseType courseType, int status)
        {
            return courseDAL.SelectCountByType(courseType, status);
        }


        // 添加课程信息数据
        public string AddCourseBaseInfo(string courseId, string courseName, decimal courseCreadit, string courseHour, string courseType, string courseDepartement, string courseClass, string courseTheory, string courseExperiment, string courseOpentime, string coursePrior, int status)
        {
            return courseDAL.AddCourseBaseInfo(courseId, courseName, courseCreadit, courseHour, courseType, courseDepartement, courseClass,courseTheory, courseExperiment, courseOpentime, coursePrior, status);
        }



        // 修改课程信息数据
        public string UpdateCourseBaseInfo(string courseID,string courseName, decimal courseCreadit, string courseHour, string courseType,string courseDepartement, string courseClass,string courseTheory, string courseExperiment, string courseOpentime, string coursePrior, int status)
        {
            return courseDAL.UpdateCourseBaseInfo(courseID,courseName, courseCreadit, courseHour, courseType, courseDepartement, courseClass, courseTheory, courseExperiment, courseOpentime, coursePrior, status);
        }


        // 删除课程信息数据
        public string DeleteCourseBaseInfo(String courseID)
        {
            return courseDAL.DeleteCourseBaseInfo(courseID);
        }

        /// <summary>
        /// 查询课程,当入参为-1或null或""时，表示省略该参数
        /// </summary>
        /// <param name="courseType">课程类型</param>
        /// <param name="courseOpentime">开课时间</param>
        /// <param name="collegeName">开课学院名称</param>
        /// <param name="courseName">课程名</param>
        /// <returns></returns>
        public IList<Course> SelectCourse(CourseType courseType, string courseOpentime, string collegeId, string courseName, int page, int size)
        {

            return courseDAL.SelectCourse(courseType, courseOpentime, collegeId, courseName, page, size);
        }


        public int SelectCount(CourseType courseType, string courseOpentime, string collegeId, string courseName)
        {
            return courseDAL.SelectCount(courseType, courseOpentime, collegeId, courseName);
        }


        public IList<College> SelectColleges()
        {
            return collegeDAL.SelectColleges();
        }
    }
}
