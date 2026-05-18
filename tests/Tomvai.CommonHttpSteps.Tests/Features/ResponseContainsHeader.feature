Feature: ResponseContainsHeader

To check if response has specific header use step 'the response headers should contain name'
To check if response doesn't have specific header use step 'the response headers should not contain'
To check if response has header with specific value use step 'the response headers should contain name and value'
or use step 'the response header should be.'

To validate this behaviour following endpoints are used:
Endpoint /returns-header/{header-name} returns response with header {header-name}
Endpoint /cars/1 returns response with header "Content-Type" and value "application/json; charset=utf-8"

Background:
	Given I set sub Uri to "/api/v1"

Scenario: the response headers should contain name. Validates that response contains header from table
	Given I have an HTTP "GET" "/headers/returns-header/Content-Type" request
	When I send the request
	Then the response headers should contain name
		| Name         |
		| Content-Type |

Scenario: the response headers should not contain. Validates that response doesn't contains header from table
	Given I have an HTTP "GET" "/headers/returns-header/Content-Type" request
	When I send the request
	Then the response headers should not contain
		| Name               |
		| Blacklisted-Header |

Scenario: the response headers should contain name and value. Validates that response contains header from table with specific value
	Given I have an HTTP "GET" "/cars/1" request
	When I send the request
	Then the response headers should contain name and value
		| Header       | Value                           |
		| Content-Type | application/json; charset=utf-8 |

Scenario: the response header should be. Validates that response contains header with specific value
	Given I have an HTTP "GET" "/cars/1" request
	When I send the request
	Then the response header "Content-Type" should be "application/json; charset=utf-8"