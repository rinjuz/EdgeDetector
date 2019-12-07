using System;
using System.Numerics;
using System.Threading.Tasks;
using TrainingSetGenerator.ViewModels;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TrainingSetGenerator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly MainVM viewModel;

        public MainPage()
        {
            InitializeComponent();

            DataContext = viewModel = new MainVM();

            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }

        async void OnLoaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync("Sectors\\3.jpg") as IStorageFile;
            if (file != null) await LoadImage(file);
#endif
        }

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainCanvas.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, MainCanvas.ActualWidth, MainCanvas.ActualHeight)
            };
        }

        async void OpenFileDialog_ButtonClick(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            var file = await openPicker.PickSingleFileAsync();
            viewModel.SelectFile(file.Path);

            await LoadImage(file);
        }

        async Task LoadImage(IStorageFile file)
        {
            using (var fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();

                await bitmapImage.SetSourceAsync(fileStream);
                MainImage.Scale = Vector3.One;
                EdgesGrid.Scale = Vector3.One;
                MainImage.Source = bitmapImage;
            }
        }

        void MainCanvas_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            UIElement element = sender as UIElement;
            var matrix = IsCtrlPressed ? ScaleScene(element, e) : MoveScene(element, e);

            var newTransform = Matrix4x4.Multiply(MainImage.TransformMatrix, matrix);
            MainImage.TransformMatrix = newTransform;
            EdgesGrid.TransformMatrix = newTransform;
        }

        Matrix4x4 ScaleScene(UIElement sender, PointerRoutedEventArgs e)
        {
            var properties = e.GetCurrentPoint(sender).Properties;

            var scale = properties.MouseWheelDelta > 0 ? 1.05f : 0.95f;

            var pointer = e.GetCurrentPoint(MainImage);
            var point = new Vector3((float)pointer.Position.X, (float)pointer.Position.Y, 0);
            return Matrix4x4.CreateScale(scale, point);
        }

        Matrix4x4 MoveScene(UIElement sender, PointerRoutedEventArgs e)
        {
            var properties = e.GetCurrentPoint(sender).Properties;

            float shiftX = 0, shiftY = 0;
            if (properties.IsHorizontalMouseWheel)
                shiftX = -properties.MouseWheelDelta;
            else shiftY = properties.MouseWheelDelta;

            return Matrix4x4.CreateTranslation(shiftX, shiftY, 0);
        }

        bool IsCtrlPressed
        {
            get
            {
                var ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
                return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
            }
        }
    }
}
