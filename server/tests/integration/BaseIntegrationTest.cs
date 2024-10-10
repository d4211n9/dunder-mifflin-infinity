using api;
using data_access;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using Xunit.Abstractions;

namespace tests.integration;

public class BaseIntegrationTest : WebApplicationFactory<Program>
{
    protected readonly PgCtxSetup<MyDbContext> _setup = new();
    protected readonly ITestOutputHelper _testOutputHelper;

    protected BaseIntegrationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Environment.SetEnvironmentVariable("conn", _setup._postgres.GetConnectionString());
    }
}