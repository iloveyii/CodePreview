# Receipt

- A desktop application for counter and inventory management
- Sub Projects
	- ApiClient
		- Read Data from remote rest api
	- Console App
		- Console application to interact and test real objects
	- Data Access
		- Database access
	- Data Model
		- Data modelling
	- Receipt
		- The windows forms desktop
	
## Install
- Entity framework
	- From nuggets install Microsoft.EntityFrameworkCore,Microsoft.EntityFrameworkCore.Relational, Microsoft.EntityFrameworkCore.Sqlite
- For config in nuggets add : System.Configuration.ConfigurationManager 

## Tables
```
CREATE TABLE "Customers" (
	"Id"	INTEGER,
	"FirstName"	TEXT,
	"LastName"	TEXT,
	"Phone"	TEXT,
	"Email"	TEXT,
	"Address"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);

CREATE TABLE "Orders" (
	"Id"	INTEGER UNIQUE,
	"Date"	NUMERIC,
	"Total"	NUMERIC,
	"CustomerId"	INTEGER,
	CONSTRAINT "PK_Orders" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Orders_Customer_CustomerId" FOREIGN KEY("CustomerId") REFERENCES "Customers"("Id")
	ON DELETE CASCADE
);
CREATE TABLE "OrderServices" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Name"	TEXT,
	"Price"	REAL,
	"ServiceId"	INTEGER,
	"OrderId"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_OrderServices_Orders_OrdersId" FOREIGN KEY("OrderId") REFERENCES "Orders"("Id") ON DELETE CASCADE
);
```
- For vendors
```
CREATE TABLE "Vendors" (
	"id"	INTEGER,
	"name"	TEXT,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE "Products" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT,
	"VendorId"	INTEGER,
	CONSTRAINT "FK_Products_Vendors_VendorId" FOREIGN KEY("VendorId") REFERENCES "Vendors"("Id"),
	CONSTRAINT "PK_Products" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE "Services" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT,
	"ProductId"	INTEGER,
	"Price"	REAL,
	CONSTRAINT "PK_Services" PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_Services_Products_ProductId" FOREIGN KEY("ProductId") REFERENCES "Products"("Id")
);
```