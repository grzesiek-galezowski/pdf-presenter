using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PdfPresenter.NonGuiCode;
using PdfPresenter.ViewModels;
using TddEbook.TddToolkit;

namespace PdfPresenterSpecification
{
  public class PresentationViewModelSpecification
  {
    [Test]
    public void ShouldNotifyOnSlideChange()
    {
      //GIVEN
      var viewModel = new PresentationViewModel(Any.String(), 0, Any.Instance<PresentationProgressObserver>())
      {
        TotalSlides = 100
      };
      var dummy = Substitute.For<IDummy>();
      viewModel.PropertyChanged += dummy.Invoked;

      //WHEN
      viewModel.NextSlideCommand.Execute(null);

      //THEN
      dummy.Received(1).Invoked(viewModel, Arg.Is<PropertyChangedEventArgs>(
        args => args.PropertyName == "CurrentSlide"));
    }
  }

  public interface IDummy
  {
    void Invoked(object sender, PropertyChangedEventArgs e);
  }
}
