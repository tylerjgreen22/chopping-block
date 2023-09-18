#Chopping block
This full-stack web application allows users to discover and share their favorite recipes. Built with Angular for the front end and ASP.NET Core for the backend, this application is designed with maintainability, scalability, and performance in mind.

##Features
User Authentication: Users can create accounts, and manage their posts and interact with others posts.

Recipe Management: Create, edit, and delete your own recipes. Share your culinary creations with the community.

Recipe Discovery: Browse a wide range of recipes from other users. Filter and search for recipes based on ingredients, categories or most liked/recent recipes.

Like Posts: Like or unlike posts from other users, filter by most liked recipes to see whats most popular.

Cloudinary Image hosting: Recipe images hosted via Cloudinary to allow fast access to images.

##Architecture

Generic Repository Pattern: Implemented the generic repository pattern to promote code maintainability and reduce duplication.

Specification Pattern: Utilizing the specification pattern for flexible and efficient querying of the database.

Unit of Work Pattern: Manage database transactions and operations using the unit of work pattern.

##Backend Features

Caching with Redis: Improve application performance by caching frequently accessed data with Redis.

Database: PostgreSQL is used as the database to store recipe information and user data.

API: Full sorting, filtering, searching and pagination functionality for API results

##Technologies Used
Front End: Angular
Back End: ASP.NET Core
Database: PostgreSQL
Caching: Redis
Authentication: ASP.net Identity w/ JWT (JSON Web Tokens)

View the application yourself at: https://choppingblock.online/

##Contact
For any questions or inquiries, please contact me at tylerjgreen22@gmail.com.

Enjoy exploring and sharing your culinary adventures with the Chopping Block Application! 🍽️
