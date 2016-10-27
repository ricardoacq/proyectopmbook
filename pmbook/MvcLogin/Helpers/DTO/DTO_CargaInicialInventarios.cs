using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.DTO
{
    public class DTO_CargaInicialInventarios
    {
        public bool bError { get; set; }
        public string cMensaje { get; set; }

        public List<DTO_CentroLogistico> lstCentrosLogisticos { get; set; }
        public List<DTO_Material> lstMateriales { get; set; }
        public List<DTO_GrupoArticulo> lstGrupos { get; set; }

        public DTO_CargaInicialInventarios()
        {
            bError = false;
            cMensaje = "";
            lstCentrosLogisticos = new List<DTO_CentroLogistico>();
            lstGrupos = new List<DTO_GrupoArticulo>();
            lstMateriales = new List<DTO_Material>();
        }
    }
}