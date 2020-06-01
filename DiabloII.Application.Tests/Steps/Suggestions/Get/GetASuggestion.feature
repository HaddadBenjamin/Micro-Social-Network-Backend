Feature: GetASuggestion
	In order to see my created susggestion
	As a developper
	I want to call the suggestions API

@suggestions
Scenario: Get a suggestion happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId        |
		| You should add more items | any user id   |
	When I get the last created suggestion
	Then the http status code should be 200
	And the suggestion should be
		| Content                   | CreatedBy     |
		| You should add more items | any user id   |
		| You should add more areas | other user id |