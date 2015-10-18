namespace ForumSystem.Data.Models
{
    using Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tag : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Name { get; set; }
    }
}
