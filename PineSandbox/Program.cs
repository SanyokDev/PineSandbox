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
            /*
            ScreenWidth = 320,
            ScreenHeight = 160,
            Title = "Test"
            */
        };

        var app = new Game(settings);
        app.Run();
    }
}
