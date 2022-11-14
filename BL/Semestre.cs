using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasOctubreContext context = new DL.LescogidoProgramacionNcapasOctubreContext())
                {
                    var usuarios = context.Semestres.FromSqlRaw("[SemestreGetAll]").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objSemestre in usuarios)
                        {

                            ML.Semestre semestre = new ML.Semestre();
                            semestre.IdSemestre = objSemestre.IdSemestre;
                            semestre.Nombre = objSemestre.Nombre;

                            result.Objects.Add(semestre);

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
    }
}
