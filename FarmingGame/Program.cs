using Raylib_cs;
using FluxEngine;

namespace FarmingGame;

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
            
            Raylib.DrawText("Hello, world!", 16, 16, 20, Color.BLACK);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
