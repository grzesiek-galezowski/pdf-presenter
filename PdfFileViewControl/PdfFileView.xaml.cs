using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using PdfiumViewer;

namespace PdfFileViewControl
{
  /// <summary>
  /// Interaction logic for UserControl1.xaml
  /// </summary>
  public partial class PdfFileView : UserControl
  {
    private readonly int _slideOffset;
    private readonly PdfRenderer _pdfRenderer;
    private WindowsFormsHost _windowsFormsHost;
    private readonly string _path;
    private int _totalPages = -1;


    private int _page = 0;

    public int? Page
    {
      get { return _page; }
      set { _page = value ?? 0; }
    }

    public string File { get; set; }


    private static void OnInitialPageNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var pdf = (PdfFileView) d;
      var value = (int?) e.NewValue;
      pdf._page = value ?? 1;
    }

    public PdfFileView()
    {
      InitializeComponent();
    }

    private void PdfFileView_OnLoaded(object sender, RoutedEventArgs e)
    {
      var pdfDocument = PdfDocument.Load(File);
      if (pdfDocument.PageCount == 0)
      {
        throw new PresentationLoadResultedInZeroPageDocumentException(_path);
      }
      _totalPages = pdfDocument.PageCount;
      _pdfRenderer.Load(pdfDocument);

      _windowsFormsHost = new WindowsFormsHost
      {
        Child = _pdfRenderer
      };

      AsSoonAsWinformsHostLoadsShowSlide(_slideOffset, _pdfRenderer);
    }

    private void AsSoonAsWinformsHostLoadsShowSlide(int startingSlide, PdfRenderer pdfRenderer)
    {
      _windowsFormsHost.Loaded += (sender, args) =>
      {
        pdfRenderer.Page = startingSlide;
        //_slideshowObserver.NotifySlideChangedTo(CurrentPage, TotalPages);
      };
    }
  }

  //bug replace this with real exception
  internal class PresentationLoadResultedInZeroPageDocumentException : Exception
  {
    public PresentationLoadResultedInZeroPageDocumentException(string path)
    {
      throw new NotImplementedException();
    }
  }
}

