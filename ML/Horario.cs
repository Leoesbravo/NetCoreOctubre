using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public string Nombre { get; set; }
        public ML.Grupo Grupo { get; set; }
        public ML.Alumno Alumno { get; set; }
    
    }
}
