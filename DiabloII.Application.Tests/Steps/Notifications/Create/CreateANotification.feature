Feature: CreateANotification
	In order to create a notification to notify my users of something have happen
	As a developper
	I want to call the notifications API

@notifications
Scenario: Create a notification happy path
	When I create the notifications with the following informations
		| Title            | Content                                                   | Type       |
		| Patch note 1.863 | A new map have been created and some mobs have been added | PatchNotes |
	Then the http status code should be 201
	And the created notification should be
		| Title            | Content                                                   | Type       |
		| Patch note 1.863 | A new map have been created and some mobs have been added | PatchNotes |