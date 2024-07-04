namespace STOCKMVC.Entities
{
    public class Auditables
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsCreated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
