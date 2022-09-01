namespace Agency.Models.VehicleValidators.Contacts
{
    public interface IValidator
    {
        int PasCapacity { get; set; }
        decimal Price { get; set; }
        bool isValid();
    }
}