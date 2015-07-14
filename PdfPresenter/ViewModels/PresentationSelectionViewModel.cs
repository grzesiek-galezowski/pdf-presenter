using System.ComponentModel;
using System.Runtime.CompilerServices;
using PdfPresenter.Annotations;

namespace PdfPresenter.ViewModels
{
  public class PresentationSelectionViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public string Title
    {
      get { return "Welcome to PDF Presenter. Choose file to present."; }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}