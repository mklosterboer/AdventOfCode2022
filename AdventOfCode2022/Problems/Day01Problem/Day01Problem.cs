using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    public class Day01Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<int> ElfCalories { get; set; }

        public Day01Problem()
        {
            var elves = new List<List<int>>(); 
            ElfCalories= new List<int>();

            var inputStrings = GetInputStringList();

            var currentSet = new List<int>();

            foreach(var inputString in inputStrings)
            {
                if(string.IsNullOrEmpty(inputString)) {
                    currentSet = new List<int>();
                    elves.Add(currentSet);
                }
                else if (Int32.TryParse(inputString, out int inputValue))
                {
                    currentSet.Add(inputValue);
                }
            }

            ElfCalories = elves.Select(x => x.Sum()).ToList();
        }

        public override object PartOne()
        {
           return ElfCalories.Max();
        }

        public override object PartTwo()
        {
            return ElfCalories.OrderByDescending(x => x).Take(3).Sum();
        }
    }
}
