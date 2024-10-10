#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=postgres;User Id=postgres;Password=password" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --context-dir . \
  --context MyDbContext \
  --no-onconfiguring \
  --data-annotations \
  --force