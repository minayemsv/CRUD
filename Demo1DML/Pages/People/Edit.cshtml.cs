using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace Demo1DML.Pages.People
{
    public class EditModel : PageModel
    {
        public Peopleinfo peopleinfo = new Peopleinfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String Id=Request.Query["id"];


            try
            {
                String connectionString= "Data Source=.\\sqlexpress;Initial Catalog=Demo1DML;Integrated Security=True";
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM People WHERE Id=@Id";
                    using(SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                peopleinfo.Id=""+reader.GetInt32(0);
                                peopleinfo.FirstName=reader.GetString(1);
                                peopleinfo.LastName=reader.GetString(2);
                                peopleinfo.DateofBirth = reader.GetString(3);
                                peopleinfo.Email=reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            peopleinfo.Id=Request.Form["id"];
            peopleinfo.FirstName = Request.Form["name"];
            peopleinfo.LastName = Request.Form["surname"];
            peopleinfo.DateofBirth = Request.Form["Date of birth"];
            peopleinfo.Email= Request.Form["email"];

            if (peopleinfo.FirstName.Length == 0 || peopleinfo.LastName.Length == 0 ||
               peopleinfo.DateofBirth.Length == 0 || peopleinfo.Email.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String connectionString= "Data Source=.\\sqlexpress;Initial Catalog=Demo1DML;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE People " +
                        "SET FirstName=@FirstName,LastName=@LastName,DateofBirth=@DateofBirth,Email=@EMail";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", peopleinfo.FirstName);
                        command.Parameters.AddWithValue("@LastName", peopleinfo.LastName);
                        command.Parameters.AddWithValue("@DateofBirth",peopleinfo.DateofBirth);
                        command.Parameters.AddWithValue("@Email", peopleinfo.Email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
            Response.Redirect("/People/Index");
        }
    }
}
