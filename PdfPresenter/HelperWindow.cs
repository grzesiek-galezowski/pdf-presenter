using System.Windows;
using System.Windows.Media;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for HelperWindow.xaml
  /// </summary>
  public partial class HelperWindow : Window
  {
    private readonly Slideshow _currentSlide; //TODO remove
    private readonly Slideshow _nextSlide;  //TODO remove
    private readonly Slideshow[] _slideshows;

    public HelperWindow(Slideshow currentSlide, Slideshow nextSlide)
    {
      _currentSlide = currentSlide;
      _nextSlide = nextSlide;
      _slideshows = new[] {currentSlide, nextSlide};

      InitializeComponent();
      this.Background = new SolidColorBrush(Colors.Black);
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      foreach (var slideshow in _slideshows)
      {
        slideshow.Load();
      }

      HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
      HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());
    }

    private void HelperWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      foreach (var slideshow in _slideshows)
      {
        slideshow.Refresh();
      }
    }
  }
}
