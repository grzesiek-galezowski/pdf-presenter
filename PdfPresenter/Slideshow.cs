using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using PdfiumViewer;
using PdfPresenter.Exceptions;
using PdfPresenter.NonGuiCode;

namespace PdfPresenter
{
  public class Slideshow : SlideshowObserver
  {
    private readonly int _slideOffset;
    private readonly PdfRenderer _pdfRenderer;
    private WindowsFormsHost _windowsFormsHost;
    private readonly string _path;
    private int _currentPage;
    private SlideshowObserver _slideshowObserver;
    private int _totalPages = -1;

    public int CurrentPage { get { return _currentPage; } }
    public int TotalPages { get { return _totalPages; } }

    public Slideshow(string path, int slideOffset = 0)
    {
      _path = path;
      _slideOffset = slideOffset;
      _currentPage = slideOffset;
      _pdfRenderer = new PdfRenderer();
      _slideshowObserver = NoObservers();
    }

    public void ReportSlideChangesTo(SlideshowObserver slideshow)
    {
      _slideshowObserver = slideshow;
    }

    public void Load()
    {
      var pdfDocument = PdfDocument.Load(_path);
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
      _windowsFormsHost.Loaded += (sender, args) => pdfRenderer.Page = startingSlide;
    }

    public void OnKeyUpGoToNextSlide()
    {

    }

    public WindowsFormsHost ToWindowsFormsHost()
    {
      return _windowsFormsHost;
    }

    public void Advance()
    {
      _pdfRenderer.Page++;
      _currentPage = _pdfRenderer.Page;
      _pdfRenderer.Refresh();
      _slideshowObserver.NotifySlideChangedTo(_pdfRenderer.Page);
    }

    public void NotifySlideChangedTo(int page)
    {
      _pdfRenderer.Page = page + _slideOffset;
      _currentPage = _pdfRenderer.Page;
    }

    public void GoBackOneSlide()
    {
      _pdfRenderer.Page--;
      _currentPage = _pdfRenderer.Page;
      _pdfRenderer.Refresh();
      _slideshowObserver.NotifySlideChangedTo(_pdfRenderer.Page);
    }

    public void Refresh()
    {
      _pdfRenderer.Page = _currentPage;
      _pdfRenderer.Refresh();
    }

    private static BroadcastingSlideshowObserver NoObservers()
    {
      return new BroadcastingSlideshowObserver();
    }

  }
}