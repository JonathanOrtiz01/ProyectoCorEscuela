namespace CorEscuela.Util
{
    public static class Printer
    {
        public static void DibujarLinea(int size = 20)
        {
            Console.WriteLine("".PadLeft(size, '='));
        }

        public static void PresionarEnter()
        {
            Console.WriteLine("Presione ENTER para continuar");
        }

        public static void EscribirTitulo(string titulo)
        {
            DibujarLinea();
            Console.WriteLine(titulo);
            DibujarLinea();
        }

        public static void Beep(int hz = 2000, int tiempo = 500, int cantidad = 1)
        {
            while (cantidad-- > 0)
            {
                Console.Beep(hz, tiempo);
            }
        }
    }


}