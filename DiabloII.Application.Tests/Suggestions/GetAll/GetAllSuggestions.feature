Feature: GetAllSuggestions
	In order to see all the suggestions to drive the development of the application
	As a developper
	I want to call the suggestions API

@suggestion
Scenario: Get all suggestions happy path
	Given I create a suggestion with the following informations
	| Content                   | UserId      |
	| You should add more items | any user id |
	| You should add more areas | any user id |
	When I get all the suggestions
	And all the suggestions should be
	| Content                   | CreatedBy   |
	| You should add more items | any user id |
	| You should add more areas | any user 2d |