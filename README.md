# Course Management System Web App

## App description 

Simple ASP.NET MVC5 web app for creating and managing online courses made up using Entity Framework 6 and Code First development approach.
This application allows to create online courses on various subjects and take courses created by others.  
  
  Every course consists of lessons which include:
- text materials
- questions 
- PDF presentations (optional)
- embedded YouTube videos (optional)

If a person that is taking a course has any questions, he can ask them in a course or lesson forum and receive feedback from other people that are taking this course and from a person that is managing the course.

## Generic functions of the app:

- creating and editing a course
- creating and editing a lesson for a course
- taking a course
- answering questions after each lesson to control mastering the material
- discussing course material with other people in forums

## Installation

To use the app go to [Course Management System](link).

To build and run the project you must have Visual Studio 2015 installed. 
Change the connection string in the Web.config file so that the SQL Server instance name is MSSQLLocalDB instead of v11.0.

In most cases you can run the application by following these steps:

1. Download and extract the .zip file.
2. Open the solution file in Visual Studio.
3. Build the solution, which automatically installs the missing NuGet packages.
4. Open the Package Manager Console, and run the update-database command to create the database.
5. Run the application. To run the solution, hit F5 or choose the Debug - Start Debugging menu command.

If you have any problems with those instructions, follow these longer instructions.

1. Download the .zip file.
2. In File Explorer, right-click the .zip file and click Properties, then in the Properties window click Unblock.
3. Unzip the file.
4. Double-click the .sln file to launch Visual Studio.
5. From the Tools menu, click Library Package Manager, then Package Manager Console.
6. In the Package Manager Console (PMC), click Restore.
7. Exit Visual Studio.
8. Restart Visual Studio, opening the solution file you closed in the previous step.
9. In the Package Manager Console (PMC), enter the Update-Database command. (If you get the following error:
"The term 'Update-Database' is not recognized as the name of a cmdlet, function, script file, or operable program", exit and restart Visual Studio.)
10. Run the application. To run the solution, hit F5 or choose the Debug - Start Debugging menu command.

## Application guide
### Creating an account
To be able to create a course or to take part in a existing course you must create an account by clicking **Sign Up** button on a navigation bar on top of the 'Home' page. After filling a form and submitting signing up your account will be created and you will automatically redirected to the 'Home' page. This account will allow you to manage your courses and subscriptions to the courses of others.
### Managing your account
Using the app next time click **Log In** button on the navigation bar to login into your account. To edit your account (username, email, password), delete it or logout click on a dropdown menu on the right side of the navigation bar and choose the appropriate menu item.
### Subscribing to a course
On the 'Home' page you can see all courses created by users. After clicking on 'More' button under any course you will be redirected to the course page that provides general information about the course and list of the lessons of the course.
To be able to see materials of the lessons, answer the course questions and discuss the course material in a forum you must subscribe to the course. You can do that by clicking **Subscribe** button either under the course on the 'Home' page or on the course page. 
### Managing subscriptions
All subscriptions are displayed on the 'My subscriptions' page. To go to that page click **My subscriptions** button on the navigation bar. 
### Course features
Each course assumes sequential going through the lessons which means that you must take one lesson and answer the questions to this lesson to be able to go to the next one.
### Creating a course
To create your own course go to the 'My courses' page by clicking **My courses** button on the navigation bar and click **Create** button. Then you will have to set a name and a description for your course. After doing that and clicking **Submit** button you will be redirected to the 'Edit course' page where you will be able to create lessons of your course and fill them with text materials, PDF presentations, YouTube videos and questions.
### Managing courses
All your courses will be displayed on the 'My courses' page. You will be able to edit your courses at any time by clicking **Edit** button under the course on the 'My courses' page or on the 'Home' page.

## Contact information
pavellev7@gmail.com
