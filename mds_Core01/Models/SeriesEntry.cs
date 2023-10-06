using System;
using System.Collections.Generic;

namespace mds_Core01.Models
{
    public partial class SeriesEntry
    {
        public int SeriesEntryId { get; set; }
        public int PlayerId { get; set; }
        public int FormatId { get; set; }

        public virtual Format Format { get; set; } = null!;
        public virtual Player Player { get; set; } = null!;
    }
}
