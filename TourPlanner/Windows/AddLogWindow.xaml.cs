using System.Windows;
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
    }
}
