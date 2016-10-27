using ADSUM;
using MvcLogin.Linq;
using MvcLogin.Models;
using MvcLogin.Helpers.Sesion;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;

namespace MvcLogin.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Realizar el llamado de la vista que contiene la GUI de Autenticación de la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult logIn()
        {
            //var encripdato = Criptografo.EncriptaTexto("1","quimiche");
            //var desencriptado = Criptografo.DesEncriptaTexto(encripdato, "quimiche");
            var model = new UserModel();
            return View("logIn", model);
        }

        /// <summary>
        /// Verificar los datos sumunistrados por el usuario al realizar la peticion Post de envio de información 
        /// mediante la GUI de Autenticación de la aplicación. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult logIn(UserModel user)
        {
			if (ModelState.IsValid)//Verificar que el modelo de datos sea valido en cuanto a la definición de las propiedades
            {
                PMBookDataContext DB = new PMBookDataContext();
				if (Isvalid(user.cLogin, user.Password, DB))//Verificar que el email y clave exista utilizando el método privado 
                {
                    FormsAuthentication.SetAuthCookie(user.cLogin, false); //crea variable de usuario 

                    //Validaciones de restriccion de modulos
                    List<DataArea> lstAreas = (from uxp in DB.Adsum_Usuarios_X_Perfils
                                               join p   in DB.Adsum_Perfiles
                                               on uxp.nPerfil equals p.nPerfil
                                               join dxp in DB.ADSUM_Derechos_X_Perfils
                                               on uxp.nPerfil equals dxp.nPerfil
                                               join a   in DB.ADSUM_Navegadors
                                               on dxp.nRama   equals a.nRama
                                               join m   in DB.ADSUM_Formas
                                               on a.cForma    equals m.cForma
                                               where uxp.cLogin == user.cLogin
                                               select new DataArea()
                                               {
                                                   cArea   = a.cArea,
                                                   cURL    = m.cControlador,
                                                   cImagen = string.Empty
                                               }).ToList();

                    this.Session["DataNav"]    = lstAreas;
                    DataSesion CI              = new DataSesion();
                    ADSUM_Usuario B            = DB.ADSUM_Usuarios.Where(x => x.cLogin == user.cLogin).SingleOrDefault();
                    CI.nIdUsuario              = B.nAdsumUsuario;
                    CI.cLogin                  = user.cLogin;
                    CI.bSesionActiva           = true;
                    CI.nEmisor                 = (int)B.nEmisor;
                    this.Session["DataSesion"] = CI;
                    return Json(new { bError   = false, url = lstAreas[0].cURL }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { bError = true, cError = "Constraseña o Usuario incorrectos" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { bError = true, cError = "Constraseña o Usuario incorrectos" }, JsonRequestBehavior.AllowGet);
                //return View(user);
            }
        }

        /// <summary>
        /// Cerrar sesion del usuario autenticado
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Eliminar datos de sesion
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("logIn", "User");
        }

        /// <summary>
        /// Metodo para validar el email y password del usuario, realiza la consulta a la bd
        /// </summary>
        /// <param name="cLogin">Email ingresado</param>
        /// <param name="password">Password ingresado</param>
        /// <returns>
        /// True:Usuario valido
        /// False Usuario Invalido
        /// </returns>
        private bool Isvalid(string cLogin, string password, PMBookDataContext db)
        {
            bool Isvalid = false;
            ADSUM_Usuario usr = (from u in db.ADSUM_Usuarios where u.cLogin == cLogin select u).SingleOrDefault();
            if (usr != null)
            {
                string sa = Criptografo.DesEncriptaTexto(db.ADSUM_Usuarios.Where(x => x.cLogin == cLogin).SingleOrDefault().cPassword, "quemiche");
                if (Criptografo.DesEncriptaTexto(db.ADSUM_Usuarios.Where(x => x.cLogin == cLogin).SingleOrDefault().cPassword, "quemiche") == password)  //Verificar password del usuario
                {
                    Isvalid = true;
                }
                else
                {
                    Isvalid = false;
                }

            }
            return Isvalid;
        }

        public ActionResult onClickCifrar()
        {
            //RijndaelCrypt Criptografo = new RijndaelCrypt();
            var nuestra_cadena = "1";
            //string encriptada = Criptografo.Encrypt(nuestra_cadena);
            //string desencriptada = Criptografo.Decrypt(encriptada);

            string encriptada    = Criptografo.EncriptaTexto(nuestra_cadena, "quemiche");
            string desencriptada = Criptografo.DesEncriptaTexto(encriptada, "quemiche");
              
            return Json(new
            {
                encriptada,
                desencriptada
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {

            return View("Error");
        }
        public ActionResult E500()
        {

            return View("Error");
        }
        public ActionResult E404()
        {

            return View("Error");
        }


    }
}
