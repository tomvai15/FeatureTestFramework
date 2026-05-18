Feature: SetSubUri

Scenario: I set sub Uri. Appends sub uri and adjusts slashes.
	Given I set sub Uri to "<SubUri>"
	And I have an HTTP "GET" "/cars/1" request
	When I send the request
	Then the response status code should be 200
Examples:
	| SubUri  |
	| api/v1/ |
	| /api/v1 |