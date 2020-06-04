Feature: UpdateAUser
	In order to update my personal information
	As a user
	I want to call the users API

@users
Scenario: Update a user happy path
	Given I create the users with the following informations
		| Email                      | Id          |
		| DiabloIIenriched@gmail.com | any user id |
	And I update the user "DiabloIIenriched@gmail.com" with the following informations
		| Email               | AcceptedNotifications                                         | AcceptedNotifiers |
		| firefouks@gmail.com | Other,PatchNotes,CreatedSuggestion,NewCommentOnYourSuggestion | InApp,Mail        |
	When I get the updated user
	Then the http status code should be 200
	And the updated user should be
		| Email               | AcceptedNotifications                                         | AcceptedNotifiers |
		| firefouks@gmail.com | Other,PatchNotes,CreatedSuggestion,NewCommentOnYourSuggestion | InApp,Mail        |