using System;
using System.Windows;

namespace ContactInfoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContactDAO _contactDAO;
        string response = string.Empty;
        public MainWindow()
        {
            _contactDAO = App.container.Resolve<IContactDAO>();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllContacts();
        }
        private void GetAllContacts()
        {
            try
            {
               var contactDetails = _contactDAO.GetContacts();
               dataGridContact.ItemsSource = contactDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            ContactInfo conInfo = SetContactInfo();
            try
            {
                if (btnAdd.Content.ToString() == "Add")
                {
                    response = _contactDAO.AddContact(conInfo);
                    MessageBox.Show(response);
                    GetAllContacts();
                }
                else
                {
                    response=_contactDAO.UpdateContact(conInfo); //update the new data 
                    MessageBox.Show(response);
                    GetAllContacts(); //display the updated record 
                }
                ClearControlsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = dataGridContact.SelectedItem;
            BindContactInfo(selectedContact as ContactInfo);
            btnAdd.Content = "Submit";
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Are you sure you want to delete?", "Please confirm.", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                try
                {
                    var selectedContact = dataGridContact.SelectedItem as ContactInfo;
                    //ContactInfo contact = session.Get<ContactInfo>(selectedContact.ContactId);
                    response= _contactDAO.DeleteContact(selectedContact); //delete the record 
                    MessageBox.Show(response);
                    GetAllContacts(); //display the new collection 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControlsData();
        }
        private ContactInfo SetContactInfo()
        {
            ContactInfo conactInfo = new ContactInfo();
            conactInfo.FirstName = txtFirstName.Text;
            conactInfo.LastName = txtLastName.Text;
            conactInfo.Mobile = txtMobile.Text;
            conactInfo.Age =Convert.ToInt32(txtAge.Text);
            if (btnAdd.Content.ToString() == "Submit")
            {
                conactInfo.ContactId= Convert.ToInt32(txtContactId.Text);
            }
            return conactInfo;
        }
        private void BindContactInfo(ContactInfo conactInfo)
        {

            txtFirstName.Text = conactInfo.FirstName;
            txtLastName.Text=conactInfo.LastName;
            txtMobile.Text = conactInfo.Mobile;
            txtAge.Text = Convert.ToString(conactInfo.Age);
            txtContactId.Text = Convert.ToString(conactInfo.ContactId);
        }
        private void ClearControlsData()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAge.Text = "0";
            txtContactId.Text = string.Empty;
            btnAdd.Content = "Add";
        }
    }
}
