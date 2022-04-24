using Dzhos_CSharp_04.Models;
using Dzhos_CSharp_04.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dzhos_CSharp_04.Repository
{
    class Storage
    {
        #region Fields
        private List<Person> _persons;
        private static readonly string StartFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Dzhos");
        private static readonly string BaseFile = Path.Combine(StartFolder, "Persons.txt");
        #endregion

        #region Constructor
        internal Storage()
        {
            if (!File.Exists(BaseFile))
            {
                if (!Directory.Exists(StartFolder))
                {
                    Directory.CreateDirectory(StartFolder);
                }
                var random = new Random();
                _persons = new List<Person>();
                for (int i = 0; i < 50; i++)
                {
                    var name = "Name" + i;
                    var surname = "SurName" + i;
                    var email = "email" + i + "@gmail.com";
                    var birthDay = new DateTime(random.Next(1940, 2021), random.Next(1, 13), random.Next(1, 27));
                    var person = new Person(name, surname, email, birthDay);
                    _persons.Add(person);
                }
                SaveInStorage(); 
            }
            else
            {
                _persons = GetAllPersonFromStorage();
            }
        }
        #endregion

        public List<Person> GetAllPersonFromStorage()
        {
            string stringObj = null;
            using (StreamReader sw = new StreamReader(BaseFile))
            {
                stringObj = sw.ReadToEnd();
            }
            return JsonSerializer.Deserialize<List<Person>>(stringObj);
        }

        public void SaveInStorage()
        {
            var stringObj = JsonSerializer.Serialize(_persons);
            using (StreamWriter sw = File.CreateText(BaseFile))
            {
                sw.Write(stringObj);
            };
        }

        public void EditPerson(Person previos, Person next)
        {
            _persons[_persons.IndexOf(previos)] = next;
            //SaveInStorage();
        }

        public void AddPerson(Person next)
        {
            _persons.Add(next);
            //SaveInStorage();
        }

        public void RemovePerson(Person person)
        {
            _persons.Remove(person);
            //SaveInStorage();
        }

        public List<Person> PersonsList
        {
            get
            {
                return _persons.ToList();

            }
            set
            {
                _persons = value;

            }
        }

        public static string StartFolder1 => StartFolder;
    }
}
