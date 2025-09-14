namespace Domain.Absractions
{
    public interface ISoftDeletable
    {
        DateTime? DeletedAt { get; set; }
    }
}
