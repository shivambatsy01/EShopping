using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Mappers;

public static class MapperExtension
{
    // Lazy<IMapper> vs Default IMapper:
       //
       // - Default IMapper is created and managed by the DI container at application startup.
       // - Lazy<IMapper> is initialized only on first use (lazy loading), reducing startup overhead.
       // - Lazy<IMapper> acts like a singleton (static instance) and doesn’t require dependency injection.
       // - Useful in libraries, static contexts, or when DI is not available.
       // - Ensures mapper configuration is loaded only once and reused everywhere.
       
       /*
        * Lazy<T> in .NET is thread-safe by default.
          The first thread that touches Mapper will run the initialization block (new MapperConfiguration(...)).
          Other threads will wait until initialization completes.
        */
       
    
    private static ILoggerFactory loggerFactory = new LoggerFactory(); 
    
    // Lazy initialization of IMapper
    private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<MappingProfiles>(); // load your mappings
        }, loggerFactory);

        return config.CreateMapper(); // return the IMapper instance
    });

    // Expose the IMapper instance
    public static IMapper Mapper => _lazy.Value;
}