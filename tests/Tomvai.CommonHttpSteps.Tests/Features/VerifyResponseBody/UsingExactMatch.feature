Feature: VerifyResponseBody_UsingExactMatch

To verify response body with exact body, step `the response body should match` can be used.

To validate this behaviour following endpoints are used:
Endpoint GET /healthcheck returns: 'Healthy'
Endpoints GET /car/1 returns body:
{
	"id": 1,
	"name": "Car"
}

Scenario: the response body should match. Verifies that json response body is asserted
	Given I have an HTTP "GET" "/api/v1/cars/1" request
	When I send the request
	Then the response status code should be 200
	And the response body should match
		"""
		{
			"id": 1,
			"name": "Car"
		}
		"""

Scenario: the response body should match. Verifies that partial body is verified
	Given I have an HTTP "GET" "/api/v1/cars/1" request
	When I send the request
	Then the response status code should be 200
	And the response body should match
		"""
		{
			"name": "Car"
		}
		"""