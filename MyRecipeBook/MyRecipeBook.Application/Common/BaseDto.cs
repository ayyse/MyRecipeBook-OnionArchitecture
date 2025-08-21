namespace MyRecipeBook.Application.Common;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public DateTime? DeletionDate { get; set; }
}