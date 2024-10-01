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

    private async Task<Order> CreateMockOrder()
    {
        Customer mockCustomer = await CreateMockCustomer();
        var result = await _setup.DbContextInstance.Orders
            .AddAsync(FakeOrders.FakeOrder1);

        await _setup.DbContextInstance.SaveChangesAsync();
        
        return result.Entity;
    }

    private async Task<Customer> CreateMockCustomer()
    {
        var result = await _setup.DbContextInstance.Customers
            .AddAsync(FakeOrders.FakeOrder1.Customer!);

        return result.Entity;
    }
}