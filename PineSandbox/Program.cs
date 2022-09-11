using Raylib_cs;
using Pine2D;

namespace PineSandbox;

internal static class Program
{
    public static void Main()
    {
        Raylib.InitWindow(640, 360, "Hello World");
        EngineTest.Log();
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing(); 
            Raylib.ClearBackground(Color.WHITE);
            
            Raylib.DrawText(Raylib.GetFPS().ToString(), 16, 16, 20, Color.BLACK);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}