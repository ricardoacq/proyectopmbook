using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.DTO
{
    public class DTO_CentroLogistico
    {
        public int nIdCentroLogistico { get; set; }
        public string cDescripcion { get; set; }
        public string cClaveERP { get; set; }
        public DTO_CentroLogistico()
        { 
        
        }
    }

    public class DTO_Material
    {
        public int nIdMaterial { get; set; }
        public string cDescripcion { get; set; }
        public string cClaveERP { get; set; }
        public int nIdGrupo { get; set; }
        public DTO_Material()
        {

        }
    }

    public class DTO_GrupoArticulo
    {
        public int nIdGrupo { get; set; }
        public string cDescripcion { get; set; }
        public string cClaveERP { get; set; }
        public DTO_GrupoArticulo()
        {

        }
    }
}