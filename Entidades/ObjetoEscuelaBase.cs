using System;

namespace CorEscuela.Entidades
{
    public class ObjetoEscuelaBase
    {
        public string UniqueId {get; private set;} 
        public string Nombre {get; set;}
    
        public ObjetoEscuelaBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }
    }


}