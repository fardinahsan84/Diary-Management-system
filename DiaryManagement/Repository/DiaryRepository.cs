using DiaryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalDiary.Repository
{
    public class DiaryRepository : Repository<Diary>, IDiaryRepository
    {
        public IEnumerable<Diary> GetHighestPaid()
        {
            throw new NotImplementedException();
        }
    }
}