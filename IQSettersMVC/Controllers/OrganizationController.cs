using IQSettersMVC.Client;
using IQSettersTestAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace IQSettersMVC.Controllers
{
    public class OrganizationController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var orgs = await WebApiClient.Get<List<OrganizationModel>>("organization");
            return View(orgs);
        }

        [HttpPost]
        public async Task<ActionResult> Save(OrganizationModel o)
        {
            await WebApiClient.Post("organization", o);
            return RedirectToAction("Index");
        }
    }
}
