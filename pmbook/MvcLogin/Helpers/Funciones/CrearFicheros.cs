using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcLogin.Helpers.Funciones
{
    public class CrearFicheros
    {
        public CrearFicheros()
        { 
        
        
        }

        public bool CreaController(string cRutaController, string cControllerBase)
        {
            bool bControllerGuardado = false;

            // crear el path
            //var archivo = "/Content/nuevo.txt";

            ;
            // eliminar el fichero si ya existe
            if (File.Exists(cRutaController))
                File.Delete(cRutaController);

            // crear el fichero
            //File.Create(cRutaController);
            //using (var fileStream = File.Create(cRutaController))
            //{
                
            //    cController.Replace("\n", Environment.NewLine);
            //    var texto = new UTF8Encoding(true).GetBytes(cControllerBase);
            //    fileStream.Write(texto, 0, texto.Length);
            //    fileStream.Flush();
            //}

            using (StreamWriter file = new StreamWriter(cRutaController, true))
            {
                var renglones = cControllerBase.Split('*');
                foreach (var r in renglones)
                {
                    file.WriteLine(r);
                }
                
            }


            
            //File.GetAccessControl(cRutaController);

            return bControllerGuardado;
        }

    }
}