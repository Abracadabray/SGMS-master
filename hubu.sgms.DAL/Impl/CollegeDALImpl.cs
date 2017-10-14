﻿using hubu.sgms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hubu.sgms.DAL.Impl
{
    public class CollegeDALImpl : ICollegeDAL
    {
        public string SelectId(string collegeName)
        {
            //数据库中的college_namew应从text改为varchar
            string sql = "select college_id from college where name = @college_name ";

            SqlParameter[] parameters = {
                new SqlParameter("@college_name",collegeName)
            };

            //查询
            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql, parameters);
            DataRow row = dataTable.Rows[0];
            string collegeId = row["college_id"].ToString();

            return collegeId;
        }

        public IList<College> SelectColleges()
        {
            string sql = "select college_id,name,sort,student_number,teacher_number,class_number from College order by sort asc";

            DataTable dataTable = DBUtils.getDBUtils().getRecords(sql);

            //存放结果的list
            IList<College> collegeList = new List<College>();
            //遍历
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //college_id,name,sort,student_number,teacher_number,class_number 
                College college = new College();
                college.college_id = dataRow["college_id"].ToString();
                college.name = dataRow["name"].ToString();
                college.sort = Convert.ToInt32(dataRow["sort"]);
                college.student_number = Convert.ToInt32(dataRow["student_number"]);
                college.teacher_number = Convert.ToInt32(dataRow["teacher_number"]);
                college.class_number = Convert.ToInt32(dataRow["class_number"]);
                collegeList.Add(college);
            }

            return collegeList;
        }
    }
}
