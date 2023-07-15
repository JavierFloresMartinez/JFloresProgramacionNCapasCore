ReadFile();
Console.ReadKey();


static void ReadFile()
{
    string file = @"C:\Users\digis\OneDrive\Documents\Javier Flores Martinez\datos.txt";


    if (File.Exists(file))
    {

        StreamReader Textfile = new StreamReader(file);
        int n = 1;
        string line;
        line = Textfile.ReadLine();
        while ((line = Textfile.ReadLine()) != null)
        {
            string[] lines = line.Split(",");

            ML.Empleado empleado = new ML.Empleado();

            empleado.NumeroEmpleado = lines[0];
            empleado.Rfc = lines[1];
            empleado.Nombre = lines[2];
            empleado.ApellidoPaterno = lines[3];
            empleado.ApellidoMaterno = lines[4];
            empleado.Email = lines[5];
            empleado.Telefono = lines[6];
            empleado.FechaNacimiento = lines[7];
            empleado.Nss = lines[8];
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = int.Parse(lines[9]);
            empleado.Foto = "";

            ML.Result result = BL.Empleado.Add(empleado);

            if (result.Correct)
            {
                Console.WriteLine("Se inserto el registro" + n);
                Console.ReadKey();
            }
            else
            {
                string fileError = @"C:\Users\digis\OneDrive\Documents\Javier Flores Martinez\MensajeError.txt";
                StreamWriter errorFile = new StreamWriter(fileError);
                errorFile.WriteLine("Ocurrio un error al insertar el texto" + result.ErrorMessage);

                errorFile.Close();
            
            }
            n++;
        }
    }
}