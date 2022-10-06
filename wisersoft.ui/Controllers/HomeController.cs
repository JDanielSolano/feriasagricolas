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
    public class HomeController : Controller
    {
        IUsers use;
        IClientes cli;
        IProductos prod;
        IProductos_Por_Agricultor prod_x_agr;
        ICanastas can;

        public HomeController()
        {
            use = new MUsers();
            cli = new MClientes();
            prod = new MProductos();
            prod_x_agr = new MProductos_Por_Agricultor();
            can = new MCanastas();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Users users)
        {
            var passwordEncripted = Encriptacion.Encriptacion.Encriptar(users.Us_Password);
            var loginResultClients = cli.BuscarClientePorLogin(users.Us_User_Name, passwordEncripted);
            var loginResultUsers = use.BuscarUsers(users.Us_User_Name, passwordEncripted);
            if (loginResultClients != null)
            { 
                //Si es nulo, entonces no existe.
                Session["UserID"] = users.Us_User_Name;
                Session["Nombre"] = loginResultClients.Cl_Nombre?? users.Us_User_Name; //Si el campo de nombre es nulo, entonces se muestra el usuario.
                Session["Type"] = "cliente";
                Session["Cedula"] = loginResultClients.Cl_Cedula ?? "0"; //Si el campo es nulo, pone un 0 por valor default.
                return RedirectToAction("Index", "SeleccionDeFeria");
            }
            else if (loginResultUsers)
            {
                Session["UserID"] = users.Us_User_Name;
                Session["Type"] = "admin";
                return RedirectToAction("UserDashboard");
            }
            else
            {
                ModelState.AddModelError("errorLogin", "Usuario y/o contrasena incorrectos");
                return View("Index");
            }
        }

        public ActionResult UserDashboard()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                ViewBag.UserId = Session["UserID"];


                //Grafico Pie Tipos de Productos 
                List<DATA.Productos> listaProductos = prod.ListarProductos();
                var producto = listaProductos.Select(x => x.Pdt_tipo).Distinct();

                List<int> listaProducto = new List<int>();
                foreach (var item in producto)
                {
                    listaProducto.Add(listaProductos.Count(x => x.Pdt_tipo == item));
                }

                var rep = listaProducto;
                ViewBag.Tipos = producto;
                ViewBag.Contenido_Tipos = listaProducto.ToList();

                // Gráfico de Pie Productos Por agricultor
                List<DATA.Productos_Por_Agricultor> listaProductosPorAgricultor = prod_x_agr.ListarProductos_Por_Agricultor();
                var producto_x_agricultor = listaProductosPorAgricultor.Select(x => x.Ppa_Id_Agricultor).Distinct();

                List<int> listaProductoPorAgricultor = new List<int>();
                foreach (var item in producto_x_agricultor)
                {
                    listaProductoPorAgricultor.Add(listaProductosPorAgricultor.Count(x => x.Ppa_Id_Agricultor == item));
                }

                var rep2 = listaProductoPorAgricultor;
                ViewBag.Agricultores = producto_x_agricultor;
                ViewBag.Contenido_Agricultores = listaProductoPorAgricultor.ToList();

                // Gráfico de Pie Canastas por Usuario
                List<DATA.Canastas> listaCanastas = can.ListarCanastas();
                var canasta = listaCanastas.Select(x => x.Can_usuario).Distinct();

                List<int> listaCanasta = new List<int>();
                foreach (var item in canasta)
                {
                    listaCanasta.Add(listaCanastas.Count(x => x.Can_usuario == item));
                }

                var rep3 = listaCanasta;
                ViewBag.Usuarios = canasta;
                ViewBag.Contenido_Canasta = listaCanasta.ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ClientDashboard()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("cliente"))
            {
                ViewBag.UserId = Session["UserID"];

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}