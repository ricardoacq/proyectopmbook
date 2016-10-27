using MvcLogin.Models;
using System;
using System.Collections.Generic;
using MvcLogin.Linq;
using System.Linq;


namespace MvcLogin.Areas.Ejecucion.Models
{
    public class ProyectosModel : CargaInicial
    {
         public ProyectosModel()
        {

        }
         public DTO_Proyectos_Result ObtenerProyecto(PMBookDataContext DB, int nProyecto)
         {
             DTO_Proyectos_Result Result = new DTO_Proyectos_Result()
             {
                 bError = false,
                 msgErr = string.Empty
             };

             try
             {
                 Result.Proyecto = (from c in DB.PMB_Proyectos

                                     where c.nProyecto == nProyecto
                                     && c.nEmisor == DataSesion.nEmisor
                                    select new DTO_Proyecto()
                                     {
                                         nProyecto = c.nProyecto,
                                         cClaveERP = c.cClaveERP,
                                         cDescripcion = c.cDescripcion,
                                         tDescripcion = c.tDescripcion,
                                         cUrlRepositorio = c.cUrlRepositorio,
                                         nEstatus = c.nEstatus,
                                         bActivo = c.bActivo,
                                         nFechaFin = c.nFechaFin,
                                         nFechaInicio = c.nFechaInicio,
                                         bProyectoSoporte = c.bProyectoSoporte,
                                         tAdaptaciones = c.tAdaptaciones

                                     }).SingleOrDefault();
             }
             catch (Exception e)
             {
                 Result.bError = true;
                 Result.msgErr = e.ToString();
             }

             return Result;

         }

         public DTO_Proyectos_Result ObtenerProyectos(PMBookDataContext DB, bool MostrarInactivos)
         {
             DTO_Proyectos_Result Result = new DTO_Proyectos_Result()
             {
                 bError = false,
                 msgErr = string.Empty
             };

             try
             {
                 Result.Proyecto = (from c in DB.PMB_Proyectos

                                    where c.nEmisor == DataSesion.nEmisor
                                    && (c.bActivo || MostrarInactivos)
                                    select new DTO_Proyecto()
                                    {
                                        nProyecto = c.nProyecto,
                                        cClaveERP = c.cClaveERP,
                                        cDescripcion = c.cDescripcion,
                                        tDescripcion = c.tDescripcion,
                                        cUrlRepositorio = c.cUrlRepositorio,
                                        nEstatus = c.nEstatus,
                                        bActivo = c.bActivo,
                                        nFechaFin = c.nFechaFin,
                                        nFechaInicio = c.nFechaInicio,
                                        bProyectoSoporte = c.bProyectoSoporte,
                                        tAdaptaciones = c.tAdaptaciones

                                    }).SingleOrDefault();
             }
             catch (Exception e)
             {
                 Result.bError = true;
                 Result.msgErr = e.ToString();
             }

             return Result;
         }

         public DTO_Proyectos_Result EliminarProyecto(PMBookDataContext DB, int nProyecto)
         {
             DTO_Proyectos_Result Result = new DTO_Proyectos_Result()
             {
                 bError = false,
                 msgErr = string.Empty
             };

             try
             {
                 PMB_Proyecto Proyecto = DB.PMB_Proyectos.Where(x => x.nProyecto == nProyecto).SingleOrDefault();
                 Proyecto.bActivo = false;
                 DB.SubmitChanges();
             }
             catch (Exception e)
             {
                 Result.bError = true;
                 Result.msgErr = e.ToString();
             }
             return Result;
         }

         public DTO_Proyectos_Result GuardarProyecto(PMBookDataContext DB, DTO_Proyecto Proyecto)
         {
             DTO_Proyectos_Result Result = new DTO_Proyectos_Result()
             {
                 bError = false,
                 msgErr = string.Empty
             };

             try
             {
                 if (DB.PMB_Proyectos.Where(x => x.nProyecto != Proyecto.nProyecto).Select(x => x.cDescripcion).ToList().Contains(Proyecto.cDescripcion))
                 {
                     Result.bError = true;
                     Result.msgErr = "La descripción del Proyecto ya existe, no pueden existir registros repetidos.";
                     return Result;
                 }

                 PMB_Proyecto RegistroProyecto = DB.PMB_Proyectos.Where(x => x.nProyecto == Proyecto.nProyecto).SingleOrDefault() ?? new PMB_Proyecto();


                 RegistroProyecto.tDescripcion = Proyecto.tDescripcion;
                 RegistroProyecto.cDescripcion = Proyecto.cDescripcion;
                 RegistroProyecto.tDescripcion = Proyecto.tDescripcion;
                 RegistroProyecto.cUrlRepositorio = Proyecto.cUrlRepositorio;
                 RegistroProyecto.nEstatus = Proyecto.nEstatus;
                 RegistroProyecto.bActivo = Proyecto.bActivo;
                 RegistroProyecto.nFechaFin = Proyecto.nFechaFin;
                 RegistroProyecto.nFechaInicio = Proyecto.nFechaInicio;
                 RegistroProyecto.bProyectoSoporte = Proyecto.bProyectoSoporte;
                 RegistroProyecto.tAdaptaciones = Proyecto.tAdaptaciones;

                 if (RegistroProyecto.nProyecto == 0)
                 {//nuevo
                     RegistroProyecto.cClaveERP = DB.PMB_Actividades.Where(x => x.nEmisor == DataSesion.nEmisor && x.bActivo).ToList().Count + 1 + "";
                     RegistroProyecto.cUsuario_Registro = DataSesion.cLogin;
                     RegistroProyecto.cMaquina_Registro = Environment.MachineName;
                     RegistroProyecto.dFecha_Registro = DateTime.Now;
                     RegistroProyecto.nEmisor = DataSesion.nEmisor;

                     DB.PMB_Proyectos.InsertOnSubmit(RegistroProyecto);
                 }
                 DB.SubmitChanges();
                 Result.Proyecto = new DTO_Proyecto() { nProyecto = RegistroProyecto.nProyecto, cDescripcion = RegistroProyecto.cDescripcion, cClaveERP = RegistroProyecto.cClaveERP };
             }
             catch (Exception e)
             {
                 Result.bError = true;
                 Result.msgErr = e.ToString();
             }
             return Result;
         }

    }

    public class DTO_Proyecto
    {

        public int nProyecto { get; set; }
        public string cClaveERP { get; set; }
        public string cDescripcion { get; set; }
        public string tDescripcion { get; set; }
        public string cUrlRepositorio { get; set; }
        public byte nEstatus { get; set; }
        public bool bActivo { get; set; }
        public long nFechaFin { get; set; }
        public long nFechaInicio { get; set; }
        public bool bProyectoSoporte { get; set; }
        public string tAdaptaciones { get; set; }
    }

    public class DTO_Proyectos_Result
    {
        public bool bError { get; set; }
        public string msgErr { get; set; }
        public DTO_Proyecto Proyecto { get; set; }
        public List<DTO_Proyecto> Proyectos { get; set; }
    }
}