using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems.Day03
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

                var firstCompartment = rucksack[..size].ToList();

                var secondCompartment = rucksack[size..].ToList();

                var commonItem = firstCompartment.Intersect(secondCompartment).First();

                totalPriorities += GetScore(commonItem);
            }

            return totalPriorities;
        }

        public override object PartTwo()
        {
            var totalPriorities = 0;

            for (var i = 0; i < Rucksacks.Count; i += 3)
            {
                var rucksackOne = Rucksacks[i].Distinct();
                var rucksackTwo = Rucksacks[i + 1].Distinct();
                var rucksackThree = Rucksacks[i + 2].Distinct();

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
