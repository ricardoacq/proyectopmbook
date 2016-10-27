using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.Sesion
{
    public class DataArea
    {
        public string cArea { get; set; }
        public string cController { get; set; }
        public string cModulo { get; set; }
        public string cURL { get; set; }
        public string cImagen { get; set; }
        public List<Modulo> lstModulos { get; set; }

        public DataArea()
        {
            this.lstModulos = new List<Modulo>();
        }
    }

    public class Modulo
    {
        public string cController { get; set; }
        public string cModulo { get; set; }
        public string cURL { get; set; }
        public string cImagen { get; set; }
        public Modulo()
        {
        }
    }
}