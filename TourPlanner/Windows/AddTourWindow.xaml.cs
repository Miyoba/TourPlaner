using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für AddTourWindow.xaml
    /// </summary>
    public partial class AddTourWindow : Window {
        public AddTourWindow()
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(this);
        }
    }
}
