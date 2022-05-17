// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using CorEscuela.App;
using CorEscuela.Entidades;
using CorEscuela.Util;
using static System.Console;

namespace CorEscuela
{
    class Program
    {
        static void Main (string[] args)
        {   
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(100, 100, 1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.EscribirTitulo("BIENVENIDOS AL ITSSY");
            
            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsig = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvalXAsig();
            var listaPromXAsig = reporteador.GetPromedioAlumnoXAsignatura();
            
            /*listaPromXAsig = reporteador.GetPromedioAlumnoXAsignatura();
            Printer.EscribirTitulo("CUADRO DE HONOR");
            foreach (var asig in listaPromXAsig)
            {
                Printer.EscribirTitulo($"   {asig.Key}");
                var itm = 1;
                foreach (var Prom in asig.Value)
                {
                    switch (itm)
                    {
                        case 1:
                            WriteLine($"Primer Lugar {Prom} Felicitaciones!");
                            break;
                        case 2:
                            WriteLine($"Segundo Lugar {Prom} Felicitaciones!");
                            break;
                        case 3:
                            WriteLine($"Tercer Lugar {Prom} Felicitaciones!");
                            break;
                        default:
                            WriteLine(Prom);
                            break;
                    }
                    itm++;
                }
             
            } */

            Printer.EscribirTitulo("Captura de una evaluación por consola");
            var newEval = new Evaluacion();
            string nombre, notastring;
            float nota;

            WriteLine("Ingrese el nombre de la evaluación");
            Printer.PresionarEnter();
            nombre = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nombre))
            {
                Printer.EscribirTitulo("El valor del nombre no puede ser vacío");
                WriteLine("Saliendo del programa...");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();

                WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PresionarEnter();
            notastring = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(notastring))
            {
                Printer.EscribirTitulo("El valor de la nota no puede ser vacío");
                WriteLine("Saliendo del programa...");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notastring);
                    if(newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluación ha sido ingresada correctamente");
                }

                catch(ArgumentOutOfRangeException arge)
                {
                    Printer.EscribirTitulo(arge.Message);
                    WriteLine("Saliendo del programa...");
                }
                catch(Exception)
                {    
                    Printer.EscribirTitulo("El valor de la nota no es válido");
                    WriteLine("Saliendo del programa...");
                }
                
                
            }


        }

        private static void AccionDelEvento(object? sender, EventArgs e)
        {
            Printer.EscribirTitulo("SALIENDO...");
            Printer.Beep(3000, 1000, 3);
        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.EscribirTitulo("Cursos de la escuela");

            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre}, Id {curso.UniqueId}"); 
                }
            }
        }
    }
}