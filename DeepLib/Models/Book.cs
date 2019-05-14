using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeepLib.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public string Cover { get; set; }
        public bool Status { get; set; }
        public string WhoChangedStatus { get; set; }
        public string Comments { get; set; }
        public DateTime WhenWasLent { get; set; }
        
        //public int RodzajNosnika { get; set; }
        //public int Autor { get; set; }
        [ForeignKey("Nosniki")]
        public virtual TypeOfMedium TypeOfMedium { get; set; }
        [ForeignKey("Authors")]
        public virtual Author Author { get; set; }


    }
}
