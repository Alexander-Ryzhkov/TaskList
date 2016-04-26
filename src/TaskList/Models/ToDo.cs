using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.Models
{
    public class ToDo
    {

        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
        [Required]
        public string Priority { get; set; }
    }
}
