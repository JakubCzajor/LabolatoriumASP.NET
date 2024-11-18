namespace LabolatoriumASP.NET.Models.Services;

public class MemoryContactService : IContactService
{
    private Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@gmail.com",
                BirthDate = new DateOnly(2003, 08, 02),
                PhoneNumber = "123 456 789",
                Category = Category.Business
            }
        },
        {
            2, new ContactModel()
            {
                Id = 2,
                FirstName = "Ala",
                LastName = "Makota",
                Email = "ala.makota@gmail.com",
                BirthDate = new DateOnly(2008, 12, 05),
                PhoneNumber = "987 654 321",
                Category = Category.Family
            }
        },
        {
            3, new ContactModel()
            {
                Id = 3,
                FirstName = "Jacek",
                LastName = "Młody",
                Email = "jacek.mlody@gmail.com",
                BirthDate = new DateOnly(1997, 01, 24),
                PhoneNumber = "481 789 093",
                Category = Category.Friend
            }
        },
    };

    private static int currentId = 3;
    
    public void Add(ContactModel model)
    {
        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
    }

    public void Update(ContactModel model)
    {
        if (_contacts.ContainsKey(model.Id))
        {
            _contacts[model.Id] = model;
        }
    }

    public void Delete(int id)
    {
        _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }
    
    public List<OrganizationEntity> GetOrganizations()
    {
        throw new NotImplementedException();
    }
}