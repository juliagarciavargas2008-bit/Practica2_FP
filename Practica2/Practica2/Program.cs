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
            RenderPajaro(fil, COL_BIRD, colision);
            RenderTechoSuelo(techo, suelo);
            
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
            RenderPajaro(fil, COL_BIRD, colision);
            RenderTechoSuelo(techo, suelo);
        }
        static void RenderTechoSuelo(int[] techo, int[] suelo)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < techo.Length; i++)          //Mira el componente de los Arrays, son iguales asi que da igual quien este en la condición
            {
                for (int j = 0; j <= (ALTO - 1 -techo[i]); j++) //En función de cuanto valga el componente repita la acción tantos bloques como haya.
                {
                    Console.SetCursorPosition((2 * i), (ALTO - 1 -techo[i] -j)); //Pinta el primero y luego el segundo, en la posición 13-X-j; para que suba -j
                    Console.Write("a");
                    Console.SetCursorPosition(2 * i + 1, (ALTO -1 -techo[i] -j)); //Pinta el segundo bloque, la lógica es la misma que en computadores
                    Console.Write("a");
                }
                
                for (int j = 0; j <= (suelo[i]); j++)       //Mira el componente del suelo y repite el for tantas veces indique.
                {
                    Console.SetCursorPosition((2 * i), (ALTO - 1 - suelo[i]) + j);  //Posicion desde 13-x+j; para que pinte hacia abajo
                    Console.Write("a");
                    Console.SetCursorPosition(2 * i + 1, (ALTO - 1 - suelo[i]) + j); //Segundo bloque
                    Console.Write("a");
                }
            }
            Console.ResetColor();
        }
        static void RenderPajaro(int fil, int COL_BIRD, bool colision)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(COL_BIRD, fil);
            if (!colision) Console.Write("->");
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("**");
            }
            Console.ResetColor();
        }
    }
}
