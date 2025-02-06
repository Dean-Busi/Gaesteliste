using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class KommentarDto
    {
        public string? GepostedVon { get; set; }

        public string? Inhalt { get; set; }

        public DateTime GepostetAm { get; set; } = DateTime.Today;
    }
}

