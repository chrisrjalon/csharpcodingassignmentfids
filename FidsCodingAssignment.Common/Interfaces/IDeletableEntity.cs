namespace FidsCodingAssignment.Common.Interfaces;

public interface IDeletableEntity : IEntity
{
    public bool IsActive { get; set; }

    public DateTime? DateDeleted { get; set; }
}