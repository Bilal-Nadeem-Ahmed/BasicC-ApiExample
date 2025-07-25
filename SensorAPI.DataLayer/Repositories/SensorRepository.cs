using Microsoft.EntityFrameworkCore;
using SensorAPI.DataLayer.Context;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorAPI.DataLayer.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly AppDbContext _context;

        public SensorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensors() => await _context.Sensors.ToListAsync();
        public async Task<Sensor?> GetSensorById(int id) => await _context.Sensors.FindAsync(id);
        public async Task<Sensor?> GetSensorByName(string name) => await _context.Sensors.FirstOrDefaultAsync(s => s.name == name);
        public async Task<Sensor> AddSensor(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return sensor;
        }

    }
}
