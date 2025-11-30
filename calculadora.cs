using System;

public class Calculadora
{
    static void Main()
    {
        int primerNumero = 0;
        int segundoNumero = 0;
        bool salir = false;
        
        // Pedir los dos números positivos
        Console.WriteLine("claculadora");
        
        do
        {
            Console.Write("Introduce el primer número (debe ser positivo): ");
            while (!int.TryParse(Console.ReadLine(), out primerNumero) || primerNumero < 0)
            {
                Console.Write("Error: Debe ser un número entero positivo. Introduce el primer número: ");
            }
            
            Console.Write("Introduce el segundo número (debe ser positivo): ");
            while (!int.TryParse(Console.ReadLine(), out segundoNumero) || segundoNumero < 0)
            {
                Console.Write("Error: Debe ser un número entero positivo. Introduce el segundo número: ");
            }
            
            // Verificar que ambos son positivos
            if (primerNumero >= 0 && segundoNumero >= 0)
            {
                salir = false;
                MostrarMenu(primerNumero, segundoNumero, ref salir);
            }
            
        } while (!salir);
        
        Console.WriteLine("Gracias por usar la calculadora");
    }
    
    static void MostrarMenu(int num1, int num2, ref bool salir)
    {
        int opcion;
        bool operacionValida;
        
        do
        {
            Console.WriteLine("\n Menu ");
            Console.WriteLine($"Números actuales: {num1} y {num2}");
            Console.WriteLine("1. Suma.");
            Console.WriteLine("2. Resta.");
            Console.WriteLine("3. Multiplicación.");
            Console.WriteLine("4. División.");
            Console.WriteLine("5. Cambiar números");
            Console.WriteLine("6. Salir.");
            
            operacionValida = int.TryParse(Console.ReadLine(), out opcion);
            
            if (!operacionValida || opcion < 1 || opcion > 6)
            {
                Console.WriteLine("Error: Opción no encontrada. Introduce una opción válida (1-6).");
                continue;
            }
            
            switch (opcion)
            {
                case 1: // Suma
                    int resultadoSuma = num1 + num2;
                    Console.WriteLine($"\nResultado de {num1} + {num2} = {resultadoSuma}");
                    break;
                    
                case 2: // Resta
                    int resultadoResta = num1 - num2;
                    Console.WriteLine($"\nResultado de {num1} - {num2} = {resultadoResta}");
                    break;
                    
                case 3: // Multiplicación
                    int resultadoMultiplicacion = num1 * num2;
                    Console.WriteLine($"\nResultado de {num1} * {num2} = {resultadoMultiplicacion}");
                    break;
                    
                case 4: // División
                    if (num2 == 0)
                    {
                        Console.WriteLine("Error: No se puede dividir entre cero.");
                    }
                    else
                    {
                        double resultadoDivision = (double)num1 / num2;
                        Console.WriteLine($"\nResultado de {num1} / {num2} = {resultadoDivision:F2}");
                    }
                    break;
                    
                case 5: // Cambiar números
                    Console.WriteLine("Volviendo para cambiar los números...");
                    return; // Sale del método para volver a pedir números
                    
                case 6: // Salir
                    Console.WriteLine("Saliendo del programa...");
                    salir = true;
                    return;
                    
                default:
                    Console.WriteLine("Error: Opción no válida.");
                    break;
            }
            
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            
        } while (!salir);
    }
}
