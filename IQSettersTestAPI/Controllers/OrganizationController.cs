using IQSettersTestAPI.Model;
using IQSettersTestAPI.Service;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Web.Http;

namespace IQSettersTestAPI.Controllers
{
    [RoutePrefix("api/organization")]
    public class OrganizationController : ApiController
    {

        private readonly DbHelper _dbHelper;

        public OrganizationController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            List<OrganizationModel> organizations = new List<OrganizationModel>();
            DataTable dt = _dbHelper.ExecuteSelect("SELECT * FROM Organization", CommandType.Text);

            foreach (DataRow row in dt.Rows)
            {
                organizations.Add(new OrganizationModel
                {
                    OrgId = Convert.ToInt32(row["OrgId"]),
                    OrgName = row["OrgName"].ToString(),
                    OrgAddress = row["OrgAddress"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return Ok(organizations);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddOrUpdate(OrganizationModel org)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@OrgId", (object)org.OrgId ?? DBNull.Value),
            new SqlParameter("@OrgName", org.OrgName),
            new SqlParameter("@OrgAddress", (object)org.OrgAddress ?? DBNull.Value)
            };

            _dbHelper.ExecuteNonQuery("sp_AddUpdateOrganization", CommandType.StoredProcedure, parameters);
            return Ok("Organization saved successfully.");
        }
    }
}
