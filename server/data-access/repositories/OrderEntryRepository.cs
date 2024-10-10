using System.Collections;
using data_access.data_transfer_objects;
using data_access.interfaces;
using data_access.models;
using Microsoft.EntityFrameworkCore;

namespace data_access.repositories;

public class OrderEntryRepository : IOrderEntryRepository
{
    private MyDbContext _myDbContext;
    
    public OrderEntryRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<IEnumerable<OrderEntry>> CreateOrderEntries(
        IEnumerable<CreateOrderEntryDto> createOrderEntryDtos)
    {
        IEnumerable<OrderEntry> orderEntriesToCreate =
            createOrderEntryDtos.Select(createOrderEntryDto => new OrderEntry
            {
                Quantity = createOrderEntryDto.Quantity,
                ProductId = createOrderEntryDto.ProductId,
                OrderId = createOrderEntryDto.OrderId
            });

        var createdEntries = orderEntriesToCreate.ToList();
        await _myDbContext.OrderEntries.AddRangeAsync(createdEntries);

        await _myDbContext.SaveChangesAsync();
        
        return createdEntries;
    }
}