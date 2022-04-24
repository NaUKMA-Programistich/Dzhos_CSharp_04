using Dzhos_CSharp_04.Models;
using Dzhos_CSharp_04.Repository;
using Dzhos_CSharp_04.Utils;
using Dzhos_CSharp_04.Utils.Extensions;
using Dzhos_CSharp_04.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Dzhos_CSharp_04.ViewModels
{
    class PersonsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;
        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _savePersonsCommand;
        
        private RelayCommand<object> _sortName;
        private RelayCommand<object> _sortSurname;
        private RelayCommand<object> _sortEmail;
        private RelayCommand<object> _sortBirthDay;

        private RelayCommand<object> _sortIsAdult;
        private RelayCommand<object> _sortIsBirthday;
        private RelayCommand<object> _sortSun;
        private RelayCommand<object> _sortChinese;
        #endregion

        #region Properties
        public Person SelectedPerson
        {
            get 
            { 
                return _selectedPerson; 
            }
            set
            {
                _selectedPerson = value;
            }
        }
        public ObservableCollection<Person> Persons
        {
            get 
            { 
                return _persons; 
            }
            private set
            {
                _persons = value;
                OnPropertyChanged("Persons");
            }
        }
        public RelayCommand<object> AddCommand
        {
            get { return _addPersonCommand ??= new RelayCommand<object>(AddPersonCommandProcess, _ => true); }
        }
        public RelayCommand<object> EditCommand
        {
            get { return _editPersonCommand ??= new RelayCommand<object>(EditPersonCommandProcess, _ => CanExecuteCommand()); }
        }
        public RelayCommand<object> DeletCommand
        {
            get { return _deletePersonCommand ??= new RelayCommand<object>(DeletePersonCommandProcess, _ => CanExecuteCommand()); }
        }
        public RelayCommand<object> SaveCommand
        {
            get { return _savePersonsCommand ??= new RelayCommand<object>(SavePersonsCommandProcess, _ => true ); }
        }
        public RelayCommand<object> SortNameCommand
        {
            get { return _sortName ??= new RelayCommand<object>(SortNameProcess, _ => true); }
        }
        public RelayCommand<object> SortSurnameCommand
        {
            get { return _sortSurname ??= new RelayCommand<object>(SortSurnameProcess, _ => true); }
        }
        public RelayCommand<object> SortEmailCommand
        {
            get { return _sortEmail ??= new RelayCommand<object>(SortEmailProcess, _ => true); }
        }
        public RelayCommand<object> SortBirthDayCommand
        {
            get { return _sortBirthDay ??= new RelayCommand<object>(SortBirthDayProcess, _ => true); }
        }
        public RelayCommand<object> SortIsAdultCommand
        {
            get { return _sortIsAdult ??= new RelayCommand<object>(SortIsAdultProcess, _ => true); }
        }
        public RelayCommand<object> SortIsBirthdayCommand
        {
            get { return _sortIsBirthday ??= new RelayCommand<object>(SortIsBirthDayProcess, _ => true); }
        }
        public RelayCommand<object> SortChineseSignCommand
        {
            get { return _sortChinese ??= new RelayCommand<object>(SortChineseSignProcess, _ => true); }
        }
        public RelayCommand<object> SortSunSignCommand
        {
            get { return _sortSun ??= new RelayCommand<object>(SortSunSignProcess, _ => true); }
        }
        #endregion

        private void AddPersonCommandProcess(object obj)
        {

            CrudPersonView window = new CrudPersonView();
            window.ShowDialog();
            Persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
        }

        private void EditPersonCommandProcess(object obj)
        {   

            CrudPersonView window = new CrudPersonView(SelectedPerson);
            window.ShowDialog();
            Persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
        }

        private async void DeletePersonCommandProcess(object obj)
        {
            await Task.Run(() =>
            {
                var message = MessageBox.Show($"Delete {_selectedPerson.Name}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (message == MessageBoxResult.Yes)
                {
                    StorageManager.Storage.RemovePerson(SelectedPerson);
                    SelectedPerson = null;
                    Persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
                }
            });
        }

        private async void SavePersonsCommandProcess(object obj)
        {
            await Task.Run(() =>
            {
                StorageManager.Storage.SaveInStorage();
            });
            MessageBox.Show("Save all");

        }

        private void SortNameProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Name select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }

        private void SortSurnameProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Surname select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }

        private void SortEmailProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Email select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }

        private void SortBirthDayProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.BirthDay select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }

        private void SortIsAdultProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.IsAdult select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortIsBirthDayProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.IsBirthday select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortChineseSignProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.ChineseSign select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortSunSignProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.SunSign select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }

        #region Constructors
        internal PersonsViewModel()
        {
            _persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
        }
        #endregion

        private bool CanExecuteCommand()
        {
            return _selectedPerson != null;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
