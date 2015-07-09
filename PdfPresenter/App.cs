using System;
using System.Configuration;
using System.Windows;
using PdfPresenter.NonGuiCode;

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
        new PresentationSelection(RunPresentation).Show();

        //string path = ConfigurationManager.AppSettings["path"];

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
      var currentSlide = new Slideshow(path, 0);
      var nextSlide = new Slideshow(path, 1);
      var mainSlideshow = new Slideshow(path);


      var helper = new HelperWindow(
        currentSlide,
        nextSlide
        );
      mainSlideshow.ReportSlideChangesTo(new BroadcastingSlideshowObserver(currentSlide, nextSlide));

      mainSlideshow.Load();
      currentSlide.Load();
      nextSlide.Load();

      var mainWindow = new MainWindow(mainSlideshow);
      mainWindow.Show();

      helper.Owner = mainWindow;
      helper.Show();

      mainWindow.FocusOnPdf();
    }

    protected override void OnExit(ExitEventArgs e)
    {
      base.OnExit(e);
    }
  }
}



