using System;

namespace Ejercicios_Delegados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio4();

            Console.WriteLine("Pulsa una tecla para salir...");
            Console.ReadKey();
        }

        // Pide un entero y repite hasta que sea válido
        static int LeerEntero(string mensaje)
        {
            int numero;
            bool esValido;
            do
            {
                Console.Write(mensaje);
                esValido = int.TryParse(Console.ReadLine(), out numero);
                if (!esValido)
                {
                    Console.WriteLine("Entrada no válida. Introduce un número entero.");
                }
            } while (!esValido);
            return numero;
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

        static int Dividir(int a, int b)
        {
            // Si b vale 0, C# lanza una DivideByZeroException
            return a / b;
        }

        // Ejercicio 4: división por cero
        static void Ejercicio4()
        {
            int numero1 = LeerEntero("Introduce el primer número: ");
            int numero2 = LeerEntero("Introduce el segundo número: ");

            Operacion operaciones = Sumar;
            operaciones += Restar;
            operaciones += Multiplicar;
            operaciones += Dividir;

            foreach (Operacion operacion in operaciones.GetInvocationList())
            {
                // Probamos cada método por separado por si alguno falla
                try
                {
                    int resultado = operacion(numero1, numero2);
                    Console.WriteLine($"{operacion.Method.Name}: {resultado}");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine($"{operacion.Method.Name}: no se puede dividir entre cero");
                }
            }
        }
    }
}