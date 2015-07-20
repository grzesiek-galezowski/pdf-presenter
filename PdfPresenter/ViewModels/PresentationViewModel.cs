using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PdfPresenter.Annotations;

namespace PdfPresenter.ViewModels
{
  public class PresentationViewModel : INotifyPropertyChanged
  {
    private readonly Slideshow _mainSlideshow;
    private Visibility _presentationVisibility;

    public PresentationViewModel(Slideshow mainSlideshow)
    {
      _mainSlideshow = mainSlideshow;
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

    public ICommand ExitCommand
    {
      get
      {
        return new RelayCommand(_ => Exit());
      }
    }

    private void Exit()
    {
      throw new NotImplementedException();
    }

    private void GoToPreviousSlide()
    {
      _mainSlideshow.PreviousSlide();
    }

    private void GoToNextSlide()
    {
      _mainSlideshow.NextSlide();
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
