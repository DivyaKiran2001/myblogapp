using System.Data.SqlClient;
using api.Models;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    public class UserController : Controller
    {
        private readonly string connectionstring;
        public UserController(IConfiguration configuration)
        {
            connectionstring = configuration["ConnectionStrings:myblogdb"] ?? "";
        }
        
        [HttpPost("user/register")]

        public async Task<IActionResult> register(UserModel userobj)
        {
            using(SqlConnection connection=new SqlConnection(connectionstring))
            {
                connection.Open();
                string query="insert into users (UserId,UserName,Email,Password) values (@UserId,@UserName,@Email,@Password)";
                using(SqlCommand command=new SqlCommand(query, connectionstring))
                {
                    command.Parameters.AddWithValue("@UserId", userobj.UserId);
                    command.Parameters.AddWithValue("@UserName", userobj.UserName);
                    command.Parameters.AddWithValue("@Email",userobj.Email);
                    command.Parameters.AddWithValue("@Password",userobj.Password);
                }

                command.ExecuteNonQuery();
            }
            return Ok(userobj);
        }



        [HttpGet("user/login")]
        public async Task<IActionResult> Login(UserModel userobj)
        {
            using(SqlConnection connection=new SqlConnection())
            {
                connection.Open();
                string query="select count(*) from users where Email=@Email and Password=@Password";
                using(SqlCommand command=new SqlCommand(query, connectionstring))
                {
                    command.Parameters.AddWithValue("@Email",userobj.Email);
                    command.Parameters.AddWithValue("@Password",userobj.Password);
                }
                int count=command.ExecuteNonQuery();
                if(count=1)
                {
                    return Ok("Login Success");
                }
                else
                {
                    return BadRequest("Invalid Email or Password");
                }
                
            }
        }

    }
}
