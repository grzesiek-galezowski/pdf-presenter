using System.Windows;
using PdfFileViewControl;

static internal class PdfFileViewProperties
{
  public static DependencyProperty CreateFileProperty()
  {
    return DependencyProperty.Register("File", typeof(string), typeof(PdfFileView), new PropertyMetadata(default(string)));
  }

  public static DependencyProperty CreateTotalPagesProperty()
  {
    return DependencyProperty.Register("TotalPages", typeof(int?), typeof(PdfFileView), new PropertyMetadata(default(int?)));
  }

  public static DependencyProperty CreatePageProperty(PropertyChangedCallback onPageNumberChanged)
  {
    return DependencyProperty.Register("Page", typeof(int), typeof(PdfFileView), 
      new PropertyMetadata(default(int), onPageNumberChanged));
  }
}