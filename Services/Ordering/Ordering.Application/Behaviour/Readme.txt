🧩 1. The Big Picture — Request Flow in a .NET API
When an HTTP request hits your API endpoint:
HTTP Request → ASP.NET Middleware Pipeline → Controller → MediatR → Behaviours → Handler → Response

Behaviours (ValidationBehaviour, UnhandledExceptionBehaviour) : 
These only run inside MediatR — they don’t directly handle HTTP responses. 
So when a ValidationException or any other exception is thrown, it bubbles up out of the MediatR pipeline, back to the ASP.NET Core middleware pipeline.
That’s where your app can catch it globally


🧱 2. What Happens by Default (Without Any Middleware)
If you don’t have any global exception middleware: The exception (like ValidationException) bubbles all the way up.
ASP.NET catches it. The application does not crash, but it returns a generic 500 Internal Server Error response like:.


🧠 3. The Right Way — Add a Global Exception Middleware
A global exception middleware sits in the ASP.NET Core request pipeline and handles all uncaught exceptions — including ones thrown from MediatR behaviors.
This is where your app decides: What HTTP status code to return. What error message or structure to show.