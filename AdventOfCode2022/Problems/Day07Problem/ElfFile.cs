using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems.Day07
{
    public class ElfFile
    {
        public string Name { get; init; }

        public ElfDirectory Parent { get; init; }

        public long Size { get; init; }

        private static readonly Regex Parser = new(@"(?<size>\d*)\s(?<name>.*)");

        public ElfFile(string line, ElfDirectory parent)
        {
            var match = Parser.Match(line);

            Name = match.Groups["name"].Value;
            Size = long.Parse(match.Groups["size"].Value);
            Parent = parent;
        }

        public void Print(int offset)
        {
            var value = $"- {Name} (file, size={Size})";
            Console.WriteLine(value.PadLeft(offset + value.Length));
        }
    }
}
