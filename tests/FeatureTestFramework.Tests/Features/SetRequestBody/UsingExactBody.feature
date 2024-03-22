Feature: SetRequestBody_UsingExactBody

Request body can be set with specific value using 'I have an HTTP request with body' step. 

To validate this behaviour foLLowing endpoints are used: 
Endpoint POST /cars requires body: 
{
	"id": int, 
	"name": string
}

Background:
	Given I set sub Uri to "/api/v1"

Scenario: I have an HTTP request with body. Sends request with specified body
	Given I have an HTTP "POST" "/cars" request with body
		"""
			{ 
				"car": { 
					"id": 1, 
					"name": "This is name" 
				} 
			} 
		"""
	When I send the request
	Then the response status code should be 200

