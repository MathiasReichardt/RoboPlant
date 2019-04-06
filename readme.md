# RoboPlant

RoboPlant is a server application which is used to demonstrate and explore how to build a (modern) backend.
This is a best effort approach. It will change to incorporate new ideas, fix issues and concepts.

### Technics used

- REST with hypermedia and HATEOAS
- Clean Architecture design (by Robert C. Martin)
- DDD Domain Driven Design
  - Identity types so surrogate Id's are typed.
- Pattern matching for operation results to avoid control flow by exception.

### Related Tutorial

There is a [tutorial](https://mathiasreichardt.github.io/HowToBuildARobot/) explaining REST aspects of the server.

## The project structure

- *Domain*: holds the core logic of the business. It is agnostic of all other layers
- *Application*: the Application/Use-Case layer: this layer provides interfaces to data sources and sinks. It uses them to trigger logic from the Domain Layer.
- *InMemoryPersistence*: In memory implementation of the data sink interfaces.
- *Server*: The host of the application. DI is configured here. Also the REST API is located here and accesses inner layers.
- *Util*: Technical extensions not belonging to any specific project.