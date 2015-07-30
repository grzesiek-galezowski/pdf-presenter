using System.Windows;
using System.Windows.Controls;

namespace PdfFileViewControl
{
  /// <summary>
  /// Interaction logic for UserControl1.xaml
  /// </summary>
  public partial class PdfFileView : UserControl
  {
    private int _initialPage = 0;

    public int? InitialPage
    {
      get { return _initialPage; }
      set { _initialPage = value ?? 0; }
    }


    private static void OnInitialPageNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var pdf = (PdfFileView)d;
      var value = (int?)e.NewValue;
      pdf._initialPage = value ?? 1;
    }

    public PdfFileView()
    {
      InitializeComponent();
    }

    private void PdfFileView_OnLoaded(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Loaded!");
    }

    
  }
}
