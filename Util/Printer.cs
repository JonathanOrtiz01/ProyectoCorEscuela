namespace CorEscuela.Util
{
    public static class Printer
    {
        public static void LineaDivisora(int size = 20)
        {
            Console.WriteLine("".PadLeft(size, '='));
        }

        public static void EscribirTitulo(string titulo)
        {
            LineaDivisora();
            Console.WriteLine(titulo);
            LineaDivisora();
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