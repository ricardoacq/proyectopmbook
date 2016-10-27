using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class ResponseObject
	{
		public bool bExito { get; set; }
		public string cMensaje { get; set; }

		public ResponseObject()
		{

		}
	}
}