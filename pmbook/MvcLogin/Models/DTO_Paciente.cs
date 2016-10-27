using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
	public class DTO_Paciente
	{
		public int nEmisor { get; set; }
		public int nPaciente { get; set; }
		public string cClaveERP { get; set; }
		public string cNombre { get; set; }
		public string cApellidoPaterno { get; set; }
		public string cApellidoMaterno { get; set; }
		public int nFechaAlta { get; set; }
		public DateTime dFechaAlta { get; set; }
		public string cTipoSangre { get; set; }
		public string cSexo { get; set; }
		public int nFechaNacimiento { get; set; }
		public DateTime dFechaNacimiento { get; set; }
		public string cLugarNacimiento { get; set; }
		public string cCURP { get; set; }
		public string cReferidoPor { get; set; }
		public int? nEstadoCivil { get; set; }
		public string cCorreoElectronico { get; set; }
		public string cMonederoElectronico { get; set; }
		public int nCiudad { get; set; }
		public int nAsentamiento { get; set; }
		public int nIDCodigoPostal { get; set; }
		public string cDomicilio { get; set; }
		public string cCodigoPostal { get; set; }
		public string cTelefonoParticular { get; set; }
		public string cTelefonoOficina { get; set; }
		public string cTelefonoCelular { get; set; }
		public string cNombreDelPadre { get; set; }
		public string cNombreDeLaMadre { get; set; }
		public string cCelularDelPadre { get; set; }
		public string cCelularDeLaMadre { get; set; }
		public string cCorreoElectronicoDelPadre { get; set; }
		public string cCorreoElectronicoDeLaMadre { get; set; }
		public string cRazonSocial { get; set; }
		public string cRFC { get; set; }
		public int? nCiudadFiscal { get; set; }
		public int? nAsentamientoFiscal { get; set; }
		public int? nIDCodigoPostalFiscal { get; set; }
		public string cCodigoPostalFiscal { get; set; }
		public string cDomicilioFiscal { get; set; }
		public int nMedico { get; set; }
		public int nUnidadMedica { get; set; }
		public int nFechaUltimaConsulta { get; set; }
		public DateTime dFechaUltimaConsulta { get; set; }
		public int? nMedicoUltimaConsulta { get; set; }
		public int? nUnidadMedicaUltimaConsulta { get; set; }
		public bool bActivo { get; set; }
		public string cUsuario_Registro { get; set; }
		public DateTime dFecha_Registro { get; set; }
		public string cMaquina_Registro { get; set; }
		public Pais Pais { get; set; }
		public Estado Estado { get; set; }
		public Municipio Municipio { get; set; }
		public Ciudad Ciudad { get; set; }
		public Asentamiento Asentamiento { get; set; }
		public DTO_Medico Medico { get; set; }
		public DTO_UnidadMedica UnidadMedica { get; set; }

		public DTO_Paciente()
		{

		}
	}
}