using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class Estado
	{
		public string cClave { get; set; }
		public string cEstado { get; set; }
		public string cPais { get; set; }
		public int nEstado { get; set; }
		public int nPais { get; set; }

		public Estado()
		{

		}
	}
}