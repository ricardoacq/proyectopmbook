using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.DTO
{
    public class DTO_Filtros
    {
        public DateTime dFechaDesde { get; set; }
        public DateTime dFechaHasta { get; set; }
        public string cTextoBreve { get; set; }
        public DTO_Material Material { get; set; }
        public DTO_GrupoArticulo Grupo { get; set; }
        public DTO_CentroLogistico CentroLogistico { get; set; }
        public bool bConsumo { get; set; }
        public bool bMerma { get; set; }

        public DTO_Filtros()
        {
            Material = new DTO_Material();
            Grupo = new DTO_GrupoArticulo();
            CentroLogistico = new DTO_CentroLogistico();
        }
    }
}