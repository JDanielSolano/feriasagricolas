using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;

namespace WiserSoft.UI.Controllers
{
    public class ProductosController : Controller
    {
        IProductos pro;
        IClasificacion cla;
        IMedidas med;
        
        // GET: Ferias_Agricolas

        public ProductosController()
        {
            pro = new MProductos();
            cla = new MClasificacion();
            med = new MMedidas();
        }

        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (TempData["delete"] != null)
                {
                    if (TempData["delete"].ToString() != "")
                    {

                        ModelState.AddModelError("error_delete_producto", TempData["delete"].ToString());
                        TempData["delete"] = "";
                    }
                }
                List<DATA.Productos> listaProductos = pro.ListarProductos();
                var productos = Mapper.Map<List<Models.Productos>>(listaProductos);

                List<DATA.Clasificacion> listaClasificacion = cla.ListarClasificaciones();
                var clasificacion = Mapper.Map<List<Models.Clasificacion>>(listaClasificacion);

                List<DATA.Medidas> listaMedidas = med.ListarMedidas();
                var medidas = Mapper.Map<List<Models.Medidas>>(listaMedidas);

                foreach (Models.Productos producto in productos)
                {
                    producto.Clasificacion = clasificacion.Where(x => x.Clasi_Id == producto.Pdt_tipo).FirstOrDefault();
                    producto.Medidas = medidas.Where(x => x.Mdd_Id == producto.Pdt_unidad_de_medida).FirstOrDefault();
                }

                return View(productos);
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
                var listaMedidas = med.ListarMedidas();
                IEnumerable<SelectListItem> selectMedidas =
                from c in listaMedidas
                select new SelectListItem
                {
                    Text = c.Mdd_Nombre,
                    Value = c.Mdd_Id.ToString()
                };

                ViewBag.Medidas = selectMedidas;

                var listaTipos = cla.ListarClasificaciones();
                IEnumerable<SelectListItem> selectTipos =
                from c in listaTipos
                select new SelectListItem
                {
                    Text = c.Clasi_Nombre,
                    Value = c.Clasi_Id.ToString()
                };

                ViewBag.Tipo = selectTipos;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        [HttpPost]
        public ActionResult Create(Models.Productos producto)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
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
                            producto.Pdt_foto = fileBytes;
                            // ... now fileBytes[] is filled with the contents of the file.
                        }

                        var productoInsertar = Mapper.Map<DATA.Productos>(producto);
                        pro.InsertarProductos(productoInsertar);

                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error_productos", "No se pudo agregar el producto.");
                    }
                }

                var listaMedidas = med.ListarMedidas();
                IEnumerable<SelectListItem> selectMedidas =
                from c in listaMedidas
                select new SelectListItem
                {
                    Text = c.Mdd_Nombre,
                    Value = c.Mdd_Id.ToString()
                };

                ViewBag.Medidas = selectMedidas;

                var listaTipos = cla.ListarClasificaciones();
                IEnumerable<SelectListItem> selectTipos =
                from c in listaTipos
                select new SelectListItem
                {
                    Text = c.Clasi_Nombre,
                    Value = c.Clasi_Id.ToString()
                };

                ViewBag.Tipo = selectTipos;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
         

        }

        public ActionResult Edit(int idProducto)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                var producto = pro.BuscarProductos(idProducto);
                var medidaBuscar = Mapper.Map<Models.Productos>(producto);

                medidaBuscar.Pdt_precio = Convert.ToDecimal( medidaBuscar.Pdt_precio.ToString().Replace(',','.'));
                var listaMedidas = med.ListarMedidas();
                IEnumerable<SelectListItem> selectMedidas =
                from c in listaMedidas
                select new SelectListItem
                {
                    Text = c.Mdd_Nombre,
                    Value = c.Mdd_Id.ToString()
                };

                ViewBag.Medidas = selectMedidas;

                var listaTipos = cla.ListarClasificaciones();
                IEnumerable<SelectListItem> selectTipos =
                from c in listaTipos
                select new SelectListItem
                {
                    Text = c.Clasi_Nombre,
                    Value = c.Clasi_Id.ToString()
                };

                ViewBag.Tipo = selectTipos;

                return View(producto);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Productos producto)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
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
                            producto.Pdt_foto = fileBytes;
                            // ... now fileBytes[] is filled with the contents of the file.
                        }
                        

                        var productoActualizar = Mapper.Map<DATA.Productos>(producto);
                        pro.ActualizarProductos(productoActualizar);

                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error_productos", "No se pudo agregar el producto.");
                    }
                }

                var listaMedidas = med.ListarMedidas();
                IEnumerable<SelectListItem> selectMedidas =
                from c in listaMedidas
                select new SelectListItem
                {
                    Text = c.Mdd_Nombre,
                    Value = c.Mdd_Id.ToString()
                };

                ViewBag.Medidas = selectMedidas;

                var listaTipos = cla.ListarClasificaciones();
                IEnumerable<SelectListItem> selectTipos =
                from c in listaTipos
                select new SelectListItem
                {
                    Text = c.Clasi_Nombre,
                    Value = c.Clasi_Id.ToString()
                };

                ViewBag.Tipo = selectTipos;


                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        public ActionResult Delete(int idProducto)
        {
            try
            {
                pro.EliminarProductos(idProducto);
            }
            catch (Exception e)
            {
                TempData["delete"] = "No se puede eliminar el producto porque tiene información relacionada a ella.";
            }

            return RedirectToAction("Index");
        }
    }
}
