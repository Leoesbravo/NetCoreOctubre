using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];

            
            if (fileTXT != null)
            {

                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                string line;
                line = Textfile.ReadLine();
                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    ML.Alumno alumno = new ML.Alumno();

                    alumno.Nombre = lines[0];
                    alumno.ApellidoPaterno = lines[1];
                    alumno.ApellidoMaterno = lines[2];
                    alumno.FechaNacimiento = lines[3];
                    alumno.Sexo = lines[4];

                    alumno.Semestre = new ML.Semestre();
                    alumno.Semestre.IdSemestre = byte.Parse(lines[5]);
                    alumno.Imagen = null;



                    ML.Result result = BL.Alumno.Add(alumno);


                    if (!result.Correct)
                    {
                        string fileError = @"C:\Users\digis\Documents\Isaac Jair Espinoza Ocampo\BlocdeNotas\ErroresLayout.txt";
                        //result.ErrorMessage;
                        StreamWriter errorFile = new StreamWriter(fileError);
                        //CREAR UN TXT DE ERRORES
                    }
                    else
                    {
                      

                    }

                }
            }
            return PartialView("Modal");
        }
        [HttpPost]
        public ActionResult AlumnoCargaMasiva(ML.Alumno alumno)
        {
            
            IFormFile archivo = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
               
            }
            else
            {
                



            }
            return PartialView("Modal");
        }

    }
}
