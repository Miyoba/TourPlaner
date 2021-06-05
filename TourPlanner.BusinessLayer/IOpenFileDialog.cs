
namespace TourPlanner.BusinessLayer {
    public interface IOpenFileDialog {
        string Filter { get; set; }
        string FileName { get; set; }
        string Title { get; set; }
        bool? ShowDialog();
        
    }
}
