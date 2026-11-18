using System;

namespace Ejercicios_Delegados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio1();

            Console.WriteLine("Pulsa una tecla para salir...");
            Console.ReadKey();
        }

        // Delegado: apunta a métodos con dos enteros que devuelven un entero
        delegate int Operacion(int x, int y);

        static int Sumar(int a, int b)
        {
            return a + b;
        }

        // Ejercicio 1: delegado básico
        static void Ejercicio1()
        {
            // Asignamos el método Sumar al delegado
            Operacion operacion = Sumar;

            int resultado = operacion(8, 3);
            Console.WriteLine($"Sumar: {resultado}");
        }
    }
}