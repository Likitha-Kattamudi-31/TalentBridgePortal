using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentBridgePortal.Data;
using TalentBridgePortal.DTOs;
using TalentBridgePortal.Models;

namespace TalentBridgePortal.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Register(RegisterDto dto)
        {
            if (await _context.JobSeekers.AnyAsync(x => x.Email == dto.Email))
                throw new InvalidOperationException("User already exists"); 

            byte[] resumeData;
            using (var ms = new MemoryStream())
            {
                await dto.Resume.CopyToAsync(ms);
                resumeData = ms.ToArray();
            }

            var user = new JobSeeker
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password, // demo only
                ResumeContent = resumeData,
                ResumeName = dto.Resume.FileName
            };

            _context.JobSeekers.Add(user);
            await _context.SaveChangesAsync();

            return user.Id; 
        }

        public async Task<JobSeekerDto?> Login(LoginDto dto)
        {
            var user = await _context.JobSeekers
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || string.IsNullOrEmpty(dto.Password))
                return null;

            // Optional: verify password properly using hashing
            if (user.Password != dto.Password)
                return null;

            // Convert resume bytes to Base64 to send via JSON
            string resumeBase64 = user.ResumeContent != null
                ? Convert.ToBase64String(user.ResumeContent)
                : "";

            return new JobSeekerDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ResumeName = user.ResumeName ?? "",
                ResumeBase64 = resumeBase64
            };
        }
    
    }
}
