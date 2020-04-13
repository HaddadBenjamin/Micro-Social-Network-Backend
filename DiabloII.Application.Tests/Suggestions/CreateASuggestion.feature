Feature: CreateASuggestion
	In order to create suggestion to drive the development of the application
	As a user of the application
	I want to call the suggestions API

@suggestion
Scenario: Create a suggestion happy path
	When I create a suggestion with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	Then the http status should be 201
	Then the suggestion created should be