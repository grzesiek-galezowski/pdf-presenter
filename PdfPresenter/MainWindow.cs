using System;
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
      // Create the interop host control.
      _slideshow.Load();
      _slideshow.OnKeyUpGoToNextSlide();
      _pdfControl = _slideshow.ToWindowsFormsHost();

      MainGrid.Children.Add(_pdfControl);

    }

    public void FocusOnPdf()
    {
      this.Focus();
      _pdfControl.Focus();
    }

    private void PreviousTrackCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Previous track");
    }

    private void NextTrackCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Next track");
    }

    private void TogglePlayPauseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Play-Pause");
    }

    private void DecreaseVolumeCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Volume down");
    }

    private void IncreaseVolumeCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      MessageBox.Show("Volume up");
    }
  }
}
