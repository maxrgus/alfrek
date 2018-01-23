using alfrek.api.Models.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alfrek.api.Models.Joins
{
    public class SolutionRole
    {
        public int SolutionId { get; set; }
        public Solution Solution { get; set; }

        public int PurposedRoleId { get; set; }
        public PurposedRole PurposedRole { get; set; }

        public SolutionRole(Solution solution, PurposedRole purposedRole)
        {
            Solution = solution;
            PurposedRole = purposedRole;
        }

        public SolutionRole()
        {
        }
    }
}
