# SOLID Principles Violations & Code Review

# 1. Single Responsibility Principle (SRP)
- Where: `Controllers/ItemController.cs` (Methods: `GetAll` and `GetById`)
- Why it is a violation: The controller should only be reponsible for handling HTTP requests. Instead, it was doing business logic (calculating TotalCount and AverageValue) and using `Console.WriteLine` directly for logging.
- The fix applied: Moved the business logic calculations out of the controller and replaced `Console.WriteLine` with a proper dependency-injected `ILogger`.

# 2. Single Responsibility Principle (SRP)
- Where: `Repositories/ItemRepository.cs` (Methods: `GetAllAsync` and `GetByIdAsync`)
- Why it is a violation: The repository's only job should be data access. Right now it contains business logic because it filters out inactive items (checking IsActive). 
- The fix applied: Removed the filtering part. Now the repo just returns all the data, and the business logic is handled by the service layer. Also, I replaced the `Console.WriteLine` in the catch block with `ILogger` to follow standard logging practices.

# 3. Code Review: Naming Convention
- Where: `Interfaces/IItemReader.cs`
- Why it is a violation: Inconsitent naming. The interface was called IItemReader but the implementation was ItemRepository.
- The fix applied: Renamed the interface to IItemRepository so it matches the standard repository pattern.

# 4. Code Review: Runtime Error (Missing DI)
- Where: `Program.cs`
- Why it is a violation: The application failed to statr because `IItemRepository` wasn't registered in the Dependency Injection container.
- The fix applied: Added `builder.Services.AddHttpClient<IItemRepository, ItemRepository>()` to Program.cs. This fixes the DI issue and sets up the HttpClient at the same time.