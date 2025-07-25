using Microsoft.EntityFrameworkCore;
using SensorAPI.DataLayer.Context;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorAPI.DataLayer.Repositories
{
    public class SensorRecordRepository : ISensorRecordRepository
    {
        private readonly AppDbContext _context;

        public SensorRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SensorRecord>> GetAllSensorRecords(DateTime? minDate, DateTime? maxDate, float? minValue, float? maxValue)
        {
            var query = _context.SensorRecords.AsQueryable();

            if (minDate.HasValue)
            {
                query = query.Where(record => record.date >= minDate.Value);
            }
                

            if (maxDate.HasValue)
            {
                query = query.Where(record => record.date <= maxDate.Value);
            }

            if (minValue.HasValue)
            {
                query = query.Where(record => record.value >= minValue.Value);
            }

            if (maxValue.HasValue)
            {
               query = query.Where(record => record.value <= maxValue.Value);
            }
             
            return await query.ToListAsync();

        }
        public async Task<SensorRecord?> GetSensorRecordById(int id) => await _context.SensorRecords.FindAsync(id);
      
        public async Task<SensorRecord> AddSensorRecord(SensorRecord sensorRecord)
        {
            _context.SensorRecords.Add(sensorRecord);
            await _context.SaveChangesAsync();
            return sensorRecord;
        }

    }
}
