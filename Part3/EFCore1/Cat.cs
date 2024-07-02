using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore1
{
    [Table("T_Cats")]
    class Cat
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(22)]
        public string Name { get; set; }
    }
}
