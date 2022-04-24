using Dzhos_CSharp_04.Models;
using Dzhos_CSharp_04.Repository;
using Dzhos_CSharp_04.Utils;
using Dzhos_CSharp_04.Utils.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Dzhos_CSharp_04.ViewModels
{
    class CrudPersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = null;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;

        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _cancelCommand;
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }
        public Action CloseWindow { get; set; }
        public RelayCommand<object> SaveCommand
        {
            get { return _saveCommand ??= new RelayCommand<object>(SaveCommandProcess, _ => CanExecute()); }
        }
        public RelayCommand<object> CancelCommand
        {
            get { return _cancelCommand ??= new RelayCommand<object>(CancelCommandProcess, _ => true); }
        }
        #endregion

        #region Constructors
        internal CrudPersonViewModel(Person person)
        {
            Person = person;
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            Date = person.BirthDay;
        }
        internal CrudPersonViewModel()
        {

        }
        #endregion

        private void SaveCommandProcess(object obj)
        {
            try
            {
                var person = new Person(Name, Surname, Email, Date);
                if (Person == null)
                {
                    var textMsg = "Sure About Save Person?:\n" + person.ToString();
                    var message = MessageBox.Show(textMsg, "Save", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    StorageManager.Storage.AddPerson(person);
                    CloseWindow();
                }
                else
                {
                    var message = MessageBox.Show("Sure About Edit Person?", "Edit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (message == MessageBoxResult.Yes)
                    {
                        StorageManager.Storage.EditPerson(_person, person);
                        CloseWindow();
                    }

                }

            } catch (MyException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void CancelCommandProcess(object obj)
        {
            var message = MessageBox.Show("Sure About Cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(message == MessageBoxResult.Yes)
            {
                CloseWindow();
            }
        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Email)
                && Date != DateTime.MinValue;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
