namespace FidsCodingAssignment.Common.Interfaces;

public interface IEntity
{
    public int Id { get; set; }
    
    public int CreatedBy { get; set; }

    public DateTime DateCreated { get; set; }
}