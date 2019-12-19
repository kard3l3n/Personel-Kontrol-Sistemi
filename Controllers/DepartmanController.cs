using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
	
	[Authorize(Roles = "A,U")]
	public class DepartmanController : Controller
    {
		PersonelDbEntities db = new PersonelDbEntities();

		
		public ActionResult Index()
        {
			var model = db.Departman.ToList();
            return View(model);
        }

		[HttpGet]
		
		public ActionResult Yeni()
		{
			return View("DepartmanForm",new Departman());
		}
		//CSRF
	    [ValidateAntiForgeryToken]
		public ActionResult Kaydet(Departman departman)
		{
			if (!ModelState.IsValid)
			{
				return View("departmanForm");
			}
			if (departman.Id==0)
			{
				db.Departman.Add(departman);
			}
			else
			{
				var guncellenecekDepartman = db.Departman.Find(departman.Id);
				if (guncellenecekDepartman == null)
				{
					return HttpNotFound();
				}
				guncellenecekDepartman.Ad = departman.Ad;
			}
			db.SaveChanges();
			return RedirectToAction("Index","Departman");
		}

		
		public ActionResult Güncelle(int id)
		{
			var model = db.Departman.Find(id);
			if (model==null)
			{
				return HttpNotFound();
			}

			return View("DepartmanForm",model);
		}
		public ActionResult Sil(int Id)
		{
			var silinecekDepartman = db.Departman.Find(Id);
			if (silinecekDepartman == null)
			{
				return HttpNotFound();
			}
			db.Departman.Remove(silinecekDepartman);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		protected override void OnActionExecuting(ActionExecutingContext filterContext)

		{

			base.OnActionExecuting(filterContext);

			if (RouteData.Values.First().Value != null)

			{

				try

				{

					if (RouteData.Values.First().Value.ToString() == "tr")

					{

						Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

						Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

					}

					if (RouteData.Values.First().Value.ToString() == "en")

					{

						Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

						Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

					}

					if (RouteData.Values.First().Value.ToString() == "de")

					{

						Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");

						Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

					}

				}

				catch (Exception ex)

				{

					Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

				}

			}

		}
	}
}