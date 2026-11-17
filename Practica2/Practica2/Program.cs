namespace Practica2
{
    internal class Program
    {
        static Random rnd = new Random(); // generador de aleatorios para colocar los obstáculos verticales
        const bool DEBUG = true; // para sacar información de depuración en el Renderizado
        const int ANCHO = 22, ALTO = 14, // tamaño del área de juego
        SEP_OBS = 6, // separación horizontal entre obstáculos
        HUECO = 7, // hueco de los obstáculos (en vertical)
        COL_BIRD = ANCHO / 3, // columna fija del pájaro
        IMPULSO = 3, // unidades de ascenso por aleteo
        DELTA = 300; // retardo entre frames (ms)
        static void Main()
        { // programa principal
            int[] suelo, techo;
            int fil, ascenso, frame, puntos;
            bool colision;
            
            Inicializa(out suelo, out techo, out fil, out ascenso, out frame, out puntos, out colision);
            RenderTecho(techo);
            RenderSuelo(suelo);
            
        }

        static void Inicializa(out int[] suelo, out int[] techo, out int fil, out int ascenso, out int frame, out int puntos, out bool colision)
        {
            fil = ALTO/2;
            ascenso = -1;
            frame = 0;
            puntos = 0;
            colision = false;

            suelo = new int[ANCHO];
            techo = new int[ANCHO];
            for (int i = 0; i<suelo.Length; i++)
            {
                suelo[i] = 0;
                techo [i] = ALTO-1;
            }
        }

        void Render(int[] suelo, int[] techo, int fil, int frame, int puntos, bool colision)
        {
            RenderTecho(techo);
            RenderSuelo(suelo);
        }
         static void RenderTecho(int[] techo)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < techo.Length; i++)
            {
                for (int j = 0; j <= (ALTO - 1 -techo[i]); j++)
                {
                    Console.SetCursorPosition((2 * i), (ALTO - 1 -techo[i] -j));
                    Console.Write("a");
                    Console.SetCursorPosition(2 * i + 1, (ALTO -1 -techo[i] -j));
                    Console.Write("a");
                }
            }
            Console.ResetColor();
        }
        static void RenderSuelo(int[] suelo)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < suelo.Length; i++)
            {
                for (int j = 0;  j <= (suelo[i]); j++)
                {
                    Console.SetCursorPosition((2*i), (ALTO -1 -suelo[i]) +j);
                    Console.Write("a");
                    Console.SetCursorPosition(2*i+1, (ALTO -1 -suelo[i]) +j);
                    Console.Write("a");
                }
            }
            Console.ResetColor ();

        }

    }
}
