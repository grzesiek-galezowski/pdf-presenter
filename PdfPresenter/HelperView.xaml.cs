using System;
using System.Windows;
using PdfPresenter.NonGuiCode;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for HelperView.xaml
  /// </summary>
  public partial class HelperView : Window, PresentationProgressObserver
  {
    private readonly PresentationTime _presentationTime;
    private int _currentSlide = 0;

    public HelperView(PresentationTime presentationTime)
    {
      InitializeComponent();
      _presentationTime = presentationTime;
    }

    private void HelperWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
      try
      {
        _presentationTime.StartMeasuring();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error in OnLoaded: " + ex.ToString());
      }
    }

    public void NotifySlideChangedTo(int page, int totalPages)
    {
      _currentSlide = page;
    }
  }
}
