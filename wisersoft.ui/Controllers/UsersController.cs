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
    public class UsersController : Controller
    {
        IUsers use;

        public UsersController()
        {
            use = new MUsers();
        }

        //
        // GET: /Users/

        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                var listaUsers = use.ListarUsers();

                var users = Mapper.Map<List<Models.Users>>(listaUsers);
                return View(users);
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
        public ActionResult Create(Models.Users users)
        {

            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                try
                {
                    if (ModelState.IsValid)
                    { 
                        //Encripta el password
                        var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(users.Us_Password);
                        //Asigna la variable encriptada al objeto Password
                        users.Us_Password = passwordEncriptado;

                        var usersInsertar = Mapper.Map<DATA.Users>(users);
                        use.InsertarUsers(usersInsertar);
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

        public ActionResult Edit(string us_User_Name)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                var users = use.BuscarUsers(us_User_Name);
                var usersBuscar = Mapper.Map<Models.Users>(users);
                //Decripta el password
                var passwordDecriptado = Encriptacion.Encriptacion.Decriptar(usersBuscar.Us_Password);
                //Asigna la variable decriptada al objeto Password
                usersBuscar.Us_Password = passwordDecriptado;
                usersBuscar.ConfirmPassowrd = passwordDecriptado;

                return View(usersBuscar);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Models.Users users)
        {
            if (Session["UserID"] != null && Session["Type"].Equals("admin"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Encripta el password
                        var passwordEncriptado = Encriptacion.Encriptacion.Encriptar(users.Us_Password);
                        //Asigna la variable encriptada al objeto Password
                        users.Us_Password = passwordEncriptado;

                        var usersEditar = Mapper.Map<DATA.Users>(users);
                        use.ActualizarUsers(usersEditar);
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("error", "No se ha podido actualizar");
                    }
                   
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Delete(string us_User_Name)
        {
            use.EliminarUsers(us_User_Name);
            return RedirectToAction("Index");
        }

    }
}
