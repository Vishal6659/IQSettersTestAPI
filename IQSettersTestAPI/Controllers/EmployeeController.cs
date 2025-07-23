using IQSettersTestAPI.Model;
using IQSettersTestAPI.Service;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace IQSettersTestAPI.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {

        private readonly DbHelper _dbHelper;

        public EmployeeController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<Employee> employees = new List<Employee>();
            DataTable dt = _dbHelper.ExecuteSelect("SELECT * FROM Employee", CommandType.Text);

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(new Employee
                {
                    EmpId = Convert.ToInt32(row["EmpId"]),
                    EmpName = row["EmpName"].ToString(),
                    Email = row["Email"].ToString(),
                    OrgId = Convert.ToInt32(row["OrgId"]),  
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return Ok(employees);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddOrUpdate(Employee emp)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@EmpId", (object)emp.EmpId ?? DBNull.Value),
            new SqlParameter("@EmpName", emp.EmpName),
            new SqlParameter("@Email", (object)emp.Email ?? DBNull.Value),
            new SqlParameter("@OrgId", emp.OrgId)
            };

            _dbHelper.ExecuteNonQuery("sp_AddUpdateEmployee", CommandType.StoredProcedure, parameters);
            return Ok("Employee saved successfully.");
        }
    }
}
