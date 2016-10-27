using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class Ciudad
	{
		public string cCiudad { get; set; }
		public string cClave { get; set; }
		public string cEstado { get; set; }
		public string cMunicipio { get; set; }
		public string cPais { get; set; }
		public int nCiudad { get; set; }
		public int nEstado { get; set; }
		public int nMunicipio { get; set; }
		public int nPais { get; set; }

		public Ciudad()
		{

		}
	}
}