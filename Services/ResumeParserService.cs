namespace TalentBridgePortal.Services
{
    public class ResumeParserService
    {
        public (List<string> skills, string experience) Extract(string resumeText)
        {
            var skills = new List<string>();

            if (resumeText.Contains("C#")) skills.Add("C#");
            if (resumeText.Contains(".NET")) skills.Add(".NET");
            if (resumeText.Contains("SQL")) skills.Add("SQL");
            if (resumeText.Contains("Azure")) skills.Add("Azure");

            string experience = "2+ years";

            return (skills, experience);
        }
    }
}
