namespace MyCookbook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyCookbook.Data.Common.Models;

    public class Rating : BaseModel<int>
    {
        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}
