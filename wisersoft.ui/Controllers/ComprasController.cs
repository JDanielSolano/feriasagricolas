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
    public class ComprasController : Controller
    {
        IProvincias prov;
        ICantones can;
        IDistritos dis;
        IProductos pro;
        IClasificacion cla;
        IMedidas med;
        IAgricultores agr;
        IProductos_Por_Agricultor ppa;
        ICanastas canas;
        ICompras com;
        // GET: Ferias_Agricolas

        public ComprasController()
        {
            prov = new MProvincias();
            can = new MCantones();
            dis = new MDistritos();
            pro = new MProductos();
            cla = new MClasificacion();
            med = new MMedidas();
            agr = new MAgricultores();
            ppa = new MProductos_Por_Agricultor();
            canas = new MCanastas();
            com = new MCompras();
        }

        

        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("cliente"))
            {
                if (Session["Feria"] != null)
                {
                    //Session["Feria"]  = 1;
                    //Session["Cliente"] = "pgomez";

                    if (TempData["Agricultores"] != null)
                    {
                        ViewData["Agricultores"] = TempData["Agricultores"];
                        ViewData["Clasificacion"] = TempData["Clasificacion"];
                        ViewData["filProducto"] = TempData["filProducto"];
                        ViewData["canasta"] = TempData["canasta"];
                    }
                    else
                    {
                        List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
                        var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

                        List<DATA.Cantones> listaCantones = can.ListarCantones();
                        var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

                        List<DATA.Provincias> listaProvincias = prov.ListarProvincias();
                        var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

                        List<DATA.Agricultores> listaAgricultores = agr.ListarAgricultores();
                        var agricultores = Mapper.Map<List<Models.Agricultores>>(listaAgricultores);

                        List<DATA.Productos_Por_Agricultor> listaProductosPorProductor = ppa.ListarProductos_Por_Agricultor();
                        var productosPorAgricultor = Mapper.Map<List<Models.Productos_Por_Agricultor>>(listaProductosPorProductor);

                        List<DATA.Productos> listaProductos = pro.ListarProductos();
                        var productos = Mapper.Map<List<Models.Productos>>(listaProductos);

                        List<DATA.Clasificacion> listaClasificacion = cla.ListarClasificaciones();
                        var clasificacion = Mapper.Map<List<Models.Clasificacion>>(listaClasificacion);

                        List<DATA.Canastas> listaCanastas = canas.ListarCanastas();
                        var canastas = Mapper.Map<List<Models.Canastas>>(listaCanastas).Where(x => x.Can_feria == Convert.ToInt32(Session["feria"])).Where(x => x.Can_usuario == Session["UserID"].ToString()).OrderByDescending(x => x.Can_id).ToList();

                        List<DATA.Medidas> listaMedidas = med.ListarMedidas();
                        var medidas = Mapper.Map<List<Models.Medidas>>(listaMedidas);

                        foreach (Models.Productos producto in productos)
                        {
                            producto.Clasificacion = clasificacion.Where(x => x.Clasi_Id == producto.Pdt_tipo).FirstOrDefault();
                            producto.Medidas = medidas.Where(x => x.Mdd_Id == producto.Pdt_unidad_de_medida).FirstOrDefault();
                        }

                        foreach (Models.Productos_Por_Agricultor ppa in productosPorAgricultor)
                        {
                            ppa.Producto = productos.Where(x => x.Pdt_id == ppa.Ppa_Id_Producto).FirstOrDefault();
                        }

                        foreach (Models.Agricultores agricultor in agricultores)
                        {
                            agricultor.Distritos = distritos.Where(x => x.dis_id == agricultor.Agr_Distrito).FirstOrDefault();
                            agricultor.Cantones = cantones.Where(x => x.cnt_id == agricultor.Distritos.dis_canton).FirstOrDefault();
                            agricultor.Provincias = provincias.Where(x => x.Pro_id == agricultor.Cantones.cnt_provincia).FirstOrDefault();
                            agricultor.Productos = productosPorAgricultor.Where(x => x.Ppa_Id_Agricultor == agricultor.Agr_Cedula).ToList();
                        }
                        agricultores = agricultores.Where(x => x.Productos.Count > 0).ToList();
                        ViewData["Agricultores"] = agricultores;

                        foreach (var item in clasificacion)
                        {
                            item.usadoEnFiltro = 1;
                        }

                        ViewData["Clasificacion"] = clasificacion;
                        ViewData["filProducto"] = "";

                        foreach (var item in canastas)
                        {
                            item.seleccionada = 0;
                        }

                        ViewData["canasta"] = canastas;
                    }
                    return View();
                }
                else {
                    return RedirectToAction("Index", "SeleccionDeFeria");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Buscar(String filProducto, String filtro, string txtcesta, string cmbcesta)
        {
            List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
            var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

            List<DATA.Cantones> listaCantones = can.ListarCantones();
            var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

            List<DATA.Provincias> listaProvincias = prov.ListarProvincias();
            var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

            List<DATA.Agricultores> listaAgricultores = agr.ListarAgricultores();
            var agricultores = Mapper.Map<List<Models.Agricultores>>(listaAgricultores);

            List<DATA.Productos_Por_Agricultor> listaProductosPorProductor = ppa.ListarProductos_Por_Agricultor();
            var productosPorAgricultor = Mapper.Map<List<Models.Productos_Por_Agricultor>>(listaProductosPorProductor);

            List<DATA.Productos> listaProductos = pro.ListarProductos();
            var productos = Mapper.Map<List<Models.Productos>>(listaProductos);

            List<DATA.Clasificacion> listaClasificacion = cla.ListarClasificaciones();
            var clasificacion = Mapper.Map<List<Models.Clasificacion>>(listaClasificacion);

            List<DATA.Canastas> listaCanastas = canas.ListarCanastas();
            var canastas = Mapper.Map<List<Models.Canastas>>(listaCanastas).Where(x => x.Can_feria == Convert.ToInt32(Session["feria"])).OrderByDescending(x => x.Can_id).ToList();

            List<DATA.Medidas> listaMedidas = med.ListarMedidas();
            var medidas = Mapper.Map<List<Models.Medidas>>(listaMedidas);
            productos = productos.Where(x => x.Pdt_nombre.Contains(filProducto)).ToList();

            foreach (Models.Productos producto in productos)
            {
                producto.Clasificacion = clasificacion.Where(x => x.Clasi_Id == producto.Pdt_tipo).FirstOrDefault();
                producto.Medidas = medidas.Where(x => x.Mdd_Id == producto.Pdt_unidad_de_medida).FirstOrDefault();
            }
            
            productos = productos.Where(x => filtro.Contains(x.Clasificacion.Clasi_Nombre)).ToList();
            //productos = productos.Where(x =>  filtro.IndexOf(x.Clasificacion.Clasi_Nombre) >= 0).ToList();
            //productos = productos.Where(x =>"banano".IndexOf(x.Pdt_nombre) >=0 ).ToList();

            foreach (Models.Productos_Por_Agricultor ppa in productosPorAgricultor)
            {
                ppa.Producto = productos.Where(x => x.Pdt_id == ppa.Ppa_Id_Producto).FirstOrDefault();
            }
            productosPorAgricultor = productosPorAgricultor.Where(x => x.Producto != null).ToList();

            foreach (Models.Agricultores agricultor in agricultores)
            {
                agricultor.Distritos = distritos.Where(x => x.dis_id == agricultor.Agr_Distrito).FirstOrDefault();
                agricultor.Cantones = cantones.Where(x => x.cnt_id == agricultor.Distritos.dis_canton).FirstOrDefault();
                agricultor.Provincias = provincias.Where(x => x.Pro_id == agricultor.Cantones.cnt_provincia).FirstOrDefault();
                agricultor.Productos = productosPorAgricultor.Where(x => x.Ppa_Id_Agricultor == agricultor.Agr_Cedula).ToList();
            }

            agricultores = agricultores.Where(x => x.Productos.Count > 0).ToList();

            TempData["Agricultores"] = agricultores;

            foreach (var item in clasificacion)
            {
                if (filtro.IndexOf(item.Clasi_Nombre) >= 0)
                {
                    item.usadoEnFiltro = 1;
                }
                else
                {
                    item.usadoEnFiltro = 0;
                }
                
            }

            TempData["Clasificacion"] = clasificacion;
            TempData["filProducto"] = filProducto;

            if (txtcesta!="")
            {
                DateTime hoy = DateTime.Today;
                Models.Canastas canasta = new Models.Canastas();
                canasta.Can_fecha_de_creacion = hoy;
                canasta.Can_feria = Convert.ToInt32(Session["feria"]);
                canasta.Can_nombre = txtcesta;
                canasta.Can_usuario = Session["UserID"].ToString();

                var canastaInsertar = Mapper.Map<DATA.Canastas>(canasta);
                canas.InsertarCanasta(canastaInsertar);
                listaCanastas = canas.ListarCanastas();
                canastas = Mapper.Map<List<Models.Canastas>>(listaCanastas);
            }
            else
            {
                if (canastas.Where(x => x.Can_id == Convert.ToInt32(cmbcesta)).Count() > 0)
                    txtcesta = canastas.Where(x => x.Can_id == Convert.ToInt32(cmbcesta)).FirstOrDefault().Can_nombre;
                else
                    txtcesta = "";
            }


            foreach(var item in canastas.Where(x=> x.Can_usuario == Session["UserID"].ToString()))
            {
                if (item.Can_nombre == txtcesta)
                {
                    item.seleccionada = 1;
                }
                else
                {
                    item.seleccionada = 0;
                }
            }

            TempData["canasta"] = canastas;

            return RedirectToAction("Index");
        }

        public ActionResult Comprar(int id_canasta, int id_producto_por_agricultor,int cantidad)
        {

            try
            {
                Models.Compras compra = new Models.Compras();
                compra.Com_Cantidad = cantidad;
                compra.Com_Id_Canasta = id_canasta;
                compra.Com_Id_Producto_Por_Agricultor = id_producto_por_agricultor;

                var compraInsertar = Mapper.Map<DATA.Compras>(compra);
                com.InsertarCompras(compraInsertar);
            }
            catch (Exception e)
            {
                return Json("Error",JsonRequestBehavior.AllowGet);
            }

            return Json("exito", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Canasta( int id_canasta)
        {
            List<DATA.Compras> listaCompras = com.ListarComprasPorCanasta(id_canasta);
            var compra = Mapper.Map<List<Models.Compras>>(listaCompras);

            foreach(var comp in compra)
            {
                var productoPorProducto = ppa.BuscarProductos_Por_Agricultor(comp.Com_Id_Producto_Por_Agricultor);
                var productoPorProducto2 = Mapper.Map<Models.Productos_Por_Agricultor>(productoPorProducto);

                var producto  = pro.BuscarProductos(productoPorProducto2.Ppa_Id_Producto);
                var producto2 = Mapper.Map<Models.Productos>(producto);

                var unidadDeMedida = med.BuscarMedidas(producto2.Pdt_unidad_de_medida);
                producto2.Medidas = Mapper.Map<Models.Medidas>(unidadDeMedida);

                comp.Producto = producto2;
                
            }


            return Json(compra, JsonRequestBehavior.AllowGet);
        }

        public ActionResult eliminarDeCanasta(int id_compra)
        {
            String resultado = "";

            try
            {
                com.EliminarCompras(id_compra);
                resultado = "Eliminado correctamente.";
            }
            catch(Exception e)
            {
                resultado = "Error al eliminar la compra.";
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pago()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("cliente"))
            {
                if (Session["Feria"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "SeleccionDeFeria");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
    
}
