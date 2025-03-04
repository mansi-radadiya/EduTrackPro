using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Implementations
{
    public class StudentRepository : IStudentInterface
    {
    //     private readonly NpgsqlConnection _conn;
    //     public StudentRepository(NpgsqlConnection connection)
    //     {
    //         _conn = connection;
    //     }

    //     public async Task<List<t_class>> GetAllClass()
    //     {

    //         DataTable dt = new DataTable();
    //         NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_class_id, c_class_name from t_class", _conn);
    //         _conn.Close();
    //         _conn.Open();
    //         NpgsqlDataReader datar = cmd.ExecuteReader();

    //         if (datar.HasRows)
    //         {
    //             dt.Load(datar);
    //         }

    //         List<t_class> Class = new List<t_class>();

    //         Class = (from DataRow dr in dt.Rows
    //                  select new t_class()
    //                  {
    //                      c_class_name = dr["c_class_name"].ToString(),
    //                      c_class_id = Convert.ToInt32(dr["c_class_id"])
    //                  }).ToList();

    //         _conn.Close();
    //         return Class;

    //     }

    //     public async Task<List<t_section>> GetSectionByClass(string classId)
    //     {
    //         DataTable dt = new DataTable();
    //         NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_section_id, c_class_id, c_section_name FROM t_section WHERE c_class_id = @c_class_id", _conn);

    //         cmd.Parameters.AddWithValue("@c_class_id", int.Parse(classId));
    //         _conn.Close();
    //         _conn.Open();
    //         NpgsqlDataReader datar = cmd.ExecuteReader();

    //         if (datar.HasRows)
    //         {
    //             dt.Load(datar);
    //         }

    //         List<t_section> section = new List<t_section>();

    //         section = (from DataRow dr in dt.Rows
    //                    select new t_section()
    //                    {
    //                        c_section_name = dr["c_section_name"].ToString(),
    //                        c_section_id = Convert.ToInt32(dr["c_section_id"]),
    //                        c_class_id = Convert.ToInt32(dr["c_class_id"])

    //                    }).ToList();

    //         _conn.Close();
    //         return section;
    //     }

      
    //     public async Task<int> Register(t_student student)
    //     {
    //         try
    //         {

    //             await _conn.CloseAsync();
    //             NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_student WHERE c_email = @c_email ;", _conn);
    //             comcheck.Parameters.AddWithValue("@c_email", student.c_email);
    //             await _conn.OpenAsync();
    //             using (NpgsqlDataReader datadr = await comcheck.ExecuteReaderAsync())
    //             {
    //                 if (datadr.HasRows)
    //                 {

    //                     await _conn.CloseAsync();
    //                     return 0;
    //                 }
    //                 else
    //                 {
    //                     await _conn.CloseAsync();
    //                     NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO t_student
    //         (c_user_id,c_full_name, c_email,c_password,c_date_of_birth,c_gender, c_class_id,c_section_id,  c_guardian_details,c_enrollment_date,c_image,c_status)
    //         VALUES (@c_user_id, @c_full_name, @c_email,@c_password,@c_date_of_birth,@c_gender,@c_class_id,@c_section_id,@c_guardian_details, @c_enrollment_date,@c_image, @c_status)", _conn);
    //                     cmd.Parameters.AddWithValue("@c_user_id", student.c_user_id);
    //                     cmd.Parameters.AddWithValue("@c_full_name", student.c_full_name);
    //                     cmd.Parameters.AddWithValue("@c_email", student.c_email);
    //                     cmd.Parameters.AddWithValue("@c_password", student.c_password);

    //                     cmd.Parameters.AddWithValue("@c_date_of_birth", student.c_date_of_birth);
    //                     cmd.Parameters.AddWithValue("@c_gender", student.c_gender);
    //                     cmd.Parameters.AddWithValue("@c_class_id", student.c_class_id);
    //                     cmd.Parameters.AddWithValue("@c_section_id", student.c_section_id);
    //                     cmd.Parameters.AddWithValue("@c_guardian_details", student.c_guardian_details);
    //                     cmd.Parameters.AddWithValue("@c_enrollment_date", student.c_enrollment_date);

    //                     cmd.Parameters.AddWithValue("@c_image", student.c_image == null ? DBNull.Value : student.c_image);
    //                     cmd.Parameters.AddWithValue("@c_status", student.c_status);

    //                     await _conn.OpenAsync();
    //                     await cmd.ExecuteNonQueryAsync();
    //                     await _conn.CloseAsync();
    //                     return 1;
    //                 }
    //             }
    //         }
    //         catch (Exception e)
    //         {
    //             await _conn.CloseAsync();
    //             Console.WriteLine("Register Failed , Error :- " + e.Message);
    //             return -1;
    //         }

    //     }

    private readonly NpgsqlConnection _conn;
        public StudentRepository(NpgsqlConnection connection)
        {
            _conn = connection;
        }

        public async Task<List<t_class>> GetAllClass()
        {

            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_class_id, c_class_name from t_class", _conn);
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader datar = cmd.ExecuteReader();

            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            List<t_class> Class = new List<t_class>();

            Class = (from DataRow dr in dt.Rows
                     select new t_class()
                     {
                         c_class_name = dr["c_class_name"].ToString(),
                         c_class_id = Convert.ToInt32(dr["c_class_id"])
                     }).ToList();

            _conn.Close();
            return Class;

        }

        public async Task<object> GetAllTeacher()
        {

            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from t_user as u join t_teacher as t on t.c_user_id = u.c_user_id;", _conn);
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader datar = cmd.ExecuteReader();

            if (datar.HasRows)
            {
                dt.Load(datar);
            }


           var  Class = (from DataRow dr in dt.Rows
                     select new 
                     {
                         c_teacher_name = dr["c_username"].ToString(),
                         c_teacher_id = Convert.ToInt32(dr["c_teacher_id"])
                     }).ToList();

            _conn.Close();
            return Class;

        }

        public async Task<List<t_section>> GetSectionByClass(string classId)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_section_id, c_class_id, c_section_name FROM t_section WHERE c_class_id = @c_class_id", _conn);

            cmd.Parameters.AddWithValue("@c_class_id", int.Parse(classId));
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader datar = cmd.ExecuteReader();

            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            List<t_section> section = new List<t_section>();

            section = (from DataRow dr in dt.Rows
                       select new t_section()
                       {
                           c_section_name = dr["c_section_name"].ToString(),
                           c_section_id = Convert.ToInt32(dr["c_section_id"]),
                           c_class_id = Convert.ToInt32(dr["c_class_id"])

                       }).ToList();

            _conn.Close();
            return section;
        }

        public async Task<int> Register(t_student student)
        {
            try
            {

                await _conn.CloseAsync();
                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_student WHERE c_email = @c_email ;", _conn);
                comcheck.Parameters.AddWithValue("@c_email", student.c_email);
                await _conn.OpenAsync();
                using (NpgsqlDataReader datadr = await comcheck.ExecuteReaderAsync())
                {
                    if (datadr.HasRows)
                    {

                        await _conn.CloseAsync();
                        return 0;
                    }
                    else
                    {
                        await _conn.CloseAsync();
                        NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO t_student
            (c_user_id,c_full_name, c_email,c_password,c_date_of_birth,c_gender, c_class_id,c_section_id,  c_guardian_details,c_enrollment_date,c_image,c_status, c_teacher_id)
            VALUES (@c_user_id, @c_full_name, @c_email,@c_password,@c_date_of_birth,@c_gender,@c_class_id,@c_section_id,@c_guardian_details, @c_enrollment_date,@c_image, @c_status, @c_teacher_id)", _conn);
                        cmd.Parameters.AddWithValue("@c_user_id", student.c_user_id);
                        cmd.Parameters.AddWithValue("@c_full_name", student.c_full_name);
                        cmd.Parameters.AddWithValue("@c_email", student.c_email);
                        cmd.Parameters.AddWithValue("@c_password", student.c_password);

                        cmd.Parameters.AddWithValue("@c_date_of_birth", student.c_date_of_birth);
                        cmd.Parameters.AddWithValue("@c_gender", student.c_gender);
                        cmd.Parameters.AddWithValue("@c_class_id", student.c_class_id);
                        cmd.Parameters.AddWithValue("@c_class_id", student.c_class_id);
                        cmd.Parameters.AddWithValue("@c_teacher_id", student.c_teacher_id);

                        cmd.Parameters.AddWithValue("@c_section_id", student.c_section_id);
                        cmd.Parameters.AddWithValue("@c_guardian_details", student.c_guardian_details);
                        cmd.Parameters.AddWithValue("@c_enrollment_date", student.c_enrollment_date);

                        cmd.Parameters.AddWithValue("@c_image", student.c_image == null ? DBNull.Value : student.c_image);
                        cmd.Parameters.AddWithValue("@c_status", student.c_status);

                        await _conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        await _conn.CloseAsync();
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                await _conn.CloseAsync();
                Console.WriteLine("Register Failed , Error :- " + e.Message);
                return -1;
            }

        }

        public async Task<int> Update(t_student studentData)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE t_student SET c_user_id = @c_user_id, c_full_name = @c_full_name, c_email = @c_email, c_date_of_birth = @c_date_of_birth, c_gender = @c_gender, c_class_id = @c_class_id, c_section_id = @c_section_id, c_teacher_id=@c_teacher_id, c_guardian_details = @c_guardian_details, c_enrollment_date = @c_enrollment_date, c_image = @c_image, c_status = @c_status  WHERE c_student_id = @c_student_id", _conn);
                cmd.Parameters.AddWithValue("@c_user_id", studentData.c_user_id);
                cmd.Parameters.AddWithValue("@c_full_name", studentData.c_full_name);
                cmd.Parameters.AddWithValue("@c_email", studentData.c_email);
                cmd.Parameters.AddWithValue("@c_date_of_birth", studentData.c_date_of_birth);
                cmd.Parameters.AddWithValue("@c_gender", studentData.c_gender);
                cmd.Parameters.AddWithValue("@c_class_id", studentData.c_class_id);
                cmd.Parameters.AddWithValue("@c_section_id", studentData.c_section_id);
                cmd.Parameters.AddWithValue("@c_teacher_id", studentData.c_teacher_id);

                cmd.Parameters.AddWithValue("@c_guardian_details", studentData.c_guardian_details);
                cmd.Parameters.AddWithValue("@c_enrollment_date", studentData.c_enrollment_date);
                cmd.Parameters.AddWithValue("@c_image", studentData.c_image == null ? DBNull.Value : studentData.c_image);
                cmd.Parameters.AddWithValue("@c_status", studentData.c_status);
                cmd.Parameters.AddWithValue("@c_student_id", studentData.c_student_id);

                // _conn.Open();
                // int rowsAffected = cmd.ExecuteNonQuery();
                // _conn.Close();

                // return rowsAffected > 0 ? 1 : 0;
                _conn.Close();
                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                _conn.Close();
                return 0;
            }
        }

        // public async Task<int> Delete(string studentid)
        // {
        //     try
        //     {
        //         NpgsqlCommand cmd = new NpgsqlCommand(@"DELETE FROM t_student WHERE c_student_id = @c_student_id", _conn);

        //         cmd.Parameters.AddWithValue("@c_student_id", int.Parse(studentid));
        //         _conn.Close();
        //         _conn.Open();
        //         cmd.ExecuteNonQuery();
        //         _conn.Close();
        //         return 1;
        //     }
        //     catch (Exception ex)
        //     {
        //         return 0;
        //     }
        // }

        public async Task<int> Delete(string studentid)
        {
            try
            {
                using (var cmd = new NpgsqlCommand(@"DELETE FROM t_student WHERE c_student_id = @c_student_id", _conn))
                {
                    cmd.Parameters.AddWithValue("@c_student_id", int.Parse(studentid));
                    await _conn.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    await _conn.CloseAsync();
                    return rowsAffected > 0 ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting student: " + ex.Message);
                await _conn.CloseAsync();
                return 0;
            }
        }

        public async Task<List<t_student>> GetAll()
        {
            DataTable dt = new DataTable();
            string query = @"Select * from t_student as s join (Select * from t_user as u join t_teacher as t on u.c_user_id = t.c_user_id) as us on s.c_teacher_id = us.c_teacher_id join t_class as c on c.c_class_id = s.c_class_id join t_section as sec on s.c_section_id = sec.c_section_id;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, _conn))
            {
                try
                {
                    _conn.Close();
                    _conn.Open();
                    using (NpgsqlDataReader datar = await cmd.ExecuteReaderAsync())
                    {
                        if (datar.HasRows)
                        {
                            dt.Load(datar);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    _conn.Close();
                }
            }

            List<t_student> studentList = (from DataRow dr in dt.Rows
                                           select new t_student()
                                           {
                                               c_student_id = Convert.ToInt32(dr["c_student_id"]),
                                               c_user_id = Convert.ToInt32(dr["c_user_id"]),
                                               c_full_name = dr["c_full_name"].ToString(),
                                               c_email = dr["c_email"].ToString(),
                                               c_password = dr["c_password"].ToString(),
                                               c_date_of_birth = Convert.ToDateTime(dr["c_date_of_birth"]),
                                               c_gender = dr["c_gender"].ToString(),
                                               c_class_id = Convert.ToInt32(dr["c_class_id"]),
                                               c_class_name = dr["c_class_name"].ToString(),
                                               c_section_id = Convert.ToInt32(dr["c_section_id"]),
                                               c_section_name = dr["c_section_name"].ToString(),
                                                c_teacher_id = Convert.ToInt32(dr["c_teacher_id"]),
                                                c_teacher_name = dr["c_username"].ToString(),

                                               c_guardian_details = dr["c_guardian_details"].ToString(),
                                               c_enrollment_date = Convert.ToDateTime(dr["c_enrollment_date"]),
                                               c_created_at = Convert.ToDateTime(dr["c_created_at"]),
                                               c_image = dr["c_image"] == DBNull.Value ? null : dr["c_image"].ToString(),
                                               c_status = Convert.ToBoolean(dr["c_status"])
                                           }).ToList();

            return studentList;
        }

        public async Task<t_student> GetOne(string studentid)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_student WHERE c_student_id = @c_student_id", _conn);
            cmd.Parameters.AddWithValue("@c_student_id", int.Parse(studentid));
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            t_student student = null;
            // bdfgh
            if (dr.Read())
            {
                student = new t_student()
                {
                    c_student_id = Convert.ToInt32(dr["c_student_id"]),
                    c_user_id = Convert.ToInt32(dr["c_user_id"]),
                    c_full_name = dr["c_full_name"].ToString(),
                    c_email = dr["c_email"].ToString(),
                    c_password = dr["c_password"].ToString(),
                    c_date_of_birth = Convert.ToDateTime(dr["c_date_of_birth"]),
                    c_gender = dr["c_gender"].ToString(),
                    c_class_id = Convert.ToInt32(dr["c_class_id"]),
                    c_section_id = Convert.ToInt32(dr["c_section_id"]),
                    c_guardian_details = dr["c_guardian_details"].ToString(),
                    c_enrollment_date = Convert.ToDateTime(dr["c_enrollment_date"]),
                    c_created_at = Convert.ToDateTime(dr["c_created_at"]),
                    c_image = dr["c_image"] == DBNull.Value ? null : dr["c_image"].ToString(),
                    c_status = Convert.ToBoolean(dr["c_status"])
                };
            }
            _conn.Close();
            return student;
        }

    
    public async Task<List<t_list>> GetData()
        {
            var values = new List<t_list>();

            if (_conn.State != System.Data.ConnectionState.Open)
                await _conn.OpenAsync();  // Use OpenAsync for better performance

            var query = @"
                SELECT 
                    'C_' || c.c_class_id AS id,  
                    c.c_class_name AS name, 
                    NULL AS parentId 
                FROM t_class c

                UNION ALL

                SELECT 
                    'S_' || s.c_section_id AS id,  
                    s.c_section_name AS name, 
                    'C_' || s.c_class_id AS parentId  
                FROM t_section s

                UNION ALL

                SELECT 
                    'ST_' || st.c_student_id AS id,  
                    st.c_full_name AS name, 
                    'S_' || st.c_section_id AS parentId  
                FROM t_student st

                ORDER BY parentId NULLS FIRST;
            ";

            using (var cmd = new NpgsqlCommand(query, _conn))
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        values.Add(new t_list
                        {
                            Id = reader.GetString(0),  // 👈 Change to string
                            Name = reader.GetString(1),
                            ParentId = reader.IsDBNull(2) ? null : reader.GetString(2)  // 👈 Change to string
                        });
                    }
                }
            }
            _conn.Close();
            return values;
        }

        public async Task<int> GetStudentCount()
        {
            try
            {
                // _conn.Close();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM t_student", _conn);
                if (_conn.State != System.Data.ConnectionState.Open)
                    await _conn.OpenAsync();
                int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                // _conn.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching student count: " + ex.Message);
                // _conn.Close();
                return 0;
            }
            finally
            {
                if (_conn.State == System.Data.ConnectionState.Open)
                    await _conn.CloseAsync();
            }
        }

    
    }
}