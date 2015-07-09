using System;

namespace PdfPresenter.Exceptions
{
  public class PresentationLoadResultedInZeroPageDocumentException : Exception
  {
    public PresentationLoadResultedInZeroPageDocumentException(string path) :
      base("Could not load the document, probably because the path \"" + path + "\" is not a valid pdf document")
    {
      
    }
  }
}