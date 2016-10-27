using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class DTO_UnidadMedica
	{
		public int nEmisor { get; set; }
		public int nUnidadMedica { get; set; }
		public string cClaveERP { get; set; }
		public string cDescripcion { get; set; }
		public int nCiudad { get; set; }
		public int nAsentamiento { get; set; }
		public int nIDCodigoPostal { get; set; }
		public string cDomicilio { get; set; }
		public int nCodigoPostal { get; set; }
		public string cTelefono { get; set; }
		public int bActivo { get; set; }

		public DTO_UnidadMedica()
		{

		}
	}
}