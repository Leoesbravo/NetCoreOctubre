using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                ML.Alumno alumno = new ML.Alumno();
                alumno.Alumnos = result.Objects;
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
    }
}
