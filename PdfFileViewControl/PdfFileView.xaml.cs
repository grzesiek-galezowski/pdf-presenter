using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using PdfiumViewer;

namespace PdfFileViewControl
{
  public partial class PdfFileView : UserControl
  {
    public static readonly DependencyProperty FileProperty = PdfFileViewProperties.CreateFileProperty<string>();
    public static readonly DependencyProperty PageProperty = PdfFileViewProperties.CreatePageProperty<int>(OnPageNumberChanged);
    public static readonly DependencyProperty TotalPagesProperty = PdfFileViewProperties.CreateTotalPagesProperty<int>();

    readonly PdfRenderer _pdfRenderer;
    readonly WindowsFormsHost _windowsFormsHost;
    int _cachedPageForErrorWorkaround;


    public string File
    {
      get { return (string) GetValue(FileProperty); }
      set { SetValue(FileProperty, value); }
    }

    public int Page
    {
      get { return (int) GetValue(PageProperty); }
      set { SetValue(PageProperty, value); }
    }

    public int TotalPages
    {
      get { return (int) GetValue(TotalPagesProperty); }
      set
      {
        SetValue(TotalPagesProperty, value);
        HideIfCurrentSlideIsBeyondPageRange();
      }
    }

    public PdfFileView()
    {
      InitializeComponent();
      _pdfRenderer = new PdfRenderer();
      _windowsFormsHost = new WindowsFormsHost
      {
        Child = _pdfRenderer
      };
      MainGrid.Children.Add(_windowsFormsHost);
    }

    void PdfFileView_OnLoaded(object sender, RoutedEventArgs e)
    {

      var pdfDocument = PdfDocument.Load(File);
      if (pdfDocument.PageCount == 0)
      {
        throw new PresentationLoadResultedInZeroPageDocumentException(File);
      }

      TotalPages = pdfDocument.PageCount;
      _pdfRenderer.Load(pdfDocument);

      AsSoonAsWinformsHostLoadsShowSlide(Page, _pdfRenderer);
    }

    void AsSoonAsWinformsHostLoadsShowSlide(int startingSlide, PdfRenderer pdfRenderer)
    {
      _windowsFormsHost.Loaded += (sender, args) =>
      {
        pdfRenderer.Page = startingSlide;
      };
    }

    static void OnPageNumberChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      var view = (PdfFileView)dependencyObject;
      var pageIndex = (int)dependencyPropertyChangedEventArgs.NewValue;
      view.SavePageIndex(pageIndex);
      view.HideIfCurrentSlideIsBeyondPageRange();
    }

    void SavePageIndex(int pageIndex)
    {
      _pdfRenderer.Page = pageIndex;
      _cachedPageForErrorWorkaround = pageIndex;
    }

    /// <summary>
    /// This is for helper slideshows that can be one slide ahead the normal slideshow.
    /// For such slideshows, going one index beyond page range is allowed.
    /// If this happens, we want to hide such slideshow.
    /// </summary>
    void HideIfCurrentSlideIsBeyondPageRange()
    {
      if (IsBeyondDocumentPageRange(_cachedPageForErrorWorkaround))
      {
        Visibility = Visibility.Hidden;
      }
      else
      {
        Visibility = Visibility.Visible;
      }
    }

    bool IsBeyondDocumentPageRange(int pageIndex)
    {
      return pageIndex >= TotalPages;
    }

    void PdfFileView_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      CenterOnCurrentPage();
    }

    void CenterOnCurrentPage()
    {
      //previously, I though I could write _pdfRenderer.Page = _pdfRenderer.Page;
      //but resizing from min to max caused the _pdfRenderer.Page to magically flip to next/prev
      _pdfRenderer.Page = _cachedPageForErrorWorkaround;
    }
  }
}

