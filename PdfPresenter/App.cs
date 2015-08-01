using System;
using System.Configuration;
using System.Windows;
using PdfPresenter.NonGuiCode;
using PdfPresenter.ViewModels;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    [STAThread]
    protected override void OnStartup(StartupEventArgs e)
    {
      try
      {
        var presentationSelectionView = new PresentationSelectionView(RunPresentation);
        presentationSelectionView.DataContext = new PresentationSelectionViewModel();
        presentationSelectionView.Show();
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message + " The application will exit now.");
        throw;
        //Shutdown(-1);
      }

    }

    private static void RunPresentation(string path)
    {
      var helperViewModel = new HelperViewModel(path);
      var presentationViewModel = new PresentationViewModel(path, 0, helperViewModel);

      var helper = new HelperView(new PresentationTime(helperViewModel))
      {
        DataContext = helperViewModel,
      };
      var presentationView = new PresentationView
      {
        DataContext = presentationViewModel
      };
      presentationView.Show();

      helper.Owner = presentationView;

      helper.Show();
      presentationView.FocusOnPdf();
    }
  }
}



