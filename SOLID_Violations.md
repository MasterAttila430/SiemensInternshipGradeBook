# SOLID Principles Violations & Code Review

# 1. Single Responsibility Principle (SRP)
- Where: `Controllers/ItemController.cs` (Methods: `GetAll` and `GetById`)
- Why it is a violation: The controller is responsible for handling HTTP requests. 
  However, it is performing business logic (calculating TotalCount and AverageValue) and directly logging to the console using `Console.WriteLine`.
- The fix applied: Removed business logic calculations from the controller and replaced `Console.WriteLine` with the standard dependency-injected `ILogger`.

# 2. Single Responsibility Principle (SRP)
- Where: Repositories/ItemRepository.cs (Methods: GetAllAsync and GetByIdAsync)
- Why it is a violation: The repository should only be responsible for data access. Currently, it contains business logic by filtering out inactive items (checking IsActive).
- The fix applied: Removed the filtering logic. The repository now returns all data, leaving business logic to the service layer.

# 3. Code Review: Naming Convention
- Where: Interfaces/IItemReader.cs
- Why it is a violation: Inconsistent naming. The interface was named IItemReader while the implementation was ItemRepository.
- The fix applied: Renamed IItemReader to IItemRepository to follow standard patterns.

# 4. Code Review: Runtime Error (Missing DI)
- Where: Program.cs
- Why it is a violation: The application failed to start because IItemRepository was not registered in the Dependency Injection container.
- The fix applied: Added builder.Services.AddScoped<IItemRepository, ItemRepository>() to Program.cs.