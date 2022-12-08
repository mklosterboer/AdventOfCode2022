using AdventOfCode2022.Problems.Day07;
using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

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

        private static readonly Regex ChangeToRootPattern = new(@"\$ cd \/");
        private static readonly Regex ChangeToParentPattern = new(@"\$ cd \.\.");
        private static readonly Regex ChangeToDirectoryPattern = new(@"\$ cd .*");
        private static readonly Regex ListPattern = new(@"\$ ls");
        private static readonly Regex DirectoryPattern = new(@"dir .*");
        private static readonly Regex FilePattern = new(@"(\d*) (.*)");

        private static ElfDirectory GetRoot(List<string> lines)
        {
            var root = new ElfDirectory("/", null);

            var currentDirectory = root;
            var lineNumber = 0;
            do
            {
                var line = lines[lineNumber];

                switch (line)
                {
                    case var x when ListPattern.IsMatch(x):
                        break;
                    case var x when ChangeToRootPattern.IsMatch(x):
                        currentDirectory = root;
                        break;
                    case var x when ChangeToParentPattern.IsMatch(x):
                        currentDirectory = currentDirectory.Parent;
                        break;
                    case var x when ChangeToDirectoryPattern.IsMatch(x):
                        currentDirectory = currentDirectory.GetSubDirectory(line[5..]);
                        break;
                    case var x when DirectoryPattern.IsMatch(x):
                        currentDirectory.AddDirectory(new ElfDirectory(line[4..], currentDirectory));
                        break;
                    case var x when FilePattern.IsMatch(x):
                        currentDirectory.AddFile(new ElfFile(line, currentDirectory));
                        break;
                    default: break;
                }

                lineNumber++;
            } while (lineNumber < lines.Count);

            return root;
        }
    }
}
