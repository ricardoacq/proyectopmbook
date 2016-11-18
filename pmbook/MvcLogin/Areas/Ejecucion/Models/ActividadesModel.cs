using MvcLogin.Models;
using System;
using System.Collections.Generic;
using MvcLogin.Linq;
using System.Linq;
using MvcLogin.Helpers.Funciones;

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

        public DTO_Actividad_Result ObtenerActividades_Grid(PMBookDataContext DB, string nFechaInicial, string nFechaFinal, int idProyecto, int idModulo, int idComponente, int idConsultor)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };



            try
            {
                long IJuliana = 0;
                long FJuliana = 0;
                try
                {
                    IJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(nFechaInicial, "dd/MM/yyyy", null));
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(nFechaFinal, "dd/MM/yyyy", null));
                }
                catch (Exception e)
                {
                    string sFechaInicial = nFechaInicial.Substring(8, 2) + "/" + nFechaInicial.Substring(5, 2) + "/" + nFechaInicial.Substring(0, 4);
                    string sFechaFinal = nFechaFinal.Substring(8, 2) + "/" + nFechaFinal.Substring(5, 2) + "/" + nFechaFinal.Substring(0, 4);
                    IJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(sFechaInicial, "dd/MM/yyyy", null));
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(sFechaFinal, "dd/MM/yyyy", null));
                }


                Result.Actividades_Grid = (from a in DB.PMB_Actividades
                                           join p in DB.PMB_Proyectos
                                           on a.nProyecto equals p.nProyecto
                                           join m in DB.PMB_ProyectosModulos
                                           on a.nProyectoModulo equals m.nProyectoModulo
                                           join c in DB.PMB_ProyectosComponentes
                                           on a.nProyectoComponente equals c.nProyectoComponente                                          
                                           join u in DB.ADSUM_Usuarios
                                           on a.nAdsumUsuarioResponsable equals u.nAdsumUsuario
                                           where a.nEmisor == DataSesion.nEmisor                                             
                                           && (a.nProyecto == idProyecto || idProyecto == 0)
                                           && (a.nAdsumUsuarioResponsable == idConsultor || idConsultor==0)
                                           && (a.nProyectoModulo == idModulo|| idModulo==0)
                                           && (a.nProyectoComponente == idComponente || idComponente == 0)
                                           && (a.bActivo)
                                           select new DTO_Actividad_Grid()
                                           {
                                               ID = (a.nActividad != null) ? a.nActividad : 0,
                                               Favorito = a.bFavorito,
                                               RequiereQC = (bool)a.bRequiereQC,
                                               SprintFavorito = a.bSprintFavorito,
                                               Terminado = a.bTerminado, 
                                               nProyecto=(int)a.nProyecto,
                                               Proyecto = p.cDescripcion,
                                               nModulo=(int)a.nProyectoModulo,
                                               Modulo = m.cDescripcion,
                                               nComponente=(int)a.nProyectoComponente,
                                               Componente = c.cDescripcion,
                                               Actividad = a.cDescripcion,
                                               //nEficiencia = a.nTrabajoRealizadoHoras > 0 ? a.nEstimacionAutorizadaHoras / a.nTrabajoRealizadoHoras : 0,
                                               Tiempoautorizado = Math.Floor(a.nEstimacionAutorizadaHoras / 60) + " Hr " + a.nEstimacionAutorizadaHoras % 60 + " min",
                                               TrabajoRealizado = Math.Floor(a.nTrabajoRealizadoHoras / 60) + " Hr " + a.nTrabajoRealizadoHoras % 60 + " min",
                                               TrabajoRestante = Math.Floor(a.nTrabajoRestanteHoras / 60) + " Hr " + a.nTrabajoRestanteHoras % 60 + " min",
                                               Avance = (double)a.nAvancePorcentualEstimado,
                                               Vuelta = a.nVuelta,
                                               Estatus = a.nEstatus,
                                               Consultor = u.cNombre,
                                               nUsuario=u.nAdsumUsuario,
                                               FechaRegistro = (a.dFecha_Registro != null) ? ((DateTime)a.dFecha_Registro).ToShortDateString() : "Sin fecha de cierre",
                                           }).OrderBy(x => x.Actividad).Distinct().ToList();
                Result.nUsuarioLogin = DataSesion.nIdUsuario;
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }

        public DTO_Actividad_Result ObtenerActividadesInActivas_Grid(PMBookDataContext DB, string nFechaInicial, string nFechaFinal, int idProyecto, int idModulo, int idComponente, int idConsultor)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {

                long IJuliana = 0;
                long FJuliana = 0;
                try
                {
                    IJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(nFechaInicial, "dd/MM/yyyy", null));
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(nFechaFinal, "dd/MM/yyyy", null));
                }
                catch (Exception e)
                {
                    string sFechaInicial = nFechaInicial.Substring(8, 2) + "/" + nFechaInicial.Substring(5, 2) + "/" + nFechaInicial.Substring(0, 4);
                    string sFechaFinal = nFechaFinal.Substring(8, 2) + "/" + nFechaFinal.Substring(5, 2) + "/" + nFechaFinal.Substring(0, 4);
                    IJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(sFechaInicial, "dd/MM/yyyy", null));
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(sFechaFinal, "dd/MM/yyyy", null));
                }

                Result.Actividades_Grid = (from a in DB.PMB_Actividades
                                           join p in DB.PMB_Proyectos
                                           on a.nProyecto equals p.nProyecto
                                           join m in DB.PMB_ProyectosModulos
                                           on a.nProyectoModulo equals m.nProyectoModulo
                                           join c in DB.PMB_ProyectosComponentes
                                           on a.nProyectoComponente equals c.nProyectoComponente
                                           join u in DB.ADSUM_Usuarios
                                           on a.nAdsumUsuarioResponsable equals u.nAdsumUsuario
                                           where a.nEmisor == DataSesion.nEmisor
                                           && (a.nProyecto == idProyecto || idProyecto == 0)
                                           && (a.nAdsumUsuarioResponsable == idConsultor || idConsultor == 0)
                                           && (a.nProyectoModulo == idModulo || idModulo == 0)
                                           && (a.nProyectoComponente == idComponente || idComponente == 0)
                                           && !(a.bActivo)
                                           select new DTO_Actividad_Grid()
                                           {
                                               ID = (a.nActividad != null) ? a.nActividad : 0,
                                               Favorito = a.bFavorito,
                                               RequiereQC = (bool)a.bRequiereQC,
                                               SprintFavorito = a.bSprintFavorito,
                                               Terminado = a.bTerminado,
                                               nProyecto = (int)a.nProyecto,
                                               Proyecto = p.cDescripcion,
                                               nModulo = (int)a.nProyectoModulo,
                                               Modulo = m.cDescripcion,
                                               nComponente = (int)a.nProyectoComponente,
                                               Componente = c.cDescripcion,
                                               Actividad = a.cDescripcion,
                                               //nEficiencia = a.nTrabajoRealizadoHoras > 0 ? a.nEstimacionAutorizadaHoras / a.nTrabajoRealizadoHoras : 0,
                                               Tiempoautorizado = Math.Floor(a.nEstimacionAutorizadaHoras / 60) + " Hr " + a.nEstimacionAutorizadaHoras % 60 + " min",
                                               TrabajoRealizado = Math.Floor(a.nTrabajoRealizadoHoras / 60) + " Hr " + a.nTrabajoRealizadoHoras % 60 + " min",
                                               TrabajoRestante = Math.Floor(a.nTrabajoRestanteHoras / 60) + " Hr " + a.nTrabajoRestanteHoras % 60 + " min",
                                               Avance = (double)a.nAvancePorcentualEstimado,
                                               Vuelta = a.nVuelta,
                                               Estatus = a.nEstatus,
                                               Consultor = u.cNombre,
                                               FechaRegistro = a.dFecha_Registro.ToString()
                                           }).OrderBy(x => x.Actividad).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }

        public DTO_Actividad_Result ObtenerImpactoAyuda(PMBookDataContext DB)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Impacto = (from a in DB.CTL_ImpactosActividadesAyudas                                         
                                           where a.nEmisor == DataSesion.nEmisor
                                            && (a.bActivo)
                                            select new DTO_Impacto()
                                           {
                                               idImpacto = (a.nImpactoActividadAyuda != null) ? a.nImpactoActividadAyuda : 0,
                                               Impacto=a.cDescripcion ?? string.Empty
                                           }).OrderBy(x => x.Impacto).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }

        public DTO_Actividad_Result ObtenerComponentes(PMBookDataContext DB, int idModulo)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.ProyectoComponentes = (from pc in DB.PMB_ProyectosComponentes
                                      where pc.nEmisor == DataSesion.nEmisor
                                      && pc.nProyectoModulo == idModulo
                                      && (pc.bActivo)
                                      select new DTO_Proyecto_Componente()
                                      {
                                          idProyectoComponente = (pc.nProyectoComponente != null) ? pc.nProyectoComponente : 0,
                                          ProyectoComponente = pc.cDescripcion ?? string.Empty
                                      }
                                    ).OrderBy(x => x.ProyectoComponente).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        public DTO_Actividad_Result ObtenerModulos(PMBookDataContext DB, int idProyecto)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.ProyectoModulos = (from pm in DB.PMB_ProyectosModulos
                                             where pm.nEmisor == DataSesion.nEmisor
                                             && pm.nProyecto == idProyecto
                                             && (pm.bActivo)
                                             select new DTO_Proyecto_Modulo()
                                             {
                                                 idProyectoModulo = (pm.nProyectoModulo != null) ? pm.nProyectoModulo : 0,
                                                 ProyectoModulo = pm.cDescripcion ?? string.Empty
                                             }
                                    ).OrderBy(x => x.ProyectoModulo).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        public DTO_Actividad_Result ObtenerProyectos(PMBookDataContext DB)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Proyecto = (from p in DB.PMB_Proyectos
                                         where p.nEmisor == DataSesion.nEmisor
                                         && (p.bActivo)
                                         select new DTO_Proyecto()
                                         {
                                             idProyecto = (p.nProyecto != null) ? p.nProyecto : 0,
                                             Proyecto = p.cDescripcion ?? string.Empty
                                         }
                                    ).OrderBy(x => x.Proyecto).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        public DTO_Actividad_Result ObtenerProyectosInactivos(PMBookDataContext DB)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Proyecto = (from p in DB.PMB_Proyectos
                                   where p.nEmisor == DataSesion.nEmisor
                                   && !(p.bActivo)
                                   select new DTO_Proyecto()
                                   {
                                       idProyecto = (p.nProyecto != null) ? p.nProyecto : 0,
                                       Proyecto = p.cDescripcion ?? string.Empty
                                   }
                                    ).OrderBy(x => x.Proyecto).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        public DTO_Actividad_Result ObtenerAcciones(PMBookDataContext DB,int idActividad)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Accion = (from a in DB.PMB_ActividadesAcciones
                                   where a.nEmisor == DataSesion.nEmisor
                                   &&a.nActividad == idActividad
                                   && (a.bActivo)
                                   select new DTO_Accion()
                                   {
                                       bCompletado = a.bCompletado,
                                       Accion = a.cDescripcion ?? string.Empty
                                   }
                                    ).OrderBy(x => x.Accion).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        public DTO_Actividad_Result ObtenerHistorial(PMBookDataContext DB,int idActividad,int idConsultor) {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Historial = (from p in DB.PMB_ProyectosRegistrosTrabajos
                                    join pa in DB.PMB_ProyectosAsignaciones
                                    on p.nProyectoAsignacion equals pa.nProyectoAsignacion
                                    join a in DB.ADSUM_Usuarios
                                    on p.nAdsumUsuario equals a.nAdsumUsuario
                                    where p.nEmisor == DataSesion.nEmisor
                                    &&pa.nProyectoActividad == idActividad
                                    && p.nAdsumUsuario==idConsultor
                                    select new DTO_Historial()
                                    {
                                        ProyectoRegistroTrabajo = (p.nProyectoRegistroTrabajo != null) ? p.nProyectoRegistroTrabajo : 0,
                                        Comentario =p.cComentario?? string.Empty,
                                        TrabajoRealizado = Math.Floor(p.nTrabajoRealizadoHoras / 60) + " Hr " + p.nTrabajoRealizadoHoras % 60 + " min",
                                        TrabajoRestante = Math.Floor(p.nTrabajoRestanteHoras / 60) + " Hr " + p.nTrabajoRestanteHoras % 60 + " min",
                                        Avance=p.nAvancePorcentualEstimado +" %",
                                        FechaRegistro = (p.dFecha_Registro != null) ? ((DateTime)p.dFecha_Registro).ToShortDateString() : "Sin fecha de cierre",
                                        Consultor=a.cNombre,
    
                                    }
                                    ).OrderBy(x => x.ProyectoRegistroTrabajo).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        
        }
        public DTO_Actividad_Result GuardarReporteAvanceActividad(PMBookDataContext DB, DTO_Actividad_Grid RegistroTrabajo,string dFecha ,string Comentario, int Horas, double Minutos, int HorasRes, double MinutosRes)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };
            decimal tiempoh = Horas + ((decimal)Minutos / 60);
            decimal tiempom = (Horas * 60) + (decimal)Minutos;
            decimal tiempohR = HorasRes + ((decimal)MinutosRes / 60);
            decimal tiempomR = (HorasRes * 60) + (decimal)MinutosRes;
            try
            {

               
                long FJuliana = 0;
                try
                {
                  
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(dFecha, "dd/MM/yyyy", null));
                }
                catch (Exception e)
                {

                    string sFechaFinal = dFecha.Substring(8, 2) + "/" + dFecha.Substring(5, 2) + "/" + dFecha.Substring(0, 4);
                    
                    FJuliana = ConvertidorJuliano.aJuliano(DateTime.ParseExact(sFechaFinal, "dd/MM/yyyy", null));
                }

                PMB_Actividade RegistroActividad = DB.PMB_Actividades.Where(x => x.nActividad == RegistroTrabajo.ID).SingleOrDefault() ?? new PMB_Actividade();

                PMB_Actividade act = (from a in DB.PMB_Actividades
                                      where a.bActivo && a.nActividad == RegistroTrabajo.ID
                                      select a).SingleOrDefault();

                PMB_ProyectosAsignacione pa = (from pas in DB.PMB_ProyectosAsignaciones
                                               orderby pas.dFecha_Registro descending
                                               where pas.nProyectoActividad == act.nActividad && pas.bActivo && (pas.nAdsumUsuario == DataSesion.nIdUsuario 
                                               || pas.nAdsumUsuarioAyudante == DataSesion.nIdUsuario)
                                               select pas).FirstOrDefault();

                PMB_ProyectosRegistrosTrabajo prt = new PMB_ProyectosRegistrosTrabajo();
                prt.cMaquina_Registro = Environment.MachineName;
                prt.cUsuario_Registro = DataSesion.cLogin;
                prt.dFecha_Registro = DateTime.Now;
                prt.nAdsumUsuario = DataSesion.nIdUsuario;
                prt.nEmisor = DataSesion.nEmisor;
                prt.cComentario = Comentario != null ? Comentario.Trim() : "";
                prt.nAvancePorcentualEstimado = RegistroTrabajo.Terminado ? 100 : (decimal)RegistroTrabajo.Avance;
                prt.nFechaRegistro = (int)FJuliana;
                //prt.nHoraRegistro = ADDA.HoraMinutoSerial(RegistroTrabajo.dFechaReporte);
                prt.nHorasGanadas = act.nTrabajoRestanteHoras - tiempomR;
                prt.nProyectoAsignacion = pa.nProyectoAsignacion;
                prt.nProyectoConsultor = pa.nProyectoConsultor;
                prt.nTrabajoRealizadoHoras = tiempom;
                prt.nTrabajoRestanteHoras = RegistroTrabajo.Terminado ? 0 : tiempomR;

                pa.nTrabajoRestanteHoras = RegistroTrabajo.Terminado ? 0 : tiempomR;
                pa.nTrabajoRealizadoHoras += tiempom;

                act.nAvancePorcentualEstimado = RegistroTrabajo.Terminado ? 100 : (decimal)RegistroTrabajo.Avance;
                act.nTrabajoRealizadoHoras += tiempom;
                act.nTrabajoRestanteHoras = RegistroTrabajo.Terminado ? 0 : tiempomR;
                act.bFavorito = RegistroTrabajo.Favorito;
                DB.PMB_ProyectosRegistrosTrabajos.InsertOnSubmit(prt);
                DB.SubmitChanges();
                
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }
            return Result;
        }
        public DTO_Actividad_Result GuardarAccion(PMBookDataContext DB,int idActividad,string Accion)
        {
            DTO_Actividad_Result Result = new DTO_Actividad_Result()
            {
                bError = false,
                msgErr = string.Empty
            };
            try
            {

                PMB_Actividade RegistroActividad = DB.PMB_Actividades.Where(x => x.nActividad == idActividad).SingleOrDefault() ?? new PMB_Actividade();

                PMB_ActividadesAccione ac = new PMB_ActividadesAccione();
                ac.cMaquina_Registro = Environment.MachineName;
                ac.cUsuario_Registro = DataSesion.cLogin;
                ac.dFecha_Registro = DateTime.Now;
                ac.nEmisor = DataSesion.nEmisor;
                ac.cDescripcion = Accion != null ? Accion.Trim() : "";
                ac.bActivo = true;
                ac.bCompletado = false;
                ac.nActividad = idActividad;

                DB.PMB_ActividadesAcciones.InsertOnSubmit(ac);
                DB.SubmitChanges();

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
        public int ID  { get; set; }
        public bool Favorito { get; set; }
        public bool RequiereQC { get; set; }
        public bool SprintFavorito { get; set; }
        public bool Terminado { get; set; }
        public int nProyecto { get; set; }
        public string Proyecto { get; set; }
        public int nModulo { get; set; }
        public string Modulo { get; set; }
        public int nComponente { get; set; }
        public string Componente { get; set; }
        public string Actividad { get; set; }
        public String Tiempoautorizado { get; set; }
        public string TrabajoRealizado { get; set; }
        public string TrabajoRestante { get; set; }
        public double Avance { get; set; }
        public int Vuelta { get; set; }
        public byte Estatus { get; set; }
        public string Consultor { get; set; }
        public string FechaRegistro { get; set; }
        public int nUsuario { get; set; }

    }
    public class DTO_Impacto{
        public int idImpacto { get; set; }
        public string Impacto { get; set; }

    }
    public class DTO_Proyecto {
        public int idProyecto { get; set; }
        public string Proyecto { get; set; }
    }
    public class DTO_Proyecto_Modulo
    {
        public int idProyectoModulo { get; set; }
        public string ProyectoModulo { get; set; }
    }
    public class DTO_Proyecto_Componente
    {
        public int idProyectoComponente { get; set; }
        public string ProyectoComponente { get; set; }
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
    public class DTO_Historial {
        public int ProyectoRegistroTrabajo { get; set; }
        public string Comentario { get; set; }
        public string TrabajoRealizado { get; set; }
        public string  TrabajoRestante { get; set; }
        public string Avance { get; set; }
        public string FechaRegistro { get; set; }
        public string Consultor { get; set; }
    }
    public class DTO_Accion {
        public bool bCompletado { get; set; }
        public string  Accion { get; set; }
    }
    public class DTO_Actividad_Result
    {
        public bool bError { get; set; }
        public string msgErr { get; set; }
        public List<DTO_Historial> Historial { get; set; }
        public List<DTO_Proyecto> Proyecto { get; set; }
        public List<DTO_Proyecto_Modulo> ProyectoModulos { get; set; }
        public List<DTO_Proyecto_Componente> ProyectoComponentes { get; set; }
        public List<DTO_Impacto>Impacto { get; set; }
        public DTO_Actividad Actividad { get; set; }
        public List<DTO_Actividad> Actividades { get; set; }
        public List<DTO_Actividad_Grid> Actividades_Grid { get; set; }
        public List<DTO_Accion> Accion { get; set; }
        public int nUsuarioLogin { get; set; }
    }
}