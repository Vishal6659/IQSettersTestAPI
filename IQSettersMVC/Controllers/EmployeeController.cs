using IQSettersMVC.Client;
using IQSettersTestAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Helpers;

namespace IQSettersMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var emps = await WebApiClient.Get<List<Employee>>("employee");
            var orgs = await WebApiClient.Get<List<OrganizationModel>>("organization");
            ViewBag.Orgs = new SelectList(orgs, "OrgId", "OrgName");
            return View(emps);
        }
        [HttpPost]
        public async Task<ActionResult> Save(Employee e)
        {
            await WebApiClient.Post("employee", e);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id)
        {
            var emp = (await WebApiClient.Get<List<Employee>>("employee")).First(x => x.EmpId == id);
            return View(emp);
        }
    }
}
