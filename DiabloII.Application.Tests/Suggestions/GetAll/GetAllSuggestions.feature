Feature: GetAllSuggestions
	In order to see all the suggestions to drive the development of the application
	As a developper
	I want to call the suggestions API

@suggestion
Scenario: Get all the suggestions happy path
	Given I create the suggestions with the following informations
	| Content                   | UserId      |
	| You should add more items | any user id |
	| You should add more areas | any user id |
	When I get all the suggestions
	Then the http status code should be 200
	And all the suggestions should be
	| Content                   | CreatedBy   |
	| You should add more items | any user id |
	| You should add more areas | any user id |