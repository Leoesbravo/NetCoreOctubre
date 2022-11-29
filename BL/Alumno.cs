using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
                {
                    alumno.Semestre.IdSemestre = (alumno.Semestre.IdSemestre == null) ? 0 : alumno.Semestre.IdSemestre; //operador ternario
                    var usuarios = context.Alumnos.FromSqlRaw($"AlumnoGetAll '{alumno.Nombre}', '{alumno.ApellidoPaterno}', {alumno.Semestre.IdSemestre}").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objAlumno in usuarios)
                        {

                            ML.Alumno alumnoobj = new ML.Alumno();
                            alumnoobj.IdAlumno = objAlumno.IdAlumno;
                            alumnoobj.Nombre = objAlumno.Nombre;
                            alumnoobj.ApellidoPaterno = objAlumno.ApellidoPaterno;
                            alumnoobj.ApellidoMaterno = objAlumno.ApellidoMaterno;
                            alumnoobj.FechaNacimiento = objAlumno.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            alumnoobj.Sexo = objAlumno.Sexo;

                            alumnoobj.Semestre = new ML.Semestre();
                            alumnoobj.Semestre.IdSemestre = objAlumno.IdSemestre.Value;
                            alumnoobj.Semestre.Nombre = objAlumno.Semestre;
                            alumnoobj.Status = objAlumno.Status.Value;

                            result.Objects.Add(alumnoobj);

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

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tablealumno = new DataTable();

                        da.Fill(tablealumno);

                        if (tablealumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tablealumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.Nombre = row[0].ToString();
                                alumno.ApellidoPaterno = row[1].ToString();
                                alumno.ApellidoMaterno = row[2].ToString();
                                alumno.FechaNacimiento = row[3].ToString();
                                alumno.Sexo = row[4].ToString();

                                alumno.Semestre = new ML.Semestre();
                                alumno.Semestre.IdSemestre = int.Parse(row[5].ToString());



                                result.Objects.Add(alumno);
                            }

                            result.Correct = true;

                        }

                        if (tablealumno.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Alumno alumno in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;


                    alumno.Nombre = (alumno.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : alumno.Nombre; //operador ternario

                    if (alumno.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }
                    if (alumno.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno ";
                    }
                    if (alumno.Semestre.IdSemestre.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }



                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        public static ML.Result ChangeStatus(int idAlumno, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"AlumnoChangeStatus {idAlumno}, {status}");

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
    }
}