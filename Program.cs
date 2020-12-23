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
        class Empresa
        {
            public int Codigo;
            public string NombreEmpresa;
            public string RepresentanteLegal;
            public int NumeroServicios;
            public int[] ListaServicios = new int[5];

        }

        //Lista de empresas
        class ListaEmpresas
        {
            public Empresa[] Empresas = new Empresa[100];
            public int num = 0;
        }

        //Muestra por pantalla las empresas que figuran en nuestro registro
        static void LecturaEmpresas(ListaEmpresas lista)
        {

           
            int i = 0;
            while (i < lista.num)
            {
                Empresa Empresa = lista.Empresas[i];

                Console.WriteLine("\tEnterprise #" + (i+1));
                Console.Write(
                                "Codigo: {0}\n " +
                                "Desarrollador:.................{1}\n " +
                                "Representante Legal:...........{2}\n " +
                                "Total Servicios:...............{3}\n ", 

                                 Empresa.Codigo,
                                 Empresa.NombreEmpresa,
                                 Empresa.RepresentanteLegal, 
                                 Empresa.NumeroServicios);

                int j = 0;
                while (j < Empresa.NumeroServicios)
                {
                    Console.Write(
                                "ID Servicio:...................{0}\n ", Empresa.ListaServicios[j]);
                    j++;
                }
                Console.WriteLine();
                i++;
            }

            Console.WriteLine("Presione una tecla para finalizar");
            Console.ReadLine();
        }


        //Cargar listas de las empresas que figuran en nuestro registro
        static int CargarLista(ListaEmpresas lista)
        {
            try
            {
        
                StreamReader F = new StreamReader("datos.txt");
                int n = Convert.ToInt32(F.ReadLine());
            
                /*Cambiamos los bucles while por for para solucionar problema 
                de retroalimentacion de la definicion de los contadores enteros*/

                for (int i=0 ; i < n ; i++)
                {
                    Empresa u = new Empresa();
                    string linea = F.ReadLine();
                    string[] trozos = linea.Split(',');
                    u.Codigo = Convert.ToInt32(trozos[0]);
                    u.NombreEmpresa = trozos[1];
                    u.RepresentanteLegal = trozos[2];
                    u.NumeroServicios = Convert.ToInt32(trozos[3]);

                    //Lectura de datos de los servicios de nuestras empresas que estan en la linea siguiente
                    string linea2 = F.ReadLine();
                    string[] trozos2 = linea2.Split(',');

                    for (int j = 0; j < u.NumeroServicios; j++)
                    {
                        u.ListaServicios[j] = Convert.ToInt32(trozos2[j]);
                    }
                    
                    lista.Empresas[i] = u;
                   
                }

                lista.num = n;
                F.Close();
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

                f.WriteLine("{0} {1} {2} {3} ", lista.Empresas[i].Codigo, lista.Empresas[i].NombreEmpresa, lista.Empresas[i].RepresentanteLegal,
                    lista.Empresas[i].NumeroServicios);

                int j = 0;
                while (j < lista.Empresas[i].NumeroServicios)
                {
                    f.Write("{0} ", lista.Empresas[i].ListaServicios[j]);
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
        static int AddEmpresas(ListaEmpresas lista, Empresa aux)
        {
            if (lista.num < 100)
            {
                lista.Empresas[lista.num] = aux;
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
        static int ModificarServicios(ListaEmpresas lista, Empresa aux)
        {
            if (lista.num < 100)
            {
                lista.Empresas[lista.num] = aux;
                lista.num += 1;

            }

            //Interfaz con las opciones para el usuario 
            Console.WriteLine("Ecoja la tarea que desea realizar:");
            Console.Write(
                            "\td Eliminar un servicio " +
                            "\ta Añadir Servicios \n");

            char inputUser = Convert.ToChar(Console.ReadLine());

             if(inputUser == 'd')
                {
                //Suprimir o eliminar un servicio 

                return 1;
                }

            else
            {
                if(inputUser == 'a'){

                    string linea2 = Console.ReadLine();
                    string[] trozos2 = linea2.Split(",");

                    
                    return 0;
                }

                else{
                    Console.WriteLine("Error");
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
                //Indica que el archo se ha leido correctamente
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Archivo leido correctamente.");
                Thread.Sleep(2000);
                //Limpia la consola para que el usuario pueda continuar con la siguiente instruccion
                Console.ResetColor();
                Console.Clear();

                //Opciones
                Console.WriteLine("Bienvenido al proyecto A1T121\n");

                Console.Write(
                                    "\ta - Mostrar Empresas por pantalla\n" +
                                    "\ts - Buscar Empresa por servicio\n" +
                                    "\td - Añadir o Eliminar Empresa \n" +
                                    "\te - Añadir o Eliminar Servicios 3\n " +
                                    "\tf - Guardar Datos" + 
                                    "\t0 - Salir\n");

                Console.Write("\n- Escoje una función:");
                switch (Console.ReadLine())
                {
                    //Leer archivo 
                    case "a":
                    case "A":
                        {
                            Console.Clear();
                            Console.Write("Empresas Registradas:\n\n");
                            LecturaEmpresas(miListaEmpresas);
                        }
                        break;

                    //Funcion 2
                    case "s":
                    case "S":
                        {

                        }
                        break;

                    //Funcion 3
                    case "d":
                    case "D":
                        {

                        }
                        break;

                    //Funcion 3
                    case "e":
                    case "E":
                        {

                        }
                        break;

                    //Funcion 3
                    case "f":
                    case "F":
                        {

                        }

                        break;

                    case "0":
                        {
                            //Salir
                        }
                        break;

                    //ERROR
                    default:
                        {
                            Console.Write(
                                            "Error, El parámetro introducido no es correcto\n " +
                                            "El programa se reiniciará automáticamente...");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Main();
                        }
                        break;

                }
       
            }
            Console.WriteLine("Cerrando...");
            Thread.Sleep(2000);
        }
    }
}