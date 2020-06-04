Feature: GetAUser
	In order to see get an user
	As a developper
	I want to call the users API

@users
Scenario: Get a user happy path
	Given I create the users with the following informations
		| Email                      | Id          |
		| DiabloIIenriched@gmail.com | any user id |
	When I get the created user
	Then the http status code should be 200
	And the user should be
		| Email                      | Id          |
		| DiabloIIenriched@gmail.com | any user id |