# Xamarin Architecture

This is a boilerplate solution using the onion architecture for Xamarin.iOS and Xamarin.Android mobile projects.

### Architecture Documentation
The solution is separated into 4 main categories:
1. **Business** - Includes core models and interfaces as well as business logic. Requires no 3rd party libraries.
2. **Infrastructure** - Includes database and web service projects to handle data transfer. References specific technologies, and are injected at the presentation layer.
3. **Presentation** - Includes iOS and Android project types.
4. **Testing** - Includes testing projects across all three layers. 

#### Business
##### Mobile.Core
* Includes:
  * Interfaces used throughout all projects
  * Concrete implementation of *most* entities (e.g. DTOs, view data models)
  * Shared enumerations

##### Mobile.Conductors
Contains conductor implementations which include business logic. Calls data access methods through dependency injection.

#### Infrastructure
##### Mobile.Services.Http
Contains web service implementations (e.g. REST APIs)
##### Mobile.Services.Realm
Implementation of local Realm database access.
* Includes:
  * Concrete implementations of Realm entities. Implementing entity interfaces from Mobile.Core.
  * Implementation of CRUD operations from service interfaces defined in Mobile.Core.
##### Mobile.DataAccess
Combines web and database services to simplify calls that require both data and web access. Classes in this project are dependency injected into Mobile.Conductors to serve *all* data access for the application. 

#### Presentation
##### Mobile.iOS
UI implementation of the app for iOS devices. Resolves conductor interfaces that were injected in Mobile.Presentation.Shared. 
* Includes:
  * **ViewController** - A view controller represents a "screen" in the app. Each screen is broken into 4 namespaced classes.
    * **Presenter Settings** - An object that contains information (as properties) passed from the view controller that the presenter requires. These properties include:
      * Main view
      * Model data for data binding
      * Event Handlers
    * **Presenter** - Responsible for creating and configuring all of the views (controls) that the screen requires. Wires up view events and binds data to UI elements.
    * **Layout** - Responsible for positioning UI elements on the screen (via auto layout constraints). 
    * **View Controller** - Instantiates the presenter (with settings) and layout classes. Handles controller events (e.g. view did load, view did appear, etc...). Includes concrete implementations of view event handling (using conductor methods for business logic).
  * **Views** - Any custom UI elements that the application requires are managed here (e.g. custom textbox input).
##### Mobile.Android
*Coming soon...*
##### Mobile.Presentation.Shared
Includes any presentation level configuration that is shared across all mobile platforms. Currently this includes:
  * Dependency injection
  * Object mappings
##### Mobile.Svg
Includes SVG resources that are used in both iOS and Android presentation projects.

#### Tests
##### Mobile.Conductor.Tests.iOS
A iOS test runner application that executes the tests created in Mobile.Conductors.Tests.Shared. 
##### Mobile.Conductor.Tests.Android
*Coming soon...*
##### Mobile.Conductors.Tests.Shared
Includes all conductor test implementations. It is referenced by the platform-specific test runner projects.

### Current Dependencies
* [Realm](https://realm.io/) - Mobile database
* [Autofac](https://autofac.org/) - Dependency Injection
* [Automapper](http://automapper.org/) - Object mapper
* [XamSvg](https://components.xamarin.com/view/xamsvg) - Vector image support in iOS and Android
* [NUnit](https://www.nunit.org/) - Unit testing framework
* [Shouldly](https://github.com/shouldly/shouldly) - Testing assertion framework

### Setup
##### Mac
*Coming soon...*
##### Windows
*Coming soon...*