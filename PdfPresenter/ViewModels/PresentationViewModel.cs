using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PdfPresenter.Annotations;
using PdfPresenter.NonGuiCode;

namespace PdfPresenter.ViewModels
{
  public class PresentationViewModel : INotifyPropertyChanged
  {
    private readonly PresentationProgressObserver _progressObserver;
    private Visibility _presentationVisibility;
    private int _currentSlide = 0;

    public PresentationViewModel(
      string path, 
      int initialSlide, 
      PresentationProgressObserver progressObserver)
    {
      _progressObserver = progressObserver;
      File = path;
      CurrentSlide = initialSlide;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand ToggleVisibility
    {
      get
      {
        return new RelayCommand(_ => ChangePresentationVisibility() );
      }
    }

    private Visibility ChangePresentationVisibility()
    {
      return PresentationVisibility = (PresentationVisibility == Visibility.Visible)
        ? Visibility.Hidden : Visibility.Visible;
    }

    public Visibility PresentationVisibility
    {
      get { return _presentationVisibility; }
      set
      {
        _presentationVisibility = value;
        OnPropertyChanged();
      }
    }

    public ICommand NextSlideCommand
    {
      get { return new RelayCommand(_ => GoToNextSlide()); }
    }

    public ICommand PreviousSlideCommand
    {
      get { return new RelayCommand(_ => GoToPreviousSlide()); }
    }

    public string File { get; private set; }

    public int CurrentSlide
    {
      get { return _currentSlide; }
      set
      {
        _currentSlide = value;
        OnPropertyChanged();
        _progressObserver.NotifySlideChangedTo(_currentSlide, TotalSlides);
      }
    }

    public int TotalSlides { get; set; }

    private void GoToPreviousSlide()
    {
      if (CurrentSlide > 0)
      {
        CurrentSlide--;
      }
    }

    private void GoToNextSlide()
    {
      if (CurrentSlide < TotalSlides)
      {
        CurrentSlide++;
      }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
