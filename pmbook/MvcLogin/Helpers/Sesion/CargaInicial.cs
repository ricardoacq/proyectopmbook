using MvcLogin.Helpers.Sesion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class CargaInicial
    {
        public DataSesion DataSesion { get; set; }
        public List<DataArea> DataNav { get; set; }

        public CargaInicial()
        {
            DataSesion = (DataSesion)HttpContext.Current.Session["DataSesion"];
            DataNav = (List<DataArea>)HttpContext.Current.Session["DataNav"];
        
        }
    }
}