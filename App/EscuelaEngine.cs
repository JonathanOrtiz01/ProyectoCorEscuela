using System.Linq;

using CorEscuela.Entidades;
using CorEscuela.Util;

namespace CorEscuela
{
    public class EscuelaEngine
    {
        public Escuela Escuela {get; set;}

        public EscuelaEngine()
        {
            
        }

        public void Inicializar()
        {
            Escuela = new Escuela("Benito Juárez", 2000, TiposEscuela.Primaria,
            ciudad: "Oxkutzab", pais: "México");

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
        }
        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprimirEval = false)
        {
            foreach (var objdic in dic)
            {
                Printer.EscribirTitulo(objdic.Key.ToString());
                Console.WriteLine(objdic);

                foreach (var val in objdic.Value)
                {
                    switch (objdic.Key)
                    {
                        case LlaveDiccionario.Evaluacion:
                            if(imprimirEval) 
                                Console.WriteLine(val); 
                        break;

                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                        break;

                        case LlaveDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                        break;

                        case LlaveDiccionario.Curso:
                        var curtmp = val as Curso;
                        if(curtmp != null)
                        {
                            int count = curtmp.Alumnos.Count;
                            Console.WriteLine("Curso: " + val.Nombre + "Cantidad de alumnos: " + count);
                        }
                        break;

                        default:
                            Console.WriteLine(val);
                        break;

                    }
                }
            }
        }
        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {   
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] {Escuela});
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listatmpEvaluaciones = new List<Evaluacion>();
            var listatmpAsignaturas = new List<Asignatura>();
            var listatmpAlumnos = new List<Alumno>();
            foreach (var cur in Escuela.Cursos)
            {
                listatmpAsignaturas.AddRange(cur.Asignaturas);
                listatmpAlumnos.AddRange(cur.Alumnos);
                foreach (var alum in cur.Alumnos)
                {
                    listatmpEvaluaciones.AddRange(alum.Evaluaciones);
                }               
                
            }

            diccionario.Add(LlaveDiccionario.Asignatura, listatmpAsignaturas.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Alumno, listatmpAlumnos.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Evaluacion, listatmpEvaluaciones.Cast<ObjetoEscuelaBase>());
            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true          
        )
        {
            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy); 
        }

         public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true          
        )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy); 
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoCursos,
            out int conteoEvaluaciones,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true          
        )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy); 
        }

         public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoAsignaturas,
            out int conteoCursos,
            out int conteoEvaluaciones,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true          
        )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy); 
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true   
        )
        {
            conteoEvaluaciones = conteoAlumnos = conteoAsignaturas = 0;
            
            var listaObj = new List<ObjetoEscuelaBase>();
                listaObj.Add(Escuela);

                if(traerCursos)
                listaObj.AddRange(Escuela.Cursos);

                conteoCursos = Escuela.Cursos.Count;
                foreach (var curso in Escuela.Cursos)
                {
                    conteoAsignaturas += curso.Asignaturas.Count;
                    conteoAlumnos += curso.Alumnos.Count;
                    if(traerAsignaturas)
                    listaObj.AddRange(curso.Asignaturas); 

                    if(traerAlumnos)
                    listaObj.AddRange(curso.Alumnos);     
                    
                    if(traerEvaluaciones)
                    {
                        foreach (var alumno in curso.Alumnos)
                        {

                            listaObj.AddRange(alumno.Evaluaciones); 
                            conteoEvaluaciones += alumno.Evaluaciones.Count;   
                        }
                    }     
                }
            return listaObj.AsReadOnly();
        }

        #region Métodos de carga

        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round((5 * (float)rnd.NextDouble()), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>()
                {
                    new Asignatura {Nombre = "Matemáticas"},
                    new Asignatura {Nombre = "Educación Física"},
                    new Asignatura {Nombre = "Historia"},
                    new Asignatura {Nombre = "Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAleatorios(int cantidad )
        {
            string[] nombre1 = {"Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás"};
            string[] apellido1 = {"Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera"};
            string[] nombre2 = {"Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro"};

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno {Nombre = $"{n1} {n2} {a1}"};

            return listaAlumnos.OrderBy((a1) => a1.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>()
            {
                new Curso() {Nombre = "101", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "301", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde}
            };
            
            Random rnd = new Random();
            
            foreach (var curso in Escuela.Cursos)
            {
                int cantidadRandom = rnd.Next(5, 20);
                curso.Alumnos = GenerarAlumnosAleatorios(cantidadRandom);
            }
        }
    }
    #endregion
}