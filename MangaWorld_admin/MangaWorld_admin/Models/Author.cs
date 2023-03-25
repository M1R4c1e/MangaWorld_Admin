namespace MangaWorld_admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Mangas = new HashSet<Manga>();
        }

        [StringLength(40)]
        public string AuthorId { get; set; }

        [Required]
        [StringLength(255)]
        public string AuthorName { get; set; }

        public string Biography { get; set; }

        public string Socials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manga> Mangas { get; set; }

        public override string ToString()
        {
            string str = '{' +
                "\"authorId\": \"" + this.AuthorId + "\", " +
                "\"name\": \"" + this.AuthorName + "\", " +
                "\"social\": \"" + this.Socials + "\"}";
            return str;
        }
    }
}
