using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PdfPresenter
{
  public interface PresentationDurationObserver
  {
    void NotifyOnTimePassed(TimeSpan time);
  }

  /// <summary>
  /// Interaction logic for HelperWindow.xaml
  /// </summary>
  public partial class HelperWindow : Window, PresentationDurationObserver
  {
    private readonly Slideshow _currentSlide; //TODO remove
    private readonly Slideshow _nextSlide;  //TODO remove
    private readonly Slideshow[] _slideshows;
    private readonly PresentationTime _presentationTime;

    public HelperWindow(Slideshow currentSlide, Slideshow nextSlide)
    {
      _currentSlide = currentSlide;
      _nextSlide = nextSlide;
      _slideshows = new[] {currentSlide, nextSlide};

      InitializeComponent();
      _presentationTime = new PresentationTime(this);
    }

    public void NotifyOnTimePassed(TimeSpan time)
    {
      var realCurrentPage = _currentSlide.CurrentPage + 1;
      TimeSinceStart.Text = time.ToString("c").Substring(0, 8);
      SlideProgression.Text = realCurrentPage + " / " + _currentSlide.TotalPages;
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      try
      {
        foreach (var slideshow in _slideshows)
        {
          slideshow.Load();
        }

        HelpCurrentSlide.Children.Add(_currentSlide.ToWindowsFormsHost());
        HelpNextSlide.Children.Add(_nextSlide.ToWindowsFormsHost());
        _presentationTime.StartMeasuring();

      }
      catch (Exception ex)
      {
        MessageBox.Show("Error in OnLoaded: " + ex.ToString());
        throw;
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
