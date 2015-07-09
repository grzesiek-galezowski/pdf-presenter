using System;
using System.IO;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private WindowsFormsHost _pdfControl;
    private readonly Slideshow _slideshow;

    public MainWindow(Slideshow mainSlideshow)
    {
      _slideshow = mainSlideshow;
      InitializeComponent();
    }


    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      _slideshow.OnKeyUpGoToNextSlide();
      _pdfControl = _slideshow.ToWindowsFormsHost();

      MainGrid.Children.Add(_pdfControl);
    }

    public void FocusOnPdf()
    {
      this.Focus();
      _pdfControl.Focus();
    }


    private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Down)
      {
        _slideshow.Advance();
      }
      else if (e.Key == Key.Up)
      {
        _slideshow.GoBackOneSlide();
      }
    }

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.OemPeriod)
      {
        _pdfControl.Visibility = (_pdfControl.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
      }
      else if (e.Key == Key.PageDown)
      {
        _slideshow.Advance();
      }
      else if (e.Key == Key.PageUp)
      {
        _slideshow.GoBackOneSlide();
      }
      else if (e.Key == Key.F5)
      {
        Close();
      }
      else if(e.Key == Key.Escape)
      {
        Close();
      }
    }
  }
}
