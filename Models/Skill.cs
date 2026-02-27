namespace TalentBridgePortal.Models
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid JobSeekerId { get; set; }
    }
}
