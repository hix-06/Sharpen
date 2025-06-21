# .NET Core Study Notes

## Table of Contents
- [API Controllers](#api-controllers)
- [Constructor Injection](#constructor-injection)
- [Dependency Resolution](#dependency-resolution)
- [API Endpoints](#api-endpoints)
  - [GET Operations](#get-operations)
  - [POST Operations](#post-operations)
  - [PUT Operations](#put-operations)
  - [DELETE Operations](#delete-operations)
- [Parameter Binding Sources](#parameter-binding-sources)
- [Object Mapping](#object-mapping)
  - [Using Mapster](#using-mapster)
  - [Mapster Global Configurations](#mapster-global-configurations)
- [Data Validation](#data-validation)
  - [Using FluentValidation](#using-fluentvalidation)
  - [Validation Examples](#validation-examples)
- [Database Integration](#database-integration)
  - [Setting Up DbContext](#setting-up-dbcontext)
  - [Entity Configurations](#entity-configurations)
  - [Connection Strings](#connection-strings)
  - [Migrations](#migrations)
- [Asynchronous Operations](#asynchronous-operations)
  - [Service Implementation](#service-implementation)
  - [Controller Implementation](#controller-implementation)

## API Controllers

Controllers are the entry point for HTTP requests in your API. They handle incoming requests and return appropriate responses.

```csharp
[Route("api/[controller]")] // Default route pattern
[ApiController] // Enables automatic model validation, binding, etc.
public class SomeController() : ControllerBase 
{
    // Controller methods go here
}
```

The `[ApiController]` attribute provides several automatic behaviors:
- Automatic model state validation
- Binding source parameter inference
- Attribute routing requirement
- Problem details for error status codes

## Constructor Injection

ASP.NET Core uses dependency injection to provide services to your controllers.

```csharp
// Primary constructor syntax (modern, cleaner approach)
public class SomeController(ISomeService someService) : ControllerBase 
{
    private readonly ISomeService _someService = someService;
    
    // Now you can use _someService in your controller methods
}
```

## Dependency Resolution

For the application to identify and resolve your services, you need to register them in the service container:

```csharp
// In Program.cs or a DependencyInjection class
builder.Services.AddScoped<ISomeService, SomeService>();
```

Service lifetimes:
- `AddSingleton`: One instance for the entire application lifetime
- `AddScoped`: One instance per request/connection
- `AddTransient`: New instance each time requested

## API Endpoints

Each endpoint has a unique signature defined by HTTP method + route template.

### GET Operations

```csharp
// Get all resources
[HttpGet("")]
public IActionResult Get()
{
    var data = _someService.GetAll();
    return Ok(data);
}

// Get resource by ID
[HttpGet("{id}")]
public IActionResult Get([FromRoute] int id)
{
    var data = _someService.Get(id);
    return data is null ? NotFound() : Ok(data);
}
```

### POST Operations

```csharp
[HttpPost("")]
public IActionResult Add([FromBody] T someDataObject)
{
    var newDataObject = _someService.Add(someDataObject);
    return CreatedAtAction(nameof(Get), new { id = newDataObject.Id }, newDataObject);
}
```

The `CreatedAtAction` returns:
- HTTP 201 status code
- Location header with URL to retrieve the created resource
- The created resource in the response body

### PUT Operations

```csharp
[HttpPut("{id}")]
public IActionResult Update([FromRoute] int id, [FromBody] T someDataObject)
{
    var isUpdated = _someService.Update(id, someDataObject);
    
    if (!isUpdated)
        return NotFound();
        
    return NoContent();
}
```

### DELETE Operations

```csharp
[HttpDelete("{id}")]
public IActionResult Delete([FromRoute] int id)
{
    var isDeleted = _someService.Delete(id);
    
    if (!isDeleted)
        return NotFound();
        
    return NoContent();
}
```

## Parameter Binding Sources

ASP.NET Core provides several binding sources for controller parameters:

- `[FromRoute]`: Binds data from route values
- `[FromQuery]`: Binds data from the query string
- `[FromHeader]`: Binds data from HTTP headers
- `[FromBody]`: Binds data from the request body (typically JSON)
- `[FromForm]`: Binds data from form values (multipart/form-data)

## Object Mapping

Mapping prevents controllers from directly exposing business models, using DTOs instead:

- **Request DTOs**: Incoming data that gets converted to business models
- **Response DTOs**: Outgoing data converted from business models

### Using Mapster

Mapster is a fast, simple object mapper:

```csharp
// Install package: Mapster

public IActionResult SomeControllerMethod(T someDataObjectRequest)
{
    // Map from DTO to business model
    var businessModel = someDataObjectRequest.Adapt<BusinessModel>();
    
    // Perform business logic
    var response = _someService.DoSomething(businessModel);
    
    // Map from business model to response DTO
    return Ok(response.Adapt<SomeDataObjectResponse>());
}

// In Program.cs
builder.Services.AddMapster();
```

### Mapster Global Configurations

For more complex mapping scenarios, you can set up global mapping configurations:

```csharp
// In Mapping/MappingConfigurations.cs
public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SourceType, DestinationType>()
            .Map(dest => dest.SomeField, src => src.SomeField);
    }
}

// In Program.cs
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));
```

## Data Validation

Data validation ensures that incoming data meets your requirements before it reaches your business logic.

### Using FluentValidation

FluentValidation provides a fluent interface for creating validation rules:

```csharp
// Install packages: 
// - FluentValidation.DependencyInjectionExtensions
// - SharpGrip.FluentValidation.AutoValidation.Mvc

public class SomeDataObjectRequestValidator : AbstractValidator<SomeDataObjectRequest>
{
    public SomeDataObjectRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Please add a {PropertyName}")
            .Length(3, 100)
            .WithMessage("Title should be {MinLength} - {MaxLength} length, you entered {PropertyValue} which is [{TotalLength}]");
    }
}

// In Program.cs
builder.Services
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
    .AddFluentValidationAutoValidation();
```

### Validation Examples

```csharp
// Date validation
RuleFor(x => x.DateOfBirth)
    .Must(BeMoreThan18Years)
    .When(x => x.DateOfBirth.HasValue)
    .WithMessage("{PropertyName} is invalid, age must be 18 years at least");

private bool BeMoreThan18Years(DateTime? dateOfBirth)
{
    return DateTime.Today > dateOfBirth!.Value.AddYears(18);
}

// Date range validation
RuleFor(x => x.StartsAt)
    .NotEmpty()
    .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

RuleFor(x => x.EndsAt)
    .NotEmpty();

// Validate across multiple properties
RuleFor(x => x)
    .Must(HasValidDates)
    .WithName(nameof(PollRequest.EndsAt))
    .WithMessage("{PropertyName} must be greater than or equals start date");

private bool HasValidDates(PollRequest pollRequest)
{
    return pollRequest.EndsAt >= pollRequest.StartsAt;
}
```

## Database Integration

.NET Core uses Entity Framework Core for database access.

### Setting Up DbContext

```csharp
// Install packages:
// - Microsoft.EntityFrameworkCore
// - Microsoft.EntityFrameworkCore.Tools
// - Npgsql.EntityFrameworkCore.PostgreSQL (or other provider)

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<SomeDataObject> SomeDataObjects { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
```

### Entity Configurations

Fluent API allows for explicit configuration of entity properties:

```csharp
public class SomeDataObjectConfiguration : IEntityTypeConfiguration<SomeDataObject>
{
    public void Configure(EntityTypeBuilder<SomeDataObject> builder)
    {
        builder.HasIndex(x => x.Title).IsUnique();
        
        builder.Property(x => x.Title).HasMaxLength(100);
        builder.Property(x => x.Summary).HasMaxLength(1500);
    }
}
```

Common Fluent API Methods:
| Method | Description |
|--------|-------------|
| HasKey() | Defines the primary key |
| IsRequired() | Makes a column NOT NULL |
| ToTable("tableName") | Maps entity to specific table |
| Ignore() | Excludes property from database |
| HasColumnName("colName") | Renames column in database |
| HasColumnType("datatype") | Sets column data type |
| HasMaxLength(n) | Limits string length |
| HasDefaultValue(value) | Sets default value |
| HasDefaultValueSql("SQL") | SQL expression for default |
| HasComputedColumnSql("SQL") | Defines computed column |
| ValueGeneratedOnAdd() | Auto-generates on insert |

### Connection Strings

```json
// In appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your connection string here"
  }
}
```

```csharp
// In Program.cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
```

### Migrations

Managing database schema through code:

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply latest migration to database
dotnet ef database update

# Apply specific migration
dotnet ef database update MigrationName

# Remove last migration
dotnet ef migrations remove
```

**Tips:**
- Always run migrations before updating the database
- Use `AsNoTracking()` for read-only queries to improve performance
- When removing a migration, first roll back the database to a specific migration, then remove

## Asynchronous Operations

Modern .NET applications should use async/await for I/O-bound operations.

### Service Implementation

```csharp
public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default) => 
        await _context.Polls.FindAsync(id, cancellationToken);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
    {
        var currentPoll = await GetAsync(id, cancellationToken);

        if (currentPoll is null)
            return false;

        currentPoll.Title = poll.Title;
        currentPoll.Summary = poll.Summary;
        currentPoll.StartsAt = poll.StartsAt;
        currentPoll.EndsAt = poll.EndsAt;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }  

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);

        if (poll is null)
            return false;
        
        _context.Remove(poll);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);

        if (poll is null)
            return false;

        poll.IsPublished = !poll.IsPublished;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
```

### Controller Implementation

```csharp
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls = await _pollService.GetAllAsync(cancellationToken);
        var response = polls.Adapt<IEnumerable<PollResponse>>();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var poll = await _pollService.GetAsync(id, cancellationToken);

        if (poll is null)
            return NotFound();

        var response = poll.Adapt<PollResponse>();
        return Ok(response);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var newPoll = await _pollService.AddAsync(request.Adapt<Poll>(), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);

        if (!isUpdated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isDeleted = await _pollService.DeleteAsync(id, cancellationToken);

        if (!isDeleted)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

        if (!isUpdated)
            return NotFound();

        return NoContent();
    }
}
```