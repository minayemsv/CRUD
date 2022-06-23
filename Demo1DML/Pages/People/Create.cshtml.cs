using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace Demo1DML.Pages.People
{
    public class CreateModel : PageModel
    {
        public Peopleinfo peopleinfo=new Peopleinfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            peopleinfo.FirstName = Request.Form["Name: "];
            peopleinfo.LastName = Request.Form["Surname: "];
            peopleinfo.DateofBirth = Request.Form["Date of birth: "];
            peopleinfo.Email=Request.Form["Email: "];
            
            if(peopleinfo.FirstName.Length==0 || peopleinfo.LastName.Length==0||
                peopleinfo.DateofBirth.Length==0 || peopleinfo.Email.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new person into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo1DML;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                               "(FirstName, LastName,DateofBirth,Email) Values" +
                               "(@FirstName,@LastName,@DateofBirth,@Email)";

                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", peopleinfo.FirstName);
                        command.Parameters.AddWithValue("@LastName",peopleinfo.LastName);
                        command.Parameters.AddWithValue("@DateofBirth", peopleinfo.DateofBirth);
                        command.Parameters.AddWithValue("@Email", peopleinfo.Email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
            peopleinfo.FirstName = "";peopleinfo.LastName = "";peopleinfo.DateofBirth = "";peopleinfo.Email = "";
            successMessage = "New person added correctly";

            Response.Redirect("/People/Index");
        }
    }
}
