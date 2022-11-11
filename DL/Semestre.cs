﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Semestre
{
    public int IdSemestre { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Alumno> Alumnos { get; } = new List<Alumno>();

    public virtual ICollection<Materium> Materia { get; } = new List<Materium>();
}
