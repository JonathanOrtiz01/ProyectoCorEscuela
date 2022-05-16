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

        public IEnumerable<Escuela> GetListaEvaluaciones()
        {
            IEnumerable<Escuela> rta;
            if(_diccionario.TryGetValue(LlaveDiccionario.Escuela, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                rta = lista.Cast<Escuela>();
            }
            {
                rta = null;
            }
            return rta;
        }
    }
}