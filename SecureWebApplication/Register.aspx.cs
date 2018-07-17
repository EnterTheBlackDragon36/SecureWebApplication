using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SecureWebApplication
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitRegistration_Click(Object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("UserAccountsConnectionString");

            SqlCommand cmd = conn.CreateCommand();

            // Create command text
            cmd.CommandText = "INSERT INTO Users (username, password, firstname, lastname, email) VALUES (@username, @password, @firstname, @lastname, @email)";

            //Fill our parameters
            cmd.Parameters.AddWithValue("@username", username.Text.ToString());
            cmd.Parameters.AddWithValue("password", FormsAuthentication.HashPasswordForStoringInConfigFile(password.Text.ToString(), "sha1"));
            cmd.Parameters.AddWithValue("@firstname", firstname.Text.ToString());
            cmd.Parameters.AddWithValue("@lastname", lastname.Text.ToString());
            cmd.Parameters.AddWithValue("@email", email.Text.ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            msglbl.Text = "Registered Successfully";
        }
    }
}