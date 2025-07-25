using SensorAPI.Models.Models;

namespace SensorAPI.DataLayer.Repositories.Interfaces
{
    public interface ISensorRecordRepository
    {
        Task<IEnumerable<SensorRecord>> GetAllSensorRecords(DateTime? minDate, DateTime? maxDate, float? minValue, float? maxValue);
        Task<SensorRecord?> GetSensorRecordById(int id);
        Task<SensorRecord> AddSensorRecord(SensorRecord sensorRecord);
    }
}
