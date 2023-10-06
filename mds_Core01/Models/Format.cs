using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mds_Core01.Models
{
    public partial class Format
    {
        public Format()
        {
            SeriesEntries = new HashSet<SeriesEntry>();
        }

        public int FormatId { get; set; }
        [Required]

        [Display(Name = "Match Format")]
        public string FormatName { get; set; } = null!;

        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
