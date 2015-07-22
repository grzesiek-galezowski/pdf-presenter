using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PdfPresenter.Annotations;
using PdfPresenter.NonGuiCode;

namespace PdfPresenter.ViewModels
{
  public class HelperViewModel : 
    INotifyPropertyChanged, 
    PresentationDurationObserver,
    PresentationProgressObserver
  {
    private string _timeSinceStartString;
    private string _slideProgressText;
    public event PropertyChangedEventHandler PropertyChanged;

    public string TimeSinceStartString
    {
      get { return _timeSinceStartString; }
      set
      {
        _timeSinceStartString = value;
        OnPropertyChanged();
      }
    }

    public string SlideProgressText
    {
      get { return _slideProgressText; }
      set
      {
        _slideProgressText = value; 
        OnPropertyChanged();
      }
    }

    public string TitleText
    {
      get { return "Move this window to second screen and maximize"; }
    }

    public void NotifyOnTimePassed(TimeSpan time)
    {
      TimeSinceStartString = Formatting.OfPresentationDurationText(time);
    }

    public void NotifySlideChangedTo(int page, int totalPages)
    {
      SlideProgressText = Formatting.OfSlideProgressionText(page, totalPages);
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}