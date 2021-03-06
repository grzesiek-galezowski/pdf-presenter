﻿using System;
using System.Configuration;
using System.Windows;
using Microsoft.Win32;
using Pri.LongPath;

namespace PdfPresenter
{
  /// <summary>
  /// Interaction logic for PresentationSelectionView.xaml
  /// </summary>
  public partial class PresentationSelectionView : Window
  {
    readonly Action<string> _runPresentation;

    public PresentationSelectionView(Action<string> runPresentation)
    {
      _runPresentation = runPresentation;
      InitializeComponent();
      PresentationPath.Text = ConfigurationManager.AppSettings["path"];
    }

    void PresentButton_OnClick(object sender, RoutedEventArgs e)
    {
      try
      {
        var text = PresentationPath.Text;

        if (new FileInfo(text).Exists)
        {
          _runPresentation(text);
        }
        else
        {
          MessageBox.Show("Invalid path!");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    void ExitButton_OnClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    void BrowseButton_OnClick(object sender, RoutedEventArgs e)
    {
      var fileDialog = new OpenFileDialog();
      fileDialog.Title = "Please select pdf file to present";
      fileDialog.ShowReadOnly = true;
      fileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
      var result = fileDialog.ShowDialog();
      switch (result)
      {
        case true:
          var file = fileDialog.FileName;
          PresentationPath.Text = file;
          PresentationPath.ToolTip = file;
          break;
        default:
          PresentationPath.Text = null;
          PresentationPath.ToolTip = null;
          break;
      }
    }
  }
}
