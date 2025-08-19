using CL_Compressor;
using WF_Compressor.Controllers;

namespace WF_Compressor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            // Configuración estándar para aplicaciones de Windows Forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- El ensamblaje final de nuestro patrón MVC ---

            // 1. Creamos una instancia de nuestra Vista (la ventana)
            // Asegúrate de que tu formulario se llame MainForm
            var view = new Form1();

            // 2. Creamos una instancia de nuestro Modelo (la lógica)
            var model = new HuffmanAlgorithm();

            // 3. Creamos el Controlador y le pasamos la Vista y el Modelo para que los conecte
            var controller = new MainController(view, model);

            // 4. Corremos la aplicación, mostrando la ventana principal
            Application.Run(view);
        }
    }
}