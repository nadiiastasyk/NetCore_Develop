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

        [Range(0, 3000)]
        public int Age { get; set; }

        public int StoryId { get; set; }

        public virtual Story Story { get; set; }

    }

    public enum Gender
    {
        Unindentified,
        Male,
        Female
    }
}
