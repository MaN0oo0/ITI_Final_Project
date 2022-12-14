// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITI_Final_Project.Models
{
    [Table("Food")]
    public partial class Food
    {
        public Food()
        {
            Buys = new HashSet<Buy>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Food_Name { get; set; }
        public double? Price { get; set; }

        [InverseProperty("Food")]
        public virtual ICollection<Buy> Buys { get; set; }
    }
}