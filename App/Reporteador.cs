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
            var listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                   where ev.Nota >= 3.0f
                   select ev.Asignatura.Nombre).Distinct(); ;
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvalXAsig()
        {
            var dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            return dicRta;
        }       
    }
}