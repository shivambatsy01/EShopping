using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Mappers;

public class DiscountMapperExtension
{
    private static ILoggerFactory loggerFactory = new LoggerFactory(); 
    
    // Lazy initialization of IMapper
    private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<DiscountProfile>(); // load your mappings
        }, loggerFactory);

        return config.CreateMapper(); // return the IMapper instance
    });

    // Expose the IMapper instance
    public static IMapper Mapper => _lazy.Value;
}