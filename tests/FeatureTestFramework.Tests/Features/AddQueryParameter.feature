Feature: AddQueryParameter

Query parameters (?parameter=value) can be appended using 'I add query parameter with value' step.
Multiple query parameters are supported.

To validate this behaviour following endpoints are used:
Endpoint /validate-query/{query-value} requires to provide query parameter, which matches query-value.
Endpoint /validate-query/{query-value1}/{query-value2} requires to provide query parameters, which match query-value1 and query-value2.

Scenario: I add a query parameter with value. Verifies that specified query parameter was added
	Given I have an HTTP "GET" "/validate-query/valid-query" request
	And I add a query parameter "include" with value "<Include>"
	When I send the request
	Then the response status code should be <Status>
Examples:
	| Include       | Status |
	| valid-query   | 200    |
	| invalid-query | 400    |

Scenario: I add a query parameter with value. Verifies that specified query parameters were added
	Given I have an HTTP "GET" "/validate-query/valid-query1/valid-query2" request
	And I add a query parameter "include1" with value "<Include1>"
	And I add a query parameter "include2" with value "<Include2>"
	When I send the request
	Then the response status code should be <Status>
Examples:
	| Include1      | Include2      | Status |
	| valid-query1  | valid-query2  | 200    |
	| invalid-query | valid-query2  | 400    |
	| valid-query1  | invalid-query | 400    |
	| invalid-query | invalid-query | 400    |