namespace BGGToolsMvC.Models
{
    public class Thing
    {
        public int ThingId { get; set; }

        public string Name { get; set; }

        public string YearPublished { get; set; }

        public string Thumbnail { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int MinPlayTime { get; set; }
        
        public int MaxPlayTime { get; set; }

        public int MinAge { get; set; }
    }
}
