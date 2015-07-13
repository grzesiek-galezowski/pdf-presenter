using System;
using System.Windows;
using PdfPresenter.NonGuiCode;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for HelperView.xaml
  /// </summary>
  public partial class HelperView : Window
  {
    private readonly Slideshow _currentSlide; //TODO remove
    private readonly Slideshow _nextSlide;  //TODO remove
    private readonly Slideshow[] _slideshows;
    private readonly PresentationTime _presentationTime;

    public HelperView(Slideshow currentSlide, Slideshow nextSlide, PresentationTime presentationTime)
    {
      _currentSlide = currentSlide;
      _nextSlide = nextSlide;
      _slideshows = new[] {currentSlide, nextSlide};

      InitializeComponent();
      _presentationTime = presentationTime;
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      try
      {
        HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
        HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());
        _presentationTime.StartMeasuring();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error in OnLoaded: " + ex.ToString());
      }
    }

    private void HelperWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      try
      {
        foreach (var slideshow in _slideshows)
        {
          slideshow.Refresh();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error in OnSizeChanged: " + ex.ToString());
        throw;
      }

    }

  }
}
