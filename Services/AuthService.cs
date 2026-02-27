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

        public async Task<string> Register(RegisterDto dto)
        {
            if (await _context.JobSeekers.AnyAsync(x => x.Email == dto.Email))
                return "User exists";

            // Create Resume Folder
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generate Unique File Name
            var fileName = Guid.NewGuid() + Path.GetExtension(dto.Resume.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            // Save Resume File
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Resume.CopyToAsync(stream);
            }

            var user = new JobSeeker
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password, // demo only
                ResumePath = filePath        // 👈 store file path
            };

            _context.JobSeekers.Add(user);
            await _context.SaveChangesAsync();

            return "Registered Successfully";
        }

        public async Task<bool?> Login(LoginDto dto)
        {
            var user = await _context.JobSeekers
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return null;

            if (dto.Password ==null)
                return null;

            return true;
        }
    }
}
