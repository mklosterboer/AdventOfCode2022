using AdventOfCode2022.Problems.Day07;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day07Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<string> Lines { get; init; }

        private const long SYSTEM_SIZE = 70000000;
        private const long SPACE_NEEDED = 30000000;

        public Day07Problem()
        {
            Lines = GetInputStringList().ToList();
        }

        public override object PartOne()
        {
            return GetRoot(Lines)
                .GetAllDirectories()
                .Select(d => d.GetSize())
                .Where(s => s <= 100000)
                .Sum();
        }

        public override object PartTwo()
        {
            var root = GetRoot(Lines);

            var totalSize = root.GetSize();
            var spaceNeeded = SPACE_NEEDED - (SYSTEM_SIZE - totalSize);

            return root
                .GetAllDirectories()
                .Select(d => d.GetSize())
                .Where(s => s >= spaceNeeded)
                .OrderBy(x => x)
                .First();
        }

        private static ElfDirectory GetRoot(List<string> lines)
        {
            var root = new ElfDirectory("/", null);

            var currentDirectory = root;
            var lineNumber = 0;

            do
            {
                var line = lines[lineNumber];

                switch (line.Split(" "))
                {
                    case ["$", "ls"]:
                        break;
                    case ["$", "cd", "/"]:
                        currentDirectory = root;
                        break;
                    case ["$", "cd", ".."]:
                        currentDirectory = currentDirectory.Parent;
                        break;
                    case ["$", "cd", var name]:
                        currentDirectory = currentDirectory.GetSubDirectory(name);
                        break;
                    case ["dir", var name]:
                        currentDirectory.AddDirectory(new ElfDirectory(name, currentDirectory));
                        break;
                    case [_, _]:
                        currentDirectory.AddFile(new ElfFile(line, currentDirectory));
                        break;
                }

                lineNumber++;
            } while (lineNumber < lines.Count);

            return root;
        }
    }
}
