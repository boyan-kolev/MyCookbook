namespace MyCookbook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Data.Common.Models;

    public class Reply : BaseDeletableModel<string>
    {
        public Reply()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
