### How long did you spend on the coding test? 

It took me about four hours, spread over two days to do it. I spent the initial hour coming up with a rough plan of how I was going to approach this task, and what tools I wished to use:

I decided to use C# as it is the language I am most comfortable with, having used it as my main language for the past five years.

Having worked on similar projects with an API backend, I decided to structure the solution as follows:

##### Business Logic
I wanted to set up a service that could be called from an external client, whether that be a console app or a web app. I modelled the contents from Fiddler, and set up my models. From here, I created the necessary interface for the main service.

Rather than call `HttpClient` from within the service, I opted to use factory objects to handle the client and request and to inject them into the service. Doing this allows for the project to be  easier to extend, and by separating the client and request objects, mocking these factories would be much simpler. I have used RestSharp in the past, so I decided to use it within the factories, and to inject these into the service using Ninject. These were set up in a Business Logic module inheriting from `NinjectModule`. 

To complete the project, I added a constants file for use later where any strings may be reused.

##### Web Project

I created a basic controller to handle the search form and results. I used Ninject to inject the service into the controller, and set up a set of partial views to render the results and a message to indicate if no results were found for the provided outcode. Attributes were set up on the ViewModel to handle the outcode being required.

While rendering the restaurants, I noticed that the sheer number of results would be annoying to users. To combat this, I set up paging on the service, and used `PagingMvc` to add basic paging to the bottom of the page and a paged collection that could be utilised by the service. Using the package to handle this saved around an hour of time.

##### Console App

I created a basic console application to further test the service. Ninject was used to inject the service into the console app, with five results at a time being rendered until no more were found.

##### Tests

I set up a set of NUnit tests alongside Moq to test the service with a set of mocked factories and some mocked data from the API. Once the service had been tested, I set up some Selenium tests to test the frontend to ensure that results were being given and to ensure that a message as displayed if a dummy outcode was provided.

### What would you add to your solution if you had more time? If you didn't spend much time on the coding test then use this as an opportunity to explain what you would add.

With some more time, I would have set up some Web API calls to hook up to the service, and to render the web application on a static HTML page using AngularJS. With this, I would have also added filtering for these results, allowing users to order by rating or to set their own page limits.

Additionally, I would have spent more time making the console application more usable. The current console application is very basic, and only consists of the bare minimum code to display the restaurants.

### What was the most useful feature that was added to the latest version of your chosen language? Please include a snippet of code that shows how you've used it.

In C#, I am looking forward to the addition of the elvis (or null-propagating) operator `?.`. It adds some syntactic sugar to allow you to get the value of a member if the instance of the variable used is not null, allowing for null checks with previous code than before.

In the following example, this:

    string tmp = "foo";
    int? strLength = tmp?.Length;

Is translated to:

    int? strLength = (tmp == null) ? null : tmp.Length;

It pairs nicely with the null-coalescing operator `??`, allowing a developer to chain up a call to set a default value without having to run through multiple null checks, like so:

    int? strLength = tmp?.Length ?? 0;

### How would you track down a performance issue in production? Have you ever had to do this?

My initial thought would be to check how the server is functioning. If everything appears to be fine on the server, I would check for exceptions being logged. If they are not being logged by the application or from IIS I would check the event log to see if anything strange had occurred. As a last check, I would check the database or any external data stores that were being used to ensure that nothing strange was happening there.

Typically, when checking for performance issues, I use .NET Profiler by redgate to profile the application while running. I have also been known to use New Relic's tools as well (typically based on what tools are available to me on the machine I am working on). Using these suites of tools, I have monitored different parts of the application, and have checked to see if certain methods are taking a long time, or if a bottleneck exists with an external data store. I have used these tools numerous times when checking over CMS websites to ensure that the database or a Lucene index isn't causing an issue for the site.

On occasion, when trying to mimic a performance issue, I have recorded IIS logs of basic interactions on a staging server, and have used a JMeter script to monitor an application with real interactions.

More often than not, in my experience working with inherited code for CMS websites, performance problems have been due to slow code, such as a collection only intended for a dozen items during testing being loaded with thousands of items. Typically, setting up .NET Profiler will point to the offending method during a dev run, and spotting the issue is rather straightforward.

### How would you improve the JUST EAT APIs that you just used?

For me, one of the key ingredients of a good public API is good documentation. When calling the API in the browser I would load up some information on the API itself, all the possible calls that can be made, alongside some examples of the API calls in action. I would also list the definitions of any objects used as inout or returned, so that a developer can be more aware of what they may need to build upfront to model their application against the API.

I would also add some basic validation against the outcode to the API, and provide a RESTful response when invalid input or invalid credentials have been used, so that I can be aware that I haven't provided the correct authorisation token.

As a security measure, I would provide an initial API call and force users to authenticate with the API before using it, either using Basic Authentication or OAuth.

Finally, I would make the URL structure more fluid to allow for different filters to be used, so instead of `http://api-interview.just-eat.com/restaurants?q=se11` I would favour something like `http://api-interview.just-eat.com/restaurants/in/se11/open-now/halal/`

### Please describe yourself using JSON.

    {  
        "name":"Mike Bull",
        "gender":"Male",
        "dateOfBirth":"27/03/1987",
        "education":[  
            {  
                "school":"The University of the West of England, Bristol",
                "course":"BSc (Hons) Computer Science"
            }
        ],
        "interests":[  
            "Football",
            "Gaming",
            "Brazilian Jiu-Jitsu"
        ],
        "programmingLanguages":[  
            "C#",
            "Java",
            "JavaScript",
            "Python"
        ]
    }