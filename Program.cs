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
            
            listaPromXAsig = reporteador.GetPromedioAlumnoXAsignatura();
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