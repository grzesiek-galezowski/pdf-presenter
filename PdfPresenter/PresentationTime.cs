using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PdfPresenter
{
  public class PresentationTime
  {
    private readonly IProgress<TimeSpan> _progress;
    private readonly Stopwatch _stopwatch;

    public PresentationTime(PresentationDurationObserver presentationDurationObserver)
    {
      _progress = new Progress<TimeSpan>(presentationDurationObserver.NotifyOnTimePassed);
      _stopwatch = new Stopwatch();
    }

    public void StartMeasuring()
    {
      _stopwatch.Start();
      Task.Run(() =>
      {
        while (true)
        {
          _progress.Report(_stopwatch.Elapsed);
          Thread.Sleep(1000);
        }
      });
    }
  }
}