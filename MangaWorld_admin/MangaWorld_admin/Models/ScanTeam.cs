namespace MangaWorld_admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScanTeam")]
    public partial class ScanTeam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScanTeam()
        {
            Chapters = new HashSet<Chapter>();
        }

        [StringLength(40)]
        public string ScanTeamId { get; set; }

        [Required]
        [StringLength(255)]
        public string TeamName { get; set; }

        public string TeamDescription { get; set; }

        [Required]
        public string TeamSocials { get; set; }

        [Required]
        [StringLength(40)]
        public string LanguageId { get; set; }

        public bool Deleted { get; set; }

        public virtual Language Language { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chapter> Chapters { get; set; }

        public override string ToString()
        {
            string str = '{' +
                "\"teamId\": \"" + this.ScanTeamId + "\", " +
                "\"language\": \"" + this.Language.LanguageName + "\", " +
                "\"description\": \"" + this.TeamDescription + "\", " +
                "\"social\": \"" + this.TeamSocials + "\", " +
                "\"teamName\": \"" + this.TeamName + "\", " +
                "\"isDeleted\": \"" + this.Deleted + "\"}";
            return str;
        }
    }
}
