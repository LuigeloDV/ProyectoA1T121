﻿/*

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

        //CARGAR LISTAS
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

        //Funcionalidades

        //1.MOSTRAR POR PANTALLA
        static void LecturaEmpresas(ListaEmpresas lista)
        {   
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tTotal Empresas: {0}\n", (lista.num));
            Console.ResetColor();

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

        //2.CONSULTAR SERVICIOS EMPRESA
        static void BuscarEmpresa(ListaEmpresas lista)
        {
            try
            {
                Console.Write("Inserte el Código de la Empresa:");
                int inputSearch = Convert.ToInt32(Console.ReadLine());

                bool notFound = true;

                for (int i = 0; i < lista.num; i++)
                {

                    Empresa Empresa = lista.Empresas[i];

                    if (inputSearch == lista.Empresas[i].Codigo)
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
                        for (int j = 0; j < Empresa.NumeroServicios; j++)
                        {
                            //Datos de cada servicio
                            Console.Write(
                                        "\t-Código Servicio {0}: {1}\n " +
                                            "\t\t Descripcion: {2}\n" +
                                            "\t\t Fecha de ultima modificacion: {3}/{4}/{5}\n" +
                                            "\t\t Precio: {6} Euros\n",

                                                j + 1,
                                                Empresa.ListaServicios[j].CodigoServicio,
                                                Empresa.ListaServicios[j].DescriptionServicio,
                                                Empresa.ListaServicios[j].DayModifyServicio,
                                                Empresa.ListaServicios[j].MonthModifyServicio,
                                                Empresa.ListaServicios[j].YearModifyServicio,
                                                Empresa.ListaServicios[j].PrecioServicio);

                        }
                    }
                }
                if (notFound)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo hay ninguna empresa que coincida con la que ha introducido.");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError en el formato de los datos introducidos");
                Console.ResetColor();
            }

        }

        //3.DAR DE ALTA UNA EMPRESA
        static int AddEmpresas(ListaEmpresas lista, Empresa n)

        {
            if (lista.num < 100)
            {

                lista.Empresas[lista.num] = n;
                lista.num = lista.num + 1;
                return 0; //Todo OK
            }
            else
                return -1; //La lista esta llena
        }

        //4. DAR DE BAJA UNA EMPRESA
        static void DeleteEmpresas(ListaEmpresas lista, int aux)
        {
            int i = 0;
            try
            {
                bool encontrado = false;
                while ((i < lista.num) && (!encontrado))
                {
                    if (lista.Empresas[i].Codigo == aux)
                    {
                        if (i == lista.num - 1)
                        {
                            lista.num--;
                        }
                        else
                        {
                            while (i < lista.num)
                            {
                                lista.Empresas[i] = lista.Empresas[i + 1];
                                i++;
                            }
                            lista.num--;
                        }
                        encontrado = true;
                    }
                    i++;
                }

                if (encontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLa empresa ha sido eliminada");
                    Console.ResetColor();

                }
                else
                {
                    if (!encontrado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nEl codigo de la empresa no existe");
                        Console.ResetColor();
                    }
                       
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError en el formato de los datos introducidos");
                Console.ResetColor();
            }
        }

        //5.DAR DE ALTA UN SERVICIO
        static void AddServicio(ListaEmpresas lista)
        {
            try
            {
                //El usuario introduce el codigo de la empresa
                Console.WriteLine("Introduzca el Código de la Empresa: ");
                int inputEmpresa = Convert.ToInt32(Console.ReadLine());


                bool empresaFinded = false;

                int i = 0;
                int inputNewCodeService;
                string inputNewDescriptionService;
                int inputNewPriceService;

                int inputNowDay = (int)DateTime.Now.Day;
                int inputNowMonth = (int)DateTime.Now.Month;
                int inputNowYear = (int)DateTime.Now.Year;

                //Match del codigo de empresa 
                while( (i < lista.num) && (!empresaFinded))
                {
                    if (lista.Empresas[i].Codigo == inputEmpresa)
                    {
                        empresaFinded = true;
                    }
                    i++;
                }
                i--;
                //Numero de servicios de la empresa selecciona
                int a = lista.Empresas[i].NumeroServicios;

                //Instruciones para introducir los datos
                if (empresaFinded)
                {
                    Console.WriteLine("Introduzca un Código de Servicio:");
                    inputNewCodeService = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Introduzca una Descripción:");
                    inputNewDescriptionService = Console.ReadLine();

                    Console.WriteLine("Introduzca el Precio al mes del Servicio:");
                    inputNewPriceService = Convert.ToInt32(Console.ReadLine());

                  
                    // Escribimos el codigo de los servicios sin saltar de linea
                    lista.Empresas[i].ListaServicios[a].CodigoServicio = inputNewCodeService;
                    lista.Empresas[i].ListaServicios[a].DescriptionServicio = inputNewDescriptionService;
                    lista.Empresas[i].ListaServicios[a].DayModifyServicio = inputNowDay;
                    lista.Empresas[i].ListaServicios[a].MonthModifyServicio = inputNowMonth;
                    lista.Empresas[i].ListaServicios[a].YearModifyServicio = inputNowYear;
                    lista.Empresas[i].ListaServicios[a].PrecioServicio = inputNewPriceService;

                    //Sumar un servicio
                    lista.Empresas[i].NumeroServicios++;

                    //Mensaje confirmatorio
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLa empresa se ha añadido correctamente.");
                    Console.ResetColor();
                }
                else
                {
                    if (!empresaFinded)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nLa Empresa con el codigo introducido no exite.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("\nError inesperado.");
                    }
                }
            }

            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError en el formato de los datos introducidos");
                Console.ResetColor();
            }
        }
        //6.DAR DE BAJA UN SERVICIO
        static void BajaServicio(ListaEmpresas lista)
        {
            try
            {
               
                int j = 0;
                bool serviceFinded = false;
                bool deletionRealized = false;

                //El usuario introduce el codigo del servicio a eliminar
                Console.WriteLine("Introduzca el Código del servicio a eliminar:");
                int inputService = Convert.ToInt32(Console.ReadLine());

                //Match del codigo de empresa 
                for (int i=0; (i < lista.num) && (!serviceFinded); i++)
                {  
                    while(j < lista.Empresas[i].NumeroServicios)
                    {
                        if (lista.Empresas[i].ListaServicios[j].CodigoServicio == inputService)
                        {
                            serviceFinded = true;
                            lista.Empresas[i].ListaServicios[j].CodigoServicio = 0;
                            lista.Empresas[i].NumeroServicios--;
                            deletionRealized = true;
                    }

                        j++;
                    }  
                }

               
                if (deletionRealized)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nBaja de servicio realizada con exito");
                    Console.ResetColor();
                    //Salida del bucle
                            
                    }

                else
                {
                    if(!deletionRealized)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nEl Código del servicio introducido no existe");
                        Console.ResetColor();

                    }

                }
               
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error en el formato de los datos introducidos");
                Console.ResetColor();
            }

        }

        //7.BUSCAR EMPRESA POR SERVICIO
        static void BuscarEmpresaServicio(ListaEmpresas lista)
        {
  
            try
            {
                Console.Write("Inserte el codigo del servicio:");
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

                                                j + 1,
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nNo hay ningún servicio que coincida con el que ha introducido.");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El Valor introducido no tiene el formato adecuado.");
                Console.ResetColor();
            }


        }

        //8.FIJAR FECHA
        static void FijarFecha(ListaEmpresas lista)
        {
            try
            {
                Console.WriteLine("Escribe el codigo de la empresa:");
                int codigoEmpresa = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Escribe el codigo del servicio:");
                int codigoServicio = Convert.ToInt32(Console.ReadLine());
                bool encontrado = false;
                int j = 0;
                int i = 0;

                while ((i <= lista.num) && (encontrado == false)) //Debemos encontrar la empresa
                {
                    if (lista.Empresas[i].Codigo == codigoEmpresa)
                        encontrado = true;
                    i++;
                }
                i--;
                encontrado = false;
                while (!encontrado && (j < lista.Empresas[i].NumeroServicios)) //Debemos encontrar el servicio
                {
                    if (codigoServicio == lista.Empresas[i].ListaServicios[j].CodigoServicio)
                        encontrado = true;
                    j++;
                }
                j--;

                //Actualizamos la última fecha 
                Console.WriteLine("Introduce la fecha actual (dd mm aaaa):");
                string linea = Console.ReadLine();
                string[] trozos = linea.Split(' ');

                if ((trozos[0] != " ") && (trozos[0].Length < 2) )
                {
                    lista.Empresas[i].ListaServicios[j].DayModifyServicio = Convert.ToInt32(trozos[0]);

                    if ((trozos[1] != " ") && (trozos[1].Length < 2))
                    {

                        lista.Empresas[i].ListaServicios[j].MonthModifyServicio = Convert.ToInt32(trozos[1]);

                        if ((trozos[2] != " ") && (trozos[2].Length < 4))
                        {
                            lista.Empresas[i].ListaServicios[j].YearModifyServicio = Convert.ToInt32(trozos[2]);
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLa fecha ha sido actualizada");
                    Console.ResetColor();
                }
                else
                {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nNo se ha definido la fecha correctamente");
                        Console.ResetColor();
                    
                }
               

               
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError en el formato de los datos introducidos");
                Console.ResetColor();
            }
        }

        //9.BUSCAR SERVICIO MAS COSTOS
        static void BuscarCoste(ListaEmpresas lista)
        {
            int caro = 0;

            for (int i=0; i < lista.num; i++)
            {
                for (int j = 0; j < lista.Empresas[i].NumeroServicios; j++)
                {
                    if (lista.Empresas[i].ListaServicios[j].PrecioServicio > caro)
                        caro = lista.Empresas[i].ListaServicios[j].PrecioServicio;
                    j++;
                }

            }



           for (int i = 0; i < lista.num; i++)
            {
                for (int j = 0; j < lista.Empresas[i].NumeroServicios; j++)
                {
                    
                    if (lista.Empresas[i].ListaServicios[j].PrecioServicio == caro)
                    {
                        Console.Write("\nEl servicio con el precio mas alto es el de codigo: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(lista.Empresas[i].ListaServicios[j].CodigoServicio);
                        Console.ResetColor();
                        Console.WriteLine("\n\tEl precio es de: {0} euros al mes", lista.Empresas[i].ListaServicios[j].PrecioServicio);
                    }
                  
                }
            }
        }

        //-S SALVAR LOS DATOS
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

        //MENU
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
                                "\t8 - Fijar Fecha\n" +
                                "\t9 - Buscar el servicio con el precio mas alto\n" +
                                "\tS - Guardar Datos \n\n" +

                                "\t0 - Salir\n");

            Console.WriteLine("\n- Escoja una función:");
        }
    
        //FUNCION EJECUTABLE POR DEFECTO
        static void Main()
        {
            //Borramos cualquier formato de consola previo guardado en cache
            Console.ResetColor();
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
                                    LecturaEmpresas(miListaEmpresas);
                                }

                                break;

                            //MOSTRAS EMPRESAS POR PANTALLA 
                            case "2":
                                {
                                    BuscarEmpresa(miListaEmpresas);
                                }
                                break;

                            //AÑADIR EMPRESA
                            case "3":
                                {
                                    try
                                    {
                                        Console.WriteLine("Escribe los datos de la empresa, siguiendo el patron de abajo");
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("\tCódigo,Nombre de la Empresa,Nombre del Representante,Número de servicios\n");
                                        Console.ResetColor();
                                        string linea = Console.ReadLine();
                                        string[] trozos = linea.Split(',');
                                        Empresa nuevo = new Empresa();
                                        int n = 0;
                                        while (n < 4)
                                        {
                                            nuevo.ListaServicios[n] = new Servicio();
                                            n++;
                                        }

                                        if (trozos.Length > 3)
                                        {
                                            if (trozos[0] != " ")
                                            {
                                                nuevo.Codigo = Convert.ToInt32(trozos[0]);

                                                if (trozos[1] != " ")
                                                {
                                                    nuevo.NombreEmpresa = trozos[1];
                                                    if (trozos[2] != " ")
                                                    {
                                                        nuevo.RepresentanteLegal = trozos[2];
                                                        if (trozos[3] != " ")
                                                        {
                                                            nuevo.NumeroServicios = Convert.ToInt32(trozos[3]);

                                                            //Instrucciones segun numero de servicios
                                                            if(nuevo.NumeroServicios >0 )
                                                            {
                                                                Console.WriteLine("Escribe los datos de los servicios a añadir, siguiendo el patron de abajo");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Escribe los datos del servicio a añadir, siguiendo el patron de abajo");
                                                            }
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("\t Código 1,Descripción 1,Dia 1,Mes 1,Año 1,Precio 1,Código 2,Descripción 2,Dia 2,Mes 2,Año 2,Precio 2,...");
                                                            Console.ResetColor();

                                                            string linea2 = Console.ReadLine();
                                                            string[] trozos2 = linea2.Split(',');
                                                            int j = 0;
                                                            bool errorDatosServicios = false;
                                                            while (j < nuevo.NumeroServicios && !errorDatosServicios)
                                                            {
                                                                //Comprobar que existan todos los datos de los servicios
                                                                if (trozos2.Length >= (6*nuevo.NumeroServicios))
                                                                {
                                                                    nuevo.ListaServicios[j].CodigoServicio = Convert.ToInt32(trozos2[0 + (j * 6)]);
                                                                    nuevo.ListaServicios[j].DescriptionServicio = trozos2[1 + (j * 6)];
                                                                    nuevo.ListaServicios[j].DayModifyServicio = Convert.ToInt32(trozos2[2 + (j * 6)]);
                                                                    nuevo.ListaServicios[j].MonthModifyServicio = Convert.ToInt32(trozos2[3 + (j * 6)]);
                                                                    nuevo.ListaServicios[j].YearModifyServicio = Convert.ToInt32(trozos2[4 + (j * 6)]);
                                                                    nuevo.ListaServicios[j].PrecioServicio = Convert.ToInt32(trozos2[3 + (j * 6)]);
                                                                    j++;

                                                                    AddEmpresas(miListaEmpresas, nuevo);
                                                                }
                                                                else 
                                                                {
                                                                    if (trozos2.Length < (6 * nuevo.NumeroServicios))
                                                                        errorDatosServicios = true;
                                                                }
                                                            }
                                                            if (!errorDatosServicios)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                Console.WriteLine("La Empresa y sus respectivos servicios se han añadido correctamente");
                                                                Console.ResetColor();
                                                            }
                                                            else
                                                            {
                                                                if (errorDatosServicios) 
                                                                { 
                                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                                    Console.WriteLine("Error: Faltaron datos de los servicios por introducir.");
                                                                    Console.ResetColor();
                                                                }
                                                            }
                                                           
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Error: Faltaron datos de la empresa por introducir.");
                                            Console.ResetColor();
                                        }

                                        
                                    }
                                    catch (FormatException)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("El Valor introducido no tiene el formato adecuado.");
                                        Console.ResetColor();
                                    }
                                }
                                break;


                            //ELIMINAR EMPRESA 
                            case "4":
                                {
                                        int aux;
                                        Console.WriteLine("Introduce el Codigo para eliminar la empresa:");
                                        aux = Convert.ToInt32(Console.ReadLine());
                                        DeleteEmpresas(miListaEmpresas, aux);

                                }
                                    break;

                            //ELIMINAR SERVICIOS DE UNA EMPRESA
                            case "5":
                                {
                                    AddServicio(miListaEmpresas);
                                }
                                break;

                            //ELIMINAR SERVICIOS DE UNA EMPRESA
                            case "6":
                                {
                                    BajaServicio(miListaEmpresas);
                                }
                                break;


                            //BUSCAR UNA EMPRESA POR NUMERO DE SERVICIO 
                            case "7":
                                {
                                    BuscarEmpresaServicio(miListaEmpresas);
                                }
                                break;
                            //SERVICIO DE MAYOR COSTE
                            case "8":
                                {
                                    FijarFecha(miListaEmpresas);
                                }
                                break;

                            //SERVICIO DE MAYOR COSTE
                            case "9":
                                {
                                    BuscarCoste(miListaEmpresas);
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
                                                        Console.WriteLine("\nPor favor introduzca un nombre para su documento ");
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
                                                            Console.WriteLine("\n\tError: El nombre del fichero contiene caracteres no validos.");
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