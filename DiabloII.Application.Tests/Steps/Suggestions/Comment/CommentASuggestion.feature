Feature: CommentASuggestion
	In order to understand the opinions of my users
	As a developer
	I want my users comment their suggestions

@suggestions
Scenario: Comment a suggestion happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	And I comment the suggestion "You should add more items"
		| Comment                                            | UserId      |
		| I'm getting bored of the current items of the game | any user id |
	And I get the comment "I'm getting bored of the current items of the game" from the suggestion "You should add more items"
	Then the http status code should be 200
	And the commented suggestion should be
		| CreatedBy   | Comment                                            |
		| any user id | I'm getting bored of the current items of the game |