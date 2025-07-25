using SensorAPI.BusinessLayer.Exceptions;
using SensorAPI.DataLayer.Repositories.Interfaces;
using SensorAPI.Models.Models;

namespace SensorAPI.BusinessLayer.Services
{
    public class SensorRecordService : ISensorRecordService
    {
        private readonly ISensorRecordRepository _sensorRecordRepository;
        private readonly ISensorService _sensorService;

        public SensorRecordService(ISensorRecordRepository sensorRecordRepository, ISensorService sensorService)
        {
            _sensorRecordRepository = sensorRecordRepository;
            _sensorService = sensorService;
        }

        public async Task<IEnumerable<SensorRecord>> GetAllSensorRecords(DateTime? minDate, DateTime? maxDate, float? minValue, float? maxValue) 
        { 
           return await _sensorRecordRepository.GetAllSensorRecords(minDate, maxDate,  minValue,  maxValue);
        }
        public async Task<SensorRecord?> GetSensorRecordById(int id) 
        {
            var sensorRecord = await _sensorRecordRepository.GetSensorRecordById(id);

            if (sensorRecord == null)
            {
                throw new NotFoundException($"SensorRecord with id {id} was not found.");
            }

            return sensorRecord;
        } 
        public async Task<SensorRecord> AddSensorRecord(SensorRecord sensorRecord)
        {
            var checkingForExistingSensor = await _sensorService.GetSensorByName(sensorRecord.sensor);

            return await _sensorRecordRepository.AddSensorRecord(sensorRecord);
        }

    }
}
