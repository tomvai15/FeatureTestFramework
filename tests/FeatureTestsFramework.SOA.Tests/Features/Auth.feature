Feature: Auth

Scenario: I have a valid auth token. Adds Jwt token to the Authorization header
	Given I have an HTTP "GET" "api/v1/protected-by-jwt" request
	And I have a valid auth token
	When I send the request
	Then the response status code should be 200