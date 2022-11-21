using System.Numerics;

namespace PineSandbox;

public static class SimConfig
{
	public const int WindowSize = 1024;
	public const int CanvasSize = 512;
	
	public const int Subdivisions = 64; //Needs to be a power of 2
	
	public const int TotalChunks = Subdivisions * Subdivisions;
	public const int ChunkSize = CanvasSize / Subdivisions;
	
	public static readonly Vector2 RenderPoint = new (0f, 0f);
	public static readonly Vector2 RenderRange = new (2f, 2f);
		
	public const int Iterations = 250;
}

//Vector2 centerPoint = new Vector2(0.3f, 0f);
//Vector2 range = new Vector2(0.1f, 0.1f);

//Vector2 centerPoint = new Vector2(-1.62917f,-0.0203968f);
//Vector2 range = new Vector2(2f / 5f, 2f / 5f);

//Vector2 centerPoint = new Vector2(-1.62917f,-0.0203968f);
//Vector2 range = new Vector2(2f / 25f, 2f / 25f);

//Note: 640s 250it
//Vector2 centerPoint = new Vector2(-0.761574f,-0.0847596f);
//Vector2 range = new Vector2(2f / 3125f, 2f / 3125f);