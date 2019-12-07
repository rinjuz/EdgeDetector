using System.Windows.Input;

namespace TrainingSetGenerator.ViewModels
{
    class MainVM : BaseVM
    {
        public MainVM()
        {
            Brightness = 0.5;
            Contrast = 0.5;
        }

        public string FilePath { get; set; }
        public string Resulition { get; set; }

        public double Brightness { get; set; }
        public double Contrast { get; set; }

        ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ?? (saveCommand = new DelegateCommand(SaveExecute, CanSaveExecute));

        bool CanSaveExecute() => !string.IsNullOrEmpty(FilePath);

        void SaveExecute()
        {

        }

        internal void SelectFile(string filePath)
        {
            FilePath = filePath;
            OnPropertyChanged(this, x => FilePath);
        }
    }
}
