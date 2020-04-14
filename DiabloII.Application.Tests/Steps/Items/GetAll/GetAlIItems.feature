Feature: GetAllItems
	In order to see all the items
	As a user
	I want to call the items API

@items
Scenario: Get all the items happy path
	When I get all the items
	Then the http status code should be 200