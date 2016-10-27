using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLogin.Helpers.Funciones
{
	public class ConvertidorJuliano
	{
		public static long aJuliano(DateTime fecha)
		{
			DateTime vFechaPivote = new DateTime(1900, 1, 1);
			long vDiasTranscurridos = (long)(fecha - vFechaPivote).TotalDays;//Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, vFechaPivote, fecha, Microsoft.VisualBasic.FirstDayOfWeek.Monday, Microsoft.VisualBasic.FirstWeekOfYear.System);
			vDiasTranscurridos += 2415021;
			return vDiasTranscurridos;
		}

		public static DateTime deJuliano(long nFechaJuliana)
		{
			nFechaJuliana -= 2415021;
			DateTime vFechaPivote = new DateTime(1900, 1, 1);
			vFechaPivote = vFechaPivote.AddDays(nFechaJuliana);
			//long vDiasTranscurridos = (long)(vFechaPivote - fecha).TotalDays;//Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, vFechaPivote, fecha, Microsoft.VisualBasic.FirstDayOfWeek.Monday, Microsoft.VisualBasic.FirstWeekOfYear.System);
			//vDiasTranscurridos += 2415021;
			return vFechaPivote;
		}

        public static string HoraJuliana(long nHoraJuliana)
        {
            var hora = nHoraJuliana / 60;
            var minutos = (decimal)(nHoraJuliana % 60);
            var cMinutos = "";
            if (minutos.ToString().Length == 1)
                cMinutos = "0" + minutos;
            else
                cMinutos = minutos.ToString();
            var cHora = hora.ToString() + ":" + cMinutos.ToString();
            return cHora;
        }

        public static long aHoraJuliana(DateTime hora)
        {
            return hora.Hour * 60 + hora.Minute;
        }
	}
}