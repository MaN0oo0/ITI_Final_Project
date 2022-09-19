﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITI_Final_Project.Models
{
    [Table("Room")]
    public partial class Room
    {
        public Room()
        {
            Revevarstions = new HashSet<Revevarstion>();
        }

        [Key]
        public int Room_number { get; set; }
        [StringLength(50)]
        public string Phone_number { get; set; }
        public int? Cleaner_Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public double? Price { get; set; }
        public string Img { get; set; }

        [ForeignKey("Cleaner_Id")]
        [InverseProperty("Rooms")]
        public virtual Cleaner Cleaner { get; set; }
        [InverseProperty("Room_NumberNavigation")]
        public virtual ICollection<Revevarstion> Revevarstions { get; set; }
    }
}