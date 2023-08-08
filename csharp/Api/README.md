# API

- An Api project with multiple sub projects
- Test with api.rest as swagger has issues with post and update body
- The GroupDocs library throws error of invalid password
- To overcome this use ImageLibrary (contains pdf also) from git.softhem.net/csharp/PdfImageLibrary

## Packages

- Add nugget packages to BookLibrary
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Relational
  - Microsoft.EntityFrameworkCore.Sqlite
  - System.Configuration.ConfigurationManager
- Add the following to ImageLibrary
	- ceTe.DynamicPDF.Rasterizer.NET
	- 

## Publish

- BookStoreWinApp
	- Before publishing fix paths to database and icons etc in App.config
	- Right click the folder name > properties > build > Platform target > Any CPU
	- Right clikc the folder name > Publicsh: Release / Any CPU, .net 7, Self-contained, Winx64

## BookStore

- This is the data modeling project

## API

- This is the Web Api project which provides data in json

## Console App

- This is the console app to test other projects like BookStore

## BookStore
- The data modeling and database services

## BookStoreWinApp
- The Windows forms desktop application

## ImageLibrary
- Extract image from pdf

## Links

- Icons: https://icons-for-free.com/book+books+education+knowledge+learning+reading+icon-1320085877896372664/
- EF Batch insert: https://entityframework-extensions.net/bulk-insert
