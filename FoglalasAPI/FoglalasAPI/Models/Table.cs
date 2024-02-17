﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FoglalasAPI.Models
{
    [Table("Tables")]
    [PrimaryKey("TableId", "RestaurantFK")]
    public class Table
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }
        [Key]
        [Required]
        [ForeignKey("RestaurantFK")]
        public Restaurant Restaurant { get; set; }

        [Required]
        public int Size { get; set; }
    }
}
