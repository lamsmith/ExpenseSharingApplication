Expense Sharing Application - README Documentation
Overview
The Expense Sharing API is a RESTful API built using C# to manage and split expenses among groups of users. It allows users to manage their expenses, track balances, and settle debts. The application is designed with Clean Architecture, leveraging EF Core for data management and PostgreSQL as the database.
This project demonstrates your ability to design APIs, implement business logic, and handle data relationships in a clean and maintainable way.

Features
User Management:
User registration and authentication
Profile management
Group Management:
Group creation and deletion
Adding and removing group members
Expense Management:
Adding expenses with descriptions and amounts
Splitting expenses among group members
Tracking balances and settlements
Payment Management:
Recording payments between users
Updating balances after payments
API Endpoints

Authentication Endpoints
POST /api/authentication/login - Logs in a user and returns an access token.
POST /api/authentication/register - Registers a new user.
POST /api/authentication/logout - Logs out the authenticated user
User Endpoints
GET /api/users/{userId} - Retrieve details of a specific user by their ID.
GET /api/users/{userId}/groups - Retrieve all groups the specified user belongs to.
GET /api/users/{userId}/settlement-history - Retrieve the settlement history for a specific user.

Group Endpoints
POST /api/groups - Create a new group.
POST /api/groups/{groupId}/add-user - Add a user to a group.
POST /api/groups/{groupId}/remove-user - Remove a user from a group.
GET /api/groups - Retrieve all groups the authenticated user belongs to.
GET /api/groups/{groupId} - Retrieve details of a specific group by its ID.
GET /api/groups/{groupId}/balances - Retrieve the balance of each user within a group (amount owed/paid).
GET /api/groups/{groupId}/expenses - Retrieve the expenses within a specific group.

Expense Endpoints
POST /api/expenses - Create a new expense.
POST /api/expenses/{expenseId}/settlement-history - Retrieve the settlement history for a specific expense.
POST /api/expenses/{expenseId}/pay-expense-share - Pay a user's share of an expense.
GET /api/expenses/{groupId}/expenses - Retrieve all expenses within a specific group.
GET /api/expenses/{expenseId} - Retrieve details of a specific expense by its ID.

Error Handling The API returns appropriate HTTP status codes and error messages for different error scenarios.
Security The API implements security measures such as:
Authentication: JWT-based authentication to protect sensitive endpoints.
Authorization: Role-based access control to restrict access to specific functionalities.
Input Validation: Validates user input to prevent malicious attacks.
Data Encryption: Sensitive data (e.g., passwords) is encrypted using strong

Additional Considerations
Scalability: Consider using techniques like horizontal scaling and load balancing to handle increasing traffic.
Performance: Optimize database queries, use caching, and profile your application to identify performance bottlenecks.
Monitoring: Implement monitoring tools to track API performance and identify potential issues.
Security Updates: Stay up-to-date with security patches for dependencies and frameworks.
