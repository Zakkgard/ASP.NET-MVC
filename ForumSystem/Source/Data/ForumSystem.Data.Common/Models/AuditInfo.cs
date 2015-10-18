namespace ForumSystem.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AuditInfo : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }
    }
}
