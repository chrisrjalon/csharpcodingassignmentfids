namespace FidsCodingAssignment.Common.Interfaces;

public interface IModifiableEntity : IEntity
{
    public int? ModifiedBy { get; set; }
    
    public DateTime? LastModified { get; set; }
}