﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace FoglalasAPI.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int Size { get; set; }
        public bool available { get; set; }
    }
}
