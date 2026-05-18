Feature: VerifyResponseBody_UsingPreviousRequestFields

To add previous request fields in the response body, ploceholder with path can be used. 
If request body is:
{
	"car": {
		"id": 1
	}
}
Then placeholders {{car.id}} will be interpreted as 1.
Fields from all previous request can be used in placeholders.

To validate this behaviour following endpoints are used:
Endpoint POST /cars requires and returns the same body.
"""
{
	"car": { 
		"id": number, 
		"name": string 
	}
}
"""

Endpoint POST /tires requires and returns the same body.
"""
{
	"tire": { 
		"id": number, 
		"creationDate": dateTime,
		"manufacturerCode": guid,
		"carId": number
	}
}
"""

Background:
	Given I set sub Uri to "/api/v1"
	And I have an HTTP "POST" "/cars" request with body
		"""
		{
			"car": { 
				"id": 1, 
				"name": "This is name" 
			}
		}
		"""
	And I have sent the request

Scenario: the response body should match. Verifies that response has same fields as the request.
	Then the response status code should be 200
	And the response body should match
		"""
		{
			"car": { 
				"id": {{car.id}}, 
				"name": "{{car.name}}" 
			}
		}
		"""

Scenario: the response body should match. Verifies that previous request placeholders are replaced.
	Given I have an HTTP "POST" "/cars/1/tires" request with body
		"""
		{
			"tire": { 
				"id": 1, 
				"creationDate": "2011-10-05T14:48:00.000Z",
				"manufacturerCode": "ba100298-f12f-49c0-a5b3-38940c788254",
				"carId": {{car.id}}
			}
		}
		"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
		"""
		{
			"tire": { 
				"id": {{tire.id}}, 
				"creationDate": "2011-10-05T14:48:00.000Z",
				"manufacturerCode": "{{tire.manufacturerCode}}",
				"carId": {{car.id}}
			}
		}
		"""