﻿namespace ML
{
    public class ErrorExcel
    {
        public int IdRegistro { get; set; }
        public string Mensaje { get; set; }
        public List<object> Errores { get; set; }
        public bool Correct { get; set; }
        //public ML.Semestre Semestre { get; set; }
    }
}