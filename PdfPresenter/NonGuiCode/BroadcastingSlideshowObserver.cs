using System.Collections.Generic;
using System.Linq;

namespace PdfPresenter.NonGuiCode
{
  public class BroadcastingSlideshowObserver : PresentationProgressObserver
  {
    private readonly List<PresentationProgressObserver> _observers;

    public BroadcastingSlideshowObserver(params PresentationProgressObserver[] slideshowObservers)
    {
      _observers = slideshowObservers.ToList();
    }

    public void NotifySlideChangedTo(int page, int totalPages)
    {
      foreach (var slideshow in _observers)
      {
        slideshow.NotifySlideChangedTo(page, totalPages);
      }
    }
  }
}