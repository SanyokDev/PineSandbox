using Pine2D.Core;

namespace PineSandbox;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static void Main()
    {
        var settings = new WindowSettings
        {
            ScreenWidth = SimConfig.WindowSize,
            ScreenHeight = SimConfig.WindowSize,
            Title = "Mandelbrot Set Visualizer - Raylib"
        };

        var app = new Game(settings);
        app.Run();
    }
}
