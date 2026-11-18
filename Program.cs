using System;

namespace Ejercicios_Delegados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio3();

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

        static int Restar(int a, int b)
        {
            return a - b;
        }

        // Ejercicio 2: asignar varios métodos al mismo delegado, uno cada vez
        static void Ejercicio2()
        {
            Operacion operacion = Sumar;
            Console.WriteLine($"Sumar: {operacion(8, 3)}");

            // Cambiamos el método al que apunta el delegado
            operacion = Restar;
            Console.WriteLine($"Restar: {operacion(8, 3)}");
        }

        static int Multiplicar(int a, int b)
        {
            return a * b;
        }

        // Ejercicio 3: delegado multicast
        static void Ejercicio3()
        {
            // Con += añadimos más métodos al mismo delegado
            Operacion operaciones = Sumar;
            operaciones += Restar;
            operaciones += Multiplicar;

            // Si lo llamamos directamente solo veríamos el último resultado,
            // así que recorremos la lista de métodos uno a uno
            foreach (Operacion operacion in operaciones.GetInvocationList())
            {
                Console.WriteLine($"{operacion.Method.Name}: {operacion(8, 3)}");
            }
        }
    }
}