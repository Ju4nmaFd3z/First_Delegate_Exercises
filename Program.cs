using System;

namespace Ejercicios_Delegados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio8();

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

        // Recibe un delegado y dos enteros, llama al delegado y muestra el resultado
        static void MostrarResultado(Operacion operacion, int x, int y)
        {
            int resultado = operacion(x, y);
            Console.WriteLine($"{operacion.Method.Name}({x}, {y}) = {resultado}");
        }

        // Ejercicio 5: pasar un delegado como parámetro
        static void Ejercicio5()
        {
            MostrarResultado(Sumar, 8, 3);
            MostrarResultado(Restar, 8, 3);
            MostrarResultado(Multiplicar, 8, 3);
        }

        // Delegado nuevo para este ejercicio
        delegate int Operar(int x, int y);

        static int Suma(int a, int b)
        {
            return a + b;
        }

        static int Resta(int a, int b)
        {
            return a - b;
        }

        static int Multiplicacion(int a, int b)
        {
            return a * b;
        }

        // Ejercicio 6: nombre del método y resultado
        static void Ejercicio6()
        {
            Operar operaciones = Suma;
            operaciones += Resta;
            operaciones += Multiplicacion;

            foreach (Operar operacion in operaciones.GetInvocationList())
            {
                // Method.Name nos da el nombre del método
                Console.WriteLine($"{operacion.Method.Name}: {operacion(8, 3)}");
            }
        }

        // Delegado que recibe un array de enteros
        delegate int OperarArray(int[] numeros);

        static int SumaArray(int[] numeros)
        {
            int suma = 0;
            foreach (int numero in numeros)
            {
                suma += numero;
            }
            return suma;
        }

        static int MultiplicacionArray(int[] numeros)
        {
            // Empezamos en 1 porque multiplicar por 0 lo anularía todo
            int producto = 1;
            foreach (int numero in numeros)
            {
                producto *= numero;
            }
            return producto;
        }

        // Ejercicio 7: delegados con arrays
        static void Ejercicio7()
        {
            int[] numeros = { 2, 3, 4, 5 };

            OperarArray operaciones = SumaArray;
            operaciones += MultiplicacionArray;

            foreach (OperarArray operacion in operaciones.GetInvocationList())
            {
                Console.WriteLine($"{operacion.Method.Name}: {operacion(numeros)}");
            }
        }

        // Delegado con un solo entero
        delegate int Transformar(int x);

        static int Doblar(int x)
        {
            return x * 2;
        }

        static int Triplicar(int x)
        {
            return x * 3;
        }

        static int Cuadrado(int x)
        {
            return x * x;
        }

        // Ejercicio 8: transformar un número
        static void Ejercicio8()
        {
            int numero = LeerEntero("Introduce un número: ");

            Transformar transformaciones = Doblar;
            transformaciones += Triplicar;
            transformaciones += Cuadrado;

            foreach (Transformar transformacion in transformaciones.GetInvocationList())
            {
                Console.WriteLine($"{transformacion.Method.Name}({numero}) = {transformacion(numero)}");
            }
        }
    }
}