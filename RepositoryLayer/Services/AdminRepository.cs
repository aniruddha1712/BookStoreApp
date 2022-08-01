using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {
        public IConfiguration Configuration { get; }

        public AdminRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public AdminModel AdminLogin(AdminLoginModel admin)
        {
            AdminModel adminRes = new AdminModel();
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spAdminLogin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", admin.EmailId);
                command.Parameters.AddWithValue("@Password", admin.Password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        adminRes.AdminId = Convert.ToInt32(reader["AdminId"] == DBNull.Value ? default : reader["AdminId"]);
                        //var password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        adminRes.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                        adminRes.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                        adminRes.MobileNumber = Convert.ToInt64(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);

                        adminRes.Token = GenerateJwtToken(adminRes.EmailId, adminRes.AdminId);
                        return adminRes;
                    }
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return default;
        }
        public static string GenerateJwtToken(string emailID, int adminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THISISKEYTOGENERATETOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, emailID),
                    new Claim("AdminId", adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
