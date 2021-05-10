using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für EditTourWindow.xaml
    /// </summary>
    public partial class EditTourWindow : Window {
        public EditTourWindow(Tour tour)
        {
            InitializeComponent();
            this.DataContext = new EditTourViewModel(this, tour);
        }
    }
}
