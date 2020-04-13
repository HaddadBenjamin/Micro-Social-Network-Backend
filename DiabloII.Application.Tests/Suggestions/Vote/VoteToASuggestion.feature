Feature: VoteToASuggestion
	In order to detect what are the most important suggestions
	As a user
	I want to vote to a suggestion

@suggestion
Scenario: Vote to a suggestion happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	When I vote to the suggestion "You should add more items"
		| IsPositive | UserId        |
		| true       | other user id |
	Then the http status code should be 201
	And the voted suggestion should be
		| Content                   | CreatedBy   | PositiveVoteCount | NegativeVoteCount |
		| You should add more items | any user id | 1                 | 0                 |
