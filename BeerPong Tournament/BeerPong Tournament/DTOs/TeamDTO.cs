using BeerPong_Tournament.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerPong_Tournament.DTOs
{
    public class TeamDTO : IDomain
    {
        [Key]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }   
       
    }
}
