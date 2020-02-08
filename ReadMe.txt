WDT -Assignment 2
-----------------

Author: Rajarajeswari Rudhrakumar
StudentID: S3725902

Author: Julian Dac Trung
StudentID: S3748402

---------------------  ***  ------------------------

Class Library:
Class Library is used mainly as a way to not repeat codes (DRY). In our project we also found it handy to separate transaction logic (deposit/withdraw, etc) from the Controllers via use of Class Library. Of course the given advantage is that our transaction logic is DRY so if in the future another type of banking operation that also needs deposit/withdraw, etc., like bank loan, it may also invoke from this Class Library. Secondly, if there is any changes to business rule, e.g. only can withdraw max 5 transactions per day, the rule can then be added to the Class Library without touching the underlying MVC. Thirdly is also that the Controllers now purely focus on connection/persistence and validation purposes and are unhindered by business logic.

Other features:
More than Class Library, we also implemented ScopedProcessingService which takes care of concurrent transactions into its own folder: HostedService. Other folders are Data which contains _context and seeddata, or migration, which contains initial migration to our database, are just as useful in separating responsibilities.

We also implemented Developer Exception to handle 4xx HTTP status code. It is put inside Startup.Configure as custom error handling.

Improvement:
Improvement in future iteration could also be that we move the js file, which currently resides within the respective cshtml file, into its own dedicated folder under "wwwroot". 

We are also aware that enterprise-level Class Library often come in three forms: CL Persistence, CL View and CL Business Logic. For this assignment 2, we could have moved our persistence into its own Class Library. However the implementation of such is too troublesome and while scalable, it will require a lot more code. Another creative use of Class Library would also be to contain all validations with a Business Object. Controllers can go through Business Objects (which are put inside Class Library) to change the database. If future business requires a new Controller but accidentally created with bad validations, the Business Objects would act as a buffer and our database would remain a valid state.

Lastly, our effort to separate concerns is self-evident in the design. Hope those who read our sourcecode will find as much joy as we have done writing it.

---------------------  ***  ------------------------

References
--------------

Assignment-3
------------
https://requestlab.fr/
API Model State validation:
https://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/




-------------------------------------------------------------------------------
Assignment-2
------------
https://www.freephototool.com/
https://www.freelogodesign.org/
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-3.1&tabs=visual-studio
https://www.c-sharpcorner.com/UploadFile/deveshomar/ways-to-bind-dropdown-list-in-Asp-Net-mvc/
https://www.pluralsight.com/guides/asp-net-mvc-populating-dropdown-lists-in-razor-views-using-the-mvvm-design-pattern-entity-framework-and-ajax
SchedulePayment:
https://docs.microsoft.com/en-us/aspnet/core/performance/performance-best-practices?view=aspnetcore-3.1
Phone Number Regex:
https://manual.limesurvey.org/Using_regular_expressions#Australian_phone_numbers
https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=netframework-4.8#System_Text_RegularExpressions_Regex_IsMatch_System_String_
http://regexstorm.net/tester
Page Redirect with parameter:
https://stackoverflow.com/questions/1257482/redirecttoaction-with-parameter
Pagination:
https://github.com/dncuug/X.PagedList
Privacy:
https://www.termsfeed.com/blog/sample-privacy-policy-template/
Background process:
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio



