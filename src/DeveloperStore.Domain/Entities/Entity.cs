using System.ComponentModel.DataAnnotations;

namespace DeveloperStore.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            UpdatedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime _createAt;
        public DateTime CreatedAt
        {
            get
            {
                return DateTime.SpecifyKind(_createAt, DateTimeKind.Utc);
            }
            set
            {
                _createAt = value;
            }
        }

        private DateTime _updateAt;
        public DateTime UpdatedAt
        {
            get
            {
                return DateTime.SpecifyKind(_updateAt, DateTimeKind.Utc);
            }
            set
            {
                _updateAt = value;
            }
        }
    }
}
