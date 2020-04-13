Feature: CreateASuggestion
	In order to create suggestion to drive the development of the application
	As a user
	I want to call the suggestions API

@suggestion, @common
Scenario: Create a suggestion happy path
	When I create a suggestion with the following informations
	| Content                   | UserId      |
	| You should add more items | any user id |
	Then the http status code should be 201
	And the created suggestion should be
	| Content                   | CreatedBy   | PositiveVoteCount | NegativeVoteCount |
	| You should add more items | any user id | 0                 | 0                 |