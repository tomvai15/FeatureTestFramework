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