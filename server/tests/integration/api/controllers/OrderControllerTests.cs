using System.Net.Http.Json;
using System.Text.Json;
using data_access;
using data_access.data_transfer_objects;
using data_access.models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using tests.helper;
using Xunit.Abstractions;

namespace tests.integration.api.controllers;

public class OrderControllerTests : BaseIntegrationTest
{
    public OrderControllerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) {}
    
    [Fact]
    public async void ChangeStatus_EnsureStatusActuallyChangedTest()
    {
        // Arrange
        var client = CreateClient();
        string updatedStatus = "Delivered";
        Order mockOrder = await CreateMockOrder();
        string oldStatus = mockOrder.Status;
        ChangeOrderStatusDto changeOrderStatusDto = new ChangeOrderStatusDto()
        {
            OrderId = mockOrder.Id,
            UpdatedStatus = updatedStatus
        };
        JsonContent jsonContent = JsonContent.Create(changeOrderStatusDto);
        
        // Act
        await client.PutAsync("/api/" +
                              nameof(Order) +
                              "/status", jsonContent);
        
        // Assert
        Order? orderFromDb = await _setup.DbContextInstance.Orders.FirstOrDefaultAsync(order => order.Id == mockOrder.Id);
        
        Assert.NotNull(orderFromDb);
        Assert.Equivalent(updatedStatus, orderFromDb.Status);
        Assert.NotEqual(oldStatus, orderFromDb.Status);
    }

    [Theory]
    [InlineData(1, 1, 5, 5, 1)]
    [InlineData(1, 2, 2, 2, 3)]
    [InlineData(1, 3, 5, 0, 1)]
    public async void GetOrdersForCustomer_EnsurePaginationWorksTest(
        int customerId, 
        int pageNumber, 
        int pageSize,
        int expectedSelectionSize,
        int expectedTotalPages)
    {
        // Arrange
        var client = CreateClient();
        await CreateMockOrdersTiedToMockCustomer();

        // Act
        var response = await client.GetAsync("/api/" + 
                                             nameof(Order) + 
                                             "/customer?" +
                                             "customerId=" + customerId +
                                             "&PaginationDto.PageNumber=" + pageNumber + 
                                             "&PaginationDto.PageSize=" + pageSize)
            .Result.Content
            .ReadAsStringAsync();
        
        SelectionWithPaginationDto<Order> selectionWithPaginationDto = JsonSerializer
            .Deserialize<SelectionWithPaginationDto<Order>>(response, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException();
        
        // Assert
        Assert.NotNull(selectionWithPaginationDto);
        Assert.Equal(expectedSelectionSize, selectionWithPaginationDto.Selection.Count());
        Assert.Equal(expectedTotalPages, selectionWithPaginationDto.TotalPages);
    }

    private async Task<Order> CreateMockOrder()
    {
        Customer mockCustomer = await CreateMockCustomer();
        var result = await _setup.DbContextInstance.Orders
            .AddAsync(FakeOrders.FakeOrder1);

        await _setup.DbContextInstance.SaveChangesAsync();
        
        return result.Entity;
    }

    private async Task<IEnumerable<Order>> CreateMockOrdersTiedToMockCustomer()
    {
        Customer mockCustomer = await CreateMockCustomer();
        IEnumerable<Order> fakeOrdersTiedToFakeCustomer = GetAndSetMockOrdersTiedToMockCustomer(mockCustomer);

        await _setup.DbContextInstance.Orders
            .AddRangeAsync(fakeOrdersTiedToFakeCustomer);

        await _setup.DbContextInstance.SaveChangesAsync();

        return fakeOrdersTiedToFakeCustomer;
    }

    private IEnumerable<Order> GetAndSetMockOrdersTiedToMockCustomer(Customer mockCustomer)
    {
        List<Order> mockOrdersTiedToMockCustomer = new List<Order>();
        mockOrdersTiedToMockCustomer.Add(FakeOrders.FakeOrder1);
        mockOrdersTiedToMockCustomer.Add(FakeOrders.FakeOrder2);
        mockOrdersTiedToMockCustomer.Add(FakeOrders.FakeOrder3);
        mockOrdersTiedToMockCustomer.Add(FakeOrders.FakeOrder4);
        mockOrdersTiedToMockCustomer.Add(FakeOrders.FakeOrder5);

        mockOrdersTiedToMockCustomer.ForEach(order =>
        {
            order.CustomerId = mockCustomer.Id;
            order.Customer = mockCustomer;
        });

        return mockOrdersTiedToMockCustomer;
    }

    private async Task<Customer> CreateMockCustomer()
    {
        var result = await _setup.DbContextInstance.Customers
            .AddAsync(FakeOrders.FakeOrder1.Customer!);

        return result.Entity;
    }
}