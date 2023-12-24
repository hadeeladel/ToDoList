using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [Column(TypeName = "nvarchar (450)")]
    public string? UserId { get; set; }
    public User User { get; set; }

    [Required]
    public string TaskName { get; set; }

    [MaxLength(200)]
    public String? TaskDescription { get; set; }

    [Required]
    public bool Finished { get; set; }
}
