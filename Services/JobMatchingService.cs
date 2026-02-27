using TalentBridgePortal.Models;

namespace TalentBridgePortal.Services
{
    public class JobMatchingService
    {
        public List<JobResult> MatchJobs(List<string> userSkills)
        {
            var jobs = new List<JobResult>
        {
            new JobResult
            {
                Title = "Senior .NET Developer",
                Company = "TechCorp",
                Location = "Remote",
                Url = "https://example.com/job1"
            },
            new JobResult
            {
                Title = "Backend Engineer",
                Company = "CloudSoft",
                Location = "Bangalore",
                Url = "https://example.com/job2"
            }
        };

            foreach (var job in jobs)
            {
                int match = userSkills.Count(skill =>
                    job.Title.Contains(skill, StringComparison.OrdinalIgnoreCase));

                job.MatchPercentage = match * 30;
            }

            return jobs.OrderByDescending(x => x.MatchPercentage).ToList();
        }
    }
}
