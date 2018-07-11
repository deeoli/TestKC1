Feature: CodifyTests - Test Framework Example
		

Scenario: User can sign up if not a member
	Given a user is not logged in
	When the user signs up with the following details:
	| username | email           | password     |
	| DeTest01 | detest01@aa.com | LoveToTest01 |       
	Then the user is automatically logged in
	And the users name is displayed

Scenario: User can log in
	Given a user is not logged in 
	When the user signs in using:
	| username | email           | password     |
	| DeTest01 | detest01@aa.com | LoveToTest01 |
	Then the user is logged in
	And the users name is displayed

Scenario: Home page shows global feeds
	Given a user is not logged in
	When the home page is shown
	Then the global feeds and popular tags are displayed

Scenario: Non-member is directed to sign-up area if they want to view an article
	Given a user is not logged in
	When a user tries to like an article
	Then the user is directed to the Sign up area

Scenario: User name most be unique
	Given a user is not logged in
	When the user tries to sign up with a username that already exists:
	| username | email           | password     |
	| DeTest01 | detest00101@aa.com | LoveToTest01 |
	Then a validation error is displayed with message "username has already been taken"

Scenario: User email be unique
	Given a user is not logged in
	When the user tries to sign up with an email address that already exists:
	| username | email           | password     |
	| DeTest0111 | detest01@aa.com | LoveToTest01 |
	Then a validation error is displayed with message "email has already been taken"

Scenario: Password most have the correct length
	Given a user is not logged in
	When the user tries to sign up with a password that is too short:
		| username  | email            | password |
		| DeTest011 | detest011@aa.com | Test     |
	Then a validation error is displayed with message "password is too short (minimum is 8 characters)"

Scenario: An empty your feeds area is show for new users
	Given a user is not logged in
	When the user signs up with the following details:
	| username | email           | password     |
	| DeTest01 | detest01@aa.com | LoveToTest01 | 
	Then the Your Feeds section should be displayed by default
	And the Your Feeds section should be empty

Scenario: Article information is displayed in the article section of the use that created it
	Given a user is logged in as:
	| username | email           | password     |
	| DeTest01 | detest01@aa.com | LoveToTest01 | 
	When the user creates a new article
	Then the Article section is displayed with the article information
	And the Comment section is displayed 
