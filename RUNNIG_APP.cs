using System;
using System.Collections.Generic;
using System.Linq;

namespace RunningApp
{
    public class Entrenamiento
    {
        public double Distancia { get; set; }
        public double Tiempo { get; set; }

        public Entrenamiento(double distancia, double tiempo)
        {
            Distancia = distancia;
            Tiempo = tiempo;
        }

        public override string ToString()
        {
            return $"Distancia: {Distancia} km, Tiempo: {Tiempo} min, Ritmo: {CalcularRitmo():F2} min/km";
        }

        private double CalcularRitmo()
        {
            return Distancia > 0 ? Tiempo / Distancia : 0;
        }
    }

    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Entrenamiento> Entrenamientos { get; set; }

        public Usuario(string nombreUsuario, string email, string password)
        {
            NombreUsuario = nombreUsuario;
            Email = email;
            Password = password;
            Entrenamientos = new List<Entrenamiento>();
        }
    }

    class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();
        static Usuario usuarioActual = null;

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarUsuario();
                        break;
                    case "2":
                        IniciarSesion();
                        break;
                    case "3":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Pulse una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("=== RUNNING_APP ===");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Iniciar sesión");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void RegistrarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE USUARIO ===");

            Console.Write("Ingrese nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            // Verificar que el nombre de usuario no exista
            if (usuarios.Any(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ya existe un usuario con ese nombre. Pulse una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese email: ");
            string email = Console.ReadLine();
            // Verificar que el email no exista
            if (usuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ya existe un usuario con ese email. Pulse una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese contraseña: ");
            string password = Console.ReadLine();

            usuarios.Add(new Usuario(nombreUsuario, email, password));
            Console.WriteLine("Usuario registrado correctamente. Pulse una tecla para continuar...");
            Console.ReadKey();
        }

        static void IniciarSesion()
        {
            Console.Clear();
            Console.WriteLine("=== INICIO DE SESIÓN ===");
            Console.Write("Nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string password = Console.ReadLine();

            usuarioActual = usuarios.FirstOrDefault(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase) && u.Password == password);
            if (usuarioActual == null)
            {
                Console.WriteLine("Credenciales incorrectas. Pulse una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // Menú de usuario
            bool cerrarSesion = false;
            while (!cerrarSesion)
            {
                MostrarMenuUsuario();
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarEntrenamiento();
                        break;
                    case "2":
                        ListarEntrenamientos();
                        break;
                    case "3":
                        VaciarEntrenamientos();
                        break;
                    case "4":
                        cerrarSesion = true;
                        usuarioActual = null;
                        Console.WriteLine("Sesión cerrada. Pulse una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Pulse una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarMenuUsuario()
        {
            Console.Clear();
            Console.WriteLine($"=== BIENVENIDO {usuarioActual.NombreUsuario} ===");
            Console.WriteLine("1. Registrar entrenamiento");
            Console.WriteLine("2. Listar entrenamientos");
            Console.WriteLine("3. Vaciar entrenamientos");
            Console.WriteLine("4. Cerrar sesión");
            Console.Write("Seleccione una opción: ");
        }

        static void RegistrarEntrenamiento()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR ENTRENAMIENTO ===");
            double distancia;
            double tiempo;
            try
            {
                Console.Write("Distancia recorrida (km): ");
                distancia = double.Parse(Console.ReadLine());
                Console.Write("Tiempo empleado (minutos): ");
                tiempo = double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error: debe introducir números válidos. Pulse una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            usuarioActual.Entrenamientos.Add(new Entrenamiento(distancia, tiempo));
            Console.WriteLine("Entrenamiento registrado correctamente. Pulse una tecla para continuar...");
            Console.ReadKey();
        }

        static void ListarEntrenamientos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE ENTRENAMIENTOS ===");
            if (usuarioActual.Entrenamientos.Count == 0)
            {
                Console.WriteLine("No hay entrenamientos registrados.");
            }
            else
            {
                for (int i = 0; i < usuarioActual.Entrenamientos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {usuarioActual.Entrenamientos[i]}");
                }
            }
            Console.WriteLine("Pulse una tecla para continuar...");
            Console.ReadKey();
        }

        static void VaciarEntrenamientos()
        {
            usuarioActual.Entrenamientos.Clear();
            Console.WriteLine("Todos los entrenamientos han sido eliminados. Pulse una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
