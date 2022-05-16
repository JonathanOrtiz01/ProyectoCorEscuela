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
            ImprimirCursosEscuela(engine.Escuela);
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            diccionario.Add(10, "JuanK");
            diccionario.Add(23, "JonaOrtiz");

            foreach (var keyValPair in diccionario)
            {
                Console.WriteLine($"Key: {keyValPair.Key}, Valor: {keyValPair.Value}");
            }

            var dictmp = engine.GetDiccionarioObjetos();

            engine.ImprimirDiccionario(dictmp, true);
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