@set BERP_VERSION=1.1.1
@set BERP_INSTALL=%USERPROFILE%\.nuget\packages\berp\%BERP_VERSION%\tools\net471

%BERP_INSTALL%\berp.exe -t %BERP_INSTALL%\GeneratorTemplates\CSharp.razor -g CucumberExpressionsGrammar.berp -o CucumberExpressionParserDemo\Parser\Parser.generated.cs
