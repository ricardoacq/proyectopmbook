using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.DTO
{
    public class DTO_Reporte
    {

        public dynamic lstRegistrosReporte { get; set; }
        public string cNombreReporte { get; set; }
        public string cNombreCentroLogistico { get; set; }
        public string cNombreGrupoArticulo { get; set; }
        public string cIdMaterial { get; set; }
        public string cPeriodo { get; set; }
        public string cTotal { get; set; }

        public DTO_Reporte()
        { 
        
        }
    }
}