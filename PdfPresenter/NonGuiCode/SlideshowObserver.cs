namespace PdfPresenter.NonGuiCode
{
  public interface SlideshowObserver
  {
    void NotifySlideChangedTo(int page);
  }
}