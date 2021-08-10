In this chapter, you will find a CQS implementation example, using .NET's MediatR package. To test the app, simply download the code, open the solution with Visual Studio 2019 and execute it.

It should launch the default read view. To try the command path, just make an HTTP request to:

`curl -d '{"property1":"new value","property2":"another value"}' -H 'Content-Type: application/json' http://localhost:2080/demo`

In case port 2080 is already busy on your machine, just change it in VS 2019 and adapt the request accordingly.
