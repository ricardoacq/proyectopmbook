using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.DTO
{
    public class DTO_BuquedaSalidas
    {
        public string Fecha { get; set; }
        public string Material { get; set; }
        public string TextoBreveMaterial { get; set; }
        public string GrupoDeeArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedidaBase { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Total { get; set; }
        public string cTipo { get; set; }
        public string cColor { get; set; }
        public bool bConsumo { get; set; }
        public bool bMerma { get; set; }


        public DTO_BuquedaSalidas()
        { 
        
        
        }
    }
}