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
    public PresentationView()
    {
      InitializeComponent();
    }

    public void FocusOnPdf()
    {
      this.Focus();
      FileView.Focus();
    }

    private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
    {
      Close();
    }

  }
}
