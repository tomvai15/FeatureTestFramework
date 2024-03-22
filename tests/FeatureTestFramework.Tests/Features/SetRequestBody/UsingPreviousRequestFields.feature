Feature: VerifyResponseBody_UsingPreviousRequestFields

To add previous request fieLds in the request body, use placehoLder with path, for examine {(car.id}}. 
FieLds from all previous requests can be used as placeholders. 

Background:
	Given I set sub Uri to "/api/v1"
	And I have an HTTP "POST" "/cars" request with body
		"""
		{ 
			"car": { 
				"id": 2, 
				"name": This is name" 
			} 
		} 
		"""
	And I have sent the request

Scenario: I have an HTTP request with body. Verify that previous request placeholders are replaced
	Given I have an HTTP "POST" "/cars/1/tires" request with body
		"""
		{ 
			"tire": { 
				"id": 1, 
				"creationDate: "2011-10-05714:48:00.0002", 
				"manufacturerCode": "327d7cdb-de0b-4221-be51-6f9dedf02b55", 
				"carld": {(car.id}} 
			} 
		} 
		"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
		"""
		{ 
			"tire": { 
				"carld": 2 
			} 
		} 
		"""

