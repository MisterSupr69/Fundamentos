using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestionTareasPersonales
{
    // Enumeración para los tipos permitidos de tarea
    public enum TipoTarea
    {
        persona,
        trabajo,
        ocio
    }

    // Clase que representa una tarea
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TipoTarea Tipo { get; set; }
        public bool Prioridad { get; set; }  // true = prioritaria, false = normal

        public override string ToString()
        {
            string prioridadStr = Prioridad ? "Sí" : "No";
            return $"ID: {Id} | Nombre: {Nombre} | Descripción: {Descripcion} | Tipo: {Tipo} | Prioritaria: {prioridadStr}";
        }
    }

    class Program
    {
        // Lista principal que almacena todas las tareas
        static List<Tarea> listaTareas = new List<Tarea>();
        // Identificador único auto-incremental
        static int siguienteId = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearTarea();
                        break;
                    case "2":
                        BuscarTareasPorTipo();
                        break;
                    case "3":
                        EliminarTarea();
                        break;
                    case "4":
                        ExportarTareas();
                        break;
                    case "5":
                        ImportarTareas();
                        break;
                    case "6":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("#### GESTIÓN DE TAREAS PERSONALES ####");
            Console.WriteLine("1. Crear tarea");
            Console.WriteLine("2. Buscar tareas por tipo");
            Console.WriteLine("3. Eliminar tarea (por ID)");
            Console.WriteLine("4. Exportar tareas a archivo");
            Console.WriteLine("5. Importar tareas desde archivo");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
        }

        // 1. Crear tarea: pide datos, asigna ID automático y agrega a la lista
        static void CrearTarea()
        {
            Console.WriteLine("\n### Crear nueva tarea ###");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            // Validación del tipo (enum)
            TipoTarea tipo = TipoTarea.persona;
            bool tipoValido = false;
            while (!tipoValido)
            {
                Console.Write("Tipo (persona / trabajo / ocio): ");
                string entradaTipo = Console.ReadLine().ToLower();
                if (entradaTipo == "persona")
                {
                    tipo = TipoTarea.persona;
                    tipoValido = true;
                }
                else if (entradaTipo == "trabajo")
                {
                    tipo = TipoTarea.trabajo;
                    tipoValido = true;
                }
                else if (entradaTipo == "ocio")
                {
                    tipo = TipoTarea.ocio;
                    tipoValido = true;
                }
                else
                {
                    Console.WriteLine("Tipo no válido. Debe ser 'persona', 'trabajo' u 'ocio'.");
                }
            }

            // Prioridad booleana
            Console.Write("¿Es prioritaria? (s/n): ");
            string prioridadInput = Console.ReadLine().ToLower();
            bool prioridad = (prioridadInput == "s" || prioridadInput == "si");

            // Crear objeto tarea con ID automático
            Tarea nuevaTarea = new Tarea
            {
                Id = siguienteId++,
                Nombre = nombre,
                Descripcion = descripcion,
                Tipo = tipo,
                Prioridad = prioridad
            };

            listaTareas.Add(nuevaTarea);
            Console.WriteLine($"\n✓ Tarea creada con ID {nuevaTarea.Id}");
        }

        // 2. Buscar tareas por tipo (filtra y muestra)
        static void BuscarTareasPorTipo()
        {
            Console.WriteLine("\n--- Buscar tareas por tipo ---");
            Console.Write("Tipo a buscar (persona / trabajo / ocio): ");
            string entradaTipo = Console.ReadLine().ToLower();

            TipoTarea tipoBuscado;
            switch (entradaTipo)
            {
                case "persona":
                    tipoBuscado = TipoTarea.persona;
                    break;
                case "trabajo":
                    tipoBuscado = TipoTarea.trabajo;
                    break;
                case "ocio":
                    tipoBuscado = TipoTarea.ocio;
                    break;
                default:
                    Console.WriteLine("Tipo no válido.");
                    return;
            }

            var tareasFiltradas = listaTareas.Where(t => t.Tipo == tipoBuscado).ToList();

            if (tareasFiltradas.Count == 0)
            {
                Console.WriteLine($"No se encontraron tareas de tipo '{tipoBuscado}'.");
            }
            else
            {
                Console.WriteLine($"\n--- Tareas de tipo '{tipoBuscado}' ---");
                foreach (var tarea in tareasFiltradas)
                {
                    Console.WriteLine(tarea.ToString());
                }
            }
        }

        // 3. Eliminar tarea por ID
        static void EliminarTarea()
        {
            Console.WriteLine("\n--- Eliminar tarea ---");
            if (listaTareas.Count == 0)
            {
                Console.WriteLine("No hay tareas para eliminar.");
                return;
            }

            Console.Write("Ingrese el ID de la tarea a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int idEliminar))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            Tarea tareaAEliminar = listaTareas.FirstOrDefault(t => t.Id == idEliminar);
            if (tareaAEliminar == null)
            {
                Console.WriteLine($"No existe ninguna tarea con ID {idEliminar}.");
            }
            else
            {
                listaTareas.Remove(tareaAEliminar);
                Console.WriteLine($"✓ Tarea '{tareaAEliminar.Nombre}' (ID {idEliminar}) eliminada.");
            }
        }

        // 4. Exportar tareas a archivo tareas.txt
        static void ExportarTareas()
        {
            Console.WriteLine("\n--- Exportar tareas ---");
            if (listaTareas.Count == 0)
            {
                Console.WriteLine("No hay tareas para exportar.");
                return;
            }

            string rutaArchivo = "tareas.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    foreach (var tarea in listaTareas)
                    {
                        // Formato: id;nombre;descripcion;tipo;prioridad
                        string linea = $"{tarea.Id};{tarea.Nombre};{tarea.Descripcion};{tarea.Tipo};{tarea.Prioridad}";
                        sw.WriteLine(linea);
                    }
                }
                Console.WriteLine($"✓ Se han exportado {listaTareas.Count} tareas al archivo '{rutaArchivo}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar: {ex.Message}");
            }
        }

        // 5. Importar tareas desde tareas.txt (las agrega a la lista actual con nuevos IDs)
        static void ImportarTareas()
        {
            Console.WriteLine("\n--- Importar tareas desde archivo ---");
            string rutaArchivo = "tareas.txt";
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"No se encontró el archivo '{rutaArchivo}'.");
                return;
            }

            int contadorImportadas = 0;
            try
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] partes = linea.Split(';');
                    if (partes.Length != 5)
                    {
                        Console.WriteLine($"Línea con formato incorrecto, se omite: {linea}");
                        continue;
                    }

                    // Ignoramos el ID del archivo (partes[0]), asignamos uno nuevo automático
                    string nombre = partes[1];
                    string descripcion = partes[2];
                    string tipoStr = partes[3];

                    // Validar y convertir el tipo
                    if (!Enum.TryParse(tipoStr, true, out TipoTarea tipo))
                    {
                        Console.WriteLine($"Tipo '{tipoStr}' no válido en línea: {linea}. Tarea omitida.");
                        continue;
                    }

                    // Convertir prioridad (bool)
                    if (!bool.TryParse(partes[4], out bool prioridad))
                    {
                        Console.WriteLine($"Prioridad '{partes[4]}' no válida en línea: {linea}. Tarea omitida.");
                        continue;
                    }

                    // Crear nueva tarea con ID autoincremental
                    Tarea nuevaTarea = new Tarea
                    {
                        Id = siguienteId++,
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Tipo = tipo,
                        Prioridad = prioridad
                    };
                    listaTareas.Add(nuevaTarea);
                    contadorImportadas++;
                }

                Console.WriteLine($"✓ Importación completada. Se agregaron {contadorImportadas} tareas.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al importar: {ex.Message}");
            }
        }
    }
}
