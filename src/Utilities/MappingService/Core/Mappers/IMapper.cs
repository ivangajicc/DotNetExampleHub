namespace Utilities.MappingService.Core.Mappers;

// For simplicity core/domain and application layers are merged. Otherwise everything related to mappers mostly goes to application layer.
public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource entity);
}
