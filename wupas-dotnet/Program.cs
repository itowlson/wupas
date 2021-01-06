using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Wasmtime;

namespace wupas_dotnet
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var engine = new Engine();
            using var host = new Host(engine);
            Wupas.DefineAll(host);

            var module = Module.FromTextFile(
                engine,
                "../examples/simple-window.wat"
            );

            using dynamic instance = host.Instantiate(module);
            instance.main();
        }
    }
}
