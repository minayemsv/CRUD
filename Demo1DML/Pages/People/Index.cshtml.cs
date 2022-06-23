using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace Demo1DML.Pages.People
{
    public class IndexModel : PageModel
    {
        public List<Peopleinfo> listPeople = new List<Peopleinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo1DML;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM People";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Peopleinfo peopleinfo = new Peopleinfo();
                                peopleinfo.Id = "" + reader.GetInt16(0);
                                peopleinfo.FirstName = reader.GetString(1);
                                peopleinfo.LastName = reader.GetString(2);
                                peopleinfo.DateofBirth = reader.GetString(3);
                                peopleinfo.Email = reader.GetString(4);
                                peopleinfo.created_at = reader.GetDateTime(5).ToString();

                                listPeople.Add(peopleinfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
    }

    public class Peopleinfo
    {
        public string Id;
        public string FirstName;
        public string LastName;
        public string DateofBirth;
        public string Email;
        public string created_at;
    }
}
