using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für EditLogWindow.xaml
    /// </summary>
    public partial class EditLogWindow : Window {
        public EditLogWindow(Tour tour, TourLog log)
        {
            InitializeComponent();
            this.DataContext = new EditLogViewModel(this, tour, log);
        }
    }
}
