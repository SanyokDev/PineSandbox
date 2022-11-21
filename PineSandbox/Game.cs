using Pine2D.Core;
using Raylib_cs;

using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using Pine2D.Math;

namespace PineSandbox;

public class Game : PineApp
{
	private readonly Color[][] _chunkData = new Color[SimConfig.TotalChunks][];
	
	private void ProcessChunk(int chunkId)
	{
		var startP = new Vector2(
			SimConfig.RenderPoint.X - SimConfig.RenderRange.X,
			SimConfig.RenderPoint.Y - SimConfig.RenderRange.Y);
		var endP = new Vector2(
			SimConfig.RenderPoint.X + SimConfig.RenderRange.X, 
			SimConfig.RenderPoint.Y + SimConfig.RenderRange.Y);

		int chunkX = SimConfig.ChunkSize * (chunkId % SimConfig.Subdivisions);
		int chunkY = SimConfig.ChunkSize * (int)MathF.Floor(chunkId / (float)SimConfig.Subdivisions);
		
		for (var y = chunkY; y < chunkY + SimConfig.ChunkSize; y++)
		{
			for (var x = chunkX; x < chunkX + SimConfig.ChunkSize; x++)
			{
				var newX = MathP.Map(x, 0, SimConfig.CanvasSize, startP.X, endP.X);
				var newY = MathP.Map(y, 0, SimConfig.CanvasSize, startP.Y, endP.Y);

				var z = new ComplexNum(0, 0);
				var c = new ComplexNum(newX, newY);

				for (var i = 0; i <= SimConfig.Iterations; i++)
				{
					//Calculate the Mandelbrot Set formula
					var pR = z.Re * z.Re - z.Im * z.Im + c.Re;
					var pI = 2 * z.Re * z.Im + c.Im;
					z = new ComplexNum(pR, pI);

					if (z.Re * z.Re + z.Im * z.Im >= 4f)
					{
						var colorVal = MathP.Map(i, 0, SimConfig.Iterations, 0, 1);
						colorVal = MathP.Map(Math.Sqrt(colorVal), 0, 1, 0, 255);

						var pixelCol = new Color
						{
							r = (byte)colorVal,
							g = (byte)colorVal,
							b = (byte)colorVal,
							a = 255
						};
						
						_chunkData[chunkId][SimConfig.ChunkSize * (y % SimConfig.ChunkSize) + (x % SimConfig.ChunkSize)] = pixelCol;
						break;
					}
					
					_chunkData[chunkId][SimConfig.ChunkSize * (y % SimConfig.ChunkSize) + (x % SimConfig.ChunkSize)] = Color.BLACK;
				}
			}
		}
	}
	
	public Game(WindowSettings windowSettings) : base(windowSettings) { }
	
	protected override void Initialize()
	{
		base.Initialize();
		
		/*
		for (var i = 0; i < SimConfig.TotalChunks; i++)
		{
			_chunkData[i] = new Color[SimConfig.ChunkSize * SimConfig.ChunkSize];
			ProcessChunk(i);
		}
		*/
		
		Parallel.For(0, SimConfig.TotalChunks, i =>
		{
			_chunkData[i] = new Color[SimConfig.ChunkSize * SimConfig.ChunkSize];
			ProcessChunk(i);
		});
	}
	protected override void Update()
	{
		base.Update();
	}

	private bool _texLoaded;
	private Texture2D[] _canvasTextures = new Texture2D[SimConfig.TotalChunks]; //TODO: Find a way to use less textures to improve load times.
	
	protected override void Draw()
	{
		base.Draw();

		if (!_texLoaded)
		{
			for (var chunkId = 0; chunkId < SimConfig.TotalChunks; chunkId++)
			{
				//Generate canvas image
				var img = Raylib.GenImageColor(SimConfig.ChunkSize, SimConfig.ChunkSize, Color.BLANK);
			
				unsafe
				{
					const int arraySize = SimConfig.ChunkSize * SimConfig.ChunkSize;
					Color* imgColors = stackalloc Color[arraySize]; //TODO: Find a way to move the stackalloc out of the loop,
																	//TODO: or find a way to use the heap instead.
					for (var i = 0; i < arraySize; i++)
					{
						imgColors[i] = _chunkData[chunkId][i];
					}

					Marshal.FreeHGlobal((IntPtr)img.data);
					img.data = imgColors;
				}
		
				_canvasTextures[chunkId] = Raylib.LoadTextureFromImage(img);
			}
			
			_texLoaded = true;
		}

		//Draw canvas
		for (var chunkId = 0; chunkId < SimConfig.TotalChunks; chunkId++)
		{
			var cX = SimConfig.ChunkSize * (chunkId % SimConfig.Subdivisions);
			var cY = SimConfig.ChunkSize * (int)MathF.Floor(chunkId / (float)SimConfig.Subdivisions);
			const float texScale = SimConfig.WindowSize / (float)SimConfig.CanvasSize;
			
			Raylib.DrawTextureEx(_canvasTextures[chunkId], new Vector2(cX, cY) * texScale, 0f, texScale, Color.WHITE);
		}
	}
}
