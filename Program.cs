using WorldMap2026.Model;
using WorldMap2026.Presenter;
using WorldMap2026.Services;

namespace WorldMap2026
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            IWorldStorage storage = new JsonWorldStorage();

            MapPresenter presenter = new MapPresenter(form, storage);

            Application.Run(form);
        }
    }
}