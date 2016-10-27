using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class DataSesion
    {
        public int nIdUsuario { get; set; }
        public string cLogin { get; set; }
        public bool bSesionActiva { get; set; }
        public int nEmisor { get; set; }
        public DataSesion()
        {
            nIdUsuario = 1;
            bSesionActiva = false;
        }
    }
}