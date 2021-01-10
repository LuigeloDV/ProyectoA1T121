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
                    int max = u.NumeroServicios;

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

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nEmpresa #{0}\n", (i + 1));
                Console.ResetColor();

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
                                "\t-ID Servicio {0}: {1}\n " +
                                    "\t\t Descripcion: {2}\n" +
                                    "\t\t Fecha de ultima modificacion {3}/{4}/{5}\n" +
                                    "\t\t Precio: {6} Euros\n",

                                     j + 1, 
                                     Empresa.ListaServicios[j].CodigoServicio,
                                     Empresa.ListaServicios[j].DescriptionServicio,
                                     Empresa.ListaServicios[j].DayModifyServicio,
                                     Empresa.ListaServicios[j].MonthModifyServicio,
                                     Empresa.ListaServicios[j].YearModifyServicio,
                                     Empresa.ListaServicios[j].PrecioServicio);

                    j++;
                }
                Console.WriteLine();
                i++;
            }

        }

        //7.Buscar Empresa por servicio
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
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\nEmpresa #{0}\n", (i + 1));
                            Console.ResetColor();
                            Console.Write(
                                            "Codigo: {0}\n " +
                                            "Empresa: {1}\n " +
                                            "Representante Legal: {2}\n " +
                                            "Total Servicios: {3}\n\n",

                                                Empresa.Codigo,
                                                Empresa.NombreEmpresa,
                                                Empresa.RepresentanteLegal,
                                                Empresa.NumeroServicios);

                            //Datos de cada servicio
                            Console.Write(
                                        "\t-Código Servicio {0}: {1}\n " +
                                            "\t\t Descripcion: {2}\n" +
                                            "\t\t Fecha de ultima modificacion: {3}/{4}/{5}\n" +
                                            "\t\t Precio: {6} Euros\n",

                                                j+1,
                                                Empresa.ListaServicios[j].CodigoServicio,
                                                Empresa.ListaServicios[j].DescriptionServicio,
                                                Empresa.ListaServicios[j].DayModifyServicio,
                                                Empresa.ListaServicios[j].MonthModifyServicio,
                                                Empresa.ListaServicios[j].YearModifyServicio,
                                                Empresa.ListaServicios[j].PrecioServicio);

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

                f.WriteLine("{0},{1},{2},{3}", lista.Empresas[i].Codigo, lista.Empresas[i].NombreEmpresa, lista.Empresas[i].RepresentanteLegal,
                    lista.Empresas[i].NumeroServicios);

                int j = 0;
                while (j < lista.Empresas[i].NumeroServicios)
                {
                    f.Write("{0},{1},{2},{3},{4},{5},",
                    // Escribimos el codigo de los servicios sin saltar de linea
                    lista.Empresas[i].ListaServicios[j].CodigoServicio,
                    lista.Empresas[i].ListaServicios[j].DescriptionServicio,
                    lista.Empresas[i].ListaServicios[j].DayModifyServicio,
                    lista.Empresas[i].ListaServicios[j].MonthModifyServicio,
                    lista.Empresas[i].ListaServicios[j].YearModifyServicio,
                    lista.Empresas[i].ListaServicios[j].PrecioServicio);

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

        //Funcion para dar de alto un servicios 
        static int AddServicio(ListaEmpresas lista, Empresa aux)
        {
           
        }

        //Funcion para dar de baja un servicio
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


        //Funcion que muestra el menu con la interfaz de usuario
        static void Welcome()
        {
            //opciónes
            Console.WriteLine("Menú principal del proyecto A1T121\n");

            Console.Write(      
                
                                "\t1 - Lista Empresas (Mostrar Empresas por pantalla)\n" +
                                "\t2 - Consulta Servicios (Buscar Empresa imprimir servicios)\n" +
                                "\t3 - Añadir Empresa \n" +
                                "\t4 - Eliminar Empresa \n" +
                                "\t5 - Añadir Servicio \n " +
                                "\t6 - Eliminar Servicio \n " +
                                "\t7 - Buscar Empresa (Buscar Empresa por servicio)\n" +
                                "\tS - Guardar Datos \n\n" +

                                "\t0 - Salir\n");

            Console.WriteLine("\n- Escoja una función:");
        }



        //8.función ejecutable por defecto
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

                    bool exit = false;

                    while (!exit)
                    {
                        switch (Console.ReadLine())
                        {
                            //MOSTRAS EMPRESAS POR PANTALLA 
                            case "1":
                                {
                                    

                                    Console.WriteLine();
                                    LecturaEmpresas(miListaEmpresas);

                                }

                                break;

                            
                            case "2":
                                {
                                   

                                   
                                    
                                }
                                break;

                            //función 3
                            case "3":
                                {

                                }
                                break;

                            //ELIMINAR SERVICIOS DE UNA EMPRESA
                            case "4":
                                {

                                    try
                                    {
                                        //El usuario selecciona la empresa a editar
                                        Console.WriteLine("Introduzca el Código de empresa que desea eliminar:");
                                        int inputEmpresa = Convert.ToInt32(Console.ReadLine());

                                        BajaServicio(miListaEmpresas, inputEmpresa);
   
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Error en el formato de los datos introducidos.\n");
                                        Console.ResetColor();
                                        
                                    }
                                }
                                break;


                            //7---BUSCAR UNA EMPRESA POR NUMERO DE SERVICIO 
                            case "7":
                                {
                                    Console.WriteLine();
                                    BuscarEmpresa(miListaEmpresas);
                                }
                                break;
                            //8- 
                            case "8":
                                {


                         

                                }
                                break;


                            //S- Salvar Datos
                            case "s":
                            case "S":
                                {

                                    try
                                    {
                                        char inputSaveConfirmation;
                                        string inputFileName;
                                        bool inputFileNameOk = false;


                                        Console.WriteLine("Por favor seleccione una opcion:");
                                        Console.Write(
                                               "\t1 - Sobreescribir el documento\n" +
                                               "\t2 - Introducir el nombre del documento\n" +
                                               "\t3 - Guardar el documento con la fecha actual \n" +

                                               "\t0 - Salir\n");

                                        switch (Console.ReadLine())
                                        {
                                            //Sobreescribir el documento
                                            case "1":
                                                {
                                                    //Declaracion del nombre igual al del fichero que se carga
                                                    inputFileName = "datos.txt";

                                                    //Opcion de confirmacion para el usuario 
                                                    Console.WriteLine("Está seguro de que desea guardar las modificaciones hechas?");
                                                    Console.WriteLine("\ts - Si \tn - No");
                                                    inputSaveConfirmation = Convert.ToChar(Console.ReadLine());

                                                    //Funcion segun la introccion del usuario
                                                    if (inputSaveConfirmation == 's')
                                                    {
                                                        //Si se quiere guardar el fichero 
                                                        SalvarDatos(inputFileName, miListaEmpresas);
                                                    }
                                                    else
                                                    {
                                                        if (inputSaveConfirmation == 'n')
                                                        {
                                                            //Si no se quiere guardar el fichero
                                                            Console.WriteLine("\nNo se han guardado los datos");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("\nError: No se han guardado los datos");
                                                        }
                                                    }

                                                }
                                                break;

                                            //Guardar el documento con el nombre introducido por el usuario
                                            case "2":
                                                {
                                                    //Se entra en un bucle hasta que el usuario introduzca un nombre correcto 
                                                    while (!inputFileNameOk)
                                                    {
                                                        bool inputInvalidChars = false;
                                                        bool inputInvalidExtension = false;

                                                        //Instrucciones para el usuario
                                                        Console.WriteLine("Por favor introduzca un nombre para su documento ");
                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                        Console.WriteLine("\tEjemplo: nombre_archivo (No introduzca la extension del archivo)");
                                                        Console.ResetColor();
                                                        //Se recoje el valor del nombre introducido por el usuario y se añade la extensión
                                                        inputFileName = Console.ReadLine();

                                                        //Se limita el nombre a no contener caracteres invalidos con la funcion siguiente y el punto para evitar las extensiones
                                                        inputInvalidChars = inputFileName.Any(Path.GetInvalidFileNameChars().Contains);
                                                        inputInvalidExtension = inputFileName.Contains(".");

                                                        if (inputInvalidChars || inputInvalidExtension)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Red;
                                                            Console.WriteLine("\nNo se a podido guardar el documento.");
                                                            Console.ResetColor();
                                                            Console.WriteLine("\tError: El nombre del fichero contiene caracteres no validos.");
                                                        }
                                                        else
                                                        {
                                                            if (!inputInvalidChars && !inputInvalidExtension)
                                                            {
                                                                Console.WriteLine("Esta seguro de que desea guardar las modificaciones hechas?");
                                                                Console.WriteLine("\ts - Si \tn - No");
                                                                inputSaveConfirmation = Convert.ToChar(Console.ReadLine());
                                                                if (inputSaveConfirmation == 's')
                                                                {
                                                                    string inputFileNameFinal = inputFileName + ".txt";
                                                                    SalvarDatos(inputFileNameFinal, miListaEmpresas);
                                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                                    Console.WriteLine("\nEl fichero se ha guardado correctamente.");
                                                                    Console.ResetColor();
                                                                    //Salimos del bucle
                                                                    inputFileNameOk = true;
                                                                    SalvarDatos(inputFileName, miListaEmpresas);
                                                                }
                                                                else
                                                                {
                                                                    if (inputSaveConfirmation == 'n')
                                                                    {
                                                                        //Salimos del bucle
                                                                        inputFileNameOk = true;

                                                                    }
                                                                    else
                                                                    {
                                                                        //Salimos del bucle
                                                                        inputFileNameOk = true;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\nNo se a podido guardar el documento.");
                                                                Console.ResetColor();
                                                                Console.WriteLine("\tError: Ha habido un error inesperado mientras se intentaba guardar el documento");
                                                            }
                                                        }
                                                    }

                                                }
                                                break;

                                            //Guardar el documento con formato de fecha
                                            case "3":
                                                {

                                                }
                                                break;
                                            //Salir
                                            case "0":
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("\n¡Atención! Ha salido sin guardar el archivo");
                                                    Console.ResetColor();
                                                }
                                                break;

                                            default:
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Error la función seleccionada no existe.\n");
                                                    Console.ResetColor();

                                                }
                                                break;

                                        }
                                        break;


                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Error en el formato de los datos introducidos.\n");
                                        Console.ResetColor();
                                    }
                                }

                                break;
                            //0-SALIR 
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
                                                    exit = true;
                                                }
                                                break;

                                            case "n":
                                            case "N":
                                                {
                                                    exit = false;
                                                }
                                                break;

                                            default:
                                                {
                                                    exit = false;
                                                }
                                                break;

                                        }
                                        break;

                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Error en el formato de los datos introducidos.\n");
                                        Console.ResetColor();
                                    }

                                }
                                break;
                                

                            //ERROR
                            default:
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Error la función seleccionada no existe.\n");
                                    Console.ResetColor();
                                   
                                }
                                break;
                        }
                        //Volver al menu principal
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("\nPresione una tecla para volver al menú principal");
                        Console.ResetColor();
                        Console.ReadLine();
                        //Console.Clear():
                        Welcome();
                    }

                }
               
            }
        }
    }
}