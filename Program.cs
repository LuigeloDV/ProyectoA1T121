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

        //Lista de empresas
        class ListaEmpresas
        {
            public Empresa[] Empresas = new Empresa[100];
            public int num = 0;
        }

        //Definicion de los datos que identificaran a cada empresa
        public class Empresa
        {
            public int Codigo;
            public string NombreEmpresa;
            public string RepresentanteLegal;
            public int NumeroServicios;
            //Cambiar codigo
            public Servicio[] ListaServicios = new Servicio[6];

        }

        public class Servicio
        {
            public int CodigoServicio;
            public string DescriptionServicio;

            public int DayModifyServicio;
            public int MonthModifyServicio;
            public int YearModifyServicio;

            public int PrecioServicio;
        }
        

        /*      1- CargarLista
         *      2- LecturaEmpresas 
         *          -Mostrar por pantalla empresas  
         *      3- BuscarEmpresa
         *          -Buscar Empresa por servicio
         *      4- SalvarDatos
         *      5- AddEmpresas
         *      6- ModificarServicios
         *      7- Main
         */
        static void Welcome()
        {
            //opciónes
            Console.WriteLine("Bienvenido al proyecto A1T121\n");

            Console.Write(
                                "\ta - Mostrar Empresas por pantalla\n" +
                                "\ts - Buscar Empresa por servicio\n" +
                                "\td - Añadir o Eliminar Empresa \n" +
                                "\te - Añadir o Eliminar Servicios \n " +
                                "\tf - Guardar Datos \n\n" +
                                "\t0 - Salir\n");

            Console.WriteLine("\n- Escoja una función:");
        }


        //1.Cargar listas de las empresas que figuran en nuestro registro
        static int CargarLista(ListaEmpresas lista)
        {
            try
            {
                StreamReader F = new StreamReader("datos.txt");
                int n = Convert.ToInt32(F.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    Empresa u = new Empresa();

                    for(int aux=0; aux < 4; aux++)
                    {
                        u.ListaServicios[aux] = new Servicio();
                    }

                    string linea = F.ReadLine();
                    string[] trozos = linea.Split(',');

                    u.Codigo = Convert.ToInt32(trozos[0]);
                    u.NombreEmpresa = trozos[1];
                    u.RepresentanteLegal = trozos[2];
                    u.NumeroServicios = Convert.ToInt32(trozos[3]);

                    int max = u.NumeroServicios - 1;

                    string linea2 = F.ReadLine();
                    string[] trozos2 = linea2.Split(',');

                    for (int j = 0; j < max; j++)
                    {
                        //Lectura de datos de los servicios de nuestras empresas que estan en la linea siguiente
                        u.ListaServicios[j].CodigoServicio = Convert.ToInt32(trozos2[ 0 + (j*6)]);

                        //Lectura de datos de los servicios de nuestras empresas que estan en la linea siguiente
                        u.ListaServicios[j].DescriptionServicio = trozos2[1 + (j*6) ];
                        u.ListaServicios[j].DayModifyServicio = Convert.ToInt32(trozos2[2 + (j*6)]);
                        u.ListaServicios[j].MonthModifyServicio = Convert.ToInt32(trozos2[3 + (j * 6)]);
                        u.ListaServicios[j].YearModifyServicio = Convert.ToInt32(trozos2[4 + (j * 6)]);
                        u.ListaServicios[j].PrecioServicio = Convert.ToInt32(trozos2[5 + (j * 6)]);

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

        //2.función que muestra por pantalla las empresas que tenemos almacenadas en nuestro registro
        static void LecturaEmpresas(ListaEmpresas lista)
        {
            Console.WriteLine("\tTotal Empresas: {0}\n", (lista.num));
            int i = 0;
            while (i < lista.num)
            {
                Empresa Empresa = lista.Empresas[i];

                Console.WriteLine("Empresa #{0}\n", (i + 1));
                Console.Write(
                                "Codigo: {0}\n " +
                                "Empresa: {1}\n " +
                                "Representante Legal: {2}\n " +
                                "Total Servicios: {3}\n ",

                                 Empresa.Codigo,
                                 Empresa.NombreEmpresa,
                                 Empresa.RepresentanteLegal,
                                 Empresa.NumeroServicios);

                int j = 0;
                while (j < Empresa.NumeroServicios)
                {
                    Console.Write(
                                "\t-ID Servicio {0}:...................{1}\n ", j, Empresa.ListaServicios[j].CodigoServicio);
                    j++;
                }
                Console.WriteLine();
                i++;
            }

        }

        //3.Buscar Empresa por servicio
        static void BuscarEmpresa(ListaEmpresas lista)
        {
            Console.Write("Inserte el codigo del servicio:");

            try
            {
                int inputSearch = Convert.ToInt32(Console.ReadLine());

                bool notFound = true;
                int i = 0;
                while (i < lista.num)
                {

                    Empresa Empresa = lista.Empresas[i];

                    int j = 0;

                    while (j < Empresa.NumeroServicios)
                    {

                        if (inputSearch == Empresa.ListaServicios[j].CodigoServicio)
                        {
                            notFound = false;
                            Console.WriteLine("\tEmpresa #{0}\n", (i + 1));
                            Console.WriteLine(
                                            "Codigo: {0}\n " +
                                            "Empresa: {1}\n " +
                                            "Representante Legal: {2}\n " +
                                            "Total Servicios: {3}\n ",

                                                Empresa.Codigo,
                                                Empresa.NombreEmpresa,
                                                Empresa.RepresentanteLegal,
                                                Empresa.NumeroServicios);
                            int k = 0;
                            while (k < Empresa.NumeroServicios)
                            {
                                Console.Write(
                                            "\t-ID Servicio {0}:...................{1}\n ", k + 1, Empresa.ListaServicios[k]);
                                k++;
                            }
                        }

                        j++;
                    }
                    i++;
                }
                if (notFound)
                {
                    Console.WriteLine("\nNo hay ningún servicio que coincida con el que ha introducido.");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El Valor introducido no tiene el formato adecuado.");
                Console.ResetColor();
            }


        }

        //4.Escribimos nuevos datos para posteriormente salvarlos
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

        //5.Añade una empresa a la lista
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
                int i = 0;
                int[] idServicios = new int[numeroServicios];
                while (i <= numeroServicios)
                {
                    Console.WriteLine("Añada el id del servicio número {0}:", i + 1);
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

            }

            return 0;

        }

        //6.Dar de alta o eliminar un servicio 
        static int ModificarServicios(ListaEmpresas lista, Empresa aux)
        {
            if (lista.num < 100)
            {
                lista.Empresas[lista.num] = aux;
                lista.num += 1;
            }

            //Interfaz con las opciónes para el usuario 
            Console.WriteLine("Ecoja la tarea que desea realizar:");
            Console.Write(
                            "\td Eliminar un servicio " +
                            "\ta Añadir Servicios \n");

            char inputUser = Convert.ToChar(Console.ReadLine());

            if (inputUser == 'd')
            {
                //Suprimir o eliminar un servicio 

                return 1;
            }

            else
            {
                if (inputUser == 'a')
                {

                    string linea2 = Console.ReadLine();
                    string[] trozos2 = linea2.Split(",");


                    return 0;
                }

                else
                {
                    Console.WriteLine("Error");
                    return -1;
                }
            }
        }

        static void BajaServicio(ListaEmpresas lista, int inputEmpresa)
        {
            try
            {
                int i = 0;
                bool empresaFinded = false;

                //Match del codigo de empresa 
                while (i + 1 <= lista.num)
                {
                    if (lista.Empresas[i].Codigo == inputEmpresa)
                    {
                        empresaFinded = true;
                        break;
                    }
                    i++;
                }

                if (empresaFinded)
                {
                    //Filtrado de la busqueda (Maximo)
                    int l = lista.Empresas[i].NumeroServicios;
                    //El usuario introduce el codigo del servicio a eliminar
                    Console.WriteLine("Introduzca el Código del servicio a eliminar:");
                    int inputService = Convert.ToInt32(Console.ReadLine());
                    int j = 0;
                    bool encontrado = false;

                    while (j < l)
                    {
                        if (lista.Empresas[i].ListaServicios[j].CodigoServicio == inputService)
                            encontrado = true;
                        j++;
                    }


                    if (encontrado)
                    {

                        lista.Empresas[i].ListaServicios[j].CodigoServicio = 0;
                        lista.Empresas[i].NumeroServicios--;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nBaja de servicio realizada con exito");
                        Console.ResetColor();

                    }

                    else
                    {
                        while (!encontrado)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nEl Código del servicio introducido no existe");
                            Console.ResetColor();

                            Console.WriteLine("\nIntroduzca el Código del servicio a eliminar:");
                            inputService = Convert.ToInt32(Console.ReadLine());
                        }

                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El codigo de la empresa no coincide con ninguna de nuestra lista.");
                    Console.ResetColor();
                }


            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error en el formato de los datos introducidos");
                Console.ResetColor();
            }

        }


        //7.función ejecutable por defecto
        static void Main()
        {
            //Llamada a la función CargarLista
            ListaEmpresas miListaEmpresas = new ListaEmpresas();
            int res = CargarLista(miListaEmpresas);
            //Se ejecuta el programa segun la respuesta recibida por la función 
            if (res == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El archivo no se ha encontrado.");
                Console.ResetColor();
                Console.ReadKey();
            }
            else
            {
                if (res == -2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("El archivo no tiene un formato válido.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();

                    //Indica que el archivo se ha leido correctamente
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Archivo leido correctamente.");
                    Thread.Sleep(2000);

                    //Limpia la consola para que el usuario pueda continuar con la siguiente instruccion
                    Console.ResetColor();
                    Console.Clear();

                    //Llamada a la función que imprime por pantalla las opciónes para el usuario
                    Welcome();

                    while (true)
                    {
                        switch (Console.ReadLine())
                        {
                            //MOSTRAS EMPRESAS POR PANTALLA 
                            case "a":
                            case "A":
                                {
                                    

                                    Console.WriteLine();
                                    LecturaEmpresas(miListaEmpresas);
                                    //Se da al usuario la posibilidad de volver a seleccionar otra función 
                                    Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                    Console.ReadLine();
                                    //Console.Clear():
                                    Welcome();
                                               
                                }
                                continue;

                            //BUSCAR UNA EMPRESA POR NUMERO DE SERVICIO 
                            case "s":
                            case "S":
                                {
                                   

                                    Console.WriteLine();
                                    BuscarEmpresa(miListaEmpresas);
                                    //Se da al usuario la posibilidad de volver a seleccionar otra función 
                                    Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                    Console.ReadLine();
                                    //Console.Clear():
                                    Welcome();
                                    

                                }
                                continue;

                            //función 3
                            case "d":
                            case "D":
                                {

                                }
                                continue;

                            //AÑADIR O ELIMINAR SERVICIOS DE UNA EMPRESA
                            case "e":
                            case "E":
                                {

                                    
                                    try
                                    {
                                        //El usuario selecciona la empresa a editar
                                        Console.WriteLine("Introduzca el Código de empresa que desea editar a eliminar:");
                                        int inputEmpresa = Convert.ToInt32(Console.ReadLine());

                                        //El usuario elige la tarea que desea realizar
                                        Console.WriteLine("\nIndique la acción que desee realizar");
                                        Console.WriteLine("\t1- Editar un servicio \t2 -Eliminar un servicio");

                                        int inputSelected = Convert.ToInt32(Console.ReadLine());

                                        //Edicion de un servicio (Cambiar)
                                        if (inputSelected == 1)
                                        {
                                            //EditServicio(miListaEmpresas, inputEmpresa);
                                        }

                                        else
                                        {
                                            //Eliminar uno de los servicios
                                            if (inputSelected == 2)
                                            {
                                                BajaServicio(miListaEmpresas, inputEmpresa);

                                                Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                                Console.ReadLine();
                                                Welcome();

                                                
                                               
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Error, la opción seleccionada no existe.\n");
                                                Console.ResetColor();

                                                Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                                Console.ReadLine();
                                                //Console.Clear():
                                                Welcome();
                                               
                                            }

                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Error en el formato de los datos introducidos.\n");
                                        Console.ResetColor();

                                        Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                        Console.ReadLine();
                                        //Console.Clear():
                                        Welcome();
                                        
                                    }
                                }
                                continue;

                            //función 3
                            case "f":
                            case "F":
                                {

                                }

                                continue;

                            case "0":
                                {


                                    try
                                    {
                                        Console.WriteLine("Esta seguro que desea cerrar el programa?\n");
                                        Console.WriteLine("\ts- Si \tn- No \n");
                                        
                                        switch (Console.ReadLine())
                                        {
                                            case "s":
                                            case "S":
                                                {
                                                    
                                                }
                                                break;

                                            case "n":
                                            case "N":
                                                {
                                                    Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                                    Console.ReadLine();
                                                    //Console.Clear():
                                                    Welcome();
                                                    continue;
                                                }
                                                

                                            default:
                                                {
                                                    Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                                    Console.ReadLine();
                                                    //Console.Clear():
                                                    Welcome();
                                                    continue;
                                                }
                                                

                                        }
                                        break;

                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Error en el formato de los datos introducidos.\n");
                                        Console.ResetColor();
                                        continue;

                                    }

                                }
                                

                            //ERROR
                            default:
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Error la función seleccionada no existe.\n");
                                    Console.ResetColor();
                                    Console.WriteLine("\nPresione una tecla para volver al menú principal");
                                    Console.ReadLine();
                                    //Console.Clear():
                                    Welcome();
                                }
                                continue;
                        }
                        break;
                    }

                }
               
            }
        }
    }
}