using System;
using RepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System.Text;
using CommonLayer;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using Experimental.System.Messaging;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        public IConfiguration Configuration { get; }

        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public UserRegisterModel Register(UserRegisterModel user)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spRegister", connection);
                command.CommandType = CommandType.StoredProcedure;

                var encryptedPass = EncryptPassword(user.Password);

                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", encryptedPass);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if(result != 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginResponseModel Login(LoginModel user)
        {
            LoginResponseModel loginRes = new LoginResponseModel();
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spLogin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmailId", user.EmailId);

                connection.Open();
                SqlDataReader reader =command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        loginRes.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        var password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        loginRes.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                        loginRes.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                        loginRes.MobileNumber = Convert.ToInt64(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);

                        var decodePass = DecryptPassword(password);
                        if(decodePass == user.Password)
                        {
                            loginRes.Token = GenerateJwtToken(loginRes.EmailId, loginRes.UserId);
                            return loginRes;
                        }
                    }
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return default;
        }

        public string ForgotPassword(string emailId)
        {
            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand command = new SqlCommand("spForget", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", emailId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var userId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);

                            string token = GenerateJwtToken(emailId, userId);
                            this.MSMQSend("Link for resetting the password "+ token);
                            this.SendEmail(emailId);
                            return "We will send you an email for resetting password";
                        }
                    }
                    else
                        return null;

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return default;
        }
        public string ResetPassword(ResetPassModel user)
        {
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spResetPassword", connection);
                command.CommandType = CommandType.StoredProcedure;

                var encryptedPass = EncryptPassword(user.Password);

                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", encryptedPass);

                connection.Open();
                var result = command.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Password Updated";
                }
                else
                {
                    return "Failed to update password";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserRegisterModel GetUserById(int userId)
        {
            UserRegisterModel user = new UserRegisterModel();
            using SqlConnection connection = new SqlConnection(Configuration["ConnectionString:BookStore"]);
            try
            {
                SqlCommand command = new SqlCommand("spGetUserById", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = ReadUserDetails(user, reader);
                        return user;
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
        public static UserRegisterModel ReadUserDetails(UserRegisterModel user, SqlDataReader reader)
        {
            user.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
            user.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
            user.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
            user.MobileNumber = Convert.ToInt64(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);

            return user;
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodeData = Convert.ToBase64String(encData_byte);
                return encodeData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Encoding" + e.Message);
            }
        }
        public static string DecryptPassword(string encodedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Decoding Password" + e.Message);
            }
        }
        public static string GenerateJwtToken(string emailID, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THISISKEYTOGENERATETOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, emailID),
                    new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = "https://localhost:44378/",
                Audience = "https://localhost:44378/",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool SendEmail(string email)
        {
            string linkToBeSend = this.ReceiveQueue();
            if (this.SendMailUsingSMTP(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }
        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }
        private void MSMQSend(string url)
        {
            try
            {
                MessageQueue messageQueue = this.QueueDetail();
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = url;
                messageQueue.Label = "url link";
                messageQueue.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string ReceiveQueue()
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            return linkToBeSend;
        }
        private bool SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress("ani964449@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset you password for Bookstore Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("ani964449@gmail.com", "fhznoukckoiqbxvn");
            smtp.Send(mailMessage);
            return true;
        }
    }
}
