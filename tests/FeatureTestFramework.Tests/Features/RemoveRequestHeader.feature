Feature: RemoveRequestHeader

Request header can be removed using 'the header removed is' or 'I remove header' steps.

To validate this behaviour following endpoints are used:
Endpoint /need-header/{header-name} requires to provide header, which has name of header-name.

Background:
	Given I set sub Uri to "/api/v1/headers"

Scenario: the header removed is. Removes header from the request
	Given I have an HTTP "GET" "/needs-header/Required-Header" request
	And I add a request header "Required-Header"
	And the header removed is "Required-Header"
	When I send the request
	Then the response status code should be 400
	
Scenario: I remove header. Removes header from the request
	Given I have an HTTP "GET" "/needs-header/Required-Header" request
	And I add a request header "Required-Header"
	And I remove header "Required-Header"
	When I send the request
	Then the response status code should be 400