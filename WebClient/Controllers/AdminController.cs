using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebClient.Net;

namespace WebClient.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : BaseMyController
    {
        [Authorize(Roles = "admin")]
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.blogs = JsonConvert.DeserializeObject<IEnumerable<BlogEntry>>(ApiConnector.RequestAsync("blogs/getList").Result);
            ViewBag.contacts = JsonConvert.DeserializeObject<IEnumerable<ContactEntry>>(ApiConnector.RequestAsync("contacts/GetList").Result);
            ViewBag.orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ApiConnector.RequestAsync("orders/getList").Result);
            ViewBag.projects = JsonConvert.DeserializeObject<IEnumerable<ProjectEntry>>(ApiConnector.RequestAsync("projects/getList").Result);
            ViewBag.services = JsonConvert.DeserializeObject<IEnumerable<ServiceEntry>>(ApiConnector.RequestAsync("services/getList").Result);

            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
