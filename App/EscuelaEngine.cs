using System.Linq;

using CorEscuela.Entidades;

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

        public List<ObjetoEscuelaBase> GetObjetosEscuela(
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
            return listaObj;
        }

        #region Métodos de carga

        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
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