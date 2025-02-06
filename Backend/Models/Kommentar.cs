using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Kommentar
    {
        public Guid Id { get; set; }

        public string? GepostedVon { get; set; }

        public string? Inhalt { get; set; }

        public DateTime GepostetAm { get; set; } = DateTime.Today;
    }
}

