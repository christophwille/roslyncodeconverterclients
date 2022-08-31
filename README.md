# RoslynCodeConverter Clients
Some demo clients for https://icsharpcode.github.io/CodeConverter/

 * DesktopConverterClient is a GUI app (WPF) that allows to submit code to the converter service. Usage is 
much like the Web GUI (see screenshot in repo root). Uses AvalonEdit for highlighting.
 * RoslynCodeConverterClientLibrary is built from the Swagger definition using AutoRest
 * MiniConsole is only intended for API verification testing
 * FailCrawler is a simple console app that recursively runs all .cs files through the converter to find
exceptions in larger code bases easily. Use case: download a Github repo zip and point the app to the 
extracted directory. Usage: failcrawler --path=d:\blah > result.txt
