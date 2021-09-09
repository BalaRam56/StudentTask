using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Task_API.Models;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Dapper;

namespace Task_API.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly IConfiguration _config;
        public StudentRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DbConnection"));
            }
        }
        public async Task<List<Student>> GetStudents()
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string Query = "USP_Get_Students";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@studentID", 0);
                    var result = await con.QueryAsync<Student>(Query, param, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Student> DeleteStudent(int id)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string Query = "USP_DeleteStudent";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@studentID", id);
                    var result = await con.QueryAsync<Student>(Query, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> ADDEditStudent(Student student)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string sQuery = "USP_Add_Update_Student";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@StudentID", student.StudentID);
                    param.Add("@FirstName", student.FirstName);
                    param.Add("@LastName", student.LastName);
                    param.Add("@Age", student.Age);
                    param.Add("@Email", student.Email);
                    param.Add("@Phone", student.Phone);
                    param.Add("@Address", student.Address);
                    var result = await con.QueryAsync<Student>(sQuery, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> GetStudentByID(int id)
        {
            try
            {
                using (IDbConnection con = connection)
                {
                    string sQuery = "USP_Get_Students";
                    con.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@studentID", id);
                    var result = await con.QueryAsync<Student>(sQuery, param, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
