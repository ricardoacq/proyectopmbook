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
       
        ///TRAE PRODUCTOS
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

        ///TRAE MODULOS
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

        ///TRAE COMPONENTES
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

        ///TRAE CLIENTES
        public DTO_Incidente_Result ObtenerClientes(PMBookDataContext DB)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Clientes = (from c in DB.CTM_Clientes
                                      where c.nEmisor == DataSesion.nEmisor
                                      && (c.bActivo)
                                      select new DTO_Cliente()
                                      {
                                          idCliente = (c.nCliente != null) ? c.nCliente : 0,
                                          Cliente = c.cDescripcion ?? string.Empty
                                      }
                                    ).OrderBy(x => x.Cliente).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }

        ///TRAE LIDERES
        public DTO_Incidente_Result ObtenerLideres(PMBookDataContext DB)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Lideres = (from c in DB.ADSUM_Usuarios
                                  join u in DB.ADSUM_Usuarios
                                  on c.nAdsumUsuario equals u.nAdsumUsuarioLider
                                   where c.nEmisor == DataSesion.nEmisor
                                   && (c.bActivo)
                                   select new DTO_Lider()
                                   {
                                       idLider =(c.nAdsumUsuario != null) ? c.nAdsumUsuario : 0,
                                       Lider = c.cNombre ?? string.Empty
                                   }
                                    ).OrderBy(x => x.Lider).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }

        ///TRAE TESTERS
        public DTO_Incidente_Result ObtenerTesters(PMBookDataContext DB)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Testers = (from c in DB.ADSUM_Usuarios
                                  join u in DB.ADSUM_Usuarios
                                  on c.nAdsumUsuario equals u.nAdsumUsuarioTester
                                  where c.nEmisor == DataSesion.nEmisor
                                  && (c.bActivo)
                                  select new DTO_Tester()
                                  {
                                      idTester = (c.nAdsumUsuario != null) ? c.nAdsumUsuario : 0,
                                      Tester = c.cNombre ?? string.Empty
                                  }
                                    ).OrderBy(x => x.Tester).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }

        ///TRAE CONSULTOR
        public DTO_Incidente_Result ObtenerConsultores(PMBookDataContext DB)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Consultores = (from c in DB.ADSUM_Usuarios
                                  where c.nEmisor == DataSesion.nEmisor
                                  && c.nAdsumUsuario==DataSesion.nIdUsuario
                                  && (c.bActivo)
                                      select new DTO_Consultor()
                                  {
                                      idConsultor = (c.nAdsumUsuario != null) ? c.nAdsumUsuario : 0,
                                      Consultor = c.cNombre ?? string.Empty
                                  }
                                    ).OrderBy(x => x.Consultor).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;

        }

     /// 


        //public DTO_Incidente_Result EliminarIncidente(PMBookDataContext DB, int nIncidente)
        //{
        //    DTO_Incidente_Result Result = new DTO_Incidente_Result()
        //    {
        //        bError = false,
        //        msgErr = string.Empty
        //    };

        //    try
        //    {
        //        PMB_Incidente Incidente = DB.PMB_Incidentes.Where(x => x.nIncidente == nIncidente).SingleOrDefault();
        //        Incidente.bActivo = false;
        //        DB.SubmitChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Result.bError = true;
        //        Result.msgErr = e.ToString();
        //    }
        //    return Result;
        //}

        //public DTO_Incidente_Result GuardarIncidente(PMBookDataContext DB, DTO_Incidente Incidente)
        //{
        //    DTO_Incidente_Result Result = new DTO_Incidente_Result()
        //    {
        //        bError = false,
        //        msgErr = string.Empty
        //    };

        //    try
        //    {
        //        if (DB.PMB_Incidentes.Where(x => x.nIncidente != Incidente.nIncidente).Select(x => x.cDescripcion).ToList().Contains(Incidente.cDescripcion))
        //        {
        //            Result.bError = true;
        //            Result.msgErr = "La descripción del Incidente ya existe, no pueden existir registros repetidos.";
        //            return Result;
        //        }

        //        PMB_Incidentes RegistroIncidente = DB.PMB_Incidentes.Where(x => x.nIncidente == Incidente.nIncidente).SingleOrDefault() ?? new PMB_Incidentes();

        //        RegistroIncidente.bActivo =Incidente.bActivo;
        //       

        //        if (RegistroIncidente.nIncidente == 0)
        //        {//nuevo
        //            RegistroIncidente.cClaveERP = DB.PMB_Incidentes.Where(x => x.nEmisor == DataSesion.nEmisor && x.bActivo).ToList().Count + 1 + "";
        //            RegistroIncidente.cUsuario_Registro = DataSesion.cLogin;
        //            RegistroIncidente.cMaquina_Registro = Environment.MachineName;
        //            RegistroIncidente.dFecha_Registro = DateTime.Now;
        //            RegistroIncidente.nEmisor = DataSesion.nEmisor;

        //            DB.PMB_Incidentes.InsertOnSubmit(RegistroIncidente);
        //        }
        //        DB.SubmitChanges();
        //        Result.Incidente = new DTO_Incidente() { nIncidente = RegistroIncidente.nIncidente, cDescripcion = RegistroIncidente.cDescripcion, cClaveERP = RegistroIncidente.cClaveERP };
        //    }
        //    catch (Exception e)
        //    {
        //        Result.bError = true;
        //        Result.msgErr = e.ToString();
        //    }
        //    return Result;
        //}

        public DTO_Incidente_Result ObtenerIncidentes(PMBookDataContext DB, int idCliente)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Incidentes = (from a in DB.PMB_Actividades
                                     join p in DB.Ctl_Productos
                                     on a.nProducto equals p.nProducto
                                     join m in DB.Ctl_ProductosModulos
                                     on a.nProductoModulo equals m.nProductoModulo
                                     join pc in DB.Ctl_ProductosComponentes
                                     on a.nProductoComponente equals pc.nProductoComponente
                                     join c in DB.CTM_Clientes
                                     on a.nCliente equals c.nCliente
                                        where a.nEmisor == DataSesion.nEmisor
                                        && a.nCliente ==idCliente
                                        && (a.bActivo)
                                        || a.nEmisor == DataSesion.nEmisor
                                        && a.nCliente == idCliente
                                        && (a.bActivo)
                                        select new DTO_Incidente()
                                        {
                                           nIncidente = (a.nActividad  != null) ? a.nActividad:0,
                                           Incidente = a.cDescripcion ?? string.Empty,
                                           Cliente = c.cDescripcion ?? string.Empty,
                                           Producto = p.cDescripcion ?? string.Empty,
                                           Modulo = m.cDescripcion ?? string.Empty,
                                           Componente = pc.cDescripcion ?? string.Empty,
                                           T_Solicitado_UE = (a.nEstimacionSolicitadaUE != null) ? a.nEstimacionSolicitadaUE:0,
                                           T_Autorizado = (a.nEstimacionAutorizadaHoras != null) ?a.nEstimacionAutorizadaHoras:0,
                                           Trab_Realizado = (a.nTrabajoRealizadoHoras != null) ? a.nTrabajoRealizadoHoras:0,
                                           Trab_Restante = (a.nTrabajoRestanteHoras != null) ?a.nTrabajoRestanteHoras:0,
                                           Avance = (a.nAvancePorcentualEstimado != null) ? a.nAvancePorcentualEstimado:0,
                                           Estatus =a.nEstatus,
                                           FechaRegistro = (a.dFecha_Registro != null) ? ((DateTime)a.dFecha_Registro).ToShortDateString() : "Sin fecha de Registro",
                                           HoraRegistro = (a.dFecha_Registro != null) ? ((DateTime)a.dFecha_Registro).ToShortTimeString() : "Sin hora de Registro"

                                               
                                        }).OrderBy(x => x.nIncidente).ToList();
            }
            catch (Exception e)
            {
                Result.bError = true;
                Result.msgErr = e.ToString();
            }

            return Result;
        }

        public DTO_Incidente_Result ObtenerIncidentesInactivos(PMBookDataContext DB, int idCliente)
        {
            DTO_Incidente_Result Result = new DTO_Incidente_Result()
            {
                bError = false,
                msgErr = string.Empty
            };

            try
            {
                Result.Incidentes = (from a in DB.PMB_Actividades
                                     join p in DB.Ctl_Productos
                                     on a.nProducto equals p.nProducto
                                     join m in DB.Ctl_ProductosModulos
                                     on a.nProductoModulo equals m.nProductoModulo
                                     join pc in DB.Ctl_ProductosComponentes
                                     on a.nProductoComponente equals pc.nProductoComponente
                                     join c in DB.CTM_Clientes
                                     on a.nCliente equals c.nCliente
                                     where a.nEmisor == DataSesion.nEmisor
                                     && a.nCliente == idCliente
                                     && !(a.bActivo)
                                     || a.nEmisor == DataSesion.nEmisor 
                                     && a.nCliente == idCliente
                                     && !(a.bActivo)
                                     select new DTO_Incidente()
                                     {
                                         nIncidente = (a.nActividad != null) ? a.nActividad : 0,
                                         Incidente = a.tDescripcion ?? string.Empty,
                                         Cliente = c.cDescripcion ?? string.Empty,
                                         Producto = p.cDescripcion ?? string.Empty,
                                         Modulo = m.cDescripcion ?? string.Empty,
                                         Componente = pc.cDescripcion ?? string.Empty,
                                         T_Solicitado_UE = (a.nEstimacionSolicitadaUE != null) ? a.nEstimacionSolicitadaUE : 0,
                                         T_Autorizado = (a.nEstimacionAutorizadaHoras != null) ? a.nEstimacionAutorizadaHoras : 0,
                                         Trab_Realizado = (a.nTrabajoRealizadoHoras != null) ? a.nTrabajoRealizadoHoras : 0,
                                         Trab_Restante = (a.nTrabajoRestanteHoras != null) ? a.nTrabajoRestanteHoras : 0,
                                         Avance = (a.nAvancePorcentualEstimado != null) ? a.nAvancePorcentualEstimado : 0,
                                         Estatus = a.nEstatus,
                                         FechaRegistro = (a.dFecha_Registro != null) ? ((DateTime)a.dFecha_Registro).ToShortDateString() : "Sin fecha de Registro",
                                         HoraRegistro = (a.dFecha_Registro != null) ? ((DateTime)a.dFecha_Registro).ToShortTimeString() : "Sin hora de Registro"


                                     }).OrderBy(x => x.nIncidente).ToList();
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
    public class DTO_Cliente
    {
        public string Cliente { get; set; }
        public int idCliente { get; set; }

    }
    public class DTO_Lider
    {
        public string Lider { get; set; }
        public int idLider { get; set; }

    }
    public class DTO_Tester
    {
        public string Tester { get; set; }
        public int idTester { get; set; }

    }
    public class DTO_Consultor
    {
        public string Consultor { get; set; }
        public int idConsultor { get; set; }

    }
    public class DTO_Incidente {
        public int nIncidente { get; set; }
        public string Incidente { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string Modulo { get; set; }
        public string Componente { get; set; }
        public decimal T_Solicitado_UE { get; set; }
        public decimal T_Autorizado { get; set; }
        public decimal Trab_Realizado { get; set; }
        public decimal Trab_Restante { get; set; }
        public decimal Avance { get; set; }
        public byte Estatus { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }

    }
    public class DTO_Incidente_Result
    {
        public bool bError { get; set; }
        public string msgErr { get; set; }
        public DTO_Incidente Incidente { get; set; }
        public List<DTO_Incidente> Incidentes { get; set; }
        public List<DTO_Producto> Productos { get; set; }
        public List<DTO_Producto_Modulo> Modulos { get; set; }
        public List<DTO_Cliente> Clientes { get; set; }
        public List<DTO_Lider> Lideres { get; set; }
        public List<DTO_Tester> Testers { get; set; }
        public List<DTO_Consultor> Consultores { get; set; }
        public List<DTO_Producto_Componente> Componentes { get; set; }
    }
}