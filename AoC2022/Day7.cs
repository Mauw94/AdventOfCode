using AoC.Shared;
using System;
namespace AoC2022
{
    class Directory
    {
        public string Name { get; set; }
        public Directory Parent { get; set; }
        public List<Directory> SubDirectories { get; set; }
        public List<(string name, int size)> Files { get; set; }

        public bool IsRoot
        {
            get
            {
                return Parent == null;
            }
        }

        public Directory(Directory parent, string name)
        {
            Parent = parent;
            Name = name;
            SubDirectories = new();
            Files = new();
        }

        public Directory Up() => IsRoot ? this : this.Parent;

        public Directory Down(string dirName) => dirName.Equals("/")
            ? this
            : SubDirectories.First(x => x.Name.Equals(dirName));

        public long GetDirectoryFileSize()
        {
            return SubDirectories.Any()
                ? SubDirectories.Select(x => x.GetDirectoryFileSize()).Sum() + Files.Select(f => f.size).Sum()
                : Files.Select(f => f.size).Sum();
        }

        public IEnumerable<long> Traverse() // breadth-first searching
        {
            Queue<Directory> queue = new Queue<Directory>(this.SubDirectories);

            while (queue.Count > 0)
            {
                var curDir = queue.Dequeue();
                yield return curDir.GetDirectoryFileSize();

                if (curDir.SubDirectories.Count > 0)
                    foreach (var dir in curDir.SubDirectories)
                        queue.Enqueue(dir);
            }
        }
    }

    public class Day7 : BaseDay
    {
        private Directory _fileSystem;
        // private List<Directory> _directories = new(); 
        // can also be used to calculate the sizes of directories 
        // -> but Traverse() is cleaner since we alrdy made a TreeNode structure

        public Day7(int day, int year, bool isTest) : base(day, year, isTest) { CreateDirectoryStructure(); }

        public Day7(int day, int year) : base(day, year) { CreateDirectoryStructure(); }

        public override object SolvePart1()
        {
            var traverseFileSize = _fileSystem.Traverse()
                .Where(x => x < 100000)
                .Sum();

            return traverseFileSize;
        }

        public override object SolvePart2()
        {
            var rootDir = _fileSystem.GetDirectoryFileSize(); // root dir size
            var allDir = _fileSystem.Traverse();
            var filesystem = 70000000;
            var unusedspace = 30000000;

            var spaceRequired = unusedspace - (filesystem - rootDir);

            return allDir.Where(x => x > spaceRequired).Min();
        }

        private void CreateDirectoryStructure()
        {
            _fileSystem = new(null, "/");
            var current = _fileSystem;
            // _directories.Add(current);

            foreach (var line in FileContent)
            {
                if (line.StartsWith("$ cd"))
                {
                    var part = line.Split(' ')[2];
                    current = part.Equals("..") ? current.Up() : current.Down(part);
                    continue;
                }

                if (line.StartsWith("$ ls"))
                    continue;

                if (line.StartsWith("dir "))
                {
                    var dir = new Directory(current, line.Split(" ")[1]);
                    // _directories.Add(dir);
                    current.SubDirectories.Add(dir);
                }
                else
                {
                    var parts = line.Split(" ");
                    current.Files.Add((parts[1], int.Parse(parts[0])));
                }
            }
        }
    }
}
