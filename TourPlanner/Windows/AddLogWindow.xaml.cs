using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für AddLogWindow.xaml
    /// </summary>
    public partial class AddLogWindow : Window {
        public AddLogWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = new AddLogViewModel(this, tour);
        }

        private void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
