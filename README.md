# VirginMediaApp Web Architecture
 
The solution contains ASP.NET Core Razor-based Web Application which is structured into the following components:
 
[Architecture]
![Architecture](/Diagrams/Architecture.png)
 
* **Scenarios Web Page** (\VirginMediaApp.Scenarios.Web\Pages)- Contains the web application for browsing Scenario data.
* **ASP.NET Core Razor Web Application** (VirginMediaApp.Scenarios.Web)- Contains the core of web application logic and wiring up all the components together.
* **Cache Mechanism** (VirginMediaApp.Scenarios.Core.Cache)- This is a bespoke component that holds the Scenarios' data response for a given number of minutes.
* **Xml DataLoader** (VirginMediaApp.Scenarios.Core\Services\IxmlDataLoader.cs) To parse and load the XML data files from the given location.
 
## General Flow
 
As per the diagram above, generally flow works as follows:
1. Browse Scenario data: The user browses the Scenario data web to view the Scenarios.
2. Request is received by the Web application and which checks for the cached results in the local cache.
3. If results for the given Scenarios and Scenarios are not found locally then it loads and refreshes the cache results.
4. Then latest results are cached for the configured amount of time in minutes.
 
 
#  Setting Up and Running the Application
 
1. **Prerequisite:** .Net 6.0
2. Application can be run via the given RunApp.ps1 PowerShell script.
   It will run all the tests, and the application will start running on a local Kestrel server.
    [RunApp.ps1]
    ![RunApp.ps1](/Diagrams/RunApp-ps1.png)
3. Launch the application by default running at [https://localhost:5001/](https://localhost:5001/)
   [App Front Page]
    ![App Front Page](/Diagrams/AppSPage.png)
4. Large data set will be paged via configurable page sized (currently set to 50 records).
    [Paging]
    ![Paging](/Diagrams/AppSPagePaging.png)

##  Running via Visual Studio (2022)
1. After checking out the code make sure VirginMediaApp.Scenarios.Web is set as your startup project.
    [Startup]
     ![Startup](/Diagrams/Startup.png)

## Features
1. Decorator pattern-based cache mechanism.
2. Clear, separated by concerns of modular design.
 
   [Modular Design]
   ![Modular Design](/Diagrams/ModDesign.png)
3. Separated set of Integrations and Unite Tests suites, which tests the whole application inside out.
    [Tests suites]![Tests suites](/Diagrams/Tests.png) 
4. Cache timing and data paging size could be configured via the given config file
    [configs]![configs](/Diagrams/Configs.png) 
## Areas for improvements
1. Test coverage is just a starting point, it's not complete and comprehensive.
2. UI is basic and could improve with proper UX input.
3. There are lots of simple assumptions that have been made about the size, volume, location and frequency etc of XML data files. 
4. Accuracy of XML data files and their error handling could be improved with business input.
5. In real-time implementation these assumptions will be clarified for a better and real-time robust design i.e processing the XML files in the background and dumping the results into a database store
5. Load, stress and performance testing in the production-like environment will help the application well in the production environment.
6. There could be improvement with more logging, error handling, alert and monitoring, parallelism and cache etc and so on
7. Peers code review and design discussions always yield better results.
