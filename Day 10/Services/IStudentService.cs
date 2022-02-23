using Day_10.Data.Entities;

namespace Day_10.Services
{
    public interface IStudentService
    {
        public Task<IList<Student>> GetAllAsync();
        public Task<Student?> GetOneAsync(int id);
        public Task<Student?> AddAsync(Student entity);
        public Task<Student?> EditAsync(Student entity);
        public Task<Student?> RemoveAsync(int id, Student entity);

    }
}