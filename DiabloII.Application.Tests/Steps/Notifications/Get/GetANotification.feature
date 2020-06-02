Feature: GetANotification
	In order to see the last notification sended from this application to my users
	As a developper
	I want to call the notifications API

@notifications
Scenario: Get a notification happy path
	When I create the notifications with the following informations
		| Title            | Content                                                   | Type              |
		| Patch note 1.863 | A new map have been created and some mobs have been added | PatchNotes        |
	When I get the created notification
	Then the http status code should be 200
	And the notification should be
		| Title            | Content                                                   | Type              |
		| Patch note 1.863 | A new map have been created and some mobs have been added | PatchNotes        |
