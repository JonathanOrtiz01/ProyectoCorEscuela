// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using CorEscuela.Entidades;
using CorEscuela.Util;
using static System.Console;

namespace CorEscuela
{
    class Program
    {
        static void Main (string[] args)
        {      
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.EscribirTitulo("BIENVENIDOS AL ITSSY");
            Printer.Beep();
            ImprimirCursosEscuela(engine.Escuela);
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