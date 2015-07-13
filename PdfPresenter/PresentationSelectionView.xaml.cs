using System;
using System.Windows;
using Microsoft.Win32;
using Pri.LongPath;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for PresentationSelectionView.xaml
  /// </summary>
  public partial class PresentationSelectionView : Window
  {
    private readonly Action<string> _runPresentation;

    public PresentationSelectionView(Action<string> runPresentation)
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
      var fileDialog = new OpenFileDialog();
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
