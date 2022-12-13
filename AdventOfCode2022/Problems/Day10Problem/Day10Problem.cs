using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems
{
    internal class Day10Problem : Problem
    {
        protected override string InputName => "Actual";

        private IEnumerable<string> Input { get; init; }

        private static readonly Regex InstructionPattern = new(@"^(?<operation>[a-z]{3,4})(\s(?<amount>-?\d*))?");

        public Day10Problem()
        {
            Input = GetInputStringList();
        }

        public override object PartOne()
        {
            var cpu = new CPU();

            foreach (var row in Input)
            {
                var instruction = InstructionPattern.Match(row).Groups;
                var operation = instruction["operation"].Value;

                switch (operation)
                {
                    case "addx":
                        var amount = int.Parse(instruction["amount"].Value);
                        cpu.AddX(amount);
                        break;
                    case "noop":
                        cpu.Noop();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return cpu.SignalStrengths.Sum();
        }

        public override object PartTwo()
        {
            // The CPU from part 1 is printing this out. 
            return "";
        }
    }

    internal class CPU
    {
        public List<int> SignalStrengths { get; set; }

        public int ClockCycle { get; private set; }

        public int RegisterX { get; private set; }

        private string CurrentCRTRow { get; set; }

        private int CRTRow { get; set; }

        public CPU()
        {
            SignalStrengths = new List<int>();
            ClockCycle = 0;
            RegisterX = 1;
            CRTRow = 0;
            CurrentCRTRow = string.Empty;
        }

        public void AddX(int amount)
        {
            Tick();
            Tick();
            RegisterX += amount;
        }

        public void Noop()
        {
            Tick();
        }

        private void Tick()
        {
            ClockCycle++;
            StoreSignalStrength();
            Draw();
        }

        private void StoreSignalStrength()
        {
            if ((ClockCycle - 20) % 40 == 0)
            {
                SignalStrengths.Add(ClockCycle * RegisterX);
            }
        }

        private void Draw()
        {
            // This seems hacky. Maybe find a better way to track the row?
            var rowAdjustedCycle = ClockCycle - 1 - (40 * CRTRow);
            if (RegisterX - 1 == rowAdjustedCycle || RegisterX == rowAdjustedCycle || RegisterX + 1 == rowAdjustedCycle)
            {
                CurrentCRTRow += "#";
            }
            else
            {
                CurrentCRTRow += ".";
            }

            if (ClockCycle % 40 == 0)
            {
                Console.WriteLine(CurrentCRTRow);
                CurrentCRTRow = string.Empty;
                CRTRow++;
            }
        }
    }
}
