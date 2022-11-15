using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
                {
                    var usuarios = context.Alumnos.FromSqlRaw("AlumnoGetAll") .ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objAlumno in usuarios)
                        {

                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = objAlumno.IdAlumno;
                            alumno.Nombre = objAlumno.Nombre;
                            alumno.ApellidoPaterno = objAlumno.ApellidoPaterno;
                            alumno.ApellidoMaterno = objAlumno.ApellidoMaterno;
                            alumno.FechaNacimiento = objAlumno.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            alumno.Sexo = objAlumno.Sexo;

                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = objAlumno.IdSemestre.Value;
                            alumno.Semestre.Nombre = objAlumno.Semestre;

                            result.Objects.Add(alumno);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
                {
                    //var usuarios = context.AlumnoUpdate(alumno.IdAlumno, alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno, alumno.FechaNacimiento, alumno.Sexo, alumno.Semestre.IdSemestre, alumno.Horario.Nombre, alumno.Horario.Grupo.IdGrupo);
                    var usuarios = context.Database.ExecuteSqlRaw($"AlumnoAdd '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.FechaNacimiento}', '{alumno.Sexo}', {alumno.Semestre.IdSemestre},'{alumno.Imagen}'");

                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        //public static ML.Result GetById(int idAlumno)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
        //        {
        //            var obj = context.Alumnos.FromSqlRaw($"[AlumnoGetById] {idAlumno}").FirstOrDefault();
        //            result.Objects = new List<object>();

        //            if (obj != null)
        //            {
        //                ML.Alumno alumno = new ML.Alumno();
        //                alumno.IdAlumno = obj.IdAlumno;
        //                alumno.Nombre = obj.Nombre;
        //                alumno.ApellidoPaterno = obj.ApellidoPaterno;
        //                alumno.ApellidoMaterno = obj.ApellidoMaterno;
        //                alumno.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
        //                alumno.Sexo = obj.Sexo;

        //                alumno.Semestre = new ML.Semestre();
        //                alumno.Semestre.IdSemestre = obj.IdSemestre.Value;

        //                alumno.Horario = new ML.Horario();
        //                alumno.Horario.IdHorario = obj.IdHorario;
        //                alumno.Horario.Nombre = obj.HorarioNombre;

        //                alumno.Horario.Grupo = new ML.Grupo();
        //                alumno.Horario.Grupo.IdGrupo = obj.IdGrupo;
        //                alumno.Horario.Grupo.Nombre = obj.NombreGrupo;

        //                alumno.Horario.Grupo.Plantel = new ML.Plantel();
        //                alumno.Horario.Grupo.Plantel.IdPlantel = obj.IdPlantel;
        //                alumno.Horario.Grupo.Plantel.Nombre = obj.NombrePlantel;

        //                result.Object = alumno;
        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se pudo realizar la consulta";
        //            }

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}
    }
}