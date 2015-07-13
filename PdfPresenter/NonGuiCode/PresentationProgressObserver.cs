namespace PdfPresenter.NonGuiCode
{
  public interface PresentationProgressObserver
  {
    void NotifySlideChangedTo(int page, int totalPages);
  }
}