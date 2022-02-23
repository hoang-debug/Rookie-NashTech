using Day_10.Data;
using Day_10.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Day_10.Services
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        public StudentService(DataContext context)
        {
            _context = context;
        }
        public async Task<IList<Student>> GetAllAsync()
        {
            return _context?.Students != null ? await _context.Students.ToListAsync() : Enumerable.Empty<Student>().ToList();
        }

        public async Task<Student?> GetOneAsync(int id)
        {

            return await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task<Student?> AddAsync(Student entity)
        {
            if (_context.Students == null) return null;

            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Student?> EditAsync(Student entity)
        {
            if (_context.Students == null) return null;

            _context.Students.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Student?> RemoveAsync(int id, Student entity)
        {            
            if (_context.Students == null) return null;

            var entityId = await _context.Students.SingleOrDefaultAsync(x => x.StudentId == id);

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}