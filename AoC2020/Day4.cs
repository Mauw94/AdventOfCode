using AoC.Shared;

namespace AoC2020
{
    public class Day4 : BaseDay
    {
        private List<string> _fields = new();

        public Day4(int day, int year, bool isTest) : base(day, year, isTest) { FillFields(); }

        public Day4(int day, int year) : base(day, year) { FillFields(); }

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
                password += FileContent[i];

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

                return field switch
                {
                    "byr" => CheckBirthYear(password, field),
                    "iyr" => CheckIssueYear(password, field),
                    "eyr" => CheckExpirationYear(password, field),
                    "hgt" => CheckHeight(password, field),
                    
                    _ => throw new ArgumentException()
                };
            }
            return false;
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