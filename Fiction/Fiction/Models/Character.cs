namespace Fiction.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int StoryId { get; set; }

        public virtual Story Story { get; set; }

    }
}
