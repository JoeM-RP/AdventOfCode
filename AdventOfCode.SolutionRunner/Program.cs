namespace AdventOfCode.SolutionRunner
{
  using System;
  using System.Linq;
  using Solutions;

  internal class Program
  {
    private static void Main(string[] args)
    {
      var repository = new SolutionRepository();

      foreach (ISolution solution in repository.GetAllSolutions().Where(s => s.Title != string.Empty))
      {
        Console.WriteLine(solution);
      }

      Console.ReadLine();
    }
  }
}
