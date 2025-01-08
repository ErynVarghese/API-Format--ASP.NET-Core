using API_main.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API_main.Utility;


namespace API_main.Repositories
{

    public class UserRepo
    {
        SqlCommand cmd = null;

        private readonly string _connectionString;

        public UserRepo()
        {
            _connectionString = ConnectionString.CName;
        }

        public List<UserModel> GetAll()
        {
            List<UserModel> oList = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", 0);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        UserModel obj = new UserModel();
                        obj.User_ID = sdr.GetInt32(0);
                        obj.User_code = sdr.GetString(1);
                        obj.Name = sdr.GetString(2);
                        obj.Email = sdr.GetString(3);
                        obj.Phone = sdr.GetString(4);
                        obj.Address = sdr.GetString(7);
                        obj.LocationName = sdr.GetString(8);
                    
                        oList.Add(obj);
                    }

                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex.InnerException;
                }
                finally
                {
                    con.Close();
                }
            }
            return oList;
        }


        public UserModel GetByID(int Id)
        {
            UserModel obj = new UserModel();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_ID", Id);
                    cmd.Parameters.AddWithValue("@Mode", 4);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        obj.User_ID = sdr.GetInt32(0);
                        obj.User_code = sdr.GetString(1);
                        obj.Name = sdr.GetString(2);
                        obj.Email = sdr.GetString(3);
                        obj.Phone = sdr.GetString(4);
                        obj.User_name = sdr.GetString(5);
                        obj.Password = sdr.GetString(6);
                        obj.Address = sdr.GetString(7);
                        obj.LocationName = sdr.GetString(8);
                        obj.ServiceID = sdr.GetInt32(9);
                        obj.UserTypeID = sdr.GetInt32(10);
                        obj.Active = sdr.GetBoolean(11);
                        obj.SLAID = sdr.GetInt32(13);
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            return obj;
        }


        public string Update(UserModel obj)
        {
            string result = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@User_ID", obj.User_ID);
                    cmd.Parameters.AddWithValue("@User_code", obj.User_code);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@User_name", obj.User_name);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@Location", obj.LocationName);
                    cmd.Parameters.AddWithValue("@Service_type", obj.ServiceID);
                    cmd.Parameters.AddWithValue("@User_type", obj.UserTypeID);
                    cmd.Parameters.AddWithValue("@SLA", obj.SLAID);
                    cmd.Parameters.AddWithValue("@Active", obj.Active);
                    cmd.Parameters.AddWithValue("@Modifiedby", obj.Modifiedby);
                    cmd.Parameters.AddWithValue("@ModifiedDate", obj.ModifiedDate);
                    cmd.Parameters.AddWithValue("@Mode", 2);

                    int status = cmd.ExecuteNonQuery();
                    if (status > 0)
                    {
                        result = "Success";
                    }
                    else
                    {
                        result = "Fail";
                    }

                }
                catch (Exception)
                {
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            return result;
        }

        public string Delete(int Id)
        {
            string result = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@User_ID", Id);
                    cmd.Parameters.AddWithValue("@Mode", 3);

                    int status = cmd.ExecuteNonQuery();
                    if (status > 0)
                    {
                        result = "Success";
                    }
                    else
                    {
                        result = "Fail";
                    }

                }
                catch (Exception)
                {
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            return result;
        }

        public string Create(UserModel obj)
        {
            string result = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@User_code", obj.User_code);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@User_name", obj.User_name);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@Location", obj.LocationName);
                    cmd.Parameters.AddWithValue("@Service_type", obj.ServiceID);
                    cmd.Parameters.AddWithValue("@User_type", obj.UserTypeID);
                    cmd.Parameters.AddWithValue("@SLA", obj.SLAID);
                    cmd.Parameters.AddWithValue("@Active", obj.Active);
                    cmd.Parameters.AddWithValue("@Createdby", obj.Createdby);
                    cmd.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);
                    cmd.Parameters.AddWithValue("@Mode", 1);

                    int status = cmd.ExecuteNonQuery();
                    if (status > 0)
                    {
                        result = "Success";
                    }
                    else
                    {
                        result = "Fail";
                    }

                }
                catch (Exception)
                {
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            return result;
        }

        public string GetMaxUserCode()
        {
            string code = string.Empty;


            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Usp_MaxUserCode", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    code = ds.Tables[0].Rows[0]["Usercode"].ToString();
                }
                catch (Exception)
                {
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            return code;
        }

        public string AddImage(int empid, string filename)
        {
            string result = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();

                    cmd = new SqlCommand("Usp_User", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@User_ID", empid);
                    cmd.Parameters.AddWithValue("@Image", filename);
                    cmd.Parameters.AddWithValue("@Mode", 9);

                    int status = cmd.ExecuteNonQuery();

                    if (status >= 0)
                    {
                        result = "Success";
                    }

                    else
                    {
                        result = "Fail";
                    }

                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex.InnerException;
                }
                finally
                {
                    con.Close();
                }
            }

            return result;

        }


        internal string GetEmployeeImage(int empid)
        {
            string image = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                try
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("Usp_User", con);
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.AddWithValue("@User_ID", empid);
                    checkCmd.Parameters.AddWithValue("@mode", 10); ;



                    SqlDataReader reader = checkCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        image = reader.GetString(0);

                    }


                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex.InnerException;
                }
                finally
                {
                    con.Close();
                }
            }

            return image;

        }

    }
}
