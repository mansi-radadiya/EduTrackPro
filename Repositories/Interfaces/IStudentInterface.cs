using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Interfaces
{
    public interface IStudentInterface
    {

        Task<int> Register(t_student student);
        Task<List<t_student>> GetAll();
        Task<t_student> GetOne(string studentid);
        Task<List<t_class>> GetAllClass();
        Task<List<t_section>> GetSectionByClass(string classId);
        Task<object> GetAllTeacher();

        Task<int> Update(t_student studentData);
        Task<int> Delete(string studentid);
        Task<List<t_list>> GetData();
        Task<int> GetStudentCount();


    }
}