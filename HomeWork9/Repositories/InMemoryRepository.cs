using HomeWork9.Models.Entities;

namespace HomeWork9.Repositories
{
    public abstract class InMemoryRepository<T> where T : BaseEntity
    {
        protected readonly List<T> _items = new List<T>();

        public virtual T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

            if (_items.Any(e => e.Id == entity.Id))
                throw new InvalidOperationException($"Сущность с Id {entity.Id} уже существует.");

            _items.Add(entity);
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _items;
        }

        public virtual T GetById(int id)
        {
            var entity = _items.FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"Сущность с Id {id} не найдена.");

            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Сущность не может быть null.");

            var existingEntity = GetById(entity.Id); // Вызовет ошибку, если не найден

            var index = _items.IndexOf(existingEntity);
            _items[index] = entity;

            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id); // Вызовет ошибку, если не найден
            _items.Remove(entity);
        }
    }
}
