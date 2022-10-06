using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiserSoft.DATA;

namespace WiserSoft.DAL.Interfaces
{
    public interface IClientes
    {
        List<Clientes> ListarClientes();
        Clientes BuscarClientes(String Cl_Cedula);
        void InsertarClientes(Clientes clientes);
        void ActualizarClientes(Clientes clientes);
        void EliminarClientes(String Cl_Cedula);
        bool BuscarClientes(string cl_user_name, string cl_password);
        Clientes BuscarClientePorLogin(string cl_user_name, string cl_password);
        Clientes BuscarClienteExistentePorUsuario(string usuario);
        Clientes BuscarClienteExistentePorCedula(string cedula);
        Clientes BuscarClienteExistentePorCorreo(string correo);
    }
}
