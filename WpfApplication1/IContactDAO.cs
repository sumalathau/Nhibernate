using System.Collections;

namespace WpfApplication1
{
    public interface IContactDAO
    {
        string AddContact(ContactInfo cInfo);
        string UpdateContact(ContactInfo cInfo);
        string DeleteContact(ContactInfo cInfo);
        IList GetContacts();
    }
}
