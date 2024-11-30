Collab Project: Hey Hi!
Overview
Collab Project is a web application designed to facilitate collaboration among users.
This platform enables users to create and manage events, groups, and educational content through YouTube links, 
creating a community-oriented environment focused on learning and interaction.

Core Concepts
Meet: Facilitate user interaction and engagement within the platform through community groups and events.
Connect: Enable users to discover and join groups based on personal interests, encouraging network building and collaboration.
Learn: Provide opportunities for knowledge sharing, skill development, and access to educational content through videos, tutorials, and organized events.

Table of Contents

API Endpoints
Contributors
Technologies Used
Getting Started
Usage
Testing
License


API Endpoints

Events API
List Events: GET /api/EventsAPI/List - Retrieves a list of all events.

Find Event by ID: GET /api/EventsAPI/Find/{id} - Retrieves a specific event by ID.

Add Event: POST /api/EventsAPI/Add - Creates a new event.

Update Event: PUT /api/EventsAPI/Update/{id} - Updates an existing event by ID.

Delete Event: DELETE /api/EventsAPI/Delete/{id} - Deletes a specific event by ID.



Groups API
List Groups: GET /api/GroupsAPI/List - Retrieves a list of all groups.

Find Group by ID: GET /api/GroupsAPI/Find/{id} - Retrieves a specific group by ID.

Add Group: POST /api/GroupsAPI/Add - Creates a new group.

Update Group: PUT /api/GroupsAPI/Update/{id} - Updates an existing group by ID.

Delete Group: DELETE /api/GroupsAPI/Delete/{id} - Deletes a specific group by ID/



Users API

List Users: GET /api/UsersAPI/List - Retrieves a list of all users.

Find User by ID: GET /api/UsersAPI/Find/{id} - Retrieves a specific user by ID.

Add User: POST /api/UsersAPI/Add - Creates a new user.

Update User: PUT /api/UsersAPI/Update/{id} - Updates an existing user by ID.

Delete User: DELETE /api/UsersAPI/Delete/{id} - Deletes a specific user by ID.



YouTube Links API
List YouTube Links: GET /api/YouTubeLinksAPI/List - Retrieves a list of all YouTube links.

Find YouTube Link by ID: GET /api/YouTubeLinksAPI/Find/{id} - Retrieves a specific YouTube link by ID.

Add YouTube Link: POST /api/YouTubeLinksAPI/Add - Creates a new YouTube link.

Update YouTube Link: PUT /api/YouTubeLinksAPI/Update/{id} - Updates an existing YouTube link by ID.

Delete YouTube Link: DELETE /api/YouTubeLinksAPI/Delete/{id} - Deletes a specific YouTube link by ID.


Contributors

Mohammed Alhamadani

Jupet Jagonos


Technologies Used


ASP.NET Core MVC

Entity Framework Core

Microsoft SQL Server
