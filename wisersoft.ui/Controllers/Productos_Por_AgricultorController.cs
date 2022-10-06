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
    public class Productos_Por_AgricultorController : Controller
    {
        IProductos_Por_Agricultor prod_x_agr;
        IProductos prod;
        IAgricultores agr;

        public Productos_Por_AgricultorController()
        {
            prod_x_agr = new MProductos_Por_Agricultor();
            prod = new MProductos();
            agr = new MAgricultores();
        }
        //
        // GET: /ProductosPorAgricultor/

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

                List<DATA.Productos> listaProductos = prod.ListarProductos();
                var productos = Mapper.Map<List<Models.Productos>>(listaProductos);

                List<DATA.Productos_Por_Agricultor> listaProductosPorAgricultor = prod_x_agr.ListarProductos_Por_Agricultor();
                var productos_por_agricultor = Mapper.Map<List<Models.Productos_Por_Agricultor>>(listaProductosPorAgricultor);

                List<DATA.Agricultores> listaAgricultores = agr.ListarAgricultores();
                var agricultores = Mapper.Map<List<Models.Agricultores>>(listaAgricultores);

                foreach (Models.Productos_Por_Agricultor producto_por_agricultor in productos_por_agricultor)
                {
                    producto_por_agricultor.Producto = productos.Where(x => x.Pdt_id == producto_por_agricultor.Ppa_Id_Producto).FirstOrDefault();
                    producto_por_agricultor.Agricultores = agricultores.Where(x => x.Agr_Cedula == producto_por_agricultor.Ppa_Id_Agricultor).FirstOrDefault();
                }

                return View(productos_por_agricultor);
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
                var listaProductos = prod.ListarProductos();
                IEnumerable<SelectListItem> selectProductos =
                from c in listaProductos
                select new SelectListItem
                {
                    Text = c.Pdt_nombre,
                    Value = c.Pdt_id.ToString()
                };

                ViewBag.Productos = selectProductos;

                var listaAgricultores = agr.ListarAgricultores();
                IEnumerable<SelectListItem> selectAgricultores =
                from c in listaAgricultores
                select new SelectListItem
                {
                    Text = c.Agr_Cedula,
                    Value = c.Agr_Cedula
                };

                ViewBag.Agricultores = selectAgricultores;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Productos_Por_Agricultor prod_x_agricultor)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var prod_x_agricultorInsertar = Mapper.Map<DATA.Productos_Por_Agricultor>(prod_x_agricultor);
                        prod_x_agr.InsertarProductos_Por_Agricultor(prod_x_agricultorInsertar);

                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error_productos", "No se pudo relacionar el producto con el agricultor.");
                    }
                }

                var listaProductos = prod.ListarProductos();
                IEnumerable<SelectListItem> selectProductos =
                from c in listaProductos
                select new SelectListItem
                {
                    Text = c.Pdt_nombre,
                    Value = c.Pdt_id.ToString()
                };

                ViewBag.Productos = selectProductos;

                var listaAgricultores = agr.ListarAgricultores();
                IEnumerable<SelectListItem> selectAgricultores =
                from c in listaAgricultores
                select new SelectListItem
                {
                    Text = c.Agr_Cedula,
                    Value = c.Agr_Cedula
                };

                ViewBag.Agricultores = selectAgricultores;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

            return View();

        }

        public ActionResult Edit(int Ppa_Id)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    var prod_x_agricultor = prod_x_agr.BuscarProductos_Por_Agricultor(Ppa_Id);
                    var prodAgricultorBuscar = Mapper.Map<Models.Productos_Por_Agricultor>(prod_x_agricultor);

                    var listaProductos = prod.ListarProductos();
                    IEnumerable<SelectListItem> selectProductos =
                    from c in listaProductos
                    select new SelectListItem
                    {
                        Text = c.Pdt_nombre,
                        Value = c.Pdt_id.ToString()
                    };

                    ViewBag.Productos = selectProductos;

                    var listaAgricultores = agr.ListarAgricultores();
                    IEnumerable<SelectListItem> selectAgricultores =
                    from c in listaAgricultores
                    select new SelectListItem
                    {
                        Text = c.Agr_Cedula,
                        Value = c.Agr_Cedula
                    };

                    ViewBag.Agricultores = selectAgricultores;

                    return View(prodAgricultorBuscar);
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
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Productos_Por_Agricultor prod_agriculto)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var prodAgricultorEditar = Mapper.Map<DATA.Productos_Por_Agricultor>(prod_agriculto);
                        prod_x_agr.ActualizarProductos_Por_Agricultor(prodAgricultorEditar);
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error_productos", "No se pudo agregar la relación");
                    }
                }

                var listaProductos = prod.ListarProductos();
                IEnumerable<SelectListItem> selectProductos =
                from c in listaProductos
                select new SelectListItem
                {
                    Text = c.Pdt_nombre,
                    Value = c.Pdt_id.ToString()
                };

                ViewBag.Productos = selectProductos;

                var listaAgricultores = agr.ListarAgricultores();
                IEnumerable<SelectListItem> selectAgricultores =
                from c in listaAgricultores
                select new SelectListItem
                {
                    Text = c.Agr_Cedula,
                    Value = c.Agr_Cedula
                };

                ViewBag.Agricultores = selectAgricultores;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           

            return View();
        }

        public ActionResult Delete(int Ppa_Id)
        {
            try
            {
                prod_x_agr.EliminarProductos_Por_Agricultor(Ppa_Id);
            }
            catch (Exception e)
            {
                TempData["delete"] = "No se puede eliminar porque tiene información relacionada a ella.";
            }

            return RedirectToAction("Index");
        }


    }
}
