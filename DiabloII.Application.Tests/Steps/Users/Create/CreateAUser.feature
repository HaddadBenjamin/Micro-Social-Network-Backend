Feature: CreateAUser
	In order to create user to be idenfified
	As a user
	I want to call the users API

@users
Scenario: Create a user happy path
	When I create the users with the following informations
		| Email                      | Id     |
		| DiabloIIenriched@gmail.com | any id |
	Then the http status code should be 201
	And the created user should be
		| Email                      | Id     |
		| DiabloIIenriched@gmail.com | any id |