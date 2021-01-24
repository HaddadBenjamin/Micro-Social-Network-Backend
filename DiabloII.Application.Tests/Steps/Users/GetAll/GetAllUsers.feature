Feature: GetAllUsers
	In order to see all the my users
	As a developper
	I want to call the users API

@users
Scenario: Get all the users happy path
	Given I create the users with the following informations
		| Email                      | Id            |
		| DiabloIIenriched@gmail.com | any user id   |
		| firefouks@gmail.com        | other user id |
	When I get all the users
	Then the http status code should be 200
	And all the users should be
		| Email                      | Id            |
		| DiabloIIenriched@gmail.com | any user id   |
		| firefouks@gmail.com        | other user id |