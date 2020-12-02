namespace AdventOfCode.Solutions.Day04
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Text.RegularExpressions;

  public class Solution : BaseSolution
  {
    private List<GuardShift> shifts = new List<GuardShift>();
    private List<Guard> guards = new List<Guard>();
    public Solution() : base(4, "")
    {
      ParseInputFile();
    }

    /* --- Day 4: Repose Record ---
    You've sneaked into another supply closet - this time, it's across from the prototype suit manufacturing lab. You need to sneak inside and fix the issues with the suit, but there's a guard stationed outside the lab, so this is as close as you can safely get.

    As you search the closet for anything that might help, you discover that you're not the first person to want to sneak in. Covering the walls, someone has spent an hour starting every midnight for the past few months secretly observing this guard post! They've been writing down the ID of the one guard on duty that night - the Elves seem to have decided that one guard was enough for the overnight shift - as well as when they fall asleep or wake up while at their post (your puzzle input).

    For example, consider the following records, which have already been organized into chronological order:

    [1518-11-01 00:00] Guard #10 begins shift
    [1518-11-01 00:05] falls asleep
    [1518-11-01 00:25] wakes up
    [1518-11-01 00:30] falls asleep
    [1518-11-01 00:55] wakes up
    [1518-11-01 23:58] Guard #99 begins shift
    [1518-11-02 00:40] falls asleep
    [1518-11-02 00:50] wakes up
    [1518-11-03 00:05] Guard #10 begins shift
    [1518-11-03 00:24] falls asleep
    [1518-11-03 00:29] wakes up
    [1518-11-04 00:02] Guard #99 begins shift
    [1518-11-04 00:36] falls asleep
    [1518-11-04 00:46] wakes up
    [1518-11-05 00:03] Guard #99 begins shift
    [1518-11-05 00:45] falls asleep
    [1518-11-05 00:55] wakes up
    Timestamps are written using year-month-day hour:minute format. The guard falling asleep or waking up is always the one whose shift most recently started. Because all asleep/awake times are during the midnight hour (00:00 - 00:59), only the minute portion (00 - 59) is relevant for those events.

    Visually, these records show that the guards are asleep at these times:

    Date   ID   Minute
                000000000011111111112222222222333333333344444444445555555555
                012345678901234567890123456789012345678901234567890123456789
    11-01  #10  .....####################.....#########################.....
    11-02  #99  ........................................##########..........
    11-03  #10  ........................#####...............................
    11-04  #99  ....................................##########..............
    11-05  #99  .............................................##########.....
    The columns are Date, which shows the month-day portion of the relevant day; ID, which shows the guard on duty that day; and Minute, which shows the minutes during which the guard was asleep within the midnight hour. (The Minute column's header shows the minute's ten's digit in the first row and the one's digit in the second row.) Awake is shown as ., and asleep is shown as #.

    Note that guards count as asleep on the minute they fall asleep, and they count as awake on the minute they wake up. For example, because Guard #10 wakes up at 00:25 on 1518-11-01, minute 25 is marked as awake.

    If you can figure out the guard most likely to be asleep at a specific time, you might be able to trick that guard into working tonight so you can have the best chance of sneaking in. You have two strategies for choosing the best guard/minute combination.

    Strategy 1: Find the guard that has the most minutes asleep. What minute does that guard spend asleep the most?

    In the example above, Guard #10 spent the most minutes asleep, a total of 50 minutes (20+25+5), while Guard #99 only slept for a total of 30 minutes (10+10+10). Guard #10 was asleep most during minute 24 (on two days, whereas any other minute the guard was asleep was only seen on one day).

    While this example listed the entries in chronological order, your entries are in the order you found them. You'll need to organize them before they can be analyzed.

    What is the ID of the guard you chose multiplied by the minute you chose? (In the above example, the answer would be 10 * 24 = 240.) */
    public override string GetPart1Answer()
    {
      foreach (var g in guards)
      {
        var lastState = GuardState.none;
        var startSleep = new DateTime();

        foreach (var s in shifts.Where(s => s.GuardId == g.GuardId).OrderBy(d => d.TimeStamp).ToList())
        {
          // shift starts
          if (s.State == GuardState.begin && (lastState == GuardState.sleep || lastState == GuardState.none))
          {
          }

          // falls alseep
          if (s.State == GuardState.sleep && (lastState == GuardState.begin || lastState == GuardState.wake))
          {
            startSleep = s.TimeStamp;
          }

          // wakes up
          if (s.State == GuardState.wake && lastState == GuardState.sleep)
          {
            var duration = s.TimeStamp - startSleep;
            g.TimeAsleep += duration;

            // iterate over time alseep and add to minuteMap
            for (var m = 0; m < duration.Minutes; m++)
            {
              var test = startSleep.Minute + m;
              g.SleepMap[test]++;
            }
          }

          // shift ends
          /* noop */

          lastState = s.State;
        }

        g.SleepyMinuteCount = g.SleepMap.OrderByDescending(m => m).First(); // time asleep on a given minute
      }

      var sleepyGuard = guards.OrderByDescending(x => x.TimeAsleep).First();
      var sleepyMinute = Array.IndexOf(sleepyGuard.SleepMap, sleepyGuard.SleepyMinuteCount);

      return (sleepyGuard.GuardId * sleepyMinute).ToString();
    }

    public override string GetPart2Answer()
    {
      var sleepyGuard = guards.OrderByDescending(x => x.SleepyMinuteCount).First();
      var sleepyMinute = Array.IndexOf(sleepyGuard.SleepMap, sleepyGuard.SleepyMinuteCount);

      return (sleepyGuard.GuardId * sleepyMinute).ToString();
    }

    private void ParseInputFile()
    {
      var idRegex = new Regex(@"(#[0-9]+)");

      var activeGuard = 0;
      shifts = Input.Select(i => new GuardShift(i))
      .OrderBy(gs => gs.TimeStamp)
      .ToList();

      foreach (var gs in shifts)
      {

        if (gs.State == GuardState.begin)
        {
          var id = idRegex.Match(gs.Description);
          activeGuard = int.Parse(id.Groups[0].Value.Substring(1)); // skip the #

          if (!guards.Any(x => x.GuardId == activeGuard)) guards.Add(new Guard(activeGuard));
        }

        gs.GuardId = activeGuard;
      }
    }

    [DebuggerDisplay("{TimeStamp} {GuardId} {State}")]
    private class GuardShift
    {
      public GuardShift(string input)
      {
        TimeStamp = DateTime.Parse(input.Substring(1, 16));
        Description = input.Substring(19);
      }
      public DateTime TimeStamp { get; }
      public int GuardId { get; set; }
      public string Description { get; }

      public GuardState State
      {
        get
        {
          if (Description.Contains("begins")) return GuardState.begin;
          if (Description.Contains("asleep")) return GuardState.sleep;
          if (Description.Contains("wakes")) return GuardState.wake;
          return GuardState.none;
        }
      }
    }

    [DebuggerDisplay("{GuardId} {TimeAsleep} {SleepyMinuteCount}")]
    private class Guard
    {
      public Guard(int id)
      {
        GuardId = id;
        SleepMap = new int[60];
      }
      public int GuardId { get; set; }
      public int SleepyMinuteCount { get; set; }
      public int[] SleepMap { get; }
      public TimeSpan TimeAsleep { get; set; }
    }
    private enum GuardState
    {
      none,
      begin,
      wake,
      sleep
    }
  }
}