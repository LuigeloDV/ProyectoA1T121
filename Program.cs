using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProyectoA1T121
{
    public class Program
    {
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
                Console.Write("\t1 - English");
                Console.Write("\t2 - Español");
                Console.Write("\t3 - Català\n");
                Console.WriteLine("Select:");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.WriteLine("Welcome to A1T121 project");
                            Console.WriteLine("\ta - Leer fichero por pantalla");
                            Console.WriteLine("\t2 - Español");
                            Console.WriteLine("\t3 - Català");
                            Console.Write("- Select an option:");

                            switch (Console.ReadLine())
                            {
                                case "a":
                                case "A":
                                    {
                                        CargarLista(miListaEmpresas);
                                        
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

                            }
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Bienvenido al proyecto A1T121");
                            Console.WriteLine("\ta - Leer fichero por pantalla");
                            Console.WriteLine("\t2 - Español");
                            Console.WriteLine("\t3 - Català");
                            Console.Write("- Escoje una función:");
                            switch (Console.ReadLine())
                            {
                                case "a":
                                case "A":
                                    {

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

                            }
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine("Benvingut al projecte A1T121");
                            Console.WriteLine("\ta - Leer fichero por pantalla");
                            Console.WriteLine("\t2 - Español");
                            Console.WriteLine("\t3 - Català");
                            Console.Write("- Escull una funció");
                            switch (Console.ReadLine())
                            {
                                case "a":
                                case "A":
                                    {

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

                            }
                        }
                   break;
                    default:
                        {
                            Console.WriteLine("No se reconoce el parámetro seleccionado, presione una tecla para iniciar el programa de nuevo");
                            Console.ReadKey();
                            Main();
                        }
                        break;
                }
            }
        }

        //LISTAS//////////////////////
        static int CargarLista(ListaEmpresas lista)
        {
            try
            {
                StreamReader f = new StreamReader("C:/Users/PC-V/Documents/Luigelo/UPC/TELECOS/SEMESTRE 1/IO/ProyectoFinal/bin/Debug/datos.txt");
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

                    int j = 0;

                    while (j < u.NumeroServicios)
                    {
                        u.ListaServicios[j] = Convert.ToInt32(trozos[j]);
                        j++;
                        lista.empresas[i] = u;
                        i = i++;
                    }
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

        public class ListaEmpresas
        {
            public Empresas[] empresas = new Empresas[100];
            public int num = 0;
        }



        //Lectura de empresas 
        public class Empresas
        {
            public int Codigo;
            public string NombreEmpresa;
            public string RepresentanteLegal;
            public int NumeroServicios;
            public int[] ListaServicios = new int[5];

        }
        static void LecturaEmpresas(ListaEmpresas lista)
        {

            Console.WriteLine("Estas son las empresas que tenemos en nuestros datos:");
            int i = 0;
            while (i < lista.num)
            {
                Empresas aux = lista.empresas[i];
                Console.WriteLine("Codigo: {0}, Desarrollador: {1}, representanteLegal: {2}, numeroServicios:{3}",
                    aux.Codigo, aux.NombreEmpresa, aux.RepresentanteLegal, aux.NumeroServicios);
                i = i + 1;
            }
        }

        //Data save
        static void SalvarDatos(string nom_fichero, ListaEmpresas lista)
        {
            int i;
            StreamWriter f = new StreamWriter(nom_fichero);
            f.WriteLine("{0}", lista.num); //escribimos el numero de servicios
            i = 0;
            while (i < lista.num)
            {
                
                f.WriteLine("{0} {1} {2} {3} ", lista.empresas[i].Codigo, lista.empresas[i].NombreEmpresa, lista.empresas[i].RepresentanteLegal,
                    lista.empresas[i].NumeroServicios);

                int j = 0;
                while (j < lista.empresas[i].NumeroServicios)
                {
                    f.Write("{0} ", lista.empresas[i].ListaServicios[j]);
                    // escribimos los servicios en la misma linea sin saltar de linea
                    j++;

                }
                f.WriteLine(); //saltamos la linea después de los servicios de 1 aplicación

                i = i++;
            }
            f.Close();
        }

       



    }
}