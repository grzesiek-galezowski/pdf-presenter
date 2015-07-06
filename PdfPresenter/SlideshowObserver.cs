namespace PdfPresenter
{
  public interface SlideshowObserver
  {
    void NotifySlideChangedTo(int page);
  }
}