namespace AdventOfCode2022.Problems.Day07
{
    public class ElfDirectory
    {
        public string Name { get; set; }

        public ElfDirectory Parent { get; set; }

        public readonly List<ElfFile> Files = new();

        public readonly List<ElfDirectory> Directories = new();

        public ElfDirectory(string name, ElfDirectory parent)
        {
            Name = name;
            Parent = parent;
        }

        public void AddFile(ElfFile fileToAdd)
        {
            if (!Files.Where(f => f.Name == fileToAdd.Name).Any())
            {
                Files.Add(fileToAdd);
            }
        }

        public void AddDirectory(ElfDirectory directoryToAdd)
        {
            if (!Directories.Where(d => d.Name == directoryToAdd.Name).Any())
            {
                Directories.Add(directoryToAdd);
            }
        }

        public ElfDirectory GetSubDirectory(string name)
        {
            return Directories.Where(d => d.Name == name).FirstOrDefault();
        }

        public long GetSize()
        {
            return Files.Sum(f => f.Size) + Directories.Sum(d => d.GetSize());
        }

        public List<ElfDirectory> GetAllDirectories()
        {
            if (Directories.Count == 0)
            {
                return new List<ElfDirectory>() { this };
            }
            else
            {
                var result = Directories.SelectMany(d => d.GetAllDirectories()).ToList();

                result.Add(this);

                return result;
            }
        }

        public void Print(int offset)
        {
            var value = $"- {Name} (dir)";
            Console.WriteLine(value.PadLeft(offset + value.Length));

            Directories.ForEach(d =>
            {
                d.Print(offset + 2);
            });

            Files.ForEach(f =>
            {
                f.Print(offset + 2);
            });
        }
    }
}
