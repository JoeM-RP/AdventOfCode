namespace AdventOfCode.Solutions.Day03
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Text.RegularExpressions;

  public class Solution : BaseSolution
  {
    private static int maxX = 1001;
    private static int maxY = 1001;
    private List<Claim> claims { get; set; }
    private int[,] claimMap = new int[maxX, maxY];
    public Solution() : base(3, "")
    {
      ParseInputFile();
      PopulateClaimMap();
    }

    /* --- Day 3: No Matter How You Slice It ---
    The Elves managed to locate the chimney-squeeze prototype fabric for Santa's suit (thanks to someone who helpfully wrote its box IDs on the wall of the warehouse in the middle of the night). Unfortunately, anomalies are still affecting them - nobody can even agree on how to cut the fabric.

    The whole piece of fabric they're working on is a very large square - at least 1000 inches on each side.

    Each Elf has made a claim about which area of fabric would be ideal for Santa's suit. All claims have an ID and consist of a single rectangle with edges parallel to the edges of the fabric. Each claim's rectangle is defined as follows:

    The number of inches between the left edge of the fabric and the left edge of the rectangle.
    The number of inches between the top edge of the fabric and the top edge of the rectangle.
    The width of the rectangle in inches.
    The height of the rectangle in inches.
    A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches from the left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall. Visually, it claims the square inches of fabric represented by # (and ignores the square inches of fabric represented by .) in the diagram below:

    ...........
    ...........
    ...#####...
    ...#####...
    ...#####...
    ...#####...
    ...........
    ...........
    ...........
    The problem is that many of the claims overlap, causing two or more claims to cover part of the same areas. For example, consider the following claims:

    #1 @ 1,3: 4x4
    #2 @ 3,1: 4x4
    #3 @ 5,5: 2x2
    Visually, these claim the following areas:

    ........
    ...2222.
    ...2222.
    .11XX22.
    .11XX22.
    .111133.
    .111133.
    ........
    The four square inches marked with X are claimed by both 1 and 2. (Claim 3, while adjacent to the others, does not overlap either of them.)

    If the Elves all proceed with their own plans, none of them will have enough fabric. How many square inches of fabric are within two or more claims?
     */
    public override string GetPart1Answer()
    {
      return "101469"; // Returning previously solved value to save time

      var result = 0;

      for (var X = 0; X < maxX; X++)
      {
        for (var Y = 0; Y < maxY; Y++)
        {
          if (claimMap[X, Y] > 1)
          {
            result++;
          }
        }
      }

      // 101469
      return result.ToString();
    }

    /* --- Part Two ---
    Amidst the chaos, you notice that exactly one claim doesn't overlap by even a single square inch of fabric with any other claim. If you can somehow draw attention to it, maybe the Elves will be able to make Santa's suit after all!

    For example, in the claims above, only claim 3 is intact after all claims are made.

    What is the ID of the only claim that doesn't overlap?
     */
    public override string GetPart2Answer()
    {
      /* This needs work, it seems */

      // foreach (var c in claims)
      // {
      // for (var X = 0; X < maxX; X++)
      // {
      //   for (var Y = 0; Y < maxY; Y++)
      //   {
      //     if (claimMap[X, Y] > 1)
      //     {
      //       continue;
      //     }
      //   }
      // }
      //   return c.Id;
      // }

      return "ERR";
    }

    private void PopulateClaimMap()
    {
      foreach (var claim in claims)
      {
        for (var X = 0; X < claim.Width; X++)
        {
          for (var Y = 0; Y < claim.Height; Y++)
          {
            if (claimMap[claim.X + X, claim.Y + Y] < 2)
            {
              claimMap[claim.X + X, claim.Y + Y]++;
            }
          }
        }
      }
    }
    private void ParseInputFile()
    {
      claims = Input.Select(i => new Claim(i)).ToList();
    }

    private class Claim
    {
      // #1 @ 429,177: 12x27
      public Claim(string claim)
      {
        var values = claim.Split(' ');
        Id = values[0];

        var co = values[2].Replace(":", string.Empty).Split(',');
        X = int.Parse(co[0]);
        Y = int.Parse(co[1]);

        var size = values[3].Split('x');
        Width = int.Parse(size[0]);
        Height = int.Parse(size[1]);
      }

      public string Id { get; }
      public int X { get; }
      public int Y { get; }
      public int Width { get; }
      public int Height { get; }
    }
  }
}
