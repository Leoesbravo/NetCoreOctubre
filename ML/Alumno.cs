using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public List<object> Alumnos { get; set; }

        //Propiedad de navegación
        public ML.Semestre Semestre { get; set; }
        public ML.Horario Horario { get; set; }

        //public ML.Horario Horario { get; set; }
    }
}
