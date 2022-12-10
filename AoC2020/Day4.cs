using System.Text.RegularExpressions;
using AoC.Shared;

namespace AoC2020
{
    public class Day4 : BaseDay
    {
        private List<string> _fields = new();

        public Day4(int day, int year, bool isTest) : base(day, year, isTest) { FillFields(); }

        public Day4(int day, int year) : base(day, year) { FillFields(); }

        public Day4(int day, int year, string fileName) : base(day, year) { FillFields(); }

        public override object SolvePart1()
        {
            var passwords = ParsePasswords();
            var validPasswords = 0;

            foreach (var password in passwords)
                if (IsValidPassword(password))
                    validPasswords++;

            return validPasswords;
        }

        public override object SolvePart2()
        {
            var passwords = ParsePasswords();
            var validPasswords = 0;

            foreach (var password in passwords)
                if (IsValidPasswordP2(password))
                    validPasswords++;

            return validPasswords;
        }

        private List<string> ParsePasswords()
        {
            var passwords = new List<string>();
            var password = string.Empty;

            for (int i = 0; i < FileContent.Count; i++)
            {
                if (FileContent[i] == string.Empty)
                {
                    passwords.Add(password);
                    password = string.Empty;
                }
                password += " " + FileContent[i];

                if (i == FileContent.Count - 1)
                    passwords.Add(password);
            }

            return passwords;
        }

        private bool IsValidPassword(string password)
        {
            if (_fields.All(x => password.Contains(x)))
                return true;
            return false;
        }

        private bool IsValidPasswordP2(string password)
        {
            foreach (var field in _fields)
            {
                if (!password.Contains(field))
                    return false;

                if (field.Equals("byr"))
                    if (!CheckBirthYear(password, field)) return false;
                if (field.Equals("iyr"))
                    if (!CheckIssueYear(password, field)) return false;
                if (field.Equals("eyr"))
                    if (!CheckExpirationYear(password, field)) return false;
                if (field.Equals("hgt"))
                    if (!CheckHeight(password, field)) return false;
                if (field.Equals("hcl"))
                    if (!CheckHairColor(password, field)) return false;
                if (field.Equals("ecl"))
                    if (!CheckEyeColor(password, field)) return false;
                if (field.Equals("pid"))
                    if (!CheckPasswordId(password, field)) return false;
            }

            return true;
        }

        private bool CheckBirthYear(string password, string field)
        {
            var index = password.IndexOf(field);
            var toCheck = password.Substring(index + field.Length + 1, 4);
            var year = int.Parse(toCheck);

            return year >= 1920 && year <= 2002;
        }

        private bool CheckIssueYear(string password, string field)
        {
            var index = password.IndexOf(field);
            var toCheck = password.Substring(index + field.Length + 1, 4);
            var year = int.Parse(toCheck);

            return year >= 2010 && year <= 2020;
        }

        private bool CheckExpirationYear(string password, string field)
        {
            var index = password.IndexOf(field);
            var toCheck = password.Substring(index + field.Length + 1, 4);
            var year = int.Parse(toCheck);

            return year >= 2020 && year <= 2030;
        }

        private bool CheckHeight(string password, string field)
        {
            var fields = password.Split(' ');
            var heightField = fields.First(x => x.StartsWith("hgt"));
            var height = heightField.Split(':')[1];
            var len = int.Parse(Regex.Match(height, @"\d+").Value);
            var unit = height.Substring(height.Length - 2);

            if (unit.Equals("cm"))
                return len >= 150 && len <= 193;
            if (unit.Equals("in"))
                return len >= 59 && len <= 76;

            return false;
        }

        private bool CheckHairColor(string password, string field)
        {
            var fields = password.Split(' ');
            var hclField = fields.First(x => x.StartsWith("hcl"));
            var chars = hclField.Split('#')[1];

            if (chars.Length == 6
                && chars.All(x => char.IsDigit(x) || char.IsLower(x)))
                return true;
            return false;
        }

        private bool CheckEyeColor(string password, string field)
        {
            var matches = new List<string>()
                {
                    "amb",
                    "blu",
                    "brn",
                    "gry",
                    "grn",
                    "hzl",
                    "oth"
                };
            var fields = password.Split(' ');
            var eclField = fields.First(x => x.StartsWith("ecl"));
            var color = eclField.Split(':')[1];

            if (matches.Contains(color)) return true;
            return false;
        }

        private bool CheckPasswordId(string password, string field)
        {
            var fields = password.Split(' ');
            var pidField = fields.First(x => x.StartsWith("pid"));
            var numbers = pidField.Split(':')[1];

            if (numbers.All(x => char.IsDigit(x))) return true;
            return false;
        }

        private void FillFields()
        {
            _fields = new()
                {
                    "byr",
                    "iyr",
                    "eyr",
                    "hgt",
                    "hcl",
                    "ecl",
                    "pid",
                    // "cid"
                };
        }
    }
}