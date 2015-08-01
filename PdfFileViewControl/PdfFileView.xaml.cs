﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using PdfiumViewer;

namespace PdfFileViewControl
{
  /// <summary>
  /// Interaction logic for UserControl1.xaml
  /// </summary>
  public partial class PdfFileView : UserControl
  {
    private readonly PdfRenderer _pdfRenderer;
    private readonly WindowsFormsHost _windowsFormsHost;
    
    public static readonly DependencyProperty FileProperty = PdfFileViewProperties.CreateFileProperty();
    public static readonly DependencyProperty PageProperty = PdfFileViewProperties.CreatePageProperty(OnPageNumberChanged);
    public static readonly DependencyProperty TotalPagesProperty = PdfFileViewProperties.CreateTotalPagesProperty();
    private int _cachedPageForErrorWorkaround;


    public string File
    {
      get { return (string) GetValue(FileProperty); }
      set { SetValue(FileProperty, value); }
    }

    public int Page
    {
      get { return (int) GetValue(PageProperty); }
      set { SetValue(PageProperty, value); }
    }

    public int? TotalPages
    {
      get { return (int?) GetValue(TotalPagesProperty); }
      set { SetValue(TotalPagesProperty, value); }
    }

    public PdfFileView()
    {
      InitializeComponent();
      _pdfRenderer = new PdfRenderer();
      _windowsFormsHost = new WindowsFormsHost
      {
        Child = _pdfRenderer
      };
      MainGrid.Children.Add(_windowsFormsHost);
    }

    private void PdfFileView_OnLoaded(object sender, RoutedEventArgs e)
    {
      
      var pdfDocument = PdfDocument.Load(File);
      if (pdfDocument.PageCount == 0)
      {
        throw new PresentationLoadResultedInZeroPageDocumentException(File);
      }

      TotalPages = pdfDocument.PageCount;
      _pdfRenderer.Load(pdfDocument);

      AsSoonAsWinformsHostLoadsShowSlide(Page, _pdfRenderer);
    }

    private void AsSoonAsWinformsHostLoadsShowSlide(int startingSlide, PdfRenderer pdfRenderer)
    {
      _windowsFormsHost.Loaded += (sender, args) =>
      {
        pdfRenderer.Page = startingSlide;
        //_slideshowObserver.NotifySlideChangedTo(CurrentPage, TotalPages);
      };
    }

    private static void OnPageNumberChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      var view = (PdfFileView) dependencyObject;
      var newValue = (int)dependencyPropertyChangedEventArgs.NewValue;
      view._pdfRenderer.Page = newValue;
      view._cachedPageForErrorWorkaround = newValue;
    }

    private void PdfFileView_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      CenterOnCurrentPage();
    }

    private void CenterOnCurrentPage()
    {
      //previously, I though I could write _pdfRenderer.Page = _pdfRenderer.Page;
      //but resizing from min to max caused the _pdfRenderer.Page to magically flip to next/prev
      _pdfRenderer.Page = _cachedPageForErrorWorkaround;
    }
  }

  //bug remove the original exception
}
