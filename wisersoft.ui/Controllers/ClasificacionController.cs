using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;


namespace WiserSoft.UI.Controllers
{
    public class ClasificacionController : Controller
    {
        IClasificacion clasi;

        public ClasificacionController()
        {
            clasi = new MClasificacion();
        }

        // GET: /Clasificacion/
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (TempData["delete"] != null)
                {
                    if (TempData["delete"].ToString() != "")
                    {

                        ModelState.AddModelError("error_delete", TempData["delete"].ToString());
                        TempData["delete"] = "";
                    }
                }
                var listaClasificacion = clasi.ListarClasificaciones();
                var clasificaciones = Mapper.Map<List<Models.Clasificacion>>(listaClasificacion);
                return View(clasificaciones);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
        public ActionResult Create(Models.Clasificacion clasificacion)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var clasificacionInsertar = Mapper.Map<DATA.Clasificacion>(clasificacion);
                        clasi.InsertarClasificacion(clasificacionInsertar);
                        return RedirectToAction("Index");
                    }
                    
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error", "No se ha podido insertar una clasificación");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Edit(int clasi_id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    var clasificacion = clasi.BuscarClasificacion(clasi_id);
                    var clasificacionBuscar = Mapper.Map<Models.Clasificacion>(clasificacion);
                    return View(clasificacionBuscar);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error", "No se ha podido encontrar la clasificación");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Models.Clasificacion clasificacion)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var clasificacionEditar = Mapper.Map<DATA.Clasificacion>(clasificacion);
                        clasi.ActualizarClasificacion(clasificacionEditar);
                        return RedirectToAction("Index");
                    }
                   
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error", "No se ha podido actualizar la clasificación");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int clasi_id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    clasi.EliminarClasificacion(clasi_id);
                }
                catch (Exception e)
                {
                    TempData["delete"] = "No se puede eliminar la clasificacion porque tiene información relacionada a ella.";
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
            return RedirectToAction("Index");
        }


    }
}
