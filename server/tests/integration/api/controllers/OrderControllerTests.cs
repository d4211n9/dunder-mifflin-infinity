using System.Net;
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
        Order mockOrder = await CreateMockOrder(FakeCustomers.FakeCustomer1, FakeOrders.FakeOrder1);
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
    [InlineData(1, 5, 5, 1)]
    [InlineData(2, 2, 2, 3)]
    [InlineData(3, 5, 0, 1)]
    public async void GetOrdersForCustomer_EnsurePaginationWorksTest(
        int pageNumber, 
        int pageSize,
        int expectedSelectionSize,
        int expectedTotalPages)
    {
        // Arrange
        var client = CreateClient();
        await CreateMockOrdersTiedToMockCustomer(FakeCustomers.FakeCustomer1);

        // Act
        var response = await client.GetAsync("/api/" + 
                                             nameof(Order) + 
                                             "/customer?" +
                                             "customerId=" + FakeCustomers.FakeCustomer1.Id +
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

    [Fact]
    public async void GetCustomerOrders_EnsureFilteringWorksTest()
    {
        // Arrange
        var client = CreateClient();
        int expectedSelectionSize = 2;
        CustomerOrdersSearchDto customerOrdersSearchDto = new CustomerOrdersSearchDto
        {
            MaxAmount = 500,
            MinAmount = 0,
            DeliveryTimeFrameDto = new DeliveryTimeFrameDto
            {
                DeliveryUntil = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(6),
                DeliverySince = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(0)
            },
            OrderTimeFrameDto = new OrderTimeFrameDto
            {
                OrderTimeUntil = DateTime.UtcNow.AddHours(6),
                OrderTimeSince = DateTime.UtcNow.AddHours(0)
            },
            OrderStatus = "Delivered",
            PaginationDto = new PaginationDto
            {
                PageNumber = 1,
                PageSize = 500
            }
        };
        JsonContent jsonContent = JsonContent.Create(customerOrdersSearchDto);
        await CreateMockOrdersTiedToMockCustomer(FakeCustomers.FakeCustomer1);

        // Act
        var response = await client.PostAsync("/api/" + 
                                             nameof(Order), jsonContent)
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
        Assert.Equal(expectedSelectionSize, selectionWithPaginationDto.Selection
            .Count(order => order.TotalAmount <= customerOrdersSearchDto.MaxAmount &&
                             order.TotalAmount >= customerOrdersSearchDto.MinAmount &&
                             order.DeliveryDate <= customerOrdersSearchDto.DeliveryTimeFrameDto.DeliveryUntil &&
                             order.DeliveryDate >= customerOrdersSearchDto.DeliveryTimeFrameDto.DeliverySince &&
                             order.OrderDate <= customerOrdersSearchDto.OrderTimeFrameDto.OrderTimeUntil &&
                             order.OrderDate >= customerOrdersSearchDto.OrderTimeFrameDto.OrderTimeSince &&
                             order.Status.Equals(customerOrdersSearchDto.OrderStatus)));

    }

    [Fact]
    public async void CreateOrderWithOrderEntries_StatusCodeIs200OkTest()
    {
        // Arrange
        var client = CreateClient();
        await CreateMockCustomer(FakeCustomers.FakeCustomer1);
        await CreateMockPaper(FakePapers.FakePaper1);
        var content = new CreateOrderWithOrderEntriesDto
        {
            CustomerId = FakeCustomers.FakeCustomer1.Id,
            OrderEntryDtos = new List<CreateOrderEntryWithoutOrderIdDto>
            {
                new()
                {
                    ProductId = FakePapers.FakePaper1.Id,
                    Quantity = 10
                }
            }
        };
        JsonContent jsonContent = JsonContent.Create(content);

        // Act
        var response = await client.PostAsync("/api/Order/create", jsonContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    private async Task<Order> CreateMockOrder(Customer mockCustomer, Order mockOrder)
    {
        await CreateMockCustomer(mockCustomer);
        var result = await _setup.DbContextInstance.Orders
            .AddAsync(mockOrder);

        await _setup.DbContextInstance.SaveChangesAsync();
        
        return result.Entity;
    }

    private async Task CreateMockOrdersTiedToMockCustomer(Customer mockCustomer)
    {
        await CreateMockCustomer(mockCustomer);
        IEnumerable<Order> fakeOrdersTiedToFakeCustomer = GetAndSetMockOrdersTiedToMockCustomer(mockCustomer);

        await _setup.DbContextInstance.Orders
            .AddRangeAsync(fakeOrdersTiedToFakeCustomer);

        await _setup.DbContextInstance.SaveChangesAsync();
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

    private async Task CreateMockCustomer(Customer mockCustomer)
    {
        var result = await _setup.DbContextInstance.Customers
            .AddAsync(mockCustomer);
    }
    
    private async Task CreateMockPaper(Paper mockPaper)
    {
        var result = await _setup.DbContextInstance.Papers
            .AddAsync(mockPaper);
    }
}