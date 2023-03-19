namespace MangaWorld_admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int AdminId { get; set; }

        [Required]
        [StringLength(255)]
        public string AdminName { get; set; }

        [Required]
        [StringLength(255)]
        public string AdminPassword { get; set; }

        public bool Deleted { get; set; }
    }
}
