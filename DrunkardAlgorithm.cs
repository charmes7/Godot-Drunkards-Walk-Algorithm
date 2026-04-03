using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public partial class DrunkardAlgorithm : TileMapLayer
{
	[Export] public int maxSteps = 600;
    [Export] public TileMapLayer dungeonMap;
    [Export] public Camera2D camera;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        dungeonMap.Clear();
		GenerateDungeon(maxSteps);
	}

	private void GenerateDungeon(int maxSteps)
	{
        HashSet<Vector2I> floorCells = GetFloorCells();
        HashSet<Vector2I> wallCells = GetWallCells(floorCells);
        DrawTiles(floorCells, wallCells);
        SetCameraPosition(floorCells);
    }

	private HashSet<Vector2I> GetFloorCells()
	{
        HashSet<Vector2I> floorCells = new HashSet<Vector2I>();
        Random random = new Random();
        Vector2I currentPosition = new Vector2I(0,0);

        // generate floor cells using drunkard's walk algo
        for (int i = 0; i < maxSteps; i++)
        {
            int directions = random.Next(0, 4);
            switch (directions)
            {
                case 0:
                    currentPosition += Vector2I.Up;
                    floorCells.Add(currentPosition);
                    break;
                case 1:
                    currentPosition += Vector2I.Down;
                    floorCells.Add(currentPosition);
                    break;
                case 2:
                    currentPosition += Vector2I.Left;
                    floorCells.Add(currentPosition);
                    break;
                case 3:
                    currentPosition += Vector2I.Right;
                    floorCells.Add(currentPosition);
                    break;
            }
        }
        return (floorCells);
    }

    private HashSet<Vector2I> GetWallCells(HashSet<Vector2I> floorCells)
    {
        HashSet<Vector2I> wallCells = new HashSet<Vector2I>();

        // generate wall cells around each existing floor cells
        foreach (Vector2I fCell in floorCells)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    Vector2I checkCell = fCell + new Vector2I(x, y);
                    if (!floorCells.Contains(checkCell))
                        wallCells.Add(checkCell);
                }
            }
        }
        //GD.Print("Wall Cells in the HashSet: ");
        //foreach (Vector2I coord in wallCells)
        //    GD.Print(coord);
        return (wallCells);
    }

    private void DrawTiles(HashSet<Vector2I> floorcells, HashSet<Vector2I> wallCells)
    {
        foreach (Vector2I fcell in floorcells)
        {
            dungeonMap.SetCell(fcell, 0, new Vector2I(5, 1), 1);
        }
        foreach (Vector2I wcell in wallCells)
        {
            dungeonMap.SetCell(wcell, 0, new Vector2I(5, 1), 0);
        }
    }

    private void SetCameraPosition(HashSet<Vector2I> floorCells)
    {
        int maxX = 0;
        int minX = 0;
        int maxY = 0;
        int minY = 0;
        foreach (Vector2I coord in floorCells)
        {
            if (coord.X > maxX)
                maxX = coord.X;
            else if (coord.X < minX)
                minX = coord.X;
            if (coord.Y > maxY)
                maxY = coord.Y;
            else if (coord.Y < minY)
                minY = coord.Y;
        }
        GD.Print("MIN X,Y" + minX + "," + minY + "\nMAX X,Y" + maxX + "," + maxY);
        Vector2I center = new Vector2I((maxX + minX) / 2, (maxY + minY) / 2);
        GD.Print("\nCenter: " + center);
        camera.Position = center * 16; // multiply by tile pixel size
        GD.Print("Camera Position: " + camera.Position);
    }
}
