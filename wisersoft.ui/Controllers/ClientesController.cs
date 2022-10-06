using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WiserSoft.DAL.Interfaces;
using WiserSoft.DAL.Metodos;
using AutoMapper;
using System.Diagnostics;

namespace WiserSoft.UI.Controllers
{
    public class ClientesController : Controller
    {
        IClientes clien;
        IDistritos dis;
        ICantones can;
        IProvincias pro;

        public ClientesController()
        {
            clien = new MClientes();
            dis = new MDistritos();
            can = new MCantones();
            pro = new MProvincias();
        }
        //
        // GET: /Clientes/

        public ActionResult Index()
        {
            List<DATA.Clientes> listaClientes = clien.ListarClientes();
            List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
            var clientes = Mapper.Map<List<Models.Clientes>>(listaClientes);
            var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

            foreach (Models.Clientes cliente in clientes)
            {
                cliente.Distritos = distritos.Where(x => x.dis_id == cliente.Cl_Distrito).FirstOrDefault();
            }


            return View(clientes);
        }

        public ActionResult Create()
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

            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Clientes cliente)
        {
            DATA.Clientes existeUsuario = null;
            DATA.Clientes existeCedula = null;
            DATA.Clientes existeCorreo = null;
            //valida usuario no existe
            try
            {
                existeUsuario = clien.BuscarClienteExistentePorUsuario(cliente.Cl_Usuario);
                existeCedula = clien.BuscarClienteExistentePorCedula(cliente.Cl_Cedula);
                existeCorreo = clien.BuscarClienteExistentePorCorreo(cliente.Cl_Correo);
            }
            catch(Exception e)
            {
                Debug.Write(e);
            }

            if (cliente.Cl_Latitude== null)
            {
                ModelState.AddModelError("error_gps", "Debe de mover el punto para ajustar  la ubicación de su hogar.");
            }

            if (existeUsuario != null)
                ModelState.AddModelError("error_existe", "El usuario ingresado ya existe.");
            if (existeCedula != null)
                ModelState.AddModelError("error_existe", "La cédula ingresada ya existe.");
            if (existeCorreo != null)
                ModelState.AddModelError("error_existe", "El correo seleccionado ya existe.");

            if (ModelState.IsValid)
            {
                try
                {
                    //Encripta el password
                    var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(cliente.Cl_Password);
                    //Asigna la variable encriptada al objeto Password
                    cliente.Cl_Password = passwordEncriptado;
                    cliente.Cl_Estado = 1;
                    var clienteInsertar = Mapper.Map<DATA.Clientes>(cliente);
                    clien.InsertarClientes(clienteInsertar);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error_feria", "No se pudo agregar la feria.");
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
                if (cliente.Provincias.Pro_id == 0)
                {
                    ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                }
                else
                {

                    var listaCanton = can.ListarCantones();
                    IEnumerable<SelectListItem> selectCantones =
                    from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(cliente.Provincias.Pro_id))
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString()
                    };

                    ViewBag.Cantones = selectCantones;
                }

                if (cliente.Cantones.cnt_id != 0)
                {
                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(cliente.Cantones.cnt_id))
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

        public ActionResult EditarCliente(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return RedirectToAction("Index", "Clientes");

            var cliente = clien.BuscarClientes(cedula);
            var infoCliente = Mapper.Map<Models.Clientes>(cliente);

            if (infoCliente == null)
                throw new Exception("Información de cliente no encontrada");

            List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
            var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

            List<DATA.Cantones> listaCantones = can.ListarCantones();
            var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

            List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
            var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

            infoCliente.Distritos = distritos.Where(x => x.dis_id == infoCliente.Cl_Distrito).FirstOrDefault();
            infoCliente.Cantones = cantones.Where(x => x.cnt_id == infoCliente.Distritos.dis_canton).FirstOrDefault();
            infoCliente.Provincias = provincias.Where(x => x.Pro_id == infoCliente.Cantones.cnt_provincia).FirstOrDefault();
            ViewData["idCanton"] = infoCliente.Cantones.cnt_id;
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
            from c in listaCanton.Where(x => x.cnt_provincia == infoCliente.Provincias.Pro_id)
            select new SelectListItem
            {
                Text = c.cnt_nombre,
                Value = c.cnt_id.ToString(),
            };

            ViewBag.Cantones = selectCantones;

            var listaDistrito = dis.ListarDistritos();
            IEnumerable<SelectListItem> selectDistritos =
            from c in listaDistrito.Where(x => x.dis_canton == infoCliente.Cantones.cnt_id)
            select new SelectListItem
            {
                Text = c.dis_nombre,
                Value = c.dis_id.ToString(),
            };

            ViewBag.Distritos = selectDistritos;
            Session["infoCliente"] = infoCliente;

            return View(infoCliente);
        }
        [HttpPost]
        public ActionResult EditarCliente(Models.Clientes cliente)
        {

            Models.Clientes infoCliente = (Models.Clientes)Session["infoCliente"];
            
            if (ModelState.IsValid)
            {
                
                var clienteActualizar = Mapper.Map<DATA.Clientes>(cliente);
                clienteActualizar.Cl_Estado = infoCliente.Cl_Estado;
                clienteActualizar.Cl_Password = infoCliente.Cl_Password;
                clienteActualizar.Cl_Password = infoCliente.Cl_Password;

                clien.ActualizarClientes(clienteActualizar);
                
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["idCanton"] = infoCliente.Cantones.cnt_id;
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

                if (infoCliente.Provincias.Pro_id == 0)
                {
                    ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                }
                else
                {

                    var listaCanton = can.ListarCantones();
                    IEnumerable<SelectListItem> selectCantones =
                    from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(infoCliente.Provincias.Pro_id))
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString()
                    };

                    ViewBag.Cantones = selectCantones;
                }

                if (infoCliente.Cantones.cnt_id != 0)
                {
                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(infoCliente.Cantones.cnt_id))
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

        public ActionResult Edit(string cedula)
        {
            if(string.IsNullOrEmpty(cedula))
                return RedirectToAction("Index", "Clientes");

            var cliente = clien.BuscarClientes(cedula);
            var infoCliente = Mapper.Map<Models.Clientes>(cliente);

            if (infoCliente == null)
                throw new Exception("Información de cliente no encontrada");

            List<DATA.Distritos> listaDistritos = dis.ListarDistritos();
            var distritos = Mapper.Map<List<Models.Distritos>>(listaDistritos);

            List<DATA.Cantones> listaCantones = can.ListarCantones();
            var cantones = Mapper.Map<List<Models.Cantones>>(listaCantones);

            List<DATA.Provincias> listaProvincias = pro.ListarProvincias();
            var provincias = Mapper.Map<List<Models.Provincias>>(listaProvincias);

            infoCliente.Distritos = distritos.Where(x => x.dis_id == infoCliente.Cl_Distrito).FirstOrDefault();
            infoCliente.Cantones = cantones.Where(x => x.cnt_id == infoCliente.Distritos.dis_canton).FirstOrDefault();
            infoCliente.Provincias = provincias.Where(x => x.Pro_id == infoCliente.Cantones.cnt_provincia).FirstOrDefault();
            ViewData["idCanton"] = infoCliente.Cantones.cnt_id;
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
            from c in listaCanton.Where(x => x.cnt_provincia == infoCliente.Provincias.Pro_id)
            select new SelectListItem
            {
                Text = c.cnt_nombre,
                Value = c.cnt_id.ToString(),
            };

            ViewBag.Cantones = selectCantones;

            var listaDistrito = dis.ListarDistritos();
            IEnumerable<SelectListItem> selectDistritos =
            from c in listaDistrito.Where(x => x.dis_canton == infoCliente.Cantones.cnt_id)
            select new SelectListItem
            {
                Text = c.dis_nombre,
                Value = c.dis_id.ToString(),
            };

            ViewBag.Distritos = selectDistritos;
            Session["infoCliente"] = infoCliente;

            return View(infoCliente);
        }

        [HttpPost]
        public ActionResult Edit(Models.Clientes cliente)
        {

            Models.Clientes infoCliente = (Models.Clientes)Session["infoCliente"];
            if (ModelState.IsValid)
            {
                var clienteActualizar = Mapper.Map<DATA.Clientes>(cliente);
                clienteActualizar.Cl_Estado = infoCliente.Cl_Estado;
                var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(cliente.Cl_Password);
                clienteActualizar.Cl_Password = passwordEncriptado;
                clien.ActualizarClientes(clienteActualizar);
                return RedirectToAction("Index","SeleccionDeFeria");

            }
            else
            {
                ViewData["idCanton"] = infoCliente.Cantones.cnt_id;
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

                if (infoCliente.Provincias.Pro_id == 0)
                {
                    ViewBag.Cantones = Enumerable.Empty<SelectListItem>();
                }
                else
                {

                    var listaCanton = can.ListarCantones();
                    IEnumerable<SelectListItem> selectCantones =
                    from c in listaCanton.Where(x => x.cnt_provincia == Convert.ToInt32(infoCliente.Provincias.Pro_id))
                    select new SelectListItem
                    {
                        Text = c.cnt_nombre,
                        Value = c.cnt_id.ToString()
                    };

                    ViewBag.Cantones = selectCantones;
                }

                if (infoCliente.Cantones.cnt_id != 0)
                {
                    var listaDistrito = dis.ListarDistritos();
                    IEnumerable<SelectListItem> selectDistritos =
                    from c in listaDistrito.Where(x => x.dis_canton == Convert.ToInt32(infoCliente.Cantones.cnt_id))
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

    }
}
