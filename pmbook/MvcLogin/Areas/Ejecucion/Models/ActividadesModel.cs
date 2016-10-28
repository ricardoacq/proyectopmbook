using MvcLogin.Models;
using System;
using System.Collections.Generic;
using MvcLogin.Linq;
using System.Linq;

namespace MvcLogin.Areas.Ejecucion.Models
{
    public class ActividadesModel : CargaInicial
    {
        public ActividadesModel()
        {
        }

        public DTO_Actividad_Result ObtenerActividad(PMBookDataContext DB, int nActividad)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Actividad = (from c in DB.PMB_Actividades
                                 
                                      where c.nActividad == nActividad
                                      && c.nEmisor == DataSesion.nEmisor
                                      select new DTO_Actividad()
                                      {
                                          bActivo = c.bActivo,
                                          bFavorito = c.bFavorito,
                                          bIncidenteCobrable = c.bIncidenteCobrable,
                                          bIncidente = c.bIncidente,
                                          bRequiereQC = (bool)c.bRequiereQC,
                                          bSprintFavorito = c.bSprintFavorito,
                                          bTerminado = c.bTerminado,
                                          cClaveERP = c.cClaveERP ?? string.Empty,
                                          cDescripcion = c.cDescripcion,
                                          cFechaCierre = (c.dFechaCierre != null) ? ((DateTime)c.dFechaCierre).ToShortDateString() : "Sin fecha de cierre",
                                          nActividad = c.nActividad,
                                          nActividadForward = c.nActvidadForward ?? 0,
                                          nEmisor = (int)c.nEmisor,
                                          nAvancePorcentualEstimado = (double)c.nAvancePorcentualEstimado,
                                          nEstatus = c.nEstatus,
                                          nEstimacionAutorizadaAdicionalHoras = (double)c.nEstimacionAutorizadaAdicionalHoras,
                                          nEstimacionAutorizadaHoras = (double)c.nEstimacionAutorizadaHoras,
                                          nEstimacionAutorizadaUE = (double)c.nEstimacionAutorizadaUE,
                                          nEstimacionBaseUE = (double)c.nEstimacionBaseUE,
                                          nEstimacionSolicitadaUE = (double)c.nEstimacionSolicitadaUE,
                                          nNivelAsociacion = c.nNivelAsociacion,
                                          nTrabajoRealizadoAdicionalHoras = (double)c.nTrabajoRealizadoAdicionalHoras,
                                          nTrabajoRealizadoHoras = (double)c.nTrabajoRealizadoHoras,
                                          nTrabajoRestanteHoras = (double)c.nTrabajoRestanteHoras,
                                          nVuelta = c.nVuelta,
                                          tDescripcion = c.tDescripcion   
                                      }).SingleOrDefault();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }

        public DTO_Actividad_Result ObtenerActividades(PMBookDataContext DB, int nFechaInicial, int nFechaFinal)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Actividades = (from c in DB.PMB_Actividades
                                       where c.nEmisor == DataSesion.nEmisor
                                       && (c.bActivo)
                                       select new DTO_Actividad()
                                       {
                                           
                                           bActivo                             = c.bActivo,
                                           bFavorito                           = c.bFavorito,
                                           bIncidenteCobrable                  = c.bIncidenteCobrable,
                                           bIncidente                          = c.bIncidente,
                                           bRequiereQC                         = c.bRequiereQC ?? false,
                                           bSprintFavorito                     = c.bSprintFavorito,
                                           bTerminado                          = c.bTerminado,   
                                           cClaveERP                           = c.cClaveERP ?? string.Empty,
                                           cDescripcion                        = c.cDescripcion,
                                           cFechaCierre                        = (c.dFechaCierre != null) ? ((DateTime) c.dFechaCierre).ToShortDateString() : "Sin fecha de cierre", 
                                           nActividad                          = c.nActividad,
                                           nActividadForward                   = c.nActvidadForward ?? 0,
                                           nEmisor                             = (int)c.nEmisor,
                                           nAvancePorcentualEstimado           = (double)c.nAvancePorcentualEstimado,
                                           nEstatus                            = c.nEstatus,
                                           nEstimacionAutorizadaAdicionalHoras = (double)c.nEstimacionAutorizadaAdicionalHoras,
                                           nEstimacionAutorizadaHoras          = (double)c.nEstimacionAutorizadaHoras,
                                           nEstimacionAutorizadaUE             = (double)c.nEstimacionAutorizadaUE,
                                           nEstimacionBaseUE                   = (double)c.nEstimacionBaseUE,
                                           nEstimacionSolicitadaUE             = (double)c.nEstimacionSolicitadaUE,
                                           nNivelAsociacion                    = c.nNivelAsociacion,
                                           nTrabajoRealizadoAdicionalHoras     = (double)c.nTrabajoRealizadoAdicionalHoras,
                                           nTrabajoRealizadoHoras              = (double)c.nTrabajoRealizadoHoras,
                                           nTrabajoRestanteHoras               = (double)c.nTrabajoRestanteHoras,
                                           nVuelta                             = c.nVuelta, 
                                           tDescripcion                        = c.tDescripcion   
                                       }).OrderBy(x => x.cDescripcion).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }

        public DTO_Actividad_Result EliminarActividad(PMBookDataContext DB, int nActividad)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                PMB_Actividade Actividad = DB.PMB_Actividades.Where(x => x.nActividad == nActividad).SingleOrDefault();
                Actividad.bActivo = false;
                DB.SubmitChanges();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }
            return Result;
        }

        public DTO_Actividad_Result GuardarActividad(PMBookDataContext DB, DTO_Actividad Actividad)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                if (DB.PMB_Actividades.Where(x => x.nActividad != Actividad.nActividad).Select(x => x.cDescripcion).ToList().Contains(Actividad.cDescripcion))
                {
                    Result.bError = true;
                    Result.msgErr = "La descripción de la Actividad ya existe, no pueden existir registros repetidos.";
                    return Result;
                }

                PMB_Actividade RegistroActividad = DB.PMB_Actividades.Where(x => x.nActividad == Actividad.nActividad).SingleOrDefault() ?? new PMB_Actividade();
               
                RegistroActividad.bActivo                             = Actividad.bActivo;
                RegistroActividad.bFavorito                           = Actividad.bFavorito;     
                RegistroActividad.bIncidenteCobrable                  = Actividad.bIncidenteCobrable;
                RegistroActividad.bIncidente                          = Actividad.bIncidente;
                RegistroActividad.bRequiereQC                         = Actividad.bRequiereQC;
                RegistroActividad.bSprintFavorito                     = Actividad.bSprintFavorito;
                RegistroActividad.bTerminado                          = Actividad.bTerminado;
                RegistroActividad.cDescripcion                        = Actividad.cDescripcion;
                RegistroActividad.cMaquina_UltimaModificacion         = Environment.MachineName;
                RegistroActividad.cUsuario_UltimaModificacion         = DataSesion.cLogin;
                RegistroActividad.dFecha_UltimaModificacion           = DateTime.Now;
                RegistroActividad.dFechaCierre                        = Actividad.dFechaCierre;
                RegistroActividad.nActvidadForward                    = Actividad.nActividadForward;
                RegistroActividad.nAvancePorcentualEstimado           = (decimal)Actividad.nAvancePorcentualEstimado;
                RegistroActividad.nEstatus                            = Actividad.nEstatus;
                RegistroActividad.nEstimacionAutorizadaAdicionalHoras = (decimal)Actividad.nEstimacionAutorizadaAdicionalHoras;
                RegistroActividad.nEstimacionAutorizadaHoras          = (decimal)Actividad.nEstimacionAutorizadaHoras;
                RegistroActividad.nEstimacionAutorizadaUE             = (decimal)Actividad.nEstimacionAutorizadaUE;
                RegistroActividad.nEstimacionBaseUE                   = (decimal)Actividad.nEstimacionBaseUE;
                RegistroActividad.nEstimacionSolicitadaUE             = (decimal)Actividad.nEstimacionSolicitadaUE;
                RegistroActividad.nNivelAsociacion                    = Actividad.nNivelAsociacion;
                RegistroActividad.nTrabajoRealizadoAdicionalHoras     = (decimal)Actividad.nTrabajoRealizadoAdicionalHoras;
                RegistroActividad.nTrabajoRealizadoHoras              = (decimal)Actividad.nTrabajoRealizadoHoras;
                RegistroActividad.nTrabajoRestanteHoras               = (decimal)Actividad.nTrabajoRestanteHoras;
                RegistroActividad.nVuelta                             = (short)Actividad.nVuelta;
                RegistroActividad.tDescripcion                        = Actividad.tDescripcion;
               
                if (RegistroActividad.nActividad == 0)
                {//nuevo
                    RegistroActividad.cClaveERP         = DB.PMB_Actividades.Where(x => x.nEmisor == DataSesion.nEmisor && x.bActivo).ToList().Count + 1 + "";
                    RegistroActividad.cUsuario_Registro = DataSesion.cLogin;
                    RegistroActividad.cMaquina_Registro = Environment.MachineName;
                    RegistroActividad.dFecha_Registro   = DateTime.Now;
                    RegistroActividad.nEmisor           = DataSesion.nEmisor;
   
                    DB.PMB_Actividades.InsertOnSubmit(RegistroActividad);
                }
                DB.SubmitChanges();
                Result.Actividad = new DTO_Actividad() { nActividad = RegistroActividad.nActividad, cDescripcion = RegistroActividad.cDescripcion, cClaveERP = RegistroActividad.cClaveERP };
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }
            return Result;
        }

        public DTO_Actividad_Result ObtenerActividades_Grid(PMBookDataContext DB, int nFechaInicial, int nFechaFinal)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Actividades_Grid = (from a  in DB.PMB_Actividades
                                           join p  in DB.PMB_Proyectos
                                           on a.nProyecto equals p.nProyecto
                                           join m  in DB.PMB_ProyectosModulos
                                           on a.nProyectoModulo equals m.nProyectoModulo
                                           join c  in DB.PMB_ProyectosComponentes
                                           on a.nProyectoComponente equals c.nProyectoComponente
                                           join pc in DB.PMB_ProyectosConsultores 
                                           on p.nProyecto equals pc.nProyecto
                                           join u  in DB.ADSUM_Usuarios 
                                           on pc.nAdsumUsuario equals u.nAdsumUsuario
                                           where a.nEmisor == DataSesion.nEmisor
                                            && (a.bActivo)
                                           select new DTO_Actividad_Grid()
                                            {
                                            ID               =p.cClaveERP,
                                            Proyecto         =p.cDescripcion,
                                            Modulo           =m.cDescripcion,
                                            Componente       =c.cDescripcion,
                                            Actividad        =a.cDescripcion,
                                            Tiempoautorizado = (double)a.nEstimacionAutorizadaUE,
                                            TrabajoRestante  = (double)a.nTrabajoRestanteHoras,
                                            Avance           = (double)a.nAvancePorcentualEstimado,
                                            Vuelta           =a.nVuelta,
                                            Estatus          =a.nEstatus,
                                            Consultor        =u.cNombre,
                                            FechaRegistro    =a.dFecha_Registro.ToString()
                                            }).OrderBy(x => x.Proyecto).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }
    }
    public class DTO_Actividad_Grid
    {
        public string ID  { get; set; }
        public string Proyecto { get; set; }
        public string Modulo { get; set; }
        public string Componente { get; set; }
        public string Actividad { get; set; }
        public double Tiempoautorizado { get; set; }
        public double TrabajoRestante { get; set; }
        public double Avance { get; set; }
        public int Vuelta { get; set; }
        public byte Estatus { get; set; }
        public string Consultor { get; set; }
        public string FechaRegistro { get; set; }


    }
    public class DTO_Actividad
    {
        
        public bool bActivo { get; set; }
        public bool bFavorito{ get; set; }
        public bool bIncidenteCobrable{ get; set; }
        public bool bIncidente { get; set; }
        public bool bRequiereQC { get; set; }
        public bool bSprintFavorito { get; set; }
        public bool bTerminado { get; set; }
        public byte nEstatus { get; set; }
        public byte nNivelAsociacion { get; set; }
        public int nActividad { get; set; }
        public int nActividadForward { get; set; }
        public int nEmisor { get; set; }
        public int nVuelta { get; set; }
        public double nAvancePorcentualEstimado { get; set; }
        public double nEstimacionAutorizadaAdicionalHoras { get; set; }
        public double nEstimacionAutorizadaHoras { get; set; }
        public double nEstimacionAutorizadaUE { get; set; }
        public double nEstimacionBaseUE { get; set; }
        public double nEstimacionSolicitadaUE { get; set; }
        public double nTrabajoRealizadoAdicionalHoras { get; set; }
        public double nTrabajoRealizadoHoras { get; set; }
        public double nTrabajoRestanteHoras{ get; set; }
        public string cClaveERP { get; set; }
        public string cDescripcion { get; set; }
        public string tDescripcion { get; set; }
        public DateTime dFechaCierre { get; set; }
        public string cFechaCierre{ get; set; }

    }
    public class DTO_Actividad_Result
    {
        public bool bError { get; set; }
        public string msgErr { get; set; }
        public DTO_Actividad Actividad { get; set; }
        public List<DTO_Actividad> Actividades { get; set; }
        public List<DTO_Actividad_Grid> Actividades_Grid { get; set; }
    }
}