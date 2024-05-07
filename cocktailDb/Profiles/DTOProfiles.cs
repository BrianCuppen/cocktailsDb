namespace CocktailDb.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Drink, DrinkDTO>()
            .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient))
            .ForMember(dest => dest.Measurement, opt => opt.MapFrom(src => src.Measurement))
            .ForMember(dest => dest.GlassType, opt => opt.MapFrom(src => src.GlassType));
    }
}

public class GlassResolver : IValueResolver<Drink, DrinkDTO, Glass>
{
    public Glass Resolve(Drink source, DrinkDTO destination, Glass destMember, ResolutionContext context)
    {
        Glass glass = source.GlassType;
        return glass;
    }
}

public class IngredientResolver : IValueResolver<Drink, DrinkDTO, Ingredient>
{
    private readonly ICocktailService _cocktailService;
    public IngredientResolver(ICocktailService cocktailService)
    {
        _cocktailService = cocktailService;
    }
    public Ingredient Resolve(Drink source, DrinkDTO destination, Ingredient destMember, ResolutionContext context)
    {

        return _cocktailService.GetIngredientByIdAsync(source.Id).Result;
    }
}

public class MeasurementResolver : IValueResolver<Drink, DrinkDTO, Measurement>
{
    private readonly ICocktailService _cocktailService;
    public MeasurementResolver(ICocktailService cocktailService)
    {
        _cocktailService = cocktailService;
    }
    public Measurement Resolve(Drink source, DrinkDTO destination, Measurement destMember, ResolutionContext context)
    {
        return _cocktailService.GetMeasurementByIdAsync(source.Id).Result;
    }
}




