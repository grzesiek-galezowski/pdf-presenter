using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for PresentationView.xaml
  /// </summary>
  public partial class PresentationView : Window
  {
    private readonly Slideshow _slideshow;

    public PresentationView(Slideshow mainSlideshow)
    {
      _slideshow = mainSlideshow;
      InitializeComponent();
    }


    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      MainGrid.Children.Add(_slideshow.ToWindowsFormsHost());
    }

    public void FocusOnPdf()
    {
      this.Focus();
      _slideshow.Focus();
    }

    private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
    {
      Close();
    }
  }
}
