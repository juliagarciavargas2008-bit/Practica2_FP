//Julia García Vargas
//Jerico Marl Valdez Duclayan


using System.Runtime.Versioning;

namespace Practica2
{
    internal class Program
    {
        static Random rnd = new Random(); // generador de aleatorios para colocar los obstáculos verticales
        const bool DEBUG = false; // para sacar información de depuración en el Renderizado
        const int ANCHO = 22, ALTO = 14, // tamaño del área de juego
        SEP_OBS = 6, // separación horizontal entre obstáculos
        HUECO = 7, // hueco de los obstáculos (en vertical)
        COL_BIRD = ANCHO / 3, // columna fija del pájaro
        IMPULSO = 3, // unidades de ascenso por aleteo
        DELTA = 300; // retardo entre frames (ms)
        static void Main()          // programa principal
        { 
            int[] suelo, techo;
            int fil, ascenso, frame, puntos;
            bool colision;
            bool salir = false, pausa = false, debug = true;
            char ch;

            Console.CursorVisible = false;
            Inicializa(out suelo, out techo, out fil, out ascenso, out frame, out puntos, out colision);

            while ((!salir)&&(!colision))
            {
                ch = LeeInput();
                ProcesaInput(ch, ref salir, ref pausa);
                if (!pausa)
                {
                    Borrado(suelo, techo, fil);
                    Avanza(suelo, techo, frame);
                    Mueve(ch, ref fil, ref ascenso);
                    colision = Colision(suelo, techo, fil);
                    if (!colision) Puntua(suelo, techo, ref puntos);
                    Render(suelo, techo, fil, frame, puntos, colision);
                    frame++;
                }  
                
                if (debug) Debug(suelo, techo, fil, frame); ;
                
                System.Threading.Thread.Sleep(DELTA);
               
            }
        }

        static void Inicializa(out int[] suelo, out int[] techo, out int fil, out int ascenso, out int frame, out int puntos, out bool colision)
        {
            fil = ALTO / 2;
            ascenso = -1;
            frame = 0;
            puntos = 0;
            colision = false;

            suelo = new int[ANCHO];
            techo = new int[ANCHO];
            for (int i = 0; i < suelo.Length; i++)
            {
                suelo[i] = 0;
                techo[i] = ALTO - 1;
            }
        }
        static void Render(int[] suelo, int[] techo, int fil, int frame, int puntos, bool colision)
        {
            RenderSueloTecho(suelo, techo);
            RenderPajaro(fil, colision);
            RenderPuntos(puntos);
        }
        #region Render auxiliares
        static void RenderSueloTecho(int[] suelo, int[] techo)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i < techo.Length; i++)          //Mira el componente de los Arrays, son iguales asi que da igual quien este en la condición
            {
                for (int j = 0; j <= (ALTO - 1 - techo[i]); j++) //En función de cuanto valga el componente repita la acción tantos bloques como haya.
                {
                    Console.SetCursorPosition((2 * i), (ALTO - 1 - techo[i]) - j); //Pinta el primero y luego el segundo, en la posición 13-X-j; para que suba -j
                    Console.Write(" ");
                    Console.SetCursorPosition(2 * i + 1, (ALTO - 1 - techo[i]) - j); //Pinta el segundo bloque, la lógica es la misma que en computadores
                    Console.Write(" ");
                }

                for (int j = 0; j <= (suelo[i]); j++)       //Mira el componente del suelo y repite el for tantas veces indique.
                {
                    Console.SetCursorPosition((2 * i), (ALTO - 1 - suelo[i]) + j);  //Posicion desde 13-x+j; para que pinte hacia abajo
                    Console.Write(" ");
                    Console.SetCursorPosition(2 * i + 1, (ALTO - 1 - suelo[i]) + j); //Segundo bloque
                    Console.Write(" ");
                }
            }
            Console.ResetColor();
        }
        static void RenderPajaro(int fil, bool colision)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(2 * COL_BIRD, fil);
            if (!colision) Console.Write("->");
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("**");
            }
            Console.ResetColor();
        }
        static void RenderPuntos(int puntos)
        {
            Console.SetCursorPosition(0, ALTO + 1);
            Console.WriteLine("Puntos: " + puntos);
        }
        static void Debug(int[] suelo, int[] techo, int fil, int frame)
        {
            Console.SetCursorPosition(0, ALTO + 3);
            Console.Write("Suelo ");
            for (int i = 0; i < suelo.Length - 1; i++)
            {
                Console.Write("{0,2:0} ", suelo[i]);
            }

            Console.SetCursorPosition(0, ALTO + 2);
            Console.Write("Techo ");
            for (int i = 0; i < techo.Length - 1; i++)
            {
                Console.Write("{0,2:0} ", techo[i]);
            }

            Console.SetCursorPosition(0, ALTO + 4);
            Console.Write($"Pos Bird: {ALTO - 1 - fil,2}");
            Console.SetCursorPosition(15, ALTO + 4);
            Console.Write($"Frame: {frame,2} ");
        }
        #endregion
        static void Borrado(int[] suelo, int[] techo, int fil)
        {
            for (int i = 0; i < techo.Length; i++)          //Mira el componente de los Arrays, son iguales asi que da igual quien este en la condición
            {
                if (!(techo[i] == ALTO - 1))
                {
                    for (int j = 0; j <= (ALTO - 1 - techo[i]); j++) //En función de cuanto valga el componente repita la acción tantos bloques como haya.
                    {
                        Console.SetCursorPosition((2 * i), (ALTO - techo[i]) - j); //Pinta el primero y luego el segundo, en la posición 13-X-j; para que suba -j
                        Console.Write(" ");
                        Console.SetCursorPosition(2 * i + 1, (ALTO - techo[i]) - j); //Pinta el segundo bloque, la lógica es la misma que en computadores
                        Console.Write(" ");
                    }
                }
                if (!(suelo[i] == 0))
                {
                    for (int j = 0; j <= (suelo[i]); j++)       //Mira el componente del suelo y repite el for tantas veces indique.
                    {
                        Console.SetCursorPosition((2 * i), (ALTO - 2 - suelo[i]) + j); //Posicion desde 13-x+j; para que pinte hacia abajo
                        Console.Write(" ");
                        Console.SetCursorPosition(2 * i + 1, (ALTO - 2 - suelo[i]) + j); //Segundo bloque
                        Console.Write(" ");
                    }
                }
            }

            Console.ResetColor();
            Console.SetCursorPosition(2 * COL_BIRD, fil);
            Console.WriteLine("  ");

        }
        static void Avanza(int[] suelo, int[] techo, int frame)
        {
            for (int i = 0; i < (suelo.Length - 1); i++)
            {
                (suelo[i], suelo[i + 1]) = (suelo[i + 1], suelo[i]);
                (techo[i], techo[i + 1]) = (techo[i + 1], techo[i]);
            }

            if (frame % SEP_OBS == 0)
            {
                int s = rnd.Next(0, ALTO - 1 - HUECO);
                suelo[ANCHO - 1] = s;
                techo[ANCHO - 1] = s + HUECO +1;
            }
            else
            {
                suelo[ANCHO - 1] = 0;
                techo[ANCHO - 1] = ALTO - 1;
            }
        }
        static void Mueve(char ch, ref int fil, ref int ascenso)
        {
            if (ch == 'I') ascenso = IMPULSO;
            if (ascenso > 0)
            {
                ascenso--;
                fil--;
            }
            else fil++;
        }
        static bool Colision(int[] suelo, int[] techo, int fil)
        {
            bool colision = false;
            if ((suelo[COL_BIRD] >= ALTO - 1 - fil) || (techo[COL_BIRD] <= ALTO - 1 - fil)) colision = true;

            return colision;
        }
        static void Puntua(int[] suelo, int[] techo, ref int puntos)
        {
            if ((suelo[COL_BIRD] != 0 || techo[COL_BIRD] != ALTO-1)) puntos++;
        }
        static char LeeInput()
        {
            char ch = ' ';
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.I) ch = 'I';
                else if (tecla.Key == ConsoleKey.P) ch = 'P';
                else if (tecla.Key == ConsoleKey.Q) ch = 'Q';
            }
            return ch;
        }
        static void ProcesaInput(char ch, ref bool salir, ref bool pausa)
        { 
            if (ch == 'Q') salir = !salir;

            if (!pausa)
            {
                if (ch == 'P')
                {
                    pausa = true;
                    for (int i = 0; i < 3; i++)                                     //Pausa el juego, parando el bucle de juego
                    {                                                               //Escribe pausa borrando una posición arriba y abajo
                        Console.SetCursorPosition(ANCHO - 2, (ALTO / 2) - 1 + i);
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition((ANCHO - 2), ALTO / 2);
                    Console.Write("PAUSE");
                }
            }
            else
            {
                if (ch == 'P')
                {
                    for (int i = 0; i < 5; i++)
                    {                                                               //Borra la línea de pause
                        Console.SetCursorPosition(ANCHO - 2 + i, (ALTO / 2));
                        Console.Write(" ");
                    }
                    pausa = false;                                                  //Si se pulsa P de nuevo se despausa, devuelve el false al bool
                }
            }
        }
    }
}
