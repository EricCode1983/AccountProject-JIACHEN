using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ExcelProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult  Index()
        {
           


            return View();
        }


        public ActionResult UploadFile()
        {
            string referencePath = "";
            string projectPath = Server.MapPath("~");
            referencePath = projectPath.Replace("ExcelProject", @"Reference\UploadFile");
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0) continue;
                string mimeType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                string fileName = Path.GetFileName(Request.Files[upload].FileName);
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                fileStream.Read(fileData, 0, fileLength);
                Request.Files[upload].SaveAs(referencePath + fileName);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult GetPublicKey()
        {
            string referencePath = "";
            string projectPath = Server.MapPath("~");
            referencePath = projectPath.Replace("ExcelProject", @"Reference\UploadFile");
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0) continue;
                string mimeType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                string fileName = Path.GetFileName(Request.Files[upload].FileName);
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                fileStream.Read(fileData, 0, fileLength);
                Request.Files[upload].SaveAs(referencePath + fileName);
            }
            return Json(new {resultcode=0 });


        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }

}