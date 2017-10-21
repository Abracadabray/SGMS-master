﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hubu.sgms.Model;
using System.Data.SqlClient;
using System.Data;
using hubu.sgms.Utils;

namespace hubu.sgms.DAL.Impl
{
    public class CourseDALIml : ICourseDAL
    {
        //基本信息对应的数据库字段
        public const string BaseInfo = "course_id,course_name,course_credit,course_hour,college_id ";
        //public const string BaseInfo = "course_id,course_name,course_credit,course_hour,course_type,course_theory,course_experiment,course_opentime,course_prior,status";



        public int SelectCountByType(CourseType courseType, int status)
        {
            string sql = "select count(*) as result from Course where course_type = @course_type and status = @status";
            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）
            SqlParameter[] parameters = {
                new SqlParameter("@course_type",courseTypeDesc),
                new SqlParameter("@status",status),
            };
            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);
            DataRow dataRow = dataTable.Rows[0];
            int count = Convert.ToInt32(dataRow["result"]);
            return count;
        }

        
        
        //--------------管理员添加修改课程----------------//
  
        public string AddCourseBaseInfo(string courseID, string courseName, decimal courseCreadit, string courseHour, string courseType, string courseDepartement, string courseClass, string courseTheory, string courseExperiment, string courseOpentime, string coursePrior, int status)
        {
            DateTime dt_new = DateTime.Now;
            DateTime dt_old = new DateTime(2010, 1, 1, 0, 0, 0);
            TimeSpan ts = dt_new.Subtract(dt_old).Duration();
            string dateDiff = ts.Days.ToString() + ts.Hours.ToString() +ts.Minutes.ToString() + ts.Seconds.ToString();
            courseID = dateDiff.ToString();

            String courseopentime= courseOpentime;
            string[] arr = courseopentime.Split(' ');
            string year = arr[3];
            string month = arr[1];
            string day = arr[2];
            string end="";
            int start=0;
            if (month.Equals("Feb")|| month.Equals("Mar")|| month.Equals("Apr")||
                month.Equals("May") || month.Equals("Jun") || month.Equals("Jul") || 
                month.Equals("Aug") || month.Equals("Sep"))
            {
                end = "02";
            }
            else
            {
                end = "01";
            }
            
           start = Convert.ToInt32(year)%2000;
           courseOpentime = start.ToString()+end ;

            //添加的id是系统自己生成的，并且保证其唯一性  专业和学院目前不插入
            string sql = "Insert into Course(course_id,course_name,course_credit,course_hour,course_type,course_theory,course_experiment,course_opentime,course_prior,status)values(@courseID, @courseName, @courseCreadit, @courseHour, @courseType,@courseTheory, @courseExperiment, @courseOpentime, @coursePrior, @status)";
            //@courseId, @courseName, @courseCreadit, @courseHour, @courseType, course_department,course_class,@courseTheory, @courseExperiment, @courseOpentime, @coursePrior, @status
            SqlParameter[] parameters = {
                new SqlParameter("@courseID",courseID),
                new SqlParameter("@courseName",courseName),
                new SqlParameter("@courseCreadit",courseCreadit),
                new SqlParameter("@courseHour",courseHour),
                new SqlParameter("@courseType",courseType),
      //          new SqlParameter("@courseDepartment",courseDepartment),
      //          new SqlParameter("@courseClass",courseClass),
                new SqlParameter("@courseTheory",courseTheory),
                new SqlParameter("@courseExperiment",courseExperiment),
                new SqlParameter("@courseOpentime",courseOpentime),
                new SqlParameter("@coursePrior",coursePrior),
                new SqlParameter("@status",status)
            };

            int count = DBUtils.getDBUtils().cud(sql, parameters);
            string succeed = "成功添加" + count + "个课程信息";
            return succeed;
        }

        public string UpdateCourseBaseInfo(string courseID, string courseName, decimal courseCreadit, string courseHour, string courseType, string courseDepartement, string courseClass, string courseTheory, string courseExperiment, string courseOpentime, string coursePrior, int status)
        {
            String courseopentime = courseOpentime;
            string[] arr = courseopentime.Split(' ');
            string year = arr[3];
            string month = arr[1];
            string day = arr[2];
            string end = "";
            int start = 0;
            if (month.Equals("Feb") || month.Equals("Mar") || month.Equals("Apr") ||
                month.Equals("May") || month.Equals("Jun") || month.Equals("Jul") ||
                month.Equals("Aug") || month.Equals("Sep"))
            {
                end = "02";
            }
            else
            {
                end = "01";
            }

            start = Convert.ToInt32(year) % 2000;
            courseOpentime = start.ToString() + end;

            //目前不更新学院和专业
            string sql = "update Course set course_name=@courseName,course_credit=@courseCreadit,course_hour=@courseHour,course_type=@courseType,course_theory=@courseTheory,course_experiment=@courseExperiment,course_opentime=@courseOpentime,course_prior=@coursePrior,status=@status where course_id=@courseID";
            
            //@courseId, @courseName, @courseCreadit, @courseHour, @courseType, @courseTheory, @courseExperiment, @courseOpentime, @coursePrior, @status
            SqlParameter[] parameters={
                new SqlParameter("@courseID",courseID),
                new SqlParameter("@courseName",courseName),
                new SqlParameter("@courseCreadit",courseCreadit),
                new SqlParameter("@courseHour",courseHour),
                new SqlParameter("@courseType",courseType),
                new SqlParameter("@courseTheory",courseTheory),
                new SqlParameter("@courseExperiment",courseExperiment),
                new SqlParameter("@courseOpentime",courseOpentime),
                new SqlParameter("@coursePrior",coursePrior),
                new SqlParameter("@status",status)
            };

            int count = DBUtils.getDBUtils().cud(sql, parameters);
            string succeed = "成功添加" + count + "个课程信息";
            return succeed;
        }


        public string DeleteCourseBaseInfo(String courseID)
        {
            //目前不更新学院和专业
            string sql = "delete from Course where course_id=@courseId";
            
            SqlParameter[] parameters = {
                new SqlParameter("@courseID",courseID)
            };

            int count = DBUtils.getDBUtils().cud(sql, parameters);
            string succeed = "成功删除" + count + "个课程信息";
            return succeed;
        }

        //---------------管理员添加修改课程---------------//


        #region 查询指定id的所有课程信息
        Course ICourseDAL.SelectCourseById(int id)
        {
            string sql = "select * from course where course_id = @course_id";
            SqlParameter[] parameters = {
                new SqlParameter("@course_id",id+"")
            };
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);
            Course course = new Course();
            if (dataTable.Rows.Count > 0)
            {
                //mdzz
                DataRow dataRow = dataTable.Rows[0];
                course.course_id = dataRow["course_id"].ToString();
                course.course_name = dataRow["course_name"].ToString();
                course.course_credit = decimal.Parse(dataRow["course_credit"].ToString());
                course.course_hour = dataRow["course_hour"].ToString();
                course.course_type = dataRow["course_type"].ToString();
                course.college_id = dataRow["college_id"].ToString();
                course.class_id = dataRow["class_id"].ToString();
                course.course_theory = dataRow["course_theory"].ToString();
                course.course_experiment = dataRow["course_experiment"].ToString();
                course.course_opentime = dataRow["course_opentime"].ToString();
                course.course_prior = dataRow["course_prior"].ToString();
                //  course.status =Int32.Parse(dataRow["status"].ToString());
                course.status =1;
                course.course_photo = dataRow["course_photo"].ToString();
            }
            return course;
        }
        #endregion


        #region 根据课程类型查询课程的基本信息列表（course_id,course_name,course_credit,course_hour,college_id）
        public IList<Course> SelectCoursesBaseInfo(CourseType courseType, int status,
                            int start, int size)
        {
            //string sql = "SELECT TOP @size " + BaseInfo + " from Course "
            //    + " where course_type = @course_type and status = @status and course_id not in "
            //    + " (SELECT TOP (@size2 * ( @start - 1 )) course_id FROM Course ORDER BY course_id) "
            //    + " ORDER BY course_id ";
            string sql = "SELECT " + BaseInfo + " from Course  where course_type = @course_type and status = @status  ORDER BY course_id";
            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）
            SqlParameter[] parameters = {
                //new SqlParameter("@size",size),
                //new SqlParameter("@start",start),
                new SqlParameter("@course_type",courseTypeDesc),
                //new SqlParameter("@size2",size),
                new SqlParameter("@status",status),
            };

            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecordsWithPage(sql, parameters, size, start);

            //存放结果
            IList<Course> courseList = new List<Course>();

            //遍历
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //public const string BaseInfo = "course_id,course_name,course_credit,course_hour,college_id ";

                Course course = new Course();
                course.course_id = dataRow["course_id"].ToString();
                course.course_name = dataRow["course_name"].ToString();
                course.course_credit = decimal.Parse(dataRow["course_credit"].ToString());
                course.course_hour = dataRow["course_hour"].ToString();
                course.course_experiment = dataRow["college_id"].ToString();
                courseList.Add(course);
            }
            return courseList;
        }
        #endregion

        #region 查询最新的size门课程的基本信息
        public IList<Course> SelectCoursesOrderByDate(CourseType courseType, int size)
        {
            string sql = "SELECT TOP @size  " + BaseInfo + " from Course "
                + "where course_type = @course_type and status='1' order by course_id ";
            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）
            SqlParameter[] parameters = {
                new SqlParameter("@size",size),
                new SqlParameter("@course_type",courseTypeDesc)
            };

            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);
            //存放结果
            IList<Course> courseList = new List<Course>();
            //遍历
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //public const string BaseInfo = "course_id,course_name,course_credit,course_hour,college_id ";

                Course course = new Course();
                course.course_id = dataRow["course_id"].ToString();
                course.course_name = dataRow["course_name"].ToString();
                course.course_credit = decimal.Parse(dataRow["course_credit"].ToString());
                course.course_hour = dataRow["course_hour"].ToString();
                course.college_id = dataRow["college_id"].ToString();
                courseList.Add(course);
            }
            return courseList;

        }
        #endregion



        #region 查询选课人数最多的size门课程
        public IList<Course> SelectCoursesOrderBySelectCount(CourseType courseType, int size)
        {
            string sql = " select top @size " + BaseInfo
                + " from (select course_id, COUNT(*) as c from Course_choosing group by course_id) as Id_C(course_id,c),Course c "
                + " where Id_C.course_id=c.course_id and course_type = @course_type "
                + " order by Id_c.c desc ";
            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）
            SqlParameter[] parameters = {
                new SqlParameter("@size",size),
                new SqlParameter("@course_type",courseTypeDesc)
            };

            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);
            //存放结果
            IList<Course> courseList = new List<Course>();
            //遍历
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //public const string BaseInfo = "course_id,course_name,course_credit,course_hour,college_id ";

                Course course = new Course();
                course.course_id = dataRow["course_id"].ToString();
                course.course_name = dataRow["course_name"].ToString();
                course.course_credit = decimal.Parse(dataRow["course_credit"].ToString());
                course.course_hour = dataRow["course_hour"].ToString();
                course.college_id = dataRow["college_id"].ToString();
                courseList.Add(course);
            }
            return courseList;
        }
        #endregion

        public IList<Course> SelectCourse(CourseType courseType, string courseOpentime, string collegeId, string courseName,
            int page, int size)
        {
            string sql = "select " + BaseInfo +
                " from Course where course_type=@courseType ";
            // and course_opentime=@courseOpentime and college_id = @collegeId order by course_id

            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@courseType", courseTypeDesc));

            //拼接sql
            #region 拼接sql，并把相应参数放入list
            if (courseOpentime != null && !"".Equals(courseOpentime))
            {
                sql += " and course_opentime=@courseOpentime ";
                paramList.Add(new SqlParameter("@courseOpentime", courseOpentime));
            }
            if (collegeId != null && !"".Equals(collegeId))
            {
                sql += " and college_id = @collegeId ";
                paramList.Add(new SqlParameter("@collegeId", collegeId));
            }
            if (courseName != null && !"".Equals(courseName))
            {
                sql += " and course_name like @course_name ";
                courseName = "%" + courseName + "%";
                paramList.Add(new SqlParameter("@course_name", courseName));
            }
            sql += " order by course_id ";
            #endregion

            SqlParameter[] parameters = paramList.ToArray();

            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecordsWithPage(sql, parameters, size, page);
            //存放结果
            IList<Course> courseList = new List<Course>();
            //遍历
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //public const string BaseInfo = "course_id,course_name,course_credit,course_hour,college_id ";

                Course course = new Course();
                course.course_id = dataRow["course_id"].ToString();
                course.course_name = dataRow["course_name"].ToString();
                course.course_credit = decimal.Parse(dataRow["course_credit"].ToString());
                course.course_hour = dataRow["course_hour"].ToString();
                course.college_id = dataRow["college_id"].ToString();
                courseList.Add(course);
            }
            return courseList;
        }

        /// <summary>
        /// 查询课程总数
        /// </summary>
        /// <param name="courseType"></param>
        /// <param name="courseOpentime"></param>
        /// <param name="collegeId"></param>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public int SelectCount(CourseType courseType, string courseOpentime, string collegeId, string courseName)
        {
            string sql = "select count(*) as res " +
                " from Course where course_type=@courseType ";
            // and course_opentime=@courseOpentime and college_id = @collegeId order by course_id

            string courseTypeDesc = CourseTypeUtils.GetInfo(courseType);//获取课程类型的中文表示（字段上的注解）

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@courseType", courseTypeDesc));

            //拼接sql
            #region 拼接sql，并把相应参数放入list
            if (courseOpentime != null && !"".Equals(courseOpentime))
            {
                sql += " and course_opentime=@courseOpentime ";
                paramList.Add(new SqlParameter("@courseOpentime", courseOpentime));
            }
            if (collegeId != null && !"".Equals(collegeId))
            {
                sql += " and college_id = @collegeId ";
                paramList.Add(new SqlParameter("@collegeId", collegeId));
            }
            if (courseName != null && !"".Equals(courseName))
            {
                sql += " and course_name like @course_name";
                courseName = "%" + courseName + "%";
                paramList.Add(new SqlParameter("@course_name", courseName));
            }
            #endregion

            SqlParameter[] parameters = paramList.ToArray();
            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);

            DataRow row = dataTable.Rows[0];
            int count = Convert.ToInt32(row["res"]);

            return count;
        }

        /// <summary>
        /// 选课
        /// </summary>
        /// <param name="student"></param>
        /// <param name="courseInfo"></param>
        public void selectCourse(Student student, Teacher_course courseInfo)
        {
            Course_choosing course_Choosing = new Course_choosing();
            BeanUils.TransFields(student, course_Choosing);
            BeanUils.TransFields(courseInfo, course_Choosing);
            string sql = "insert into Course_choosing(course_choosing_id,student_id,student_name,teacher_course_id,teacher_id,teacher_name,course_id,course_name,classroom_id,status) "
                + " values(@course_choosing_id,@student_id,@student_name,@teacher_course_id,@teacher_id,@teacher_name,@course_id,@course_name,@classroom_id,@status)";
            IList<SqlParameter> sqlParameterList = BeanUils.SetInSQL(sql, course_Choosing);
            DBUtils.getDBUtils().cud(sql, sqlParameterList.ToArray());
        }

    }
}
