using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
