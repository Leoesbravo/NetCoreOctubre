using Microsoft.AspNetCore.Mvc;
using ML;
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
            ML.Result result = new Result();
            return View(result);
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
            
            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null) //sesion nula o que no exista 
            {
                //validar el excel

                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }
                            //leer
                        }
                    }

                }


                
                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {
                //add 
                //errores al agregar 



            }
            return PartialView("Modal");
        }

    }
}
