/*

   ###       ##   ########    ##    #######     ##   
  ## ##    ####      ##     ####   ##     ##  ####   
 ##   ##     ##      ##       ##          ##    ##   
##     ##    ##      ##       ##    #######     ##   
#########    ##      ##       ##   ##           ##   
##     ##    ##      ##       ##   ##           ##   
##     ##  ######    ##     ###### #########  ###### 

    Version: 0.0.3
    Authors: Marta Contreras, Luigelo Davila & Carles Villacañas
    Repo: https://github.com/LuigeloDV/ProyectoA1T121

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ProyectoA1T121
{
    public class Program
    {

        //Definicion de las variables que tendra cada empresa
        public class Empresas
        {
            public int Codigo;
            public string NombreEmpresa;
            public string RepresentanteLegal;
            public int NumeroServicios;
            public int[] ListaServicios = new int[5];

        }

        //Lista de empresas
        public class ListaEmpresas
        {
            public Empresas[] empresas = new Empresas[100];
            public int num = 0;
        }


        //Cargar listas de las empresas que figuran en nuestro registro
        static int CargarLista(ListaEmpresas lista)
        {
            try
            {
        
                StreamReader f = new StreamReader("datos.txt");
                int n = Convert.ToInt32(f.ReadLine());
                int i = 0;

                while (i < n)
                {
                    Empresas u = new Empresas();
                    string linea = f.ReadLine();
                    string[] trozos = linea.Split(',');
                    u.Codigo = Convert.ToInt32(trozos[0]);
                    u.NombreEmpresa = trozos[1];
                    u.RepresentanteLegal = trozos[2];
                    u.NumeroServicios = Convert.ToInt32(trozos[3]);

                    //Lectura de datos de los servicios de nuestras empresas que estan en la linea siguiente
                    string linea2 = f.ReadLine();
                    string[] trozos2 = linea.Split(',');
                    int j = 0;

                    while (j < u.NumeroServicios)
                    {
                        u.ListaServicios[j] = Convert.ToInt32(trozos2[j]);
                        j++;
                    }
                    lista.empresas[i] = u;
                    i = i++;
                }
                lista.num = n;
                f.Close();
                return 0;
            }

            catch (FileNotFoundException)
            {
                return -1;
            }

            catch (FormatException)
            {
                return -2;
            }
        }

        
        //Muestra por pantalla las empresas que figuran en nuestro registro
        static void LecturaEmpresas(ListaEmpresas lista)
        {

            Console.WriteLine("Estas son las empresas que tenemos en nuestros datos:");
            int i = 0;

            while (i < lista.num)
            {
                Empresas aux = lista.empresas[i];
                Console.WriteLine("Codigo: {0}, Desarrollador: {1}, representanteLegal: {2}, numeroServicios:{3}",
                    aux.Codigo, aux.NombreEmpresa, aux.RepresentanteLegal, aux.NumeroServicios);
                i++;
            }
        }

        //Escribimos nuevos datos para posteriormente salvarlos
        static void SalvarDatos(string nom_fichero, ListaEmpresas lista)
        {
            int i;
            StreamWriter f = new StreamWriter(nom_fichero);
            //Define el numero total de servicios de la empresa que se guardara
            f.WriteLine("{0}", lista.num); 
            i = 0;
            while (i < lista.num)
            {

                f.WriteLine("{0} {1} {2} {3} ", lista.empresas[i].Codigo, lista.empresas[i].NombreEmpresa, lista.empresas[i].RepresentanteLegal,
                    lista.empresas[i].NumeroServicios);

                int j = 0;
                while (j < lista.empresas[i].NumeroServicios)
                {
                    f.Write("{0} ", lista.empresas[i].ListaServicios[j]);
                    // Escribimos el codigo de los servicios sin saltar de linea
                    j++;

                }
                //Finalmente saltamos de linea para pasar a la siguiente empresa
                f.WriteLine(); 

                 i++;
            }
            f.Close();
        }


        

        //Añade una empresa a la lista
        static int AddEmpresas(ListaEmpresas lista, Empresas aux)
        {
            if (lista.num < 100)
            {
                lista.empresas[lista.num] = aux;
                lista.num += 1;
            }


            Console.WriteLine("Ponga el código de la nueva empresa:");
            char codigoEmpresa = Convert.ToChar(Console.ReadLine());
            //Insertar variable dentro de array
            Console.WriteLine("Ponga el nombre del representante de la nueva empresa:");
            string nombreRepresentante = Console.ReadLine();
            //Insertar variable dentro de array
            Console.WriteLine("Añada los servicios para la nueva empresa:");
            int numeroServicios = Convert.ToInt32(Console.ReadLine());
            //Insertar variable dentro de array
            if (numeroServicios > 0)
            {
                int i= 0;
                int[] idServicios = new int[numeroServicios];
                while (i <= numeroServicios)
                {
                    Console.WriteLine("Añada el id del servicio número {0}:", i+1);
                     idServicios[i] = Convert.ToInt32(Console.ReadLine());
                    //Insertar variable dentro de array
                    i++;
                }
                //Insertar array dentro del fichero datos.txt teniendo 
                //en cuenta que no puede repetirse el codigo de empresa
                Console.WriteLine("Su empresa ha sido añadida correctamente");
            }
            else
            {
                Console.WriteLine("Error ha introducido un número de servicios no válido");
                Thread.Sleep(2000);
                Console.Clear();
                Main();
            }

            return 0;

        }

        //Dar de alta o eliminar un servicio 
        static int ModificarServicios(ListaEmpresas lista, Empresas aux)
        {
            if (lista.num < 100)
            {
                lista.empresas[lista.num] = aux;
                lista.num += 1;

            }

            //Interfaz con las opciones para el usuario 
            Console.WriteLine("Ecoja la tarea que desea realizar:");
            Console.Write("\td Eliminar un servicio \t Añadir un servicio \n");

            char inputUser = Convert.ToChar(Console.ReadLine());

             if(inputUser == 'd')
                {
                //Suprimir o eliminar un servicio 

                return 1;
                }
            else
            {
                if(inputUser == 'a'){
                    return 0;
                }
                else{
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return -1 ;
                }
            }
        }
        static void Main()
        {
            //Definición
            ListaEmpresas miListaEmpresas = new ListaEmpresas();
            int res = CargarLista(miListaEmpresas);
            if (res == -1)
            {

                Console.WriteLine("The file was not found.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("File read correctly, press any button to continue.");
                // Ask the user to choose an option.
                Console.WriteLine("Choose your language to continue");
                Console.Write("\t1 - English \t2 - Español \t3 - Català \n");
                Console.WriteLine("Select:");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to A1T121 project");
                            Console.WriteLine("\ta - Read file");
                            Console.WriteLine("\t2 - Fuction2");
                            Console.WriteLine("\t3 - Function2");
                            Console.Write("- Select an option:");

                            switch (Console.ReadLine())
                            {
                                case "a":
                                case "A":
                                    {
                                        CargarLista(miListaEmpresas);
                                        Console.ReadKey();

                                    }
                                    break;
                                case "b":
                                case "B":
                                    {

                                    }
                                    break;
                                case "c":
                                case "C":
                                    {

                                    }
                                    break;

                                default:
                                    {
                                        Console.Write("Error, the introduced parameter is not recognized\n The program will be restart automaticaly:");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        Main();
                                    }
                                    break;


                            }
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            Console.WriteLine("Bienvenido al proyecto A1T121");
                            Console.WriteLine("\ta - Leer fichero por pantalla");
                            Console.WriteLine("\t2 - Funcion2");
                            Console.WriteLine("\t3 - Funcion2");
                            Console.Write("- Escoje una función:");
                            switch (Console.ReadLine())
                            {
                                //Definir caso
                                case "a":
                                case "A":
                                    {
                                        CargarLista(miListaEmpresas);
                                        Console.ReadKey();
                                    }
                                    break;
                                //Definir caso
                                case "b":
                                case "B":
                                    {

                                    }
                                    break;
                                //Definir caso
                                case "c":
                                case "C":
                                    {

                                    }
                                    break;
                                //Valor error
                                default:
                                    {
                                        Console.Write("Error, El parámetro introducido no es correcto \n " +
                                                        "El programa se reiniciará automáticamente");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        Main();
                                    }
                                    break;

                            }
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("Benvingut al projecte A1T121");
                            Console.WriteLine("\ta - Llegir fitxer");
                            Console.WriteLine("\t2 - funcio2");
                            Console.WriteLine("\t3 - funcio2");
                            Console.Write("- Escull una funció:");
                            switch (Console.ReadLine())
                            {
                                case "a":
                                case "A":
                                    {
                                        CargarLista(miListaEmpresas);
                                        Console.ReadKey();
                                    }
                                    break;
                                case "b":
                                case "B":
                                    {

                                    }
                                    break;
                                case "c":
                                case "C":
                                    {

                                    }
                                    break;
                                //Valor error
                                default:
                                    {
                                        Console.Write("Error, El paràmetre introduit es incorrecte \n " +
                                                        "El programa es reiniciará automàticament");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        Main();
                                    }
                                    break;

                            }
                        }
                        break;

                    default:
                        {
                            Console.Write("Error, the introduced parameter is not recognized\n The program will be restart automaticaly:");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Main();
                        }
                        break;

                }
            }
        }
    }
}