using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day03Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<string> Rucksacks { get; set; }

        public Day03Problem()
        {
            Rucksacks = GetInputStringList().ToList();
        }

        public override object PartOne()
        {
            var totalPriorities = 0;

            foreach (var rucksack in Rucksacks)
            {
                var size = rucksack.Length / 2;

                var compartmentOne = rucksack[..size];
                var compartmentTwo = rucksack[size..];

                var commonItem = compartmentOne
                    .Intersect(compartmentTwo)
                    .First();

                totalPriorities += GetScore(commonItem);
            }

            return totalPriorities;
        }

        public override object PartTwo()
        {
            var totalPriorities = 0;

            for (var i = 0; i < Rucksacks.Count; i += 3)
            {
                var rucksackOne = Rucksacks[i];
                var rucksackTwo = Rucksacks[i + 1];
                var rucksackThree = Rucksacks[i + 2];

                var commonItem = rucksackOne
                    .Intersect(rucksackTwo)
                    .Intersect(rucksackThree)
                    .First();

                totalPriorities += GetScore(commonItem);
            }

            return totalPriorities;
        }

        private static int GetScore(char item)
        {
            if (item <= 'Z')
            {
                // A-Z is 27-52
                // 'A' is 65, so subtract 38 to get 'A' to equal 27
                return (int)item - 38;
            }
            else
            {
                // a-z is 1-26
                // 'a' is 97, so subtract 96 to get 'a' to equal 1
                return (int)item - 96;
            }
        }
    }
}
