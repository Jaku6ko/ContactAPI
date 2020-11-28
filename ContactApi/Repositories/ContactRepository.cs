using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using ContactApi.Models;
using System.Data.SqlClient;

namespace ContactApi.Repositories
{
    public class ContactRepository
    {
        public Contact Get(int id)
        {

            string commandLine = string.Format("SELECT * FROM contacts WHERE id ={0}", id);
            var connection = getConnection();
            connection.Open();
            var command = new SqlCommand(commandLine, connection);
            var result = command.ExecuteReader();
            result.Read();
            var contact = FillContact(result);
            connection.Close();
            return contact;
        }
        public List<Contact> List(int Limit = 5)
        {
            var contacts = new List<Contact>();
            string commandLine = string.Format("SELECT TOP({0}) * FROM contacts", Limit);
            var connection = getConnection();
            var command = new SqlCommand(commandLine, connection);
            connection.Open();
            var result = command.ExecuteReader();
            while (result.Read())
            {
                contacts.Add(FillContact(result));
            }
            connection.Close();
            return contacts;
        }
        public void Create(Contact contact)//Method that creates a new contact in the database
        {
            string commandLine = string.Format("INSERT INTO contacts VALUES('{0}', '{1}', '{2}')", contact.Name, contact.Email, contact.MobileNumber);
            var connection = getConnection();
            var command = new SqlCommand(commandLine, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(int id)
        {
            string commandLine = string.Format("DELETE FROM contacts WHERE id = {0} ", id);
            var connection = getConnection();
            connection.Open();
            var command = new SqlCommand(commandLine, connection);
            var result = command.ExecuteNonQuery();
            connection.Close();
        }
        public void Update(int id, Contact contact)
        {
            string commandLine = string.Format("UPDATE contacts SET name = '{1}', email = '{2}', mobile_number = '{3}' WHERE id = {0}",
                id, contact.Name.ToString(), contact.Email.ToString(), contact.MobileNumber.ToString());
            var connection = getConnection();
            connection.Open();
            var command = new SqlCommand(commandLine, connection);
            var result = command.ExecuteNonQuery();
            connection.Close();

        }
        private SqlConnection getConnection()//Method that establishes connection to the database
        {
            return new SqlConnection(@"Data Source=.\SQLEXPRESS;
                Initial Catalog=contact_database;
                Integrated Security=True;
                Connect Timeout=30;
                Encrypt=False;
                TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;
                MultiSubnetFailover=False"
                );
        }
        public Contact FillContact(SqlDataReader result)//Method that fills the contact variable with database info
        {
            var Contact = new Contact();
            Contact.Id = (int)result.GetValue(0);
            Contact.Name = (string)result.GetValue(1);
            Contact.Email = (string)result.GetValue(2);
            Contact.MobileNumber = (string)result.GetValue(3);
            return Contact;

        }
    }

}
