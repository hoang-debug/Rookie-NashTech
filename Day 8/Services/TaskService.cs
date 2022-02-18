using Task = Day_8.Models.Task;
namespace Day_8.Services
{
    public class TaskService : ITaskService
    {
        private static readonly List<Task> _dataSource = new List<Task>();
        public Task Add(Task task)
        {
            _dataSource.Add(task);
            return task;
        }


        public List<Task> Add(List<Task> tasks)
        {
            _dataSource.AddRange(tasks);
            return tasks;
        }

        public Task? Edit(Task task)
        {
            var current = _dataSource.FirstOrDefault(d => d.Id == task.Id);
            if (current == null) return null;

            current.Title = task.Title;
            current.Description = task.Description;
            current.Completed = task.Completed;
            return current;
        }

        public bool Exits(Guid id)
        {
            return _dataSource.Any(d => d.Id == id);
        }

        public List<Task> GetAll()
        {
            return _dataSource;
        }

        public Task? GetOne(Guid id)
        {
            return _dataSource.FirstOrDefault(o => o.Id == id);
        }

        public void Remove(Guid id)
        {
            var current = _dataSource.FirstOrDefault(o => o.Id == id);
            if (current != null)
            {
                _dataSource.Remove(current);
            }
        }

        public void Remove(List<Guid> ids)
        {
            _dataSource.RemoveAll(d => ids.Contains(d.Id));
        }
    }
}