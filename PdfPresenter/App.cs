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


      var helperViewModel = new HelperViewModel();

      var helper = new HelperView(
        currentSlide,
        nextSlide, new PresentationTime(helperViewModel))
      {
        DataContext = helperViewModel
      };

      mainSlideshow.ReportSlideChangesTo(All(currentSlide, nextSlide, helperViewModel));

      mainSlideshow.Load();
      currentSlide.Load();
      nextSlide.Load();

      var presentationView = new PresentationView(mainSlideshow);
      presentationView.DataContext = new PresentationViewModel(mainSlideshow);
      presentationView.Show();

      helper.Owner = presentationView;

      helper.Show();

      presentationView.FocusOnPdf();
    }

    private static BroadcastingSlideshowObserver All(params PresentationProgressObserver[] observers)
    {
      return new BroadcastingSlideshowObserver(observers);
    }
  }
}



