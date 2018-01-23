using alfrek.api.Models.Joins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace alfrek.api.Models.Solutions
{
    [Table("PurposedRoles")]
    public class PurposedRole
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public List<SolutionRole> SolutionRoles { get; set;  }

        public PurposedRole()
        {

        }

        public PurposedRole(string name)
        {
            Name = name;
        }

        public PurposedRole(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
