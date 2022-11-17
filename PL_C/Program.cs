// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

ReadFile();
Console.ReadKey();


static void ReadFile()
{
    string file = @"C:\Users\digis\Documents\Isaac Jair Espinoza Ocampo\BlocdeNotas\LayoutAlumnos.txt";

    if (File.Exists(file))
    {
        
        StreamReader Textfile = new StreamReader(file);
        
        string line;
        line = Textfile.ReadLine();
        while ((line = Textfile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            ML.Alumno alumno = new ML.Alumno();

            alumno.Nombre = lines[0];
            alumno.ApellidoPaterno = lines[1];
            alumno.ApellidoMaterno = lines[2];
            alumno.FechaNacimiento = lines[3];
            alumno.Sexo = lines[4];

            alumno.Semestre = new ML.Semestre();
            alumno.Semestre.IdSemestre = byte.Parse(lines[5]);
            alumno.Imagen = null;



            ML.Result result = BL.Alumno.Add(alumno);


            if (result.Correct)
            {
                Console.WriteLine("Correcto");
                Console.ReadKey();
            }
            else
            {
                string fileError = @"C:\Users\digis\Documents\Isaac Jair Espinoza Ocampo\BlocdeNotas\ErroresLayout.txt";
                //result.ErrorMessage;
                StreamWriter errorFile = new StreamWriter(fileError);
                //CREAR UN TXT DE ERRORES

            }

        }
    }
}