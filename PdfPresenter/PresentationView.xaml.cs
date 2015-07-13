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
  /// Interaction logic for PresentationView.xaml
  /// </summary>
  public partial class PresentationView : Window
  {
    private WindowsFormsHost _pdfControl;
    private readonly Slideshow _slideshow;

    public PresentationView(Slideshow mainSlideshow)
    {
      _slideshow = mainSlideshow;
      InitializeComponent();
    }


    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
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
        _slideshow.NextSlide();
      }
      else if (e.Key == Key.Up)
      {
        _slideshow.PreviousSlide();
      }
    }

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.OemPeriod)
      {
        ToggleVisibilityOf(_pdfControl);
      }
      else if (e.Key == Key.PageDown)
      {
        _slideshow.NextSlide();
      }
      else if (e.Key == Key.PageUp)
      {
        _slideshow.PreviousSlide();
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

    private void ToggleVisibilityOf(UIElement uiElement)
    {
      uiElement.Visibility = (uiElement.Visibility == Visibility.Visible) 
        ? Visibility.Hidden : Visibility.Visible;
    }
  }
}
