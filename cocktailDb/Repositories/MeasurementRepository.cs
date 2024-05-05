namespace CocktailDb.Repositories;

public interface IMeasurementRepository
{
    Task<IEnumerable<Measurement>> GetAllMeasurementsAsync();
    Task<Measurement> GetMeasurementByIdAsync(int id);
}
public class MeasurementRepository : IMeasurementRepository
{

        private readonly CocktailContext _context;

    public MeasurementRepository(CocktailContext context)
    {
        _context = context;
    }

    //GetAllMeasurementsAsync
    public async Task<IEnumerable<Measurement>> GetAllMeasurementsAsync()
    {
        return await _context.Measurements.Where(measurement => !measurement.IsDeleted).ToListAsync();
    }

    //GetMeasurementByIdAsync
    public async Task<Measurement> GetMeasurementByIdAsync(int id)
    {
        return await _context.Measurements.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
    }
}