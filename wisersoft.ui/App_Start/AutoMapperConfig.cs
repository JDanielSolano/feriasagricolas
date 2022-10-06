using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WiserSoft.UI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Ferias_Agricolas, DATA.Ferias_Agricolas>();
                cfg.CreateMap<DATA.Ferias_Agricolas, Models.Ferias_Agricolas>();

                cfg.CreateMap<Models.Distritos, DATA.Distritos>();
                cfg.CreateMap<DATA.Distritos, Models.Distritos>();

                cfg.CreateMap<Models.Cantones, DATA.Cantones>();
                cfg.CreateMap<DATA.Cantones, Models.Cantones>();

                cfg.CreateMap<Models.Provincias, DATA.Provincias>();
                cfg.CreateMap<DATA.Provincias, Models.Provincias>();

                cfg.CreateMap<Models.Productos, DATA.Productos>();
                cfg.CreateMap<DATA.Productos, Models.Productos>();

                cfg.CreateMap<Models.Clasificacion, DATA.Clasificacion>();
                cfg.CreateMap<DATA.Clasificacion, Models.Clasificacion>();

                cfg.CreateMap<Models.Medidas, DATA.Medidas>();
                cfg.CreateMap<DATA.Medidas, Models.Medidas>();

                cfg.CreateMap<Models.Users, DATA.Users>();
                cfg.CreateMap<DATA.Users, Models.Users>();

                cfg.CreateMap<Models.Clientes, DATA.Clientes>();
                cfg.CreateMap<DATA.Clientes, Models.Clientes>();

                cfg.CreateMap<Models.Compras, DATA.Compras>();
                cfg.CreateMap<DATA.Compras, Models.Compras>();

                cfg.CreateMap<Models.Agricultores, DATA.Agricultores>();
                cfg.CreateMap<DATA.Agricultores, Models.Agricultores>();

                cfg.CreateMap<Models.Productos_Por_Agricultor, DATA.Productos_Por_Agricultor>();
                cfg.CreateMap<DATA.Productos_Por_Agricultor, Models.Productos_Por_Agricultor>();

                cfg.CreateMap<Models.Canastas, DATA.Canastas>();
                cfg.CreateMap<DATA.Canastas, Models.Canastas>();

            });
        }
    }
}