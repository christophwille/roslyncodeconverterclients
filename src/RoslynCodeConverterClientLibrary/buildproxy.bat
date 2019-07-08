REM https://github.com/Azure/AutoRest - it is a npm package, no longer a tool inside a NuGet package
REM https://github.com/Azure/autorest/blob/master/docs/examples/generating-a-client.md

REM npm install -g autorest

REM *** OLD CODE ***
REM ..\packages\autorest.0.14.0\tools\AutoRest.exe -Input swagger.json -Namespace RoslynCodeConverterClientLibrary.Proxies -OutputDirectory Proxies -CodeGenerator CSharp

REM Current build with autorest@2.0.4283

autorest --input-file=swagger.json --csharp --output-folder=Proxies --namespace=RoslynCodeConverter.Client.Proxies