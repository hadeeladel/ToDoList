using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAPI.Models;

public class task
{
    [Key]
    public int TaskId { get; set; }
    [Required]
    [MaxLength(100)]
    public string UserId { get; set; }

    [Required]
    public string TaskName { get; set; }

    [MaxLength(200)]
    public String? TaskDescription { get; set; }

    [Required]
    public bool Finished { get; set; }
}
