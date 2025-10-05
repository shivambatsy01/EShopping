using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Mappers;

/*
 * // NOTE: This static MapperExtension provides a lazily initialized singleton IMapper instance.
   // This design removes the need for dependency injection (DI) because:
   // 1. The IMapper is created once and reused across the entire application (thread-safe via Lazy<T>).
   // 2. It can be accessed globally via MapperExtension.Mapper without constructor injection.
   // 3. Suitable for small or simple applications where DI configuration overhead is unnecessary.
   //
   // ⚠️ In larger or test-driven projects, consider using DI (e.g., services.AddAutoMapper())
   // for better testability and maintainability.
 */

public class MapperExtension
{
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