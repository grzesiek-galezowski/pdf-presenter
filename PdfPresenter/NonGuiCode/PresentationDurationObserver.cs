using System;

namespace PdfPresenter
{
  public interface PresentationDurationObserver
  {
    void NotifyOnTimePassed(TimeSpan time);
  }

  
}