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
    public class AgricultoresController : Controller
    {
        IAgricultores agr;
        IDistritos dis;
        IProvincias pro;
        ICantones can;
        IFerias_Agricolas fer;


        public AgricultoresController()
        {
            agr = new MAgricultores();
            dis = new MDistritos();
            can = new MCantones();
            pro = new MProvincias();
            fer = new MFerias_Agricolas();
        }

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

                    ViewBag.UserId = Session["UserID"];
               
                    List<DATA.Ferias_Agricolas> listaFerias = fer.ListarFerias_Agricolas();
                    List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                    List<DATA.Agricultores> listaAgricultores = agr.ListarAgricultores();

                    var agricultores = Mapper.Map<List<Models.Agricultores>>(listaAgricultores);
                    var ferias = Mapper.Map<List<Models.Ferias_Agricolas>>(listaFerias);
                    var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                    foreach (Models.Agricultores agricultor in agricultores)
                    {
                        agricultor.Distritos = distritos.Where(x => x.dis_id == agricultor.Agr_Distrito).FirstOrDefault();
                        agricultor.Ferias_Agricolas = ferias.Where(x => x.Fa_Id == agricultor.Agr_Feria_A_La_Que_Asiste).FirstOrDefault();
                    }

                    return View(agricultores);
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
                var listaFerias = fer.ListarFerias_Agricolas();
                var listaProvincia = pro.ListarProvincias();

                IEnumerable<SelectListItem> selectProvincias =
                from c in listaProvincia
                select new SelectListItem
                {
                    Text = c.Pro_nombre,
                    Value = c.Pro_id.ToString()
                };

                IEnumerable<SelectListItem> selectFerias =
                from c in listaFerias
                select new SelectListItem
                {
                    Text = c.Fa_Nombre,
                    Value = c.Fa_Id.ToString()
                };

                ViewBag.Provincia = selectProvincias;
                ViewBag.Ferias = selectFerias;

                ViewBag.Cantones = Enumerable.Empty<SelectListItem>();

                ViewBag.Distritos = Enumerable.Empty<SelectListItem>();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult Create(Models.Agricultores agricultores)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Files["Photo"];

                    if (file != null)
                    {
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);
                        agricultores.Agr_Foto = fileBytes;
                        // ... now fileBytes[] is filled with the contents of the file.
                    }

                    string varSwitch = Request.Form["switch"].Split(',')[0];
                    if (varSwitch == "true")
                        agricultores.Agr_Estado = 1;
                    else
                        agricultores.Agr_Estado = 0;

                    var agrInsertar = Mapper.Map<DATA.Agricultores>(agricultores);
                    agr.InsertarAgricultor(agrInsertar);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                    ModelState.AddModelError("error_agricultor", "No se pudo Agregar el agricultor.");
                }

            }
            else
            {
                try
                {
                    List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                    var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                    List<DATA.Cantones> listaCantones = can.ListarCantones();
                    var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                    List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
                    var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                    List<DATA.Ferias_Agricolas> listaFerias = fer.ListarFerias_Agricolas();
                    var ferias = Mapper.Map<List<Models.Ferias_Agricolas>>(listaFerias);

                    var listaFeria = fer.ListarFerias_Agricolas();
                    var listaProvincia = pro.ListarProvincias();

                    IEnumerable<SelectListItem> selectProvincias =
                    from c in listaProvincia
                    select new SelectListItem
                    {
                        Text = c.Pro_nombre,
                        Value = c.Pro_id.ToString()
                    };

                    IEnumerable<SelectListItem> selectFerias =
                    from c in listaFerias
                    select new SelectListItem
                    {
                        Text = c.Fa_Nombre,
                        Value = c.Fa_Id.ToString()
                    };

                    ViewBag.Provincia = selectProvincias;
                    ViewBag.Ferias = selectFerias;

                    if (agricultores.Provincias.Pro_id == 0)
                    {
                        ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                    }
                    else
                    {

                        var listaCanton = can.ListarCantones();
                        IEnumerable<SelectListItem> selectCantones =
                        from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(agricultores.Provincias.Pro_id))
                        select new SelectListItem
                        {
                            Text = c.cnt_nombre,
                            Value = c.cnt_id.ToString()
                        };

                        ViewBag.Cantones = selectCantones;
                    }

                    if (agricultores.Cantones.cnt_id != 0)
                    {
                        var listaDistrito = dis.ListarDistritos();
                        IEnumerable<SelectListItem> selectDistritos =
                        from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(agricultores.Cantones.cnt_id))
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
                }
                catch (Exception)
                {

                    ModelState.AddModelError("error_agricultor", "No se pudo Agregar el agricultor.");
                }



            }
            return View();
        }
        public ActionResult Edit(String id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                    var agricultor = agr.BuscarAgricultor(id);
                    var agricultorBuscar = Mapper.Map<Models.Agricultores>(agricultor);

                    List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                    var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                    List<DATA.Cantones> listaCantones = can.ListarCantones();
                    var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                    List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
                    var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                    List<DATA.Ferias_Agricolas> listaFerias = fer.ListarFerias_Agricolas();
                    var ferias = Mapper.Map<List<Models.Ferias_Agricolas>>(listaFerias);

                    agricultorBuscar.Ferias_Agricolas = ferias.Where(x => x.Fa_Id == agricultorBuscar.Agr_Feria_A_La_Que_Asiste).FirstOrDefault();
                    agricultorBuscar.Distritos = distritos.Where(x => x.dis_id == agricultorBuscar.Agr_Distrito).FirstOrDefault();
                    agricultorBuscar.Cantones = cantones.Where(x => x.cnt_id == agricultorBuscar.Distritos.dis_canton).FirstOrDefault();
                    agricultorBuscar.Provincias = provincias.Where(x => x.Pro_id == agricultorBuscar.Cantones.cnt_provincia).FirstOrDefault();
                    ViewData["idCanton"] = agricultorBuscar.Cantones.cnt_id;

                    var listaProvincia = pro.ListarProvincias();
                    var listaFeria = fer.ListarFerias_Agricolas();

                    IEnumerable<SelectListItem> selectFerias =
                   from c in listaFeria
                   select new SelectListItem
                   {
                       Text = c.Fa_Nombre,
                       Value = c.Fa_Id.ToString()
                   };

                    ViewBag.Ferias = selectFerias;

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
                    from c in listaCanton.Where(x => x.cnt_provincia == agricultorBuscar.Provincias.Pro_id)
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString(),
                    };

                    ViewBag.Cantones = selectCantones;

                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == agricultorBuscar.Cantones.cnt_id)
                    select new SelectListItem
                    {
                        Text = c.dis_nombre,
                        Value = c.dis_id.ToString(),
                    };

                    ViewBag.Distritos = selectDistritos;

                    return View(agricultorBuscar);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Models.Agricultores agricultor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Files["Photo"];
                    if (file != null)
                    {
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, file.ContentLength);
                        agricultor.Agr_Foto = fileBytes;

                    }

                    var productoEditar = Mapper.Map<DATA.Agricultores>(agricultor);
                    agr.ActualizarAgricultor(productoEditar);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                    ModelState.AddModelError("error_agricultores", "Error al actualizar una feria");
                }

            }
            else
            {
                ViewData["idCanton"] = agricultor.Cantones.cnt_id;
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
                if (agricultor.Provincias.Pro_id == 0)
                {
                    ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                }
                else
                {
                    var listaCanton = can.ListarCantones();
                    IEnumerable<SelectListItem> selectCantones =
                    from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(agricultor.Provincias.Pro_id))
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString()
                    };

                    ViewBag.Cantones = selectCantones;
                }

                if (agricultor.Cantones.cnt_id != 0)
                {
                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(agricultor.Cantones.cnt_id))
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
            return View();
        }

        public ActionResult Delete(String id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    agr.EliminarAgricultor(id);
                    return RedirectToAction("Index");
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

        public ActionResult BuscarCantones(int idProvincia)
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

        public ActionResult BuscarDistritos(int idCanton)
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


    }
}
