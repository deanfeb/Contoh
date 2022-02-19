using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Unit")]
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
    }
}
