namespace LabolatoriumASP.NET.Models;

public class ContactMapper
{
    public static ContactEntity ToEntity(ContactModel model)
    {
        return new ContactEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Category = model.Category
        };
    }

    public static ContactModel ToModel(ContactEntity entity)
    {
        return new ContactModel()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Category = entity.Category
        };
    }
}