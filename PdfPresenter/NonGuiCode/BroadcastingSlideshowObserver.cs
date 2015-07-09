using System.Collections.Generic;
using System.Linq;

namespace PdfPresenter.NonGuiCode
{
  public class BroadcastingSlideshowObserver : SlideshowObserver
  {
    private readonly List<SlideshowObserver> _observers;

    public BroadcastingSlideshowObserver(params SlideshowObserver[] slideshowObservers)
    {
      _observers = slideshowObservers.ToList();
    }

    public void NotifySlideChangedTo(int page)
    {
      foreach (var slideshow in _observers)
      {
        slideshow.NotifySlideChangedTo(page);
      }
    }
  }
}