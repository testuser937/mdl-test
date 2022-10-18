# Test task for Monq Digital Lab 

A simple web service that can generate and send emails to recipients and log the result in the database.

## API description

- POST /api/mails 

    request body:
    ```
    {
        "subject": "string",
        "body": "string",
        "recipients": [string]
    }
    ```
    The method generate email and send it to the recipients, then add row to database including the request body, current date and time, send result (OK/Failed), failed message (if sending failed)
- GET /api/mails

    The method returns all sent messages from database

## Stack
- ASP.NET Core
- Dapper
- PostgreSQL
- Swagger
