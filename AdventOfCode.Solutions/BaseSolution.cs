using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AdventOfCode.Solutions.Properties;

namespace AdventOfCode.Solutions
{
  public abstract class BaseSolution : ISolution
  {
    protected BaseSolution(int day, string title)
    {
      Day = day;
      Title = title;
      Input = GetDayInput();
    }

    public List<string> Input { get; }
    public int Day { get; }
    public string Title { get; }

    public override string ToString()
    {
      var sb = new StringBuilder();

      sb.AppendLine($"Day {Day} | {Title}");
      sb.AppendLine($"Solution Part 1: {GetPart1Answer()}");
      sb.AppendLine($"Solution Part 2: {GetPart2Answer()}");

      return sb.ToString();
    }

    public abstract string GetPart1Answer();

    public abstract string GetPart2Answer();

    protected List<string> GetDayInput()
    {
      if (Day < 1 || Title == string.Empty) return new List<string>();

      var lines = System.IO.File.ReadAllLines($"../../../../AdventOfCode.Solutions/Day{Day.ToString("D2")}/input.txt");

      return new List<string>(lines);
    }

    protected string GetResourceString() => Resources.ResourceManager.GetString($"Day{Day.ToString("D2")}");
  }
}