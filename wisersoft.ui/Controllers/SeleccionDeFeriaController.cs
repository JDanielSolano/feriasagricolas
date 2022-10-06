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
    public class SeleccionDeFeriaController : Controller
    {
        IFerias_Agricolas fer;
        IClientes cli;
        // GET: Ferias_Agricolas

        public SeleccionDeFeriaController()
        {
            fer = new MFerias_Agricolas();
            cli = new MClientes();
        }
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["Type"].Equals("cliente"))
            {
                List<DATA.Ferias_Agricolas> listaFerias = fer.ListarFerias_Agricolas();
                var ferias = Mapper.Map<List<Models.Ferias_Agricolas>>(listaFerias);
                String feriasData = "";
                foreach (Models.Ferias_Agricolas feria in ferias)
                {
                    feriasData += feria.Fa_Id + "," + feria.Fa_Latitud + "," + feria.Fa_longitud + "," + feria.Fa_Nombre + "~";
                }

                var cliente = cli.BuscarClienteExistentePorUsuario(Session["UserID"].ToString());
                var infoCliente = Mapper.Map<Models.Clientes>(cliente);

                ViewData["ferias"] = feriasData;
                ViewData["cliente"] = infoCliente.Cl_Latitude+","+infoCliente.Cl_Longitud;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Seleccion(int idFeria)
        {
            Session["Feria"] = idFeria;
            //return RedirectToAction("Index");
            return Json("Correcto", JsonRequestBehavior.AllowGet);
        }
    }
}