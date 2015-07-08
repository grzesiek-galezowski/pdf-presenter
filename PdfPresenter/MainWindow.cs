using System;
using System.Windows;
using System.Windows.Forms.Integration;
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

    protected override void OnSourceInitialized(EventArgs e)
    {
      base.OnSourceInitialized(e);
      HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
      source.AddHook(WndProc);
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      // Handle messages...
      //MessageBox.Show(msg.ToString());

      //bug handle messages from presenter
      return IntPtr.Zero;
    }

  }
}
