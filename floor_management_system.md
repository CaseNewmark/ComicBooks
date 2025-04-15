# Floor Management System Diagram

```mermaid
classDiagram
    class FloorPlan {
        +Guid Id
        +string Name
        +List~Section~ Sections
    }

    class Section {
        +Guid Id
        +string Name
        +List~string~ Genres
        +int Capacity
    }

    class IFloorPlanService {
        +Task~FloorPlan~ CreateFloorPlanAsync(string name)
        +Task AddSectionAsync(Guid floorPlanId, Section section)
        +Task~FloorPlan~ GetFloorPlanAsync(Guid floorPlanId)
    }

    class ISectionService {
        +Task~Section~ CreateSectionAsync(string name, List~string~ genres, int capacity)
    }

    FloorPlan "1" --> "*" Section
    IFloorPlanService ..> FloorPlan
    ISectionService ..> Section