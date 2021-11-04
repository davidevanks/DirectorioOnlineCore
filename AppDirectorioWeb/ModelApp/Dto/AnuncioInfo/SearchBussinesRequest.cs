using System;
using System.Collections.Generic;
using System.Text;

namespace ModelApp.Dto.AnuncioInfo
{
    public class SearchBussinesRequest
    {
        public string Search { get; set; }
        public string Address  { get; set; }

        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        public int Distance { get; set; }
    }
}
