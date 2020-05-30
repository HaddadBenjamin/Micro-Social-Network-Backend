Feature: IdentifyUser
	In order to use the application
	As a user
	I need to be identified

@users
Scenario: Identify me happy path
	Given I identify me
	Then the http status code should be 200
	And the identified user should exists