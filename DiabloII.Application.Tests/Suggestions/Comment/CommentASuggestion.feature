Feature: CommentASuggestion
	In order to understand the opinions of my users
	As a developer
	I want my users comment their suggestions

@suggestion
Scenario: Comment a suggestion happy path
	Given I create the suggestions with the following informations
	| Content                   | UserId      |
	| You should add more items | any user id |
	When I comment the suggestion "You should add more items"
	| Comment                                            | UserId      |
	| I'm getting bored of the current items of the game | any user id |
	Then the http status code should be 201
	And the commented suggestion should be
	| Content                   | CreatedBy   | Comments                                                       |
	| You should add more items | any user id | I'm getting bored of the current items of the game,any user id |
