Clean Architecture: (High level of decoupling among the layers)
- Focuses on decoupling so that anytime any layer can be migrated to any new technology
########################################################################################################################


------------------------------------------------------------------------------------------------------------------------
API : API Interfaces exposed to outer world
    This project consumes the Core Application project in its reference
    High leve of decoupling, e.g. If we want to expose diffrent type of interface say WebSockets or REST or gRPC,
     we can easily implement one

------------------------------------------------------------------------------------------------------------------------

CoreApplication : (Business logic in Application Layer  + Entities definition in Domain layer)
 [Exposed interface : Command and Query handlers]
    
    Application : 
        Contracts
            Persistence Layer
                - Interfaces of Repository
                
        Features : (Business logic layer + Exposed interfaces viz command and Query)
            Commands
                Directory for writing an entity1
                    - Command
                    - CommandHandler
                    - Validators
                    - DTOs (if needed)
                    - etc : whatever needed
                Directory for writing an entity2...
                Directory for writing an entity2...
                    
            Queries
                Directory for Reading an entity1
                    - Query
                    - QueryHandler
                    - DTO
                Directory for Reading an entity2...
                Directory for Reading an entity3....
                
                
    Domain : All Domain related Data (Entities) : This project has reference into Infrastructure as well
        Entities
            Definition of Entities and their relations
            

------------------------------------------------------------------------------------------------------------------------

Infrastructure : (DB and related operations)
    DB Operations
    Implementations of Repository interfaces defined in Application layer : (High level of decoupling e.g. DB migration)

------------------------------------------------------------------------------------------------------------------------

UI : 
    UI Layer of application
    Can consume both APIs and Application layer 
    - If want to deploy separately, it can be easily done
    - High level of decoupling

------------------------------------------------------------------------------------------------------------------------

Test : 
    - Unit Test for all modules
    - Integration tests

------------------------------------------------------------------------------------------------------------------------


Later analyse flow of both Command and Query in code.

------------------------------------------------------------------------------------------------------------------------





