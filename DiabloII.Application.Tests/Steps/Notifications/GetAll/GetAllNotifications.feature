Feature: GetAllNotifications
	In order to see all notifications that my application send to my users
	As a developper
	I want to call the notifications API

@notifications
Scenario: Get all the notifications happy path
	When I create the notifications with the following informations
		| Title            | Content                                                   | Type              |
		| Patch note 1.863 | A new map have bene created and some mobs have been added | PatchNotes        |
		| New suggestion   | You should add more maps in this game                     | CreatedSuggestion |
	When I get all the notifications
	Then the http status code should be 200
	And all the notifications should be
		| Patch note 1.863 | A new map have bene created and some mobs have been added | PatchNotes        |
		| New suggestion   | You should add more maps in this game                     | CreatedSuggestion |