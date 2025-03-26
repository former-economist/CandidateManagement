using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateManagement.Infrastructure;
using CandidateManagement.Infrastructure.Entity;
using CandidateManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Repositories.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly Context _context;
        public CourseRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAndCentresAsync()
        {
            return await _context.Courses.Include(course => course.Centres).ToListAsync();
        }

        public async Task UpdateCourseCentresAsync(Course course,Centre newCentre)
        {
            course.Centres.Add(newCentre);
            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        //public bool CheckCourseExists(Course courseToMatch)
        //{
        //    var doesCourseExist = _context.Courses.Any(course => course.Id == courseToMatch.Id);

        //    if (doesCourseExist)
        //    {
        //        return true;
        //    }

        //    return false;
            
        //}
    }
}
