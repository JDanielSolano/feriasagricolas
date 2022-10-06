using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class Ferias_AgricolasController : Controller
    {
        IFerias_Agricolas fer;
        IDistritos dis;
        ICantones can;
        IProvincias pro;
        // GET: Ferias_Agricolas

        public Ferias_AgricolasController()
        {
            fer = new MFerias_Agricolas();
            dis = new MDistritos();
            can = new MCantones();
            pro = new MProvincias();
        }
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (TempData["delete"] != null)
                {
                    if (TempData["delete"].ToString() != "")
                    {

                        ModelState.AddModelError("error_delete_feria", TempData["delete"].ToString());
                        TempData["delete"] = "";
                    }
                }
                List<DATA.Ferias_Agricolas> listaFerias = fer.ListarFerias_Agricolas();
                List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                var ferias = Mapper.Map<List<Models.Ferias_Agricolas>>(listaFerias);
                var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);
                String feriasData = "";
                foreach (Models.Ferias_Agricolas feria in ferias)
                {
                    feria.Distritos = distritos.Where(x => x.dis_id == feria.Fa_Distrito).FirstOrDefault();
                    feriasData += feria.Fa_Id + "," + feria.Fa_Latitud + "," + feria.Fa_longitud + "," + feria.Fa_Nombre + "~";
                }
                ViewData["ferias"] = feriasData;
                return View(ferias);
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
                var listaProvincia = pro.ListarProvincias();
                IEnumerable<SelectListItem> selectProvincias =
                from c in listaProvincia
                select new SelectListItem
                {
                    Text = c.Pro_nombre,
                    Value = c.Pro_id.ToString()
                };

                ViewBag.Provincia = selectProvincias;
                ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                ViewBag.Distritos = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Ferias_Agricolas feria)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (feria.Fa_Latitud == null)
                {
                    ModelState.AddModelError("error_gps", "Debe de mover el punto para ajustar  la ubicación de la feria.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var feriaInsertar = Mapper.Map<DATA.Ferias_Agricolas>(feria);
                        fer.InsertarFerias_Agricolas(feriaInsertar);

                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error_feria", "No se pudo Agregar la feria.");
                    }

                }
                else
                {

                    List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                    var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                    List<DATA.Cantones> listaCantones = can.ListarCantones();
                    var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                    List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
                    var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                    var listaProvincia = pro.ListarProvincias();
                    IEnumerable<SelectListItem> selectProvincias =
                    from c in listaProvincia
                    select new SelectListItem
                    {
                        Text = c.Pro_nombre,
                        Value = c.Pro_id.ToString()
                    };

                    ViewBag.Provincia = selectProvincias;

                    //Debug.WriteLine("valor ----->"+
                    if (feria.Provincias.Pro_id == 0)
                    {
                        ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                    }
                    else
                    {

                        var listaCanton = can.ListarCantones();
                        IEnumerable<SelectListItem> selectCantones =
                        from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(feria.Provincias.Pro_id))
                        select new SelectListItem
                        {
                            Text = c.cnt_nombre,
                            Value = c.cnt_id.ToString()
                        };

                        ViewBag.Cantones = selectCantones;
                    }

                    if (feria.Cantones.cnt_id != 0)
                    {
                        var listaDistrito = dis.ListarDistritos();
                        IEnumerable<SelectListItem> selectDistritos =
                        from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(feria.Cantones.cnt_id))
                        select new SelectListItem
                        {
                            Text = c.dis_nombre,
                            Value = c.dis_id.ToString()
                        };

                        ViewBag.Distritos = selectDistritos;
                    }
                    else
                    {
                        ViewBag.Distritos = Enumerable.Empty<SelectListItem>();
                    }

                    return View();
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }

        public ActionResult Edit(int idFeria)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    var listaFerias = fer.BuscarFerias_Agricolas(idFeria);
                    var ferias = Mapper.Map<Models.Ferias_Agricolas>(listaFerias);

                    List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                    var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                    List<DATA.Cantones> listaCantones = can.ListarCantones();
                    var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                    List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
                    var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                    ferias.Distritos = distritos.Where(x => x.dis_id == ferias.Fa_Distrito).FirstOrDefault();
                    ferias.Cantones = cantones.Where(x => x.cnt_id == ferias.Distritos.dis_canton).FirstOrDefault();
                    ferias.Provincias = provincias.Where(x => x.Pro_id == ferias.Cantones.cnt_provincia).FirstOrDefault();
                    ViewData["idCanton"] = ferias.Cantones.cnt_id;
                    var listaProvincia = pro.ListarProvincias();
                    IEnumerable<SelectListItem> selectProvincias =
                    from c in listaProvincia
                    select new SelectListItem
                    {
                        Text = c.Pro_nombre,
                        Value = c.Pro_id.ToString()
                    };

                    ViewBag.Provincia = selectProvincias;

                    var listaCanton = can.ListarCantones();
                    IEnumerable<SelectListItem> selectCantones =
                    from c in listaCanton.Where(x => x.cnt_provincia == ferias.Provincias.Pro_id)
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString(),
                    };

                    ViewBag.Cantones = selectCantones;

                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == ferias.Cantones.cnt_id)
                    select new SelectListItem
                    {
                        Text = c.dis_nombre,
                        Value = c.dis_id.ToString(),
                    };

                    ViewBag.Distritos = selectDistritos;

                    return View(ferias);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("error", "No se ha podido actualizar");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Ferias_Agricolas feria)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    Debug.WriteLine("----> " + feria.Fa_Nombre);
                    if (feria.Fa_Latitud == null)
                    {
                        ModelState.AddModelError("error_gps", "Debe de mover el punto para ajustar  la ubicación de la feria.");
                    }

                    if (ModelState.IsValid)
                    {

                        var feriaActualizar = Mapper.Map<DATA.Ferias_Agricolas>(feria);
                        fer.ActualizarFerias_Agricolas(feriaActualizar);
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewData["idCanton"] = feria.Cantones.cnt_id;
                        List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                        var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                        List<DATA.Cantones> listaCantones = can.ListarCantones();
                        var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                        List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
                        var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                        var listaProvincia = pro.ListarProvincias();
                        IEnumerable<SelectListItem> selectProvincias =
                        from c in listaProvincia
                        select new SelectListItem
                        {
                            Text = c.Pro_nombre,
                            Value = c.Pro_id.ToString()
                        };

                        ViewBag.Provincia = selectProvincias;

                        //Debug.WriteLine("valor ----->"+
                        if (feria.Provincias.Pro_id == 0)
                        {
                            ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                        }
                        else
                        {

                            var listaCanton = can.ListarCantones();
                            IEnumerable<SelectListItem> selectCantones =
                            from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(feria.Provincias.Pro_id))
                            select new SelectListItem
                            {
                                Text = c.cnt_nombre,
                                Value = c.cnt_id.ToString()
                            };

                            ViewBag.Cantones = selectCantones;
                        }

                        if (feria.Cantones.cnt_id != 0)
                        {
                            var listaDistrito = dis.ListarDistritos();
                            IEnumerable<SelectListItem> selectDistritos =
                            from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(feria.Cantones.cnt_id))
                            select new SelectListItem
                            {
                                Text = c.dis_nombre,
                                Value = c.dis_id.ToString()
                            };

                            ViewBag.Distritos = selectDistritos;
                        }
                        else
                        {
                            ViewBag.Distritos = Enumerable.Empty<SelectListItem>();
                        }

                        return View();
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error", "No se ha podido actualizar");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult Delete(int idFeria)
        {
            try
            {
                fer.EliminarFerias_Agricolas(idFeria);
            }
            catch (Exception e)
            {
                TempData["delete"] ="No se puede eliminar la feria porque tiene información relacionada a ella.";
            }

            return RedirectToAction("Index");
        }

        
        public ActionResult BuscarCantones(int idProvincia)
        {
            try
            {
                List<DATA.Cantones> listaCantones = can.ListarCantones();
                IEnumerable<SelectListItem> selectCantones =
                from c in listaCantones.Where(x => x.cnt_provincia == idProvincia)
                select new SelectListItem
                {
                    Text = c.cnt_nombre,
                    Value = c.cnt_id.ToString()
                };

                return Json(selectCantones, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public ActionResult BuscarDistritos(int idCanton)
        {
            try
            {
                List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                IEnumerable<SelectListItem> selectDistritos =
                from c in listaDistritos.Where(x => x.dis_canton == idCanton)
                select new SelectListItem
                {
                    Text = c.dis_nombre,
                    Value = c.dis_id.ToString()
                };

                return Json(selectDistritos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
