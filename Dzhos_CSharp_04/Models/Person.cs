using Dzhos_CSharp_04.Utils.Extensions;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Dzhos_CSharp_04.Models
{
    public class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private DateTime _birthDay;
        private string _email;
        private int _age;

        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly string _sunSign;
        private readonly string _chineseSign;

        private readonly string[] chineseZodiacArray =
        { "Monkey", "Rooster", "Dog",
          "Pig", "Rat", "Ox",
          "Tiger", "Rabbit", "Dragon",
          "Snake", "Horse", "Sheep"
        };
        #endregion

        #region Properties
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDay { get; private set; }
        public string Email 
        {
            get
            {
                return _email;
            }
            private set
            {
                if (IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new EmailException("Not Valid Email");
                }
            } 
        }

        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public int Age { get; private set; }
        public bool IsAdult { get; private set; }
        public bool IsBirthday { get; private set; }
        public string SunSign { get; private set; }
        public string ChineseSign { get; private set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public Person(string name, string surname, string email, DateTime birthDay)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDay = birthDay;
            Age = CalculateAge();

            ChineseSign = ChineseZodiac();
            SunSign = SunZodiac();
            IsBirthday = IsTodayBirthDay();
            IsAdult = Age >= 18;
        }
        Person(string firstName, string surName, string email)
            : this(firstName, surName, email, DateTime.Today) { }
        Person(string firstName, string surName, DateTime birthDay)
            : this(firstName, surName, "test@gmail.com", birthDay) { }
        #endregion

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDay.Year;
            if (BirthDay.Date > today.AddYears(-age)) age--;
            if (age < 0) throw new FeatureBirthDayException("Not Valid Birth Day(Future?)");
            if (age > 135) throw new OldBirthDayException("Not Valid Birth Day(So old)");
            return age;
        }
        private bool IsTodayBirthDay()
        {
            var today = DateTime.Today;
            return BirthDay.Month == today.Month && BirthDay.Day == today.Day;
        }
        private string ChineseZodiac()
        {
            var year = BirthDay.Year;
            return chineseZodiacArray[year % 12];
        }
        private string SunZodiac()
        {
            var day = BirthDay.Day;
            var month = BirthDay.Month;
            switch (month)
            {
                case 1:
                    if (day < 20) return "Capricorn";
                    else return "Aquarius";
                case 2:
                    if (day < 19) return "Aquarius";
                    else return "Pisces";
                case 3:
                    if (day < 21) return "Pisces";
                    else return "Aries";

                case 4:
                    if (day < 20) return "Aries";
                    else return "Taurus";
                case 5:
                    if (day < 21) return "Taurus";
                    else return "Gemini";
                case 6:
                    if (day < 21) return "Gemini";
                    else return "Cancer";

                case 7:
                    if (day < 23) return "Cancer";
                    else return "Leo";
                case 8:
                    if (day < 23) return "Leo";
                    else return "Virgo";
                case 9:
                    if (day < 23) return "Virgo";
                    else return "Libra";

                case 10:
                    if (day < 23) return "Libra";
                    else return "Scorpio";
                case 11:
                    if (day < 22) return "Scorpio";
                    else return "Sagittarius";
                case 12:
                    if (day < 22) return "Sagittarius";
                    else return "Capricorn";
                default: return "Error";
            }
        }
        public bool ValidBirthday()
        {
            if (Age < 0) return false;
            if (Age > 135) return false;
            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.AppendLine($"Your name: {Name}");
            builder.AppendLine($"Your surname: {Surname}");
            builder.AppendLine($"Your email: {Email}");
            builder.AppendLine($"Your birthday: {BirthDay.Day}.{BirthDay.Month}.{BirthDay.Year}");
            builder.AppendLine($"Your chinese sign: {ChineseSign}");
            builder.AppendLine($"Your sun sign: {SunSign}");
            builder.AppendLine($"You adult? {IsAdult}");
            if (IsBirthday)
            {
                builder.AppendLine("Congratulations on your birthday");
            }
            builder.AppendLine($"Your age: {Age}");
            return builder.ToString();
        }
    }
}
