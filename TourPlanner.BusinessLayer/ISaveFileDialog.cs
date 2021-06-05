namespace TourPlanner.BusinessLayer {
    public interface ISaveFileDialog
    {
        string Filter { get; set; }
        bool? ShowDialog();
        string FileName { get; set; }
    }
}
