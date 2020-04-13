Feature: DeleteASuggestion
	In order to delete a suggestion that I don't like
	As a user
	I want to call the suggestions API

@suggestion
Scenario: Delete a suggestion happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	When I delete the suggestion "You should add more items"
		| UserId      |
		| any user id |
	Then the http status code should be 200