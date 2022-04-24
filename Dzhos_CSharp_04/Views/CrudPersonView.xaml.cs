using Dzhos_CSharp_04.Models;
using Dzhos_CSharp_04.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Dzhos_CSharp_04.Views
{
    /// <summary>
    /// Interaction logic for BirthDayView.xaml
    /// </summary>
    public partial class CrudPersonView : Window
    {
        private readonly CrudPersonViewModel _viewModel;

        public CrudPersonView()
        {
            InitializeComponent();
            DataContext = _viewModel = new CrudPersonViewModel();
            if (_viewModel.CloseWindow == null)
            {
                _viewModel.CloseWindow = new Action(this.Close);
            }
               
        }

        public CrudPersonView(Person person)
        {
            InitializeComponent();
            DataContext = _viewModel = new CrudPersonViewModel(person);
            if (_viewModel.CloseWindow == null)
            {
                var window = Window.GetWindow(this);
                _viewModel.CloseWindow = new Action(window.Close);
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate != null)
            {
                _viewModel.Date = selectedDate.Value;
            }
        }
    }
}
