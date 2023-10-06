using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mds_Core01.Models
{
    public partial class Player
    {
        public Player()
        {
            SeriesEntries = new HashSet<SeriesEntry>();
        }

        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public bool MaritalStatus { get; set; }

        public virtual ICollection<SeriesEntry> SeriesEntries { get; set; }
    }
}
