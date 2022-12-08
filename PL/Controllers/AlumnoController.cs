﻿using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AlumnoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = new ML.Result();
            alumno.Semestre = new ML.Semestre();

            ML.Result resultSemestre = BL.Semestre.GetAll();
             //result = BL.Alumno.GetAll(alumno);
            //"UrlAPI": "http://localhost:5246/api/",

            
            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Alumno/Add");
                    //result = bl.alumno.GetAll();
                   
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                            result.Objects.Add(resultItem);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            if (result.Correct)
            {
                alumno.Semestre.Semestres = resultSemestre.Objects;
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los alumnos";
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetAll(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            alumno.Semestre = new ML.Semestre();

            ML.Result resultSemestre = BL.Semestre.GetAll();
            result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                alumno.Semestre.Semestres = resultSemestre.Objects;

                return View(alumno);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los alumnos";
                return View();
            }
        }
        [HttpGet]//muestra las vistas
        public ActionResult Form(int? idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Semestre = new ML.Semestre();
            alumno.Horario = new ML.Horario();
            alumno.Horario.Grupo = new ML.Grupo();
            alumno.Horario.Grupo.Plantel = new ML.Plantel();

            ML.Result resultPlanteles = BL.Plantel.GetAll();
            ML.Result resultSemestre = BL.Semestre.GetAll();

            if (idAlumno == null)
            {
                alumno.Semestre.Semestres = resultSemestre.Objects;
                alumno.Horario.Grupo.Plantel.Planteles = resultPlanteles.Objects;
                return View(alumno);
            }
            else
            {

                //GetbyId
                //ML.Result result = BL.Alumno.GetByIdEF(idAlumno.Value);

                //if (result.Correct)
                //{
                //    alumno = (ML.Alumno)result.Object;
                //    alumno.Semestre.Semestres = resultSemestre.Objects;
                //    alumno.Horario.Grupo.Plantel.Planteles = resultPlanteles.Objects;

                //    ML.Result resultGrupos = BL.Grupo.GetByIdPlantel(alumno.Horario.Grupo.IdGrupo);
                //    alumno.Horario.Grupo.Grupos = resultGrupos.Objects;


                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al consultar el alummno seleccionado";
                //}


                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                alumno.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            if (!ModelState.IsValid)
            {
                alumno.Semestre = new ML.Semestre();
                ML.Result resultSemestre = BL.Semestre.GetAll();

                alumno.Semestre.Semestres = resultSemestre.Objects;
                alumno.Horario = new ML.Horario();
                alumno.Horario.Grupo = new ML.Grupo();
                alumno.Horario.Grupo.Plantel = new ML.Plantel();

                ML.Result resultPlanteles = BL.Plantel.GetAll();
                alumno.Horario.Grupo.Plantel.Planteles = resultPlanteles.Objects;
                return View(alumno);
            }
            else
            {


                ML.Result result = new ML.Result();

                if (alumno.IdAlumno == 0)
                {
                    result = BL.Alumno.Add(alumno);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Alumno agregado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar al alumno" + result.ErrorMessage;
                    }

                }
                else
                {
                    //result = BL.Alumno.Update(alumno);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "Alumno actualizado correctamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrio un error al actualizar al alumno" + result.ErrorMessage;
                    //}

                }
                return View("Modal");
            }
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        public JsonResult CambiarStatus(int idAlumno, bool status)
        {
            ML.Result result = BL.Alumno.ChangeStatus(idAlumno,status);

            return Json(result);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            ML.Result result = BL.Alumno.GetByUsername(userName);
            if (result.Correct)
            {
                ML.Alumno alumno = new ML.Alumno();
                if(alumno.password == password)
                {
                    return View("home", "Index");
                }
                else
                {
                    ViewBag.Mensaje = "el usuario o la contraseña son incorrectos";
                    return PartialView("Modal")
                }
            }
            else
            {
                ViewBag.Mensaje = "el usuario o la contraseña son incorrectos";
                return PartialView("Modal")
            }
            return View();
        }
    }

}
