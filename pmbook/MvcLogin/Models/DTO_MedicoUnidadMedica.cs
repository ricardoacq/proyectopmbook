using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class DTO_MedicoUnidadMedica
	{
		public int nMedicoUnidadMedica { get; set; }
		public int nEmisor { get; set; }
		public string cClaveERP { get; set; }
		public int nMedico { get; set; }
		public int nUnidadMedica { get; set; }
		public string cDescripcion { get; set; }
		public bool bActivo { get; set; }

		public DTO_MedicoUnidadMedica()
		{

		}
	}
}