# InsightHub
.NET Team Project

This document describes a team project assignment for the .NET cohort at Telerik Academy.

Project Description

Today, the pace of digital change is faster than ever. Looking ahead is critical to success. Businesses need instant access to reports that lists insights into major business and technology trends that will help them stay ahead and make smarter decisions for their organizations. Your company is about to build such portal - InsightHub. InsightHub is a Reports Portal web application. Some of the possible actions it enables its users to do are:

- Publish numerous reports 
- Browse reports based on an industry 
- Download reports
- Subscribe for email notifications when new reports are available 

You may look for inspiration at the Gartner portal, the Forrester portal or the IDC portal. The goal is to have a place that makes it easy for businesses to have instant access to insights and analysis. Build a clean, simple application that gets the job done!

Functional Requirements

Public Part (anonymous users)

The public part of your projects should be visible without authentication. This includes:

A landing page that has a list with three categories:
- Featured (reports selected by the admins)
- Popular (the reports with the most downloads)
- New (the newest reports)

Users that are not logged in must be able to still see details for each report:
- Name
- Summary
- Creator
- Tags
- Number of downloads
- Download button (that redirects to the sign-in page)

Users that are not logged in must be able to filter reports by name, industry, tags and sort by the following fields:
- Report Name
- Number of downloads
- Upload date
- Users that are not logged in must not be able to download a report.
- There must be capability for users to register/login

Registered users
Registered users should be in one of three roles – authors, customers or admins.

Authors

Authors must have private area in the web application accessible after successful login, where they could see all reports that are owned by the currently logged user. Additionally, the author user must be able to:
Delete/Update/Create their own report
Each report must have the following data:
- Name
- Description
- Author
- Industry (IT, Healthcare, Government, Finance, Defence, etc.)
- Tags (e.g. Audit & Risk, Customer Service & Support, Human Resources, Marketing, Sales,  etc.)
- Binary content (the report document itself)

Once report is created the it is “pending” state until the administrator approves it. The report is visible to customers only if it is approved.

Customers
Customers must be able to browse and download all reports. They should also be able to subscribe for email notification (per industry) in case a new report is released (I.e. approved by admins and visible to customers).
Additionally, customers must have private area in the web application accessible after successful login, where they could see all reports that they have downloaded.

Admins

System administrators can administer all major information objects in the system. The administrators have the following capabilities:
- Administrators must be able to approve new reports
- Administrators must be able to delete/edit all reports
- Administrators must be able to approve user accounts
- Administrators must be able to disable users accounts

Registration process
To register, anonymous users must provide their first name, last name, email, type of registration (author or customer), phone number and to specify their password. They should be able to login into the system only after admin approves their registration request.

REST API

In order to provide other organizations and/or developers with your service, you need to develop a REST API. The REST API should leverage HTTP as a transport protocol and clear text JSON for the request and response payloads.
API documentation is the information that is required to successfully consume and integrate with an API. Use Swagger to document yours.
Note: All privacy rules apply for REST API endpoints. For example, unauthenticated user cannot edit a beer or add it to his wish list.
Technical Requirements
General development guidelines
Use Visual Studio
Following OOP principles when coding
Following KISS, SOLID, DRY principles when coding
Following REST API design best practices when designing the REST API
Following BDD when writing tests
You should implement sensible Exception handling
Database
The data of the application must be stored in a relational database (Microsoft SQL Server is preferred). Identify the core domain objects and model their relationships accordingly. Follow the data modeling best practices. Normalize!

Backend

Use C# and .NET Core
Use tiered project structure (separate the application components in layers)
Use ASP.NET Core MVC
Use Entity Framework Core in the Persistence layer
Service layer (e.g. “business” functionality) should have at least 80%-unit test coverage

Frontend

Use AJAX for making asynchronous requests to the server where you find it appropriate
You may change the standard theme and modify it to apply own web design and visual styles. For example, you could search and use some free html & css template to make your web application look good. 
It is highly recommended to use Bootstrap

Optional Requirements

Integrate your app with a Continuous Integration server (e.g. GitLab’s own, Jenkins or other). Configure your unit tests to run on each commit to your master branch 
Host your application’s backend in a public hosting provider of your choice (e.g. AWS, Azure)
Research Gitflow Workflow and work on branches

Use Git issues to track bugs
Use JavaScript (AJAX) to fetch data from the API asynchronously 