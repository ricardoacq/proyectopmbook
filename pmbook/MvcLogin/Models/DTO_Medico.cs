using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class DTO_Medico
	{
		public int nEmisor { get; set; }
		public int nMedico { get; set; }
		public string cClaveERP { get; set; }
		public string cNombre { get; set; }
		public string cApellidoParterno { get; set; }
		public string cApellidoMaterno { get; set; }
		public string cNombreEnReceta { get; set; }
		public string cCedulaProfesional { get; set; }
		public string cRFC { get; set; }
		public string cSexo { get; set; }
		public int nEspecialidad { get; set; }
		public int nCiudad { get; set; }
		public int nAsentamiento { get; set; }
		public string cCodigoPostal { get; set; }
		public int nIDCodigoPostal { get; set; }
		public string cDomicilio { get; set; }
		public string cTelefono { get; set; }
		public string cCelular1 { get; set; }
		public string cCelular2 { get; set; }
		public string cLogin { get; set; }
		public bool bActivo { get; set; }

		public DTO_Medico()
		{

		}

	}
}