Feature: AddRequestHeader

Request header with generated value can be appended using 'I add request header' step
Request header with specific value can be appended using 'I add request header with value' step

To validate this behaviour following endpoints are used:
Endpoint GET /need-header/{header-name} requires to provide header with name of {header-name}
Endpoint GET /protected/{password} requires to provide Authorization header with value of {password}

Background:
	Given I set sub Uri to "/api/v1/headers"

Scenario: I add a request header. Adds header with generated value
	Given I have an HTTP "GET" "/needs-header/mandatory-header" request
	And I add a request header "<Header>"
	When I send the request
	Then the response status code should be <Status>
Examples:
	| Header           | Status |
	| mandatory-header | 200    |
	| other-header     | 400    |

Scenario: I add a request header. Adds header with specified value
	Given I have an HTTP "GET" "/protected/correct-password" request
	And I add a request header "Authorization" with value "<Password>"
	When I send the request
	Then the response status code should be <Status>
Examples:
	| Password           | Status |
	| correct-password   | 200    |
	| incorrect-password | 401    |
