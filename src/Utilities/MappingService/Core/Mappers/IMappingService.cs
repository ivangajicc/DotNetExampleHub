namespace Utilities.MappingService.Core.Mappers;

public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource entity);
}
