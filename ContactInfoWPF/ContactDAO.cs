using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Linq;

namespace ContactInfoWPF
{
    public class ContactDAO:IContactDAO
    {
        string resultMsg = string.Empty;
        public string AddContact(ContactInfo conInfo)
        {
            using (ISession session = SessionFactory.OpenSession)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(conInfo);
                        transaction.Commit();
                        resultMsg = "New contact added successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultMsg = ex.Message;
                    }
                }
            }
            return resultMsg;
        }
        public string UpdateContact(ContactInfo conInfo)
        {
            using (ISession session = SessionFactory.OpenSession)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(conInfo);
                        transaction.Commit();
                        resultMsg = "Contact updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultMsg = ex.Message;
                    }
                }
            }
            return resultMsg;
        }
        public string DeleteContact(ContactInfo conInfo)
        {
            using (ISession session = SessionFactory.OpenSession)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {

                        session.Delete(conInfo); //delete the record 
                        transaction.Commit(); //commit it 
                        resultMsg = "Contact deleted successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        resultMsg = ex.Message;
                    }
                }
            }
            return resultMsg;
        }
        public IList GetContacts()
        {
            using (ISession session = SessionFactory.OpenSession)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var contactList = session.Query<ContactInfo>().ToList();
                    return contactList;
                }
            }
        }
    }
}
