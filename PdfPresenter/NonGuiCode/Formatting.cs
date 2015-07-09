using System;

namespace PdfPresenter.NonGuiCode
{
  static internal class Formatting
  {
    public static string OfPresentationDurationText(TimeSpan time)
    {
      return time.ToString("c").Substring(0, 8);
    }

    public static string OfSlideProgressionText(Slideshow currentSlide)
    {
      return currentSlide.CurrentPage + 1 + " / " + currentSlide.TotalPages;
    }
  }
}