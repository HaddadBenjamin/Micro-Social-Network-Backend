Feature: GetAllErrorLogs
	In order to see all the error logs to be able to reproduce the errors of my users
	As a developper
	I want to call the error logs API

@errorlogs
Scenario: Get all the error logs happy path
	When I get all the error logs
	Then the http status code should be 200