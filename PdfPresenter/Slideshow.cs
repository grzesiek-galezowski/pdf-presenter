using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using PdfiumViewer;

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
      _pdfRenderer.Load(PdfDocument.Load(_path));

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
      _pdfRenderer.KeyUp += (o, args) =>
      {
        if (args.KeyCode == Keys.Down)
        {
          Advance();
        }
        else if (args.KeyCode == Keys.Up)
        {
          GoBack();
        }
      };
    }

    public WindowsFormsHost ToWindowsFormsHost()
    {
      return _windowsFormsHost;
    }

    private void Advance()
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

    private void GoBack()
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