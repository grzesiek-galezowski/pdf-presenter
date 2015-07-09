using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pri.LongPath;
using Path = Pri.LongPath.Path;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for PresentationSelection.xaml
  /// </summary>
  public partial class PresentationSelection : Window
  {
    private readonly Action<string> _runPresentation;

    public PresentationSelection(Action<string> runPresentation)
    {
      _runPresentation = runPresentation;
      InitializeComponent();
    }

    public void PresentButton_OnClick(object sender, RoutedEventArgs e)
    {
      try
      {
        var text = presentationPath.Text;
        //bug check whether path exists
        if (new FileInfo(text).Exists)
        {
          _runPresentation(text);
        }
        else
        {
          MessageBox.Show("Invalid path!");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public void ExitButton_OnClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    public void BrowseButton_OnClick(object sender, RoutedEventArgs e)
    {
      var fileDialog = new Microsoft.Win32.OpenFileDialog();
      fileDialog.Title = "Please select pdf file to present";
      fileDialog.ShowReadOnly = true;
      fileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
      var result = fileDialog.ShowDialog();
      switch (result)
      {
        case true:
          var file = fileDialog.FileName;
          presentationPath.Text = file;
          presentationPath.ToolTip = file;
          break;
        default:
          presentationPath.Text = null;
          presentationPath.ToolTip = null;
          break;
      }
    }
  }
}
