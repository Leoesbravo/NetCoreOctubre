using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; } //validando, manejar el flujo de mi codigo, me dices si salio bien o no mi metodo
        public string ErrorMessage { get; set; } //almanecenar el mensaje de error
        public object Object { get; set; } //Solo un objeto
        public List<object> Objects { get; set; } //Lista de objetos 
        public Exception Ex { get; set; } //almacenar la excepción comp
    }
}
