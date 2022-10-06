using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace WiserSoft.UI.Controllers
{
    public class MedidasController : Controller
    {
        IMedidas medi;

        public MedidasController()
        {
            medi = new MMedidas();
        }
        //
        // GET: /Medidas/

        public ActionResult Index()
        {
            if (TempData["delete"] != null)
            {
                if (TempData["delete"].ToString() != "")
                {

                    ModelState.AddModelError("error_delete", TempData["delete"].ToString());
                    TempData["delete"] = "";
                }
            }

            var listaMedidas = medi.ListarMedidas();
            var medidas = Mapper.Map<List<Models.Medidas>>(listaMedidas);
            return View(medidas);
        }

        public ActionResult Create()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public ActionResult Create(Models.Medidas medidas)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var medidaInsertar = Mapper.Map<DATA.Medidas>(medidas);
                        medi.InsertarMedidas(medidaInsertar);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error", "No se ha podido insertar");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        public ActionResult Edit(int mdd_id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    var medida = medi.BuscarMedidas(mdd_id);
                    var medidaBuscar = Mapper.Map<Models.Medidas>(medida);
                    return View(medidaBuscar);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
            return View();
           
        }

        [HttpPost]
        public ActionResult Edit(Models.Medidas medida)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var medidaEditar = Mapper.Map<DATA.Medidas>(medida);
                        medi.ActualizarMedidas(medidaEditar);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        public ActionResult Delete(int mdd_id)
        {
            try
            {
                medi.EliminarMedidas(mdd_id);
            }
            catch (Exception e)
            {
                TempData["delete"] = "No se puede eliminar la medida porque tiene información relacionada a ella.";
            }

            return RedirectToAction("Index");
        }

    }
}
