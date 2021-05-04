using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace Desafio3
{
    class Program
    {
        static StreamReader Leer;
        static StreamWriter Escribir;
        static String Path_Usu = @".\..\..\..\Login_Usu\";
        static String Path_Emple = @".\..\..\..\calculoEmple\";
        static String Path_Hist = @".\..\..\..\hist\";
        static Empleados[] c_empleados = new Empleados[3];

        //Usuarios
        struct Usuarios
        {
            public int ID_Usu;
            public String N_Usu;
            public String Pass_Usu;
            public int Estado_Usu;
            public int Intento_Usu;
        }
        //Empleados
        struct Empleados
        {
            public int ID_Empleado;
            public String Nombre;
            public String Apelido;
            public String Cargo;
            public int horasTrabajadas;
            public double sueldo_Base;
            public double sueldo_Liquido;
            public double desc_ISSS;
            public double desc_AFP;
            public double desc_Renta;
            public double bono;

        }
        static void Main(string[] args)
        {
            ValidacionArchivos();

            Login();

            Menu();




            Console.ReadKey();
        }

        //Validacion
        static void ValidacionArchivos()
        {
            //Validacion que exista la carpeta de usuarios
            if (!Directory.Exists(Path_Usu))
            {

                Console.WriteLine("Directorio inexistente --> Creando el directorio: {0}", Path_Usu);
                DirectoryInfo diU = Directory.CreateDirectory(Path_Usu);


            }
            //Validacion que exista el archivo de usuarios
            if (!File.Exists(Path_Usu + "usuarios.txt"))
            {
                Console.WriteLine($"Archivo inexistente --> Creando archivo en el directorio: {Path_Usu + "usuarios.txt"}");
                File.WriteAllText(Path_Usu + "usuarios.txt", "");
            }
            //Validacion que exista la carpeta de empleados
            if (!Directory.Exists(Path_Emple))
            {
                Console.WriteLine("Directorio inexistente --> Creando el directorio: {0}", Path_Emple);
                DirectoryInfo diE = Directory.CreateDirectory(Path_Emple);

            }
            //Validacion que exista la carpeta de historial
            if (!Directory.Exists(Path_Hist))
            {

                Console.WriteLine("Directorio inexistente --> Creando el directorio: {0}", Path_Hist);
                DirectoryInfo diU = Directory.CreateDirectory(Path_Hist);


            }
            
            Console.ReadKey();

        }


        //Login
        static void Login()
        {

            Usuarios usuarios = new Usuarios();           

            Leer = new StreamReader(Path_Usu + "usuarios.txt", true);
            if (Leer.ReadToEnd() == "")
            {
                Leer.Close();
                Console.WriteLine("\n");
                Console.WriteLine("\n No hay usuarios registrados");
                Console.WriteLine("\n Por favor, ingrese las credenciales");
                usuarios.ID_Usu = 1;
                Console.WriteLine("\n Usuario: ");
                usuarios.N_Usu = Console.ReadLine();
                Console.WriteLine("\n Contraseña: ");
                usuarios.Pass_Usu = Console.ReadLine();
                usuarios.Estado_Usu = 0;
                usuarios.Intento_Usu = 0;

                Escribir = new StreamWriter(Path_Usu + "usuarios.txt", true);
                Escribir.WriteLine($"{usuarios.ID_Usu}-{ usuarios.N_Usu}-{ usuarios.Pass_Usu}-{ usuarios.Estado_Usu}-{ usuarios.Intento_Usu}");
                Escribir.Close();
            }
            else
            {
                Leer.Close();
            }
            bool a = true;
            do
            {
                Console.Clear();
                Console.WriteLine("\n            ---=INICIAR SESIÓN=---           ");
                Console.WriteLine("\n");
                Console.WriteLine("\nUsuario: ");
                string input_usu = Console.ReadLine();
                Console.WriteLine("\nContraseña: ");
                string input_pass = Console.ReadLine();
                Leer = new StreamReader(Path_Usu + "usuarios.txt", true);

                int j = 0;
                char delim = '-';
                //contador de usuarios
                for (int i = 0; i <= j; i++)
                {
                    if (Leer.ReadLine() != null)
                    {

                        j++;
                    }

                }
                Leer.Close();

                Leer = new StreamReader(Path_Usu + "usuarios.txt", true);
                Usuarios[] c_usu = new Usuarios[j];
                string[] s_usu = new string[j];

                for (int i = 0; i < s_usu.Length; i++)
                {
                    s_usu[i] = Leer.ReadLine();
                    string[] temp = s_usu[i].Split(delim);

                    c_usu[i].ID_Usu = Int32.Parse(temp[0]);
                    c_usu[i].N_Usu = temp[1].ToString();
                    c_usu[i].Pass_Usu = temp[2].ToString();
                    c_usu[i].Estado_Usu = Int32.Parse(temp[3]);
                    c_usu[i].Intento_Usu = Int32.Parse(temp[4]);
                }
                for (int i = 0; i < c_usu.Length; i++)
                {

                    if (input_usu == c_usu[i].N_Usu)
                    {
                        if (input_pass == c_usu[i].Pass_Usu)
                        {
                            if (c_usu[i].Estado_Usu == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Usuario bloqueado por 3 intentos fallidos, no puede iniciar sesión");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("¿Desea desbloquear Usuario?[0 = si][1 = no]");
                                Console.ForegroundColor = ConsoleColor.White;
                                int d = Int32.Parse(Console.ReadLine());
                                if (d == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Usuario desbloqueado, puede volver iniciar sesión ;)");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    c_usu[i].Estado_Usu = 0;
                                    c_usu[i].Intento_Usu = 0;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Usuario continua bloqueado ;(");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Inicio de sesión exitoso ;)");
                                Console.ForegroundColor = ConsoleColor.White;
                                c_usu[i].Intento_Usu = 0;
                                a = false;
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                        else
                        {
                            if (c_usu[i].Intento_Usu == 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ha llegado al máximo de intentos");
                                Console.WriteLine("Usuario Bloqueado");
                                Console.ForegroundColor = ConsoleColor.White;
                                c_usu[i].Estado_Usu = 1;
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Contraseña Incorrecta, solo tiene 3 intentos");
                                Console.ForegroundColor = ConsoleColor.White;
                                c_usu[i].Intento_Usu = c_usu[i].Intento_Usu + 1;
                                if (c_usu[i].Intento_Usu == 3)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nHa llegado al máximo de intentos");
                                    Console.WriteLine("Usuario Bloqueado");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    c_usu[i].Estado_Usu = 1;
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                }
                Leer.Close();
                File.Delete(Path_Usu + "usuarios.txt");


                Escribir = new StreamWriter(Path_Usu + "usuarios.txt", true);
                for (int i = 0; i < c_usu.Length; i++)
                {
                    Escribir.WriteLine($"{c_usu[i].ID_Usu}-{ c_usu[i].N_Usu}-{ c_usu[i].Pass_Usu}-{ c_usu[i].Estado_Usu}-{ c_usu[i].Intento_Usu}");

                }
                Escribir.Close();

                Console.Clear();
            } while (a);
        }
        //Menu
        static void Menu()
        {
            int j = 1;
            for (int i = 0; i < j; i++)
            {
                Console.WriteLine("\n               --=MENU PRINCIPAL=--               ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n ===============================================");
                Console.WriteLine("\n");
                Console.WriteLine("\t1) Ingresar Empleados");
                Console.WriteLine("\t2) Calculo de salarios");
                Console.WriteLine("\t3) Cerrar Sesión");
                Console.WriteLine("\n");
                Console.WriteLine("\n ===============================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n");
                Console.Write("\tDigitar la opción deseada [1..3]: ");
                int opc = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n");

                switch (opc)
                {
                    case 1:
                        Ingreso_Empleados();
                        break;
                    case 2:
                        calculo_Salario();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\tOpcion erronea, acepta[1..4]");
                        j++;
                        break;
                }
                Console.Clear();
            }
        }

        static void Ingreso_Empleados()
        {
            Console.Clear();

            int ij = 1;
            for (int i = 0; i < ij; i++)
            {
                Console.WriteLine("\n               --=INGRESO DE EMPLEADOS=--               ");
                Console.WriteLine("\n");
                try
                {



                    int[] cod = new int[c_empleados.Length];
                    for (int k = 0; k < c_empleados.Length; k++)
                    {
                        bool c = true;
                        //[]
                        Console.WriteLine($"\n\tEmpleado #{k + 1}.");

                        Console.WriteLine("\nID/Codigo del empleado (No se pueden repetir): ");
                        c_empleados[k].ID_Empleado = Int32.Parse(Console.ReadLine());
                        cod[k] = c_empleados[k].ID_Empleado;

                        int m = c_empleados.Length;
                        for (int l = 0; l < m; l++)
                        {
                            if (c_empleados[k].ID_Empleado != c_empleados[l].ID_Empleado)
                            {
                                c = true;

                            }
                            else
                            {
                                if (k == l)
                                {
                                    c = true;
                                }
                                else
                                {
                                    Console.WriteLine("\nID/Codigo del empleado repetido, por favor ingrese de nuevo ");
                                    c = false;
                                    k--;
                                }

                            }
                        }
                        if (c)
                        {
                            bool d = true;
                            Console.WriteLine("\nNombres del empleado: ");
                            c_empleados[k].Nombre = Console.ReadLine();
                            Console.WriteLine("\nApellidos del empleado: ");
                            c_empleados[k].Apelido = Console.ReadLine();

                            while (d)
                            {
                                Console.WriteLine("\nCargo del empleado ");
                                Console.WriteLine("\n\t1) Gerente");
                                Console.WriteLine("\n\t2) Asistente");
                                Console.WriteLine("\n\t3) Secretaria");
                                Console.WriteLine("\n\t4) Otros");
                                Console.WriteLine("\nIngrese el numero de las opciones: ");
                                int car = Int32.Parse(Console.ReadLine());
                                if (car >= 1 && car <= 4)
                                {
                                    d = false;
                                    if (car == 1)
                                    {
                                        c_empleados[k].Cargo = "Gerente";
                                    }
                                    else if (car == 2)
                                    {
                                        c_empleados[k].Cargo = "Asistente";
                                    }
                                    else if (car == 3)
                                    {
                                        c_empleados[k].Cargo = "Secretaria";
                                    }
                                    else if (car == 4)
                                    {
                                        c_empleados[k].Cargo = "Otros";
                                    }
                                    bool e = true;
                                    while (e)
                                    {
                                        Console.WriteLine("\nHoras trabajadas del empleado: ");
                                        c_empleados[k].horasTrabajadas = Int32.Parse(Console.ReadLine());
                                        if (c_empleados[k].horasTrabajadas <= 0)
                                        {
                                            Console.WriteLine("\nIngrese horas trabajadas no negativas,ni menores a 1h");
                                        }
                                        else
                                        {
                                            e = false;
                                        }

                                    }

                                }
                                else
                                {
                                    Console.WriteLine("\nIngrese las opcions validas de 1 a 4");
                                }
                            }

                        }

                    }
                }
                catch (Exception)
                {
                    ij++;
                    Console.WriteLine($"\n\nPor favor ingrese los datos correctos en los campos que corresponden.");


                }

            }
            Console.WriteLine($"\n\nFinal de ingreso de empleados");
            Console.ReadKey();
            Console.Clear();
            Menu();

        }

        static void calculo_Salario()
        {
            double desc = 0, isss = 0.0525, AFP = 0.0688, rent = 0.1;
            string nb = "NO HAY BONO";

            for (int i = 0; i < c_empleados.Length; i++)
            {
                //[]
                if (c_empleados[i].horasTrabajadas <= 160)
                {
                    c_empleados[i].sueldo_Base = Math.Round((c_empleados[i].horasTrabajadas * 9.75), 2);
                }
                else
                {
                    double hex1;

                    hex1 = c_empleados[i].horasTrabajadas - 160;
                    c_empleados[i].sueldo_Base = Math.Round((c_empleados[i].horasTrabajadas * 9.75), 2) + Math.Round((hex1 * 11.5), 2);

                }
                c_empleados[i].desc_ISSS = Math.Round(c_empleados[i].sueldo_Base * isss, 2);
                c_empleados[i].desc_AFP = Math.Round(c_empleados[i].sueldo_Base * AFP, 2);
                c_empleados[i].desc_Renta = Math.Round(c_empleados[i].sueldo_Base * rent, 2);
                desc = c_empleados[i].desc_ISSS + c_empleados[i].desc_AFP + c_empleados[i].desc_Renta;
                c_empleados[i].sueldo_Liquido = c_empleados[i].sueldo_Base - desc;

                if (c_empleados[i].Cargo == "Gerente")
                {
                    c_empleados[i].bono = Math.Round(c_empleados[i].sueldo_Liquido * 0.10, 2);
                }
                else if (c_empleados[i].Cargo == "Asistente")
                {
                    c_empleados[i].bono = Math.Round(c_empleados[i].sueldo_Liquido * 0.05, 2);
                }
                else if (c_empleados[i].Cargo == "Secretaria")
                {
                    c_empleados[i].bono = Math.Round(c_empleados[i].sueldo_Liquido * 0.03, 2);
                }
                else if (c_empleados[i].Cargo == "Otros")
                {
                    c_empleados[i].bono = Math.Round(c_empleados[i].sueldo_Liquido * 0.02, 2);
                }

            }

            DateTime date1 = DateTime.Now;
            Console.WriteLine(date1.ToString("ddMMyyyy_HH:mm"));
            string file ="calculoSalario_" + date1.ToString("ddMMyyyy_HHmmss");

            Escribir = new StreamWriter(Path_Emple + file + ".txt", true);


            Console.Clear();
            Escribir.WriteLine("\n/----------------------------Datos----------------------------");
            Console.WriteLine("\n/----------------------------Datos----------------------------");
            for (int j = 0; j < c_empleados.Length; j++)
            {
                Escribir.WriteLine("\n/*************************************************************");
                Escribir.WriteLine($"\n\t\t--Empleado # {j + 1}--");
                Escribir.WriteLine($"\n\tNombre completo: {c_empleados[j].Nombre} {c_empleados[j].Apelido}");
                Escribir.WriteLine($"\n\tCargo: {c_empleados[j].Cargo}");
                Escribir.WriteLine($"\n\t\t--Descuentos--");
                Escribir.WriteLine($"\n\tISSS: ${c_empleados[j].desc_ISSS}");
                Escribir.WriteLine($"\n\tAFP: ${c_empleados[j].desc_AFP}");
                Escribir.WriteLine($"\n\tRenta: ${c_empleados[j].desc_Renta}");
                Escribir.WriteLine($"\n\tSueldo Base: ${c_empleados[j].sueldo_Base}");
                Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[j].sueldo_Liquido}");
                Escribir.WriteLine("\n/*************************************************************");

                
                Console.WriteLine("\n/*************************************************************");
                Console.WriteLine($"\n\t\t--Empleado # {j + 1}--");
                Console.WriteLine($"\n\tNombre completo: {c_empleados[j].Nombre} {c_empleados[j].Apelido}");
                Console.WriteLine($"\n\tCargo: {c_empleados[j].Cargo}");
                Console.WriteLine($"\n\t\t--Descuentos--");
                Console.WriteLine($"\n\tISSS: ${c_empleados[j].desc_ISSS}");
                Console.WriteLine($"\n\tAFP: ${c_empleados[j].desc_AFP}");
                Console.WriteLine($"\n\tRenta: ${c_empleados[j].desc_Renta}");
                Console.WriteLine($"\n\tSueldo Base: ${c_empleados[j].sueldo_Base}");
                Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[j].sueldo_Liquido}");
                Console.WriteLine("\n/*************************************************************");
            }
            Escribir.WriteLine("\n\t\t--->Bonos<---");
            Console.WriteLine("\n\t\t--->Bonos<---");
            if (c_empleados[0].Cargo == "Gerente" && c_empleados[1].Cargo == "Asistente" && c_empleados[2].Cargo == "Secretaria")
            {
                Escribir.WriteLine($"\n\t\t\a{nb}");
                Escribir.WriteLine("\n/*************************************************************");

                Console.WriteLine($"\n\t\t\a{nb}");
                Console.WriteLine("\n/*************************************************************");
            }
            else
            {
                Escribir.WriteLine("\n\t\t--Empleado # 1--");
                Escribir.WriteLine($"\n\tBono: ${c_empleados[0].bono}");
                Escribir.WriteLine("\n/*************************************************************");
                Escribir.WriteLine("\n\t\t--Empleado # 2--");
                Escribir.WriteLine($"\n\tBono: ${c_empleados[1].bono}");
                Escribir.WriteLine("\n/*************************************************************");
                Escribir.WriteLine("\n\t\t--Empleado # 3--");
                Escribir.WriteLine($"\n\tBono: ${c_empleados[2].bono}");
                Escribir.WriteLine("\n/*************************************************************");

                Console.WriteLine("\n\t\t--Empleado # 1--");
                Console.WriteLine($"\n\tBono: ${c_empleados[0].bono}");
                Console.WriteLine("\n/*************************************************************");
                Console.WriteLine("\n\t\t--Empleado # 2--");
                Console.WriteLine($"\n\tBono: ${c_empleados[1].bono}");
                Console.WriteLine("\n/*************************************************************");
                Console.WriteLine("\n\t\t--Empleado # 3--");
                Console.WriteLine($"\n\tBono: ${c_empleados[2].bono}");
                Console.WriteLine("\n/*************************************************************");
            }
            Escribir.WriteLine("\n\t\t--->Empleado con mayor salario<---");
            Console.WriteLine("\n\t\t--->Empleado con mayor salario<---");

            if (c_empleados[0].sueldo_Liquido < c_empleados[1].sueldo_Liquido)
            {
                if (c_empleados[1].sueldo_Liquido < c_empleados[2].sueldo_Liquido)
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 3--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 3--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
                else
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 2--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 2--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
            }
            else if (c_empleados[0].sueldo_Liquido > c_empleados[1].sueldo_Liquido)
            {
                if (c_empleados[0].sueldo_Liquido < c_empleados[2].sueldo_Liquido)
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 3--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 3--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
                else
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 1--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 1--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
            }
            Escribir.WriteLine("\n\t\t--->Empleado con menor salario<---");
            Console.WriteLine("\n\t\t--->Empleado con menor salario<---");
            if (c_empleados[0].sueldo_Liquido > c_empleados[1].sueldo_Liquido)
            {
                if (c_empleados[1].sueldo_Liquido > c_empleados[2].sueldo_Liquido)
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 3--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 3--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
                else
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 2--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 2--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
            }
            else if (c_empleados[0].sueldo_Liquido < c_empleados[1].sueldo_Liquido)
            {
                if (c_empleados[0].sueldo_Liquido > c_empleados[2].sueldo_Liquido)
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 3--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 3--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
                else
                {
                    Escribir.WriteLine("\n\t\t--Empleado # 1--");
                    Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                    Escribir.WriteLine("\n/*************************************************************");

                    Console.WriteLine("\n\t\t--Empleado # 1--");
                    Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                    Console.WriteLine("\n/*************************************************************");
                }
            }

            Escribir.WriteLine("\n\t\t--->Empleados que ganan mas de $300<---");
            Console.WriteLine("\n\t\t--->Empleados que ganan mas de $300<---");

            if (c_empleados[0].sueldo_Liquido >= 300)
            {
                Escribir.WriteLine("\n\t\t--Empleado # 1--");
                Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                Escribir.WriteLine("\n/*************************************************************");

                Console.WriteLine("\n\t\t--Empleado # 1--");
                Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[0].sueldo_Liquido}");
                Console.WriteLine("\n/*************************************************************");
            }
            //Empleado 2
            if (c_empleados[1].sueldo_Liquido >= 300)
            {
                Escribir.WriteLine("\n\t\t--Empleado # 2--");
                Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                Escribir.WriteLine("\n/*************************************************************");

                Console.WriteLine("\n\t\t--Empleado # 2--");
                Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[1].sueldo_Liquido}");
                Console.WriteLine("\n/*************************************************************");
            }
            //Empleado 3
            if (c_empleados[2].sueldo_Liquido >= 300)
            {
                Escribir.WriteLine("\n\t\t--Empleado # 3--");
                Escribir.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                Escribir.WriteLine("\n/*************************************************************");

                Console.WriteLine("\n\t\t--Empleado # 3--");
                Console.WriteLine($"\n\tSueldo Liquido: ${c_empleados[2].sueldo_Liquido}");
                Console.WriteLine("\n/*************************************************************");
            }
            Escribir.Close();
            Zip_data(file);
            Console.Clear();
            Menu();

        }

        private static void Zip_data(string file)
        {
            
            ZipFile.CreateFromDirectory(Path_Emple, Path_Hist + file + ".zip");
            System.Diagnostics.Process.Start("notepad.exe", Path_Emple + file + ".txt");
            Console.WriteLine("\n\n\tPulsa cualquier tecla");
            Console.ReadKey();
            
        }

    }
}

