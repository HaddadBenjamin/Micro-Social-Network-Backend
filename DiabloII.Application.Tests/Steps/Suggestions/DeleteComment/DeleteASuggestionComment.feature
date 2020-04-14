Feature: DeleteASuggestionComment
	In order to delete a suggestion comment that I don't like
	As a user
	I want to call the suggestions API

@suggestions
Scenario: Delete a suggestion comment happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	And I comment the suggestion "You should add more items"
		| Comment               | UserId      |
		| more items pleaaaase! | any user id |
	When I delete the suggestion comment "more items pleaaaase!" from the suggestion "You should add more items"
		| UserId      |
		| any user id |
	Then the http status code should be 200