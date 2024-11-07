@ignore
Feature: auth

Scenario: PostOrder. Gets user and creates an order.
	Given I have an HTTP "POST" "/create-order" request with body
		"""
		{	
			"userId": 1,
			"orderName": "TestOrder"	
		}
		"""
	And I have a valid auth token with claims
		| Type | Value |
		| role | admin |
	And service "UserService" returns response 
	"""
		{	
			"userId": 1,
			"orderName": "TestOrder"	
		}
	"""
	When I send the request
	Then the response status code should be 200
	And the response body should match
	"""
		{	
			"userId": 1,
			"orderName": "TestOrder",
			"creationDate": "{{CloseToNow}}"
		}
	"""

Scenario: GetUserInformation. Returns 404 when not found.
	Given I have an HTTP "GET" "/UserInformation/1" request with body
	And service "UserService" returns response with status code 200
	"""
	{
		"name": "John",
		"surname": "Doe"
	}
	"""
	And service "UserService" returns response with status code 404
	When I send the request
	Then the response status code should be 404