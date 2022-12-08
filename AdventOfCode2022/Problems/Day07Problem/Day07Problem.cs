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

                if (line.StartsWith('$'))
                {
                    // This is a command
                    if (line.StartsWith("$ cd"))
                    {
                        var path = line[5..];
                        if (path == "/")
                        {
                            currentDirectory = root;
                        }
                        else if (path == "..")
                        {
                            currentDirectory = currentDirectory.Parent;
                        }
                        else
                        {
                            var newDirectoryName = line[5..];
                            currentDirectory = currentDirectory.GetSubDirectory(newDirectoryName);
                        }
                    }
                    else if (line == "$ ls")
                    {
                        // Don't really need this, but this is a no action for parsing the input.
                    }
                }
                else
                {
                    // This is a listing of a file or directory
                    if (line.StartsWith("dir"))
                    {
                        var newDirectoryName = line[4..];
                        currentDirectory.AddDirectory(new ElfDirectory(newDirectoryName, currentDirectory));
                    }
                    else
                    {
                        currentDirectory.AddFile(new ElfFile(line, currentDirectory));
                    }
                }

                lineNumber++;
            } while (lineNumber < lines.Count);

            return root;
        }
    }
}
