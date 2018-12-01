namespace AdventOfCode.SolutionRunner
{
  using System;
  using Solutions;

  internal class Program
  {
    private static void Main(string[] args)
    {
      Console.Clear();
      var repository = new SolutionRepository();

      foreach (ISolution solution in repository.GetAllSolutions())
      {
        Console.WriteLine(solution);
      }

      Console.ReadLine();
    }
  }
}
