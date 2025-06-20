BabenNew is a C# Windows Forms-based restaurant management system. This application is designed to manage restaurant operations such as table management, order taking, payment processing, personnel management, reservation handling, and product variety management. The system works with a database and is intended to simplify daily operations for restaurant businesses.

Features:
Table Management:

Manages the status of tables (empty, occupied, reserved).

Allows for switching between tables and creating bills for each table.

Provides a user-friendly interface to manage table statuses visually.

Table status is updated in the database.

Order Management:

Users can select products from the menu, specify quantities, and create orders.

Orders are stored in the database and prepared for payment.

Orders are displayed and managed via ListView.

Reservation Management:

Allows for table reservations for customers.

Reservations are searchable by table ID.

Reservation information is stored in the database.

Payment and Billing:

Bills can be created, and payment can be processed.

Payment types include cash, credit card, or tickets.

Payment details, including total amount and payment method, are saved to the database.

Payment and billing information can be reported.

Personnel Management:

Personnel login system to track their actions.

Personnel information is fetched from the database and shown in a ComboBox.

Personnel activity (login/logout) is recorded and tracked.

Product Management:

Products are organized under specific categories (e.g., main courses, desserts, beverages).

Users can search products and display them on the menu.

Product details such as prices, descriptions, and IDs are fetched from the database.

Additional Features:

Spotify Integration: Launch Spotify directly from the application.

Admin Access: Some buttons in the interface are hidden by default and can be revealed with admin access.

User Interface:
The user interface is designed using Windows Forms.

ListView controls are used for dynamically managing products, orders, and tables.

Button, ComboBox, TextBox controls allow for data entry and interaction.

Database:
SQL Server is used as the database.

Tables: Personnel, tables, reservations, orders, products, bills, payments, and other data sources are included.

SQL queries are used to fetch, insert, and update data in the database.

Technologies:
C# (Windows Forms)

SQL Server (Database management)

ADO.NET (Database connection and management)

Windows Forms (UI design)

Installation and Running:
Set up SQL Server and configure the database.

Open the project in a C# development environment like Visual Studio.

Configure the database connection string.

Launch the application and start using all the features.

Additional Features:
User Login: Secure login for personnel with username and password verification.

Dynamic Menu: Products are organized by categories and can be searched by users.

Reporting: Reports on payments, reservations, and orders can be generated.

Multi-table Management: Multiple tables and bills can be managed simultaneously.
Will be updated.
