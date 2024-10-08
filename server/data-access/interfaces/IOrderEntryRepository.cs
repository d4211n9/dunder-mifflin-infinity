using data_access.data_transfer_objects;
using data_access.models;

namespace data_access.interfaces;

public interface IOrderEntryRepository
{
    Task<IEnumerable<OrderEntry>> CreateOrderEntries(IEnumerable<CreateOrderEntryDto> createOrderEntryDtos);
}