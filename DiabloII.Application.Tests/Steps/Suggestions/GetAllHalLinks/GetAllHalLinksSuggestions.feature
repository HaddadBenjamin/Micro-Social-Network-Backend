Feature: GetAllHalLinksSuggestions
	In order to determine which part of the UI is showed and on which links I could let the users navigate
	As a developper
	I want to retrieve the suggestions hal links

@suggestions
Scenario: Get all suggestions should get hal links
	Given I am "any user id"
	And I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	And I comment the suggestion "You should add more items"
		| Comment               | UserId      |
		| more items pleaaaase! | any user id |
	And I vote to the suggestion "You should add more items"
		| IsPositive | UserId        |
		| true       | other user id |
	And I am "<SearchUser>"
	When I get all suggestions with hal links
	Then the http status code should be 200
	And I should retrieve "<HalLinks>" as suggestions hal links

	Examples:
		| SearchUser    | HalLinks                                                                      |
		| any user id   | suggestion_create;suggestion_delete;vote_create;comment_create;comment_delete |
		| other user id | suggestion_create                                                             |