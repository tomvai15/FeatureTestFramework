Feature: CreateOrder

Scenario: PostOrder. Gets user and creates an order.
	Given I have an HTTP "POST" "/create-order" request with body
		"""
		{	
			"userId": 1,
			"orderName": "TestOrder"	
		}
		"""
	When I send the request
	Then service "UserService" should be called with HTTP "GET" "/users/1"
	And service "OrderService" should be called with HTTP "POST" "/orders" and body
		"""
		{	
			"userName": "TestName",
			"orderName": "TestOrder"	
		}
		"""
	And the response status code should be 200