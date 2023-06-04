
# Schedule-Generator

Schedule-Generator is a project developed by Accenture Training Academy in collaboration with STI Global City. It allows you to create schedules for professors, rooms, and other entities.

## Usage Instructions

To use Schedule-Generator, please follow these steps:

1. Create a database with a name of your choice, or you can use the default name "Sched".
2. Execute the provided SQL query to add the necessary data. You can find the query at the following link: [SQL Query](https://pastebin.pl/view/e7022307).
3. Update the connection string to match your database configuration. Locate the connection string in the project's configuration files and modify the `server` and `database` values accordingly. For example:
   ```
   <connectionStrings>
     <add name="DefaultConnection" connectionString="Data Source=your_server_name;Initial Catalog=your_database_name;Integrated Security=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
4. Build and run the Schedule-Generator project.
5. Start creating schedules for professors, rooms, and other entities as needed.

Feel free to explore the various features and functionalities of Schedule-Generator to effectively manage and generate schedules.

## License

This project is licensed under the [MIT License](LICENSE).
