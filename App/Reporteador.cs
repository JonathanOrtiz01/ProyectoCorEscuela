using System.Collections.Generic;
using System.Linq;
using CorEscuela.Entidades;

namespace CorEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if(dicObsEsc == null)
            throw new ArgumentNullException(nameof(dicObsEsc));
            _diccionario = dicObsEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            if(_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            {
                return new List<Evaluacion>();
            }   
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }
        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                   where ev.Nota >= 3.0f
                   select ev.Asignatura.Nombre).Distinct(); ;
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvalXAsig()
        {
            var dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;
                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }       

        public Dictionary<string, IEnumerable<object>> GetPromedioAlumnoXAsignatura(int top = 3)
        {
            var rta = new Dictionary<string, IEnumerable<object>>();

            var dicEvalXAsig = GetDicEvalXAsig();

            foreach (var asigconEval in dicEvalXAsig)
            {
                var promedioAlumnos = from eval in asigconEval.Value
                            group eval by new 
                            {
                                eval.Alumno.UniqueId,
                                eval.Alumno.Nombre
                            }
                            into groupEvalAlumno
                            select new AlumnoPromedio
                            {
                               alumnoid = groupEvalAlumno.Key.UniqueId,
                               alumnoNombre = groupEvalAlumno.Key.Nombre,
                               promedio = groupEvalAlumno.Average(evaluacion => evaluacion.Nota)
                            };

                rta.Add(asigconEval.Key, promedioAlumnos.OrderByDescending((lp) => lp.promedio).Take(top));
            }   

            return rta;      
        }
    }
}