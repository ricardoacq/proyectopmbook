using MvcLogin.Models;
using System;
using System.Collections.Generic;
using MvcLogin.Linq;
using System.Linq;

namespace MvcLogin.Areas.Ejecucion.Models
{
    public class IncidentesModel : CargaInicial
    {
        public IncidentesModel()
        {
        }

        public DTO_Incidente_Result ObtenerProductos(PMBookDataContext DB)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Productos = (from p in DB.Ctl_Productos
                                    where p.nEmisor == DataSesion.nEmisor
                                    && (p.bActivo)
                                    select new DTO_Producto()
                                     {
                                        idProducto=p.nProducto,
                                        Producto=p.cDescripcion
                                     }
                                    ).OrderBy(x => x.Producto).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        ///
        public DTO_Incidente_Result ObtenerModulos(PMBookDataContext DB,int idProducto)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Modulos = (from pm in DB.Ctl_ProductosModulos
                                    where pm.nEmisor == DataSesion.nEmisor
                                    && pm.nProducto==idProducto
                                    && (pm.bActivo)
                                    select new DTO_Producto_Modulo()
                                    {
                                        idModulo = (pm.nProductoModulo != null) ? pm.nProductoModulo:0,
                                        Modulo = pm.cDescripcion ?? string.Empty
                                    }
                                    ).OrderBy(x => x.Modulo).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        ///
        public DTO_Incidente_Result ObtenerComponentes(PMBookDataContext DB, int idModulo)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Componentes = (from pc in DB.Ctl_ProductosComponentes
                                  where pc.nEmisor == DataSesion.nEmisor
                                  && pc.nProductoModulo == idModulo
                                  && (pc.bActivo)
                                  select new DTO_Producto_Componente()
                                  {
                                      idComponente = (pc.nProductoComponente != null) ? pc.nProductoComponente : 0,
                                      Componente = pc.cDescripcion ?? string.Empty
                                  }
                                    ).OrderBy(x => x.Componente).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }
        ///
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

                                          bActivo = c.bActivo,
                                          bFavorito = c.bFavorito,
                                          bIncidenteCobrable = c.bIncidenteCobrable,
                                          bIncidente = c.bIncidente,
                                          bRequiereQC = c.bRequiereQC ?? false,
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

                RegistroActividad.bActivo = Actividad.bActivo;
                RegistroActividad.bFavorito = Actividad.bFavorito;
                RegistroActividad.bIncidenteCobrable = Actividad.bIncidenteCobrable;
                RegistroActividad.bIncidente = Actividad.bIncidente;
                RegistroActividad.bRequiereQC = Actividad.bRequiereQC;
                RegistroActividad.bSprintFavorito = Actividad.bSprintFavorito;
                RegistroActividad.bTerminado = Actividad.bTerminado;
                RegistroActividad.cDescripcion = Actividad.cDescripcion;
                RegistroActividad.cMaquina_UltimaModificacion = Environment.MachineName;
                RegistroActividad.cUsuario_UltimaModificacion = DataSesion.cLogin;
                RegistroActividad.dFecha_UltimaModificacion = DateTime.Now;
                RegistroActividad.dFechaCierre = Actividad.dFechaCierre;
                RegistroActividad.nActvidadForward = Actividad.nActividadForward;
                RegistroActividad.nAvancePorcentualEstimado = (decimal)Actividad.nAvancePorcentualEstimado;
                RegistroActividad.nEstatus = Actividad.nEstatus;
                RegistroActividad.nEstimacionAutorizadaAdicionalHoras = (decimal)Actividad.nEstimacionAutorizadaAdicionalHoras;
                RegistroActividad.nEstimacionAutorizadaHoras = (decimal)Actividad.nEstimacionAutorizadaHoras;
                RegistroActividad.nEstimacionAutorizadaUE = (decimal)Actividad.nEstimacionAutorizadaUE;
                RegistroActividad.nEstimacionBaseUE = (decimal)Actividad.nEstimacionBaseUE;
                RegistroActividad.nEstimacionSolicitadaUE = (decimal)Actividad.nEstimacionSolicitadaUE;
                RegistroActividad.nNivelAsociacion = Actividad.nNivelAsociacion;
                RegistroActividad.nTrabajoRealizadoAdicionalHoras = (decimal)Actividad.nTrabajoRealizadoAdicionalHoras;
                RegistroActividad.nTrabajoRealizadoHoras = (decimal)Actividad.nTrabajoRealizadoHoras;
                RegistroActividad.nTrabajoRestanteHoras = (decimal)Actividad.nTrabajoRestanteHoras;
                RegistroActividad.nVuelta = (short)Actividad.nVuelta;
                RegistroActividad.tDescripcion = Actividad.tDescripcion;

                if (RegistroActividad.nActividad == 0)
                {//nuevo
                    RegistroActividad.cClaveERP = DB.PMB_Actividades.Where(x => x.nEmisor == DataSesion.nEmisor && x.bActivo).ToList().Count + 1 + "";
                    RegistroActividad.cUsuario_Registro = DataSesion.cLogin;
                    RegistroActividad.cMaquina_Registro = Environment.MachineName;
                    RegistroActividad.dFecha_Registro = DateTime.Now;
                    RegistroActividad.nEmisor = DataSesion.nEmisor;

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

    }
    public class DTO_Producto
    {
        public int idProducto { get; set; }
        public string Producto { get; set; }


    }
    public class DTO_Producto_Modulo{
        public string Modulo { get; set; }
        public int idModulo { get; set; }
    }
    public class DTO_Producto_Componente {
        public string Componente { get; set; }
        public int idComponente { get; set; }

    }
    public class DTO_Incidente_Result
    {
        public bool bError { get; set; }
        public string msgErr { get; set; }
        public List<DTO_Producto> Productos { get; set; }
        public List<DTO_Producto_Modulo> Modulos { get; set; }
        public List<DTO_Producto_Componente> Componentes { get; set; }
    }
}