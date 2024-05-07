# cocktailsDb
Backend Dev exam project 2024

In this readme guide I will write what I did in every guthub push so it is easily trackable after.


## Project structure 

I'll be using the free accessible cocktaildb that stores different type of drinks in their database. It's flexible, offers enough data and it is very easy applyable in data structures. (link:https://www.thecocktaildb.com/)![homepage](image.png)

This project will include :
    - Dotnet web application ✔
    - EF Database using MySql (with data seeding) ✔
    - Different kind of HTTP endpoints ✔
    - REST Client files (http.rest) ✔
    - Swagger Documentation
    - FluentValidation ✔
    - Automapper ✔
    - Route Groups ✔
    - API Keys for security
    - Versioning ✔
    - Caching ✔
    - Background Services ✔
    - Logging ✔
    - Testing
        + Unit 
        + Integration 
        + K6
    - Blazor UI
    - Extensions
        + GraphQl
        + Extra fancy stuff
    


## Initial push
    - Created a new dotnet web application + EF prep 

## 1 -  EF preperation
    - Created project structure (context)
    - added newtonsoft for correct data binding

## 2 - Route Creation
    - Created Repository & Services
    - Created HTTP Endpoints plus testing
    - added fluent validation
    - upload and download endpoint
    - automapper
    - versioning

## 3 - Efficiency & Documentation
    -  Swagger Doc x
    - Route groups
    - Caching
    - Background Services
    - Logging
## 4 - Security & Testing
    - API Keys
    - Unit Tests
    - Integration Tests

## 5 Blazor UI & Extensions
    - Blazor


     {
    DbDrinkId: "TestId",
    Name: "IntegrationTestDrink",
    AlternateName: "Tommy's Favorite",
    Category: "Apple",
    Iba: "Contemporary Classics",
    Alcoholic: true,
    ImageUrl: "https://www.thecocktaildb.com/images/media/drink/wpxpvu1439905379.jpg",
    IsEdited: false,
    IsDeleted: false,
    GlassType:
        {
        Name: "VeryCoolGlass"
    },
    Ingredient:
        {
        Ingredient1: "Yum",   
      Ingredient3: "Yum2",   
      Ingredient2: "Yum3",
      IsDeleted: false
    },
    Measurement:
        {
        Measure1: "1 cup",
        Measure2: "2 cups",
        Measure3: "4 cups",
      IsDeleted: false
    }
    }