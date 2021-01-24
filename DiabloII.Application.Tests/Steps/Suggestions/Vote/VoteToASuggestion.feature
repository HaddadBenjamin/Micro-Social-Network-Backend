Feature: VoteToASuggestion
	In order to detect what are the most important suggestions
	As a user
	I want to vote to a suggestion

@suggestions
Scenario: Vote to a suggestion happy path
	Given I create the suggestions with the following informations
		| Content                   | UserId      |
		| You should add more items | any user id |
	And I vote to the suggestion "You should add more items"
		| IsPositive | UserId        |
		| true       | other user id |
	And I get the vote created by "other user id" from the suggestion "You should add more items"
	Then the http status code should be 200
	And the voted suggestion should be
		| CreatedBy     | IsPositive |
		| other user id | true       |