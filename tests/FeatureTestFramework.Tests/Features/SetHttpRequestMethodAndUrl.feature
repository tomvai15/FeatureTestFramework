Feature: SetHttpRequestMethodAndUrl

Scenario: I have an HTTP request. Calls GET endpoint.
	Given I have an HTTP "GET" "/api/v1/cars/1" request
	When I send the request
	Then the response status code should be 200

Scenario: I have an HTTP request. Calls DELETE endpoint.
	Given I have an HTTP "DELETE" "/api/v1/cars/1" request
	When I send the request
	Then the response status code should be 200

Scenario: I have an HTTP request. Calls PUT endpoint.
	Given I have an HTTP "PUT" "/api/v1/cars/1" request with body
		"""
		{	
			"id": 1,
			"name": "Car"	
		}
		"""
	When I send the request
	Then the response status code should be 200

Scenario: I have an HTTP request. Calls POST endpoint.
	Given I have an HTTP "POST" "/api/v1/cars" request with body
		"""
		{	
			"car": {
				"id": 1,
				"name": "Car"	
			}
		}
		"""
	When I send the request
	Then the response status code should be 200

Scenario: I have an HTTP request. Replaces placeholders in Url.
	Given I have an HTTP "GET" "<Url>" request
	When I send the request
	Then the response status code should be 200
Examples:
	| Url                                                                |
	| /api/v1/cars/{{AnyNumber}}                                         |
	| /api/v1/cars/{{AnyNumber}}/tires/{{AnyNumber}}                     |
	| /api/v1/cars/{{AnyNumber}}/tires/{{AnyNumber}}/bolts/{{AnyNumber}} |

