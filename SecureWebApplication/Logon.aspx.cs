using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SecureWebApplication
{
    public partial class Logon : System.Web.UI.Page
    {

        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            int saltSize = 5;
            string salt = CreateSalt(saltSize);
            string passwordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            try
            {
                StoreAccountDetails(txtUserName.Text, passwordHash, RoleList.SelectedItem.Text.ToString());
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void StoreAccountDetails(string userName, string passwordHash, string role)
        {
          // See "How To Use DPAPI (Machine Store) from ASP.NET" for information 
          // about securely storing connection strings.
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UserAccountsConnectionString"].ToString());


          SqlCommand cmd = new SqlCommand("RegisterUser", conn);
          cmd.CommandType = CommandType.StoredProcedure;
          SqlParameter sqlParam = null;
          //Usage of Sql parameters also helps avoid SQL Injection attacks.
          sqlParam = cmd.Parameters.Add("@userName", txtUserName.Text);
          //sqlParam.Value = userName;

          sqlParam = cmd.Parameters.Add("@passwordHash", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"));
          //sqlParam.Value = passwordHash;

          sqlParam = cmd.Parameters.Add("@role", RoleList.SelectedItem.Text.ToString());
          //sqlParam.Value = role;

          try
          {
            conn.Open();
            cmd.ExecuteNonQuery();
          }
          catch( Exception ex )
          {
            // Code to check for primary key violation (duplicate account name)
            // or other database errors omitted for clarity
            throw new Exception("Exception adding account. " + ex.Message);
          }
          finally
          {
            conn.Close();
          } 
        }

        

        protected void btnLogon_Click(object sender, EventArgs e)
        {
            // Initialize FormsAuthentication, for what it's worth
            FormsAuthentication.Initialize();

            // Create our connection and command objects
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["UserAccountsConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("LookupUser", conn);

            // Fill our parameters
            cmd.Parameters.AddWithValue("@userName", txtUserName.Text);
            cmd.Parameters.AddWithValue("@passwordHash", FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1")); // Or "sha1"

            // Execute the command
            conn.Open();

    

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Create a new ticket used for authentication
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                   1, // Ticket version
                   txtUserName.Text, // Username associated with ticket
                   DateTime.Now, // Date/time issued
                   DateTime.Now.AddMinutes(30), // Date/time to expire
                   true, // "true" for a persistent user cookie
                   reader.GetString(0), // User-data, in this case the roles
                   FormsAuthentication.FormsCookiePath);// Path cookie valid for

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash); // Hashed ticket

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);

                // Redirect to requested URL, or homepage if no previous page
                // requested
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null) returnUrl = "/";

                // Don't call FormsAuthentication.RedirectFromLoginPage since it
                // could
                // replace the authentication ticket (cookie) we just added
                Response.Redirect(returnUrl);

                lblMessage.Text = "User has been authenticated Successfully.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                // Never tell the user if just the username is password is incorrect.
                // That just gives them a place to start, once they've found one or
                // the other is correct!
                lblMessage.Text = "Username / password incorrect. Please try again.";
                lblMessage.Visible = true;
            }

            reader.Close();
            conn.Close();
        }






    }

}