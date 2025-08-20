namespace MyRecipeBook.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public DateTime? DeletionDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}