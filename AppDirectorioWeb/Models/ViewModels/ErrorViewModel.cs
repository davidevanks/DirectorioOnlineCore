using System;
using System.Security.AccessControl;

namespace AppDirectorioWeb.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string MessageError { get; set; }
    }
}
