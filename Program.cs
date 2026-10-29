using System;
using System.Windows.Forms;

namespace tp_winform_equipo_y_recuperatorio
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CatalogoForm());
        }
    }
}
