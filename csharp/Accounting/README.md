# Blazor App

- Keep track of earnings and expenses

![screenshot](blazorapp.png)

## Commands

- Add git ignore: dotnet new gitignore
- Install nuget : dotnet new install dotnet-new-project-helpter
- Add readme: dotnet new add-readme

## Links

- <https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0>
- <https://github.com/claudiobernasconi/FinanceMentor>
- https://github.com/radzenhq/radzen-examples/tree/master

## Testing / Specflow / E2E testing blazor apps

- Architecture
  - The ASP .net applications run in a browser
  - A Selenium tool that drives the browser
  - The testing code , test cases in C#
- Project
  - Add new project of type MSTest project in current solution
- Tools
  - Selenium
  - Specflow.MsTest
- Packages
  - Selenium.WebDriver
  - Selenium.WebDriver.ChromeDriver - check version for you chrome browser
  - Specflow.MsTest
- Tips
	- Press FN + F12 on a feature to generate step definition to clip board
	- For clicking elements remove the headless option
	- For RadzenUpload to work (show uploading file in controller) it must be enclosed inside RadzenContent

## Deployment / Dockerize

- The app has server dll files and should be run to serve the requests for the client. The server can be run from command line using the command below. It will show if it can be run successfully.
- The nginx cannot run the dll file (i think), therefore it should only act as a proxy for the requests comming from the client.
- We can run the server dll file from command line as mentioned about but a good option is to make service, which could restart in case of errors, and then the nginx proxy to it.
- Luckily once the server is compiled it can serve the client (static html) without any configuration.
- Commands :
  - Create Dockerfile and nginx.conf in terminal and not i VS - not using nginx anymore as it cannot serve dll, the image in use is donet and no more nginx
  - Build : docker build -t blazorapp .
  - Run : docker run --name blazorapp -d -p 5005:3000 --rm blazorapp
  - To debug run: docker run blazorapp
  - To run the server dll build it locally :
    - `dotnet publish "./Server/BlazorApp.Server.csproj" -c release -o app --no-restore``
    - Run server on a specific port as (cd app): `dotnet BlazorApp.Server.dll --urls http://0.0.0.0:3000`
    - The client can be served by nginx however the server is run using the above command
    - This command is easy to be used in Dockerfile as it enables to listen from outside and not only localhost

## Deploy / Script

- ./start.sh

## Distance links

- https://developers.google.com/maps/documentation/javascript/place-autocomplete
- https://developers.google.com/maps/documentation/javascript/examples/places-autocomplete-addressform
- https://stackoverflow.com/questions/1555253/how-to-make-an-autocomplete-address-field-with-google-maps-api
- Google maps : Sony xperia acc
    - API Key: <>
    - Project ID: <>
- Bing
    - Key : <>
    - 
- Draw polygon
    - Goto mymap.google.com & click the Draw a line from toolbar
    - From the left sidebar , infront of the map name , click the three elepsis (...) and click export