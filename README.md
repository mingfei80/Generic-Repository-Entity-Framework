# Generic Repository for Entity Framework Core

## Overview

This repository provides a generic implementation of the Repository Pattern using Entity Framework Core. It is designed to simplify data access logic by offering common CRUD operations for any entity model, reducing code duplication and improving maintainability.

## Features

Generic Implementation: Works with any entity class.

Async Support: Utilizes async/await for efficient database operations.

Queryable Extensions: Supports filtering, tracking options, and includes for navigation properties.

Batch Operations: Add, update, and delete multiple entities at once.

## Installation & Usage

### 1. Install Entity Framework Core

Ensure that your project has EF Core installed:

```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

### 2. Implement in Your Project

Define Your DbContext
```
public class AveryLamDbContext : DbContext
{
    public AveryLamDbContext(DbContextOptions<AveryLamDbContext> options) : base(options) { }
    
    public DbSet<MyEntity> MyEntities { get; set; }
}
```

Use the Repository in Your Service Layer
```
public class MyService
{
    private readonly IRepository<MyEntity> _repository;

    public MyService(IRepository<MyEntity> repository)
    {
        _repository = repository;
    }

    public async Task<MyEntity> AddEntityAsync(MyEntity entity)
    {
        return await _repository.AddAsync(entity);
    }
}
```

### 3. Register Dependencies

In Program.cs, configure Dependency Injection:
```
builder.Services.AddDbContext<AveryLamDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

## Methods Available

### Add
```
await repository.AddAsync(entity);
await repository.AddManyAsync(entities);
```

### Get
```
var item = await repository.FindOneAsync(e => e.Id == id);
var items = await repository.GetAllAsync();
```

### Update
```
await repository.UpdateAsync(entity);
```


### Delete
```
await repository.DeleteAsync(entity);
await repository.DeleteManyAsync(e => e.Status == "Inactive");
```

### Check Existence
```
bool exists = await repository.AnyAsync(e => e.Name == "Example");
int count = await repository.CountAsync(e => e.Active);
```

