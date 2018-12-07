namespace AdventOfCode.Solutions.Day06
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;

  public class Solution : BaseSolution
  {
    List<Coordinate> coordinates;
    string[,] map;
    int maxX, maxY;

    public Solution() : base(6, "Chronal Coordinates")
    {
      coordinates = Input.Select(i => new Coordinate(i)).ToList();
      maxX = coordinates.OrderByDescending(x => x.X).First().X + 1;
      maxY = coordinates.OrderByDescending(y => y.Y).First().Y + 1;

      map = new string[maxY, maxX];
      FillArray(map, ".");
    }

    /* --- Day 6: Chronal Coordinates ---
    The device on your wrist beeps several times, and once again you feel like you're falling.

    "Situation critical," the device announces. "Destination indeterminate. Chronal interference detected. Please specify new target coordinates."

    The device then produces a list of coordinates (your puzzle input). Are they places it thinks are safe or dangerous? It recommends you check manual page 729. The Elves did not give you a manual.

    If they're dangerous, maybe you can minimize the danger by finding the coordinate that gives the largest distance from the other points.

    Using only the Manhattan distance, determine the area around each coordinate by counting the number of integer X,Y locations that are closest to that coordinate (and aren't tied in distance to any other coordinate).

    Your goal is to find the size of the largest area that isn't infinite. For example, consider the following list of coordinates:

    1, 1
    1, 6
    8, 3
    3, 4
    5, 5
    8, 9
    If we name these coordinates A through F, we can draw them on a grid, putting 0,0 at the top left:

    ..........
    .A........
    ..........
    ........C.
    ...D......
    .....E....
    .B........
    ..........
    ..........
    ........F.
    This view is partial - the actual grid extends infinitely in all directions. Using the Manhattan distance, each location's closest coordinate can be determined, shown here in lowercase:

    aaaaa.cccc
    aAaaa.cccc
    aaaddecccc
    aadddeccCc
    ..dDdeeccc
    bb.deEeecc
    bBb.eeee..
    bbb.eeefff
    bbb.eeffff
    bbb.ffffFf
    Locations shown as . are equally far from two or more coordinates, and so they don't count as being closest to any.

    In this example, the areas of coordinates A, B, C, and F are infinite - while not shown here, their areas extend forever outside the visible grid. However, the areas of coordinates D and E are finite: D is closest to 9 locations, and E is closest to 17 (both including the coordinate's location itself). Therefore, in this example, the size of the largest area is 17.

    What is the size of the largest area that isn't infinite?
     */
    public override string GetPart1Answer()
    {
      foreach (var c in coordinates)
      {
        map[c.Y, c.X] = "X";
      }

      PrintArray(map);
      return string.Empty;
    }

    public override string GetPart2Answer()
    {
      return string.Empty;
    }

    private static void FillArray(string[,] array, string value)
    {
      for (int i = 0; i < array.GetLength(0); i++)
      {
        for (int j = 0; j < array.GetLength(1); j++)
        {
          array[i, j] = value;
        }
      }
    }

    private static void PrintArray(string[,] array)
    {
      int rowLength = array.GetLength(0);
      int colLength = array.GetLength(1);

      for (int i = 0; i < rowLength; i++)
      {
        for (int j = 0; j < colLength; j++)
        {
          Console.Write(string.Format("{0} ", array[i, j]));
        }
        Console.Write(Environment.NewLine + Environment.NewLine);
      }
      Console.ReadLine();
    }

    [DebuggerDisplay("{X},{Y}")]
    private class Coordinate
    {
      public Coordinate(string val)
      {
        var pair = val.Replace(" ", string.Empty).Split(',');
        X = int.Parse(pair[0]);
        Y = int.Parse(pair[1]);
      }

      public int X { get; }
      public int Y { get; }
    }
  }
}
