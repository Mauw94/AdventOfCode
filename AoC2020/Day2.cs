using AoC.Shared;

namespace AoC2020
{
    public class Day2 : BaseDay
    {
        private int _min;
        private int _max;
        private char _character;
        private string _password;

        public Day2(int day, int year, bool isTest) : base(day, year, isTest) { }
        public Day2(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var validPasswords = 0;

            foreach (var line in FileContent)
            {
                if (IsValid(line))
                    validPasswords++;
            }

            return validPasswords;
        }

        public override object SolvePart2()
        {
            var validPasswords = 0;

            foreach (var line in FileContent)
                if (IsValidPart2(line))
                    validPasswords++;

            return validPasswords;
        }

        private bool IsValid(string line)
        {
            Parse(line);

            Dictionary<char, int> chars = new();

            foreach (var c in _password)
            {
                if (!chars.ContainsKey(c))
                    chars.Add(c, 0);

                chars[c]++;
            }

            if (!chars.ContainsKey(_character)) return false;

            return chars[_character] >= _min && chars[_character] <= _max;
        }

        private bool IsValidPart2(string line)
        {
            Parse(line);
            var contains = 0;

            for (int i = 0; i < _password.Length; i++)
            {
                if (_min - 1 == i && _password[i] == _character)
                    contains++;

                if (_max - 1 == i && _password[i] == _character)
                    contains++;
            }

            return contains == 1 ? true : false;
        }

        private void Parse(string line)
        {
            var index = line.IndexOf(":") + 2;
            var originalindex = index - 2;
            var checks = line[..originalindex].Split(" ");
            var numbers = checks[0].Split("-");

            _password = line[index..];
            _min = int.Parse(numbers[0]);
            _max = int.Parse(numbers[1]);
            _character = char.Parse(checks[1]);
        }
    }
}
