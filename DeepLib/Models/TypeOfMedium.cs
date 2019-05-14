using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeepLib.Models
{
    [Table("Medium")]
    public class TypeOfMedium
    {
        [Key]
        public virtual int Id { get; set; }
        public string Medium { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
