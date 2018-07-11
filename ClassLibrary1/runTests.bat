@echo off
if exist TestResult.xml del TestResult.xml
if exist TestResult.txt del TestResult.txt
if exist TestReport.html del TestReport.html
 

echo executing tests
"%~dp0\Lib\NUnit.Console-3.6.0\nunit3-console.exe" --labels=All --out=TestResult.txt "--result=TestResult.xml;format=nunit2" "%~dp0\bin\Debug\ClassLibrary1.dll"

echo generating reports
"%~dp0\Lib\Specflow-1.9\specflow" nunitexecutionreport "%~dp0\CodifyTestFWork.csproj" /xmlTestResult:TestResult.xml /out:TestReport.html

pause