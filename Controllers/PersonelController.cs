﻿using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;



namespace PersonelMVCUI.Controllers
{
	[Authorize(Roles = "A,U")]
	public class PersonelController : Controller
    {
		PersonelDbEntities db = new PersonelDbEntities();
        // GET: Personel
		
        public ActionResult Index()
        {
			var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }
		
	  public ActionResult Yeni()
		{
			var model = new PersonelFormViewModel(){
			    Departmanlar = db.Departman.ToList(),
			    Personel = new Personel()
			};
			return View("personelForm",model);
		}
		
		[ValidateAntiForgeryToken]
		public ActionResult Kaydet(Personel personel)
		{
			if (!ModelState.IsValid)
			{

			    var model = new PersonelFormViewModel()
			    {
				    Departmanlar = db.Departman.ToList(),
				    Personel = personel
			    };
			    return View("personelForm",model);
			}
			if (personel.Id == 0)
			{
				db.Personel.Add(personel);
			}
			else
			{
				db.Entry(personel).State = System.Data.Entity.EntityState.Modified;

			}
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult Güncelle(int Id)
		{
			var model = new PersonelFormViewModel()
			{
				Departmanlar = db.Departman.ToList(),
				Personel = db.Personel.Find(Id)
			};
			return View("personelForm",model);

		}
		public ActionResult Sil(int id)
		{
			var silinecekPersonel = db.Personel.Find(id);
			if(silinecekPersonel==null)
			{
				return HttpNotFound();

			}
			db.Personel.Remove(silinecekPersonel);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
    }
}