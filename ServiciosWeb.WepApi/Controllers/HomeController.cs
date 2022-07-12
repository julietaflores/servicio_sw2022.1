using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiciosWeb.WepApi.Models;

namespace ServiciosWeb.WepApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        //GetSubirArchivo
        public ActionResult SubirArchivo()
        {
            return View();
        }
        //Post SubirArchivo
        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
            SubirArchivoModelo modelo = new SubirArchivoModelo();
            if (file != null)
            {
               string[] paths = { @"F:\Fotos\" };
               string fullPath = Path.Combine(paths);
                //string[] paths = {@"d:\archives", "2001", "media", "images"};
                string ruta1 =(fullPath);
                ruta1 += file.FileName;
              //  ruta += file.FileName;
                modelo.SubirArchivo(ruta1, file);
                ViewBag.Error = modelo.error;
                ViewBag.Correcto = modelo.Confirmacion;
            }
            return View();
        }
    }
}
