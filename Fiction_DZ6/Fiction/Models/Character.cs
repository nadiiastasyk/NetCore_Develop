using System.ComponentModel.DataAnnotations;

namespace Fiction_DZ6.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [Range(18, 35, ErrorMessage = "Age must be in a range")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Story Id must not be empty")]
        public int? StoryId { get; set; }

        public virtual Story Story { get; set; }

    }

    public enum Gender
    {
        Unindentified,
        Male,
        Female
    }
}
