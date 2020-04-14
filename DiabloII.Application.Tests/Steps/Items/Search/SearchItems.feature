Feature: SearchItems
	In order to search the items
	As a user
	I want to call the items API

@items
Scenario: Search items happy path
	When I search the items
		| SubCategories |
		| Armor         |
	Then the http status code should be 200