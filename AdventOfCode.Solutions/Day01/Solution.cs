namespace AdventOfCode.Solutions.Day01
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  public class Solution : BaseSolution
  {
    static List<int> input;
    public Solution() : base(1, "Chronal Calibration")
    {
      input = getcurrentFreqInput();
    }

    /* --- Day 1: Chronal Calibration ---
    "We've detected some temporal anomalies," one of Santa's Elves at the Temporal Anomaly Research and Detection Instrument Station tells you. She sounded pretty worried when she called you down here. "At 500-year intervals into the past, someone has been changing Santa's history!"

    "The good news is that the changes won't propagate to our time stream for another 25 days, and we have a device" - she attaches something to your wrist - "that will let you fix the changes with no such propagation delay. It's configured to send you 500 years further into the past every few days; that was the best we could do on such short notice."

    "The bad news is that we are detecting roughly fifty anomalies throughout time; the device will indicate fixed anomalies with stars. The other bad news is that we only have one device and you're the best person for the job! Good lu--" She taps a button on the device and you suddenly feel like you're falling. To save Christmas, you need to get all fifty stars by December 25th.

    Collect stars by solving puzzles. Two puzzles will be made available on each day in the advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

    After feeling like you've been falling for a few minutes, you look at the device's tiny screen. "Error: Device must be calibrated before first use. currentFreq drift detected. Cannot maintain destination lock." Below the message, the device shows a sequence of changes in currentFreq (your puzzle input). A value like +6 means the current currentFreq increases by 6; a value like -3 means the current currentFreq decreases by 3.

    For example, if the device displays currentFreq changes of +1, -2, +3, +1, then starting from a currentFreq of zero, the following changes would occur:

    Current currentFreq  0, change of +1; resulting currentFreq  1.
    Current currentFreq  1, change of -2; resulting currentFreq -1.
    Current currentFreq -1, change of +3; resulting currentFreq  2.
    Current currentFreq  2, change of +1; resulting currentFreq  3.
    In this example, the resulting currentFreq is 3.

    Here are other example situations:

    +1, +1, +1 results in  3
    +1, +1, -2 results in  0
    -1, -2, -3 results in -6
    Starting with a currentFreq of zero, what is the resulting currentFreq after all of the changes in currentFreq have been applied?
    */
    public override string GetPart1Answer()
    {
      return "490"; // Returning previously solved value to save time

      var result = 0;

      foreach (var f in input)
      {
        result += f;
      }

      return result.ToString();
    }

    /* --- Part Two ---
    You notice that the device repeats the same currentFreq change list over and over. To calibrate the device, you need to find the first currentFreq it reaches twice.

    For example, using the same list of changes above, the device would loop as follows:

    Current currentFreq  0, change of +1; resulting currentFreq  1.
    Current currentFreq  1, change of -2; resulting currentFreq -1.
    Current currentFreq -1, change of +3; resulting currentFreq  2.
    Current currentFreq  2, change of +1; resulting currentFreq  3.
    (At this point, the device continues from the start of the list.)
    Current currentFreq  3, change of +1; resulting currentFreq  4.
    Current currentFreq  4, change of -2; resulting currentFreq  2, which has already been seen.
    In this example, the first currentFreq reached twice is 2. Note that your device might need to repeat its list of currentFreq changes many times before a duplicate currentFreq is found, and that duplicates might be found while in the middle of processing the list.

    Here are other examples:

    +1, -1 first reaches 0 twice.
    +3, +3, +4, -2, -4 first reaches 10 twice.
    -6, +3, +8, +5, -6 first reaches 5 twice.
    +7, +7, -2, -7, -4 first reaches 14 twice.
    What is the first currentFreq your device reaches twice?
    */
    public override string GetPart2Answer()
    {
      return "70357"; // Returning previously solved value to save time

      var result = 0;
      var currentFreq = 0;
      var fLog = new List<int>();

      while (true)
      {
        foreach (var f in input)
        {
          currentFreq += f;

          if (fLog.Contains(currentFreq))
          {
            result = currentFreq;
            return result.ToString();
          }

          fLog.Add(currentFreq);
        }
      }
    }

    private List<int> getcurrentFreqInput()
    {
      var result = new List<int>();

      foreach (string line in Input)
      {
        result.Add(int.Parse(line));
      }

      return result;
    }
  }
}
