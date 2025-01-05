# Application Patterns

## Layering and Clean Architecture

Layering is a design approach that organizes an application into separate tiers or layers, each with a specific responsibility. This improves maintainability, scalability, and separation of concerns.

### Classic layering model

- Presentation - Allows user to interact with our application.
- Domain (business logic layer) - Business reasons that make clear why this software needs to exists. Heart of our application.
- Data - Abstracts communication between our business and data persistence.

In classic layering, each layer communicates only with the adjacent layer and remains independent. Each layer owns its models—view models shouldn't be passed to the domain, domain models should be transformed to data models before sending to data layer and vice versa - domain objects should be transformed into DTOs before they are returned to the client.

This way of doing layering is old and avoiding doing layering like this is highly recommended.

Some of drawbacks:
1. Adding some field to existing entity leads to propagating change through all layers.
2. Implementing new feature is challenging for newcomers even if it is small as all layers need to be touched.
3. Abstraction and dependency flow sometimes becomes really hard to maintain.
4. There are a lot of copy paste classes. DTO -> Domain model -> Data model.

### Rich and anemic domain models
- Rich domain models - A rich domain model is more object-oriented and encapsulates the domain logic as part of the model inside methods. The main advantage of this approach is that most of the logic resides within the model, making it highly domain-centric, with operations implemented as methods on model entities.
Downsides:
	1. Accumulation of responsibilities by a single class.
	2. Injecting dependencies into the domain model is harder than other objects, such as services.
- Anemic domain models - An anemic domain model typically consists of only getters and setters without any methods. These models lack business logic, as such rules are delegated to other objects, such as domain service classes.
**Note: We are talking above about domain services which are doing some business logic against domain models. Services can also be application services. (Like Email service)**
