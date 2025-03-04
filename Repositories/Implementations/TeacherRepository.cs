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
    public class TeacherRepository : ITeacherInterface
    {
        private readonly NpgsqlConnection _conn;
        public TeacherRepository(NpgsqlConnection connection)
        {
            _conn = connection;
        }

        public async Task<List<t_teacher>> GetAll()
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from t_teacher as t join t_user as u on t.c_user_id = u.c_user_id;", _conn);
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader datar = cmd.ExecuteReader();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }
            List<t_teacher> teacherList = new List<t_teacher>();
            teacherList = (from DataRow dr in dt.Rows
                           select new t_teacher()
                           {
                               c_teacher_id = Convert.ToInt32(dr["c_teacher_id"]),
                               c_user_id = int.Parse(dr["c_user_id"].ToString()),
                               c_teacher_name = dr["c_username"].ToString(),
                               c_email = dr["c_email"].ToString(),

                               c_phone_number = dr["c_phone_number"].ToString(),
                               c_date_of_birth = Convert.ToDateTime(dr["c_date_of_birth"]),
                               c_qualification = dr["c_qualification"].ToString(),
                               c_experience = Convert.ToInt32(dr["c_experience"]),
                               c_subject_expertise = dr["c_subject_expertise"].ToString(),
                               c_status = dr["c_status"].ToString()
                           }).ToList();
            _conn.Close();
            return teacherList;

        }



        public async Task<t_teacher> GetOne(string teacherid)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_teacher WHERE c_teacher_id = @c_teacher_id", _conn);
            cmd.Parameters.AddWithValue("@c_teacher_id", int.Parse(teacherid));
            _conn.Close();
            _conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            t_teacher teacher = null;
            if (dr.Read())
            {
                teacher = new t_teacher()
                {
                    c_teacher_id = Convert.ToInt32(dr["c_teacher_id"]),
                    c_user_id = int.Parse(dr["c_user_id"].ToString()),
                    c_phone_number = dr["c_phone_number"].ToString(),
                    c_date_of_birth = Convert.ToDateTime(dr["c_date_of_birth"]),
                    c_qualification = dr["c_qualification"].ToString(),
                    c_experience = Convert.ToInt32(dr["c_experience"]),
                    c_subject_expertise = dr["c_subject_expertise"].ToString(),
                    c_status = dr["c_status"].ToString()
                };
            }
            _conn.Close();
            return teacher;
        }

        public async Task<int> UpdateStatus(string teacherId, string status)
        {
            try
            {

                await _conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(@"UPDATE t_teacher SET 
                                                c_status = @c_status
                                                WHERE c_teacher_id = @c_teacher_id", _conn))
                {
                    cmd.Parameters.AddWithValue("@c_status", status);
                    cmd.Parameters.AddWithValue("@c_teacher_id", int.Parse(teacherId));

                    await cmd.ExecuteNonQueryAsync();
                    return 1; // Success
                }

            }
            catch (Exception ex)
            {
                return 0; // Failure
            }
        }

        public async Task<int> Delete(string teacherid)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"DELETE FROM t_teacher WHERE c_teacher_id = @c_teacher_id", _conn);

                cmd.Parameters.AddWithValue("@c_teacher_id", int.Parse(teacherid));
                _conn.Close();
                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }


        }
        public async Task<int> GetPendingTeacherCount()
        {
            try
            {
                await _conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM t_teacher WHERE c_status = 'Pending'", _conn))
                {
                    var count = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(count);
                }
            }
            catch (Exception ex)
            {
                // Log the error
                return 0;
            }
            finally
            {
                await _conn.CloseAsync();
            }
        }
        public async Task<int> GetTeacherCount()
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM t_teacher", _conn);
                if (_conn.State != System.Data.ConnectionState.Open)
                    await _conn.OpenAsync();
                int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                // _conn.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching teacher count: " + ex.Message);
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