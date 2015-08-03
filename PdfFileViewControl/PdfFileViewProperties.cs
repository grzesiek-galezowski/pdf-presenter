using System.Windows;
using PdfFileViewControl;

static class PdfFileViewProperties
{
  public static DependencyProperty CreateFileProperty<T>()
  {
    return DependencyProperty.Register("File", typeof(T), typeof(PdfFileView), new PropertyMetadata(default(T)));
  }

  public static DependencyProperty CreateTotalPagesProperty<T>()
  {
    return DependencyProperty.Register("TotalPages", typeof(T), typeof(PdfFileView), new PropertyMetadata(default(T)));
  }

  public static DependencyProperty CreatePageProperty<T>(PropertyChangedCallback onPageNumberChanged)
  {
    return DependencyProperty.Register("Page", typeof(T), typeof(PdfFileView), 
      new PropertyMetadata(default(T), onPageNumberChanged));
  }
}