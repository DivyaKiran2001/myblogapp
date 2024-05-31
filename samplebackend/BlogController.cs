using System.Data.SqlClient;
using System.Data.SqlTypes;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;



namespace MVC.Controllers
{
    
    public class BlogController : Controller
    {
        private readonly string connectionstring;
        private readonly IWebHostEnvironment _env;
        public BlogController(IConfiguration configuration, IWebHostEnvironment env)
        {
            connectionstring = configuration["ConnectionStrings:myblogdb"] ?? "";
            _env = env;
        }

        [HttpPost("blog/addblog")]

        public async Task<IActionResult> addblog(BlogPost blog,IFormFile file)
        {
            try
            {

                if(file!=null && file.Length>0)
                {
                    var uploads=Path.Combine(_env.WebRootPath,"uploads");
                    if(!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    var filePath=Path.Combine(uploads,"file.FileName");
                    using(var stream=new FileStream(filePath,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    blog.FilePath=filePath;
                }
                
                using(SqlConnection connection=new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query="insert into blogs (Title,Author,Category,FilePath,CreatedAt,UserId) values (@Title,@Author,@Category,@FilePath,@CreatedAt,@UserId)";
                    using(SqlCommand command=new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", blog.Title);
                        command.Parameters.AddWithValue("@Author", blog.Author);
                        command.Parameters.AddWithValue("@Category",blog.Category);
                        command.Parameters.AddWithValue("@FilePath",blog.FilePath);
                        command.Parameters.AddWithValue("@CreatedAt",blog.CreatedAt);
                        command.Parameters.AddWithValue("@UserId",blog.UserId);
                    }
                
                    command.ExecuteNonQuery();
                }
                return Ok(new {message="Blog Added Successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500,new {message="An error occurred while adding the blog",error=ex.Message});
            }
            
        }

        [HttpPut("blog/editblog")]

        public async Task<IActionResult> editBlog(BlogPost blog,string userid)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query="update blogs set Title=@Title,Author=@Author,Category=@Category,FilePath=@FilePath,CreatedAt=@CreatedAt where UserId=userid";
                    using(SqlCommand command=new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title",blog.Title);
                        command.Parameters.AddWithValue("@Author",blog.Author);
                        command.Parameters.AddWithValue("@Category",blog.Category);
                        command.Parameters.AddWithValue("@FilePath",blog.FilePath);
                        command.Parameters.AddWithValue("@CreatedAt", blog.CreatedAt);
                    }
                    command.ExecuteNonQuery();
                }
                return Ok(new {message="Blog details updated successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500,new {message="An error occurred while adding the blog",error=ex.Message});
            }
        }

    
        [HttpGet("blog/viewblogsbyid")]

        public async Task<IActionResult> GetBlogs(string blogid,string userid)
        {
            try
            {
                List<BlogPost> blogs=new List<BlogPost>();
                using(SqlConnection connection=new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query="select from blogs where BlogId=@BlogId and UserId=@UserId";
                    using(SqlCommand command=new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BlogId",blogid);
                        command.Parameters.AddWithValue("@UserId",userid);
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                BlogPost blog=new BlogPost
                                {
                                    BlogId=reader.GetString(0);
                                    Title = reader.GetString(1),
                                    Author = reader.GetString(2),
                                    Category = reader.GetString(3),
                                    FilePath = reader.GetString(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    UserId = reader.GetString(6)
                                };
                                blogs.Add(blog);
                            }
                        }
                    }
                    command.ExecuteNonQuery();
                }
                return Ok(blogs);
            }
            catch(Exception ex)
            {
                return StatusCode(500,new {message="An error occurred while getting the blog details",error=ex.Message})
            }
        }

        [HttpGet("blog/viewallblogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                List<BlogPost> blogs = new List<BlogPost>();
                using(SqlConnection connection=new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query="select * from blogs";
                    using(SqlCommand command=new SqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@BlogId",blogid);
                        command.Parameters.AddWithValue("@UserId",userid);
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                BlogPost blog=new BlogPost
                                {
                                    BlogId=reader.GetString(0);
                                    Title = reader.GetString(1),
                                    Author = reader.GetString(2),
                                    Category = reader.GetString(3),
                                    FilePath = reader.GetString(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    UserId = reader.GetString(6)
                                };
                                blogs.Add(blog);
                            }
                        }
                    }

                    command.ExecuteNonQuery();
                }
                return Ok(blogs);
            }
            catch(Exception ex)
            {
                return StatusCode(500,new {message="An error occurred while getting the blog details",error=ex.Message})
            }
        }

        [HttpDelete("userid")]

        public async Task<IActionResult> deleteblog(string blogid,string userid)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string query="delete from blogs where BlogId=@BlogId and UserId=@UserId"
                    using(SqlCommand command=new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BlogId=",blogid);
                        command.Parameters.AddWithValue("@UserId",userid);
                    }
                    command.ExecuteNonQuery();
                }
                return Ok(new { message="Blog deleted successfully"});
            }
            catch(Exception ex)
            {
                return StatusCode(500,new {message="An error occurred while deleting the blog",error=ex.Message});     
            }
        }    
    }
}
