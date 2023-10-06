using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mds_Core01.Models.ViewModels
{
    public class PlayerVM
    {
        public PlayerVM()
        {
            this.FormatList = new List<int>();
        }


        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Enter Player Name"), StringLength(200), Display(Name = "Player Name")]

        public string PlayerName { get; set; } = null!;
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Picture Path")]
        public IFormFile PicturePath { get; set; }
        public string? Picture { get; set; } = null!;
        [Display(Name = "Marital Status")]

        public bool MaritalStatus { get; set; }


        public List<int> FormatList { get; set; }
    }
}
