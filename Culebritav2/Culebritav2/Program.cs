using System;
using System.Threading;

namespace Culebrita
{
    class Program
    {
        //clases y estructuras
        //inicia estructura coordenadaz
        public struct Coordenadaz
        {
            public int x;//valor en x, horizontal
            public int y;//valor en y, vertical
            public override bool Equals(object obj)
            {
                return obj is Coordenadaz && this == (Coordenadaz)obj;
            }//sobre escribe el metodo Equals para que funcione con 2 estructuras coordenadaz
            public override int GetHashCode()
            {
                return x.GetHashCode() ^ y.GetHashCode();
            }//sobre escribe el metodo GetHashCode para que trabaje con 2 estructuras coordenadaz
            public static bool operator ==(Coordenadaz c1, Coordenadaz c2)
            {
                return (c1.x == c2.x) && (c1.y == c2.y);
            }//sobrecarga el operador == (igual a) para que coompare 2 estructuras coordenadaz
            public static bool operator !=(Coordenadaz c1, Coordenadaz c2)
            {
                return (c1.x != c2.x) || (c1.y != c2.y);
            }//sobrecarga el operado != (no igual a) para que coompare 2 estructuras coordenadaz
        }//termina estructura coordenadaz
        //inicia clase culebrita
        class Culebrita
        {
            //atributos de la clase culebrita
            public Coordenadaz cabeza;
            Coordenadaz cuerpo1;
            Coordenadaz cuerpo2;
            public Coordenadaz cola;
            Coordenadaz borrador;
            Coordenadaz NewPos;
            //constructor
            public Culebrita()
            {
                cabeza = new Coordenadaz();
                cuerpo1 = new Coordenadaz();
                cuerpo2 = new Coordenadaz();
                cola = new Coordenadaz();
                borrador = new Coordenadaz();
                NewPos = new Coordenadaz();
            }//inicializa los atributos de la culebrita
            //metodos
            public void CulebritaPosicionInicial(Tablero tab)
            {
                cabeza.x = tab.MargenAncho;
                cabeza.y = tab.MargenAlto / 2;
                cuerpo1.x = cabeza.x - 2;
                cuerpo1.y = cabeza.y;
                cuerpo2.x = cuerpo1.x - 2;
                cuerpo2.y = cabeza.y;
                cola.x = cuerpo2.x - 2;
                cola.y = cabeza.y;
                borrador.x = tab.MargenAncho + 1;
                borrador.y = 0;
                NewPos.x = 0;
                NewPos.y = 0;
            }//asigna los valores iniciales de la culebrita en relacion al tablero
            public void Actualizar(ConsoleKey teclaPresionada)
            {
                borrador = cola;
                cola = cuerpo2;
                cuerpo2 = cuerpo1;
                cuerpo1 = cabeza;
                switch (teclaPresionada)
                {
                    case ConsoleKey.UpArrow:
                        cabeza.y--;
                        break;
                    case ConsoleKey.DownArrow:
                        cabeza.y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        cabeza.x -= 2;
                        break;
                    case ConsoleKey.RightArrow:
                        cabeza.x += 2;
                        break;
                }
            }//actualiza las coordenadas de la culebrita con forme a un una tecla flecha
            public bool EvaluarCabezaIgualCola(ConsoleKey teclaPresionada)
            {
                bool resultado = false;
                NewPos = cabeza;
                switch (teclaPresionada)
                {
                    case ConsoleKey.UpArrow:
                        NewPos.y--;
                        break;
                    case ConsoleKey.DownArrow:
                        NewPos.y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        NewPos.x -= 2;
                        break;
                    case ConsoleKey.RightArrow:
                        NewPos.x += 2;
                        break;
                }
                if (NewPos == cola)
                    resultado = true;
                return resultado;
            }//evalua si la posicion que tomara la cabeza de la culebrita es igual a la de la cola
            public bool EvaluarCabezaIgualCuerpo1(ConsoleKey teclaPresionada)
            {
                bool resultado = false;
                NewPos = cabeza;
                switch (teclaPresionada)
                {
                    case ConsoleKey.UpArrow:
                        NewPos.y--;
                        break;
                    case ConsoleKey.DownArrow:
                        NewPos.y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        NewPos.x -= 2;
                        break;
                    case ConsoleKey.RightArrow:
                        NewPos.x += 2;
                        break;
                }
                if (NewPos == cuerpo1)
                    resultado = true;
                return resultado;
            }//evalua si la posicion que tomara la cabeza de la culebrita es igual a la del primer segmento de cuerpo
            public bool EvaluarCabezaMargen(ConsoleKey teclaPresionada, Tablero tab)
            {
                bool resultado = false;
                NewPos = cabeza;
                switch (teclaPresionada)
                {
                    case ConsoleKey.UpArrow:
                        NewPos.y--;
                        break;
                    case ConsoleKey.DownArrow:
                        NewPos.y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        NewPos.x -= 2;
                        break;
                    case ConsoleKey.RightArrow:
                        NewPos.x += 2;
                        break;
                }
                if (NewPos.x == tab.MargenAncho * 2 - 1 || NewPos.y == tab.MargenAlto - 1 || NewPos.x == 1 || NewPos.y == 0)
                    resultado = true;
                return resultado;
            }//evalua si la posicion que tomara la cabeza de la culebrita coincide con un margen del tablero
            public void DibujarCulebrita()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(cabeza.x, cabeza.y);
                Console.Write("0");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(cuerpo1.x, cuerpo1.y);
                Console.Write("0");
                Console.SetCursorPosition(cuerpo2.x, cuerpo2.y);
                Console.Write("0");
                Console.SetCursorPosition(cola.x, cola.y);
                Console.Write("0");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(borrador.x, borrador.y);
                Console.Write("0");
            }//dibuja la culebrita
        }//termina clase culebrita
        //inicia clase tablero
        class Tablero
        {
            //atributos del tablero
            int AnchoCuadrante;
            int AltoCuadrante;
            int AnchoTotal;
            int AltoTotal;
            public int MargenAncho;
            public int MargenAlto;
            //constructor
            public Tablero()
            {
                AnchoCuadrante = 0;
                AltoCuadrante = 0;
                AnchoTotal = 0;
                AltoTotal = 0;
                MargenAncho = 0;
                MargenAlto = 0;
            }//inicializa los valores del tablero
            //metodos
            public void DibujarTablero(int temp1, int temp2)
            {
                Console.Clear();
                AnchoCuadrante = temp1;
                AltoCuadrante = temp2;
                AnchoTotal = 2 * AnchoCuadrante + 1;
                AltoTotal = 2 * AltoCuadrante + 1;
                MargenAncho = AnchoTotal + 2;
                MargenAlto = AltoTotal + 2;
                for (int filas = 0; filas < MargenAlto; filas++)
                {
                    for (int columnas = 0; columnas < MargenAncho; columnas++)
                    {
                        if (columnas == 0 || columnas == MargenAncho - 1 || filas == 0 || filas == MargenAlto - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write(" 0");
                    }
                    Console.WriteLine();
                }
            }//dibuja el tablero con las dimensiones recibidas
        }//termina clase tablero
        static void Main(string[] args)//inicia el codigo principal
        {
            //declaracion, instanciacion y declaracion
            string val1, val2;
            int alto, ancho;
            bool FinDelJuego = false;
            Tablero TableroDeJuego;
            Culebrita CulebritaDeJuego;
            ConsoleKeyInfo teclaPresionada;
            do//inicia bloque de instrucciones a repetir si se reinicia el juego
            {
                TableroDeJuego = new Tablero();//crea el tablero de juego como un tablero
                CulebritaDeJuego = new Culebrita();//crea la culebrita de juego como una culebrita
                FinDelJuego = false;//indica que el juego esta activo
                inicioprograma://punto de retorno si los datos ingresados no son validos
                Console.ForegroundColor = ConsoleColor.White;//asigna color de texto blanco
                Console.WriteLine("Instrucciones:");
                Console.WriteLine("Ingresa los valores del primer cuadrante: ");
                Console.WriteLine("valor minimo 3, valor maximo 15");//escribe las instrucciones
                Console.Write("x: ");
                val1 = Console.ReadLine();//pide y asigna el valor para x (horizontal primer cuadrante del tablero)
                Console.Write("y: ");
                val2 = Console.ReadLine();//pide y asigna el valor para y (vertical primer cuadrante del tablero)
                int temp1, temp2;
                if (int.TryParse(val1, out temp1) & int.TryParse(val2, out temp2))//verifica si los valores son numericos y enteros
                {
                    if ((int.Parse(val1) >= 3) & (int.Parse(val1) <= 15) & (int.Parse(val2) >= 3) & (int.Parse(val2) <= 15))//si son numeros enteros, verifica si estan dentro del rango minimo
                    {
                        ancho = int.Parse(val1);
                        alto = int.Parse(val2);
                    }//si son valores numericos enteros dentro de los rangos, convierte y asigna las dimensiones del primer cuadrante del tablero
                    else
                    {
                        Console.WriteLine("Valores fuera de rango, ingresalos nuevamente.");
                        Console.WriteLine("Enter para continuar...");
                        Console.ReadLine();
                        Console.Clear();
                        goto inicioprograma;
                    }//si son enteros pero estan fuera de rango, da aviso de error y regresa a pedir los datos de nuevo
                }
                else
                {
                    Console.WriteLine("Uno o todos los valores no son valios, ingresalos nuevamente.");
                    Console.WriteLine("Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    goto inicioprograma;
                }//si no son valres numericos, indica el error y regresa a pedir los datos nuevamente
                //inicio del proceso de dibujado
                TableroDeJuego.DibujarTablero(ancho, alto);//envia al tabrelos los valores del primer cuadrante y se dibujan los 4 margenes de lo tablero
                CulebritaDeJuego.CulebritaPosicionInicial(TableroDeJuego);//envia a la culebrita los datos de tablero para asignar la posicion inicial en el centro del tablero
                CulebritaDeJuego.DibujarCulebrita();//dibuja la culebrita
                do//bloque de instrucciones a repetir si NO se presiona la tecla x
                {
                    CulebritaDeJuego.DibujarCulebrita();//dibuja la culebrita
                    Console.SetCursorPosition(1, TableroDeJuego.MargenAlto + 1);//posiciona el cursor debajo del tablero
                    Console.WriteLine("                                                       ");//borra la liena de coordenada (1 linea debajo del tablero)
                    Console.SetCursorPosition(1, TableroDeJuego.MargenAlto + 1);//regresa a la linea de coordenada (1 linea debajo del tablero)
                    Console.ForegroundColor = ConsoleColor.White;//asigna color de texto blanco
                    Console.WriteLine("Coordenadas de la cabeza: ({0},{1})  cola:({2},{3})", (CulebritaDeJuego.cabeza.x - TableroDeJuego.MargenAncho) / 2, (TableroDeJuego.MargenAlto - 1) / 2 - CulebritaDeJuego.cabeza.y, (CulebritaDeJuego.cola.x - TableroDeJuego.MargenAncho) / 2, (TableroDeJuego.MargenAlto - 1) / 2 - CulebritaDeJuego.cola.y);//escribe las coordenadas de la cabeza de la culebrita
                    Console.ForegroundColor = ConsoleColor.Black;//asigna color de texto negro
                    teclaPresionada = Console.ReadKey();//lee la tecla presionada y la almacena
                    Console.SetCursorPosition(1, TableroDeJuego.MargenAlto + 3);//se posiciona 3 lineas debajo del tablero
                    Console.Write("                                                                      ");//borra el mensaje de error anterior
                    Console.SetCursorPosition(1, TableroDeJuego.MargenAlto + 3);//se posciciona en la linea de mensaje de error(3 lineas debajo de tablero)
                    Console.ForegroundColor = ConsoleColor.White;//asigna color de texto blanco
                    ConsoleKey temp = teclaPresionada.Key;
                    if (temp == ConsoleKey.UpArrow || temp == ConsoleKey.DownArrow || temp == ConsoleKey.LeftArrow || temp == ConsoleKey.RightArrow)//evalua que sea una tecla valida para despues evaluar el movimiento y despues actualizar la posicion de la culebrita
                    {
                        if (CulebritaDeJuego.EvaluarCabezaIgualCola(teclaPresionada.Key))//evalua si se mordio la cola
                        {
                            Console.WriteLine("Te mordiste la cola");
                            FinDelJuego = true;
                        }//si se mordio la cola, muestra el mensaje de error y asigna el final del juego en verdadero
                        else if (CulebritaDeJuego.EvaluarCabezaIgualCuerpo1(teclaPresionada.Key))//evalua si se intenta regresar
                            Console.WriteLine("No puedes regresar");//si se intenta regresar, muestra el mensaje de error y se queda esperando otra direccion
                        else if (CulebritaDeJuego.EvaluarCabezaMargen(teclaPresionada.Key, TableroDeJuego))//evalua si se choco con el margen
                        {
                            Console.WriteLine("Chocaste con la pared");
                            FinDelJuego = true;
                        }//si se choco con el margen muestra el mensaje de error y asigna el fin del juego en verdadero
                        else
                            CulebritaDeJuego.Actualizar(teclaPresionada.Key);//si no se mordio la cola, choco contra un margen, o intento regresar, actualiza la posicion de la culebrita
                    }
                    } while (teclaPresionada.Key != ConsoleKey.X && !FinDelJuego);//se realiza el bloque anterior si NO se presiona X(instruccion cerrar juego) o si fin del juego es falso
                if (teclaPresionada.Key == ConsoleKey.X)//evalua si la tecla presionada es igual a la tecla X
                {
                    Console.WriteLine(" Gracias por jugar culebrita"); //si la tecla presionada es x, muestra mensaje de cierre
                }
                else
                    Console.WriteLine(" Presione una tecla para reiniciar");//si no se presiona x, se muestra el mensaje para reiniciar el juego
                Console.ForegroundColor = ConsoleColor.Black;//asigna el color del tecto en negro
                Console.ReadKey();//lee una tecla para pausar el programa
                Console.Clear();//limpia el tablero para reiniciar el programa
                CulebritaDeJuego = null;//borra los todos los valores de la culebrita de juego
                TableroDeJuego = null;//borra todos los valores del tablero de juego
            } while (teclaPresionada.Key != ConsoleKey.X);//repite el bloque anterior si NO se presiona x

        }//fin del codigo principal
    }//fin de la clase programa
}//fin del programa entero