using AdventOfCode2022.Problems.Day02;
using AdventOfCode2022.Utilities;
using System.Diagnostics;

var stopwatch = Stopwatch.StartNew();

IProblem problem = new Day02Problem();

var runner = new Runner(problem);

runner.Run();

stopwatch.Stop();

Console.WriteLine($"Total runtime: {stopwatch.ElapsedMilliseconds:F10} ms");
